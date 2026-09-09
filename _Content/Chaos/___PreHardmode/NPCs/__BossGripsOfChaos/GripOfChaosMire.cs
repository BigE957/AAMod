using AAModClassic._Content.Chaos.___PreHardmode.Items._BossGripsOfChaos.BossStandard;
using AAModClassic._Content.Chaos.___PreHardmode.Items._BossGripsOfChaos.Weapons;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic._Content.Mire.World.Biomes;
using AAModClassic._CrossMod.CalamityMod.LoreItems;
using AAModClassic.Achievements;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos.___PreHardmode.NPCs.__BossGripsOfChaos
{
    [AutoloadBossHead]
    public class GripOfChaosMire : GripOfChaosAbstract
    {
        public override void SetDefaults()
        {
			base.SetDefaults();
			NPC.lifeMax = 1400;
            NPC.damage = 30;
            NPC.defense = 10;		
            NPC.buffImmune[BuffID.Poisoned] = true;	

			offsetBasePoint = new Vector2(240f, 0f);
            SpawnModBiomes = new int[1] { ModContent.GetInstance<MireBiome>().Type };
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && !Main.dedServ) //this make so when the npc has 0 life(dead) he will spawn this
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("MireGripGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("MireGripGore2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("MireGripGore3").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("MireGripGore4").Type, 1f);
            }
        }

        public override void OnKill()
        {
            int redGripExists = NPC.CountNPCS(ModContent.NPCType<GripOfChaosInferno>());
            if (redGripExists == 0)
            {
                NPC.SetEventFlagCleared(ref AAWorld.downedGrips, -1);
                if (NPC.playerInteraction[Main.myPlayer])
                    GripsOfChaosKilled.Condition.Complete();
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MireGripTrophy>(), 10));

            LeadingConditionRule masterMode = new(new MissingGripMaster());

            masterMode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<GripsOfChaosRelic>()));

            npcLoot.Add(masterMode);

            LeadingConditionRule notExpert = new(new Conditions.NotExpert());

            notExpert.OnSuccess(ItemDropRule.Common(ModContent.ItemType<AbyssiumOre>(), 1, 30, 44));

            notExpert.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MireGripMask>(), 7));

            LeadingConditionRule lastStandingAlways = new(new MissingGripAlways());

            LeadingConditionRule loreCondition = new(new LoreItemDropCondition(() => AAWorld.downedGrips));
            lastStandingAlways.OnSuccess(loreCondition).OnSuccess(new PerPlayerDropRule(ModContent.ItemType<GripsOfChaosLore>(), 1));

            lastStandingAlways.OnSuccess(ItemDropRule.BossBag(ModContent.ItemType<GripsOfChaosTreasureBag>()));

            lastStandingAlways.OnSuccess(ItemDropRule.ByCondition(new MasterRevDropRule(), ModContent.ItemType<GripsOfChaosRelic>()));

            LeadingConditionRule lastStandingNormal = new(new MissingGripNormal());

            lastStandingNormal.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ClawBaton>(), 4));

            npcLoot.Add(lastStandingAlways);
            npcLoot.Add(lastStandingNormal);
            npcLoot.Add(notExpert);
        }

        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            if (Main.rand.NextBool(2) || Main.expertMode && Main.rand.Next(0) == 0)       //Chances for it to inflict the debuff
            {
                target.AddBuff(BuffID.Poisoned, Main.rand.Next(180, 250));       //Main.rand.Next part is the length of the buff, so 8.3 seconds to 16.6 seconds
            }
        }		
    }
}
