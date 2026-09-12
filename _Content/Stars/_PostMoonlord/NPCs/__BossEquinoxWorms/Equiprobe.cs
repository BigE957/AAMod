using AAModClassic._Content.Stars.Projectiles;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.NPCs.__BossEquinoxWorms
{
    public class Equiprobe : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Equiprobe");
            Main.npcFrameCount[NPC.type] = 1;
			this.HideFromBestiary();
		}

		public override void SetDefaults()
		{
            NPC.width = 38;
            NPC.height = 38;
            NPC.value = 0;
            NPC.npcSlots = 1;
            NPC.aiStyle = -1;
            NPC.lifeMax = 1200;
            NPC.defense = 120;
            NPC.damage = 80;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
            NPC.knockBackResist = 0.3f;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky);
        }

        public override void HitEffect(NPC.HitInfo hit)
		{		
			bool isDead = NPC.life <= 0;
			for (int m = 0; m < (isDead ? 25 : 5); m++)
			{
				int dustType = Main.rand.NextBool(2) ? ModContent.DustType<Dusts.NightcrawlerDust>() : ModContent.DustType<Dusts.DaybringerDust>();
				Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType, NPC.velocity.X * 0.2f, NPC.velocity.Y * 0.2f, 100, Color.White, isDead ? 2f : 1.1f);
			}
		}

		float shootAI = 0;
		public override void AI()
		{
            BaseAI.AISkull(NPC, ref NPC.ai, false, 6f, 350f, 0.1f, 0.15f);
			Player player = Main.player[NPC.target];
			bool playerActive = player != null && player.active && !player.dead;
            BaseAI.LookAt(playerActive ? player.Center : NPC.Center + NPC.velocity, NPC, 0);		
			if(Main.netMode != NetmodeID.MultiplayerClient && playerActive)
			{
				shootAI++;
				if(shootAI >= 90)
				{
					shootAI = 0;
					int projType = !Main.dayTime ? ModContent.ProjectileType<Moonray>() : ModContent.ProjectileType<Sunbeam>();					
					if(Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
						BaseAI.FireProjectile(player.Center, NPC, projType, (int)(NPC.damage * 0.25f), 0f, 2f);
				}
			}
		}

		public override Color? GetAlpha(Color drawColor)
		{
			Color c = Color.White * (Main.mouseTextColor / 255f);
			c.A = 255;
			return c;
		}		
	}
}