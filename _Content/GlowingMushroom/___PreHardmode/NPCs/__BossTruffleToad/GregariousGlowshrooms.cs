using AAModClassic.Utilities;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.GlowingMushroom.___PreHardmode.NPCs.__BossTruffleToad
{
    public class GregariousGlowshrooms : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gregarious Glowshrooms");
            // gregarious is a real term used to describe mushrooms which are close together but not super packed
            // cluster usually refers to shrooms connected by the stem, so get outta here with that bullshit
            Main.npcFrameCount[NPC.type] = 7;
            this.HideFromBestiary();
        }

        public override void SetDefaults()
        {
            NPC.width = 48;
            NPC.height = 40;
            NPC.aiStyle = -1;
            NPC.damage = 30;
            NPC.defense = 40;
            NPC.lifeMax = 200;
            NPC.knockBackResist = 0f;
            NPC.npcSlots = 0f;
            NPC.aiStyle = -1;
            if (!NPC.IsABestiaryIconDummy)
                NPC.alpha = 255;
            NPC.dontTakeDamage = true;
            NPC.noTileCollide = false;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.SurfaceMushroom,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundMushroom,
            ]);
        }

        public override void AI()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.ai[0]++;
            }
            if (NPC.ai[0] < 600)
            {
                if (NPC.alpha > 0)
                {
                    NPC.alpha -= 4;
                }
                else
                {
                    NPC.alpha = 0;
                }
            }
            else
            {
                if (NPC.alpha < 255)
                {
                    NPC.alpha += 5;
                }
                else
                {
                    NPC.active = false;
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            if (NPC.frameCounter++ > 5)
            {
                NPC.frame.Y += frameHeight;
                NPC.frameCounter = 0;
            }
            if (NPC.frame.Y > frameHeight * 4)
            {
                NPC.frame.Y = frameHeight * 4;
            }
            if (NPC.IsABestiaryIconDummy)
                NPC.alpha = 0;
        }

        public override bool PreKill()
        {
            return false;
        }
    }
}