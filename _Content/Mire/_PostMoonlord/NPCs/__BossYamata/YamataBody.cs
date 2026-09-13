using AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.Ammo;
using AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.BossStandard;
using AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.Tools;
using AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.Weapons;
using AAModClassic._Content.Mire._PostMoonlord.Items.Materials;
using AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata.Awakened;
using AAModClassic._Content.Mire.World.Biomes;
using AAModClassic._CrossMod.CalamityMod.LoreItems;
using AAModClassic._Removed.Content._Tinker._PostMoonlord.Items.Accessories;
using AAModClassic.Achievements;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Music;
using AAModClassic.UI.Titles;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.NPCs;
using AAModClassic.Utilities.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items.AAConditions;

namespace AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata
{
    [AutoloadBossHead]
    public class YamataBody : YamataBoss
    {
        public NPC TrueHead;
        public NPC Head2;
        public NPC Head3;
        public NPC Head4;
        public NPC Head5;
        public NPC Head6;
        public NPC Head7;
        public bool HeadsSpawned = false;
        private bool quarterHealth = false;
        private bool threeQuarterHealth = false;
        private bool HalfHealth = false;
        public bool loludide = false;
        public bool flag;

        public static Asset<Texture2D> HeadTex; //Different cause ModNPC has HeadTexture...
        public static Asset<Texture2D> HeadF1Texture;
        public static Asset<Texture2D> HeadF2Texture;
        public static Asset<Texture2D> HeadGlowTexture;
        public static Asset<Texture2D> HeadF1GlowTexture;
        public static Asset<Texture2D> HeadF2GlowTexture;
        public static Asset<Texture2D> NeckTexture;
        public static Asset<Texture2D> TailTexture;


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

        public override void SetStaticDefaults()
        {
            //displayName = "Yamata";

            NPCID.Sets.NPCBestiaryDrawModifiers value = new()
            {
                Scale = 0.7f,
                PortraitScale = 0.8f,
                PortraitPositionYOverride = 64,
                Position = new(0, 72)
            };
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
            NPCID.Sets.BossBestiaryPriority.Add(Type);

            if (!Main.dedServ)
            {
                HeadTex = ModContent.Request<Texture2D>(Texture.Replace("Body", "") + "Head");
                HeadF1Texture = ModContent.Request<Texture2D>(Texture.Replace("Body", "") + "HeadFake1");
                HeadF2Texture = ModContent.Request<Texture2D>(Texture.Replace("Body", "") + "HeadFake2");
                HeadGlowTexture = ModContent.Request<Texture2D>(Texture.Replace("Body", "") + "Head_Glow");
                HeadF1GlowTexture = ModContent.Request<Texture2D>(Texture.Replace("Body", "") + "HeadFake1_Glow");
                HeadF2GlowTexture = ModContent.Request<Texture2D>(Texture.Replace("Body", "") + "HeadFake2_Glow");

                string texRoot = Texture + "_";
                NeckTexture = ModContent.Request<Texture2D>(texRoot + "Neck");
                TailTexture = ModContent.Request<Texture2D>(texRoot + "Tail");

                LegInfo.normalTextures = new Asset<Texture2D>[5];
                LegInfo.normalTextures[0] = ModContent.Request<Texture2D>(texRoot + "LegCapL");
                LegInfo.normalTextures[1] = ModContent.Request<Texture2D>(texRoot + "LegSegmentL");
                LegInfo.normalTextures[2] = ModContent.Request<Texture2D>(texRoot + "LegCapR");
                LegInfo.normalTextures[3] = ModContent.Request<Texture2D>(texRoot + "LegSegmentR");
                LegInfo.normalTextures[4] = ModContent.Request<Texture2D>(texRoot + "Foot");

                texRoot = ModContent.GetInstance<YamataABody>().Texture + "_";
                LegInfo.awakenedTextures = new Asset<Texture2D>[5];
                LegInfo.awakenedTextures[0] = ModContent.Request<Texture2D>(texRoot + "LegCapL");
                LegInfo.awakenedTextures[1] = ModContent.Request<Texture2D>(texRoot + "LegSegmentL");
                LegInfo.awakenedTextures[2] = ModContent.Request<Texture2D>(texRoot + "LegCapR");
                LegInfo.awakenedTextures[3] = ModContent.Request<Texture2D>(texRoot + "LegSegmentR");
                LegInfo.awakenedTextures[4] = ModContent.Request<Texture2D>(texRoot + "Foot");
            }
        }

        public override void SetDefaults()
        {
            NPC.npcSlots = 100;
            NPC.width = 80;
            NPC.height = 90;
            NPC.aiStyle = -1;
            NPC.damage = 0;
            NPC.lifeMax = 400000;
            NPC.value = Item.buyPrice(0, 30, 0, 0);
            NPC.defense = 999999;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            Music = MusicManagementSystem.MusicSlots["Yamata"];
            SceneEffectPriority = SceneEffectPriority.BossHigh;
            NPC.noGravity = true;
            NPC.netAlways = true;
            frameWidth = 162;
            frameHeight = 118;
            if(!NPC.IsABestiaryIconDummy)
                NPC.alpha = 255;
            NPC.frame = BaseDrawing.GetFrame(frameCount, frameWidth, frameHeight, 0, 2);
            frameBottom = BaseDrawing.GetFrame(frameCount, frameWidth, 54, 0, 2);
            frameHead = BaseDrawing.GetFrame(frameCount, frameWidth, 118, 0, 2);
            NPC.DeathSound = new SoundStyle("AAModClassic/Sounds/YamataRoar");
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
            NPC.chaseable = false;
            SpawnModBiomes = [ModContent.GetInstance<MireBiome>().Type];

            NPC.BossBar = Main.BigBossProgressBar.NeverValid;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
                new FlavorTextBestiaryInfoElement("Mods.AAModClassic.Bestiary.Yamata")
            ]);
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.5f * balance);
        }

        public override void BossLoot(ref int potionType)
        {
            if (!Main.expertMode)
            potionType = ItemID.SuperHealingPotion;
            else
            {
                potionType = 0;
            }
        }

        public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
        {
            modifiers.TargetDamageMultiplier *= 0;

            if (!AAWorld.downedYamata)
            {
                if (NPC.life <= NPC.lifeMax / 4 * 3 && threeQuarterHealth == false)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Yamata.Health.First.ThreeQuarters"), AAColor.YamataDialogue);
                    threeQuarterHealth = true;
                }
                if (NPC.life <= NPC.lifeMax / 2 && HalfHealth == false)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Yamata.Health.First.Half"), AAColor.YamataDialogue);
                    HalfHealth = true;
                }
                if (NPC.life <= NPC.lifeMax / 4 && quarterHealth == false)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Yamata.Health.First.Quarter"), AAColor.YamataDialogue);
                    quarterHealth = true;
                }
            }
            else
            {
                if (NPC.life <= NPC.lifeMax / 4 * 3 && threeQuarterHealth == false)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Yamata.Health.Repeat.ThreeQuarters"), AAColor.YamataDialogue);
                    threeQuarterHealth = true;
                }
                if (NPC.life <= NPC.lifeMax / 2 && HalfHealth == false)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Yamata.Health.Repeat.Half"), AAColor.YamataDialogue);
                    HalfHealth = true;
                }
                if (NPC.life <= NPC.lifeMax / 4 && quarterHealth == false)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Yamata.Health.Repeat.Quarter"), AAColor.YamataDialogue);
                    quarterHealth = true;
                }
            }
        }

        public bool Dead = false;

        public override bool PreKill()
        {
            if (Main.expertMode)
                NPC.boss = false;
            return true;
        }

        public override void OnKill()
        {
            Dead = true;

            if (!Main.expertMode)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Yamata.Defeat.NotExpert"), AAColor.YamataDialogue);
                if (!NPC.BeenKilled(true))
                {
                    NPC.OnGameEventClearedForTheFirstTime(-1);
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Yamata.Defeat.Status"), Color.Indigo);
                }

                if (NPC.playerInteraction[Main.myPlayer])
                    YamataKilled.Condition.Complete();
            }
            if (Main.expertMode)
            {
                int npcID = NPC.NewNPC(NPC.GetSource_Death(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<YamataTransition>(), 0, 0, 0, 0, 0, NPC.target);
                Main.npc[npcID].Center = NPC.Center;
                Main.npc[npcID].netUpdate2 = true; Main.npc[npcID].netUpdate = true;
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<YamataTrophy>(), 10));

            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<YamataMask>(), 7));

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<DreadScale>(), 1, 20, 30));

            int[] lootTable = { ModContent.ItemType<Flairdra>(), ModContent.ItemType<Crescent>(), ModContent.ItemType<Amenomuraku>(), ModContent.ItemType<EventideArrow>(), ModContent.ItemType<HydraStabber>(), ModContent.ItemType<MidnightWrath>(), ModContent.ItemType<AbyssalYari>(), ModContent.ItemType<AbyssalBomb>(), ModContent.ItemType<AbyssalEruption>(), ModContent.ItemType<Darksprayer>(), ModContent.ItemType<FallingTwilight>(), ModContent.ItemType<Sevenshot>(), ModContent.ItemType<ThrowingCrescent>(), ModContent.ItemType<DreadTerratool>() };

            notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, lootTable));

            LeadingConditionRule loreCondition = new(new LoreItemDropCondition<YamataBody>());
            notExpertRule.OnSuccess(loreCondition.OnSuccess(new PerPlayerDropRule(ModContent.ItemType<YamataLore>(), 1)));

            npcLoot.Add(notExpertRule);

            LeadingConditionRule anceintsDownAndRemoved = new(new PostLateAncientsAndRemovedWorldAndNotExpert());

            anceintsDownAndRemoved.OnSuccess(ItemDropRule.Common(ModContent.ItemType<SpaceStone>(), 50));

            npcLoot.Add(anceintsDownAndRemoved);
        }

        public int playerTooFarDist = 800;
        public Rectangle frameBottom = new Rectangle(0, 0, 1, 1), frameHead = new Rectangle(0, 0, 1, 1);
        public bool prevHalfHPLeft = false, halfHPLeft = false, prevFourthHPLeft = false, fourthHPLeft = false;
        public Player playerTarget = null;
        public static int flyingTileCount = 6, totalMinionCount = 0;
        public int MinionTimer = 0;

        //clientside stuff
        public Vector2 bottomVisualOffset = default;
        public LegInfo[] legs = null;
        public IKLeg[] unofficialLegs = null;
        public bool[] headsSaidOw = new bool[7];
        public bool TeleportMe1 = false;
        public bool TeleportMe2 = false;
        public bool TeleportMe3 = false;
        public bool TeleportMe4 = false;
        public bool TeleportMe5 = false;
        public bool TeleportMe6 = false;
        public static bool TeleportMeBitch = false;

        public int SayTheLineYamata = 300;
        public bool FirstLine = false;
        public bool NoFly4U = false;
        public int NoFlyCountDown = 60;

        public void HandleHeads()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (!HeadsSpawned)
                {
                    TrueHead = Main.npc[NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<YamataHead>(), 0)];
                    TrueHead.ai[0] = NPC.whoAmI;
                    Head2 = Main.npc[NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<YamataHeadFake1>(), 0)];
                    Head2.ai[0] = NPC.whoAmI;
                    Head3 = Main.npc[NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<YamataHeadFake1>(), 0)];
                    Head3.ai[0] = NPC.whoAmI;
                    Head4 = Main.npc[NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<YamataHeadFake1>(), 0)];
                    Head4.ai[0] = NPC.whoAmI;
                    Head5 = Main.npc[NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<YamataHeadFake2>(), 0)];
                    Head5.ai[0] = NPC.whoAmI;
                    Head6 = Main.npc[NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<YamataHeadFake2>(), 0)];
                    Head6.ai[0] = NPC.whoAmI;
                    Head7 = Main.npc[NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<YamataHeadFake2>(), 0)];
                    Head7.ai[0] = NPC.whoAmI;

                    TrueHead.netUpdate = true;
                    Head2.netUpdate = true;
                    Head3.netUpdate = true;
                    Head4.netUpdate = true;
                    Head5.netUpdate = true;
                    Head6.netUpdate = true;
                    Head7.netUpdate = true;
                    HeadsSpawned = true;
                }
            }
            else
            {
                //the AI[0] checks are so when this is fargo'd into a multispawn it doesn't try to attach all the heads to one enemy if they are too close together.
                if (!HeadsSpawned)
                {
                    int[] npcs = BaseAI.GetNPCs(NPC.Center, -1, default, 1000f, null);
                    if (npcs != null && npcs.Length > 0)
                    {
                        foreach (int npcID in npcs)
                        {
                            NPC npc2 = Main.npc[npcID];
                            if (npc2 != null)
                            {
                                if (TrueHead == null && npc2.type == ModContent.NPCType<YamataHead>() && npc2.ai[0] == NPC.whoAmI)
                                TrueHead = npc2;
                                else
                                if (Head2 == null && npc2.type == ModContent.NPCType<YamataHeadFake1>() && npc2.ai[0] == NPC.whoAmI)
                                Head2 = npc2;
                                else
                                if (Head3 == null && npc2.type == ModContent.NPCType<YamataHeadFake1>() && npc2.ai[0] == NPC.whoAmI)
                                Head3 = npc2;
                                else
                                if (Head4 == null && npc2.type == ModContent.NPCType<YamataHeadFake1>() && npc2.ai[0] == NPC.whoAmI)
                                Head4 = npc2;
                                else
                                if (Head5 == null && npc2.type == ModContent.NPCType<YamataHeadFake2>() && npc2.ai[0] == NPC.whoAmI)
                                Head5 = npc2;
                                else
                                if (Head6 == null && npc2.type == ModContent.NPCType<YamataHeadFake2>() && npc2.ai[0] == NPC.whoAmI)
                                Head6 = npc2;
                                else
                                if (Head7 == null && npc2.type == ModContent.NPCType<YamataHeadFake2>() && npc2.ai[0] == NPC.whoAmI)
                                Head7 = npc2;
                            }
                        }
                    }
                    if (TrueHead != null && Head2 != null && Head3 != null && Head4 != null && Head5 != null && Head6 != null && Head7 != null)
                    HeadsSpawned = true;
                }
            }
        }

        public override void AI()
        {
            NPC.GetGlobalNPC<TitleGlobalNPC>().ShowTitle = true;

            TargetClosest();
            HandleHeads();

            if (SayTheLineYamata <= 0)
            SayTheLineYamata = 300;

            if (Main.dayTime)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient && !flag)
                {
                    flag = true;
                    ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Yamata.Despawn.Daytime"), AAColor.YamataDialogue);
                }
                NPC.alpha += 10;
                if (NPC.alpha >= 255)
                NPC.active = false;
                return;
            }

            prevHalfHPLeft = halfHPLeft;
            prevFourthHPLeft = fourthHPLeft;
            halfHPLeft = halfHPLeft || NPC.life <= NPC.lifeMax / 2;
            fourthHPLeft = fourthHPLeft || NPC.life <= NPC.lifeMax / 4;

            for (int m = NPC.oldPos.Length - 1; m > 0; m--)
            {
                NPC.oldPos[m] = NPC.oldPos[m - 1];
            }
            NPC.oldPos[0] = NPC.position;

            bool foundTarget = TargetClosest();
            if (foundTarget)
            {
                for (int p = 0; p < Main.maxPlayers; p++)
                {
                    Player t = Main.player[p];
                    if (t.active && !t.dead)
                    Main.player[p].AddBuff(ModContent.BuffType<YamataBody_AbyssalGravity>(), 10, true);
                }
                NoFlyCountDown--;
                if (!NoFly4U && NoFlyCountDown <= 0 && !AAWorld.downedYamata)
                {
                    NoFlyCountDown = 0;
                    NoFly4U = true;

                    if (NPC.type == ModContent.NPCType<YamataBody>()) if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Yamata.NoFly"), AAColor.YamataDialogue);
                }

                float dist = NPC.Distance(playerTarget.Center);
                if (dist > 1200 || !Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient && SayTheLineYamata == 300)
                    {
                        if (!FirstLine)
                        {
                            if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Yamata.Teleport"), AAColor.YamataDialogue);
                            FirstLine = true;
                        }
                    }
                    SayTheLineYamata--;
                    NPC.alpha += 3;
                    if (NPC.alpha >= 255)
                    {
                        NPC.alpha = 255;
                        Vector2 tele = playerTarget.Center + new Vector2(0, -200);// +  (playerTarget.velocity == new Vector2(0,0)? new Vector2(0,0) : Vector2.Normalize(playerTarget.velocity) * playerTarget.velocity.Length() * 54.33f);
                        TeleportMe1 = true;
                        TeleportMe2 = true;
                        TeleportMe3 = true;
                        TeleportMe4 = true;
                        TeleportMe5 = true;
                        TeleportMe6 = true;
                        TeleportMeBitch = true;
                        NPC.Center = tele;
                        NPC.netOffset = Vector2.Zero;
                        NPC.dontTakeDamage = true;
                        TrueHead.dontTakeDamage = true;
                        Head2.dontTakeDamage = true;
                        Head3.dontTakeDamage = true;
                        Head4.dontTakeDamage = true;
                        Head5.dontTakeDamage = true;
                        Head6.dontTakeDamage = true;
                        Head7.dontTakeDamage = true;
                    }
                }
                else
                {
                    NPC.alpha -= 8;
                    SayTheLineYamata = 300;
                    if (NPC.alpha <= 0)
                    {
                        NPC.dontTakeDamage = false;
                        TrueHead.dontTakeDamage = false;
                        Head2.dontTakeDamage = false;
                        Head3.dontTakeDamage = false;
                        Head4.dontTakeDamage = false;
                        Head5.dontTakeDamage = false;
                        Head6.dontTakeDamage = false;
                        Head7.dontTakeDamage = false;
                        NPC.alpha = 0;
                    }
                }
                NPC.timeLeft = 300;
                float playerDistance = Vector2.Distance(playerTarget.Center, NPC.Center);
                if (playerDistance < playerTooFarDist - 100f && Math.Abs(NPC.velocity.X) > 12f) NPC.velocity.X *= 0.8f;
                if (playerDistance < playerTooFarDist - 100f && Math.Abs(NPC.velocity.Y) > 12f) NPC.velocity.Y *= 0.8f;
                if (NPC.velocity.Y > 7f) NPC.velocity.Y *= 0.75f;
                AIMovementNormal(playerDistance);
            }
            else
            {
                AIMovementRunAway();
            }
            bottomVisualOffset = new Vector2(Math.Min(3f, Math.Abs(NPC.velocity.X)), 0f) * (NPC.velocity.X < 0 ? 1 : -1);
            UpdateLimbs();
        }

        public void AIMovementRunAway()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient && !loludide)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Yamata.Kill"), AAColor.YamataDialogue);
                loludide = true;
            }

            NPC.alpha += 10;
            if (NPC.alpha >= 255)
            NPC.active = false;
        }

        public void AIMovementNormal(float playerDistance)
        {
            bool playerTooFar = playerDistance > playerTooFarDist;
            HandleYamataBody(NPC, ref NPC.ai, true, 0.2f, 3.5f, 8f, 0.07f, 1.5f, 4);
            if (playerTooFar) 
                NPC.position += playerTarget.position - playerTarget.oldPosition;
            NPC.rotation = 0f;
        }

        public static void HandleYamataBody(NPC npc, ref float[] ai, bool ignoreWet = true, float moveInterval = 0.2f, float maxSpeedX = 2f, float maxSpeedY = 1.5f, float hoverInterval = 0.04f, float hoverMaxSpeed = 1.5f, int hoverHeight = 3)
        {
            bool flyUpward = false;
            if (npc.justHit)
                ai[2] = 0f;

            if (ai[2] >= 0f)
            {
                int tileDist = 16;
                bool inRangeX = false;
                bool inRangeY = false;
                if (npc.position.X > ai[0] - tileDist && npc.position.X < ai[0] + tileDist) 
                    inRangeX = true;
                else if (npc.velocity.X < 0f && npc.direction > 0 || npc.velocity.X > 0f && npc.direction < 0)
                    inRangeX = true;
                tileDist += 24;
                if (npc.position.Y > ai[1] - tileDist && npc.position.Y < ai[1] + tileDist)
                    inRangeY = true;
                if (inRangeX && inRangeY)
                {
                    ai[2] += 1f;
                    if (ai[2] >= 30f && tileDist == 16)
                    flyUpward = true;
                    if (ai[2] >= 60f)
                    ai[2] = 0f;
                }
                else
                {
                    ai[0] = npc.position.X;
                    ai[1] = npc.position.Y;
                    ai[2] = 0f;
                }
                npc.TargetClosest(true);
            }
            else
            {
                ai[2] += 1f;
                if (Main.player[npc.target].position.X + Main.player[npc.target].width / 2 > npc.position.X + npc.width / 2)
                npc.direction = -1;
                else
                {
                    npc.direction = 1;
                }
            }

            int tileX = (int)(npc.Center.X / 16f) + npc.direction * 2;
            int tileY = (int)((npc.position.Y + npc.height) / 16f);
            bool tileBelowEmpty = true;

            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) && npc.target > -1)
            {
                if (npc.Center.Y < Main.player[npc.target].Center.Y - 48)
                    npc.directionY = 1;
                else
                    npc.directionY = -1;
            }

            for (int tY = tileY; tY < tileY + hoverHeight; tY++)
            {
                if (Main.tile[tileX, tY] == null)
                    continue;
                if (Main.tile[tileX, tY].HasUnactuatedTile && Main.tileSolid[Main.tile[tileX, tY].TileType] || Main.tile[tileX, tY].LiquidAmount > 0)
                {
                    tileBelowEmpty = false;
                    break;
                }
            }
            if (flyUpward)
                tileBelowEmpty = true;

            if (tileBelowEmpty)
            {
                npc.velocity.Y += moveInterval;
                if (npc.velocity.Y > 9f)
                npc.velocity.Y = 9f;
            }
            else
            {
                if (npc.directionY < 0 && npc.velocity.Y > 0f) npc.velocity.Y -= moveInterval;
                if (npc.velocity.Y < -maxSpeedY) npc.velocity.Y = -maxSpeedY;
            }

            if (!ignoreWet && npc.wet)
            {
                npc.velocity.Y -= moveInterval;
                if (npc.velocity.Y < -maxSpeedY * 0.75f) npc.velocity.Y = -maxSpeedY * 0.75f;
            }


            if (npc.collideY)
            {
                npc.velocity.Y = npc.oldVelocity.Y * -0.25f;
                if (npc.velocity.Y > 0f && npc.velocity.Y < 1f) npc.velocity.Y = 1f;
                if (npc.velocity.Y < 0f && npc.velocity.Y > -1f) npc.velocity.Y = -1f;
            }

            if (!tileBelowEmpty && npc.target > -1 && Main.player[npc.target].active && !Main.player[npc.target].dead && Math.Abs(Main.player[npc.target].Center.X - npc.Center.X) < 50) //force a hover
            {
                if (Math.Abs(npc.velocity.X) > 0.3f) npc.velocity.X *= 0.9f;
                if (Math.Abs(npc.velocity.Y) > 0.3f) npc.velocity.Y *= 0.9f;
            }
            else if (npc.direction == -1 && npc.velocity.X > -maxSpeedX)
            {
                npc.velocity.X -= moveInterval * 0.5f;
                if (npc.velocity.X > maxSpeedX) npc.velocity.X -= 0.1f;
                else
                    if (npc.velocity.X > 0f) npc.velocity.X += 0.05f;
                if (npc.velocity.X < -maxSpeedX) npc.velocity.X = -maxSpeedX;
            }
            else if (npc.direction == 1 && npc.velocity.X < maxSpeedX)
            {
                npc.velocity.X += moveInterval * 0.5f;
                if (npc.velocity.X < -maxSpeedX) 
                    npc.velocity.X += 0.1f;
                else if (npc.velocity.X < 0f) 
                    npc.velocity.X -= 0.05f;

                if (npc.velocity.X > maxSpeedX) 
                    npc.velocity.X = maxSpeedX;
            }


            if (npc.directionY == -1 && (double)npc.velocity.Y > -hoverMaxSpeed)
            {
                npc.velocity.Y -= hoverInterval;
                if ((double)npc.velocity.Y > hoverMaxSpeed)
                    npc.velocity.Y -= 0.05f;
                else if (npc.velocity.Y > 0f) 
                    npc.velocity.Y += hoverInterval - 0.01f;

                if ((double)npc.velocity.Y < -hoverMaxSpeed)
                    npc.velocity.Y = -hoverMaxSpeed;
            }
            else if (npc.directionY == 1 && (double)npc.velocity.Y < hoverMaxSpeed)
            {
                npc.velocity.Y += hoverInterval;
                if ((double)npc.velocity.Y < -hoverMaxSpeed) 
                    npc.velocity.Y += 0.05f;
                else if (npc.velocity.Y < 0f) 
                    npc.velocity.Y -= hoverInterval - 0.01f;

                if ((double)npc.velocity.Y > hoverMaxSpeed) 
                    npc.velocity.Y = hoverMaxSpeed;
            }

            
        }

        public bool TargetClosest()
        {
            int[] players = BaseAI.GetPlayers(NPC.Center, 4200f);
            float dist = 999999999f;
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
            scale *= 2;
            return true;
        }

        public void UpdateLimbs()
        {
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                if (unofficialLegs == null)
                {
                    unofficialLegs = new IKLeg[4];
                    unofficialLegs[0] = new(NPC, new(-60, -10), 100, 80, false, true, -7, 56, 0.3f); //Back Left
                    unofficialLegs[1] = new(NPC, new(60, -10), 100, 80, false, false, -7, 56, 0.3f); //Back Right
                    unofficialLegs[2] = new(NPC, new(-20, -10), 90, 80, true, true, -7, 56, 0.3f); //Front Left
                    unofficialLegs[3] = new(NPC, new(20, -10), 90, 80, true, false, -7, 56, 0.3f); //Front Right        

                    unofficialLegs[0].PairedLeg = unofficialLegs[3];
                    unofficialLegs[3].PairedLeg = unofficialLegs[0];

                    unofficialLegs[0].SisterLeg = unofficialLegs[2];
                    unofficialLegs[2].SisterLeg = unofficialLegs[0];

                    unofficialLegs[1].PairedLeg = unofficialLegs[2];
                    unofficialLegs[2].PairedLeg = unofficialLegs[1];

                    unofficialLegs[1].SisterLeg = unofficialLegs[3];
                    unofficialLegs[3].SisterLeg = unofficialLegs[1];
                }

                foreach (IKLeg leg in unofficialLegs)
                    leg.Update(unofficialLegs.ToList());
            }
            else
            {
                if (legs == null || legs.Length < 4)
                {
                    legs = new LegInfo[4];
                    legs[0] = new LegInfo(0, NPC.Bottom + new Vector2(60, 0), false);
                    legs[1] = new LegInfo(1, NPC.Bottom + new Vector2(-82, 0), false);
                    legs[2] = new LegInfo(2, NPC.Bottom + new Vector2(80, 0), false);
                    legs[3] = new LegInfo(3, NPC.Bottom + new Vector2(-102, 0), false);
                }

                for (int m = 0; m < 4; m++)
                {
                    legs[m].UpdateLeg(NPC);
                }
            }
        }

        public Vector2 position, oldPosition;
        private static float X(float t, float x0, float x1, float x2)
        {
            return (float)(
                x0 * Math.Pow(1 - t, 2) +
                x1 * 2 * t * Math.Pow(1 - t, 1) +
                x2 * Math.Pow(t, 2)
            );
        }
        private static float Y(float t, float y0, float y1, float y2)
        {
            return (float)(
                 y0 * Math.Pow(1 - t, 2) +
                 y1 * 2 * t * Math.Pow(1 - t, 1) +
                 y2 * Math.Pow(t, 2)
             );
        }

        public void DrawHead(SpriteBatch spriteBatch, Texture2D headTexture, Texture2D glowMaskTexture, Vector2 drawPos, Rectangle drawFrame, float drawRot, Color drawColor, bool DrawUnder)
        {
            Color glowColor = Color.White;
            {
                Texture2D neckTex2D = NeckTexture.Value;
                Vector2 neckOrigin = new Vector2(NPC.Center.X, NPC.Center.Y - 40 * NPC.scale) - (NPC.IsABestiaryIconDummy ? Vector2.Zero : Main.screenPosition);
                float chainsPerUse = 0.05f * NPC.scale;
                for (float i = 0; i <= 1; i += chainsPerUse)
                {
                    Vector2 distBetween;
                    float projTrueRotation;
                    if (i != 0)
                    {
                        distBetween = new Vector2(X(i, neckOrigin.X, (neckOrigin.X + drawPos.X) / 2, drawPos.X) -
                        X(i - chainsPerUse, neckOrigin.X, (neckOrigin.X + drawPos.X) / 2, drawPos.X),
                        Y(i, neckOrigin.Y, neckOrigin.Y + 50, drawPos.Y) -
                        Y(i - chainsPerUse, neckOrigin.Y, neckOrigin.Y + 50, drawPos.Y));
                        projTrueRotation = distBetween.ToRotation() - (float)Math.PI / 2;
                        Vector2 neckPos = new Vector2(X(i, neckOrigin.X, (neckOrigin.X + drawPos.X) / 2, drawPos.X), Y(i, neckOrigin.Y, neckOrigin.Y + 50, drawPos.Y));
                        spriteBatch.Draw(neckTex2D, neckPos, null, drawColor, projTrueRotation, neckTex2D.Size() * 0.5f, NPC.scale, 0, 0);
                    }
                }

                spriteBatch.Draw(headTexture, drawPos, drawFrame, drawColor, drawRot, drawFrame.Size() * 0.5f, NPC.scale, 0, 0);
                spriteBatch.Draw(glowMaskTexture, drawPos, drawFrame, glowColor, drawRot, drawFrame.Size() * 0.5f, NPC.scale, 0, 0);
            }
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Color lightColor = NPC.GetAlpha(drawColor);
            SpriteBatch sb = spriteBatch;
            spriteBatch.Draw(TailTexture.Value, NPC.Center + new Vector2(0f, NPC.gfxOffY + 24 * NPC.scale) + bottomVisualOffset - screenPos, null, lightColor, NPC.rotation, TailTexture.Size() * 0.5f, NPC.scale, 0, 0);
            //BaseDrawing.DrawTexture(spriteBatch, TailTexture.Value, 0, NPC.position + new Vector2(0f, NPC.gfxOffY) + bottomVisualOffset, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.spriteDirection, Main.npcFrameCount[NPC.type], frameBottom, lightColor, false);

            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                if (NPC.IsABestiaryIconDummy)
                {
                    List<(Vector2 offset, float lengthA, float lengthB, bool frontSet, bool leftSet, float yOffset)> dummyLegs = [
                        new(new(-60, -10), 100, 80, false, true, -7), //Back Left
                        new(new(60, -10), 100, 80, false, false, -7), //Back Right
                        new(new(-20, -10), 90, 80, true, true, -7), //Front Left
                        new(new(20, -10), 90, 80, true, false, -7)
                    ];
                    foreach(var (offset, lengthA, lengthB, frontSet, leftSet, yOffset) in dummyLegs)
                        DrawYamataLeg(spriteBatch, NPC, NPC.Center + (offset * NPC.scale), 
                            NPC.Center + (offset * NPC.scale) + new Vector2(lengthA * NPC.scale * (leftSet ? -1 : 1), 0).RotatedBy(leftSet ? 0.2f : -0.2f),
                            NPC.Center + (offset * NPC.scale) + new Vector2(lengthA * NPC.scale * (leftSet ? -1 : 1), 0).RotatedBy(leftSet ? 0.2f : -0.2f) + Vector2.UnitY * lengthB * NPC.scale,
                            leftSet, frontSet, true);
                }
                else if(unofficialLegs != null)
                {
                    foreach (IKLeg leg in unofficialLegs)
                        DrawYamataLeg(spriteBatch, NPC, leg.Start, leg.Middle, leg.End, leg.LeftSet, leg.FrontSet);
                }
            }
            else
            {
                if (NPC.IsABestiaryIconDummy)
                {
                    for (int i = 3; i >= 0; i--)
                    {
                        Vector2 start = NPC.Center + new Vector2(i == 3 || i == 1 ? -40f : 40f, 0f);
                        Vector2 end = NPC.Bottom;
                        switch(i)
                        {
                            case 0:
                                end += new Vector2(60, 0);
                                break;
                            case 1:
                                end += new Vector2(-82, 0);
                                break;
                            case 2:
                                end += new Vector2(80, 0);
                                break;
                            case 3:
                                end += new Vector2(-102, 0);
                                break;
                        }
                        Vector2 middle = Vector2.Lerp(end, start, 0.3f) + new Vector2(i == 3 || i == 1 ? 30 : 0f, -30);
                        DrawYamataLeg(spriteBatch, NPC, start, middle, end, i == 3 || i == 1, i <= 1, true);
                    }
                }
                else if (legs != null && legs.Length == 4)
                {
                    for (int i = 3; i >= 0; i--)
                    {
                        var leg = legs[i];
                        Vector2 start = leg.GetBodyConnector(NPC);
                        Vector2 middle = leg.LegJoint;
                        Vector2 end = leg.position - new Vector2(0f, leg.VelOffsetY);

                        DrawYamataLeg(spriteBatch, NPC, start, middle, end, leg.LeftLeg, i <= 1);
                    }
                }
            }

            if(NPC.IsABestiaryIconDummy)
            {
                bool isSmall = NPC.scale == 0.7f;
                List<Vector2> heads = [
                    new Vector2(isSmall ? -12 : -32, -12),
                    new Vector2(isSmall ? 12 : 32, -12),
                    new Vector2(isSmall ? 36 : 64, 0),
                    new Vector2(isSmall ? -36 : -64, 0), 
                    new Vector2(isSmall ? -18 : -72, isSmall ? 26 : 44),     
                    new Vector2(isSmall ? 18 : 72, isSmall ? 26 : 44), 
                ];
                foreach (Vector2 headPos in heads)
                {
                    if (headPos.X > 0)
                        DrawHead(sb, HeadF1Texture.Value, HeadF1GlowTexture.Value, NPC.Center - (Vector2.UnitY * 110f * NPC.scale) + headPos, HeadF1Texture.Frame(1, 3), 0, lightColor, false);
                    else
                        DrawHead(sb, HeadF2Texture.Value, HeadF2GlowTexture.Value, NPC.Center - (Vector2.UnitY * 110f * NPC.scale) + headPos, HeadF2Texture.Frame(1, 3), 0, lightColor, false);
                }
            }
            else if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                List<NPC> heads = [Head2, Head3, Head4, Head5, Head6, Head7];
                heads.Sort((n1, n2) => n1.Center.Y.CompareTo(n2.Center.Y));
                foreach (NPC head in heads)
                {
                    if (head != null && head.active && head.ModNPC != null && (head.ModNPC is YamataHead || head.ModNPC is YamataHeadFake1))
                    {
                        if (head.whoAmI == Head2.whoAmI || head.whoAmI == Head3.whoAmI || head.whoAmI == Head4.whoAmI)
                            DrawHead(sb, HeadF1Texture.Value, HeadF1GlowTexture.Value, head.Center - screenPos, head.frame, head.rotation, lightColor, false);
                        else
                            DrawHead(sb, HeadF2Texture.Value, HeadF2GlowTexture.Value, head.Center - screenPos, head.frame, head.rotation, lightColor, false);
                    }
                }
            }
            else
            {
                if (Head2 != null && Head2.active && Head2.ModNPC != null && (Head2.ModNPC is YamataHead || Head2.ModNPC is YamataHeadFake1))
                    DrawHead(sb, HeadF1Texture.Value, HeadF1GlowTexture.Value, Head2.Center - screenPos, Head2.frame, Head2.rotation, lightColor, false);
                if (Head3 != null && Head3.active && Head3.ModNPC != null && (Head3.ModNPC is YamataHead || Head3.ModNPC is YamataHeadFake1))
                    DrawHead(sb, HeadF1Texture.Value, HeadF1GlowTexture.Value, Head3.Center - screenPos, Head3.frame, Head3.rotation, lightColor, false);
                if (Head4 != null && Head4.active && Head4.ModNPC != null && (Head4.ModNPC is YamataHead || Head4.ModNPC is YamataHeadFake1))
                    DrawHead(sb, HeadF1Texture.Value, HeadF1GlowTexture.Value, Head4.Center - screenPos, Head4.frame, Head4.rotation, lightColor, false);
                if (Head5 != null && Head5.active && Head5.ModNPC != null && (Head5.ModNPC is YamataHead || Head5.ModNPC is YamataHeadFake1))
                    DrawHead(sb, HeadF2Texture.Value, HeadF2GlowTexture.Value, Head5.Center - screenPos, Head5.frame, Head5.rotation, lightColor, false);
                if (Head6 != null && Head6.active && Head6.ModNPC != null && (Head6.ModNPC is YamataHead || Head6.ModNPC is YamataHeadFake1))
                    DrawHead(sb, HeadF2Texture.Value, HeadF2GlowTexture.Value, Head6.Center - screenPos, Head6.frame, Head6.rotation, lightColor, false);
                if (Head7 != null && Head7.active && Head7.ModNPC != null && (Head7.ModNPC is YamataHead || Head7.ModNPC is YamataHeadFake1))
                    DrawHead(sb, HeadF2Texture.Value, HeadF2GlowTexture.Value, Head7.Center - screenPos, Head7.frame, Head7.rotation, lightColor, false);
            }

            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center + new Vector2(0f, NPC.gfxOffY - 10 * NPC.scale) - screenPos, null, lightColor, NPC.rotation, TextureAssets.Npc[NPC.type].Size() * 0.5f, NPC.scale, NPC.SpriteEffectDirection(), 0);

            if (NPC.IsABestiaryIconDummy)
                DrawHead(sb, HeadTex.Value, HeadGlowTexture.Value, NPC.Center - (Vector2.UnitY * 110f * NPC.scale), HeadTex.Frame(1, 3), 0, lightColor, false);
            else
                DrawHead(sb, HeadTex.Value, HeadGlowTexture.Value, TrueHead.Center - screenPos, TrueHead.frame, TrueHead.rotation, lightColor, false);
        }
    }
}