using AAModClassic._Content.Desert.___PreHardmode.NPCs.__Friendly;
using AAModClassic._Content.Desert.__Hardmode.Items._BossAnubis.BossStandard;
using AAModClassic._Content.Desert.__Hardmode.Items._BossAnubis.Weapons;
using AAModClassic._Content.Desert.__Hardmode.Items.Materials;
using AAModClassic._Content.Desert.__Hardmode.NPCs.__BossAnubis;
using AAModClassic._Content.Desert._PostMoonlord.NPCs.__BossAnubisA;
using AAModClassic._CrossMod.CalamityMod.LoreItems;
using AAModClassic._Unreleased.Content.Desert.__Hardmode.NPCs.__BossAnubis.Runes;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Music;
using AAModClassic.UI.Titles;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.Desert.__Hardmode.NPCs.__BossAnubis
{
    [AutoloadBossHead]
    public class AnubisUnreleased : ModNPC
    {
        public ref float AttackCurrent => ref NPC.ai[0];
        public ref float AttackTimer => ref NPC.ai[1];
        public ref float ShotTimer => ref NPC.ai[2];
        public ref float AttackNext => ref NPC.ai[3];

        public bool HasDonePreamble = false;
        public bool HasSpawnedLocusts = false;
        public bool IsBelow66Percent => NPC.life < NPC.lifeMax * 0.66;
        public bool IsBelow50Percent => NPC.life < NPC.lifeMax * 0.5;
        public bool IsBelow33Percent => NPC.life < NPC.lifeMax * 0.33;

        Vector2 movePoint = Vector2.Zero;
        Vector2 DashPoint = Vector2.Zero;

        public enum AnubisAttacks
        {
            DetermineNextAttack = 0,
            ShootRuneblasts = 1,
            ThrowScepter = 2, 
            BlockCrush = 3,
            ThrowAxe = 4,
            SwipeBuildup = 5,
            SwipeExecute = 6,
            ThrowAxe2 = 7,
            ThrowAxe3 = 8,

            Preamble = 39
        }

        public static Asset<Texture2D> Glowmask;
        public static Asset<Texture2D> ThrowAxe;
        public static Asset<Texture2D> ThrowAxeGlowmask;
        public static Asset<Texture2D> SwipeBuildup;
        public static Asset<Texture2D> SwipeBuildupGlowmask;
        public static Asset<Texture2D> SwipeExecute;
        public static Asset<Texture2D> SwipeExecuteGlowmask;
        public static Asset<Texture2D> Prelude;
        public static Asset<Texture2D> PreludeGlowmask;
        public static Asset<Texture2D> ThrowAxeHuge;
        public static Asset<Texture2D> ThrowAxeHugeGlowmask;

        const int THROWAXE_FRAMECOUNT = 5;
        const int SWIPEBUILDUP_FRAMECOUNT = 4;
        const int SWIPEEXECUTE_FRAMECOUNT = 8;
        const int PRELUDE_FRAMECOUNT = 11;

        public Texture2D CurrentTexture;
        public Texture2D CurrentGlowmask;
        public int CurrentTextureFrameCount;

        #region Packet Sending/Recieving
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(HasDonePreamble);
                writer.Write(HasSpawnedLocusts);
                writer.Write(movePoint.X);
                writer.Write(movePoint.Y);
                writer.Write(DashPoint.X);
                writer.Write(DashPoint.Y);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                HasDonePreamble = reader.ReadBoolean();
                HasSpawnedLocusts = reader.ReadBoolean();
                movePoint.X = reader.ReadSingle();
                movePoint.Y = reader.ReadSingle();
                DashPoint.X = reader.ReadSingle();
                DashPoint.Y = reader.ReadSingle();
            }
        }
        #endregion

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Anubis Legendscribe");
            Main.npcFrameCount[NPC.type] = 4;
            this.HideFromBestiary();

            Glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
            ThrowAxe = ModContent.Request<Texture2D>(Texture + "_ThrowAxe");
            ThrowAxeGlowmask = ModContent.Request<Texture2D>(Texture + "_ThrowAxe_Glow");
            SwipeBuildup = ModContent.Request<Texture2D>(Texture + "_SwipeBuildup");
            SwipeBuildupGlowmask = ModContent.Request<Texture2D>(Texture + "_SwipeBuildup_Glow");
            SwipeExecute = ModContent.Request<Texture2D>(Texture + "_SwipeExecute");
            SwipeExecuteGlowmask = ModContent.Request<Texture2D>(Texture + "_SwipeExecute_Glow");
            Prelude = ModContent.Request<Texture2D>(Texture + "_Prelude");
            PreludeGlowmask = ModContent.Request<Texture2D>(Texture + "_Prelude_Glow");
            ThrowAxeHuge = ModContent.Request<Texture2D>(Texture + "_ThrowAxeHuge");
            ThrowAxeHugeGlowmask = ModContent.Request<Texture2D>(Texture + "_ThrowAxeHuge_Glow");
        }

        public override void SetDefaults()
        {
            NPC.width = 76;
            NPC.height = 100;
            NPC.aiStyle = -1;
            NPC.damage = 35;
            NPC.defense = 40;
            NPC.lifeMax = 30000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            Music = MusicManagementSystem.MusicSlots["Anubis"];
            //bossBag/* tModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = ModContent.ItemType<AnubisBag>();
            NPC.value = Item.buyPrice(0, 1, 0, 0);
            NPC.noTileCollide = false;
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.75f * balance);
            NPC.damage = (int)(NPC.damage * 0.85f);
        }

        public int LocustCount = Main.expertMode ? 6 : 4;

        public override void AI()
        {
            NPC.velocity.X *= 0;

            if (!NPC.HasPlayerTarget)
            {
                NPC.TargetClosest();
            }
            Player player = Main.player[NPC.target];

            if (player.Center.X < NPC.Center.X)
            {
                NPC.direction = NPC.spriteDirection = 1;
            }
            else
            {
                NPC.direction = NPC.spriteDirection = -1;
            }

            if (HasDonePreamble == false)
            {
                NPC.noGravity = false;
                AttackTimer++;
                Preamble();
                return;
            }
            NPC.noTileCollide = true;
            NPC.dontTakeDamage = false;
            NPC.noGravity = true;

            if (AttackCurrent == (int)AnubisAttacks.SwipeExecute)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient && NPC.damage != 50)
                {
                    NPC.velocity.Y *= 0f;
                    NPC.damage = 50;
                    NPC.netUpdate = true;
                }
            }
            else
            {
                if (Main.netMode != NetmodeID.MultiplayerClient && NPC.damage != 35)
                {
                    NPC.damage = 35;
                    NPC.netUpdate = true;
                }
            }

            if (IsBelow33Percent && HasSpawnedLocusts == false)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    for (int m = 0; m < LocustCount; m++)
                    {
                        int npcID = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<Locust>(), 0);
                        Main.npc[npcID].Center = NPC.Center;
                        Main.npc[npcID].velocity = new Vector2(MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()), MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()));
                        Main.npc[npcID].velocity *= 8f;
                        Main.npc[npcID].ai[0] = m;
                        Main.npc[npcID].netUpdate2 = true;
                        if (!Main.expertMode)
                        {
                            Main.npc[npcID].ai[2] = 0;
                            if (m == 0 || m == 2)
                            {
                                Main.npc[npcID].ai[2] = 40;
                            }
                        }
                        else
                        {
                            Main.npc[npcID].ai[2] = 0;
                            if (m == 0 || m == 3)
                            {
                                Main.npc[npcID].ai[2] = 40;
                            }
                            else if (m == 2 || m == 4)
                            {
                                Main.npc[npcID].ai[2] = 80;
                            }
                        }
                        int dustType = ModContent.DustType<Dusts.JudgementDust>();
                        int pieCut = 20;
                        for (int i = 0; i < pieCut; i++)
                        {
                            int dustID = Dust.NewDust(Main.npc[npcID].position, Main.npc[npcID].width, Main.npc[npcID].height, dustType, 0f, 0f, 100, Color.White, 1.6f);
                            Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(6f, 0f), i / (float)pieCut * 6.28f);
                            Main.dust[dustID].noLight = false;
                            Main.dust[dustID].noGravity = true;
                        }
                        for (int i = 0; i < pieCut; i++)
                        {
                            int dustID = Dust.NewDust(Main.npc[npcID].position, Main.npc[npcID].width, Main.npc[npcID].height, dustType, 0f, 0f, 100, Color.White, 2f);
                            Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(9f, 0f), i / (float)pieCut * 6.28f);
                            Main.dust[dustID].noLight = false;
                            Main.dust[dustID].noGravity = true;
                        }
                    }
                }
                HasSpawnedLocusts = true;
            }

            Vector2 targetPos = player.Center;
            AttackTimer++;
            switch (AttackCurrent)
            {
                case (int)AnubisAttacks.DetermineNextAttack:
                    if (AttackNext == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        AttackNext = Main.rand.Next(5) + 1;
                        //AttackNext = (int)AnubisAttacks.SwipeBuildup;

                        /*
                        if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) && IsBelow66Percent)
                        {
                            if (AttackNext == (int)AnubisAttacks.ThrowAxe)
                                AttackNext = (int)AnubisAttacks.ThrowAxe2;
                        }
                        if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) && IsBelow33Percent)
                        {
                            if (AttackNext == (int)AnubisAttacks.ThrowAxe2)
                                AttackNext = (int)AnubisAttacks.ThrowAxe3;
                        }
                        */

                        if (AttackNext == (int)AnubisAttacks.SwipeBuildup)
                        {
                            int posX;
                            if (player.position.X > NPC.position.X)
                            {
                                posX = -400;
                            }
                            else
                            {
                                posX = 400;
                            }
                            movePoint = new Vector2(targetPos.X + posX, targetPos.Y);
                        }
                        else
                        {
                            int posX = Main.rand.Next(-400, 400);

                            int posY = Main.rand.Next(0, 400);
                            if (posX > -150 && posX < 150)
                            {
                                posY = Main.rand.Next(150, 400);
                            }
                            movePoint = new Vector2(targetPos.X + posX, targetPos.Y - posY);
                        }

                        if (Main.netMode != NetmodeID.MultiplayerClient && IsBelow66Percent && Main.rand.Next(4 - Repeat()) == 0)
                        {
                            if (Main.rand.Next(2) == 0 && IsBelow33Percent)
                            {
                                int a = Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(NPC.Center.X + 100, NPC.Center.Y), Vector2.Zero, ModContent.ProjectileType<Anubis_EyeSentrySummon>(), 0, 0, -1, NPC.Center.X - 200, NPC.Center.Y);
                                int b = Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(NPC.Center.X - 100, NPC.Center.Y), Vector2.Zero, ModContent.ProjectileType<Anubis_EyeSentrySummon>(), 0, 0, -1, NPC.Center.X + 200, NPC.Center.Y);
                            }
                            else
                            {
                                int m = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X + 100, (int)NPC.position.Y, ModContent.NPCType<MinionRitual>());
                                Main.npc[m].Center = new Vector2(NPC.Center.X + 100, NPC.Center.Y);

                                int n = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X - 100, (int)NPC.position.Y, ModContent.NPCType<MinionRitual>());
                                Main.npc[n].Center = new Vector2(NPC.Center.X - 100, NPC.Center.Y);

                                if (IsBelow50Percent)
                                {
                                    int o = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y + 100, ModContent.NPCType<MinionRitual>());
                                    Main.npc[o].Center = new Vector2(NPC.Center.X, NPC.Center.Y + 100);

                                    int p = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y - 100, ModContent.NPCType<MinionRitual>());
                                    Main.npc[p].Center = new Vector2(NPC.Center.X, NPC.Center.Y - 100);
                                }
                            }
                        }

                        NPC.netUpdate = true;
                    }

                    if (AttackTimer == 40 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<AnubisCircle>(), 0, movePoint.X, movePoint.Y, AttackNext - 1, NPC.whoAmI);
                        NPC.netUpdate = true;
                    }

                    if (AttackTimer >= 80 - 10 * Repeat())
                    {
                        MoveToPoint(movePoint);

                        if (Vector2.Distance(NPC.Center, movePoint) <= 10)
                        {
                            NPC.velocity *= 0;
                            AttackCurrent = AttackNext;
                            AttackTimer = 0;
                            ShotTimer = 0;
                            AttackNext = 0;
                        }
                    }

                    break;
                case (int)AnubisAttacks.ShootRuneblasts:
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        BaseAI.ShootPeriodic(NPC, player.position, player.width, player.height, ModContent.ProjectileType<Anubis_Runeblast>(), ref ShotTimer, 80, 20, 10, true);

                    if (ShotTimer == 79)
                    {
                        int posX = Main.rand.Next(-400, 400);

                        int posY = Main.rand.Next(0, 400);
                        if (posX > -150 && posX < 150)
                        {
                            posY = Main.rand.Next(150, 400);
                        }

                        movePoint = new Vector2(targetPos.X + posX, targetPos.Y - posY);
                    }

                    MoveToPoint(movePoint);

                    if (AttackTimer == 241 + 80 * Repeat())
                    {
                        ResetAI();
                    }
                    break;
                case (int)AnubisAttacks.ThrowScepter:
                    if (Main.netMode != NetmodeID.MultiplayerClient && AttackTimer == 120)
                    {
                        BaseAI.FireProjectile(player.position, NPC.position, ModContent.ProjectileType<Anubis_Scepter>(), 20, 14, 10, -1);
                    }
                    if (AttackTimer == 160)
                    {
                        targetPos.X += 300 * (NPC.Center.X < targetPos.X ? 1 : -1);
                        targetPos.Y -= 300;
                        movePoint = targetPos;
                    }
                    if (AttackTimer >= 160)
                    {
                        MoveToPoint(movePoint);
                    }

                    if (AttackTimer > 160 && !AAGlobalProjectile.AnyProjectiles(ModContent.ProjectileType<Anubis_Scepter>()))
                    {
                        ResetAI();
                    }
                    break;
                case (int)AnubisAttacks.BlockCrush:
                    if (!IsBelow66Percent)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient && AttackTimer == 60)
                        {
                            if (Main.rand.NextBool(2))
                            {
                                int l = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(-800, 0), Vector2.Zero, ModContent.ProjectileType<Anubis_BlockVertical>(), 18, 7, -1, 0, 0);
                                int r = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(800, 0), Vector2.Zero, ModContent.ProjectileType<Anubis_BlockVertical>(), 18, 7, -1, 1, 0);
                                Main.projectile[l].ai[1] = r;
                                Main.projectile[l].Center = player.Center + new Vector2(-800, 0);
                                Main.projectile[r].ai[1] = l;
                                Main.projectile[r].Center = player.Center + new Vector2(800, 0);
                            }
                            else
                            {
                                int u = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(0, -800), Vector2.Zero, ModContent.ProjectileType<Anubis_BlockHorizontal>(), 18, 7, -1, 0, 0);
                                int d = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(0, 800), Vector2.Zero, ModContent.ProjectileType<Anubis_BlockHorizontal>(), 18, 7, -1, 1, 0);
                                Main.projectile[u].ai[1] = d;
                                Main.projectile[u].Center = player.Center + new Vector2(0, -800);
                                Main.projectile[d].ai[1] = u;
                                Main.projectile[d].Center = player.Center + new Vector2(0, 800);
                            }
                        }

                        if (AttackTimer > 120 && !AAGlobalProjectile.AnyProjectiles(ModContent.ProjectileType<Anubis_BlockVertical>()) && !AAGlobalProjectile.AnyProjectiles(ModContent.ProjectileType<Anubis_BlockHorizontal>()))
                        {
                            ResetAI();
                        }
                    }
                    else if (IsBelow66Percent)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            if (AttackTimer == 50)
                            {
                                int l = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(-800, 0), Vector2.Zero, ModContent.ProjectileType<Anubis_BlockVertical>(), 18, 7, -1, 0, 0);
                                int r = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(800, 0), Vector2.Zero, ModContent.ProjectileType<Anubis_BlockVertical>(), 18, 7, -1, 1, 0);
                                Main.projectile[l].ai[1] = r;
                                Main.projectile[l].Center = player.Center + new Vector2(-800, 0);
                                Main.projectile[r].ai[1] = l;
                                Main.projectile[r].Center = player.Center + new Vector2(800, 0);
                            }
                            if (AttackTimer == 100)
                            {
                                int u = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(0, -800), Vector2.Zero, ModContent.ProjectileType<Anubis_BlockHorizontal>(), 18, 7, -1, 0, 0);
                                int d = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(0, 800), Vector2.Zero, ModContent.ProjectileType<Anubis_BlockHorizontal>(), 18, 7, -1, 1, 0);
                                Main.projectile[u].ai[1] = d;
                                Main.projectile[u].Center = player.Center + new Vector2(0, -800);
                                Main.projectile[d].ai[1] = u;
                                Main.projectile[d].Center = player.Center + new Vector2(0, 800);
                            }
                        }
                        if (AttackTimer > 180 && !AAGlobalProjectile.AnyProjectiles(ModContent.ProjectileType<Anubis_BlockVertical>()) && !AAGlobalProjectile.AnyProjectiles(ModContent.ProjectileType<Anubis_BlockHorizontal>()))
                        {
                            ResetAI();
                        }
                    }
                    else if (IsBelow33Percent)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient && AttackTimer % 40 == 0)
                        {
                            if (Main.rand.NextBool(2))
                            {
                                int l = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(-800, 0), Vector2.Zero, ModContent.ProjectileType<Anubis_BlockVertical>(), 18, 7, -1, 0, 0);
                                int r = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(800, 0), Vector2.Zero, ModContent.ProjectileType<Anubis_BlockVertical>(), 18, 7, -1, 1, 0);
                                Main.projectile[l].ai[1] = r;
                                Main.projectile[l].Center = player.Center + new Vector2(-800, 0);
                                Main.projectile[r].ai[1] = l;
                                Main.projectile[r].Center = player.Center + new Vector2(800, 0);
                            }
                            else
                            {
                                int u = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(0, -800), Vector2.Zero, ModContent.ProjectileType<Anubis_BlockHorizontal>(), 18, 7, -1, 0, 0);
                                int d = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(0, 800), Vector2.Zero, ModContent.ProjectileType<Anubis_BlockHorizontal>(), 18, 7, -1, 1, 0);
                                Main.projectile[u].ai[1] = d;
                                Main.projectile[u].Center = player.Center + new Vector2(0, -800);
                                Main.projectile[d].ai[1] = u;
                                Main.projectile[d].Center = player.Center + new Vector2(0, 800);
                            }
                        }

                        if (AttackTimer > 270 && !AAGlobalProjectile.AnyProjectiles(ModContent.ProjectileType<Anubis_BlockVertical>()) && !AAGlobalProjectile.AnyProjectiles(ModContent.ProjectileType<Anubis_BlockHorizontal>()))
                        {
                            ResetAI();
                        }
                    }
                    break;
                case (int)AnubisAttacks.ThrowAxe:
                    if (AttackTimer == 80)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            if(WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                            {
                                Vector2 goal = player.Center - NPC.Center;
                                if (!MathUtils.TryGetLaunchVelocity(goal, 10, 0.2f, out Vector2 velocity))
                                    velocity = new Vector2(Math.Sign(goal.X) == -1 ? -10 : 10, 10);
                                if (Main.netMode != NetmodeID.MultiplayerClient)
                                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, velocity, ModContent.ProjectileType<Axe>(), 18, 0.5f, -1);

                                CurrentTextureFrame = 3;
                                NPC.frameCounter = 0;
                            }
                            else if(Main.netMode != NetmodeID.MultiplayerClient)
                                BaseAI.FireProjectile(player.position, NPC.position, ModContent.ProjectileType<Axe>(), 20, 14, 10, -1);
                        }
                    }
                    if (AttackTimer == 86)
                    {
                        ResetAI();
                    }

                    break;
                case (int)AnubisAttacks.SwipeBuildup:
                    if (AttackTimer == 30 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int posX;
                        if (player.position.X > NPC.position.X)
                        {
                            posX = 400;
                        }
                        else
                        {
                            posX = -400;
                        }
                        DashPoint = new Vector2(targetPos.X + posX, targetPos.Y);

                        AttackCurrent = 6;
                        AttackTimer = 0;
                        NPC.netUpdate = true;
                    }
                    break;
                case (int)AnubisAttacks.SwipeExecute:
                    if (AttackTimer < 36)
                        MoveToPoint(DashPoint);
                    else
                        NPC.velocity = Vector2.Zero;

                    if (AttackTimer >= 54)
                        ResetAI();

                    break;
                case (int)AnubisAttacks.ThrowAxe2:
                    CurrentFrameRate = 4;
                    if (AttackTimer >= ThrownAxe2_TimeBetweenAxes * 5)
                        CurrentFrameRate--;
                    if (AttackTimer >= ThrownAxe2_TimeBetweenAxes * 10)
                        CurrentFrameRate--;

                    if (!ThrowAxe2_HasDoneWindup)
                    {
                        if (AttackTimer > THROWAXE2_WINDUPSTART)
                        {
                            AttackTimer = 0;
                            ThrowAxe2_HasDoneWindup = true;
                        }
                        break;
                    }

                    ShotTimer--;

                    if (ShotTimer <= 0)
                    {
                        ShotTimer = ThrownAxe2_TimeBetweenAxes;
                        
                        Vector2 goal = player.Center - NPC.Center;
                        int randomBorderMin = 200;
                        int randomBorderMax = 600;
                        int randomCenterMin = -90;
                        int randomCenterMax = 60;

                        float direction = ThrowAxe2_AmountOfAxesThrown % 3;
                        if (direction == 0)
                            goal.X += Main.rand.NextFloat(randomBorderMin, randomBorderMax);
                        else if (direction == 1)
                            goal.X += Main.rand.NextFloat(-randomBorderMax, -randomBorderMin);
                        else
                            goal.X += Main.rand.NextFloat(randomCenterMin, randomCenterMax);

                        if (!MathUtils.TryGetLaunchVelocity(goal, 10, 0.2f, out Vector2 velocity))
                            velocity = NPC.DirectionTo(targetPos) * 100;
                        
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, velocity, ModContent.ProjectileType<Axe>(), 18, 0.5f, -1);

                        ThrowAxe2_AmountOfAxesThrown++;
                    }

                    if (ThrowAxe2_AmountOfAxesThrown >= THROWAXE2_AMOUNTOFAXES && ShotTimer >= ThrownAxe2_TimeBetweenAxes)
                        ResetAI();
                    break;
                case (int)AnubisAttacks.ThrowAxe3:
                    CurrentFrameRate = 4;
                    if (AttackTimer >= ThrownAxe2_TimeBetweenAxes * 3)
                        CurrentFrameRate--;
                    if (AttackTimer >= ThrownAxe2_TimeBetweenAxes * 7)
                        CurrentFrameRate--;

                    if (!ThrowAxe2_HasDoneWindup && !ThrowAxe3_HasThrownSmallAxes)
                    {
                        if (AttackTimer > THROWAXE2_WINDUPSTART)
                        {
                            AttackTimer = 0;
                            ThrowAxe2_HasDoneWindup = true;
                        }
                        break;
                    }

                    ShotTimer--;

                    if (ShotTimer <= 0 && !ThrowAxe3_HasThrownSmallAxes)
                    {
                        ShotTimer = ThrownAxe2_TimeBetweenAxes;

                        Vector2 goal = player.Center - NPC.Center;
                        int randomBorderMin = 200;
                        int randomBorderMax = 600;
                        int randomCenterMin = -90;
                        int randomCenterMax = 60;

                        float direction = ThrowAxe2_AmountOfAxesThrown % 3;
                        if (direction == 0)
                            goal.X += Main.rand.NextFloat(randomBorderMin, randomBorderMax);
                        else if (direction == 1)
                            goal.X += Main.rand.NextFloat(-randomBorderMax, -randomBorderMin);
                        else
                            goal.X += Main.rand.NextFloat(randomCenterMin, randomCenterMax);

                        if (!MathUtils.TryGetLaunchVelocity(goal, 10, 0.2f, out Vector2 velocity))
                            velocity = NPC.DirectionTo(targetPos) * 100;

                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, velocity, ModContent.ProjectileType<Axe>(), 18, 0.5f, -1);

                        ThrowAxe2_AmountOfAxesThrown++;
                    }

                    if (ThrowAxe2_AmountOfAxesThrown >= THROWAXE3_AMOUNTOFAXES && ShotTimer >= ThrownAxe2_TimeBetweenAxes && !ThrowAxe3_HasThrownSmallAxes)
                    {
                        AttackTimer = 0;
                        ThrowAxe3_HasThrownSmallAxes = true;
                        ThrowAxe2_HasDoneWindup = false;
                    }

                    if (ThrowAxe3_HasThrownSmallAxes && !ThrowAxe2_HasDoneWindup && !ThrowAxe3_HasThrownHugeAxe)
                    {
                        CurrentFrameRate = THROWAXE3_WINDUPFRAMERATE;
                        if (AttackTimer > THROWAXE3_WINDUPSUPERAXE)
                        {
                            ThrowAxe2_HasDoneWindup = true;
                        }
                    }
                    else if (ThrowAxe3_HasThrownSmallAxes && ThrowAxe2_HasDoneWindup && !ThrowAxe3_HasThrownHugeAxe)
                    {
                        Vector2 goal = player.Center - NPC.Center;

                        float vyi = 12;
                        float vyisq = 100;
                        float g = 0.2f;

                        float vyf = -MathF.Sqrt(vyisq + 2 * g * goal.Y);
                        float t = Math.Abs((vyf - vyi) / g);

                        float vxi = goal.X / t;

                        Vector2 velocity = new Vector2(float.IsNaN(vxi) ? goal.X > 0 ? 10 : -10 : vxi, -vyi);
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, velocity, ModContent.ProjectileType<HugeAxe>(), 18, 0.5f, -1);

                        ThrowAxe3_HasThrownHugeAxe = true;
                        AttackTimer = 0;
                    }
                    else if (ThrowAxe3_HasThrownHugeAxe && !ThrowAxe3_HasDoneAxeThrowSmearFrames)
                    {
                        CurrentFrameRate = FRAMECHANGERATE_ORIGINAL;
                        if (AttackTimer > THROWAXE3_AXETHROWSMEARFRAMES)
                        {
                            AttackTimer = 0;
                            ThrowAxe3_HasDoneAxeThrowSmearFrames = true;
                        }
                    }
                    else if (ThrowAxe3_HasDoneAxeThrowSmearFrames)
                    {
                        //AttackTimer = 0;
                        //ThrowAxe3_HasThrownHugeAxe = false;
                        //ThrowAxe3_HasDoneAxeThrowSmearFrames = false;
                        ResetAI();
                    }

                        break;
                default:
                    AttackCurrent = 1;
                    goto case 1;
            }

            for (int m = NPC.oldPos.Length - 1; m > 0; m--)
            {
                NPC.oldPos[m] = NPC.oldPos[m - 1];
            }
            NPC.oldPos[0] = NPC.position;
        }
        public int THROWAXE2_AMOUNTOFFRAMESUSED = 3;
        public int THROWAXE2_AMOUNTOFAXES = 18;
        public int THROWAXE2_WINDUPSTART = FRAMECHANGERATE_ORIGINAL * 2;
        public int ThrownAxe2_TimeBetweenAxes => CurrentFrameRate * THROWAXE2_AMOUNTOFFRAMESUSED;
        public int ThrowAxe2_AmountOfAxesThrown = 0;
        public bool ThrowAxe2_HasDoneWindup = false;

        public int THROWAXE3_AMOUNTOFAXES = 15;
        public int THROWAXE3_WINDUPFRAMERATE = 12;
        public int THROWAXE3_WINDUPSUPERAXE => THROWAXE3_WINDUPFRAMERATE * 4;
        public int THROWAXE3_AXETHROWSMEARFRAMES => FRAMECHANGERATE_ORIGINAL * 2;
        //TODO: replace with an int that ticks up determining attack substate 
        public bool ThrowAxe3_HasThrownSmallAxes = false;
        public bool ThrowAxe3_HasThrownHugeAxe = false;
        public bool ThrowAxe3_HasDoneAxeThrowSmearFrames = false;
        public int? ThrowAxe3_HugeAxeProjID = null;

        public void ResetAI()
        {
            NPC.velocity = Vector2.Zero;
            AttackCurrent = 0;
            AttackTimer = 0;
            ShotTimer = 0;
            AttackNext = 0;

            ThrowAxe2_AmountOfAxesThrown = 0;
            ThrowAxe2_HasDoneWindup = false;

            ThrowAxe3_HasThrownSmallAxes = false;
            ThrowAxe3_HasThrownHugeAxe = false;
            ThrowAxe3_HasDoneAxeThrowSmearFrames = false;
        }

        public int Repeat()
        {
            if (IsBelow33Percent)
            {
                return 2;
            }
            if (IsBelow66Percent)
            {
                return 1;
            }
            return 0;
        }

        public void MoveToPoint(Vector2 point)
        {
            float Speed = 13;

            float velMultiplier = 1f;
            Vector2 dist = point - NPC.Center;
            float length = dist == Vector2.Zero ? 0f : dist.Length();
            if (length < Speed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / Speed);
            }
            if (length < 200f)
            {
                Speed *= 0.5f;
            }
            if (length < 100f)
            {
                Speed *= 0.5f;
            }
            if (length < 50f)
            {
                Speed *= 0.5f;
            }
            NPC.velocity = length == 0f ? Vector2.Zero : Vector2.Normalize(dist);
            NPC.velocity *= Speed;
            NPC.velocity *= velMultiplier;
        }

        int deathtimer = 0;

        public bool AliveCheck(Player player)
        {
            if (!player.active || player.dead || Vector2.Distance(NPC.Center, player.Center) > 5000f || !player.ZoneDesert)
            {
                NPC.TargetClosest();
                if (!player.active || player.dead || Vector2.Distance(NPC.Center, player.Center) > 5000f || !player.ZoneDesert)
                {
                    deathtimer++;
                    if (Main.netMode != NetmodeID.MultiplayerClient && deathtimer > 240)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Anubis.Kill"), Color.Gold);
                            int a = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<Legendscribe>());
                            Main.npc[a].Center = NPC.Center;
                        }
                        NPC.active = false;
                    }
                    return false;
                }
                else
                {
                    deathtimer = 0;
                }
            }
            deathtimer = 0;
            return true;
        }

        public override void BossLoot(ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }

        public override bool PreKill()
        {
            if (NPC.downedMoonlord && NPCExtensions.BeenKilled<Anubis>())
            {
                if (!AAWorld.AnubisAwakened)
                    AAWorld.AnubisAwakened = true;

                NPC.boss = false;
            }
            return true;
        }

        public override void OnKill()
        {
            Main.BestiaryTracker.Kills.RegisterKill(ContentSamples.NpcsByNetId[ModContent.NPCType<Anubis>()]);

            bool beenKilled = NPCExtensions.BeenKilled<Anubis>(true);
            if(!beenKilled)
                NPC.OnGameEventClearedForTheFirstTime(-1);

            if (NPC.downedMoonlord && beenKilled)
                NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<AnubisForsakenTransition>());
            else
                NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<Legendscribe>());
        }

        //TODO: make this pull from reg anubis' lootpool
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<AnubisTreasureBag>()));

            npcLoot.AddLoreItemDrop<Anubis>(ModContent.ItemType<AnubisLore>());

            LeadingConditionRule masterMode = new(new AAConditions.RevOrMaster());

            masterMode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<AnubisRelic>()));

            npcLoot.Add(masterMode);

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AnubisTrophy>(), 10));

            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<AnubisMask>(), 7));

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ForsakenFragment>(), 1, 8, 16));

            int[] lootTable = { ModContent.ItemType<Judgment>(), ModContent.ItemType<NeithsString>(), ModContent.ItemType<DesertStaff>(), ModContent.ItemType<JackalsWrath>(), ModContent.ItemType<Sandthrower>(), ModContent.ItemType<SentryOfTheEye>() };

            notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, lootTable));

            npcLoot.Add(notExpertRule);
        }

        int CurrentTextureFrame = 0;
        const int FRAMECHANGERATE_ORIGINAL = 6;
        int CurrentFrameRate = FRAMECHANGERATE_ORIGINAL;

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter > CurrentFrameRate)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;
                CurrentTextureFrame++; 
            }

            if (AttackTimer == 0)
            {
                NPC.frameCounter = 0;
                CurrentTextureFrame = 0;
            }

            if (HasDonePreamble == false)
            {
                CurrentTexture = Prelude.Value;
                CurrentGlowmask = PreludeGlowmask.Value;
                CurrentTextureFrameCount = PRELUDE_FRAMECOUNT;
                
                if (AttackTimer >= 240 && AttackTimer < 320)
                {
                    if (CurrentTextureFrame < 9)
                    {
                        CurrentTextureFrame = 9;
                    }
                    if (CurrentTextureFrame >= 10)
                    {
                        CurrentTextureFrame = 10;
                    }
                }
                else
                {
                    if (NPC.velocity.Y == 0)
                    {
                        if (CurrentTextureFrame < 4 || CurrentTextureFrame > 8)
                        {
                            CurrentTextureFrame = 4;
                        }
                    }
                    else
                    {
                        if (NPC.frame.Y > 3)
                        {
                            CurrentTextureFrame = 0;
                        }
                    }
                }
            }
            else
            {
                if (AttackCurrent == (int)AnubisAttacks.ThrowAxe)
                {
                    CurrentTexture = ThrowAxe.Value;
                    CurrentGlowmask = ThrowAxeGlowmask.Value;
                    CurrentTextureFrameCount = THROWAXE_FRAMECOUNT;

                    if (AttackTimer < 80 && CurrentTextureFrame > 3)
                        CurrentTextureFrame = 3;
                    else if (AttackTimer == 80)
                        CurrentTextureFrame = 4;
                }
                else if (AttackCurrent == (int)AnubisAttacks.SwipeBuildup)
                {
                    CurrentTexture = SwipeBuildup.Value;
                    CurrentGlowmask = SwipeBuildupGlowmask.Value;
                    CurrentTextureFrameCount = SWIPEBUILDUP_FRAMECOUNT;

                    if (CurrentTextureFrame > 3)
                    {
                        CurrentTextureFrame = 3;
                    }
                }
                else if (AttackCurrent == (int)AnubisAttacks.SwipeExecute)
                {
                    CurrentTexture = SwipeExecute.Value;
                    CurrentGlowmask = SwipeExecuteGlowmask.Value;
                    CurrentTextureFrameCount = SWIPEEXECUTE_FRAMECOUNT;

                    if (CurrentTextureFrame > 7)
                    {
                        CurrentTextureFrame = 7;
                    }
                }
                else if (AttackCurrent == (int)AnubisAttacks.ThrowAxe2 || AttackCurrent == (int)AnubisAttacks.ThrowAxe3)
                {
                    CurrentTexture = ThrowAxe.Value;
                    CurrentGlowmask = ThrowAxeGlowmask.Value;
                    CurrentTextureFrameCount = THROWAXE_FRAMECOUNT;

                    if (!ThrowAxe2_HasDoneWindup && !ThrowAxe3_HasThrownSmallAxes)
                    {
                        if (AttackTimer > 4)
                            CurrentTextureFrame = 1;
                        else
                            CurrentTextureFrame = 0;
                    }
                    else if (!ThrowAxe3_HasThrownSmallAxes)
                    {
                        float frameProgress = ((float)ShotTimer / ThrownAxe2_TimeBetweenAxes);
                        if (frameProgress < 0.33f)
                            CurrentTextureFrame = 0;
                        else if (frameProgress < 0.66f)
                            CurrentTextureFrame = 1;
                        else
                            CurrentTextureFrame = 4;
                    }
                    else if (ThrowAxe3_HasThrownSmallAxes && !ThrowAxe2_HasDoneWindup)
                    {
                        CurrentTexture = ThrowAxeHuge.Value;
                        CurrentGlowmask = ThrowAxeHugeGlowmask.Value;
                        CurrentTextureFrameCount = THROWAXE_FRAMECOUNT;

                        if (AttackTimer > THROWAXE3_WINDUPFRAMERATE * 3)
                            CurrentTextureFrame = 3;
                        else if (AttackTimer > THROWAXE3_WINDUPFRAMERATE * 2)
                            CurrentTextureFrame = 2;
                        else if (AttackTimer > THROWAXE3_WINDUPFRAMERATE)
                            CurrentTextureFrame = 1;
                        else
                            CurrentTextureFrame = 0;
                    }
                    else if (!ThrowAxe3_HasDoneAxeThrowSmearFrames)
                    {
                        //TODO: it needs two frames bcuz its huge
                        if (AttackTimer > FRAMECHANGERATE_ORIGINAL)
                            CurrentTextureFrame = 0;
                        else
                            CurrentTextureFrame = 4;
                    }
                    else
                    {
                        CurrentTexture = TextureAssets.Npc[NPC.type].Value;
                        CurrentGlowmask = Glowmask.Value;
                        CurrentTextureFrameCount = Main.npcFrameCount[NPC.type];
                        CurrentTextureFrame = 0;
                    }
                }
                else
                {
                    CurrentTexture = TextureAssets.Npc[NPC.type].Value;
                    CurrentGlowmask = Glowmask.Value;
                    CurrentTextureFrameCount = Main.npcFrameCount[NPC.type];
                    CurrentFrameRate = FRAMECHANGERATE_ORIGINAL;

                    if (NPC.frame.Y > frameHeight * 3)
                    {
                        NPC.frame.Y = 0;
                    }

                    CurrentTextureFrame = NPC.frame.Y / frameHeight;
                }
            }
        }

        public void Teleport()
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
                int num88 = Dust.NewDust(position, num84, height3, DustID.GoldCoin, 0f, 0f, 50, default, 3.7f);
                Main.dust[num88].position = NPC.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f;
                Main.dust[num88].noGravity = true;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity *= 3f;
                Main.dust[num88].velocity += NPC.DirectionTo(Main.dust[num88].position) * (2f + Main.rand.NextFloat() * 4f);
                num88 = Dust.NewDust(position, num84, height3, DustID.GoldCoin, 0f, 0f, 25, default, 1.5f);
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
                int num90 = Dust.NewDust(position, num84, height3, DustID.GoldCoin, 0f, 0f, 0, default, 2.7f);
                Main.dust[num90].position = NPC.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(NPC.velocity.ToRotation(), default) * num84 / 2f;
                Main.dust[num90].noGravity = true;
                Main.dust[num90].noLight = true;
                Main.dust[num90].velocity *= 3f;
                Main.dust[num90].velocity += NPC.DirectionTo(Main.dust[num90].position) * 2f;
            }
            for (int num91 = 0; num91 < 30; num91++)
            {
                int num92 = Dust.NewDust(position, num84, height3, DustID.GoldCoin, 0f, 0f, 0, default, 1.5f);
                Main.dust[num92].position = NPC.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(NPC.velocity.ToRotation(), default) * num84 / 2f;
                Main.dust[num92].noGravity = true;
                Main.dust[num92].velocity *= 3f;
                Main.dust[num92].velocity += NPC.DirectionTo(Main.dust[num92].position) * 3f;
            }

            Player player = Main.player[NPC.target];
            Vector2 targetPos = player.Center;
            int posX = Main.rand.Next(-400, 400);

            int posY = Main.rand.Next(0, 400);
            if (posX > -150 && posX < 150)
            {
                 posY = Main.rand.Next(150, 400);
            }

            NPC.position = new Vector2(targetPos.X + posX, targetPos.Y - posY);
            NPC.netOffset = Vector2.Zero;
            int pieCut = 20;
            SoundEngine.PlaySound(SoundID.Item14, NPC.position);
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(NPC.Center.X - 1, NPC.Center.Y - 1), 2, 2, ModContent.DustType<Dusts.JudgementDust>(), 0f, 0f, 100, Color.White, 1.6f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(6f, 0f), m / (float)pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(NPC.Center.X - 1, NPC.Center.Y - 1), 2, 2, ModContent.DustType<Dusts.JudgementDust>(), 0f, 0f, 100, Color.White, 2f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(9f, 0f), m / (float)pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
        }

        public void ScepterTeleport()
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
                int num88 = Dust.NewDust(position, num84, height3, DustID.GoldCoin, 0f, 0f, 50, default, 3.7f);
                Main.dust[num88].position = NPC.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f;
                Main.dust[num88].noGravity = true;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity *= 3f;
                Main.dust[num88].velocity += NPC.DirectionTo(Main.dust[num88].position) * (2f + Main.rand.NextFloat() * 4f);
                num88 = Dust.NewDust(position, num84, height3, DustID.GoldCoin, 0f, 0f, 25, default, 1.5f);
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
                int num90 = Dust.NewDust(position, num84, height3, DustID.GoldCoin, 0f, 0f, 0, default, 2.7f);
                Main.dust[num90].position = NPC.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(NPC.velocity.ToRotation(), default) * num84 / 2f;
                Main.dust[num90].noGravity = true;
                Main.dust[num90].noLight = true;
                Main.dust[num90].velocity *= 3f;
                Main.dust[num90].velocity += NPC.DirectionTo(Main.dust[num90].position) * 2f;
            }
            for (int num91 = 0; num91 < 30; num91++)
            {
                int num92 = Dust.NewDust(position, num84, height3, DustID.GoldCoin, 0f, 0f, 0, default, 1.5f);
                Main.dust[num92].position = NPC.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(NPC.velocity.ToRotation(), default) * num84 / 2f;
                Main.dust[num92].noGravity = true;
                Main.dust[num92].velocity *= 3f;
                Main.dust[num92].velocity += NPC.DirectionTo(Main.dust[num92].position) * 3f;
            }

            Vector2 targetPos = Main.player[NPC.target].Center;
            targetPos.X += 300 * (NPC.Center.X < targetPos.X ? 1 : -1);
            targetPos.Y -= 300;
            NPC.position = targetPos;
            NPC.netOffset = Vector2.Zero;

            int pieCut = 20;
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(NPC.Center.X - 1, NPC.Center.Y - 1), 2, 2, ModContent.DustType<Dusts.JudgementDust>(), 0f, 0f, 100, Color.White, 1f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(6f, 0f), m / (float)pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(NPC.Center.X - 1, NPC.Center.Y - 1), 2, 2, ModContent.DustType<Dusts.JudgementDust>(), 0f, 0f, 100, Color.White, 1.5f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(9f, 0f), m / (float)pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
        }

        public void Preamble()
        {
            NPC.dontTakeDamage = true;

            AttackNext = (int)AnubisAttacks.Preamble;
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Music = MusicLoader.GetMusicSlot("AAModClassic/Music/silence");
                if (NPC.velocity.Y == 0)
                {
                    if (AttackTimer++ < 420)
                    {
                        if (!NPCExtensions.BeenKilled<Anubis>())
                        {
                            if (AttackTimer == 60)
                            {
                                int activePlayers = 0;
                                foreach (Player p in Main.ActivePlayers)
                                    activePlayers++;
                                string s = activePlayers > 1 ? "Multiplayer" : "Singleplayer";
                                if (Main.netMode != NetmodeID.MultiplayerClient)
                                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Anubis.Intro.1." + s), Color.Gold);
                            }

                            if (AttackTimer == 150)
                            {
                                if (Main.netMode != NetmodeID.MultiplayerClient) 
                                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Anubis.Intro.2"), Color.Gold);
                            }

                            if (AttackTimer == 240)
                            {
                                if (Main.netMode != NetmodeID.MultiplayerClient) 
                                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Anubis.Intro.3"), Color.Gold);
                            }

                            if (AttackTimer == 320)
                            {
                                if (Main.netMode != NetmodeID.MultiplayerClient) 
                                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Anubis.Intro.4"), Color.Gold);
                            }

                            if (AttackTimer >= 410)
                            {
                                Music = MusicLoader.GetMusicSlot("AAModClassic/Music/Anubis");
                                if (Main.netMode != NetmodeID.MultiplayerClient) 
                                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Anubis.Intro.5"), Color.Gold);
                                HasDonePreamble = true;
                                AttackNext = 0;
                                NPC.GetGlobalNPC<TitleGlobalNPC>().ShowTitle = true;
                                Teleport();
                                NPC.netUpdate = true;
                            }
                        }
                        else
                        {
                            Music = MusicLoader.GetMusicSlot("AAModClassic/Music/Anubis");
                            if (Main.netMode != NetmodeID.MultiplayerClient) 
                                BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Anubis.Intro.Rematch"), Color.Gold);
                            HasDonePreamble = true;
                            AttackNext = 0;
                            NPC.GetGlobalNPC<TitleGlobalNPC>().ShowTitle = true;
                            Teleport();
                            NPC.netUpdate = true;
                        }
                    }
                }
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(150, 255, 150) * (Main.mouseTextColor / 255f);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (CurrentTexture == null)
                return false;
            Rectangle frame = BaseDrawing.GetFrame(CurrentTextureFrame, CurrentTexture.Width, CurrentTexture.Height / CurrentTextureFrameCount, 0, 0);

            double hoverOffset = 0;
            if (AttackCurrent == (int)AnubisAttacks.SwipeExecute)
            {
                

            }
            hoverOffset = Math.Sin((5 / 3) * Main.GlobalTimeWrappedHourly) * 5;
            Vector2 position = NPC.position;
            position.Y += (float)hoverOffset;
            //Main.NewText(hoverOffset);
            //Main.NewText(NPC.velocity.Y);

            if (HasDonePreamble == true && NPC.velocity != Vector2.Zero)
                DrawingUtils.DrawAfterimageWithVelocity(spriteBatch, CurrentTexture, NPC.Center - Main.screenPosition, NPC.velocity, 8, frame, new Color(150, 255, 150) * (Main.mouseTextColor / 255f), NPC.scale, [NPC.rotation], frame.Size() * 0.5f, NPC.SpriteEffectDirection());
            BaseDrawing.DrawTexture(spriteBatch, CurrentTexture, 0, position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, CurrentTextureFrameCount, frame, drawColor, true);
            BaseDrawing.DrawTexture(spriteBatch, CurrentGlowmask, 0, position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, CurrentTextureFrameCount, frame, AAColor.COLOR_WHITEFADE1, true);

            return false;
        }
    }
}