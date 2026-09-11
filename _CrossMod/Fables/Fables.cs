using AAModClassic._Content.Desert.___PreHardmode.NPCs.__BossDesertDjinn;
using AAModClassic.Utilities;
using MonoMod.RuntimeDetour;
using System;
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
        private static dynamic _desertDjinnTextbox;
        private static Action _returnToNautilusMain;

        delegate object orig_GetRandomMainTextbox();
        delegate object hook_GetRandomMainTextbox(orig_GetRandomMainTextbox orig);
        private static Hook _mainTextboxHook;

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

            dynamic regularSpeechVoice = asm.GetType("CalamityFables.Content.Boss.SeaKnightMiniboss.SirNautilus").GetField("RegularSpeech", BindingFlags.Public | BindingFlags.Static).GetValue(null);

            dynamic portraits = _dialogueType.GetField("portraits", BindingFlags.Public | BindingFlags.Static).GetValue(null);
            dynamic myPortrait = portraits["pog"];

            const string locKey = "Mods.AAModClassic.CrossMod.Fables.Nautilus.DesertDjinnDefeated.Dialogue";

            dynamic mySentence = Activator.CreateInstance(_sentenceType, [480f, regularSpeechVoice, "placeholder"]);
            _plainTextField.SetValue(mySentence, Language.GetText(locKey));
            mySentence.UpdateLocalization();

            _desertDjinnTextbox = Activator.CreateInstance(_textboxInfoType, [mySentence, myPortrait, (Action)null, true, null]);
            _boxClickField.SetValue(_desertDjinnTextbox, _returnToNautilusMain);

            dynamic startFightBtn = _dialogueType.GetField("StartFightButton", BindingFlags.Public | BindingFlags.Static).GetValue(null);
            dynamic loreBtn = _dialogueType.GetField("LoreButton", BindingFlags.Public | BindingFlags.Static).GetValue(null);
            _desertDjinnTextbox.AddButton(startFightBtn).AddButton(loreBtn);

            MethodInfo original = _dialogueType.GetMethod("GetRandomMainTextbox", BindingFlags.Public | BindingFlags.Static);
            _mainTextboxHook = new Hook(original, new hook_GetRandomMainTextbox(Hook_GetRandomMainTextbox));
        }

        private static object Hook_GetRandomMainTextbox(orig_GetRandomMainTextbox orig)
        {
            var tracker = Main.LocalPlayer.GetModPlayer<NautilusDialogueTracker>();
            if (NPCExtensions.BeenKilled<DesertDjinn>() && !tracker.HasSpokenAboutDesertDjinn)
            {
                tracker.HasSpokenAboutDesertDjinn = true;
                return _desertDjinnTextbox;
            }
            return orig();
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
