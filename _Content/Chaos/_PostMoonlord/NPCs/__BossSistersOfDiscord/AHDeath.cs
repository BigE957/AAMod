using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Music;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossSistersOfDiscord
{
    public class AHDeath : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sisters Defeat");
            NPCID.Sets.ShouldBeCountedAsBoss[NPC.type] = true;
            this.HideFromBestiary();
        }

        public override void SetDefaults()
        {
            NPC.dontTakeDamage = true;
            NPC.lifeMax = 1;
            NPC.width = 100;
            NPC.height = 100;
            NPC.friendly = false;
            NPC.lifeMax = 1;
            NPC.dontTakeDamage = true;
            NPC.noGravity = true;
            NPC.aiStyle = -1;
            NPC.timeLeft = 10;
            Music = MusicManagementSystem.MusicSlots["Sisters_Outro"];
            NPC.boss = false;

            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
        }
        public override void AI()
        {
            NPC.ai[1]++;
            NPC.TargetClosest();
            Player player = Main.player[NPC.target];

            if (AAConfigClient.Instance.NoBossDialogue)
            {
                NPC.SetEventFlagCleared(ref AAWorld.downedSisters, -1);
                NPC.active = false;
                return;
            }

            NPC.Center = player.Center;
            NPC.netOffset = Vector2.Zero;

            if (NPC.ai[1] == 240)
            {
                if (AAWorld.downedSisters)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SistersOfDiscord.Defeat.1"), new Color(102, 20, 48));
                }
                else
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SistersOfDiscord.Defeat.First.1"), new Color(72, 78, 117));
                }
            }

            if (NPC.ai[1] == 360)
            {
                if (AAWorld.downedSisters)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SistersOfDiscord.Defeat.2"), new Color(72, 78, 117));
                }
                else
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SistersOfDiscord.Defeat.First.2.Front") + (player.Male ? Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.male") : Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.fimale")) + Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SistersOfDiscord.Defeat.First.2.Back"), new Color(102, 20, 48));
                }
            }

            if (NPC.ai[1] == 600)
            {
                if (AAWorld.downedSisters)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SistersOfDiscord.Defeat.3"), new Color(102, 20, 48));
                    NPC.active = false;
                }
                else
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SistersOfDiscord.Defeat.First.3"), new Color(72, 78, 117));
                }
            }
            
            if (NPC.ai[1] == 840)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SistersOfDiscord.Defeat.First.4"), new Color(102, 20, 48));
                NPC.SetEventFlagCleared(ref AAWorld.downedSisters, -1);
                NPC.active = false;
            }
        }
    }
}