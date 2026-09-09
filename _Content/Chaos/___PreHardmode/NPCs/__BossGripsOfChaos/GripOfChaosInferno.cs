using AAModClassic._Content.Chaos.___PreHardmode.Items._BossGripsOfChaos.BossStandard;
using AAModClassic._Content.Chaos.___PreHardmode.Items._BossGripsOfChaos.Weapons;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno.World.Biomes;
using AAModClassic._CrossMod.CalamityMod.LoreItems;
using AAModClassic.Achievements;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos.___PreHardmode.NPCs.__BossGripsOfChaos
{
    [AutoloadBossHead]
    public class GripOfChaosInferno : GripOfChaosAbstract
    {
        public static Asset<Texture2D> Glowmask;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            Glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
        }

        public override void SetDefaults()
        {
			base.SetDefaults();
			NPC.lifeMax = 1600;
            NPC.damage = 32;
            NPC.defense = 15;	
            NPC.buffImmune[BuffID.OnFire] = true;			

			offsetBasePoint = new Vector2(-240f, 0f);

            SpawnModBiomes = new int[1] { ModContent.GetInstance<InfernoBiome>().Type };
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && !Main.dedServ) //this make so when the npc has 0 life(dead) he will spawn this
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("InfernoGripGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("InfernoGripGore2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("InfernoGripGore3").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("InfernoGripGore4").Type, 1f);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos, NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            spriteBatch.Draw(Glowmask.Value, NPC.Center - screenPos, NPC.frame, Color.White, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            return false;
        }

        public override void OnKill()
        {
            int blueGripExists = NPC.CountNPCS(ModContent.NPCType<GripOfChaosMire>());
            if (blueGripExists == 0)
            {
                NPC.SetEventFlagCleared(ref AAWorld.downedGrips, -1);
                if (NPC.playerInteraction[Main.myPlayer])
                    GripsOfChaosKilled.Condition.Complete();
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<InfernoGripTrophy>(), 10));

            LeadingConditionRule masterMode = new(new MissingGripMaster());

            masterMode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<GripsOfChaosRelic>()));

            npcLoot.Add(masterMode);

            LeadingConditionRule notExpert = new(new Conditions.NotExpert());

            notExpert.OnSuccess(ItemDropRule.Common(ModContent.ItemType<IncineriteOre>(), 1, 30, 44));

            notExpert.OnSuccess(ItemDropRule.Common(ModContent.ItemType<InfernoGripMask>(), 7));

            LeadingConditionRule lastStandingAlways = new(new MissingGripAlways());

            lastStandingAlways.OnSuccess(ItemDropRule.BossBag(ModContent.ItemType<GripsOfChaosTreasureBag>()));

            LeadingConditionRule loreCondition = new(new LoreItemDropCondition(() => AAWorld.downedGrips));
            lastStandingAlways.OnSuccess(loreCondition).OnSuccess(new PerPlayerDropRule(ModContent.ItemType<GripsOfChaosLore>(), 1));

            lastStandingAlways.OnSuccess(ItemDropRule.ByCondition(new MasterRevDropRule(), ModContent.ItemType<GripsOfChaosRelic>()));

            LeadingConditionRule lastStandingNormal = new(new MissingGripNormal());

            lastStandingNormal.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ClawBaton>(), 4));

            npcLoot.Add(lastStandingAlways);
            npcLoot.Add(lastStandingNormal);
            npcLoot.Add(notExpert);
        }

        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            if (Main.rand.NextBool(2) || Main.expertMode && Main.rand.Next(0) == 0)
            {
                target.AddBuff(BuffID.OnFire, Main.rand.Next(180, 250));
            }
        }
    }
}
