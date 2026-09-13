using AAModClassic._Content._Misc._PostMoonlord.Items.Consumables;
using AAModClassic._Content.Chaos._PostMoonlord.Items._BossShenDoragon.BossStandard;
using AAModClassic._Content.Chaos._PostMoonlord.Items._BossShenDoragon.Tools;
using AAModClassic._Content.Chaos._PostMoonlord.Items._BossShenDoragon.Weapons;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.Awakened;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.GripsOfDiscord;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.SistersOfAnarchy.FuryAshe;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.SistersOfAnarchy.WrathHaruka;
using AAModClassic._Content.Inferno.World.Biomes;
using AAModClassic._Content.Inferno.World.Tiles;
using AAModClassic._Content.Mire.World.Biomes;
using AAModClassic._Content.Terrarium.Buffs;
using AAModClassic._CrossMod.CalamityMod.LoreItems;
using AAModClassic.Achievements;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Music;
using AAModClassic.UI.Core.BestiaryBackgrounds;
using AAModClassic.UI.Titles;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.Renderers;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

using static AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.ShenDoragonUtils;

namespace AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon
{
    [AutoloadBossHead]
    public class ShenDoragon : ModNPC
    {
        public bool IsAwakened => NPC.type == ModContent.NPCType<ShenDoragonA>();

        public bool SpawnMinionPhaseCharacters = false;

        public int damage = 50; //Complete guess

        private static Asset<Texture2D> Glowmask;
        private static Asset<Texture2D> EyeGlowmask;
        private static Asset<Texture2D> Body;
        private static Asset<Texture2D> HeadClosed;
        private static Asset<Texture2D> HeadOpenTop;
        private static Asset<Texture2D> HeadOpenBottom;
        private static Asset<Texture2D> WingFront;
        private static Asset<Texture2D> WingBack;
        private static Asset<Texture2D> UpperArmsFront;
        private static Asset<Texture2D> LowerArmsFront;
        private static Asset<Texture2D> UpperArmsBack;
        private static Asset<Texture2D> LowerArmsBack;

        private static ParticlePool<RandomizedFrameParticle> telegraphParticles;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Shen Doragon; Discordian Doomsayer");
            Main.npcFrameCount[NPC.type] = 2;

            NPCID.Sets.TrailingMode[Type] = 3;
            NPCID.Sets.TrailCacheLength[Type] = 12;

            Glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
            EyeGlowmask = ModContent.Request<Texture2D>(Texture + "_EyeGlow");
            HeadClosed = ModContent.Request<Texture2D>(Texture + "_HeadClosed");
            HeadOpenTop = ModContent.Request<Texture2D>(Texture + "_HeadOpen_Top");
            HeadOpenBottom = ModContent.Request<Texture2D>(Texture + "_HeadOpen_Bottom");
            Body = ModContent.Request<Texture2D>(Texture + "_Body");
            WingFront = ModContent.Request<Texture2D>(Texture + "_WingsFront");
            WingBack = ModContent.Request<Texture2D>(Texture + "_WingsBack");
            UpperArmsFront = ModContent.Request<Texture2D>(Texture + "_ArmsFront_Upper");
            LowerArmsFront = ModContent.Request<Texture2D>(Texture + "_ArmsFront_Lower");
            UpperArmsBack = ModContent.Request<Texture2D>(Texture + "_ArmsBack_Upper");
            LowerArmsBack = ModContent.Request<Texture2D>(Texture + "_ArmsBack_Lower");
            telegraphParticles = new(100, () => new RandomizedFrameParticle());

            NPCID.Sets.NPCBestiaryDrawModifiers value = new()
            {
                //PortraitPositionYOverride = 64f
                Velocity = 0
            };
            value.Position.X += 170;
            value.Position.Y += 24;
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
        }

        public override void SetDefaults()
        {
            NPC.noTileCollide = true;
            NPC.height = 100;
            NPC.width = 444;
            NPC.aiStyle = -1;
            NPC.netAlways = true;
            NPC.knockBackResist = 0f;
            NPC.damage = 120;
            NPC.defense = 70;
            NPC.lifeMax = 800000;
            NPC.value = Item.buyPrice(20, 0, 0, 0);
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.aiStyle = -1;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = new SoundStyle("AAModClassic/Sounds/ShenRoar");
            Music = MusicManagementSystem.MusicSlots["Shen"];
            SceneEffectPriority = (SceneEffectPriority)11;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
            NPC.buffImmune[ModContent.BuffType<Terrablaze_Buff>()] = false;
            SpawnModBiomes = new int[2] { ModContent.GetInstance<InfernoBiome>().Type, ModContent.GetInstance<MireBiome>().Type };
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.AddTags([new ShenDoragonBestiaryBackground()]);
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.5f * balance);
            NPC.damage = (int)(NPC.damage * .8f);
            if (IsAwakened)
                NPC.defense = (int)(NPC.defense * 1.2f);
        }

        public bool Weakness = false;
        public const float _normalSpeed = 15f;
        public const float _chargeSpeed = 40f;

        public Vector2 AttackPosition = Vector2.Zero;

        public float MoveSpeed
        {
            get
            {
                float playerRunAcceleration = 1f;
                if (Main.player[NPC.target].active && !Main.player[NPC.target].dead) //if you have a target, speed up to keep up
                {
                    playerRunAcceleration = Math.Max(Math.Abs(Main.player[NPC.target].moveSpeed), Main.player[NPC.target].runAcceleration);
                    if (playerRunAcceleration <= 1f) playerRunAcceleration = 1f;
                }
                if (Dashing)
                {
                    return _chargeSpeed * playerRunAcceleration;
                }
                else
                {
                    return _normalSpeed * playerRunAcceleration;
                }
            }
        }

        //clientside stuff
        public Rectangle wingFrameFront = new(0, 0, 444, 400);
        public Rectangle wingFrameBack = new(0, 0, 444, 400);
        public const int FRAMECOUNT_X = 3;
        public const int FRAMECOUNT_Y = 2;
        public int roarTimer = 0; //if this is > 0, then use the roaring frame.
        public int roarTimerMax = 120; //default roar timer. only changed for fire breath as it's longer.
        public bool Roaring => roarTimer > 0; //wether or not he is roaring. only used clientside for frame visuals.

        public int chargeWidth = 50;
        public int normalWidth = 444;

        public override void BossLoot(ref int potionType)
        {
            if (Main.expertMode && !IsAwakened)
            {
                potionType = 0;
                return;
            }
            potionType = ModContent.ItemType<GrandHealingPotion>();
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public void Roar(int timer, bool fireSound)
        {
            roarTimer = timer;
            if (fireSound)
            {
                SoundEngine.PlaySound(SoundID.NPCDeath60, NPC.Center);
            }
            else
            {
                SoundEngine.PlaySound(new SoundStyle("AAModClassic/Sounds/ShenRoar"), NPC.Center);
            }
        }

        public int Side;
        public bool Health4 = false;
        public bool Health3 = false;
        public bool Health2 = false;
        public bool Health1 = false;

        public float[] FleeTimer = new float[1];

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(FleeTimer[0]);
            writer.Write(AttackPosition.X);
            writer.Write(AttackPosition.Y);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            FleeTimer[0] = reader.ReadSingle();
            float attackX = reader.ReadSingle();
            float attackY = reader.ReadSingle();
            AttackPosition = new(attackX, attackY);
        }

        public override void AI()
        {
            NPC.GetGlobalNPC<TitleGlobalNPC>().ShowTitle = true;

            NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];
            Vector2 targetPos;

            Main.dayTime = false;
            Main.time = 18000;

            if (!AliveCheck(player)) 
                return;

            Dashing = false;
            if (Roaring) 
                roarTimer--;

            if (Dashing)
            {
                if (NPC.width != chargeWidth)
                {
                    Vector2 center = NPC.Center;
                    NPC.width = chargeWidth;
                    NPC.Center = center;
                    NPC.netOffset = Vector2.Zero;
                    NPC.netUpdate = true;
                }
            }
            else if (NPC.width != normalWidth)
            {
                Vector2 center = NPC.Center;
                NPC.width = normalWidth;
                NPC.Center = center;
                NPC.netOffset = Vector2.Zero;
                NPC.netUpdate = true;
            }

            if (!NPC.AnyNPCs(ModContent.NPCType<ShenDoragon_Hitbox>()))
            {
                int hitbox = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<ShenDoragon_Hitbox>(), 0, NPC.whoAmI, 0f, 0f, 0f, 255);
                Main.npc[hitbox].netUpdate = true;
            }

            if (NPC.AnyNPCs(ModContent.NPCType<BlazeGrip>()) || NPC.AnyNPCs(ModContent.NPCType<AbyssGrip>()) || NPC.AnyNPCs(ModContent.NPCType<FuryAshe>()) || NPC.AnyNPCs(ModContent.NPCType<WrathHaruka>()))
            {
                if (NPC.alpha > 50)
                {
                    NPC.alpha = 50;
                }
                else
                {
                    NPC.alpha += 4;
                }
                NPC.dontTakeDamage = true;
            }
            else
            {
                if (NPC.alpha > 0)
                {
                    for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                    {
                        int dust = spawnDust == 1 ? ModContent.DustType<Dusts.AkumaADust>() : ModContent.DustType<Dusts.YamataADust>();
                        if (Main.rand.NextBool(4)) 
                            dust = ModContent.DustType<Dusts.Discord_Dust>();
                        if (IsAwakened)
                            dust = ModContent.DustType<Dusts.DiscordLight>();
                        int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust, 0f, 0f, 100, default, 2f);
                        Main.dust[num935].noGravity = true;
                        Main.dust[num935].noLight = true;
                    }
                    NPC.alpha -= 4;
                }
                if (NPC.alpha < 0)
                {
                    NPC.alpha = 0;
                }
                NPC.dontTakeDamage = false;
            }

            if (player.dead || !player.active || Vector2.Distance(NPC.Center, player.Center) > 10000)
            {
                NPC.TargetClosest();

                if (player.dead || !player.active || Vector2.Distance(NPC.Center, player.Center) > 10000)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient && FleeTimer[0]++ >= 120)
                    {
                        if (FleeTimer[0] < 130)
                        {
                            NPC.velocity.Y += 1f;
                            NPC.netUpdate = true;
                        }
                        else if (FleeTimer[0] == 130)
                        {
                            NPC.velocity.Y = -6f;
                            NPC.netUpdate = true;
                        }
                        else if (FleeTimer[0] > 130)
                            NPC.velocity.Y = -6f;

                        if (NPC.position.Y + NPC.velocity.Y <= 0f && Main.netMode != NetmodeID.MultiplayerClient)
                            BaseAI.KillNPC(NPC); NPC.netUpdate = true;
                    }
                }
                else
                {
                    FleeTimer[0] = 0;
                }
            }

            switch ((int)NPC.ai[0])
            {
                case 0: //target for first time, navigate beside player (spawn deathray if awakened)
                    if (!NPC.HasPlayerTarget)
                        NPC.TargetClosest();
                    if (!AliveCheck(Main.player[NPC.target]))
                        break;
                    targetPos = player.Center;
                    targetPos.X += 600 * (NPC.Center.X < targetPos.X ? -1 : 1);
                    Movement(targetPos, 1f);
                    if (++NPC.ai[2] > 240)
                    {
                        Roar(roarTimerMax, false);
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        NPC.ai[3] = NPC.Center.X < player.Center.X ? 0 : (float)Math.PI;
                        NPC.netUpdate = true;
                        NPC.velocity.X = 2 * (NPC.Center.X < player.Center.X ? -1 : 1);
                        NPC.velocity.Y *= 0.2f;
                        if (IsAwakened && Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.UnitX.RotatedBy(NPC.ai[3]), ModContent.ProjectileType<ShenDoragonA_Deathray>(), 40, 0f, -1, 0, NPC.whoAmI);
                    }
                    else if(!Main.dedServ && WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) && NPC.ai[2] > 180 && NPC.ai[2] % 3 == 0)
                    {
                        RandomizedFrameParticle lightning = telegraphParticles.RequestParticle();
                        Main.instance.LoadProjectile(ProjectileID.ScytheWhipProj);
                        lightning.SetBasicInfo(TextureAssets.Projectile[ProjectileID.ScytheWhipProj], null, Vector2.Zero, Main.rand.NextVector2Circular(8f, 8f));
                        lightning.SetTypeInfo(Main.projFrames[ProjectileID.ScytheWhipProj], 2, 24f); 
                        lightning.Velocity = (NPC.spriteDirection == -1 ? MathHelper.Pi : 0 + Main.rand.NextFloat(-MathHelper.PiOver4, MathHelper.PiOver4)).ToRotationVector2() * Main.rand.NextFloat(2, 4);
                        lightning.ColorTint = AAColor.Shen3;
                        lightning.LocalPosition = NPC.Center + new Vector2(190 * NPC.spriteDirection, 0);
                        lightning.Rotation = lightning.Velocity.ToRotation();
                        lightning.Velocity += NPC.velocity / 2f;
                        lightning.Scale = new Vector2(Main.rand.NextFloat(1f, 3f), 1f) * Main.rand.NextFloat(0.25f, 1f);
                        lightning.FadeInNormalizedTime = 0.01f;
                        lightning.FadeOutNormalizedTime = 0.5f;
                        lightning.ScaleVelocity = new Vector2(0.025f);
                        Main.ParticleSystem_World_BehindPlayers.Add(lightning);
                    }
                    if (++NPC.ai[1] > 60)
                    {
                        NPC.ai[1] = 0;
                        Roar(roarTimerMax, false);
                        NPC.netUpdate = true;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            for (int i = -2; i <= 2; i++)
                                NPC.NewProjectileFlipped<ShenDoragon_ChaosFireballSpread>(NPC.GetSource_FromThis(), NPC.Center, 30 * Vector2.UnitX.RotatedBy(Math.PI / 4 * i) * (NPC.Center.X < player.Center.X ? -1 : 1), 30, 0f, -1, 20, 20 + 60);
                    }
                    break;

                case 1: //Fire Breath (do nothing if awakened)
                    if (!IsAwakened && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Vector2 to = player.Center;
                        NPC.ai[2]--;
                        if (NPC.ai[2] <= 0)
                        {
                            bool unofficial = WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial);
                            Vector2 from = NPC.Center + new Vector2(132 * NPC.spriteDirection, 12);
                            from -= (to - from).SafeNormalize(Vector2.UnitX * NPC.spriteDirection) * 36;
                            BaseAI.FireProjectile(to, from, ModContent.ProjectileType<ShenDoragonA_FireBreath>(), damage, 0f, 13);

                            NPC.ai[2] = unofficial ? 3 : 5;
                            NPC.netUpdate = true;
                        }
                    }
                    //BaseAI.ShootPeriodic(NPC, player.position, player.width, player.height, ModContent.ProjectileType<ShenABreath>(), ref NPC.ai[2], 5, NPC.damage / 2, 13, false, new Vector2(167, 0));
                    if (++NPC.ai[1] > 120)
                    {
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.ai[3] = 0;
                        NPC.netUpdate = true;
                    }
                    break;

                case 2: //fly to corner for dash
                    if (!AliveCheck(player))
                        break;
                    targetPos = player.Center;
                    targetPos.X += 800 * (NPC.Center.X < targetPos.X ? -1 : 1);
                    targetPos.Y -= 800;
                    Movement(targetPos, 1.2f);
                    if (++NPC.ai[1] > 180 || Math.Abs(NPC.Center.Y - targetPos.Y) < 100) //initiate dash
                    {
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.netUpdate = true;
                        NPC.velocity = NPC.DirectionTo(player.Center) * 45;
                    }
                    NPC.rotation = 0;
                    break;

                case 3: //dashing
                    if (NPC.Center.Y > player.Center.Y + 700 || Math.Abs(NPC.Center.X - player.Center.X) > 1500)
                    {
                        NPC.velocity.Y *= 0.5f;
                        NPC.ai[1] = 0;
                        if (++NPC.ai[2] >= 3) //repeat three times
                        {
                            NPC.ai[0]++;
                            NPC.ai[2] = 0;
                        }
                        else
                            NPC.ai[0]--;
                        NPC.netUpdate = true;
                    }
                    Dashing = true;
                    NPC.rotation = NPC.velocity.ToRotation();
                    if (NPC.velocity.X < 0)
                        NPC.rotation += (float)Math.PI;
                    break;

                case 4: //prepare for queen bee dashes
                    if (!AliveCheck(player))
                        break;
                    if (++NPC.ai[1] > 30)
                    {
                        targetPos = player.Center;
                        targetPos.X += 1000 * (NPC.Center.X < targetPos.X ? -1 : 1);
                        Movement(targetPos, 0.8f);
                        if (NPC.ai[1] > 180 || Math.Abs(NPC.Center.Y - targetPos.Y) < 50) //initiate dash
                        {
                            NPC.ai[0]++;
                            NPC.ai[1] = 0;
                            NPC.netUpdate = true;
                            NPC.velocity.X = -40 * (NPC.Center.X < player.Center.X ? -1 : 1);
                            NPC.velocity.Y *= 0.1f;
                        }
                    }
                    else
                    {
                        NPC.velocity *= 0.9f; //decelerate briefly
                    }
                    NPC.rotation = 0;
                    break;

                case 5: //dashing (leave trail of vertical deathrays if awakened)
                    if (NPC.ai[3] == 0 && --NPC.ai[2] < 0)
                    {
                        NPC.ai[2] = 4;
                        //TODO: Idk what the deal is with the roars but i think this is how they *should* work (unofficial)
                        if (IsAwakened)
                        {
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.UnitY, ModContent.ProjectileType<ShenDoragonA_DeathrayVertical>(), 30, 0f, -1, 0f, NPC.whoAmI);
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -Vector2.UnitY, ModContent.ProjectileType<ShenDoragonA_DeathrayVertical>(), 30, 0f, -1, 0f, NPC.whoAmI);
                            }
                            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                                Roar(roarTimerMax, false);
                        }
                        else if(!WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                            Roar(roarTimerMax, false);
                    }
                    if (++NPC.ai[1] > 240 || (Math.Sign(NPC.velocity.X) > 0 ? NPC.Center.X > player.Center.X + 900 : NPC.Center.X < player.Center.X - 900))
                    {
                        if (IsAwakened)
                            Roar(roarTimerMax, false);
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        if (++NPC.ai[3] >= 3) //repeat dash three times
                        {
                            NPC.ai[0]++;
                            NPC.ai[3] = 0;
                        }
                        else
                            NPC.ai[0]--;
                        NPC.netUpdate = true;
                    }
                    Dashing = true;
                    break;

                case 6: //fly at player, spit mega balls
                    if (!AliveCheck(player))
                        break;
                    targetPos = player.Center;
                    targetPos.X += 700 * (NPC.Center.X < targetPos.X ? -1 : 1);
                    float speedModifier = IsAwakened ? 0.5f : 0.8f;
                    Movement(targetPos, speedModifier);
                    if (++NPC.ai[2] > 80)
                    {
                        NPC.ai[2] = 0;
                        Roar(roarTimerMax, false);
                        NPC.netUpdate = true;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Vector2 spawnPos = NPC.Center;
                            spawnPos.X += 250 * (NPC.Center.X < player.Center.X ? 1 : -1);
                            if (!IsAwakened)
                                spawnPos.Y -= 25;
                            Vector2 vel = (player.Center - spawnPos) / 30;
                            if (vel.Length() < 25)
                                vel = Vector2.Normalize(vel) * 25;
                            NPC.NewProjectileFlipped<ShenDoragon_ChaosFireballFrag>(NPC.GetSource_FromThis(), spawnPos, vel, 30, 0f, -1);
                        }
                    }
                    if (++NPC.ai[1] > 210)
                    {
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        NPC.netUpdate = true;
                    }
                    break;

                case 7: 
                    goto case 2;

                case 8: 
                    goto case 3;

                case 9: //prepare for fishron dash
                    if (!AliveCheck(player))
                        break;
                    targetPos = player.Center + player.DirectionTo(NPC.Center) * 600;
                    Movement(targetPos, 0.8f);
                    if (++NPC.ai[1] > 20)
                    {
                        Roar(roarTimerMax, false);
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.netUpdate = true;
                        NPC.velocity = NPC.DirectionTo(player.Center) * 40;
                    }
                    NPC.rotation = 0;
                    break;

                case 10: //dashing
                    if (++NPC.ai[2] > 3)
                    {
                        NPC.ai[2] = 0;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            const float ai0 = 0.01f;
                            NPC.NewProjectileFlipped<ShenDoragon_ChaosFireballAccel>(NPC.GetSource_FromThis(), NPC.Center, Vector2.Normalize(NPC.velocity).RotatedBy(Math.PI / 2), 30, 0f, -1, ai0);
                            NPC.NewProjectileFlipped<ShenDoragon_ChaosFireballAccel>(NPC.GetSource_FromThis(), NPC.Center, Vector2.Normalize(NPC.velocity).RotatedBy(-Math.PI / 2), 30, 0f, -1, ai0);
                        }
                    }
                    int amtOfDahes = IsAwakened ? 5 : 3;
                    if (++NPC.ai[1] > 40)
                    {
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        if (++NPC.ai[3] >= amtOfDahes) //dash three times
                        {
                            NPC.ai[0]++;
                            NPC.ai[3] = 0;
                        }
                        else
                            NPC.ai[0]--;
                        NPC.netUpdate = true;
                    }
                    Dashing = true;
                    NPC.rotation = NPC.velocity.ToRotation();
                    if (NPC.velocity.X < 0)
                        NPC.rotation += (float)Math.PI;
                    break;

                case 11: //fly up, prepare to spit mega homing and dash
                    if (!AliveCheck(player))
                        break;
                    targetPos = player.Center;
                    targetPos.X += 600 * (NPC.Center.X < targetPos.X ? -1 : 1);
                    targetPos.Y -= 600;
                    Movement(targetPos, 0.8f);
                    if (++NPC.ai[1] > 180 || NPC.Distance(targetPos) < 50)
                    {
                        Roar(roarTimerMax, false);
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.netUpdate = true;
                        NPC.velocity.X = -30 * (NPC.Center.X < player.Center.X ? -1 : 1);
                        NPC.velocity.Y = 5f;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            NPC.NewProjectileFlipped<ShenDoragon_ChaosFireballHoming>(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, 40, 0f, -1, NPC.target, 8f);
                    }
                    NPC.rotation = 0;
                    break;

                case 12: //dashing
                    Dashing = true;
                    NPC.velocity *= 0.98f;
                    if (++NPC.ai[1] > 30)
                    {
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.netUpdate = true;
                    }
                    break;

                case 13: //hover nearby, shoot fireballs
                    if (!AliveCheck(player))
                        break;
                    targetPos = player.Center;
                    targetPos.X += 700 * (NPC.Center.X < targetPos.X ? -1 : 1);
                    Movement(targetPos, 0.7f);
                    if (++NPC.ai[2] > 60)
                    {
                        Roar(roarTimerMax, false);
                        NPC.ai[2] = 0;
                        if (Main.netMode != NetmodeID.MultiplayerClient) //spawn lightning
                        {
                            float verticalOffset = IsAwakened ? 45f : 65f;
                            Vector2 infernoPos = new Vector2(200f, NPC.direction == 1 ? verticalOffset : -verticalOffset);
                            Vector2 vel = new Vector2(MathHelper.Lerp(6f, 8f, (float)Main.rand.NextDouble()), MathHelper.Lerp(-4f, 4f, (float)Main.rand.NextDouble()));

                            if (player.active && !player.dead)
                            {
                                float rot = BaseUtility.RotationTo(NPC.Center, player.Center);
                                infernoPos = BaseUtility.RotateVector(Vector2.Zero, infernoPos, rot);
                                vel = BaseUtility.RotateVector(Vector2.Zero, vel, rot);
                                vel *= MoveSpeed / _normalSpeed; //to compensate for players running away
                                int dir = NPC.Center.X < player.Center.X ? 1 : -1;
                                if (dir == -1 && NPC.velocity.X < 0 || dir == 1 && NPC.velocity.X > 0) vel.X += NPC.velocity.X;
                                vel.Y += NPC.velocity.Y;
                                infernoPos += NPC.Center;
                                infernoPos.Y -= 70;
                            }
                            if (IsAwakened)
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), (int)infernoPos.X, (int)infernoPos.Y + 16, vel.X * 2, vel.Y * 2, ModContent.ProjectileType<ShenDoragonA_ChaosLightning>(), 30, 0f, -1, vel.ToRotation(), 0f);
                            else
                            {
                                //REMEMBER: PROJECTILES DOUBLE DAMAGE so to get an accurate damage count you divide it by 2!
                                float InfernoType;
                                if (NPC.spriteDirection == -1)
                                    InfernoType = 1;
                                else
                                    InfernoType = 2;

                                int projectile = Projectile.NewProjectile(NPC.GetSource_FromThis(), infernoPos, vel, ModContent.ProjectileType<ShenDoragon_DiscordianInferno>(), damage, 0f, -1, InfernoType, 0f);
                                Main.projectile[projectile].velocity = vel;
                                Main.projectile[projectile].netUpdate = true;
                            }
                        }
                    }
                    if (++NPC.ai[1] > 360)
                    {
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.ai[2] = NPC.Center.X > player.Center.X ? 1 : -1;
                        NPC.ai[3] = 720;// MathHelper.Clamp(NPC.Distance(player.Center), 1f, 400f);
                        AttackPosition = player.Center;
                        NPC.netUpdate = true;
                        NPC.velocity = NPC.DirectionTo(player.Center).RotatedBy(Math.PI / 2) * 40;
                    }
                    break;

                case 14: //fly in jumbo circle
                    float rotAngle = MathHelper.Pi / 48f * NPC.ai[1];
                    float radius = 720;
                    NPC.Center = AttackPosition + Vector2.UnitX.RotatedBy(rotAngle) * NPC.ai[2] * radius;
                    NPC.velocity = Vector2.Zero;
                    NPC.netOffset = Vector2.Zero;
                    //NPC.velocity -= NPC.velocity.RotatedBy(MathHelper.Pi / 2f) * NPC.velocity.Length() / NPC.ai[3];
                    //NPC.velocity = NPC.velocity.ClampMagnitude(0f, 16f);
                    rotAngle += MathHelper.PiOver2 * NPC.ai[2];

                    if (NPC.ai[1] % (IsAwakened ? 2 : 6) == 0)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            const float ai0 = 0.004f;
                            NPC.NewProjectileFlipped<ShenDoragon_ChaosFireballAccel>(NPC.GetSource_FromThis(), NPC.Center, rotAngle.ToRotationVector2().RotatedBy(Math.PI / 2), 30, 0f, -1, ai0);
                            NPC.NewProjectileFlipped<ShenDoragon_ChaosFireballAccel>(NPC.GetSource_FromThis(), NPC.Center, rotAngle.ToRotationVector2().RotatedBy(-Math.PI / 2), 30, 0f, -1, ai0);
                        }
                    }
                    if (NPC.ai[1] <= 1)
                    {
                        Roar(roarTimerMax, false);
                    }
                    if (++NPC.ai[1] > 150)
                    {
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        NPC.ai[3] = 0;
                    }
                    NPC.rotation = rotAngle;
                    if (NPC.spriteDirection == -1)
                        NPC.rotation += MathHelper.Pi;
                    Dashing = true;
                    break;

                case 15: //wait for old attack to go away
                    if (!AliveCheck(player))
                        break;
                    targetPos = player.Center;
                    targetPos.X += 600 * (NPC.Center.X < targetPos.X ? -1 : 1);
                    Movement(targetPos, 1f);
                    if (++NPC.ai[2] > 120)
                    {
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        NPC.ai[3] = 0;
                        NPC.netUpdate = true;
                    }
                    NPC.rotation = 0;
                    break;

                default:
                    NPC.ai[0] = 0;
                    goto case 0;
            }
        }

        private bool AliveCheck(Player player)
        {
            if (!player.active || player.dead || Vector2.Distance(NPC.Center, player.Center) > 5000f)
            {
                NPC.TargetClosest();
                if (!player.active || player.dead || Vector2.Distance(NPC.Center, player.Center) > 5000f)
                {
                    if (NPC.timeLeft > 60)
                        NPC.timeLeft = 60;
                    BaseAI.KillNPC(NPC);
                    NPC.netUpdate2 = true;
                    return false;
                }
            }
            if (NPC.timeLeft < 600)
                NPC.timeLeft = 600;
            return true;
        }

        private void Movement(Vector2 targetPos, float speedModifier)
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

        bool Dashing = false;

        public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
        {
            modifiers.TargetDamageMultiplier *= .8f;
        }

        public bool Health9 = false;
        public bool Health8 = false;
        public bool Health7 = false;
        public bool Health6 = false;
        public bool Health5 = false;
        public bool HealthOneHalf = false;

        public override void HitEffect(NPC.HitInfo hit)
        {
            Player player = Main.player[NPC.target];

            if (!IsAwakened)
            {
                if (NPC.life <= NPC.lifeMax / 2 && !SpawnMinionPhaseCharacters)
                {
                    SpawnMinionPhaseCharacters = true;
                    if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Summon"), Color.DarkMagenta);
                    AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<AbyssGrip>(), false, 0, 0);
                    AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<BlazeGrip>(), false, 0, 0);
                    SoundEngine.PlaySound(SoundID.Roar, player.position);
                }

                if (NPC.life <= NPC.lifeMax * 0.80f && !Health4)
                {
                    if (AAWorld.downedShen)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Health.80.Repeat"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    }
                    else
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Health.80.First"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    }
                    Health4 = true;
                    NPC.netUpdate = true;
                }
                if (NPC.life <= NPC.lifeMax * 0.66f && !Health3)
                {
                    if (AAWorld.downedShen)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Health.66.Repeat"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    }
                    else
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Health.66.First"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    }
                    Health3 = true;
                    NPC.netUpdate = true;
                }
                if (NPC.life <= NPC.lifeMax * 0.30f && !Health1)
                {
                    if (AAWorld.downedShen)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Health.30.Repeat"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    }
                    else
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Health.30.First"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    }
                    Health1 = true;
                    NPC.netUpdate = true;
                }
            }
            else
            {
                if (NPC.life <= NPC.lifeMax * .4f && !SpawnMinionPhaseCharacters)
                {
                    SpawnMinionPhaseCharacters = true;

                    if (AAWorld.downedShen)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Awakened.Summon.Repeat.Shen"), Color.DarkMagenta);
                        if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Awakened.Summon.Repeat.Ashe"), new Color(102, 20, 48));
                        if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Awakened.Summon.Repeat.Haruka"), new Color(72, 78, 117));
                    }
                    else
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Awakened.Summon.First.Shen"), Color.DarkMagenta);
                        if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Awakened.Summon.First.Ashe"), new Color(102, 20, 48));
                        if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Awakened.Summon.First.Haruka"), new Color(72, 78, 117));
                    }

                    AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<FuryAshe>(), false, 0, 0);
                    AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<WrathHaruka>(), false, 0, 0);
                }

                if (NPC.life <= NPC.lifeMax * 0.9f && !Health9)
                {
                    if (AAWorld.downedShen)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Awakened.Health.90.Repeat"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    }
                    else
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Awakened.Health.90.First"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    }
                    Health9 = true;
                    NPC.netUpdate = true;
                }
                if (NPC.life <= NPC.lifeMax * 0.8f && !Health8)
                {
                    if (AAWorld.downedShen)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Awakened.Health.80.Repeat"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    }
                    else
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Awakened.Health.80.First"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    }
                    Health8 = true;
                    NPC.netUpdate = true;
                }
                if (NPC.life <= NPC.lifeMax * 0.7f && !Health7)
                {
                    if (AAWorld.downedShen)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Awakened.Health.70.Repeat"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    }
                    else
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Awakened.Health.70.First"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    }
                    Health7 = true;
                    NPC.netUpdate = true;
                }
                if (NPC.life <= NPC.lifeMax * 0.6f && !Health6)
                {
                    if (AAWorld.downedShen)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Awakened.Health.60.Repeat"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    }
                    else
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Awakened.Health.60.First"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    }
                    Health6 = true;
                    NPC.netUpdate = true;
                }
                if (NPC.life <= NPC.lifeMax * 0.5f && !Health5)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(GetCrossModDialogue(), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    Health5 = true;
                    NPC.netUpdate = true;
                }
                if (NPC.life <= NPC.lifeMax * 0.3f && !Health3)
                {
                    if (AAWorld.downedShen)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Awakened.Health.30.Repeat"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    }
                    else
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Awakened.Health.30.First"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    }
                    Health3 = true;
                    NPC.netUpdate = true;
                }
                if (NPC.life <= NPC.lifeMax * 0.2f && !Health2)
                {
                    if (AAWorld.downedShen)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Awakened.Health.20.Repeat"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    }
                    else
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Awakened.Health.20.First"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    }
                    Health2 = true;
                    NPC.netUpdate = true;
                }
                if (NPC.life <= NPC.lifeMax * 0.1f && !Health1)
                {
                    if (AAWorld.downedShen)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Awakened.Health.10.Repeat"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    }
                    else
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Awakened.Health.10.First"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    }
                    Health1 = true;
                    NPC.netUpdate = true;
                }
                if (Health2)
                {
                    if (!AAConfigClient.Instance.DisablePinchThemes)
                        Music = MusicManagementSystem.MusicSlots["Superancients_Pinch"];
                }
            }
        }

        public override bool PreKill()
        {
            if (Main.expertMode && !IsAwakened)
                NPC.boss = false;
            return true;
        }

        public override void OnKill()
        {
            if (!IsAwakened)
            {
                if (!Main.expertMode)
                {
                    if (!NPC.BeenKilled(true))
                    {
                        NPC.OnGameEventClearedForTheFirstTime(-1);

                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Defeat.NotExpert.First"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                        if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                        {
                            TileProtectionSystem.UnprotectTiles(ModContent.TileType<ScorchedDynastyWood_Tile>(), ModContent.TileType<ScorchedPlatform_Tile>(), ModContent.TileType<ScorchedShingles_Tile>());
                            TileProtectionSystem.UnprotectWalls(ModContent.WallType<ScorchedDynastyWoodWall_Wall>());
                        }
                    }
                    else
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Defeat.NotExpert.Repeat"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    }

                    if (NPC.playerInteraction[Main.myPlayer])
                        ShenDoragonKilled.Condition.Complete();
                }
                else
                {
                    NPC.NewNPC(NPC.GetSource_Death(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<ShenDoragonTransition>());
                }
            }
            else 
            {
                if (Main.expertMode)
                {
                    if (!NPC.AnyNPCs(ModContent.NPCType<ShenDoragonDefeat>()))
                    {
                        MusicUtils.InstantSwitchMusic(MusicManagementSystem.MusicSlots["Shen_Outro"]);
                        NPC.NewNPC(NPC.GetSource_Death(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<ShenDoragonDefeat>());
                    }

                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                    {
                        TileProtectionSystem.UnprotectTiles(ModContent.TileType<ScorchedDynastyWood_Tile>(), ModContent.TileType<ScorchedPlatform_Tile>(), ModContent.TileType<ScorchedShingles_Tile>());
                        TileProtectionSystem.UnprotectWalls(ModContent.WallType<ScorchedDynastyWoodWall_Wall>());
                    }

                    if (NPC.playerInteraction[Main.myPlayer])
                        ShenDoragonKilled.Condition.Complete();

                    if (!NPC.BeenKilled(true))
                        NPC.OnGameEventClearedForTheFirstTime(-1);
                }
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ShenDoragonTrophy>(), 10));

            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ShenDoragonMask>(), 7));

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ChaosScale>(), 1, 20, 30));

            int[] lootTable = { ModContent.ItemType<ChaosSlayer>(), ModContent.ItemType<MeteorStrike>(), ModContent.ItemType<Skyfall>(), ModContent.ItemType<Asteroid>(), ModContent.ItemType<DraconicRipper>(), ModContent.ItemType<FlamingTwilight>(), ModContent.ItemType<DiscordianTerratool>(), ModContent.ItemType<Timesplitter>() };
            notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, lootTable));

            LeadingConditionRule loreCondition = new(new LoreItemDropCondition<ShenDoragon>());
            notExpertRule.OnSuccess(loreCondition.OnSuccess(new PerPlayerDropRule(ModContent.ItemType<ShenDoragonLore>(), 1)));

            npcLoot.Add(notExpertRule);
        }

        public override void FindFrame(int frameHeight)
        {
            Player player = Main.player[NPC.target];
            int frameWidth = TextureAssets.Npc[NPC.type].Width() / FRAMECOUNT_X;

            if(WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                NPC.frame = new Rectangle(0, 0, frameWidth, Body.Height());
            else
                NPC.frame = new Rectangle(0, Roaring ? frameHeight : 0, frameWidth, frameHeight);

            if (Dashing)
            {
                NPC.frameCounter = 0;
                wingFrameFront.Y = frameHeight;
            }
            else
            {
                NPC.frameCounter++;
                if (NPC.frameCounter >= 5)
                {
                    NPC.frameCounter = 0;
                    wingFrameFront.Y += frameHeight;
                    if (wingFrameFront.Y > frameHeight * 4)
                    {
                        NPC.frameCounter = 0;
                        wingFrameFront.Y = 0;
                    }
                }
                if (!IsAwakened || NPC.ai[0] != 1)
                {
                    NPC.spriteDirection = NPC.Center.X < player.Center.X ? 1 : -1;
                }
            }

            wingFrameBack = wingFrameFront;

            if (IsAwakened)
            {
                NPC.frame.X = frameWidth * 2;
                wingFrameFront.X = frameWidth * 2;
                wingFrameBack.X = frameWidth * 2;
            }
            else if (NPC.spriteDirection == 1)
            {
                NPC.frame.X = frameWidth;
                wingFrameFront.X = frameWidth;
                wingFrameBack.X = 0;
            }
            else
            {
                NPC.frame.X = 0;
                wingFrameFront.X = 0;
                wingFrameBack.X = frameWidth;
            }

        }

        private float smoothedLateralSpeed = 0f;

        private Vector2 bendFrontPos, bendBodyAxis, bendPerp;
        private float bendMaxOffset, bendBodyLength;

        private void UpdateBodyBend()
        {
            bendBodyLength = NPC.frame.Width * NPC.scale;
            bendBodyAxis = new Vector2(NPC.spriteDirection, 0).RotatedBy(NPC.rotation);
            bendFrontPos = NPC.Center + bendBodyAxis * (bendBodyLength / 2f);
            bendPerp = new Vector2(-bendBodyAxis.Y, bendBodyAxis.X);

            float rawLateral = -Vector2.Dot(NPC.velocity, bendPerp);
            const float SMOOTHING = 0.15f;
            smoothedLateralSpeed = MathHelper.Lerp(smoothedLateralSpeed, rawLateral, SMOOTHING);

            const float BEND_COEFF = 0.05f;
            bendMaxOffset = MathHelper.Clamp(smoothedLateralSpeed * BEND_COEFF * bendBodyLength,
                                             -bendBodyLength * 0.5f, bendBodyLength * 0.5f);
        }

        private (Vector2 pos, Vector2 forward) GetBodyPoint(float t)
        {
            Vector2 straightPos = bendFrontPos - bendBodyAxis * t * bendBodyLength;
            float offset = bendMaxOffset * t * t;
            Vector2 pos = straightPos + bendPerp * offset;

            Vector2 tangent = -bendBodyAxis * bendBodyLength + bendPerp * (2f * bendMaxOffset * t);
            if (tangent.LengthSquared() > 0.001f)
                tangent.Normalize();
            else
                tangent = -bendBodyAxis;
            Vector2 forward = -tangent;

            return (pos, forward);
        }

        private const int NumStripPoints = 12;
        private readonly Vector2[] _stripPointsCache = new Vector2[NumStripPoints];
        private readonly VertexPositionColorTexture[] _stripVertsCache = new VertexPositionColorTexture[NumStripPoints * 2];

        private void FillStripPoints()
        {
            for (int i = 0; i < NumStripPoints; i++)
            {
                float t = (float)i / (NumStripPoints - 1);
                (_stripPointsCache[i], _) = GetBodyPoint(t);
            }
        }

        private VertexPositionColorTexture[] BuildStrip(Vector2 screenPos, Color color)
        {
            FillStripPoints();
            Vector2[] points = _stripPointsCache;

            float frameUMin = (float)NPC.frame.X / Body.Value.Width;
            float frameUMax = (float)(NPC.frame.X + NPC.frame.Width) / Body.Value.Width;
            float halfHeight = Body.Value.Height * NPC.scale * 0.5f;
            Vector2 referenceUp = new(MathF.Sin(NPC.rotation), -MathF.Cos(NPC.rotation));

            for (int i = 0; i < NumStripPoints; i++)
            {
                float u = MathHelper.Lerp(frameUMin, frameUMax, (float)i / (NumStripPoints - 1));

                Vector2 tangent;
                if (i == 0)
                    tangent = (points[0] - points[1]).SafeNormalize(Vector2.UnitX);
                else if (i == NumStripPoints - 1)
                    tangent = (points[i - 1] - points[i]).SafeNormalize(Vector2.UnitX);
                else
                    tangent = (points[i - 1] - points[i + 1]).SafeNormalize(Vector2.UnitX);

                Vector2 perp = new Vector2(-tangent.Y, tangent.X);
                if (Vector2.Dot(perp, referenceUp) < 0)
                    perp = -perp;
                perp *= halfHeight;

                Vector2 world = points[i] - screenPos;
                _stripVertsCache[i * 2 + 0] = new VertexPositionColorTexture(new Vector3(world - perp, 0f), color, new Vector2(u, 1f));
                _stripVertsCache[i * 2 + 1] = new VertexPositionColorTexture(new Vector3(world + perp, 0f), color, new Vector2(u, 0f));
            }

            return _stripVertsCache;
        }

        private static BasicEffect _bodyEffect;

        private static BasicEffect GetBodyEffect(GraphicsDevice gd)
        {
            if (_bodyEffect == null || _bodyEffect.IsDisposed)
            {
                _bodyEffect = new BasicEffect(gd)
                {
                    TextureEnabled = true,
                    VertexColorEnabled = true
                };
            }
            return _bodyEffect;
        }

        public override void Unload()
        {
            try
            {
                if (_bodyEffect != null && !_bodyEffect.IsDisposed)
                    _bodyEffect.Dispose();
            }
            catch { }
            finally
            {
                _bodyEffect = null;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            bool unofficial = WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial);
            bool hasHistory = unofficial && NPC.oldPos[0] != Vector2.Zero;

            Vector2 sharedWingPos = Vector2.Zero;
            float sharedWingRot = 0f;
            bool wingCached = false;

            // back wing
            Vector2 backWingPos = NPC.Center - screenPos;
            float backWingRot = NPC.rotation;
            if (hasHistory && !NPC.IsABestiaryIconDummy)
            {
                UpdateBodyBend();
                (Vector2 wingPos, Vector2 wingForward) = GetBodyPoint(0.5f);
                sharedWingPos = wingPos;
                sharedWingRot = wingForward.ToRotation();
                if (NPC.spriteDirection == -1)
                    sharedWingRot += MathHelper.Pi;
                wingCached = true;

                backWingPos = sharedWingPos - screenPos;
                backWingRot = sharedWingRot;
            }
            spriteBatch.Draw(WingBack.Value, backWingPos, wingFrameBack, NPC.GetAlpha(drawColor), backWingRot, wingFrameBack.Size() / 2, NPC.scale, NPC.SpriteEffectDirection(true), 0);

            // back arm
            if (unofficial)
            {
                int backArmFrameX = IsAwakened ? 2 : (NPC.spriteDirection == 1 ? 0 : 1);
                Rectangle upperBackArmFrame = UpperArmsBack.Frame(3, frameX: backArmFrameX);
                float tBackArm = 0.5f - 84f / NPC.frame.Width;

                Vector2 upperBackArmPos;
                float bodyFacingAngle;
                if (hasHistory && !NPC.IsABestiaryIconDummy)
                {
                    (Vector2 attachPos, Vector2 forward) = GetBodyPoint(tBackArm);
                    upperBackArmPos = attachPos - screenPos + Vector2.UnitY * -10;
                    bodyFacingAngle = forward.ToRotation();
                    if (NPC.spriteDirection == -1)
                        bodyFacingAngle += MathHelper.Pi;
                }
                else
                {
                    upperBackArmPos = NPC.Center + (new Vector2(84 * NPC.spriteDirection, -8).RotatedBy(NPC.rotation) * NPC.scale) - screenPos;
                    bodyFacingAngle = NPC.rotation;
                }
                Vector2 upperBackArmOrigin = new Vector2(12, 8);
                if (NPC.spriteDirection == 1)
                    upperBackArmOrigin.X = upperBackArmFrame.Width - upperBackArmOrigin.X;

                float upperBackArmRotation;
                if (Dashing)
                    upperBackArmRotation = (MathHelper.Pi / 4f + MathF.Sin(Main.GlobalTimeWrappedHourly * 8f + MathHelper.Pi) * MathHelper.Pi / 16f) * -NPC.spriteDirection;
                else
                    upperBackArmRotation = (MathHelper.Pi / 3f + MathF.Sin(Main.GlobalTimeWrappedHourly * 3f) * MathHelper.Pi / 8f) * -NPC.spriteDirection;
                float upperBackArmRotationOffset = -0.85f * -NPC.spriteDirection;

                float upperWorldRot = bodyFacingAngle + upperBackArmRotation + upperBackArmRotationOffset;

                spriteBatch.Draw(UpperArmsBack.Value, upperBackArmPos, upperBackArmFrame, NPC.GetAlpha(drawColor), upperWorldRot, upperBackArmOrigin, NPC.scale, NPC.SpriteEffectDirection(true), 0);

                // Lower back arm
                Rectangle lowerBackArmFrame = LowerArmsBack.Frame(3, frameX: backArmFrameX);
                Vector2 lowerBackArmOrigin = new Vector2(42, 0);
                if (NPC.spriteDirection == 1)
                    lowerBackArmOrigin.X = lowerBackArmFrame.Width - lowerBackArmOrigin.X;

                Vector2 elbowLocal = (upperBackArmRotation.ToRotationVector2() * -24f * NPC.spriteDirection) * NPC.scale;
                Vector2 elbowOffset = elbowLocal.RotatedBy(bodyFacingAngle);
                Vector2 lowerBackArmPos = upperBackArmPos + elbowOffset;

                float lowerBackArmRotation;
                if (Dashing)
                    lowerBackArmRotation = (-MathHelper.Pi / 16f + MathF.Sin(Main.GlobalTimeWrappedHourly * 8f + MathHelper.PiOver2) * MathHelper.Pi / 8f) * -NPC.spriteDirection;
                else
                    lowerBackArmRotation = (MathHelper.PiOver2 + MathF.Sin(Main.GlobalTimeWrappedHourly * 3f + MathHelper.PiOver2) * MathHelper.Pi / 4f) * -NPC.spriteDirection;
                float lowerBackArmRotationOffset = (-MathHelper.PiOver2 - MathHelper.PiOver4) * -NPC.spriteDirection;

                float lowerWorldRot = bodyFacingAngle + upperBackArmRotation + lowerBackArmRotation + lowerBackArmRotationOffset;

                spriteBatch.Draw(LowerArmsBack.Value, lowerBackArmPos, lowerBackArmFrame, NPC.GetAlpha(drawColor), lowerWorldRot, lowerBackArmOrigin, NPC.scale, NPC.SpriteEffectDirection(true), 0);
            }

            // afterimage
            if (Dashing)
                DrawingUtils.DrawAfterimageWithVelocity(spriteBatch, unofficial ? Body.Value : TextureAssets.Npc[Type].Value, NPC.Center - screenPos, NPC.velocity, 3, NPC.frame, new Color(drawColor.R, drawColor.G, drawColor.B, 150), 1f, [NPC.rotation], NPC.frame.Size() * 0.5f, NPC.SpriteEffectDirection(true), 1.5f);

            // body + head
            float headRotation = NPC.rotation;
            if (unofficial)
            {
                if (!hasHistory || NPC.IsABestiaryIconDummy)
                    spriteBatch.Draw(Body.Value, NPC.Center - screenPos, NPC.frame, NPC.GetAlpha(drawColor), NPC.rotation, NPC.frame.Size() / 2, NPC.scale, NPC.SpriteEffectDirection(true), 0);
                else
                {
                    var verts = BuildStrip(screenPos, NPC.GetAlpha(drawColor));
                    spriteBatch.End();

                    var gd = Main.instance.GraphicsDevice;
                    var effect = GetBodyEffect(gd);
                    effect.Texture = Body.Value;
                    effect.World = Main.GameViewMatrix.TransformationMatrix;
                    effect.View = Matrix.Identity;
                    effect.Projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);

                    var prevBlend = gd.BlendState;
                    var prevRaster = gd.RasterizerState;
                    gd.BlendState = BlendState.AlphaBlend;
                    gd.RasterizerState = RasterizerState.CullNone;

                    foreach (var pass in effect.CurrentTechnique.Passes)
                    {
                        pass.Apply();
                        gd.DrawUserPrimitives(PrimitiveType.TriangleStrip, verts, 0, verts.Length - 2);
                    }

                    gd.RasterizerState = prevRaster;
                    gd.BlendState = prevBlend;

                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);
                }

                bool Lasering = IsAwakened && ((NPC.ai[0] == 0 && NPC.ai[2] >= 240) || NPC.ai[0] == 1);
                Vector2 headOffset = new Vector2(154 * NPC.spriteDirection, -18).RotatedBy(NPC.rotation);
                if (Roaring || Lasering)
                {
                    Rectangle headTopFrame = HeadOpenTop.Frame(3, frameX: IsAwakened ? 2 : NPC.spriteDirection == 1 ? 1 : 0);
                    Vector2 headTopOrigin = new(68, 50);
                    if (NPC.spriteDirection == 1)
                        headTopOrigin.X = headTopFrame.Width - headTopOrigin.X;

                    headRotation = NPC.rotation;
                    if(Lasering)
                        headRotation -= (MathHelper.Pi / 6f + (MathF.Sin(Main.GlobalTimeWrappedHourly * 36) * MathHelper.Pi / 36f)) * NPC.spriteDirection; 
                    else if (!Dashing)
                    {
                        float goalAngle = (NPC.Center + headOffset).AngleTo(Main.player[NPC.target == -1 ? Main.myPlayer : NPC.target].Center);
                        if (NPC.spriteDirection == -1)
                            goalAngle += MathHelper.Pi;
                        headRotation = headRotation.AngleLerp(goalAngle, 0.666f * MathHelper.Clamp(NPC.Distance(Main.player[NPC.target == -1 ? Main.myPlayer : NPC.target].Center) / 100f, 0f, 1f));
                    }

                    spriteBatch.Draw(HeadOpenTop.Value, NPC.Center + headOffset - screenPos, headTopFrame, NPC.GetAlpha(drawColor), headRotation, headTopOrigin, NPC.scale, NPC.SpriteEffectDirection(true), 0);

                    Rectangle headBottomFrame = HeadOpenBottom.Frame(3, frameX: IsAwakened ? 2 : NPC.spriteDirection == 1 ? 1 : 0);
                    Vector2 headBottomOrigin = new(52, 2);
                    if (NPC.spriteDirection == 1)
                        headBottomOrigin.X = headBottomFrame.Width - headBottomOrigin.X;

                    Vector2 jawOffset = new Vector2(8 * NPC.spriteDirection, 6).RotatedBy(headRotation);

                    float jawRotation = headRotation;
                    if (Lasering)
                        jawRotation += (MathHelper.Pi / 3f + (MathF.Sin(Main.GlobalTimeWrappedHourly * 36) * MathHelper.Pi / 18f)) * NPC.spriteDirection;
                    else
                        jawRotation += (MathHelper.Pi / 8f + (MathF.Sin(Main.GlobalTimeWrappedHourly * 24) * MathHelper.Pi / 48f)) * NPC.spriteDirection;
                    spriteBatch.Draw(HeadOpenBottom.Value, NPC.Center + headOffset + jawOffset - screenPos, headBottomFrame, NPC.GetAlpha(drawColor), jawRotation, headBottomOrigin, NPC.scale, NPC.SpriteEffectDirection(true), 0);
                }
                else
                {
                    Rectangle headFrame = HeadClosed.Frame(3, frameX: IsAwakened ? 2 : NPC.spriteDirection == 1 ? 1 : 0);
                    Vector2 headOrigin = new(68, 50);
                    if (NPC.spriteDirection == 1)
                        headOrigin.X = headFrame.Width - headOrigin.X;

                    headRotation = NPC.rotation;
                    if (!Dashing)
                    {
                        Vector2 angleTo = NPC.IsABestiaryIconDummy ? Main.MouseScreen : Main.player[NPC.target == -1 ? Main.myPlayer : NPC.target].Center;
                        float dampen = 1f;
                        if ((NPC.spriteDirection == -1 && angleTo.X > (NPC.Center + headOffset).X) || (NPC.spriteDirection == 1 && angleTo.X < (NPC.Center + headOffset).X))
                            dampen = 1 - MathHelper.Clamp((Math.Abs(angleTo.X - (NPC.Center + headOffset).X)) / 64f, 0f, 1f);

                        if (dampen != 0)
                        {
                            float goalAngle = (NPC.Center + headOffset).AngleTo(angleTo);
                            if (NPC.spriteDirection == -1)
                                goalAngle += MathHelper.Pi;
                            headRotation = headRotation.AngleLerp(goalAngle, 0.666f * dampen * MathHelper.Clamp(((NPC.Center + headOffset).Distance(angleTo)) / 100f, 0f, 1f));
                        }
                    }

                    spriteBatch.Draw(HeadClosed.Value, NPC.Center + headOffset - screenPos, headFrame, NPC.GetAlpha(drawColor), headRotation, headOrigin, NPC.scale, NPC.SpriteEffectDirection(true), 0);
                }
            }
            else
                spriteBatch.Draw(TextureAssets.Npc[Type].Value, NPC.Center - screenPos, NPC.frame, NPC.GetAlpha(drawColor), NPC.rotation, NPC.frame.Size() / 2, NPC.scale, NPC.SpriteEffectDirection(true), 0);

            //draw glow/glow afterimage
            if (IsAwakened)
            {
                Color color = NPC.IsABestiaryIconDummy ? AAColor.Shen3 : NPC.GetAlpha(AAColor.Shen3);

                if (unofficial)
                {
                    Vector2 headOffset = new Vector2(154 * NPC.spriteDirection, -18).RotatedBy(NPC.rotation);
                    Vector2 eyeOffset = new Vector2(13 * NPC.spriteDirection, -13).RotatedBy(headRotation);
                    Vector2 position = NPC.Center + headOffset + eyeOffset;
                    spriteBatch.Draw(EyeGlowmask.Value, position - screenPos, null, color, headRotation, EyeGlowmask.Size() / 2, NPC.scale, NPC.SpriteEffectDirection(true), 0);
                    DrawingUtils.DrawAfterimageWithVelocity(spriteBatch, EyeGlowmask.Value, position - screenPos, NPC.velocity, 8, null, color, 1f, [headRotation], EyeGlowmask.Size() * 0.5f, NPC.SpriteEffectDirection(true), 0.3f);
                }
                else
                {
                    spriteBatch.Draw(Glowmask.Value, NPC.Center - screenPos, NPC.frame, color, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, NPC.SpriteEffectDirection(true), 0);
                    DrawingUtils.DrawAfterimageWithVelocity(spriteBatch, Glowmask.Value, NPC.Center - screenPos, NPC.velocity, 8, NPC.frame, color, 1f, [NPC.rotation], NPC.frame.Size() * 0.5f, NPC.SpriteEffectDirection(true), 0.3f);
                }
            }

            // front arm
            if (unofficial)
            {
                int frontArmFrameX = IsAwakened ? 2 : (NPC.spriteDirection == 1 ? 1 : 0);
                Rectangle upperFrontArmFrame = UpperArmsFront.Frame(3, frameX: frontArmFrameX);

                float tFrontArm = 0.5f - (68f / NPC.frame.Width);

                Vector2 upperFrontArmPos;
                float frontBodyFacingAngle;
                if (hasHistory && !NPC.IsABestiaryIconDummy)
                {
                    (Vector2 attachPos, Vector2 forward) = GetBodyPoint(tFrontArm);
                    upperFrontArmPos = attachPos - screenPos + Vector2.UnitY * -10;
                    frontBodyFacingAngle = forward.ToRotation();
                    if (NPC.spriteDirection == -1)
                        frontBodyFacingAngle += MathHelper.Pi;
                }
                else
                {
                    upperFrontArmPos = NPC.Center + (new Vector2(68 * NPC.spriteDirection, -12).RotatedBy(NPC.rotation) * NPC.scale) - screenPos;
                    frontBodyFacingAngle = NPC.rotation;
                }
                Vector2 upperFrontArmOrigin = new(12, 8);
                if (NPC.spriteDirection == 1)
                    upperFrontArmOrigin.X = upperFrontArmFrame.Width - upperFrontArmOrigin.X;

                float upperFrontArmRotation;
                if (Dashing)
                    upperFrontArmRotation = (MathHelper.Pi / 4f + MathF.Sin(Main.GlobalTimeWrappedHourly * 8f + MathHelper.Pi + MathHelper.PiOver2) * MathHelper.Pi / 16f) * -NPC.spriteDirection;
                else
                    upperFrontArmRotation = (MathHelper.Pi / 3f + MathF.Sin(Main.GlobalTimeWrappedHourly * 3f + MathHelper.Pi + MathHelper.PiOver2) * MathHelper.Pi / 8f) * -NPC.spriteDirection;
                float upperFrontArmRotationOffset = -0.85f * -NPC.spriteDirection;

                float upperFrontWorldRot = frontBodyFacingAngle + upperFrontArmRotation + upperFrontArmRotationOffset;

                spriteBatch.Draw(UpperArmsFront.Value, upperFrontArmPos, upperFrontArmFrame, NPC.GetAlpha(drawColor), upperFrontWorldRot, upperFrontArmOrigin, NPC.scale, NPC.SpriteEffectDirection(true), 0);

                // Lower front arm
                Rectangle lowerFrontArmFrame = LowerArmsFront.Frame(3, frameX: frontArmFrameX);
                Vector2 lowerFrontArmOrigin = new(38, -2);
                if (NPC.spriteDirection == 1)
                    lowerFrontArmOrigin.X = lowerFrontArmFrame.Width - lowerFrontArmOrigin.X;

                Vector2 lowerFrontArmLocalOffset = new Vector2(-6 * -NPC.spriteDirection, 8).RotatedBy(upperFrontArmRotation);
                Vector2 lowerFrontArmLocal = (upperFrontArmRotation.ToRotationVector2() * -28f * NPC.spriteDirection + lowerFrontArmLocalOffset) * NPC.scale;
                Vector2 lowerFrontArmWorldOffset = lowerFrontArmLocal.RotatedBy(frontBodyFacingAngle);
                Vector2 lowerFrontArmPos = upperFrontArmPos + lowerFrontArmWorldOffset;

                float lowerFrontArmRotation;
                if (Dashing)
                    lowerFrontArmRotation = (-MathHelper.Pi / 16f + MathF.Sin(Main.GlobalTimeWrappedHourly * 8f) * MathHelper.Pi / 8f) * -NPC.spriteDirection;
                else
                    lowerFrontArmRotation = (MathHelper.PiOver2 + MathF.Sin(Main.GlobalTimeWrappedHourly * 3f) * MathHelper.Pi / 4f) * -NPC.spriteDirection;
                float lowerFrontArmRotationOffset = (-MathHelper.PiOver2 - MathHelper.PiOver4) * -NPC.spriteDirection;

                float lowerFrontWorldRot = frontBodyFacingAngle + upperFrontArmRotation + lowerFrontArmRotation + lowerFrontArmRotationOffset;

                spriteBatch.Draw(LowerArmsFront.Value, lowerFrontArmPos, lowerFrontArmFrame, NPC.GetAlpha(drawColor), lowerFrontWorldRot, lowerFrontArmOrigin, NPC.scale, NPC.SpriteEffectDirection(true), 0);
            }

            //front wing
            Vector2 frontWingPos = NPC.Center - screenPos;
            float frontWingRot = NPC.rotation;
            if (hasHistory && !NPC.IsABestiaryIconDummy && wingCached)
            {
                frontWingPos = sharedWingPos - screenPos;
                frontWingRot = sharedWingRot;
            }
            spriteBatch.Draw(WingFront.Value, frontWingPos, wingFrameFront, NPC.GetAlpha(drawColor), frontWingRot, wingFrameFront.Size() / 2, NPC.scale, NPC.SpriteEffectDirection(true), 0);
            return false;
        }

        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            return false;
        }
    }
}
