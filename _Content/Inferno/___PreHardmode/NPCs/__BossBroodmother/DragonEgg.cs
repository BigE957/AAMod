using AAModClassic._Content.Inferno.World.Biomes;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Inferno.___PreHardmode.NPCs.__BossBroodmother
{
    [AutoloadBossHead]
    public class DragonEgg : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dragon Egg");
            this.HideFromBestiary();
        }
        public override void SetDefaults()
        {
            NPC.width = 34;
            NPC.height = 34;
            NPC.aiStyle = -1;
            NPC.damage = 0;
            NPC.defense = 20;
            NPC.lavaImmune = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.lifeMax = 50;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 0f;
            NPC.knockBackResist = .2f;
            NPC.npcSlots = 0f;
            SpawnModBiomes = new int[1] { ModContent.GetInstance<InfernoBiome>().Type };
            NPC.BossBar = Main.BigBossProgressBar.NeverValid;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
			bool isDead = NPC.life <= 0;
            if (isDead && !Main.dedServ)
            {
				for(int m = 0; m < 4; m++)
				{
					Vector2 offset = new Vector2(Main.rand.Next(NPC.width), Main.rand.Next(NPC.height));
					Gore.NewGore(NPC.GetSource_OnHurt(null), NPC.position + offset, NPC.velocity * 0.2f, Mod.Find<ModGore>("BroodGorePlate3").Type, 1f); //reused brood gore, it looks right for the egg
				}
            }
			for (int m = 0; m < (isDead ? 20 : 5); m++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Torch, NPC.velocity.X * 0.2f, NPC.velocity.Y * 0.2f, 100, Color.White, 1.3f);
			}
        }
        
        public override void AI()
        {
            if (NPC.velocity.Y == 0f)
            {
                NPC.velocity.X = NPC.velocity.X * 0.9f;
                NPC.rotation += NPC.velocity.X * 0.02f;
            }
            else
            {
                NPC.velocity.X = NPC.velocity.X * 0.99f;
                NPC.rotation += NPC.velocity.X * 0.04f;
            }
            int hatchTimer = 900;
            if (Main.expertMode)
            {
                hatchTimer = 700;
            }
            if (NPC.justHit)
            {
                NPC.ai[3] -= Main.rand.Next(10, 21);
                if (!Main.expertMode)
                {
                    NPC.ai[3] -= Main.rand.Next(10, 21);
                }
            }
            NPC.ai[3] += 1f;
            if (NPC.ai[3] >= hatchTimer)
            {
                NPC.Transform(ModContent.NPCType<Broodmini>());
            }
            if (Main.netMode != NetmodeID.MultiplayerClient && NPC.velocity.Y == 0f && Math.Abs(NPC.velocity.X) < 0.2 && NPC.ai[3] >= hatchTimer * 0.75)
            {
                float wiggleAmount = NPC.ai[3] - hatchTimer * 0.75f;
                wiggleAmount /= hatchTimer * 0.25f;
                if (Main.rand.Next(-10, 120) < wiggleAmount * 100f)
                {
                    NPC.velocity.Y = NPC.velocity.Y - Main.rand.Next(20, 40) * 0.025f;
                    NPC.velocity.X = NPC.velocity.X + Main.rand.Next(-20, 20) * 0.025f;
                    NPC.velocity *= 1f + wiggleAmount * 2f;
                    NPC.netUpdate = true;
                    return;
                }
            }
        }

		public static Color GetGlowAlpha()
		{
			return ColorUtils.COLOR_GLOWPULSE;// new Color(255, 255, 255) * ((float)Main.mouseTextColor / 255f);
		}

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
			BaseDrawing.DrawTexture(spriteBatch, ModContent.Request<Texture2D>(Texture + "_Glow").Value, 0, NPC, GetGlowAlpha());
        }		
    }
}