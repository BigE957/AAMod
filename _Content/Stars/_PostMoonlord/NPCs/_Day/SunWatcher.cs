using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic._Content.Stars.Projectiles;
using AAModClassic._CrossMod.CalamityMod;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;
using AAModClassic.Utilities.Interfaces;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.NPCs._Day
{
    public class SunWatcher : ModNPC, IBannerNPC
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Sun Watcher");
            Main.npcFrameCount[NPC.type] = 1;
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
			Banner = NPC.type;
			//BannerItem = ModContent.ItemType<AAModClassic.Items.Banners.SunWatcherBanner>();
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
			bestiaryEntry.Info.Add(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.dayTime && AAWorld.downedEquinox && spawnInfo.Player.AAPlayer().ZoneStars && !NPCUtils.AnyEvents(spawnInfo.Player))
                return 0.2f;

            return 0f;
        }

		public override void HitEffect(NPC.HitInfo hit)
		{		
			bool isDead = NPC.life <= 0;
			for (int m = 0; m < (isDead ? 25 : 5); m++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.DaybringerDust>(), NPC.velocity.X * 0.2f, NPC.velocity.Y * 0.2f, 100, Color.White, isDead ? 2f : 1.1f);
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
					int projType = ModContent.ProjectileType<Sunbeam>();					
					if(Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
						BaseAI.FireProjectile(player.Center, NPC, projType, (int)(NPC.damage * 0.25f), 0f, 2f);
				}
			}
		}

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule spawnedByGrips = new(new EquinoxWormsDefeated());

            spawnedByGrips.OnSuccess(ItemDropRule.Common(ModContent.ItemType<RadiantPhoton>(), 1, 0, 2));

            npcLoot.Add(spawnedByGrips);
        }

        public class EquinoxWormsDefeated : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info)
            {
				return AAWorld.downedEquinox;
            }

            public bool CanShowItemDropInUI() => true;

            public string GetConditionDescription() => Language.GetTextValue("Mods.AAModClassic.Common.Conditions.EquinoxWormsDefeated");
        }

        public override Color? GetAlpha(Color drawColor)
		{
			Color c = Color.White * (Main.mouseTextColor / 255f);
			c.A = 255;
			return c;
		}		
	}
}