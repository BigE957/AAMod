using AAModClassic._Content.Desert.___PreHardmode.NPCs.__BossDesertDjinn;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using MonoMod.RuntimeDetour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace AAModClassic._CrossMod.Fables
{
    public class Fables : ModSystem
    {
        internal static Mod CalamityFables = null;
        public static bool IsEnabled => CalamityFables != null;

        private static Type _dialogueType, _textboxInfoType, _buttonInfoType, _sentenceType;
        private static FieldInfo _plainTextField, _boxClickField;
        private static dynamic _theUI;
        private static readonly Dictionary<string, dynamic> _newNautilusDialogue = [];
        private static Action _returnToNautilusMain;

        delegate object orig_GetRandomMainTextbox();
        delegate object hook_GetRandomMainTextbox(orig_GetRandomMainTextbox orig);
        private static Hook _mainTextboxHook;

        private static PropertyInfo _readDoppelgangerProp;
        private static int _maskSlot, _bodySlot, _legsSlot;

        private static HashSet<object> _postDefeatTextboxSet;

        public override void Load()
        {
            ModLoader.TryGetMod("CalamityFables", out CalamityFables);
        }

        public override void Unload() => _mainTextboxHook?.Dispose();

        public override void PostSetupContent()
        {
            if (!IsEnabled)
                return;

            try 
            { 
                SetupCrossModDialogue(); 
            }
            catch (Exception e) 
            { 
                Mod.Logger.Warn($"CalamityFables cross-mod dialogue hook failed: {e}"); 
            }
        }

        public static object Call(params object[] args) => CalamityFables?.Call(args);

        //Nautilus Portraits
        /*
        frontfacing, neutral, laughing, angry, 
        angryhands, enraged, solemn, bored, curious, 
        pog, starstruck, starstruckhands, surprised, 
        surprisedhands, suspicious, unamused, shocked, 
        hollow, nerd, uncanny, shellshocked, fury.
        */

        private static void SetupCrossModDialogue()
        {
            Assembly asm = CalamityFables.Code;

            _dialogueType = asm.GetType("CalamityFables.Content.Boss.SeaKnightMiniboss.SirNautilusDialogue");
            _textboxInfoType = asm.GetType("CalamityFables.Content.UI.TextboxInfo");
            _sentenceType = asm.GetType("CalamityFables.Core.AwesomeSentence");

            _plainTextField = _sentenceType.GetField("plainText", BindingFlags.NonPublic | BindingFlags.Instance);
            _boxClickField = _textboxInfoType.GetField("clickEvent", BindingFlags.Public | BindingFlags.Instance);

            _theUI = asm.GetType("CalamityFables.Content.UI.CoolDialogueUIManager").GetField("theUI", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);

            MethodInfo switchToMain = _dialogueType.GetMethod("SwitchToMainTextbox", BindingFlags.Public | BindingFlags.Static);
            _returnToNautilusMain = (Action)Delegate.CreateDelegate(typeof(Action), switchToMain);

            _readDoppelgangerProp = _dialogueType.GetProperty("ReadThroughDoppelgangerEasterEgg", BindingFlags.Public | BindingFlags.Static);

            _maskSlot = EquipLoader.GetEquipSlot(CalamityFables, "SirNautilusBossMask", EquipType.Head);
            _bodySlot = EquipLoader.GetEquipSlot(CalamityFables, "SeaRiderTunic", EquipType.Body);
            _legsSlot = EquipLoader.GetEquipSlot(CalamityFables, "SeaRiderGreaves", EquipType.Legs);

            dynamic regularSpeechVoice = asm.GetType("CalamityFables.Content.Boss.SeaKnightMiniboss.SirNautilus").GetField("RegularSpeech", BindingFlags.Public | BindingFlags.Static).GetValue(null);

            dynamic portraits = _dialogueType.GetField("portraits", BindingFlags.Public | BindingFlags.Static).GetValue(null);

            const string locPath = "Mods.AAModClassic.CrossMod.Fables.Nautilus.";

            string[] newTopics = ["DesertDjinnActive.1", "DesertDjinnActive.2", "DesertDjinnDefeated.First", "DesertDjinnDefeated.Repeat"];
            string[] topicPortraits = ["angryhands", "enraged", "shocked", "laughing"];

            for(int i = 0; i < newTopics.Length; i++)
            {
                string topic = newTopics[i];
                string portrait = topicPortraits[i];
                dynamic mySentence = Activator.CreateInstance(_sentenceType, [480f, regularSpeechVoice, "placeholder"]);
                _plainTextField.SetValue(mySentence, Language.GetText(locPath + topic));
                mySentence.UpdateLocalization();

                _newNautilusDialogue.Add(topic, Activator.CreateInstance(_textboxInfoType, [mySentence, portraits[portrait], (Action)null, true, null]));
                _boxClickField.SetValue(_newNautilusDialogue[topic], _returnToNautilusMain);
            }

            dynamic startFightBtn = _dialogueType.GetField("StartFightButton", BindingFlags.Public | BindingFlags.Static).GetValue(null);
            dynamic loreBtn = _dialogueType.GetField("LoreButton", BindingFlags.Public | BindingFlags.Static).GetValue(null);
            foreach(var value in _newNautilusDialogue.Values)
                value.AddButton(startFightBtn).AddButton(loreBtn);

            Array postDefeatArr = (Array)_dialogueType.GetField("Main_PostDefeatTextboxes", BindingFlags.Public | BindingFlags.Static).GetValue(null);
            _postDefeatTextboxSet = new HashSet<object>(postDefeatArr.Cast<object>());

            MethodInfo original = _dialogueType.GetMethod("GetRandomMainTextbox", BindingFlags.Public | BindingFlags.Static);
            _mainTextboxHook = new Hook(original, new hook_GetRandomMainTextbox(Hook_GetRandomMainTextbox));
        }

        private static object Hook_GetRandomMainTextbox(orig_GetRandomMainTextbox orig)
        {
            if(WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                return orig();

            if (!(bool)CalamityFables.Call("progression.defeatednautilus"))
                return orig();

            if (IsDoppelganger())
                return orig();

            var tracker = Main.LocalPlayer.GetModPlayer<NautilusDialogueTracker>();

            // Slots between doppelganger and desert scourge in terms of priority
            bool killedDjinn = NPCExtensions.BeenKilled<DesertDjinn>();
            if (killedDjinn && !tracker.HasSpokenAboutDesertDjinn)
            {
                tracker.HasSpokenAboutDesertDjinn = true;
                return _newNautilusDialogue["DesertDjinnDefeated.First"];
            }

            object result = orig();

            // Has a chance to replace generic repeatable dialogue with dialogue abt Djinn
            if (true && (!killedDjinn || tracker.HasSpokenAboutDesertDjinn) && Main.rand.NextBool(4) && _postDefeatTextboxSet.Contains(result))
            {
                if(killedDjinn)
                    return _newNautilusDialogue["DesertDjinnDefeated.Repeat"];
                else
                    return _newNautilusDialogue["DesertDjinnActive." + (Main.rand.Next(2) + 1)];
            }

            return result;
        }

        private static bool IsDoppelganger()
        {
            Player p = Main.LocalPlayer;
            bool notYetRead = !(bool)_readDoppelgangerProp.GetValue(null);
            return notYetRead && p.head == _maskSlot && p.body == _bodySlot && p.legs == _legsSlot;
        }
    }

    internal class NautilusDialogueTracker : ModPlayer
    {
        internal bool HasSpokenAboutDesertDjinn = false;

        public override void SaveData(TagCompound tag)
        {
            tag.Add("DesertDjinn", HasSpokenAboutDesertDjinn);
        }

        public override void LoadData(TagCompound tag)
        {
            HasSpokenAboutDesertDjinn = tag.GetBool("DesertDjinn");
        }
    }
}
