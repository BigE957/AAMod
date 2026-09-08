using AAModClassic._Content.Mire.___PreHardmode.Items._BossHydra.BossStandard;
using AAModClassic._Content.Mire.___PreHardmode.Items.Accessories;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic._Content.Mire.___PreHardmode.Items.Pets;
using AAModClassic._Content.Mire.___PreHardmode.Items.Weapons;
using AAModClassic._Content.Mire.World.Biomes;
using AAModClassic._CrossMod;
using AAModClassic._CrossMod.CalamityMod.LoreItems;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Music;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using SteelSeries.GameSense.DeviceZone;
using System;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.UI.BigProgressBar;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.___PreHardmode.NPCs.__BossHydra
{
    [AutoloadBossHead]
    public class HydraBody : ModNPC
    {
        public NPC Head1;
        public NPC Head2;
        public NPC Head3;
        public NPC Head4;
        public NPC Head5;
        public NPC Head6;
        public NPC Head7;
        public NPC Head8;
        public NPC Head9;
        public bool HeadsSpawned = false;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hydra");
            Main.npcFrameCount[NPC.type] = 15;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new()
            {
                Scale = 0.65f,
                PortraitScale = 0.75f,
                PortraitPositionYOverride = 64,
                Position = new(0, 96)
            };
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
        }

        public override void SetDefaults()
        {
            NPC.npcSlots = 100;
            NPC.width = 130;
            NPC.height = 116;
            NPC.aiStyle = -1;
            NPC.damage = 40;
            NPC.defense = 300;
            NPC.lifeMax = 4000;
            NPC.value = Item.buyPrice(0, 5, 0, 0);
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.Item80;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.noGravity = false;
            NPC.netAlways = true;
            Music = MusicManagementSystem.MusicSlots["Hydra"];
            NPC.buffImmune[BuffID.Poisoned] = true;
            SpawnModBiomes = [ModContent.GetInstance<MireBiome>().Type];
            NPC.BossBar = new HydraBossBar();
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
                new FlavorTextBestiaryInfoElement("Mods.AAModClassic.Bestiary.Hydra")
            ]);
        }

        public override void BossLoot(ref int potionType)
        {
            potionType = ItemID.HealingPotion;
        }

        public override void OnKill()
        {
            if (!NPCExtensions.BeenKilled<HydraBody>(true))
                NPC.NewNPC(NPC.GetSource_Death(), (int)NPC.position.X + (Main.rand.NextBool(2) ? 200 : -200), (int)NPC.position.Y - 200, ModContent.NPCType<HarukaShadowPostHydra>());

            //NPC.value = 0f;
            //NPC.boss = false;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<HydraTreasureBag>()));

            npcLoot.AddLoreItemDrop<HydraBody>(ModContent.ItemType<HydraLore>());

            LeadingConditionRule masterMode = new(new AAConditions.RevOrMaster());

            masterMode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<HydraRelic>()));

            npcLoot.Add(masterMode);

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<HydraTrophy>(), 10));

            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

            if (ContentReplacementSystem.NeedToReplaceContent)
                notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, ModContent.ItemType<HydrasSpear>(), ModContent.ItemType<Mossket>(), ModContent.ItemType<GunkWand>(), ModContent.ItemType<GlowingMossBall>(), ModContent.ItemType<ShadowBand>()));

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<HydraMask1>(), 7));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<HydraMask2>(), 7));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<HydraMask3>(), 7));

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<HydraHide>(), 1, 30, 50));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<AbyssiumOre>(), 1, 40, 90));

            npcLoot.Add(notExpertRule);
        }

        public Rectangle frameBottom = new Rectangle(0, 0, 1, 1);
        public bool chasePlayer = false;
        public bool runningAway = false;
        public Player playerTarget = null;

        public bool TeleportMe1 = false;
        public bool TeleportMe2 = false;
        public bool TeleportMe3 = false;

		public void HandleHeads()
		{
			if(Main.netMode != NetmodeID.MultiplayerClient)
			{
				if(!HeadsSpawned)
				{
                    headindex[0] = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<HydraHead1>(), 0);
					Head1 = Main.npc[headindex[0]];
					Head1.ai[0] = NPC.whoAmI;

                    headindex[1] = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<HydraHead2>(), 0);
					Head2 = Main.npc[headindex[1]];
					Head2.ai[0] = NPC.whoAmI;

                    headindex[2] = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<HydraHead3>(), 0);
					Head3 = Main.npc[headindex[2]];
					Head3.ai[0] = NPC.whoAmI;					

					Head1.netUpdate = true;
					Head2.netUpdate = true;
					Head3.netUpdate = true;
					HeadsSpawned = true;
                    NPC.netUpdate = true;
				}
			}
            else
			{
				if(!HeadsSpawned)
				{
                    if(headindex[0] != -1)
                    {
                        Head1 = Main.npc[headindex[0]];
					    Head1.ai[0] = NPC.whoAmI;
                    }
                    if(headindex[1] != -1)
                    {
                        Head2 = Main.npc[headindex[1]];
					    Head1.ai[0] = NPC.whoAmI;
                    }
                    if(headindex[2] != -1)
                    {
                        Head3 = Main.npc[headindex[2]];
					    Head1.ai[0] = NPC.whoAmI;
                    }

					if(Head1 != null && Head2 != null && Head3 != null)
					{
						HeadsSpawned = true;
					}
				}
			}
		}

        public override void AI()
        {

            if (Main.dayTime)
            {
                AIMovementRunAway();
                return;
            }

            HandleHeads();

            if (playerTarget != null)
            {
                float dist = NPC.Distance(playerTarget.Center);
                if (!playerTarget.ZoneAnyMire())
                {
                    NPC.alpha += 3;
                    if (NPC.alpha >= 255)
                    {
                        NPC.alpha = 255;
                    }
                    if (dist > 700 || !Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
                    {
                        NPC.alpha += 3;
                        if (NPC.alpha >= 255)
                        {
                            Vector2 tele = new Vector2(playerTarget.Center.X + (Main.rand.NextBool(2) ? 120 : -120), playerTarget.Center.Y - 16);
                            TeleportMe1 = true;
                            TeleportMe2 = true;
                            TeleportMe3 = true;
                            NPC.Center = tele;
                            NPC.netOffset = Vector2.Zero;
                            NPC.netUpdate = true;
                        }
                    }
                }
                else
                {
                    if (dist > 700 || !Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
                    {
                        NPC.alpha += 3;
                        if (NPC.alpha >= 255)
                        {
                            Vector2 tele = new Vector2(playerTarget.Center.X + (Main.rand.NextBool(2) ? 120 : -120), playerTarget.Center.Y - 16);
                            TeleportMe1 = true;
                            TeleportMe2 = true;
                            TeleportMe3 = true;
                            NPC.Center = tele;
                            NPC.netOffset = Vector2.Zero;
                            NPC.netUpdate = true;
                        }
                    }
                    else
                    {
                        NPC.alpha -= 3;
                        if (NPC.alpha <= 0)
                        {
                            NPC.alpha = 0;
                        }
                    }
                }
            }

            for (int m = NPC.oldPos.Length - 1; m > 0; m--)
            {
                NPC.oldPos[m] = NPC.oldPos[m - 1];
            }
            NPC.oldPos[0] = NPC.position;

            bool foundTarget = TargetClosest();
            if (playerTarget != null && WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased))
                playerTarget.AddBuff(ModContent.BuffType<HydraBody_Hunted>(), 10, true);

            if (!runningAway && foundTarget)
            {
                if (Math.Abs(NPC.velocity.X) > 12f) NPC.velocity.X *= 0.8f;
                if (Math.Abs(NPC.velocity.Y) > 12f) NPC.velocity.Y *= 0.8f;
                if (NPC.velocity.Y > 7f) NPC.velocity.Y *= 0.75f;
                NPC.timeLeft = 50;
                AIMovementNormal();
            }
            else
            {
                runningAway = true;
                AIMovementRunAway();
                return;
            }
            
            bool noHeads = !NPC.AnyNPCs(ModContent.NPCType<HydraHead1>()) && !NPC.AnyNPCs(ModContent.NPCType<HydraHead2>()) && !NPC.AnyNPCs(ModContent.NPCType<HydraHead3>()) &&
                !NPC.AnyNPCs(ModContent.NPCType<HydraHead4>()) && !NPC.AnyNPCs(ModContent.NPCType<HydraHead5>()) && !NPC.AnyNPCs(ModContent.NPCType<HydraHead6>()) &&
                !NPC.AnyNPCs(ModContent.NPCType<HydraHead7>()) && !NPC.AnyNPCs(ModContent.NPCType<HydraHead8>()) && !NPC.AnyNPCs(ModContent.NPCType<HydraHead9>());

            if (HeadsSpawned && noHeads)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.life = 0;
                    NPC.checkDead();
                    NPC.netUpdate = true;
                }
                return;
            }
        }

        public float[] internalAI = new float[1];

        public int[] headindex = {-1, -1, -1};
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(internalAI[0]);
                writer.Write(headindex[0]);
                writer.Write(headindex[1]);
                writer.Write(headindex[2]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                internalAI[0] = reader.ReadSingle();
                headindex[0] = reader.ReadInt32();
                headindex[1] = reader.ReadInt32();
                headindex[2] = reader.ReadInt32();
            }
        }

        public override void FindFrame(int frameHeight)
        {
            if (NPC.velocity.X != 0)
                NPC.spriteDirection = NPC.velocity.X > 0 ? 1 : -1;

            NPC.frameCounter--;
            if (NPC.frameCounter <= 0)
            {
                NPC.frameCounter = 5;
                NPC.frame.Y += frameHeight;
                if (NPC.frame.Y > frameHeight * 14)
                {
                    NPC.frame.Y = frameHeight * 2;
                }
            }
            if (NPC.velocity.X == 0)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y = 0;
            }
            if (NPC.velocity.Y != 0)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y = frameHeight;
            }
        }

        public void AIMovementRunAway()
        {
            NPC.alpha += 2;
            if (Main.netMode != NetmodeID.MultiplayerClient) internalAI[0] += 2;
            if (internalAI[0] >= 255)
            {
                NPC.active = false;
                NPC.netUpdate = true;
            }
        }

        public void AIMovementNormal()
        {
            BaseAI.AIZombie(NPC, ref NPC.ai, false, false, -1, 0.07f, 3f, 14, 20, 1, true, 1, 1, true, null, false);
            NPC.rotation = 0f;
        }

        public bool TargetClosest()
        {
            int[] players = BaseAI.GetPlayers(NPC.Center, 2000f);
            float dist = 999999;
            int foundPlayer = -1;
            if (foundPlayer != -1)
            {
                BaseAI.SetTarget(NPC, foundPlayer);
                playerTarget = Main.player[foundPlayer];
                return true;
            }
            else
            {
                for (int m = 0; m < players.Length; m++)
                {
                    Player p = Main.player[players[m]];
                    if (Vector2.Distance(p.Center, NPC.Center) < dist)
                    {
                        dist = Vector2.Distance(p.Center, NPC.Center);
                        foundPlayer = p.whoAmI;
                    }
                }
            }
            if (foundPlayer != -1)
            {
                BaseAI.SetTarget(NPC, foundPlayer);
                playerTarget = Main.player[foundPlayer];
                return true;
            }
            return false;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && !Main.dedServ)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("HydraGoreBody").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("HydraGoreLeg").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("HydraGoreLeg").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("HydraGoreLeg").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("HydraGoreLeg").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("HydraGoreTail").Type, 1f);
            }
        }
        private static float X(float t,
        float x0, float x1, float x2)
        {
            return (float)(
                x0 * Math.Pow(1 - t, 2) +
                x1 * 2 * t * Math.Pow(1 - t, 1) +
                x2 * Math.Pow(t, 2)
            );
        }
        private static float Y(float t,
            float y0, float y1, float y2)
        {
            return (float)(
                 y0 * Math.Pow(1 - t, 2) +
                 y1 * 2 * t * Math.Pow(1 - t, 1) +
                 y2 * Math.Pow(t, 2)
             );
        }
        public void DrawHead(SpriteBatch spriteBatch, string headTexture, string glowMaskTexture, Vector2 drawPos, Rectangle drawFrame, float drawRot, Color drawColor)
        {
            string neckTex = Texture + "_Neck";
            Texture2D neckTex2D = ModContent.Request<Texture2D>(neckTex).Value;
            Vector2 neckOrigin = new Vector2(NPC.Center.X, NPC.Center.Y - 30 * NPC.scale) - (NPC.IsABestiaryIconDummy ? Vector2.Zero : Main.screenPosition);
            float chainsPerUse = 0.05f * NPC.scale;
            for (float i = 0; i <= 1; i += chainsPerUse)
            {
                Vector2 distBetween;
                float projTrueRotation;
                if (i != 0)
                {
                    distBetween = new Vector2(
                    X(i, neckOrigin.X, (neckOrigin.X + drawPos.X) / 2, drawPos.X) -
                    X(i - chainsPerUse, neckOrigin.X, (neckOrigin.X + drawPos.X) / 2, drawPos.X),
                    Y(i, neckOrigin.Y, neckOrigin.Y + 50, drawPos.Y) -
                    Y(i - chainsPerUse, neckOrigin.Y, neckOrigin.Y + 50, drawPos.Y));
                    projTrueRotation = distBetween.ToRotation() - (float)Math.PI / 2;

                    Vector2 chainPos = new(X(i, neckOrigin.X, (neckOrigin.X + drawPos.X) / 2, drawPos.X), Y(i, neckOrigin.Y, neckOrigin.Y + 50, drawPos.Y));
                    spriteBatch.Draw(neckTex2D, chainPos, null, drawColor, projTrueRotation, neckTex2D.Size() * 0.5f, NPC.scale, 0, 0);
                }
            }
            Texture2D headTex = ModContent.Request<Texture2D>(headTexture).Value;
            Texture2D glowTex = ModContent.Request<Texture2D>(glowMaskTexture).Value;
            spriteBatch.Draw(headTex, drawPos, drawFrame, drawColor, drawRot, drawFrame.Size() * 0.5f, NPC.scale, SpriteEffects.None, 0f);
            spriteBatch.Draw(glowTex, drawPos, drawFrame, Color.White, drawRot, drawFrame.Size() * 0.5f, NPC.scale, SpriteEffects.None, 0f);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            drawColor = NPC.GetAlpha(drawColor);

            int frameWidth = 152;
            frameBottom = BaseDrawing.GetFrame(0, frameWidth, 44, 0, 2);

            HeadDraw(spriteBatch, drawColor);

            string tailTex = Texture + "_Tail";
            spriteBatch.Draw(ModContent.Request<Texture2D>(tailTex).Value, NPC.Center + new Vector2(0f, NPC.gfxOffY - (30 * NPC.scale)) - screenPos, NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.SpriteEffectDirection(), 0);
            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center + new Vector2(0f, NPC.gfxOffY) - screenPos, NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.SpriteEffectDirection(), 0);

            string headTex = Texture.Replace("Body", "Head") + "1";
            if (NPC.IsABestiaryIconDummy)
            {
                Rectangle frame = ModContent.Request<Texture2D>(headTex).Frame(1, 2);
                DrawHead(spriteBatch, headTex, headTex + "_Glow", NPC.Center + new Vector2(0, -110) * NPC.scale, frame, 0, drawColor); //draw main head last!
            }
            else if (Head1 != null && Head1.active && Head1.type == ModContent.NPCType<HydraHead1>())
            {
                DrawHead(spriteBatch, headTex, headTex + "_Glow", Head1.Center - Main.screenPosition, Head1.frame, Head1.rotation, drawColor); //draw main head last!
            }
            return false;
        }

        public void HeadDraw(SpriteBatch sb, Color drawColor)
        {
            string headTex = Texture.Replace("Body", "Head");

            if(NPC.IsABestiaryIconDummy)
            {
                Rectangle frame = ModContent.Request<Texture2D>(headTex + "2").Frame(1, 2);
                bool small = NPC.scale == 0.65f;
                DrawHead(sb, headTex + "2", headTex + "2_Glow", NPC.Center + new Vector2(small ? 36 : 80, -90) * NPC.scale, frame, 0, drawColor);

                DrawHead(sb, headTex + "3", headTex + "3_Glow", NPC.Center + new Vector2(small ? -36 : -80, -90) * NPC.scale, frame, 0, drawColor);
            }

            if (Head2 != null && Head2.active && Head2.type == ModContent.NPCType<HydraHead2>())
            {
                DrawHead(sb, headTex + "2", headTex + "2_Glow", Head2.Center - Main.screenPosition, Head2.frame, Head2.rotation, drawColor);
            }

            if (Head3 != null && Head3.active && Head3.type == ModContent.NPCType<HydraHead3>())
            {
                DrawHead(sb, headTex + "3", headTex + "3_Glow", Head3.Center - Main.screenPosition, Head3.frame, Head3.rotation, drawColor);
            }

            if (Head4 != null && Head4.active && Head4.type == ModContent.NPCType<HydraHead4>())
            {
                DrawHead(sb, headTex + "4", headTex + "4_Glow", Head4.Center - Main.screenPosition, Head4.frame, Head4.rotation, drawColor);
            }

            if (Head5 != null && Head5.active && Head5.type == ModContent.NPCType<HydraHead5>())
            {
                DrawHead(sb, headTex + "5", headTex + "5_Glow", Head5.Center - Main.screenPosition, Head5.frame, Head5.rotation, drawColor);
            }

            if (Head6 != null && Head6.active && Head6.type == ModContent.NPCType<HydraHead6>())
            {
                DrawHead(sb, headTex + "6", headTex + "6_Glow", Head6.Center - Main.screenPosition, Head6.frame, Head6.rotation, drawColor);
            }

            if (Head7 != null && Head7.active && Head7.type == ModContent.NPCType<HydraHead7>())
            {
                DrawHead(sb, headTex + "7", headTex + "5_Glow", Head7.Center - Main.screenPosition, Head7.frame, Head7.rotation, drawColor);
            }

            if (Head8 != null && Head8.active && Head8.type == ModContent.NPCType<HydraHead8>())
            {
                DrawHead(sb, headTex + "8", headTex + "4_Glow", Head8.Center - Main.screenPosition, Head8.frame, Head8.rotation, drawColor);
            }

            if (Head9 != null && Head9.active && Head9.type == ModContent.NPCType<HydraHead9>())
            {
                DrawHead(sb, headTex + "9", headTex + "6_Glow", Head9.Center - Main.screenPosition, Head9.frame, Head9.rotation, drawColor);
            }
        }
    }

    public class HydraBossBar : ModBossBar
    {
        private Asset<Texture2D> _icon = ModContent.Request<Texture2D>("AAModClassic/_Content/Mire/___PreHardmode/NPCs/__BossHydra/HydraHead1_Head_Boss");
        public override bool? ModifyInfo(ref BigProgressBarInfo info, ref float life, ref float lifeMax, ref float shield, ref float shieldMax)
        {

            if (Main.npc.Length < info.npcIndexToAimAt)
                return false;

            NPC npc = Main.npc[info.npcIndexToAimAt];

            if (npc == null || !npc.active)
                return false;

            if (npc.ModNPC is not HydraBody Body)
                return false;

            // set value to default value
            life = 0;
            lifeMax = 0;

            if (Body.Head1 == null)
                return false;

            // lifemax will be calculate here
            lifeMax = Body.Head1.lifeMax * 9;

            // combine all head's life
            NPC[] heads = [Body.Head1, Body.Head2, Body.Head3, Body.Head4, Body.Head5, Body.Head6, Body.Head7, Body.Head8, Body.Head9];

            foreach (var head in heads)
            {
                if (head == null)
                {
                    life += Body.Head1.lifeMax;
                    continue;
                }

                if (head.life >= 0)
                    life += head.life;
            }

            return true;
        }

        public override Asset<Texture2D> GetIconTexture(ref Rectangle? iconFrame) => _icon;
    }
}