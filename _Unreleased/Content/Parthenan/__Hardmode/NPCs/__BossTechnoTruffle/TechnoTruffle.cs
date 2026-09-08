using AAModClassic._Content.Void._PostMoonlord.NPCs.__BossZero;
using AAModClassic._Unreleased.Content.Parthenan.__Hardmode.Items._BossTechnoTruffle.BossStandard;
using AAModClassic._Unreleased.Content.Parthenan.World.Biomes;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Music;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.UI.BigProgressBar;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.Parthenan.__Hardmode.NPCs.__BossTechnoTruffle
{
    [AutoloadBossHead]
    public class TechnoTruffle : ModNPC
    {
        public static Asset<Texture2D> Glowmask1;
        public static Asset<Texture2D> Glowmask2;

        public bool UseFungusAI = true;
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(internalAI[0]);
                writer.Write(internalAI[1]);
                writer.Write(internalAI[2]);
                writer.Write(internalAI[3]);
                writer.Write(UseFungusAI);
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
                UseFungusAI = reader.ReadBoolean();
            }
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Techno Truffle");
            Main.npcFrameCount[NPC.type] = 17;

            Glowmask1 = ModContent.Request<Texture2D>(Texture + "_Glow1");
            Glowmask2 = ModContent.Request<Texture2D>(Texture + "_Glow2");
            NPCID.Sets.BossBestiaryPriority.Add(Type);
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 30000;
            NPC.damage = 50;
            NPC.defense = 40;
            NPC.knockBackResist = 0f;   //this boss will behavior like the DemonEye  //boss frame/animation 
            NPC.value = Item.buyPrice(0, 12, 0, 0);
            NPC.aiStyle = NPCAIStyleID.FaceClosestPlayer;
            NPC.width = 66;
            NPC.height = 104;
            NPC.npcSlots = 1f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = false;
            NPC.buffImmune[46] = true;
            NPC.buffImmune[47] = true;
            NPC.netAlways = true;
            NPC.noTileCollide = true;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            Music = MusicManagementSystem.MusicSlots["TechnoTruffle"];
            SpawnModBiomes = [ModContent.GetInstance<ParthenanBiome>().Type];
            NPC.BossBar = new TechnoTruffleBossBar();
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public static int AISTATE_HOVER = 0, AISTATE_FLIER = 1, AISTATE_SHOOT = 2, AISTATE_ROCKET = 3;
        public static int AISTATE_DASH = 0, AISTATE_CHARGE = 1, AISTATE_FLY = 2;
        public float[] internalAI = new float[4];
        bool HasStopped = false;
        bool SelectPoint = false;
        Vector2 MovePoint = new Vector2(0, 0);
        //as a field this ran while the mod was still loading, before there was a world, so
        //expertMode was always false and this was always 6
        public int ProbeCount => Main.expertMode ? 4 : 6;

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter >= 10)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;
                if (internalAI[1] == AISTATE_ROCKET || internalAI[3] == AISTATE_FLY)
                {
                    if (NPC.frame.Y > frameHeight * 11 && NPC.frame.Y < frameHeight * 8)
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y = frameHeight * 8;
                    }
                }
                else if (!UseFungusAI)
                {
                    if (NPC.frame.Y > frameHeight * 16 && NPC.frame.Y < frameHeight * 12)
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y = frameHeight * 12;
                    }
                }
                else
                {
                    if (NPC.frame.Y > frameHeight * 7)
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                    }
                }
            }
            if (NPC.frame.Y > frameHeight * 16)
            {
                NPC.frame.Y = frameHeight * 12;
            }
        }

        public override void AI()
        {
            NPC.dontTakeDamage = AnyOwnedProbes();
            NPC.TargetClosest();
            Player player = Main.player[NPC.target];

            if (!Main.dedServ)
            {
                Color color = BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, Lighting.GetColor(NPC.Center.ToTileCoordinates()), Lighting.GetColor(NPC.Center.ToTileCoordinates()), Color.Violet, Lighting.GetColor(NPC.Center.ToTileCoordinates()), Color.Violet, Lighting.GetColor(NPC.Center.ToTileCoordinates()));
                Lighting.AddLight((int)(NPC.Center.X) / 16, (int)(NPC.position.Y) / 16, color.R / 255f, color.G / 255f, color.B / 255f);
            }

            if (Main.dayTime)
            {
                NPC.active = false;
                if(Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(0f, 0f), ModContent.ProjectileType<TechnoTruffle_BookIt>(), 0, 0);
                return;
            }
            if (Main.player[NPC.target].dead)
            {
                NPC.TargetClosest(true);
                if (Main.player[NPC.target].dead)
                {
                    NPC.active = false;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(0f, 0f), ModContent.ProjectileType<TechnoTruffle_BookIt>(), 0, 0);
                    return;
                }
            }
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                internalAI[2]++;
            }

            if (internalAI[2] > 900)
            {
                if (!TileBelowEmpty(player))
                {
                    if (UseFungusAI)
                    {
                        UseFungusAI = false;
                    }
                    else
                    {
                        NPC.noGravity = true;
                        UseFungusAI = true;
                    }
                    internalAI[0] = 0;
                    internalAI[1] = 0;
                    internalAI[3] = 0;
                    SelectPoint = true;
                }
                else
                {
                    UseFungusAI = true;
                }
                internalAI[2] = 0;
                NPC.netUpdate = true;
            }

            if (!UseFungusAI)
            {
                MonarchAI();
            }
            else
            {
                FungusAI();
            }
        }

        public bool TileBelowEmpty(Player player)
        {
            int tileX = (int)(player.Center.X / 16f) + player.direction * 2;
            int tileY = (int)((player.position.Y + player.height) / 16f);

            for (int tY = tileY; tY < tileY + 17; tY++)
            {
                if (Main.tile[tileX, tY] == null)
                    continue;
                if (Main.tile[tileX, tY].HasUnactuatedTile && Main.tileSolid[Main.tile[tileX, tY].TileType] && !TileID.Sets.Platforms[Main.tile[tileX, tY].TileType] || Main.tile[tileX, tY].LiquidAmount > 0)
                {
                    return false;
                }
            }
            return true;
        }

        public void MonarchAI()
        {
            UseFungusAI = false;
            //NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];

            if (player.Center.X > NPC.Center.X) // so it faces the player
            {
                NPC.spriteDirection = -1;
            }
            else
            {
                NPC.spriteDirection = 1;
            }

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (internalAI[3] != AISTATE_FLY)
                {
                    NPC.noGravity = false;
                    internalAI[0]++;
                }
                else
                {
                    NPC.noGravity = true;
                }
                if (internalAI[0] >= 180)
                {
                    internalAI[0] = 0;
                    internalAI[3] = Main.rand.Next(3);
                    NPC.ai = new float[4];
                    NPC.netUpdate = true;
                }
                else if (!Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
                {
                    internalAI[3] = AISTATE_FLY;
                    NPC.ai = new float[4];
                    NPC.netUpdate = true;
                }
            }
            if (internalAI[3] == AISTATE_DASH)
            {
                if ((SelectPoint || MovePoint == Vector2.Zero) && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.direction = NPC.Center.X > player.Center.X ? -1 : 1;
                    float Point = 300 * NPC.direction;
                    MovePoint = player.Center + new Vector2(Point, 0);
                    SelectPoint = false;
                    NPC.netUpdate = true;
                }

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    internalAI[0]++;
                }
                if (internalAI[0] >= 120)
                {
                    NPC.direction = NPC.Center.X > MovePoint.X ? -1 : 1;
                    MoveToPoint(MovePoint);
                    NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 1.57f;
                    Lighting.AddLight((int)(NPC.Center.X) / 16, (int)(NPC.position.Y) / 16, Color.LightCyan.R / 255f, Color.LightCyan.G / 255f, Color.LightCyan.B / 255f);
                    if (Main.netMode != NetmodeID.MultiplayerClient && 
                        (Math.Abs(NPC.velocity.X) < 0.05f || 
                        (NPC.direction == 1 && NPC.Center.X > MovePoint.X) || 
                        (NPC.direction == -1 && NPC.Center.X < MovePoint.X))
                    )
                    {
                        NPC.rotation = 0;
                        internalAI[0] = 0;
                        internalAI[3] = Main.rand.Next(3);
                        SelectPoint = true;
                        NPC.netUpdate = true;
                        MovePoint = Vector2.Zero;
                    }
                }
            }
            else if (internalAI[3] == AISTATE_FLY)
            {
                NPC.noTileCollide = true;
                NPC.noGravity = true;
                BaseAI.AISpaceOctopus(NPC, ref NPC.ai, .05f, 8, 250, 0, null);
                NPC.rotation = 0;
                if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
                {
                    NPC.rotation = 0;
                    NPC.noGravity = false;
                    internalAI[0] = 0;
                    internalAI[3] = Main.rand.Next(3);
                    SelectPoint = true;
                    NPC.ai = new float[4];
                    NPC.netUpdate = true;
                    NPC.noTileCollide = false;
                }
                NPC.rotation = 0;
            }
            else
            {
                NPC.direction = (NPC.Center.X > player.Center.X ? -1 : 1);
                if (NPC.velocity == Vector2.Zero)
                    NPC.velocity = Vector2.UnitX * 0.1f * NPC.direction;
                BaseAI.AICharger(NPC, ref NPC.ai, 0.5f, 15f, false);
                NPC.rotation = 0;
                MovePoint = Vector2.Zero;
            }
        }

        public void FungusAI()
        {
            UseFungusAI = true;
            if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
            {
                NPC.noTileCollide = false;
            }
            else
            {
                NPC.noTileCollide = true;
            }
            NPC.TargetClosest();
            Player player = Main.player[NPC.target];
            if (Main.netMode != NetmodeID.MultiplayerClient && internalAI[1] != AISTATE_SHOOT)
            {
                internalAI[0]++;
                if (internalAI[0] >= 180)
                {
                    internalAI[0] = 0;
                    internalAI[1] = Main.rand.Next(3);
                    NPC.ai = new float[4];
                    NPC.netUpdate = true;
                }
            }
            if (internalAI[1] == AISTATE_HOVER)
            {
                BaseAI.AISpaceOctopus(NPC, ref NPC.ai, player.Center, 0.15f, 4f, 170, 8, FireMagic);
            }
            else if (internalAI[1] == AISTATE_FLIER)
            {
                BaseAI.AIFlier(NPC, ref NPC.ai, true, 0.2f, 0.04f, 8f, 3f, false, 1);
            }
            else if (internalAI[1] == AISTATE_SHOOT)
            {
                if (HasStopped)
                {
                    internalAI[0]++;
                }
                if (internalAI[0] >= 60)
                {
                    int attack = Main.rand.Next(4);
                    internalAI[1] = Main.rand.Next(3);
                    internalAI[0] = 0;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        FungusAttack(attack);
                    NPC.netUpdate = true;
                }

                NPC.velocity *= 0.7f;

                if (NPC.velocity.X <= .1f && NPC.velocity.X >= -.1f)
                {
                    NPC.velocity.X = 0;
                }
                if (NPC.velocity.Y <= .1f && NPC.velocity.Y >= -.1f)
                {
                    NPC.velocity.Y = 0;
                }
                if (NPC.velocity == new Vector2(0, 0))
                {
                    HasStopped = true;
                }
            }
            NPC.rotation = 0;
        }

        public float[] shootAI = new float[4];

        public void FireMagic(NPC npc, Vector2 velocity)
        {
            Player player = Main.player[npc.target];
            if (Main.netMode != NetmodeID.MultiplayerClient)
                BaseAI.ShootPeriodic(npc, player.position, player.width, player.height, ModContent.ProjectileType<TechnoTruffle_TruffleShot>(), ref shootAI[0], 5, (int)(npc.damage * (Main.expertMode ? 0.25f : 0.5f)), 8f, true, new Vector2(20f, 15f));
            npc.netUpdate = true;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)          //this make so when the npc has 0 life(dead) he will spawn this
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity, ModContent.ProjectileType<TechnoTruffle_BookIt>(), 0, 0, -1, NPC.scale);
            }
        }

        public override void BossLoot(ref int potionType)
        {   //boss drops
            potionType = ItemID.GreaterHealingPotion;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<TechnoTruffleTreasureBag>()));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TechnoTruffleTrophy>(), 10));

            LeadingConditionRule masterMode = new(new AAConditions.RevOrMaster());

            masterMode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<TechnoTruffleRelic>()));

            npcLoot.Add(masterMode);

            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<TechnoTruffleMask>(), 7));

            notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, ItemID.SoulofFright, ItemID.SoulofSight, ItemID.SoulofMight));

            npcLoot.Add(notExpertRule);
        }

        public void FungusAttack(int Attack)
        {
            if (Attack != 0)
            {
                if (NPC.CountNPCS(ModContent.NPCType<Truffling>()) < 4)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (i == 1)
                        {
                            NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X + 40, (int)NPC.Center.Y - 40, ModContent.NPCType<Truffling>());
                        }
                        if (i == 2)
                        {
                            NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X + 40, (int)NPC.Center.Y + 40, ModContent.NPCType<Truffling>());
                        }
                        if (i == 3)
                        {
                            NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X - 40, (int)NPC.Center.Y - 40, ModContent.NPCType<Truffling>());
                        }
                        else
                        {
                            NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X - 40, (int)NPC.Center.Y + 40, ModContent.NPCType<Truffling>());
                        }
                    }
                }
            }
            else
            {
                //wait for the last wave to be cleared. probes used to stack up every time this
                //attack came around, and the truffle is invulnerable while any of them are alive
                if (!AnyOwnedProbes())
                {
                    for (int i = 0; i < ProbeCount; i++)
                    {
                        NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X + 10, (int)NPC.Center.Y - 10, ModContent.NPCType<TruffleProbe>(), ai0: i);
                    }
                    NPC.netUpdate = true;
                }
            }
        }

        //AnyNPCs is too broad to gate invulnerability on; it also picks up another truffle's
        //probes, and probes left active with no health
        private bool AnyOwnedProbes()
        {
            int probeType = ModContent.NPCType<TruffleProbe>();
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && npc.life > 0 && npc.type == probeType
                    && npc.ModNPC is TruffleProbe probe && (probe.body == -1 || probe.body == NPC.whoAmI))
                {
                    return true;
                }
            }
            return false;
        }

        public void MoveToPoint(Vector2 point, bool goUpFirst = false)
        {
            float moveSpeed = 14f;
            if (moveSpeed == 0f || NPC.Center == point) return; //don't move if you have no move speed
            float velMultiplier = 1f;
            Vector2 dist = point - NPC.Center;
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
            Collision.StepUp(ref NPC.position, ref NPC.velocity, NPC.width, NPC.height, ref NPC.stepSpeed, ref NPC.gfxOffY);
        }


        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D glowTex = Glowmask1.Value;
            Texture2D glowTex1 = Glowmask2.Value;

            Color color = BaseUtility.MultiLerpColor(((int)(Main.GlobalTimeWrappedHourly * 60)) % 100 / 100f, drawColor, drawColor, Color.Violet, drawColor, Color.Violet, drawColor);

            if (internalAI[1] == AISTATE_ROCKET)
            {
                BaseDrawing.DrawAfterimage(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, 1.5f, 1f, 5, false, 0f, 0f, Color.LightCyan);
            }
            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos, NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            spriteBatch.Draw(glowTex, NPC.Center - screenPos, NPC.frame, color, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            spriteBatch.Draw(glowTex1, NPC.Center - screenPos, NPC.frame, Color.White, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            return false;
        }
    }

    public class TechnoTruffleBossBar : ModBossBar
    {
        private float _shieldMax = 0f;
        private Asset<Texture2D> _icon = ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/Parthenan/__Hardmode/NPCs/__BossTechnoTruffle/TechnoTruffle_Head_Boss");

        public override bool? ModifyInfo(ref BigProgressBarInfo info, ref float life, ref float lifeMax, ref float shield, ref float shieldMax)
        {
            if (Main.npc.Length < info.npcIndexToAimAt)
                return false;

            NPC target = Main.npc[info.npcIndexToAimAt];

            if (target == null || !target.active)
                return false;

            if (target.ModNPC is not TechnoTruffle technoTruffle)
                return false;

            life = target.life;
            lifeMax = target.lifeMax;

            if (!target.dontTakeDamage)
            {
                _shieldMax = 0;
                return true;
            }

            shield = 0;
            shieldMax = 0;

            int probeType = ModContent.NPCType<TruffleProbe>();
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && npc.life > 0 && npc.type == probeType
                    && npc.ModNPC is TruffleProbe probe && (probe.body == -1 || probe.body == technoTruffle.NPC.whoAmI))
                {
                    shield += npc.life;
                    shieldMax += npc.lifeMax;
                }
            }

            if (shieldMax > _shieldMax)
                _shieldMax = shieldMax;
            else
                shieldMax = _shieldMax;

            return true; 
        }

        public override Asset<Texture2D> GetIconTexture(ref Rectangle? iconFrame) => _icon;
    }
}


