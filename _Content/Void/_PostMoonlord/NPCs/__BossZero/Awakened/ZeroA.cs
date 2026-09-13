using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma.Awakened;
using AAModClassic._Content.Void._PostMoonlord.Items._BossZero;
using AAModClassic._Content.Void._PostMoonlord.Items._BossZero.BossStandard;
using AAModClassic._Content.Void.World.Biomes;
using AAModClassic._CrossMod.CalamityMod.LoreItems;
using AAModClassic._Removed.Content._Tinker._PostMoonlord.Items.Accessories;
using AAModClassic._Unreleased.Content.Void._PostMoonLord.NPCs.InfinityZero;
using AAModClassic.Achievements;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Music;
using AAModClassic.UI.Core;
using AAModClassic.UI.Titles;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items.AAConditions;

namespace AAModClassic._Content.Void._PostMoonlord.NPCs.__BossZero.Awakened
{
    [AutoloadBossHead]
    public class ZeroA : ModNPC
    {
        public int timer;
        public static int type;
        public int damage = 0;
        public bool PlayerDead = false;

        public bool Counterattack = false;
        public int deathTimer = 0;

        public static Asset<Texture2D> Glowmask;
        public static Asset<Texture2D> Trail;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("ZER0 PR0T0C0L");
            Main.npcFrameCount[NPC.type] = 7; 
            NPCID.Sets.TrailCacheLength[NPC.type] = 20;
            NPCID.Sets.TrailingMode[NPC.type] = 0;

            Glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
            Trail = ModContent.Request<Texture2D>(Texture + "_Trail");
            NPCID.Sets.BossBestiaryPriority.Add(Type);
        }
        public override void SetDefaults()
        {
            NPC.lifeMax = 500000;
            NPC.damage = 120;
            NPC.defense = 70;
            NPC.knockBackResist = 0f;
            NPC.width = 170;
            NPC.height = 170;
            NPC.friendly = false;
            NPC.aiStyle = NPCAIStyleID.FaceClosestPlayer;
            NPC.value = Item.buyPrice(0, 40, 0, 0);
            NPC.npcSlots = 1f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = new SoundStyle("AAModClassic/Sounds/Zerohit");
            NPC.DeathSound = new SoundStyle("AAModClassic/Sounds/ZeroDeath");
            Music = MusicManagementSystem.MusicSlots["Zero_Awakened"];
            SceneEffectPriority = SceneEffectPriority.BossHigh;
            NPC.netAlways = true;
            NPC.npcSlots = 200;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
            SpawnModBiomes = [ModContent.GetInstance<VoidBiome>().Type];
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.Add(new ColoredFlavorTextBestiaryInfoElement("Mods.AAModClassic.Bestiary.ZeroProtocol", AAColor.OblivionDialogue));
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.damage = (int)(NPC.damage * .7f);
            NPC.lifeMax = (int)(NPC.lifeMax * 0.5f * balance);
        }

        public float[] Move = new float[4];
        public float[] start = new float[1];
        public float[] Minion = new float[1];
        public int[] Counter = new int[3];

        Vector2 ShootDir = new Vector2(0,0);

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(Move[0]);
                writer.Write(Move[1]);
                writer.Write(Move[2]);
                writer.Write(Move[3]);
                writer.Write(Counter[0]); //the hit TP counter
                writer.Write(Counter[1]); //the charge counter
                writer.Write(Counter[2]); //the pointchange counter
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                Move[0] = reader.ReadSingle();
                Move[1] = reader.ReadSingle();
                Move[2] = reader.ReadSingle();
                Move[3] = reader.ReadSingle();
                Counter[0] = reader.ReadInt32();
                Counter[1] = reader.ReadInt32();
                Counter[2] = reader.ReadInt32();
            }
        }

        public override void OnKill()
        {
            if (Main.expertMode)
            {
                Projectile.NewProjectile(NPC.GetSource_Death(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(0f, 0f), ModContent.ProjectileType<ZeroDeath1>(), 0, 0);

                if (NPC.playerInteraction[Main.myPlayer])
                    ZeroKilled.Condition.Complete();

                if (!NPC.BeenKilled(true))
                    NPC.OnGameEventClearedForTheFirstTime(-1);
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<ZeroTreasureBag>()));

            npcLoot.AddLoreItemDrop<ZeroA>(ModContent.ItemType<ZeroLore>());

            LeadingConditionRule masterMode = new(new AAConditions.RevOrMaster());

            masterMode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ZeroRelic>()));

            npcLoot.Add(masterMode);

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ZeroATrophy>(), 10));

            LeadingConditionRule firstKill = new(new FirstTimeKillingZeroP());

            firstKill.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ERROR_NULL>()));

            LeadingConditionRule shenDefeated = new(new AkumaAHead.ShenDefeated());

            shenDefeated.OnSuccess(ItemDropRule.Common(ModContent.ItemType<EXSoul>(), 50));

            npcLoot.Add(firstKill);
            npcLoot.Add(shenDefeated);

            LeadingConditionRule anceintsDownAndRemoved = new(new PostLateAncientsAndRemovedWorldAndExpert());

            anceintsDownAndRemoved.OnSuccess(ItemDropRule.Common(ModContent.ItemType<RealityStone>(), 50));

            npcLoot.Add(anceintsDownAndRemoved);
        }

        public class FirstTimeKillingZeroP : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !NPCExtensions.BeenKilled<ZeroA>(true);
            public bool CanShowItemDropInUI() => true;
            public string GetConditionDescription() => null;
        }

        public override void BossLoot(ref int potionType)
        {
            if (Main.expertMode)
            {
                potionType = ItemID.SuperHealingPotion;
            }
            else
            {
                potionType = 0;
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Counter[0] > 3000 && NPC.ai[0] != 4 && NPC.ai[0] != 2 && !isCharging)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Teleport(Main.rand.NextBool(2) ? 1:2);
                    NPC.ai[0] = 4;
                    NPC.ai[1] = 0;
                    NPC.ai[2] = 0;
                    NPC.ai[3] = 0;
                    Counter[0] = 0;
                    Counterattack = true;
                    NPC.netUpdate = true;
                }
            }
            else if(NPC.ai[0] != 4 && NPC.ai[0] != 2 && !isCharging)
            {
                int TeleportChance = 1000 * (NPC.life / NPC.lifeMax);
                if (TeleportChance < 10)
                {
                    TeleportChance = 10;
                }
                if (Main.rand.Next(TeleportChance) == 0)
                {
                    Teleport(0);
                }
            }
            if (NPC.life <= 0 && !Main.expertMode)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Zero.Awakened.Defeat.Cheat"), Color.Red.R, Color.Red.G, Color.Red.B);
            }
        }

        public float auraPercent = 0f;
        public bool auraDirection = true;

        int Frame = 0;

        public override void FindFrame(int frameHeight)
        {
            int frameWidth = TextureAssets.Npc[NPC.type].Width() / 2;
            NPC.frame.Width = frameWidth;

            if (NPC.frameCounter++ > 3)
            {
                NPC.frameCounter = 0;
                Frame += 1;
            }

            if (Frame > 6)
            {
                Frame = 0;
            }

            NPC.frame.X = isCharging ? frameWidth : 0;
            NPC.frame.Y = frameHeight * Frame;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (auraDirection) 
            { 
                auraPercent += 0.1f; 
                auraDirection = auraPercent < 1f; 
            }
            else 
            { 
                auraPercent -= 0.1f; 
                auraDirection = auraPercent <= 0f; 
            }

            if(NPC.IsABestiaryIconDummy || (!(NPC.ai[0] == 4 && NPC.CountNPCS(ModContent.NPCType<ZeroEcho>()) > 0 && !Counterattack)))
            {
                Color c = AAColor.Oblivion;
                if (NPC.IsABestiaryIconDummy)
                    c = Color.Red;

                //BaseDrawing.DrawAfterimage(spriteBatch, Trail.Value, 0, NPC, 1, 1, 8, true, 0, 0, Color.Black, NPC.frame, 7);
                if(!NPC.IsABestiaryIconDummy)
                    DrawingUtils.DrawCenteredAfterimages(NPC, NPCID.Sets.TrailingMode[NPC.type], Color.Black, 1, Trail.Value);
                spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos, NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, NPC.SpriteEffectDirection(), 0f);
                DrawingUtils.DrawAura(spriteBatch, Glowmask.Value, NPC, auraPercent, 1, 0, 0, c);
                spriteBatch.Draw(Glowmask.Value, NPC.Center - screenPos, NPC.frame, c, NPC.rotation, NPC.frame.Size() / 2f, NPC.scale, NPC.SpriteEffectDirection(), 0f);
            }
            
            return false;
        }

        bool isCharging = false;

        public override void AI()
        {
            NPC.GetGlobalNPC<TitleGlobalNPC>().ShowTitle = true;

            NPC.height = 120;
            if (!isCharging)
                NPC.height = 170;

            if (NPC.ai[0] == 4 && NPC.CountNPCS(ModContent.NPCType<ZeroEcho>()) > 0 && !Counterattack || isCharging)
            {
                NPC.chaseable = false;
                NPC.defense = 9999;
            }
            else
            {
                NPC.chaseable = true;
                NPC.defense = NPC.defDefense;
            }
            
            int Repeats;
            if (NPC.life < NPC.life * (2 / 3))
            {
                Counter[0] ++;
                Counter[1] ++;
                Repeats = 4;
            }
            else if (NPC.life < NPC.life / 3)
            {
                Counter[0] ++;
                Counter[1] ++;
                Repeats = 5;
            }
            else
            {
                Counter[0] += 2;
                Counter[1] += 2;
                Repeats = 3;
            }
            NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];

            if (!AliveCheck(player)) return;

            if(Counter[0] >= 4000)
            {
                Counter[0] = 4000;
            }

            if(Counter[1] >= 6000)
            {
                Counter[1] = 0;
                isCharging = true;
                NPC.ai[0] = 5;
                NPC.ai[1] = 0;
                NPC.ai[2] = 0;
                NPC.ai[3] = 0;
            }

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                AAWorld.zeroUS = false;
            }

            for (int m = NPC.oldPos.Length - 1; m > 0; m--)
            {
                NPC.oldPos[m] = NPC.oldPos[m - 1];
            }
            NPC.oldPos[0] = NPC.position;

            int Changerate = NPC.life < NPC.lifeMax / 2 ? 150 : 120;

            if (NPC.ai[2]++ > Changerate && !Counterattack)
            {
                if (NPC.ai[0] != 0)
                {
                    NPC.velocity *= .0f;
                }
                switch (NPC.ai[0])
                {
                    case 0:
                        if (!AliveCheck(player))
                            break;
                        float spread = 45f * 0.0174f;
                        Vector2 dir = Vector2.Normalize(player.Center - NPC.Center);
                        dir *= 12f;
                        float baseSpeed = (float)Math.Sqrt(dir.X * dir.X + dir.Y * dir.Y);
                        double startAngle = Math.Atan2(dir.X, dir.Y) - .1d;
                        double deltaAngle = spread / 6f;
                        if (NPC.ai[2] % 30 == 0)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                if (NPC.ai[2] % Main.rand.Next(10) == 0 && Main.rand.NextBool(2))
                                {
                                    double offsetAngle = startAngle + deltaAngle * i;
                                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), ModContent.ProjectileType<ZeroA_StaticSphere>(), 30, 5, -1);
                                }
                            }
                        }
                        if (NPC.ai[2] > 271)
                        {
                            AIChange();
                        }
                        break;
                    case 1:
                        if (!AliveCheck(player))
                            break;
                        if (NPC.ai[2] % 30 == 0 && NPC.ai[2] < 121)
                        {
                            Teleport(3);
                        }

                        if (NPC.ai[2] % 60 == 30)
                        {
                            Attack(Main.rand.Next(4));
                        }

                        if (NPC.ai[3] < Repeats && NPC.ai[2] > 280)
                        {
                            NPC.ai[3]++;
                            NPC.ai[2] = Changerate;
                        }
                        else
                        {
                            AIChange();
                        }

                        break;
                    case 2:
                        if (!AliveCheck(player))
                            break;
                        NPC.velocity *= 0;
                        if (NPC.ai[2] == 280)
                        {
                            Teleport(3);
                            if (NPC.life > NPC.lifeMax / 2)
                            {
                                if(Main.netMode != NetmodeID.MultiplayerClient)
                                {
                                    if (Main.rand.NextBool(2))
                                    {
                                        int dirY = player.velocity.Y > 0? 1:-1;

                                        int yPos = Math.Abs(player.velocity.Y) > 4f? -500 * dirY : -750 * dirY;

                                        for (int z = 0; z < 7; z++)
                                        {
                                            int a1 = Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(player.Center.X, player.Center.Y), Vector2.Zero, ModContent.ProjectileType<ZeroA_EchoGigablast>(), damage, 3, Main.myPlayer, 0f, 0f);
                                            int a2 = Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(player.Center.X, player.Center.Y), Vector2.Zero, ModContent.ProjectileType<ZeroA_EchoGigablast>(), damage, 3, Main.myPlayer, 1f, 0f);
                                            Main.projectile[a1].Center = player.Center + new Vector2(-500, yPos);
                                            Main.projectile[a2].Center = player.Center + new Vector2(500, yPos);
                                            yPos += 250 * dirY;
                                        }
                                    }
                                    else
                                    {
                                        int dirX = player.velocity.X > 0? 1:-1;

                                        int xPos = Math.Abs(player.velocity.X) > 4f? -500 * dirX : -750 * dirX;

                                        for (int z = 0; z < 7; z++)
                                        {
                                            int h1 = Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(player.Center.X, player.Center.Y), Vector2.Zero, ModContent.ProjectileType<ZeroA_EchoGigablast>(), damage, 3, Main.myPlayer , 2f, 0f);
                                            int h2 = Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(player.Center.X, player.Center.Y), Vector2.Zero, ModContent.ProjectileType<ZeroA_EchoGigablast>(), damage, 3, Main.myPlayer , 3f, 0f);
                                            Main.projectile[h1].Center = player.Center + new Vector2(xPos, -500);
                                            Main.projectile[h2].Center = player.Center + new Vector2(xPos, 500);
                                            xPos += 250 * dirX;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if(Main.netMode != NetmodeID.MultiplayerClient)
                                {
                                    int dirX = player.velocity.X > 0? 1:-1;
                                    int dirY = player.velocity.Y > 0? 1:-1;

                                    int xPos = Math.Abs(player.velocity.X) > 4f? -500 * dirX : -750 * dirX;
                                    int yPos = Math.Abs(player.velocity.Y) > 4f? -500 * dirY : -750 * dirY;

                                    for (int z = 0; z < 13; z++)
                                    {
                                        int a1 = Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(player.Center.X, player.Center.Y), Vector2.Zero, ModContent.ProjectileType<ZeroA_EchoGigablast>(), damage, 3, Main.myPlayer, 0f, 0f);
                                        int a2 = Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(player.Center.X, player.Center.Y), Vector2.Zero, ModContent.ProjectileType<ZeroA_EchoGigablast>(), damage, 3, Main.myPlayer, 1f, 0f);
                                        Main.projectile[a1].Center = player.Center + new Vector2(-500, yPos);
                                        Main.projectile[a2].Center = player.Center + new Vector2(500, yPos);
                                        yPos += 250 * dirY;
                                    }
                                    for (int z = 0; z < 13; z++)
                                    {
                                        int h1 = Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(player.Center.X, player.Center.Y), Vector2.Zero, ModContent.ProjectileType<ZeroA_EchoGigablast>(), damage, 3, Main.myPlayer , 2f, 0f);
                                        int h2 = Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(player.Center.X, player.Center.Y), Vector2.Zero, ModContent.ProjectileType<ZeroA_EchoGigablast>(), damage, 3, Main.myPlayer , 3f, 0f);
                                        Main.projectile[h1].Center = player.Center + new Vector2(xPos, -500);
                                        Main.projectile[h2].Center = player.Center + new Vector2(xPos, 500);
                                        xPos += 250 * dirX;
                                    }
                                }
                            }
                        }
                        if (NPC.ai[2] > 520)
                        {
                            NPC.ai[0]++;
                            NPC.ai[1] = 0;
                            NPC.ai[2] = 0;
                            NPC.ai[3] = 0;
                            NPC.netUpdate = true;
                        }
                        break;
                    case 3:
                        if (!AliveCheck(player))
                            break;
                        if (NPC.ai[2] == (NPC.life < NPC.lifeMax / 2 ? 200 : 300))
                        {
                            Teleport(3);
                            if(Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                int a = Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(0f, -12f), ModContent.ProjectileType<ZeroA_ProtoStarRay>(), damage, 3);
                                Main.projectile[a].Center = NPC.Center + new Vector2(-100, 0);
                                int b = Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(0f, 12f), ModContent.ProjectileType<ZeroA_ProtoStarRay>(), damage, 3);
                                Main.projectile[b].Center = NPC.Center + new Vector2(100, 0);
                                int c = Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(-12f, 0), ModContent.ProjectileType<ZeroA_ProtoStarRay>(), damage, 3);
                                Main.projectile[c].Center = NPC.Center + new Vector2(0, 100);
                                int d = Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(12f, 0), ModContent.ProjectileType<ZeroA_ProtoStarRay>(), damage, 3);
                                Main.projectile[d].Center = NPC.Center + new Vector2(0, -100);
                                if (NPC.life < NPC.lifeMax / 2)
                                {
                                    int a1 = Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(0f, 12f), ModContent.ProjectileType<ZeroA_ProtoStarRay>(), damage, 3);
                                    Main.projectile[a1].Center = NPC.Center + new Vector2(-100, 0);
                                    int b1 = Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(0f, -12f), ModContent.ProjectileType<ZeroA_ProtoStarRay>(), damage, 3);
                                    Main.projectile[b1].Center = NPC.Center + new Vector2(100, 0);
                                    int c1 = Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(12f, 0), ModContent.ProjectileType<ZeroA_ProtoStarRay>(), damage, 3);
                                    Main.projectile[c1].Center = NPC.Center + new Vector2(0, 100);
                                    int d1 = Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(-12f, 0), ModContent.ProjectileType<ZeroA_ProtoStarRay>(), damage, 3);
                                    Main.projectile[d1].Center = NPC.Center + new Vector2(0, -100);
                                    int e = Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(8f, -8f), ModContent.ProjectileType<ZeroA_ProtoStarRay>(), damage, 3);
                                    Main.projectile[e].Center = NPC.Center + new Vector2(-80, -80);
                                    int e1 = Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(-8f, 8f), ModContent.ProjectileType<ZeroA_ProtoStarRay>(), damage, 3);
                                    Main.projectile[e1].Center = NPC.Center + new Vector2(-80, -80);
                                    int f = Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(-8f, 8f), ModContent.ProjectileType<ZeroA_ProtoStarRay>(), damage, 3);
                                    Main.projectile[f].Center = NPC.Center + new Vector2(80, 80);
                                    int f1 = Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(8f, -8f), ModContent.ProjectileType<ZeroA_ProtoStarRay>(), damage, 3);
                                    Main.projectile[f1].Center = NPC.Center + new Vector2(80, 80);
                                    int g = Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(-8f, -8f), ModContent.ProjectileType<ZeroA_ProtoStarRay>(), damage, 3);
                                    Main.projectile[g].Center = NPC.Center + new Vector2(-80, 80);
                                    int g1 = Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(8f, 8f), ModContent.ProjectileType<ZeroA_ProtoStarRay>(), damage, 3);
                                    Main.projectile[g1].Center = NPC.Center + new Vector2(-80, 80);
                                    int h = Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(8f, 8f), ModContent.ProjectileType<ZeroA_ProtoStarRay>(), damage, 3);
                                    Main.projectile[h].Center = NPC.Center + new Vector2(80, -80);
                                    int h1 = Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(-8f, -8f), ModContent.ProjectileType<ZeroA_ProtoStarRay>(), damage, 3);
                                    Main.projectile[h1].Center = NPC.Center + new Vector2(80, -80);
                                }
                            }
                        }

                        if (NPC.ai[2] > (NPC.life < NPC.lifeMax / 2 ? 260 : 360))
                        {
                            AIChange();
                        }

                        break;
                    case 4:
                        if (!AliveCheck(player))
                            break;
                        if (NPC.ai[2] < (NPC.life < NPC.lifeMax / 2 ? 360 : 270))
                        {
                            if (NPC.ai[3] ++  > 60)
                            {
                                NPC.ai[3] = 0;
                                Teleport(0);
                                NPC npc = NPC.NewNPCDirect(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<ZeroEcho>());
                                if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                                    npc.Center = NPC.Center;
                            }
                        }
                        if(NPC.ai[2] == (NPC.life < NPC.lifeMax / 2 ? 400 : 310))
                        {
                            NPC.ai[1] = 1f;
                        }
                        else
                        {
                            NPC.ai[1] = 0f;
                        }
                        if(NPC.ai[2] >= (NPC.life < NPC.lifeMax / 2 ? 480 : 390))
                        {
                            NPC.ai[0] = 1;
                            NPC.ai[1] = 0;
                            NPC.ai[2] = 0;
                            NPC.ai[3] = 0;
                        }
                        break;

                    case 5:
                        if (!AliveCheck(player))
                            break;
                        
                        Counterattack = false;
                        
                        if (NPC.ai[1]++ == 100)
                        {
                            if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Zero.Awakened.Self-Organization.4"), Color.Red.R, Color.Red.G, Color.Red.B);
                            if (ShootDir == new Vector2(0,0)) ShootDir = NPC.DirectionTo(player.Center);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + 60f * ShootDir, 10f * ShootDir, ModContent.ProjectileType<ZeroA_EchoDeathray>(), 100, 3f, Main.myPlayer, 0, NPC.whoAmI);
                            NPC.ai[3] = 1f;
                        }
                        else
                        {
                            NPC.ai[3] = 0f;
                        }

                        if(NPC.ai[1] < 85)
                        {
                            if(NPC.ai[2] % (NPC.life < NPC.lifeMax / 2? 40:60) == 10)
                            {
                                Teleport(3);
                                NPC.NewNPC(NPC.GetSource_FromThis(), (int)player.Center.X + 50 * Main.rand.Next(4, 6) * (Main.rand.NextBool(2) ? -1:1), (int)player.Center.Y + 50 * Main.rand.Next(4, 6) * (Main.rand.NextBool(2) ? -1:1), ModContent.NPCType<ZeroMini>());
                            }
                            NPC.rotation = NPC.DirectionTo(player.Center).ToRotation() + (float)Math.PI/2;
                            ShootDir = NPC.DirectionTo(player.Center);
                        }

                        if (NPC.ai[1] >= 190)
                        {
                            isCharging = false;
                            NPC.ai[0] = 1;
                            NPC.ai[1] = 0;
                            NPC.ai[2] = 0;
                            NPC.ai[3] = 0;
                        }
                        break;
                    
                    default:
                        if (!AliveCheck(player))
                            break;
                        NPC.ai[0] = 0;
                        goto case 0;
                }
            }
            else if(isCharging)
            {
                if(NPC.ai[2] == 10)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Zero.Awakened.Self-Organization.1"), Color.Red.R, Color.Red.G, Color.Red.B);
                    if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Zero.Awakened.Self-Organization.2"), Color.Red.R, Color.Red.G, Color.Red.B);
                }
                if(NPC.ai[2] == 40)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        string path = Main.SavePath;
                        if(ModContent.GetInstance<AAConfigClient>().HideIdentifiableInfo)
                        {
                            string name = "";
                            string previous = "";
                            bool onName = false;
                            for(int i = 0; i < path.Length; i++)
                            {
                                if (path[i] == '\\')
                                {
                                    if(onName)
                                    {
                                        name = previous;
                                        break;
                                    }
                                    if (previous.ToLower() == "users")
                                        onName = true;
                                    previous = "";
                                }
                                else
                                    previous += path[i];
                            }
                            path = path.Replace(name, PlayerIdentityHelper.GetRealName());
                        }
                        ChatUtils.Chat(Language.GetText("Mods.AAModClassic.NPCs.BossDialogue.Zero.Awakened.Self-Organization.3." + (Oblivion.IsPlayerStreaming() ? "Streaming" : "Normal")).WithFormatArgs(path.ToUpper().Replace(" ", "").Replace("O", "0")).Value, Color.Red.R, Color.Red.G, Color.Red.B);
                    }
                }
                if(NPC.ai[2] == 110)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(@"[Y]", Color.Red.R, Color.Red.G, Color.Red.B);
                }
                NPC.rotation = NPC.DirectionTo(player.Center).ToRotation() + (float)Math.PI/2;
                if(NPC.ai[2] % (NPC.life < NPC.lifeMax / 2? 60:80) == 20)
                {
                    Teleport(3);
                    if(Main.netMode != NetmodeID.MultiplayerClient) NPC.NewNPC(NPC.GetSource_FromThis(), (int)player.Center.X + 50 * Main.rand.Next(4, 6) * (Main.rand.NextBool(2) ? -1:1), (int)player.Center.Y + 50 * Main.rand.Next(4, 6) * (Main.rand.NextBool(2) ? -1:1), ModContent.NPCType<ZeroMini>());
                }
                Counterattack = false;
                NPC.ai[1] = 0f;
                NPC.ai[3] = 0f;
            }
            else if(Counterattack)
            {
                NPC.ai[2] ++;
                NPC.position = player.Center - new Vector2(0, 600);
                if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                    NPC.Center = player.Center - new Vector2(0, 400);
                NPC.netOffset = Vector2.Zero;
                NPC.velocity = Vector2.Zero;

                if(NPC.ai[2] == 240 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)player.Center.X, (int)player.Center.Y + 500, ModContent.NPCType<ZeroEcho>());
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)player.Center.X, (int)player.Center.Y - 500, ModContent.NPCType<ZeroEcho>());
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)player.Center.X + 500, (int)player.Center.Y, ModContent.NPCType<ZeroEcho>());
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)player.Center.X - 500, (int)player.Center.Y, ModContent.NPCType<ZeroEcho>());
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)player.Center.X + 500, (int)player.Center.Y + 500, ModContent.NPCType<ZeroEcho>());
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)player.Center.X - 500, (int)player.Center.Y - 500, ModContent.NPCType<ZeroEcho>());
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)player.Center.X + 500, (int)player.Center.Y - 500 , ModContent.NPCType<ZeroEcho>());
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)player.Center.X - 500, (int)player.Center.Y + 500, ModContent.NPCType<ZeroEcho>());
                }
                
                if(NPC.ai[2] == 420)
                {
                    NPC.ai[1] = 1f;
                }
                else
                {
                    NPC.ai[1] = 0f;
                }

                if(NPC.ai[2] > 520)
                {
                    Counterattack = false;
                    NPC.ai[0] = 2;
                    NPC.ai[1] = 0;
                    NPC.ai[2] = 0;
                    NPC.ai[3] = 0;
                }
                
            }
            else
            {
                BaseAI.AISkull(NPC, ref Move, true, 14, 350, .04f, .05f);

                int Frequency = Main.rand.Next(30, 50);
                if (NPC.life < NPC.lifeMax / 2)
                {
                    Frequency = Main.rand.Next(20, 50);
                }
                if (NPC.life < NPC.lifeMax / 4)
                {
                    Frequency = Main.rand.Next(10, 40);
                }
                if (Main.rand.NextBool(2))
                {
                    BaseAI.ShootPeriodic(NPC, player.position, player.width, player.height, ModContent.ProjectileType<ZeroA_GlitchBomb>(), ref NPC.ai[3], Frequency, 40, 10, true);
                }
                else
                {
                    BaseAI.ShootPeriodic(NPC, player.position, player.width, player.height, ModContent.ProjectileType<ZeroA_GlitchRocket>(), ref NPC.ai[3], Frequency, 40, 10, true);
                }
            }

            if((NPC.Center - player.Center).Length() > 2000 && !isCharging && !Counterattack)
            {
                Teleport(0);
            }

            if(NPC.ai[0] != 5)
            {
                NPC.direction = NPC.spriteDirection = 1;
                NPC.rotation = 0;
            }
        }

        private void AIChange()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.ai[0] = Main.rand.Next(5);
                NPC.ai[1] = 0;
                NPC.ai[2] = 0;
                if (NPC.ai[0] == 2 || NPC.ai[0] == 4)
                {
                    Teleport(Main.rand.NextBool(2) ? 1:2);
                }
                else if (NPC.life < NPC.lifeMax * (3 / 4) && Main.rand.NextBool(3))
                {
                    Teleport(Main.rand.NextBool(2) ? 1:2);
                }
                else if (NPC.life < NPC.lifeMax / 2 && Main.rand.NextBool(2))
                {
                    Teleport(Main.rand.NextBool(2) ? 1:2);
                }
                if (NPC.life < NPC.lifeMax / 4)
                {
                    Teleport(Main.rand.NextBool(2) ? 1:2);
                }
            }
            NPC.netUpdate = true;
        }

        public bool AliveCheck(Player player)
        {
            bool tooFar = Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) > 8000f || Math.Abs(NPC.Center.Y - Main.player[NPC.target].Center.Y) > 8000f;
            if (player.dead || tooFar || !player.active)
            {
                NPC.TargetClosest(true);

                if (Main.player[NPC.target].dead || !Main.player[NPC.target].active || tooFar)
                {
                    if (!PlayerDead)
                    {
                        if (player.dead || !player.active)
                        {
                            if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Zero.Awakened.Kill"), Color.Red.R, Color.Red.G, Color.Red.B);
                        }
                        else if (tooFar)
                        {
                            if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Zero.Awakened.Despawn"), Color.Red.R, Color.Red.G, Color.Red.B);
                        }
                        PlayerDead = true;
                    }
                    NPC.velocity.Y = NPC.velocity.Y - 0.04f;
                    if (NPC.timeLeft > 10)
                    {
                        NPC.timeLeft = 10;
                    }
                    NPC.ai[1] = 0;
                    NPC.ai[2] = 0;
                    NPC.ai[3] = 0;
                    if (NPC.position.Y + NPC.height - NPC.velocity.Y <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                        BaseAI.KillNPC(NPC); NPC.netUpdate2 = true;
                    return false;
                }
            }
            return true;
        }

        public void Teleport(int a = 0)
        {
            bool safe = false;
            Player player = Main.player[NPC.target];
            Vector2 targetPos = player.Center; 
            TPDust();

            if (a == 0)
            {
                while (!safe)
                {
                    int posX = Main.rand.Next(-500, 500);
                    int posY = Main.rand.Next(-500, 500);

                    if (posX < 50 && posX > -50 && posY < 50 && posY > -50)
                    {
                        return;
                    }
                    NPC.Center = new Vector2(targetPos.X + posX, targetPos.Y + posY);
                    NPC.netOffset = Vector2.Zero;
                    safe = true;
                }
            }
            else if (a == 1)
            {
                targetPos.X += 430 * (NPC.Center.X > targetPos.X ? -1 : 1);
                targetPos.Y -= 430;
                NPC.Center = new Vector2(targetPos.X, targetPos.Y);
                NPC.netOffset = Vector2.Zero;
            }
            else if (a == 2)
            {
                targetPos.X += 430 * (NPC.Center.X > targetPos.X ? -1 : 1);
                targetPos.Y += 430;
                NPC.Center = new Vector2(targetPos.X, targetPos.Y);
                NPC.netOffset = Vector2.Zero;
            }
            else
            {
                NPC.Center = player.Center + new Vector2(0, -200);
                NPC.netOffset = Vector2.Zero;
            }

            NPC.velocity *= 0;
            TPDust();
        }

        public void TPDust()
        {
            Vector2 position = NPC.Center + Vector2.One * -20f;
            int num84 = 40;
            int height3 = num84;
            for (int num85 = 0; num85 < 3; num85++)
            {
                int num86 = Dust.NewDust(position, num84, height3, DustID.GemRuby, 0f, 0f, 100, default, 1.5f);
                //Main.dust[num86].shader = GameShaders.Armor.GetSecondaryShader(59, Main.LocalPlayer);
                Main.dust[num86].position = NPC.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f;
            }
            for (int num87 = 0; num87 < 7; num87++)
            {
                int num88 = Dust.NewDust(position, num84, height3, DustID.GemRuby, 0, 0, 100, new Color(), 2f);
                //Main.dust[num88].shader = GameShaders.Armor.GetSecondaryShader(59, Main.LocalPlayer);
                Main.dust[num88].position = NPC.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f;
                Main.dust[num88].noGravity = true;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity *= 3f;
                Main.dust[num88].velocity += NPC.DirectionTo(Main.dust[num88].position) * (2f + Main.rand.NextFloat() * 4f);
                num88 = Dust.NewDust(position, num84, height3, DustID.GemRuby, 0, 0, 100, new Color(), 2f);
                //Main.dust[num88].shader = GameShaders.Armor.GetSecondaryShader(59, Main.LocalPlayer);
                Main.dust[num88].position = NPC.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f;
                Main.dust[num88].velocity *= 2f;
                Main.dust[num88].noGravity = true;
                Main.dust[num88].fadeIn = 1f;
                Main.dust[num88].color = Color.Black * 0.5f;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity += NPC.DirectionTo(Main.dust[num88].position) * 8f;
            }
            for (int num89 = 0; num89 < 5; num89++)
            {
                int num90 = Dust.NewDust(position, num84, height3, DustID.GemRuby, 0, 0, 100, new Color(), 2f);
                //Main.dust[num90].shader = GameShaders.Armor.GetSecondaryShader(59, Main.LocalPlayer);
                Main.dust[num90].position = NPC.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(NPC.velocity.ToRotation(), default) * num84 / 2f;
                Main.dust[num90].noGravity = true;
                Main.dust[num90].noLight = true;
                Main.dust[num90].velocity *= 3f;
                Main.dust[num90].velocity += NPC.DirectionTo(Main.dust[num90].position) * 2f;
            }
            for (int num91 = 0; num91 < 15; num91++)
            {
                int num92 = Dust.NewDust(position, num84, height3, DustID.GemRuby, 0, 0, 100, new Color(), 2f);
                //Main.dust[num92].shader = GameShaders.Armor.GetSecondaryShader(59, Main.LocalPlayer);
                Main.dust[num92].position = NPC.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(NPC.velocity.ToRotation(), default) * num84 / 2f;
                Main.dust[num92].noGravity = true;
                Main.dust[num92].velocity *= 3f;
                Main.dust[num92].velocity += NPC.DirectionTo(Main.dust[num92].position) * 3f;
            }
        }

        public void Attack(int Attack)
        {
            if (Attack == 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (i == 1)
                    {
                        NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X + 30, (int)NPC.Center.Y + 30, ModContent.NPCType<NullZP>());
                    }
                    else if (i == 2)
                    {
                        NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X + 30, (int)NPC.Center.Y - 30, ModContent.NPCType<NullZP>());
                    }
                    else if (i == 3)
                    {
                        NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X - 30, (int)NPC.Center.Y + 30, ModContent.NPCType<NullZP>());
                    }
                    else
                    {
                        NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X - 30, (int)NPC.Center.Y - 30, ModContent.NPCType<NullZP>());
                    }
                }
            }
            else if (Attack == 1)
            {

                float spread = 12f * 0.0174f;
                double startAngle = Math.Atan2(NPC.velocity.X, NPC.velocity.Y) - spread / 2;
                double deltaAngle = 6;
                double offsetAngle;
                for (int i = 0; i < 6; i++)
                {
                    offsetAngle = startAngle + deltaAngle * (i + i * i) / 2f + 32f * i;
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)(Math.Sin(offsetAngle) * 4f), (float)(Math.Cos(offsetAngle) * 2f), ModContent.ProjectileType<ZeroA_GlitchRocket>(), 67, 0, Main.myPlayer, 0f, 0f);
                }
            }
            else if (Attack == 2)
            {
                float spread = 12f * 0.0174f;
                double startAngle = Math.Atan2(NPC.velocity.X, NPC.velocity.Y) - spread / 2;
                double deltaAngle = 5;
                double offsetAngle;
                for (int i = 0; i < 5; i++)
                {
                    offsetAngle = startAngle + deltaAngle * (i + i * i) / 2f + 32f * i;
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)(Math.Sin(offsetAngle) * 4f), (float)(Math.Cos(offsetAngle) * 2f), ModContent.ProjectileType<ZeroA_ERROR>(), 67, 0, Main.myPlayer, 0f, 0f);
                }
            }
            else if (Attack == 3)
            {
                float spread = 12f * 0.0174f;
                double startAngle = Math.Atan2(NPC.velocity.X, NPC.velocity.Y) - spread / 2;
                double deltaAngle = 4;
                double offsetAngle;
                for (int i = 0; i < 4; i++)
                {
                    offsetAngle = startAngle + deltaAngle * (i + i * i) / 2f + 32f * i;
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)(Math.Sin(offsetAngle) * 2), (float)Math.Cos(offsetAngle), ModContent.ProjectileType<ZeroA_StaticSphere>(), 67, 0, Main.myPlayer, 0f, 0f);
                }
            }
        }
    }
}
