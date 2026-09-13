using AAModClassic._CrossMod.CalamityMod.LoreItems;
using AAModClassic._Unofficial.Content.SunkenShip._PostMoonlord.Items._BossSoulOfCthulhu.BossStandard;
using AAModClassic._Unofficial.Content.SunkenShip._PostMoonlord.NPCs;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.Items.SoulOfCthulhu;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.Items.SoulOfCthulhu.BossStandard;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.Items.SoulOfCthulhu.Weapons;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityBrain;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityEater;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityEye;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityLeviathan;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityRose;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeitySkull;
using AAModClassic._Unreleased.Content.SunkenShip.World.Biomes;
using AAModClassic.Achievements;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Music;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using AAModClassic.Utilities.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu
{
    [AutoloadBossHead]
    public class SoulOfCthulhu : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Soul of Cthulhu");
            NPCID.Sets.BossBestiaryPriority.Add(Type);
        }

        public override void SetDefaults()
        {
            NPC.npcSlots = 100;
            NPC.width = 54;
            NPC.height = 54;
            NPC.aiStyle = -1;
            NPC.damage = 100;
            NPC.defense = 150;
            NPC.lifeMax = 1000000;
            NPC.value = Item.buyPrice(35, 0, 0, 0);
            NPC.HitSound = SoundID.NPCHit54;
            NPC.DeathSound = SoundID.Item88;// new LegacySoundStyle(2, 88, Terraria.Audio.SoundType.Sound);
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            Music = MusicManagementSystem.MusicSlots["SoulOfCthulhu"];
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.netAlways = true;
            for (int m = 0; m < NPC.buffImmune.Length; m++) NPC.buffImmune[m] = true;
            SpawnModBiomes = [ModContent.GetInstance<SunkenShipBiome>().Type];
        }

        private VerletObject BigVine = null;
        private VerletObject[] SmallVines = [];
        private VerletObject[] BackVines = [];
        private float[] BackVineAngleOffsets = [];
        private bool initailizedVerlets = false;

        private void InitializeVerlets()
        {
            if (!Main.dedServ && WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                Vector2 start = NPC.Center + new Vector2(14, 35).RotatedBy(Rotation);
                int count = 12;
                BigVine = VerletIntegration.CreateVerletChain(start, start + Vector2.UnitY * count * 6, count, 6);

                start = NPC.Center + new Vector2(-16, 36).RotatedBy(Rotation);
                SmallVines = new VerletObject[5];
                for (int i = 0; i < SmallVines.Length; i++)
                {
                    count = Main.rand.Next(5, 8);
                    Vector2 myStart = start + Vector2.One * -4 * i;
                    SmallVines[i] = VerletIntegration.CreateVerletChain(myStart, myStart + Vector2.UnitY * count * 6, count, 6);
                }

                BackVines = new VerletObject[8];
                BackVineAngleOffsets = new float[8];

                for (int i = 0; i < BackVines.Length; i++)
                {
                    count = Main.rand.Next(4, 7);
                    BackVineAngleOffsets[i] = Main.rand.NextFloat(MathHelper.Pi / 16f - 0.05f, MathHelper.Pi / 16f + 0.05f);
                    Vector2 myStart = NPC.Center + Vector2.UnitX.RotatedBy(Rotation + (MathHelper.TwoPi / BackVines.Length * i) + BackVineAngleOffsets[i]) * 44;
                    BackVines[i] = VerletIntegration.CreateVerletChain(myStart, myStart + Vector2.UnitY * count * 6, count, 6);
                }
            }
            initailizedVerlets = true;
        }

        private void UpdateVerlets()
        {
            if (!WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                return;

            Vector2 start = NPC.Center + new Vector2(16, 36).RotatedBy(Rotation);
            if (BigVine != null)
            {
                BigVine.Points[0].Position = start;
                VerletIntegration.VerletSimulation(BigVine);
            }

            start = NPC.Center + new Vector2(-18, 34).RotatedBy(Rotation);
            for (int i = 0; i < SmallVines.Length; i++)
            {
                Vector2 myStart = start + Vector2.One.RotatedBy(Rotation) * -4 * i;
                if (i % 2 != 0)
                    myStart += Vector2.One.RotatedBy(Rotation + MathHelper.PiOver2) * 4;
                SmallVines[i].Points[0].Position = myStart;
                VerletIntegration.VerletSimulation(SmallVines[i]);
            }

            for (int i = 0; i < BackVines.Length; i++)
            {
                Vector2 myStart = NPC.Center + Vector2.UnitX.RotatedBy(Rotation + (MathHelper.TwoPi / BackVines.Length * i) + BackVineAngleOffsets[i]) * 42;
                BackVines[i].Points[0].Position = myStart;
                VerletIntegration.VerletSimulation(BackVines[i]);
            }
        }

        private void DrawVines(SpriteBatch spriteBatch, Color drawColor)
        {
            if (!WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                return;

            Texture2D vinesAtlas = ModContent.Request<Texture2D>(Texture + "_Vines").Value;
            foreach (var vine in SmallVines)
            {
                for (int i = 0; i < vine.Count - 1; i++)
                {
                    Vector2 start = vine.Positions[i];
                    Vector2 end = vine.Positions[i + 1];
                    Vector2 dir = start.DirectionTo(end);

                    Rectangle frame = vinesAtlas.Frame(4, 4, 0, (i == vine.Count - 2 ? 3 : i % 3));
                    if (i != vine.Count - 2)
                        frame.Height -= 2;
                    float stretch = Vector2.Distance(start, end) / frame.Height;
                    stretch += 0.1f;
                    spriteBatch.Draw(vinesAtlas, start - (NPC.IsABestiaryIconDummy ? Vector2.Zero : Main.screenPosition), frame, drawColor, dir.ToRotation() - MathHelper.PiOver2, new Vector2(frame.Width / 2f, 2f), new Vector2(1, stretch), 0, 0);
                }
            }

            if (BigVine != null)
            {
                for (int i = 0; i < BigVine.Count - 1; i++)
                {
                    Vector2 start = BigVine.Positions[i];
                    Vector2 end = BigVine.Positions[i + 1];
                    Vector2 dir = start.DirectionTo(end);

                    Rectangle frame = vinesAtlas.Frame(4, 4, 0, (i == BigVine.Count - 2 ? 3 : i % 3));
                    if (i != BigVine.Count - 2)
                        frame.Height -= 2;
                    float stretch = Vector2.Distance(start, end) / frame.Height;
                    stretch += 0.1f;
                    spriteBatch.Draw(vinesAtlas, start - (NPC.IsABestiaryIconDummy ? Vector2.Zero : Main.screenPosition), frame, drawColor, dir.ToRotation() - MathHelper.PiOver2, new Vector2(frame.Width / 2f, 2f), new Vector2(1, stretch), 0, 0);
                }
            }
        }

        private void DrawBackVines(SpriteBatch spriteBatch, Color drawColor)
        {
            if (!WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                return;

            Texture2D vinesAtlas = ModContent.Request<Texture2D>(Texture + "_Vines").Value;

            foreach (var vine in BackVines)
            {
                for (int i = 0; i < vine.Count - 1; i++)
                {
                    Vector2 start = vine.Positions[i];
                    Vector2 end = vine.Positions[i + 1];
                    Vector2 dir = start.DirectionTo(end);

                    Rectangle frame = vinesAtlas.Frame(4, 4, (i % 3 + 1), (i == vine.Count - 2 ? 3 : 2));
                    if (i != vine.Count - 2)
                        frame.Height -= 2;
                    float stretch = Vector2.Distance(start, end) / frame.Height;
                    stretch += 0.1f;
                    spriteBatch.Draw(vinesAtlas, start - (NPC.IsABestiaryIconDummy ? Vector2.Zero : Main.screenPosition), frame, drawColor, dir.ToRotation() - MathHelper.PiOver2, new Vector2(frame.Width / 2f, 2f), new Vector2(1, stretch), 0, 0);
                }
            }
        }

        public float[] customAI = new float[4];

        //public bool LeaveLine = false;
        //public bool Leviathan = false;

        //public float morphTimer = 0;
        //public bool Morphed = false;
        //public static bool ComeBack = false;
        //public int ReturnTimer = 100;

        //Client Side
        public bool Summon = false;
        public float Rotation = 0;
        //public float AlphaTimer = 0;
        public float alpha = 255;
        public float scale = 0;
        public float RingRotation = 0;
        public float RiftSpin = 0;     

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(customAI[0]);
            writer.Write(customAI[1]);
            writer.Write(customAI[2]);
            writer.Write(customAI[3]);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            customAI[0] = reader.ReadSingle();
            customAI[1] = reader.ReadSingle();
            customAI[2] = reader.ReadSingle();
            customAI[3] = reader.ReadSingle();
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void OnKill()
        {
            if (Main.expertMode)
                NPC.NewNPC(NPC.GetSource_Death(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<CthulhuPortal>(), 0, 0);
            else
            {
                if (NPC.playerInteraction[Main.myPlayer])
                    SoulOfCthulhuKilled.Condition.Complete();

                if (!NPC.BeenKilled(true))
                    NPC.OnGameEventClearedForTheFirstTime(-1);
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule unofficialRule = new(new AAConditions.UnofficialNotExpert());

            unofficialRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<SoulOfCthulhuMask>(), 7));

            npcLoot.Add(unofficialRule);

            int[] lootTable =
            {
                ModContent.ItemType<RealityAnchor>(),
                ModContent.ItemType<SquidStorm>(),
                ModContent.ItemType<CthulhuCannon>(),
                ModContent.ItemType<GalacticStormspike>(),
            };

            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<RealityBar>(), 1, 25, 35));
            notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, lootTable));

            LeadingConditionRule loreCondition = new(new LoreItemDropCondition<SoulOfCthulhu>());
            notExpertRule.OnSuccess(loreCondition.OnSuccess(new PerPlayerDropRule(ModContent.ItemType<SoulOfCthulhuLore>(), 1)));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SoulOfCthulhuTrophy>(), 10));

            npcLoot.Add(notExpertRule);
        }

        int oneTime = 0;

        public int EnemyTimer = 0;
        
        public override void AI()
        {
            Player player = Main.player[NPC.target];
            SunkenShipSystem.Leave = false;
            NPC.rotation = NPC.velocity.X / 15f;
            Vector2 spawnAt = NPC.Center + new Vector2(0f, NPC.height / 2f);
            float EyeSummon = NPC.lifeMax * .85f;
            float EaterSummon = NPC.lifeMax * .70f;
            float BrainSummon = NPC.lifeMax * .55f;
            float SkullSummon = NPC.lifeMax * .40f;
            float RoseSummon = NPC.lifeMax * .25f;
            float LeviathanSummon = NPC.lifeMax * .10f;
            bool BossAlive = NPC.AnyNPCs(ModContent.NPCType<DeityEye>()) || NPC.AnyNPCs(ModContent.NPCType<DeityEater>()) || NPC.AnyNPCs(ModContent.NPCType<DeityBrain>()) || NPC.AnyNPCs(ModContent.NPCType<DeitySkull>()) || NPC.AnyNPCs(ModContent.NPCType<DeityLeviathan>()) || NPC.AnyNPCs(ModContent.NPCType<DeityRose>());
            NPC.ai[3]++;
            customAI[3]++;
            if (NPC.ai[3] >= 600 && !BossAlive)
            {
                NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, ModContent.NPCType<Portal>(), 0, -NPC.velocity.X, -NPC.velocity.Y);
                NPC.ai[3] = 0;
            }

            if (oneTime == 0)
            {
                RainStart();
                oneTime++;
            }

            if (BossAlive)
            {
                NPC.alpha += 12;
                if (NPC.alpha >= 140)
                {
                    NPC.alpha = 140;
                }
                NPC.dontTakeDamage = true;
                NPC.Center = new Vector2(player.Center.X, player.Center.Y - 60);
                NPC.netOffset = Vector2.Zero;
                return;
            }
            else
            {
                NPC.alpha -= 30;
                if (NPC.alpha <= 0)
                {
                    NPC.alpha = 0;
                }
                NPC.dontTakeDamage = false;
            }

            if (NPC.life < EyeSummon && customAI[2] == 0) //Spawn Eye boi
            {
                customAI[2] = 1;
                NPC.ai[1] = 2f;
                NPC.dontTakeDamage = true;
                customAI[3] = 0;
                customAI[1] = 0;
            }
            else if (NPC.life < EaterSummon && customAI[2] == 1)
            {
                customAI[2] = 2;
                NPC.ai[1] = 2f;
                NPC.dontTakeDamage = true;
                customAI[3] = 0;
                customAI[1] = 0;
            }
            else if (NPC.life < BrainSummon && customAI[2] == 2)
            {
                customAI[2] = 3;
                NPC.ai[1] = 2f;
                NPC.dontTakeDamage = true;
                customAI[3] = 0;
                customAI[1] = 0;
            }
            else if (NPC.life < SkullSummon && customAI[2] == 3)
            {
                customAI[2] = 4;
                NPC.ai[1] = 2f;
                NPC.dontTakeDamage = true;
                customAI[3] = 0;
                customAI[1] = 0;
            }
            else if (NPC.life < RoseSummon && customAI[2] == 4)
            {
                customAI[2] = 5;
                NPC.ai[1] = 2f;
                NPC.dontTakeDamage = true;
                customAI[3] = 0;
                customAI[1] = 0;
            }
            else if (NPC.life < LeviathanSummon && customAI[2] == 5)
            {
                customAI[2] = 6;
                NPC.ai[1] = 2f;
                NPC.dontTakeDamage = true;
                customAI[3] = 0;
                customAI[1] = 0;
            }

            if (Main.player[NPC.target].dead)
            {
                NPC.TargetClosest(true);
                if (Main.player[NPC.target].dead)
                {
                    NPC.ai[1] = 3f;
                }
            }
            if (Math.Abs(NPC.position.X - Main.player[NPC.target].position.X) > 6000f || Math.Abs(NPC.position.Y - Main.player[NPC.target].position.Y) > 6000f)
            {
                NPC.TargetClosest(true);
                if (Main.player[NPC.target].dead)
                {
                    NPC.ai[1] = 4f;
                }
            }
            if (NPC.ai[1] == 0f)
            {
                NPC.damage = 100;

                Rotation += NPC.velocity.X * .01f;
                RiftSpin -= NPC.velocity.X * .01f;


                NPC.rotation = NPC.velocity.X / 15f;
                if (NPC.position.Y > Main.player[NPC.target].position.Y - 200f)
                {
                    if (NPC.velocity.Y > 0f)
                    {
                        NPC.velocity.Y = NPC.velocity.Y * 0.98f;
                    }
                    NPC.velocity.Y = NPC.velocity.Y - 0.1f;
                    if (NPC.velocity.Y > 2f)
                    {
                        NPC.velocity.Y = 2f;
                    }
                }
                else if (NPC.position.Y < Main.player[NPC.target].position.Y - 300f)
                {
                    if (NPC.velocity.Y < 0f)
                    {
                        NPC.velocity.Y = NPC.velocity.Y * 0.98f;
                    }
                    NPC.velocity.Y = NPC.velocity.Y + 0.1f;
                    if (NPC.velocity.Y < -2f)
                    {
                        NPC.velocity.Y = -2f;
                    }
                }
                if (NPC.position.X + (float)(NPC.width / 2) > Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) + 100f)
                {
                    if (NPC.velocity.X > 0f)
                    {
                        NPC.velocity.X = NPC.velocity.X * 0.98f;
                    }
                    NPC.velocity.X = NPC.velocity.X - 0.1f;
                    if (NPC.velocity.X > 8f)
                    {
                        NPC.velocity.X = 8f;
                    }
                }
                if (NPC.position.X + (float)(NPC.width / 2) < Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - 100f)
                {
                    if (NPC.velocity.X < 0f)
                    {
                        NPC.velocity.X = NPC.velocity.X * 0.98f;
                    }
                    NPC.velocity.X = NPC.velocity.X + 0.1f;
                    if (NPC.velocity.X < -8f)
                    {
                        NPC.velocity.X = -8f;
                        return;
                    }
                }
                NPC.ai[2] += 1f;
                if (NPC.ai[2] >= 600f)
                {
                    NPC.ai[2] = 0f;
                    NPC.ai[1] = 1f;
                    NPC.TargetClosest(true);
                    NPC.netUpdate = true;
                }
            }
            else
            {
                if (NPC.ai[1] == 1f)
                {
                    NPC.defense = 180;
                    NPC.damage = 200;
                    NPC.ai[2] += 1f;
                    if (NPC.ai[2] == 2f)
                    {
                        SoundEngine.PlaySound(SoundID.Zombie92, NPC.Center);
                    }
                    if (NPC.ai[2] >= 400f)
                    {
                        NPC.ai[2] = 0f;
                        NPC.ai[1] = 0f;
                    }
                    NPC.rotation += NPC.direction * 0.7f;
                    Vector2 vector44 = new Vector2(NPC.position.X + ((float)NPC.width * 0.5f), NPC.position.Y + ((float)NPC.height * 0.5f));
                    float num441 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector44.X;
                    float num442 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector44.Y;
                    float num443 = (float)Math.Sqrt((double)((num441 * num441) + (num442 * num442)));
                    float num4 = 5f + num443 / 100f;
                    if (num4 < 8.0)
                        num4 = 8f;
                    if (num4 > 32.0)
                        num4 = 32f;
                    float num5 = num4 / num443;
                    NPC.velocity.X = num441 * num5;
                    NPC.velocity.Y = num442 * num5;
                    Rotation += NPC.velocity.X * .08f;
                    RiftSpin -= NPC.velocity.X * .08f;
                    return;

                }
                if (NPC.ai[1] == 2f)
                {
                    Summon = true;
                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                        NPC.dontTakeDamage = true;
                    NPC.velocity *= .8f;
                    if (NPC.velocity.X < .5f || NPC.velocity.X > -.5f)
                    {
                        NPC.velocity.X = 0;
                    }
                    if (NPC.velocity.Y < .5f || NPC.velocity.Y > -.5f)
                    {
                        NPC.velocity.Y = 0;
                    }

                    if (NPC.velocity.X == 0 && NPC.velocity.Y == 0)
                    {

                        Rotation += .2f;
                        RiftSpin -= .2f;
                        customAI[1]++;

                        if (customAI[1] > 300)
                        {
                            if (customAI[0] == 0)
                            {
                                Summon = true;
                                customAI[0] = 1;
                                NPC.SpawnBoss((int)spawnAt.X, (int)spawnAt.Y, ModContent.NPCType<DeityEye>(), NPC.target);
                                NPC.ai[2] = 0f;
                                NPC.ai[1] = 0f;
                                return;
                            }
                            if (customAI[0] == 1)
                            {
                                Summon = true;
                                customAI[0] = 2;
                                NPC.SpawnBoss((int)spawnAt.X, (int)spawnAt.Y, ModContent.NPCType<DeityEater>(), NPC.target);
                                NPC.ai[2] = 0f;
                                NPC.ai[1] = 0f;
                                return;
                            }
                            if (customAI[0] == 2)
                            {
                                Summon = true;
                                customAI[0] = 3;
                                NPC.SpawnBoss((int)spawnAt.X, (int)spawnAt.Y, ModContent.NPCType<DeityBrain>(), NPC.target);
                                NPC.ai[2] = 0f;
                                NPC.ai[1] = 0f;
                                return;
                            }
                            if (customAI[0] == 3)
                            {
                                Summon = true;
                                customAI[0] = 4;
                                NPC.SpawnBoss((int)spawnAt.X, (int)spawnAt.Y, ModContent.NPCType<DeitySkull>(), NPC.target);
                                NPC.ai[2] = 0f;
                                NPC.ai[1] = 0f;
                                return;
                            }
                            if (customAI[0] == 4)
                            {
                                Summon = true;
                                customAI[0] = 5;
                                NPC.SpawnBoss((int)spawnAt.X, (int)spawnAt.Y, ModContent.NPCType<DeityRose>(), NPC.target);
                                NPC.ai[2] = 0f;
                                NPC.ai[1] = 0f;
                                return;
                            }
                            if (customAI[0] == 5)
                            {
                                Summon = true;
                                customAI[0] = 6;
                                NPC.SpawnBoss((int)spawnAt.X, (int)spawnAt.Y, ModContent.NPCType<DeityLeviathan>(), NPC.target);
                                NPC.ai[2] = 0f;
                                NPC.ai[1] = 0f;
                                return;
                            }
                        }
                    }
                    return;
                }
                if (NPC.ai[1] == 3f)
                {
                    if(Main.netMode != NetmodeID.MultiplayerClient)
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.Kill"), Color.DarkCyan);
                    NPC.ai[1] = 5f;
                }
                if (NPC.ai[1] == 4f)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.Despawn"), Color.DarkCyan);
                    NPC.ai[1] = 5f;
                }
                if (NPC.ai[1] == 5f)
                {
                    NPC.alpha += 5;
                    {
                        if (NPC.alpha >= 255)
                        {
                            NPC.active = false;
                        }
                    }
                }
            }
        }

        public override void PostAI()
        {
            if (!initailizedVerlets)
                InitializeVerlets();

            UpdateVerlets();
        }

        /*
        public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
        {
            if (AAWorld.Anticheat)
            {
                if (damage > NPC.lifeMax / 8)
                {
        
                    Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.Anticheat"), Color.DarkCyan);
                    damage = 0;
                }
            }
        }
        */

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && !Main.dedServ)
            {
                Vector2 baseVelocity = NPC.velocity * Main.rand.NextFloat();
                Gore.NewGore(NPC.GetSource_FromAI(), NPC.Center, baseVelocity + Vector2.UnitX * 2f, Mod.Find<ModGore>("SoCGore1").Type, NPC.scale);
                Gore.NewGore(NPC.GetSource_FromAI(), NPC.Center, baseVelocity + Vector2.UnitX * -2f, Mod.Find<ModGore>("SoCGore1").Type, NPC.scale);
                for (int i = 0; i < 8; i++)
                {
                    int num = 3 + i;
                    Vector2 extraVelo = Vector2.UnitY.RotatedBy(MathHelper.TwoPi / 8f * i) * -2f;
                    Gore.NewGore(NPC.GetSource_FromAI(), NPC.Center, baseVelocity + extraVelo, Mod.Find<ModGore>("SoCGore" + num).Type, NPC.scale);
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture2D13 = TextureAssets.Npc[NPC.type].Value;
            Texture2D WheelTex = ModContent.Request<Texture2D>(Texture + (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) && !NPC.IsABestiaryIconDummy ? "_Wheel_Unofficial" : "_Wheel")).Value;
            Texture2D RingTex = ModContent.Request<Texture2D>(Texture + "_DeityCircle").Value;
            Texture2D RitualTex = ModContent.Request<Texture2D>(Texture + "_DeityRitual").Value;
            Texture2D Rift = ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/SunkenShip/_PostMoonLord/NPCs/SoulOfCthulhu/UDUNFUKED_Rift").Value;
            Texture2D GlowTex = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
            Vector2 vector38 = NPC.position + new Vector2(NPC.width, NPC.height) / 2f + Vector2.UnitY * NPC.gfxOffY - Main.screenPosition;
            Vector2 origin8 = new Vector2((float)RitualTex.Width, (float)RitualTex.Height) / 2f;
            bool BossAlive = !NPC.IsABestiaryIconDummy && (NPC.AnyNPCs(ModContent.NPCType<DeityEye>()) || NPC.AnyNPCs(ModContent.NPCType<DeityEater>()) || NPC.AnyNPCs(ModContent.NPCType<DeityBrain>()) || NPC.AnyNPCs(ModContent.NPCType<DeitySkull>()) || NPC.AnyNPCs(ModContent.NPCType<DeityLeviathan>()) || NPC.AnyNPCs(ModContent.NPCType<DeityRose>()));
            if (!NPC.IsABestiaryIconDummy && Summon)
            {
                Rotation += .2f;
                RiftSpin -= .2f;
                if (customAI[3] < 300f)
                {
                    alpha -= 5;
                }
                else
                {
                    alpha += 12;
                }
                if (alpha < 0)
                {
                    alpha = 0;
                }
                if (alpha > 255)
                {
                    alpha = 255;
                }
                scale = 1f - alpha / 255f;
                RingRotation += 0.0149599658f;
                spriteBatch.Draw(RingTex, vector38, null, AAColor.Cthulhu, -RingRotation, RingTex.Size() / 2f, scale, SpriteEffects.None, 0f);
                spriteBatch.Draw(RitualTex, vector38, null, AAColor.Cthulhu, RingRotation, origin8, scale, SpriteEffects.None, 0f);
                spriteBatch.Draw(RingTex, vector38, null, AAColor.Cthulhu, -RingRotation, RingTex.Size() / 2f, scale * 0.42f, SpriteEffects.None, 0f);
            }

            int shader;
            if (BossAlive)
                shader = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingOceanDye);
            else
                shader = 0;

            spriteBatch.Draw(Rift, NPC.Center - screenPos, null, AAColor.Cthulhu, RiftSpin, Rift.Size() * 0.5f, 1.5f, 0, 0);

            if (!BossAlive)
                DrawBackVines(spriteBatch, drawColor);

            if (shader == 0)
                spriteBatch.Draw(WheelTex, NPC.Center - screenPos, null, drawColor, Rotation, WheelTex.Size() * 0.5f, NPC.scale, 0, 0);
            else
                DrawingUtils.DrawWithVanillaShader(spriteBatch, shader, (sb) =>
                {
                    sb.Draw(WheelTex, NPC.Center - screenPos, null, drawColor, Rotation, WheelTex.Size() * 0.5f, NPC.scale, 0, 0);
                });

            if (shader == 0)
                spriteBatch.Draw(texture2D13, NPC.Center - screenPos, null, drawColor, NPC.rotation, texture2D13.Size() * 0.5f, NPC.scale, 0, 0);
            else
                DrawingUtils.DrawWithVanillaShader(spriteBatch, shader, (sb) =>
                {
                    sb.Draw(texture2D13, NPC.Center - screenPos, null, drawColor, NPC.rotation, texture2D13.Size() * 0.5f, NPC.scale, 0, 0);
                });

            if (!NPC.IsABestiaryIconDummy && (BossAlive || Summon))
            {
                spriteBatch.Draw(GlowTex, NPC.Center - Main.screenPosition, null, AAColor.Cthulhu2, NPC.rotation, GlowTex.Size() * 0.5f, NPC.scale, 0, 0);

                //These don't do anything since SoC sets its position rather than using velocity
                //BaseDrawing.DrawAfterimage(spriteBatch, GlowTex, 0, NPC, 0.8f, 1f, 6, false, 0f, 0f, AAColor.Cthulhu2);
            }

            if (!BossAlive)
                DrawVines(spriteBatch, drawColor);

            return false;
        }

        private static void RainStart()
        {
            if (!Main.raining)
            {
                int num = 86400;
                int num2 = num / 24;
                Main.rainTime = Main.rand.Next(num2 * 8, num);
                if (Main.rand.Next(3) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2);
                }
                if (Main.rand.Next(4) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 2);
                }
                if (Main.rand.Next(5) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 2);
                }
                if (Main.rand.Next(6) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 3);
                }
                if (Main.rand.Next(7) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 4);
                }
                if (Main.rand.Next(8) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 5);
                }
                float num3 = 1f;
                if (Main.rand.Next(2) == 0)
                {
                    num3 += 0.05f;
                }
                if (Main.rand.Next(3) == 0)
                {
                    num3 += 0.1f;
                }
                if (Main.rand.Next(4) == 0)
                {
                    num3 += 0.15f;
                }
                if (Main.rand.Next(5) == 0)
                {
                    num3 += 0.2f;
                }
                Main.rainTime = (int)((float)Main.rainTime * num3);
                Main.raining = true;
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.WorldData, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
                }
            }
        }
    }
}