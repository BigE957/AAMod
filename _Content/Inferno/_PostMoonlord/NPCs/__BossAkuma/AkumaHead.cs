using AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Ammo;
using AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.BossStandard;
using AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Tools;
using AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Weapons;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Materials;
using AAModClassic._Content.Inferno.World.Biomes;
using AAModClassic._CrossMod.CalamityMod.LoreItems;
using AAModClassic._Removed.Content._Tinker._PostMoonlord.Items.Accessories;
using AAModClassic.Achievements;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Music;
using AAModClassic.UI.Titles;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
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

namespace AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma
{
    [AutoloadBossHead]
    public class AkumaHead : ModNPC
    {
        public bool loludided;
        private bool weakness;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Akuma; Draconian Demon");
            NPCID.Sets.ShouldBeCountedAsBoss[NPC.type] = true;
            Main.npcFrameCount[NPC.type] = 3;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.5f * balance);
            NPC.damage = (int)(NPC.damage * 0.6f);
        }

        public override void SetDefaults()
        {
            NPC.noTileCollide = true;
            NPC.height = 80;
            NPC.width = 80;
            NPC.aiStyle = -1;
            NPC.netAlways = true;
            NPC.knockBackResist = 0f;
            NPC.damage = 140;
            NPC.defense = 80;
            NPC.lifeMax = 400000;
            if (Main.expertMode)
            {
                NPC.value = Item.buyPrice(0, 0, 0, 0);
            }
            else
            {
                NPC.value = Item.buyPrice(0, 30, 0, 0);
            }
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.aiStyle = -1;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.behindTiles = true;
            Music = MusicManagementSystem.MusicSlots["Akuma"];
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = new SoundStyle("AAModClassic/Sounds/AkumaRoar");
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
            NPC.buffImmune[103] = false;
            if (!NPC.IsABestiaryIconDummy)
                NPC.alpha = 255;
            SceneEffectPriority = SceneEffectPriority.BossHigh;

            SpawnModBiomes = [ModContent.GetInstance<InfernoBiome>().Type];
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
                new FlavorTextBestiaryInfoElement("Mods.AAModClassic.Bestiary.Akuma")
            ]);
        }

        private bool fireAttack;
        private int attackFrame;
        private int attackCounter;
        private int attackTimer;
        public static int MinionCount = 0;
        public int MaxMinons = Main.expertMode ? 3 : 4;
        public int damage = 0;

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

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override bool PreAI()
        {
            NPC.GetGlobalNPC<TitleGlobalNPC>().ShowTitle = true;

            Player player = Main.player[NPC.target];
            damage = 70;

            if (fireAttack == true || internalAI[0] >= 450)
            {
                attackCounter++;
                if (attackCounter > 10)
                {
                    attackFrame++;
                    attackCounter = 0;
                }
                if (attackFrame >= 3)
                {
                    attackFrame = 2;
                }
            }
            float dist = NPC.Distance(player.Center);
            internalAI[0]++;
            if (internalAI[0] == 350)
            {
                QuoteSaid = false;
                Roar(roarTimerMax, false);
                internalAI[1] = Main.rand.Next(3);
            }
            if (internalAI[0] > 300)
            {
                Attack(NPC);
            }
            if (internalAI[0] >= 400)
            {
                internalAI[0] = 0;
            }

            if (dist > 300 & Main.rand.NextBool(20) && fireAttack == false && internalAI[0] < 500)
            {
                fireAttack = true;
            }

            if (fireAttack == true)
            {
                attackTimer++;
                if ((attackTimer % 20 == 0) && NPC.HasBuff(BuffID.Wet))
                {
                    for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                    {
                        int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, ModContent.DustType<Dusts.MireBubbleDust>(), 0f, 0f, 90, default, 2f);
                        Main.dust[num935].noGravity = true;
                        Main.dust[num935].velocity.Y -= 1f;
                    }
                    if (weakness == false)
                    {
                        weakness = true;
                        if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Akuma.WaterHit"), new Color(180, 41, 32));
                    }
                }
                else if (!NPC.HasBuff(BuffID.Wet))
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        AAAI.BreatheFire(NPC, 35, true, ModContent.ProjectileType<AkumaHead_Breath>(), 2);
                }
                if (attackTimer >= 80)
                {
                    fireAttack = false;
                    attackTimer = 0;
                    attackFrame = 0;
                    attackCounter = 0;
                }
            }
            AAAI.DustOnNPCSpawn(NPC, ModContent.DustType<Dusts.AkumaDust>(), 2, 12);

            NPC.spriteDirection = NPC.velocity.X > 0 ? -1 : 1;
            NPC.ai[1]++;
            if (NPC.ai[1] >= 1200)
                NPC.ai[1] = 0;
            NPC.TargetClosest(true);
            if (!Main.player[NPC.target].active || Main.player[NPC.target].dead)
            {
                NPC.TargetClosest(true);
                if (!Main.player[NPC.target].active || Main.player[NPC.target].dead)
                {
                    NPC.ai[3]++;
                    NPC.velocity.Y = NPC.velocity.Y + 0.11f;
                    if (NPC.ai[3] >= 300)
                    {
                        NPC.active = false;
                    }
                }
                else
                    NPC.ai[3] = 0;
            }
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (NPC.ai[0] == 0)
                {
                    NPC.realLife = NPC.whoAmI;
                    int latestNPC = NPC.whoAmI;
                    int[] Frame = { 1, 2, 0, 1, 2, 1, 2, 0, 1, 2, 1, 2, 0, 1, 2, 3, 4 };
                    for (int i = 0; i < Frame.Length; ++i)
                    {
                        latestNPC = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<AkumaBody>(), NPC.whoAmI, 0, latestNPC);
                        Main.npc[latestNPC].realLife = NPC.whoAmI;
                        Main.npc[latestNPC].ai[3] = NPC.whoAmI;
                        Main.npc[latestNPC].netUpdate = true;
                        Main.npc[latestNPC].ai[2] = Frame[i];
                    }
                    NPC.ai[0] = 1;
                    NPC.netUpdate2 = true;
                }
            }

            bool collision = true;

            float speed = 12f;
            float acceleration = 0.13f;

            Vector2 npcCenter = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
            float targetXPos = Main.player[NPC.target].position.X + (Main.player[NPC.target].width / 2);
            float targetYPos = Main.player[NPC.target].position.Y + (Main.player[NPC.target].height / 2);

            float targetRoundedPosX = (int)(targetXPos / 16.0) * 16;
            float targetRoundedPosY = (int)(targetYPos / 16.0) * 16;
            npcCenter.X = (int)(npcCenter.X / 16.0) * 16;
            npcCenter.Y = (int)(npcCenter.Y / 16.0) * 16;
            float dirX = targetRoundedPosX - npcCenter.X;
            float dirY = targetRoundedPosY - npcCenter.Y;

            float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
            if (!collision)
            {
                NPC.TargetClosest(true);
                NPC.velocity.Y = NPC.velocity.Y + 0.11f;
                if (NPC.velocity.Y > speed)
                    NPC.velocity.Y = speed;
                if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < speed * 0.4)
                {
                    if (NPC.velocity.X < 0.0)
                        NPC.velocity.X = NPC.velocity.X - acceleration * 1.1f;
                    else
                        NPC.velocity.X = NPC.velocity.X + acceleration * 1.1f;
                }
                else if (NPC.velocity.Y == speed)
                {
                    if (NPC.velocity.X < dirX)
                        NPC.velocity.X = NPC.velocity.X + acceleration;
                    else if (NPC.velocity.X > dirX)
                        NPC.velocity.X = NPC.velocity.X - acceleration;
                }
                else if (NPC.velocity.Y > 4.0)
                {
                    if (NPC.velocity.X < 0.0)
                        NPC.velocity.X = NPC.velocity.X + acceleration * 0.9f;
                    else
                        NPC.velocity.X = NPC.velocity.X - acceleration * 0.9f;
                }
            }
            else
            {
                if (NPC.soundDelay == 0)
                {
                    float num1 = length / 40f;
                    if (num1 < 10.0)
                        num1 = 10f;
                    if (num1 > 20.0)
                        num1 = 20f;
                    NPC.soundDelay = (int)num1;
                }
                float absDirX = Math.Abs(dirX);
                float absDirY = Math.Abs(dirY);
                float newSpeed = speed / length;
                dirX *= newSpeed;
                dirY *= newSpeed;
                if (NPC.velocity.X > 0.0 && dirX > 0.0 || NPC.velocity.X < 0.0 && dirX < 0.0 || NPC.velocity.Y > 0.0 && dirY > 0.0 || NPC.velocity.Y < 0.0 && dirY < 0.0)
                {
                    if (NPC.velocity.X < dirX)
                        NPC.velocity.X = NPC.velocity.X + acceleration;
                    else if (NPC.velocity.X > dirX)
                        NPC.velocity.X = NPC.velocity.X - acceleration;
                    if (NPC.velocity.Y < dirY)
                        NPC.velocity.Y = NPC.velocity.Y + acceleration;
                    else if (NPC.velocity.Y > dirY)
                        NPC.velocity.Y = NPC.velocity.Y - acceleration;
                    if (Math.Abs(dirY) < speed * 0.2 && (NPC.velocity.X > 0.0 && dirX < 0.0 || NPC.velocity.X < 0.0 && dirX > 0.0))
                    {
                        if (NPC.velocity.Y > 0.0)
                            NPC.velocity.Y = NPC.velocity.Y + acceleration * 2f;
                        else
                            NPC.velocity.Y = NPC.velocity.Y - acceleration * 2f;
                    }
                    if (Math.Abs(dirX) < speed * 0.2 && (NPC.velocity.Y > 0.0 && dirY < 0.0 || NPC.velocity.Y < 0.0 && dirY > 0.0))
                    {
                        if (NPC.velocity.X > 0.0)
                            NPC.velocity.X = NPC.velocity.X + acceleration * 2f;
                        else
                            NPC.velocity.X = NPC.velocity.X - acceleration * 2f;
                    }
                }
                else if (absDirX > absDirY)
                {
                    if (NPC.velocity.X < dirX)
                        NPC.velocity.X = NPC.velocity.X + acceleration * 1.1f;
                    else if (NPC.velocity.X > dirX)
                        NPC.velocity.X = NPC.velocity.X - acceleration * 1.1f;
                    if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < speed * 0.5)
                    {
                        if (NPC.velocity.Y > 0.0)
                            NPC.velocity.Y = NPC.velocity.Y + acceleration;
                        else
                            NPC.velocity.Y = NPC.velocity.Y - acceleration;
                    }
                }
                else
                {
                    if (NPC.velocity.Y < dirY)
                        NPC.velocity.Y = NPC.velocity.Y + acceleration * 1.1f;
                    else if (NPC.velocity.Y > dirY)
                        NPC.velocity.Y = NPC.velocity.Y - acceleration * 1.1f;
                    if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < speed * 0.5)
                    {
                        if (NPC.velocity.X > 0.0)
                            NPC.velocity.X = NPC.velocity.X + acceleration;
                        else
                            NPC.velocity.X = NPC.velocity.X - acceleration;
                    }
                }
            }
            NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 1.57f;

            if (!Main.dayTime)
            {
                if (loludided == false)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Akuma.Despawn.Night"), new Color(180, 41, 32));
                    loludided = true;
                }
                NPC.velocity.Y = NPC.velocity.Y + 1f;
                if (NPC.position.Y - NPC.height - NPC.velocity.Y >= Main.maxTilesY && Main.netMode != NetmodeID.MultiplayerClient) { BaseAI.KillNPC(NPC); NPC.netUpdate2 = true; }
            }

            if (Main.player[NPC.target].dead || Math.Abs(NPC.position.X - Main.player[NPC.target].position.X) > 6000f || Math.Abs(NPC.position.Y - Main.player[NPC.target].position.Y) > 6000f)
            {
                if (loludided == false)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Akuma.Despawn.Escape"), new Color(180, 41, 32));
                    loludided = true;
                }
                NPC.velocity.Y = NPC.velocity.Y - 1f;
                if (NPC.position.Y < 0)
                {
                    NPC.velocity.Y = NPC.velocity.Y - 1f;
                }
                if (NPC.position.Y < 0)
                {
                    for (int num957 = 0; num957 < 200; num957++)
                    {
                        if (Main.npc[num957].aiStyle == NPC.aiStyle)
                        {
                            Main.npc[num957].active = false;
                        }
                    }
                }
            }

            if (collision)
            {
                if (NPC.localAI[0] != 1)
                    NPC.netUpdate = true;
                NPC.localAI[0] = 1f;
            }
            else
            {
                if (NPC.localAI[0] != 0.0)
                    NPC.netUpdate = true;
                NPC.localAI[0] = 0.0f;
            }
            if ((NPC.velocity.X > 0.0 && NPC.oldVelocity.X < 0.0 || NPC.velocity.X < 0.0 && NPC.oldVelocity.X > 0.0 || NPC.velocity.Y > 0.0 && NPC.oldVelocity.Y < 0.0 || NPC.velocity.Y < 0.0 && NPC.oldVelocity.Y > 0.0) && !NPC.justHit)
                NPC.netUpdate = true;

            return false;
        }

        public bool Quote1;
        public bool Quote2;
        public bool Quote3;
        public bool Quote4;
        public bool Quote5;
        public bool QuoteSaid;

        public void Attack(NPC npc)
        {
            bool sayQuote = Main.rand.NextBool(4);
            if (internalAI[1] == 0)
            {
                if (!QuoteSaid && sayQuote)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat((!Quote1) ? Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Akuma.Attacks.Skyfall.A") : Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Akuma.Attacks.Skyfall.B"), new Color(180, 41, 32));
                    QuoteSaid = true;
                    Quote1 = true;
                }
                if (internalAI[0] == 320 || internalAI[0] == 340 || internalAI[0] == 360 || internalAI[0] == 380)
                {
                    int Fireballs = Main.expertMode ? 10 : 8;
                    for (int Loops = 0; Loops < Fireballs; Loops++)
                    {
                        AkumaAttacks.Dragonfire(npc, Mod, false);
                    }
                }

            }
            else if (internalAI[1] == 1)
            {
                if (!QuoteSaid && sayQuote)
                {
                    if (!Quote3 || Main.rand.NextBool(4))
                        if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat((!Quote3) ? Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Akuma.Attacks.BigShot.A") : Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Akuma.Attacks.BigShot.B"), new Color(180, 41, 32));
                    QuoteSaid = true;
                    Quote3 = true;
                }
                if (internalAI[0] == 350)
                {
                    Projectile.NewProjectile(npc.GetSource_FromThis(), npc.Center.X, npc.Center.Y, npc.velocity.X * 2, npc.velocity.Y, ModContent.ProjectileType<AkumaHead_MegaFireBomb>(), damage, 3, Main.myPlayer);
                }
            }
            else
            {
                if (!QuoteSaid && sayQuote)
                {
                    if (!Quote5 || Main.rand.NextBool(4))
                        if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat((!Quote5) ? Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Akuma.Attacks.SeekingFlames.A") : Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Akuma.Attacks.SeekingFlames.B"), new Color(180, 41, 32));
                    QuoteSaid = true;
                    Quote5 = true;
                }
                if (internalAI[0] == 350)
                {
                    int Fireballs = Main.expertMode ? 6 : 10;
                    float spread = 70f * 0.0174f;
                    float baseSpeed = (float)Math.Sqrt((npc.velocity.X * npc.velocity.X) + (npc.velocity.Y * npc.velocity.Y));
                    double startAngle = Math.Atan2(npc.velocity.X, npc.velocity.Y) - .1d;
                    double deltaAngle = spread / 6f;
                    double offsetAngle;
                    for (int i = 0; i < Fireballs; i++)
                    {
                        offsetAngle = startAngle + (deltaAngle * i);
                        Projectile.NewProjectile(npc.GetSource_FromThis(), npc.Center.X, npc.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle) * 2, baseSpeed * (float)Math.Cos(offsetAngle) * 2, ModContent.ProjectileType<AkumaHead_FireBomb>(), damage, 3, Main.myPlayer);
                    }
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            NPCID.Sets.NPCBestiaryDrawModifiers value = new()
            {
                PortraitPositionXOverride = 36,
                Position = new Vector2(72, 40),
                Scale = 0.75f,
                PortraitScale = 0.75f
            };
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;

            if (NPC.IsABestiaryIconDummy && NPC.type == ModContent.NPCType<AkumaHead>())
            {
                int[] Frame = { 1, 2, 0, 1, 2, 1, 2, 0, 1, 2, 1, 2, 0, 1, 2, 3, 4 };

                int segmentCount = 4;
                Vector2 baseOffset = Vector2.Zero;
                float segmentSpacing = 38;
                float animationSpeed = 1f;
                float range = 20f;
                float rotationStrength = 0.1f;

                float headOffset = -20;
                int headSpeedOffset = 0;

                float offset = -0.2f;
                float startX = baseOffset.X;
                float startY = baseOffset.Y;
                float wormTimer = base.NPC.GetGlobalNPC<BestiaryDrawingNPC>().bestiaryWormTimer;

                // Draw the body segments
                for (int i = segmentCount; i > 0; i--)
                {
                    int myFrame = Frame[i];
                    if (myFrame != 0)
                        continue;

                    float bodyOffset = i * segmentSpacing - segmentSpacing * 0.5f;

                    AkumaBody.DrawBackArm(spriteBatch, NPC.position + new Vector2(startX + bodyOffset, MathF.Sin((wormTimer + offset * i) * animationSpeed) * range + startY), Color.White, NPC.rotation - MathHelper.PiOver2 - MathF.Cos((wormTimer + offset * i) * animationSpeed) * MathHelper.PiOver4 * rotationStrength, 1, NPC.scale, wormTimer + i);
                }

                for (int i = segmentCount; i > 0; i--)
                {
                    // The first segment is slightly closer to keep up with the head
                    float bodyOffset = i * segmentSpacing - segmentSpacing * 0.5f;

                    int myFrame = Frame[i];
                    if (myFrame == 0)
                    {
                        spriteBatch.Draw(AkumaBody.ArmlessBody.Value, NPC.position + new Vector2(startX + bodyOffset, MathF.Sin((wormTimer + offset * i) * animationSpeed) * range + startY), null, NPC.GetAlpha(drawColor), NPC.rotation - MathHelper.PiOver2 - MathF.Cos((wormTimer + offset * i) * animationSpeed) * MathHelper.PiOver4 * rotationStrength, AkumaBody.ArmlessBody.Size() * 0.5f, NPC.scale, SpriteEffects.FlipHorizontally, 0);
                    }
                    else
                    {
                        Rectangle frame = TextureAssets.Npc[ModContent.NPCType<AkumaBody>()].Frame(1, 5, 0, myFrame);
                        spriteBatch.Draw(TextureAssets.Npc[ModContent.NPCType<AkumaBody>()].Value, NPC.position + new Vector2(startX + bodyOffset, MathF.Sin((wormTimer + offset * i) * animationSpeed) * range + startY), frame, NPC.GetAlpha(drawColor), NPC.rotation - MathHelper.PiOver2 - MathF.Cos((wormTimer + offset * i) * animationSpeed) * MathHelper.PiOver4 * rotationStrength, frame.Size() / 2, NPC.scale, SpriteEffects.FlipHorizontally, 0f);
                    }
                }

                for (int i = segmentCount; i > 0; i--)
                {
                    int myFrame = Frame[i];
                    if (myFrame != 0)
                        continue;

                    float bodyOffset = i * segmentSpacing - segmentSpacing * 0.5f;

                    AkumaBody.DrawFrontArm(spriteBatch, NPC.position + new Vector2(startX + bodyOffset, MathF.Sin((wormTimer + offset * i) * animationSpeed) * range + startY), Color.White, NPC.rotation - MathHelper.PiOver2 - MathF.Cos((wormTimer + offset * i) * animationSpeed) * MathHelper.PiOver4 * rotationStrength, 1, NPC.scale, wormTimer + i);
                }

                // Draw the head
                spriteBatch.Draw(TextureAssets.Npc[Type].Value, NPC.position + new Vector2(startX + headOffset, MathF.Sin((wormTimer - headSpeedOffset) * animationSpeed) * range + startY), NPC.frame, NPC.GetAlpha(drawColor), NPC.rotation - MathHelper.PiOver2 - MathF.Cos((wormTimer - headSpeedOffset) * animationSpeed) * MathHelper.PiOver4 * rotationStrength, NPC.frame.Size() * 0.5f, NPC.scale, SpriteEffects.FlipHorizontally, 0f);
                return false;
            }


            Texture2D texture = TextureAssets.Npc[NPC.type].Value;
            if (fireAttack)
            {
                Vector2 drawCenter = new Vector2(NPC.Center.X, NPC.Center.Y);
                int num214 = texture.Height / 3;
                int y6 = num214 * attackFrame;
                spriteBatch.Draw(texture, drawCenter - screenPos, new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, y6, texture.Width, num214)), NPC.IsABestiaryIconDummy ? Color.White : NPC.GetAlpha(drawColor), NPC.rotation, new Vector2(texture.Width / 2f, num214 / 2f), NPC.scale, NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }
            else
            {
                spriteBatch.Draw(texture, NPC.Center - screenPos, NPC.frame, NPC.IsABestiaryIconDummy ? Color.White : NPC.GetAlpha(drawColor), NPC.rotation, NPC.frame.Size() / 2, NPC.scale, NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }
            
            return false;
        }

        public override bool PreKill()
        {
            if (Main.expertMode)
                NPC.boss = false;
            return true;
        }

        public override void OnKill()
        {
            if (!Main.expertMode)
            {
                if (!NPC.BeenKilled(true))
                {
                    NPC.OnGameEventClearedForTheFirstTime(-1);
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Akuma.Defeat.Status"), Color.DarkOrange.R, Color.DarkOrange.G, Color.DarkOrange.B, false);
                }
                
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Akuma.Defeat.NotExpert"), new Color(180, 41, 32));

                if (NPC.playerInteraction[Main.myPlayer])
                    AkumaKilled.Condition.Complete();
            }
            if (Main.expertMode)
            {
                int npcID = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<AkumaTransition>(), 0, 0, 0, 0, 0, NPC.target);
                Main.npc[npcID].Center = NPC.Center;
                Main.npc[npcID].netUpdate2 = true; Main.npc[npcID].netUpdate = true;
            }
        }

        public override void BossLoot(ref int potionType)
        {
            if (Main.expertMode)
                potionType = 0;
            else
                potionType = ItemID.SuperHealingPotion;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AkumaTrophy>(), 10));

            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<AkumaMask>(), 7));

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<CrucibleScale>(), 1, 20, 30));

            int[] lootTable = { ModContent.ItemType<DraconianTerratool>(), ModContent.ItemType<Daystorm>(), ModContent.ItemType<AncientLungStaff>(), ModContent.ItemType<MorningGlory>(), ModContent.ItemType<RadiantDawn>(), ModContent.ItemType<Solar>(), ModContent.ItemType<SunPartisan>(), ModContent.ItemType<ReignOfFire>(), ModContent.ItemType<DaybreakArrow>(), ModContent.ItemType<Daycrusher>(), ModContent.ItemType<Dawnstrike>(), ModContent.ItemType<Sunstorm>(), ModContent.ItemType<SolarStaff>(), ModContent.ItemType<DragonShiv>(), ModContent.ItemType<YearOfTheDragon>() };
            notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, lootTable));

            LeadingConditionRule loreCondition = new(new LoreItemDropCondition<AkumaHead>());
            notExpertRule.OnSuccess(loreCondition.OnSuccess(new PerPlayerDropRule(ModContent.ItemType<AkumaLore>(), 1)));

            npcLoot.Add(notExpertRule);

            LeadingConditionRule anceintsDownAndRemoved = new(new PostLateAncientsAndRemovedWorldAndNotExpert());

            anceintsDownAndRemoved.OnSuccess(ItemDropRule.Common(ModContent.ItemType<PowerStone>(), 50));

            npcLoot.Add(anceintsDownAndRemoved);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                NPC.position.X = NPC.position.X + NPC.width / 2;
                NPC.position.Y = NPC.position.Y + NPC.height / 2;
                NPC.position.X = NPC.position.X - NPC.width / 2;
                NPC.position.Y = NPC.position.Y - NPC.height / 2;
                int dust1 = ModContent.DustType<Dusts.AkumaDust>();
                int dust2 = ModContent.DustType<Dusts.AkumaDust>();
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust1, 0f, 0f, 0, Scale: 1.3f);
                Main.dust[dust1].velocity *= 0.5f;
                Main.dust[dust1].fadeIn = 1f;
                Main.dust[dust1].noGravity = false;
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust2, 0f, 0f, 0, Scale: 1.3f);
                Main.dust[dust2].velocity *= 0.5f;
                Main.dust[dust2].fadeIn = 1f;
                Main.dust[dust2].noGravity = true;
            }
        }


        public int roarTimer = 0; //if this is > 0, then use the roaring frame.
        public int roarTimerMax = 120; //default roar timer. only changed for fire breath as it's longer.
        public bool Roaring //wether or not he is roaring. only used clientside for frame visuals.
        {
            get
            {
                return roarTimer > 0;
            }
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
                SoundEngine.PlaySound(new SoundStyle("AAModClassic/Sounds/AkumaRoar"), NPC.Center);
            }
        }


        public override void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
        {
            spriteEffects = NPC.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = NPC.rotation;
        }
    }
}

