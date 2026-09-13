using AAModClassic._Content.Chaos.Buffs;
using AAModClassic._Content.Inferno.Buffs;
using AAModClassic._Content.Mire.Buffs;
using AAModClassic._Content.Stars.Buffs;
using AAModClassic._Content.Terrarium.Buffs;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu;
using AAModClassic._Unreleased.Content.Void.Buffs;
using AAModClassic.Buffs;
using AAModClassic.Globals;
using AAModClassic.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._CrossMod.CalamityMod
{
    public class CalamityMod : ModSystem
    {
        internal static Mod Calamity = null;
        public override void Load()
        {
            if (!ModLoader.TryGetMod("CalamityMod", out Calamity))
                Calamity = null;
        }

        public override void AddRecipes()
        {
            if (IsEnabled)
                rogueClass = Calamity.Find<DamageClass>("RogueDamageClass");
        }
        public override void PostSetupContent()
        {
            if (Calamity != null)
            {
                if(!Main.dedServ)
                    astralDust = Calamity.Find<ModDust>("AstralChunkDust").Type;

                //Debuff Registering
                Calamity.Call("RegisterDebuff", FilePathUtils.TexturePath<SpearStuck_Buff>(), (NPC npc) => npc.GetGlobalNPC<AAModGlobalNPC>().Spear);
                Calamity.Call("RegisterDebuff", FilePathUtils.TexturePath<Impaled_Buff>(), (NPC npc) => npc.HasBuff<Impaled_Buff>());
                Calamity.Call("RegisterDebuff", FilePathUtils.TexturePath<Electrified_Buff>(), (NPC npc) => npc.GetGlobalNPC<AAModGlobalNPC>().Electrified);
                Calamity.Call("RegisterDebuff", FilePathUtils.TexturePath<BrokenArmor_Buff>(), (NPC npc) => npc.GetGlobalNPC<AAModGlobalNPC>().BrokenArmor);
                Calamity.Call("RegisterDebuff", FilePathUtils.TexturePath<InfinityScorch_Buff>(), (NPC npc) => npc.GetGlobalNPC<AAModGlobalNPC>().InfinityScorch);
                Calamity.Call("RegisterDebuff", FilePathUtils.TexturePath<RealityBent_Buff>(), (NPC npc) => npc.GetGlobalNPC<AAModGlobalNPC>().riftBent);
                Calamity.Call("RegisterDebuff", FilePathUtils.TexturePath<Terrablaze_Buff>(), (NPC npc) => npc.GetGlobalNPC<AAModGlobalNPC>().terraBlaze);
                Calamity.Call("RegisterDebuff", FilePathUtils.TexturePath<RadiumInferno_Buff>(), (NPC npc) => npc.HasBuff<RadiumInferno_Buff>());
                Calamity.Call("RegisterDebuff", FilePathUtils.TexturePath<Moonraze_Buff>(), (NPC npc) => npc.GetGlobalNPC<AAModGlobalNPC>().Moonraze);
                Calamity.Call("RegisterDebuff", FilePathUtils.TexturePath<HydraToxin_Buff>(), (NPC npc) => npc.GetGlobalNPC<AAModGlobalNPC>().Hydratoxin);
                Calamity.Call("RegisterDebuff", FilePathUtils.TexturePath<DragonFire_Buff>(), (NPC npc) => npc.HasBuff<DragonFire_Buff>());
                Calamity.Call("RegisterDebuff", FilePathUtils.TexturePath<DiscordianInferno_Buff>(), (NPC npc) => npc.GetGlobalNPC<AAModGlobalNPC>().DiscordInferno);

                //Drae-Talk
                string path = "Mods.AAModClassic.CrossMod.CalamityMod.DraeTalk.";
                string[] topics = ["ChaosBiomes", "Ancients", "Fungus", "Void", "Equinox"];
                foreach (string topic in topics)
                    Calamity.Call("CreateCodebreakerDialogOption", Language.GetTextValue(path + topic + ".Prompt"), Language.GetTextValue(path + topic + ".Dialogue"), () => true);
            }
        }

        private static readonly Dictionary<string, int> modItemCache = []; 
        private static readonly Dictionary<string, int> modProjectileCache = [];
        private static readonly Dictionary<string, int>  modBuffCache = [];

        public static int AstralChunkDust => astralDust;
        private static int astralDust = -1;

        public static bool IsEnabled => Calamity != null;
        public static bool IsRevengance => Calamity != null && (bool)Calamity.Call("GetDifficultyActive", "revengeance");
        public static bool IsDeath => Calamity != null && (bool)Calamity.Call("GetDifficultyActive", "death");

        private static DamageClass rogueClass = null;
        public static DamageClass RogueClass => rogueClass ?? (rogueClass = Calamity.Find<DamageClass>("RogueDamageClass"));

        public static object Call(params object[] args) => Calamity?.Call(args);
        
        public static int GetModItem(string name)
        {
            if (Calamity == null)
                return -1;

            if (modItemCache.TryGetValue(name, out int type))
                return type;

            if (Calamity.TryFind(name, out ModItem item))
            {
                modItemCache.Add(name, item.Type);
                return item.Type;
            }

            return -1;
        }

        public static int GetModProjectileType(string name)
        {
            if (Calamity == null)
                return -1;

            if (modProjectileCache.TryGetValue(name, out int type))
                return type;

            if (Calamity.TryFind(name, out ModProjectile projectile))
            {
                modProjectileCache.Add(name, projectile.Type);
                return projectile.Type;
            }

            return -1;
        }
    
        public static int GetModBuffType(string name)
        {
            if (Calamity == null)
                return -1;

            if (modBuffCache.TryGetValue(name, out int type))
                return type;

            if (Calamity.TryFind(name, out ModBuff buff))
            {
                modBuffCache.Add(name, buff.Type);
                return buff.Type;
            }

            return -1;
        }
    }
}
