using AAModClassic._Content.Acropolis.__Hardmode.Items.Materials;
using AAModClassic._Content.Acropolis.__Hardmode.NPCs.__BossAthena;
using AAModClassic._Content.Acropolis._PostMoonlord.Items._BossAthenaA.BossStandard;
using AAModClassic._Content.Acropolis._PostMoonlord.Items._BossAthenaA.Weapons;
using AAModClassic._Content.Acropolis._PostMoonlord.Items.Materials;
using AAModClassic._Content.Acropolis._PostMoonlord.Items.Tiles.Decoration;
using AAModClassic._Content.Acropolis.World.Biomes;
using AAModClassic._Content.Acropolis.World.Tiles;
using AAModClassic._Content.Stars._PostMoonlord.Items.Quest;
using AAModClassic._CrossMod.CalamityMod.LoreItems;
using AAModClassic._Unreleased.Content.Acropolis.__Hardmode.NPCs.__Athena;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Dusts;
using AAModClassic.Globals;
using AAModClassic.Music;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Acropolis._PostMoonlord.NPCs.__BossAthenaA
{
    [AutoloadBossHead]
    public class AthenaA : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Olympian Athena");
            Main.npcFrameCount[NPC.type] = 7;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new()
            {
                PortraitPositionXOverride = -8,
                PortraitPositionYOverride = 0,
                Position = new(-8, 30),
                SpriteDirection = 1
            };
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
        }

        public int damage = 0;

        public static Point CloudPoint => AAWorld.acropolisPos;
        public Vector2 Origin => CloudPoint.ToWorldCoordinates();

        public override void SetDefaults()
        {
            NPC.width = 152;
            NPC.height = 114;
            NPC.value = Item.buyPrice(0, 10, 0, 0);
            NPC.npcSlots = 1000;
            NPC.aiStyle = -1;
            NPC.lifeMax = 110000;
            NPC.defense = 70;
            NPC.damage = 110;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.boss = true;
            Music = MusicManagementSystem.MusicSlots["Athena_Awakened"];
            NPC.noTileCollide = true;
            SpawnModBiomes = [ModContent.GetInstance<AcropolisBiome>().Type];
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,
                new FlavorTextBestiaryInfoElement("Mods.AAModClassic.Bestiary.OlympianAthena")
            ]);
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * balance);
            NPC.damage = (int)(NPC.damage * 0.6f);
        }

        public float[] internalAI = new float[4];

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(internalAI[0]);
                writer.Write(internalAI[1]);
                writer.Write(internalAI[2]);
                writer.Write(internalAI[3]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                internalAI[0] = reader.ReadSingle();
                internalAI[1] = reader.ReadSingle();
                internalAI[2] = reader.ReadSingle();
                internalAI[3] = reader.ReadSingle();
            }
        }

        public override void AI()
        {
            if (!NPC.HasPlayerTarget)
            {
                NPC.TargetClosest();
            }
            Player player = Main.player[NPC.target];

            if (internalAI[2] == 0 && NPC.life < NPC.lifeMax / 3 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC n1 = NPC.NewNPCDirect(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<AthenaCloneDark>());
                NPC n2 = NPC.NewNPCDirect(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<AthenaCloneLight>());
                Main.BestiaryTracker.Sights.RegisterWasNearby(n1);
                Main.BestiaryTracker.Sights.RegisterWasNearby(n2);
                internalAI[2] = 1;
                NPC.netUpdate = true;
            }

            Vector2 targetPos;

            switch ((int)NPC.ai[0])
            {
                case 0:
                    if (!AliveCheck(player))
                        break;
                    targetPos = player.Center;
                    targetPos.X += 500 * (NPC.Center.X < targetPos.X ? -1 : 1);
                    
                    for(int pos = -200; pos < 200; pos += 50)
                    {
                        targetPos.Y = player.Center.Y - pos;
                        if(Collision.CanHit(targetPos, NPC.width, NPC.height, player.position, player.width, player.height))
                        {
                            break;
                        }
                    }
                    MoveToVector2(targetPos);

                    BaseAI.ShootPeriodic(NPC, player.position, player.width, player.height, ModContent.ProjectileType<AthenaA_VarianBurst>(), ref NPC.ai[1], 50, 36, 10, true);

                    if (internalAI[3]++ >= 250 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int Choice = Main.rand.Next(2);
                        if (Choice == 0)
                        {
                            NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X + 100, (int)NPC.Center.Y, ModContent.NPCType<OlympianDragon>(), ai1: 1);
                            NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X - 100, (int)NPC.Center.Y, ModContent.NPCType<OlympianDragon>(), ai1: 1);
                        }
                        else
                        {
                            NPC Seraph1 = Main.npc[NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y + 150, ModContent.NPCType<SeraphA>())];
                            for (int i = 0; i < 3; i++)
                            {
                                Dust.NewDust(Seraph1.position, Seraph1.height, Seraph1.width, ModContent.DustType<FeatherDust>(), Main.rand.Next(-1, 2), 1, 0);
                            }
                            NPC Seraph2 = Main.npc[NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X + 150, (int)NPC.Center.Y - 75, ModContent.NPCType<SeraphA>())];
                            for (int i = 0; i < 3; i++)
                            {
                                Dust.NewDust(Seraph2.position, Seraph2.height, Seraph2.width, ModContent.DustType<FeatherDust>(), Main.rand.Next(-1, 2), 1, 0);
                            }
                            NPC Seraph3 = Main.npc[NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X + 150, (int)NPC.Center.Y - 75, ModContent.NPCType<SeraphA>())];
                            for (int i = 0; i < 3; i++)
                            {
                                Dust.NewDust(Seraph3.position, Seraph3.height, Seraph3.width, ModContent.DustType<FeatherDust>(), Main.rand.Next(-1, 2), 1, 0);
                            }
                        }
                        internalAI[3] = 0;
                        NPC.netUpdate = true;
                    }
                    if (NPC.ai[2]++ > 560)
                    {
                        Teleport(1);
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        NPC.ai[3] = 0;
                    }
                    break;
                case 1:
                    if (!AliveCheck(player))
                        break;
                    targetPos = player.Center;
                    targetPos.X += 500 * (NPC.Center.X < targetPos.X ? -1 : 1);

                    for(int pos = -200; pos < 200; pos += 50)
                    {
                        targetPos.Y = player.Center.Y - pos;
                        if(Collision.CanHit(targetPos, NPC.width, NPC.height, player.position, player.width, player.height))
                        {
                            break;
                        }
                    }

                    MoveToVector2(targetPos);

                    BaseAI.ShootPeriodic(NPC, player.position, player.width, player.height, ModContent.ProjectileType<AthenaA_SwiftwindSpear>(), ref NPC.ai[1], 100, 36, 10, true);

                    if (NPC.ai[2]++ > 400)
                    {
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        NPC.ai[3] = 0;
                    }

                    break;
                case 2:
                    if (!AliveCheck(player))
                        break;

                    NPC.velocity *= 0;
                    if (NPC.ai[2] == 0)
                    {
                        Teleport(0);
                    }

                    NPC.ai[2]++;

                    if (NPC.ai[2] == 120)
                    {
                        int projType = ModContent.ProjectileType<AthenaA_RazorGust>();
                        float spread = 45f * 0.0174f;
                        Vector2 dir = Vector2.Normalize(player.Center - NPC.Center);
                        dir *= 14f;
                        float baseSpeed = (float)Math.Sqrt(dir.X * dir.X + dir.Y * dir.Y);
                        double startAngle = Math.Atan2(dir.X, dir.Y) - .1d;
                        double deltaAngle = spread / 6f;
                        for (int i = 0; i < 3; i++)
                        {
                            double offsetAngle = startAngle + deltaAngle * i;
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), projType, 55, 5, -1);
                        }
                    }
                    if (NPC.ai[2] == 180 && NPC.life < NPC.lifeMax / 2)
                    {
                        int projType = ModContent.ProjectileType<AthenaA_RazorGust>();
                        float spread = 45f * 0.0174f;
                        Vector2 dir = Vector2.Normalize(player.Center - NPC.Center);
                        dir *= 14f;
                        float baseSpeed = (float)Math.Sqrt(dir.X * dir.X + dir.Y * dir.Y);
                        double startAngle = Math.Atan2(dir.X, dir.Y) - .1d;
                        double deltaAngle = spread / 6f;
                        for (int i = 0; i < 3; i++)
                        {
                            double offsetAngle = startAngle + deltaAngle * i;
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), projType, 55, 5, -1);
                        }
                    }
                    if (NPC.ai[2] > 220)
                    {
                        if (NPC.ai[3] < Repeats())
                        {
                            NPC.ai[2] = 0;
                            NPC.ai[3]++;
                        }
                        else
                        {
                            Teleport(2);
                            NPC.ai[0]++;
                            NPC.ai[1] = 0;
                            NPC.ai[2] = 0;
                            NPC.ai[3] = 0;
                        }
                    }
                    break;
                case 3:

                    NPC.ai[1]++;

                    targetPos = player.Center;
                    targetPos.Y -= 500;
                    MoveToVector2(targetPos);

                    if (NPC.ai[1] == 120)
                    {
                        int a = Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(8f, -8f), ModContent.ProjectileType<AthenaA_ChargedVarianBurst>(), 55, 3);
                        Main.projectile[a].Center = NPC.Center;
                        int b = Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(8f, 8f), ModContent.ProjectileType<AthenaA_ChargedVarianBurst>(), 55, 3);
                        Main.projectile[b].Center = NPC.Center;
                        int c = Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(-8f, 8f), ModContent.ProjectileType<AthenaA_ChargedVarianBurst>(), 55, 3);
                        Main.projectile[c].Center = NPC.Center;
                        int d = Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(-8f, -8f), ModContent.ProjectileType<AthenaA_ChargedVarianBurst>(), 55, 3);
                        Main.projectile[d].Center = NPC.Center;
                    }
                    if (NPC.ai[1] > 180)
                    {
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        NPC.ai[3] = 0;
                    }
                    break;
                case 4: //prepare for queen bee dashes
                    if (!AliveCheck(player))
                        break;
                    if (++NPC.ai[1] > 30)
                    {
                        targetPos = player.Center;
                        targetPos.X += 1000 * (NPC.Center.X < targetPos.X ? -1 : 1);
                        DashMovement(targetPos, 0.8f);
                        if (NPC.ai[1] > 180 || Math.Abs(NPC.Center.Y - targetPos.Y) < 50) //initiate dash
                        {
                            NPC.ai[0]++;
                            NPC.ai[1] = 0;
                            NPC.netUpdate = true;
                            NPC.velocity.X = -30 * (NPC.Center.X < player.Center.X ? -1 : 1);
                            NPC.velocity.Y *= 0.1f;
                        }
                    }
                    else
                    {
                        NPC.velocity *= 0.9f; //decelerate briefly
                    }
                    NPC.rotation = 0;
                    break;

                case 5: //dashing
                    if (++NPC.ai[1] > 240 || (Math.Sign(NPC.velocity.X) > 0 ? NPC.Center.X > player.Center.X + 900 : NPC.Center.X < player.Center.X - 900))
                    {
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        if (++NPC.ai[3] >= Repeats()) //repeat dash three times
                        {
                            NPC.ai[0]++;
                            NPC.ai[3] = 0;
                        }
                        else
                            NPC.ai[0]--;
                        NPC.netUpdate = true;
                    }
                    break;
                default:
                    NPC.ai[0] = 0;
                    goto case 0;

            }

            if (player.Center.X < NPC.Center.X + 200)
            {
                NPC.direction = -1;
            }
            else
            {
                NPC.direction = 1;
            }

            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                NPC.rotation = NPC.velocity.X * 0.03F;
            else
                NPC.rotation = 0;
        }

        public int Repeats()
        {
            if (NPC.life < NPC.lifeMax * (2/3))
            {
                return 5;
            }
            else if (NPC.life < NPC.lifeMax / 3)
            {
                return 4;
            }
            else
            {
                return 3;
            }
        }

        public void Teleport(int where)
        {
            Vector2 position = NPC.Center + Vector2.One * -20f;
            int num84 = 40;
            int height3 = num84;
            for (int num85 = 0; num85 < 3; num85++)
            {
                int num86 = Dust.NewDust(position, num84, height3, DustID.Granite, 0f, 0f, 100, default, 1.5f);
                Main.dust[num86].position = NPC.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f;
            }
            for (int num87 = 0; num87 < 15; num87++)
            {
                int num88 = Dust.NewDust(position, num84, height3, DustID.Electric, 0f, 0f, 50, default, 3.7f);
                Main.dust[num88].position = NPC.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f;
                Main.dust[num88].noGravity = false;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity *= 3f;
                Main.dust[num88].velocity += NPC.DirectionTo(Main.dust[num88].position) * (2f + Main.rand.NextFloat() * 4f);
                num88 = Dust.NewDust(position, num84, height3, DustID.Electric, 0f, 0f, 25, default, 1.5f);
                Main.dust[num88].position = NPC.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f;
                Main.dust[num88].velocity *= 2f;
                Main.dust[num88].noGravity = false;
                Main.dust[num88].fadeIn = 1f;
                Main.dust[num88].color = Color.Black * 0.5f;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity += NPC.DirectionTo(Main.dust[num88].position) * 8f;
            }
            for (int num89 = 0; num89 < 10; num89++)
            {
                int num90 = Dust.NewDust(position, num84, height3, DustID.Electric, 0f, 0f, 0, default, 2.7f);
                Main.dust[num90].position = NPC.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(NPC.velocity.ToRotation(), default) * num84 / 2f;
                Main.dust[num90].noGravity = false;
                Main.dust[num90].noLight = true;
                Main.dust[num90].velocity *= 3f;
                Main.dust[num90].velocity += NPC.DirectionTo(Main.dust[num90].position) * 2f;
            }
            for (int num91 = 0; num91 < 30; num91++)
            {
                int num92 = Dust.NewDust(position, num84, height3, DustID.Electric, 0f, 0f, 0, default, 1.5f);
                Main.dust[num92].position = NPC.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(NPC.velocity.ToRotation(), default) * num84 / 2f;
                Main.dust[num92].noGravity = false;
                Main.dust[num92].velocity *= 3f;
                Main.dust[num92].velocity += NPC.DirectionTo(Main.dust[num92].position) * 3f;
            }
            if (where == 0)
            {
                NPC.Center = CloudPick();
                NPC.netOffset = Vector2.Zero;
            }
            else if (where == 1)
            {
                Vector2 targetPos = Main.player[NPC.target].Center;
                targetPos.X += 500 * (NPC.Center.X < targetPos.X ? 1 : -1);
                targetPos.Y -= 200;
                NPC.position = targetPos;
                NPC.netOffset = Vector2.Zero;
            }
            else
            {
                NPC.position = new Vector2(Origin.X + 79 * 16, Origin.Y + 79 * 16);
                NPC.netOffset = Vector2.Zero;
            }

            position = NPC.Center + Vector2.One * -20f;
            num84 = 40;
            height3 = num84;
            for (int num85 = 0; num85 < 3; num85++)
            {
                int num86 = Dust.NewDust(position, num84, height3, DustID.Granite, 0f, 0f, 100, default, 1.5f);
                Main.dust[num86].position = NPC.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f;
            }
            for (int num87 = 0; num87 < 15; num87++)
            {
                int num88 = Dust.NewDust(position, num84, height3, DustID.Electric, 0f, 0f, 50, default, 3.7f);
                Main.dust[num88].position = NPC.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f;
                Main.dust[num88].noGravity = true;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity *= 3f;
                Main.dust[num88].velocity += NPC.DirectionTo(Main.dust[num88].position) * (2f + Main.rand.NextFloat() * 4f);
                num88 = Dust.NewDust(position, num84, height3, DustID.Electric, 0f, 0f, 25, default, 1.5f);
                Main.dust[num88].position = NPC.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f;
                Main.dust[num88].velocity *= 2f;
                Main.dust[num88].noGravity = true;
                Main.dust[num88].fadeIn = 1f;
                Main.dust[num88].color = Color.Black * 0.5f;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity += NPC.DirectionTo(Main.dust[num88].position) * 8f;
            }
            for (int num89 = 0; num89 < 10; num89++)
            {
                int num90 = Dust.NewDust(position, num84, height3, DustID.Electric, 0f, 0f, 0, default, 2.7f);
                Main.dust[num90].position = NPC.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(NPC.velocity.ToRotation(), default) * num84 / 2f;
                Main.dust[num90].noGravity = true;
                Main.dust[num90].noLight = true;
                Main.dust[num90].velocity *= 3f;
                Main.dust[num90].velocity += NPC.DirectionTo(Main.dust[num90].position) * 2f;
            }
            for (int num91 = 0; num91 < 30; num91++)
            {
                int num92 = Dust.NewDust(position, num84, height3, DustID.Electric, 0f, 0f, 0, default, 1.5f);
                Main.dust[num92].position = NPC.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(NPC.velocity.ToRotation(), default) * num84 / 2f;
                Main.dust[num92].noGravity = true;
                Main.dust[num92].velocity *= 3f;
                Main.dust[num92].velocity += NPC.DirectionTo(Main.dust[num92].position) * 3f;
            }
        }


        public Vector2 CloudPick()
        {
            int CloudChoice = Main.rand.Next(12);
            Vector2 Cloud1 = new Vector2(Origin.X + 79 * 16, Origin.Y + 10 * 16);
            Vector2 Cloud2 = new Vector2(Origin.X + 112 * 16, Origin.Y + 19 * 16);
            Vector2 Cloud3 = new Vector2(Origin.X + 135 * 16, Origin.Y + 40 * 16);
            Vector2 Cloud4 = new Vector2(Origin.X + 140 * 16, Origin.Y + 69 * 16);
            Vector2 Cloud5 = new Vector2(Origin.X + 135 * 16, Origin.Y + 99 * 16);
            Vector2 Cloud6 = new Vector2(Origin.X + 112 * 16, Origin.Y + 120 * 16);
            Vector2 Cloud7 = new Vector2(Origin.X + 79 * 16, Origin.Y + 129 * 16);
            Vector2 Cloud8 = new Vector2(Origin.X + 46 * 16, Origin.Y + 120 * 16);
            Vector2 Cloud9 = new Vector2(Origin.X + 23 * 16, Origin.Y + 99 * 16);
            Vector2 Cloud10 = new Vector2(Origin.X + 18 * 16, Origin.Y + 69 * 16);
            Vector2 Cloud11 = new Vector2(Origin.X + 23 * 16, Origin.Y + 40 * 16);
            Vector2 Cloud12 = new Vector2(Origin.X + 46 * 16, Origin.Y + 19 * 16);
            if (CloudChoice == 1)
            {
                return Cloud2;
            }
            else if (CloudChoice == 2)
            {
                return Cloud3;
            }
            else if (CloudChoice == 3)
            {
                return Cloud4;
            }
            else if (CloudChoice == 4)
            {
                return Cloud5;
            }
            else if (CloudChoice == 5)
            {
                return Cloud6;
            }
            else if (CloudChoice == 6)
            {
                return Cloud7;
            }
            else if (CloudChoice == 7)
            {
                return Cloud8;
            }
            else if (CloudChoice == 8)
            {
                return Cloud9;
            }
            else if (CloudChoice == 9)
            {
                return Cloud10;
            }
            else if (CloudChoice == 10)
            {
                return Cloud11;
            }
            else if (CloudChoice == 11)
            {
                return Cloud12;
            }
            else
            {
                return Cloud1;
            }

        }

        private bool AliveCheck(Player player)
        {
            ZAAPlayer modPlayer = player.GetModPlayer<ZAAPlayer>();
            Vector2 Acropolis = new Vector2(Origin.X + 79 * 16, Origin.Y + 79 * 16);
            if (player.dead || !player.active || Vector2.Distance(NPC.position, player.position) > 6000 || !modPlayer.ZoneAcropolis || Vector2.Distance(Acropolis, player.position) > 1500)
            {
                NPC.TargetClosest();
                if (Main.player[NPC.target].dead || !Main.player[NPC.target].active || Math.Abs(Vector2.Distance(NPC.position, Main.player[NPC.target].position)) > 6000 || !Main.player[NPC.target].GetModPlayer<ZAAPlayer>().ZoneAcropolis || Vector2.Distance(Acropolis, Main.player[NPC.target].position) > 1500)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Athena.Awakened.Kill"), Color.CornflowerBlue);
                    int p = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<AthenaFlee>());
                    Main.npc[p].Center = NPC.Center;
                    NPC.active = false;
                    NPC.netUpdate = true;
                    return false;
                }
            }
            if (NPC.timeLeft < 600)
                NPC.timeLeft = 600;
            return true;
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter >= 6)
            {
                NPC.frame.Y += frameHeight;
                NPC.frameCounter = 0;
            }
            if (NPC.frame.Y >= frameHeight * 7)
            {
                NPC.frame.Y = 0;
            }

            NPC.spriteDirection = NPC.direction;
        }

        public void MoveToVector2(Vector2 p)
        {
            float moveSpeed = 16f;
            float velMultiplier = 1f;
            Vector2 dist = p - NPC.Center;
            float length = dist == Vector2.Zero ? 0f : dist.Length();
            if (length < moveSpeed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
            }
            if (length < 200f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 100f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 50f)
            {
                moveSpeed *= 0.5f;
            }
            NPC.velocity = length == 0f ? Vector2.Zero : Vector2.Normalize(dist);
            NPC.velocity *= moveSpeed;
            NPC.velocity *= velMultiplier;
        }

        private void DashMovement(Vector2 targetPos, float speedModifier)
        {
            if (NPC.Center.X < targetPos.X)
            {
                NPC.velocity.X += speedModifier;
                if (NPC.velocity.X < 0)
                    NPC.velocity.X += speedModifier * 2;
            }
            else
            {
                NPC.velocity.X -= speedModifier;
                if (NPC.velocity.X > 0)
                    NPC.velocity.X -= speedModifier * 2;
            }
            if (NPC.Center.Y < targetPos.Y)
            {
                NPC.velocity.Y += speedModifier;
                if (NPC.velocity.Y < 0)
                    NPC.velocity.Y += speedModifier * 2;
            }
            else
            {
                NPC.velocity.Y -= speedModifier;
                if (NPC.velocity.Y > 0)
                    NPC.velocity.Y -= speedModifier * 2;
            }
            if (Math.Abs(NPC.velocity.X) > 30)
                NPC.velocity.X = 30 * Math.Sign(NPC.velocity.X);
            if (Math.Abs(NPC.velocity.Y) > 30)
                NPC.velocity.Y = 30 * Math.Sign(NPC.velocity.Y);
        }

        public override void BossLoot(ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }

        public override void OnKill()
        {
            if (!NPCExtensions.BeenKilled<AthenaA>(true))
            {
                int p = NPC.NewNPC(NPC.GetSource_Death(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<AthenaDefeat>(), 0, 0, 0, 1);
                Main.npc[p].Center = NPC.Center;

                if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                {
                    TileProtectionSystem.UnprotectTiles(ModContent.TileType<SkymarbleBrick_Tile>(), ModContent.TileType<SkycrystalBrick_Tile>(), ModContent.TileType<SkyCrystal_Tile>());
                    TileProtectionSystem.UnprotectWalls(ModContent.WallType<AcropolisBrickWall_Wall>(), ModContent.WallType<AcropolisPillarWall_Wall>());
                }
            }
            else
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Athena.Awakened.Defeat.Repeat"), Color.CornflowerBlue);
                int p = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<AthenaFlee>());
                Main.npc[p].Center = NPC.Center;
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<AthenaATreasureBag>()));

            npcLoot.AddLoreItemDrop<AthenaA>(ModContent.ItemType<OlympianAthenaLore>());

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<StarChart>()));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AthenaATrophy>(), 10));

            LeadingConditionRule masterMode = new(new AAConditions.RevOrMaster());

            masterMode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<AthenaARelic>()));

            npcLoot.Add(masterMode);

            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<AthenaAMask>(), 7));

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<GoddessFeather>(), 1, 20, 25));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<SkyCrystal>(), 1, 25, 40));

            int[] lootTable = { ModContent.ItemType<HurricaneStone>(), ModContent.ItemType<Olympia>(), ModContent.ItemType<Windfury>(), ModContent.ItemType<GaleForce>() };

            notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, lootTable));

            npcLoot.Add(notExpertRule);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D tex = TextureAssets.Npc[NPC.type].Value;
            Texture2D tex2 = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
            Texture2D tex3 = ModContent.Request<Texture2D>(Texture + "_Glow1").Value;
            BaseDrawing.DrawAfterimage(spriteBatch, tex, 0, NPC.position, NPC.width, NPC.height, NPC.oldPos, NPC.scale, NPC.rotation, NPC.direction, 7, NPC.frame, 1f, 1f, 5, false, 0f, 0f, Color.CornflowerBlue);

            spriteBatch.Draw(tex, NPC.Center - screenPos, NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.SpriteEffectDirection(true), 0);
            spriteBatch.Draw(tex2, NPC.Center - screenPos, NPC.frame, AAColor.Flash, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.SpriteEffectDirection(true), 0);
            spriteBatch.Draw(tex3, NPC.Center - screenPos, NPC.frame, Color.White, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.SpriteEffectDirection(true), 0);
            return false;
        }
    }
}