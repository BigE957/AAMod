using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata;
using AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.BossStandard;
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
using static AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma.Awakened.AkumaAHead;
using static AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items.AAConditions;

namespace AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata.Awakened
{
    [AutoloadBossHead]
    public class YamataABody : YamataBoss
    {
        public static Asset<Texture2D> HeadTex; //Different cause ModNPC has HeadTexture...
        public static Asset<Texture2D> HeadFTexture;
        public static Asset<Texture2D> HeadGlowTexture;
        public static Asset<Texture2D> HeadFGlowTexture;
        public static Asset<Texture2D> NeckTexture;
        public static Asset<Texture2D> TailTexture;
        public static Asset<Texture2D> Glow;

        public NPC TrueHead;
        public NPC Head2;
        public NPC Head3;
        public NPC Head4;
        public NPC Head5;
        public NPC Head6;
        public NPC Head7;
        public bool HeadsSpawned = false;
        private bool threeQuarterHealth = false;
        private bool HalfHealth = false;
        private bool tenthHealth = false;
        public bool loludide = false;
        public bool flag;

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
            displayName = "Yamata no Orochi";
            //Main.npcFrameCount[npc.type] = 7;

            HeadTex = ModContent.Request<Texture2D>(Texture.Replace("Body", "") + "Head");
            HeadFTexture = ModContent.Request<Texture2D>(Texture.Replace("Body", "") + "HeadFake");
            HeadGlowTexture = ModContent.Request<Texture2D>(Texture.Replace("Body", "") + "Head_Glow");
            HeadFGlowTexture = ModContent.Request<Texture2D>(Texture.Replace("Body", "") + "HeadFake_Glow");

            string texRoot = Texture + "_";
            NeckTexture = ModContent.Request<Texture2D>(texRoot + "Neck");
            Glow = ModContent.Request<Texture2D>(texRoot + "Glow");
            TailTexture = ModContent.Request<Texture2D>(texRoot + "Tail");

            NPCID.Sets.NPCBestiaryDrawModifiers value = new()
            {
                Scale = 0.7f,
                PortraitScale = 0.8f,
                PortraitPositionYOverride = 210,
                Position = new(0, 190)
            };
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
        }

        public override void SetDefaults()
        {
            NPC.npcSlots = 100;
            NPC.width = 80;
            NPC.height = 90;
            NPC.aiStyle = -1;
            NPC.damage = 0;
            NPC.lifeMax = 480000;
            NPC.defense = 999999;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.noGravity = true;
            NPC.netAlways = true;
            frameWidth = 324;
            frameHeight = 236;
            if (!NPC.IsABestiaryIconDummy)
                NPC.alpha = 255;
            NPC.frame = BaseDrawing.GetFrame(0, frameWidth, frameHeight, 0, 2);
            frameBottom = BaseDrawing.GetFrame(frameCount, frameWidth, 54, 0, 2);
            frameHead = BaseDrawing.GetFrame(frameCount, frameWidth, 118, 0, 2);
            NPC.DeathSound = new SoundStyle("AAModClassic/Sounds/YamataRoar");
            NPC.chaseable = false;
            NPC.value = Item.buyPrice(0, 40, 0, 0);
            Music = MusicManagementSystem.MusicSlots["Yamata_Awakened"];
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
            SceneEffectPriority = SceneEffectPriority.BossHigh;
            SpawnModBiomes = [ModContent.GetInstance<MireBiome>().Type];
            NPC.BossBar = Main.BigBossProgressBar.NeverValid;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
                new FlavorTextBestiaryInfoElement("Mods.AAModClassic.Bestiary.YamataNoOrochi")
            ]);
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.5f * balance);
            NPC.damage = (int)(NPC.damage * .7f);
        }
        
        public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
        {
            modifiers.TargetDamageMultiplier *= 0;
            
            int dust1 = ModContent.DustType<Dusts.YamataADust>();
            int dust2 = ModContent.DustType<Dusts.YamataADust>();
            if (NPC.life <= 0)
            {
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust1, 0f, 0f, 0);
                Main.dust[dust1].velocity *= 0.5f;
                Main.dust[dust1].scale *= 1.3f;
                Main.dust[dust1].fadeIn = 1f;
                Main.dust[dust1].noGravity = false;
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust2, 0f, 0f, 0);
                Main.dust[dust2].velocity *= 0.5f;
                Main.dust[dust2].scale *= 1.3f;
                Main.dust[dust2].fadeIn = 1f;
                Main.dust[dust2].noGravity = true;

            }
            if (!AAWorld.downedYamata)
            {
                if (NPC.life <= NPC.lifeMax / 4 * 3 && threeQuarterHealth == false)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Yamata.Awakened.Health.First.ThreeQuarters"), new Color(146, 30, 68));
                    threeQuarterHealth = true;
                }
                if (NPC.life <= NPC.lifeMax / 2 && HalfHealth == false)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Yamata.Awakened.Health.First.Half"), new Color(146, 30, 68));
                    HalfHealth = true;
                }
                if (NPC.life <= NPC.lifeMax / 10 && tenthHealth == false)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Yamata.Awakened.Health.First.Tenth"), new Color(146, 30, 68));
                    tenthHealth = true;
                }
            }
            if (AAWorld.downedYamata)
            {
                if (NPC.life <= NPC.lifeMax / 4 * 3 && threeQuarterHealth == false)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Yamata.Awakened.Health.Repeat.ThreeQuarters"), new Color(146, 30, 68));
                    threeQuarterHealth = true;
                }
                if (NPC.life <= NPC.lifeMax / 2 && HalfHealth == false)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Yamata.Awakened.Health.Repeat.Half"), new Color(146, 30, 68));
                    HalfHealth = true;
                }
                if (NPC.life <= NPC.lifeMax / 10 && tenthHealth == false)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Yamata.Awakened.Health.Repeat.Tenth"), new Color(146, 30, 68));
                    tenthHealth = true;
                }
            }
        }

        public override void BossLoot(ref int potionType)
        {
            if (Main.expertMode)
            {
                potionType = ItemID.SuperHealingPotion;
            }
        }

        public override void OnKill()
        {
            if (!Main.expertMode)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Yamata.Awakened.Defeat.Cheat"), new Color(146, 30, 68));
            }
            else if (NPC.playerInteraction[Main.myPlayer])
                YamataKilled.Condition.Complete();

            if (!NPC.BeenKilled(true))
            {
                NPC.OnGameEventClearedForTheFirstTime(-1);

                if (Main.netMode != NetmodeID.MultiplayerClient)
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Yamata.Awakened.Defeat.First"), new Color(146, 30, 68));
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Yamata.Defeat.Status"), Color.Indigo);
            }
            else
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Yamata.Awakened.Defeat.Repeat"), new Color(146, 30, 68));
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<YamataTreasureBag>()));

            npcLoot.AddLoreItemDrop<YamataABody>(ModContent.ItemType<YamataLore>());

            LeadingConditionRule masterMode = new(new AAConditions.RevOrMaster());

            masterMode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<YamataRelic>()));

            npcLoot.Add(masterMode);

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<YamataATrophy>(), 10));

            LeadingConditionRule firstKill = new(new FirstTimeKillingYamataA());

            firstKill.OnSuccess(ItemDropRule.Common(ModContent.ItemType<DreadMoonRune>()));

            LeadingConditionRule shenDefeated = new(new ShenDefeated());

            shenDefeated.OnSuccess(ItemDropRule.Common(ModContent.ItemType<EXSoul>(), 50));

            npcLoot.Add(firstKill);
            npcLoot.Add(shenDefeated);

            LeadingConditionRule anceintsDownAndRemoved = new(new PostLateAncientsAndRemovedWorldAndExpert());

            anceintsDownAndRemoved.OnSuccess(ItemDropRule.Common(ModContent.ItemType<SpaceStone>(), 50));

            npcLoot.Add(anceintsDownAndRemoved);
        }

        public class FirstTimeKillingYamataA : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !NPCExtensions.BeenKilled<YamataABody>(true);
            public bool CanShowItemDropInUI() => true;
            public string GetConditionDescription() => null;
        }


        public int playerTooFarDist = 800;
        public Rectangle frameBottom = new Rectangle(0, 0, 1, 1), frameHead = new Rectangle(0, 0, 1, 1);
        public bool prevHalfHPLeft = false, halfHPLeft = false, prevFourthHPLeft = false, fourthHPLeft = false;
        public Player playerTarget = null;
        public static int flyingTileCount = 6, totalMinionCount = 0;
        public int MinionTimer = 0;

        //clientside stuff
        public Vector2 bottomVisualOffset = default;
        public Vector2 topVisualOffset = default;
        public LegInfo[] legs = null;
        public IKLeg[] unofficialLegs = null;
        public bool[] headsSaidOw = new bool[7];
        public bool Tag = false;
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
                    const int headX = 300;
                    const int headY = -500;

                    TrueHead = Main.npc[NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<YamataAHead>(), 0)];
                    TrueHead.ai[0] = NPC.whoAmI;
                    TrueHead.ai[1] = 0;
                    TrueHead.ai[2] = headY;
                    Head2 = Main.npc[NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<YamataAHeadFake>(), 0)];
                    Head2.ai[0] = NPC.whoAmI;
                    Head2.ai[1] = headX * -3f;
                    Head2.ai[2] = headY * 0.7f;
                    Head2.ai[3] = 3f;
                    Head3 = Main.npc[NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<YamataAHeadFake>(), 0)];
                    Head3.ai[0] = NPC.whoAmI;
                    Head3.ai[1] = headX * -2f;
                    Head3.ai[2] = headY * 0.8f;
                    Head3.ai[3] = 2f;
                    Head4 = Main.npc[NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<YamataAHeadFake>(), 0)];
                    Head4.ai[0] = NPC.whoAmI;
                    Head4.ai[1] = headX * -1f;
                    Head4.ai[2] = headY * 0.9f;
                    Head4.ai[3] = 1f;
                    Head5 = Main.npc[NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<YamataAHeadFake>(), 0)];
                    Head5.ai[0] = NPC.whoAmI;
                    Head5.ai[1] = headX * 1f;
                    Head5.ai[2] = headY * 0.9f;
                    Head5.ai[3] = 1f;
                    Head6 = Main.npc[NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<YamataAHeadFake>(), 0)];
                    Head6.ai[0] = NPC.whoAmI;
                    Head6.ai[1] = headX * 2f;
                    Head6.ai[2] = headY * 0.8f;
                    Head6.ai[3] = 2f;
                    Head7 = Main.npc[NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<YamataAHeadFake>(), 0)];
                    Head7.ai[0] = NPC.whoAmI;
                    Head7.ai[1] = headX * 3f;
                    Head7.ai[2] = headY * 0.7f;
                    Head7.ai[3] = 3f;

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
                if (!HeadsSpawned)
                {
                    int[] npcs = BaseAI.GetNPCs(NPC.Center, -1, default, 200f, null);
                    if (npcs != null && npcs.Length > 0)
                    {
                        foreach (int npcID in npcs)
                        {
                            NPC npc2 = Main.npc[npcID];
                            if (npc2 != null)
                            {
                                if (TrueHead == null && npc2.type == ModContent.NPCType<YamataAHead>() && npc2.ai[0] == NPC.whoAmI)
                                {
                                    TrueHead = npc2;
                                }
                                else
                                if (Head2 == null && npc2.type == ModContent.NPCType<YamataAHeadFake>() && npc2.ai[0] == NPC.whoAmI)
                                {
                                    Head2 = npc2;
                                }
                                else
                                if (Head3 == null && npc2.type == ModContent.NPCType<YamataAHeadFake>() && npc2.ai[0] == NPC.whoAmI)
                                {
                                    Head3 = npc2;
                                }
                                else
                                if (Head4 == null && npc2.type == ModContent.NPCType<YamataAHeadFake>() && npc2.ai[0] == NPC.whoAmI)
                                {
                                    Head4 = npc2;
                                }
                                else
                                if (Head5 == null && npc2.type == ModContent.NPCType<YamataAHeadFake>() && npc2.ai[0] == NPC.whoAmI)
                                {
                                    Head5 = npc2;
                                }
                                else
                                if (Head6 == null && npc2.type == ModContent.NPCType<YamataAHeadFake>() && npc2.ai[0] == NPC.whoAmI)
                                {
                                    Head6 = npc2;
                                }
                                else
                                if (Head7 == null && npc2.type == ModContent.NPCType<YamataAHeadFake>() && npc2.ai[0] == NPC.whoAmI)
                                {
                                    Head7 = npc2;
                                }
                            }
                        }
                    }
                    if (TrueHead != null && Head2 != null && Head3 != null && Head4 != null && Head5 != null && Head6 != null && Head7 != null)
                    {
                        HeadsSpawned = true;
                    }
                }
            }
        }

        public override void AI()
        {
            NPC.GetGlobalNPC<TitleGlobalNPC>().ShowTitle = true;

            TargetClosest();
            HandleHeads();

            if (Tag)
            {
                NPC.life = 0;
                NPC.netUpdate = true;
            }
            if (SayTheLineYamata <= 0)
            {
                SayTheLineYamata = 300;
            }

            if (Main.dayTime)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient && !flag)
                {
                    flag = true;
                    ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Yamata.Awakened.NightReset"), new Color(146, 30, 68));
                }
                Main.dayTime = false;
                Main.time = 0;
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
                NoFlyCountDown--;

                for (int p = 0; p < Main.maxPlayers; p++)
                {
                    Player t = Main.player[p];
                    if (t.active && !t.dead)
                    {
                        Main.player[p].AddBuff(ModContent.BuffType<YamataABody_TrueAbyssalGravity>(), 10, true);
                    }
                }

                float dist = NPC.Distance(playerTarget.Center);
                if (dist > 1200 || !Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height)
                    || Main.player[NPC.target].position.Y < NPC.position.Y - 500)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient && SayTheLineYamata == 300)
                    {
                        if (!FirstLine)
                        {
                            if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Yamata.Awakened.Teleport"), new Color(146, 30, 68));
                            FirstLine = true;
                        }
                    }
                    SayTheLineYamata--;
                    NPC.alpha += 1;
                    if (NPC.alpha >= 255)
                    {
                        NPC.alpha = 255;
                        Vector2 tele = playerTarget.Center + new Vector2(0, -100) +  (playerTarget.velocity == new Vector2(0,0)? new Vector2(0,0) : Vector2.Normalize(playerTarget.velocity) * playerTarget.velocity.Length() * 54.33f);
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
                        NPC.alpha = 0;
                        NPC.dontTakeDamage = false;
                        TrueHead.dontTakeDamage = false;
                        Head2.dontTakeDamage = false;
                        Head3.dontTakeDamage = false;
                        Head4.dontTakeDamage = false;
                        Head5.dontTakeDamage = false;
                        Head6.dontTakeDamage = false;
                        Head7.dontTakeDamage = false;
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
                if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Yamata.Awakened.Kill"), new Color(146, 30, 68));
                loludide = true;
            }

            NPC.alpha += 10;
            if (NPC.alpha >= 255)
            {
                NPC.active = false;
            }
        }

        public void AIMovementNormal(float playerDistance)
        {
            bool playerTooFar = playerDistance > playerTooFarDist;
            YamataBody(NPC, ref NPC.ai, true, 0.2f, 3.5f, 8f, 0.07f, 1.5f, 4);
            if (playerTooFar) NPC.position += playerTarget.position - playerTarget.oldPosition;
            NPC.rotation = 0f;
        }

        public static void YamataBody(NPC npc, ref float[] ai, bool ignoreWet = true, float moveInterval = 0.2f, float maxSpeedX = 2f, float maxSpeedY = 1.5f, float hoverInterval = 0.04f, float hoverMaxSpeed = 1.5f, int hoverHeight = 3)
        {
            bool flyUpward = false;
            if (npc.justHit) { ai[2] = 0f; }
            if (ai[2] >= 0f)
            {
                int tileDist = 16;
                bool inRangeX = false;
                bool inRangeY = false;
                if (npc.position.X > ai[0] - tileDist && npc.position.X < ai[0] + tileDist) { inRangeX = true; }
                else
                    if (npc.velocity.X < 0f && npc.direction > 0 || npc.velocity.X > 0f && npc.direction < 0) { inRangeX = true; }
                tileDist += 24;
                if (npc.position.Y > ai[1] - tileDist && npc.position.Y < ai[1] + tileDist)
                {
                    inRangeY = true;
                }
                if (inRangeX && inRangeY)
                {
                    ai[2] += 1f;
                    if (ai[2] >= 30f && tileDist == 16)
                    {
                        flyUpward = true;
                    }
                    if (ai[2] >= 60f)
                    {
                        ai[2] = -200f;
                        npc.direction *= -1;
                        npc.velocity.X *= -1f;
                        npc.collideX = false;
                    }
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
                {
                    npc.direction = -1;
                }
                else
                {
                    npc.direction = 1;
                }
            }

            int tileX = (int)(npc.Center.X / 16f) + npc.direction * 2;
            int tileY = (int)((npc.position.Y + npc.height) / 16f);
            bool tileBelowEmpty = true;

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
            {
                tileBelowEmpty = true;
            }

            if (tileBelowEmpty)
            {
                npc.velocity.Y += moveInterval;
                if (npc.velocity.Y > 9f)
                {
                    npc.velocity.Y = 9f;
                }
            }
            else
            {
                if (npc.directionY < 0 && npc.velocity.Y > 0f) { npc.velocity.Y -= moveInterval; }
                if (npc.velocity.Y < -maxSpeedY) { npc.velocity.Y = -maxSpeedY; }
            }


            if (!ignoreWet && npc.wet)
            {
                npc.velocity.Y -= moveInterval;
                if (npc.velocity.Y < -maxSpeedY * 0.75f) { npc.velocity.Y = -maxSpeedY * 0.75f; }
            }


            if (npc.collideY)
            {
                npc.velocity.Y = npc.oldVelocity.Y * -0.25f;
                if (npc.velocity.Y > 0f && npc.velocity.Y < 1f) { npc.velocity.Y = 1f; }
                if (npc.velocity.Y < 0f && npc.velocity.Y > -1f) { npc.velocity.Y = -1f; }
            }

            if (!tileBelowEmpty && npc.target > -1 && Main.player[npc.target].active && !Main.player[npc.target].dead && Math.Abs(Main.player[npc.target].Center.X - npc.Center.X) < 50) //force a hover
            {
                if (Math.Abs(npc.velocity.X) > 0.3f) npc.velocity.X *= 0.9f;
                if (Math.Abs(npc.velocity.Y) > 0.3f) npc.velocity.Y *= 0.9f;
            }
            else
            if (npc.direction == -1 && npc.velocity.X > -maxSpeedX)
            {
                npc.velocity.X -= moveInterval * 0.5f;
                if (npc.velocity.X > maxSpeedX) { npc.velocity.X -= 0.1f; }
                else
                    if (npc.velocity.X > 0f) { npc.velocity.X += 0.05f; }
                if (npc.velocity.X < -maxSpeedX) { npc.velocity.X = -maxSpeedX; }
            }
            else
            if (npc.direction == 1 && npc.velocity.X < maxSpeedX)
            {
                npc.velocity.X += moveInterval * 0.5f;
                if (npc.velocity.X < -maxSpeedX) { npc.velocity.X += 0.1f; }
                else
                    if (npc.velocity.X < 0f) { npc.velocity.X -= 0.05f; }
                if (npc.velocity.X > maxSpeedX) { npc.velocity.X = maxSpeedX; }
            }


            if (npc.directionY == -1 && (double)npc.velocity.Y > -hoverMaxSpeed)
            {
                npc.velocity.Y -= hoverInterval;
                if ((double)npc.velocity.Y > hoverMaxSpeed) { npc.velocity.Y -= 0.05f; }
                else
                    if (npc.velocity.Y > 0f) { npc.velocity.Y += hoverInterval - 0.01f; }
                if ((double)npc.velocity.Y < -hoverMaxSpeed) { npc.velocity.Y = -hoverMaxSpeed; }
            }
            else
            if (npc.directionY == 1 && (double)npc.velocity.Y < hoverMaxSpeed)
            {
                npc.velocity.Y += hoverInterval;
                if ((double)npc.velocity.Y < -hoverMaxSpeed) { npc.velocity.Y += 0.05f; }
                else
                if (npc.velocity.Y < 0f) { npc.velocity.Y -= hoverInterval - 0.01f; }
                if ((double)npc.velocity.Y > hoverMaxSpeed) { npc.velocity.Y = hoverMaxSpeed; }
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
                    unofficialLegs[0] = new(NPC, new(-60, -30), 140, 100, false, true, -16, 72, 0.6f); //Back Left
                    unofficialLegs[1] = new(NPC, new(60, -30), 140, 100, false, false, -16, 72, 0.6f); //Back Right
                    unofficialLegs[2] = new(NPC, new(-20, -30), 120, 100, true, true, -16, 72, 0.6f); //Front Left
                    unofficialLegs[3] = new(NPC, new(20, -30), 120, 100, true, false, -16, 72, 0.6f); //Front Right        

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
                    legs[0] = new LegInfo(0, NPC.Bottom + new Vector2(60, 0), true);
                    legs[1] = new LegInfo(1, NPC.Bottom + new Vector2(-82, 0), true);
                    legs[2] = new LegInfo(2, NPC.Bottom + new Vector2(80, 0), true);
                    legs[3] = new LegInfo(3, NPC.Bottom + new Vector2(-102, 0), true);
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
            drawPos -= Vector2.UnitY * 30;
            Color glowColor = AAColor.COLOR_WHITEFADE1;
            Texture2D neckTex2D = NeckTexture.Value;
            Vector2 neckOrigin = new Vector2(NPC.Center.X, NPC.Center.Y - 110 * NPC.scale) - (NPC.IsABestiaryIconDummy ? Vector2.Zero : Main.screenPosition);
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

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Color lightColor = NPC.GetAlpha(drawColor);
            SpriteBatch sb = spriteBatch;
            spriteBatch.Draw(TailTexture.Value, NPC.Center + new Vector2(0f, NPC.gfxOffY) + bottomVisualOffset - screenPos, null, lightColor, NPC.rotation, TailTexture.Size() * 0.5f, NPC.scale, 0, 0);
            //BaseDrawing.DrawTexture(spriteBatch, TailTexture.Value, 0, NPC.position + new Vector2(0f, NPC.gfxOffY) + bottomVisualOffset + new Vector2(0, -32), NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.spriteDirection, Main.npcFrameCount[NPC.type], frameBottom, lightColor, false);

            if (!NPC.IsABestiaryIconDummy)
            {
                if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) && unofficialLegs != null)
                {
                    foreach (IKLeg leg in unofficialLegs)
                        DrawYamataLeg(spriteBatch, NPC, leg.Start, leg.Middle, leg.End, leg.LeftSet, leg.FrontSet);
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

            if (NPC.IsABestiaryIconDummy)
            {
                List<Vector2> heads = [
                    new Vector2(-70, 24),
                    new Vector2(70, 24),
                ];
                foreach (Vector2 headPos in heads)
                    DrawHead(sb, HeadFTexture.Value, HeadFGlowTexture.Value, NPC.Center - (Vector2.UnitY * 240f * NPC.scale) + headPos, HeadFTexture.Frame(1, 3), 0, lightColor, false);
            }
            else
            {
                if (Head2 != null && Head2.active && Head2.ModNPC != null && Head2.ModNPC is YamataAHeadFake)
                    DrawHead(sb, HeadFTexture.Value, HeadFGlowTexture.Value, Head2.Center - screenPos, Head2.frame, Head2.rotation, lightColor, false);
                if (Head3 != null && Head3.active && Head3.ModNPC != null && Head2.ModNPC is YamataAHeadFake)
                    DrawHead(sb, HeadFTexture.Value, HeadFGlowTexture.Value, Head3.Center - screenPos, Head3.frame, Head3.rotation, lightColor, false);
                if (Head4 != null && Head4.active && Head4.ModNPC != null && Head2.ModNPC is YamataAHeadFake)
                    DrawHead(sb, HeadFTexture.Value, HeadFGlowTexture.Value, Head4.Center - screenPos, Head4.frame, Head4.rotation, lightColor, false);
                if (Head5 != null && Head5.active && Head5.ModNPC != null && Head2.ModNPC is YamataAHeadFake)
                    DrawHead(sb, HeadFTexture.Value, HeadFGlowTexture.Value, Head5.Center - screenPos, Head5.frame, Head5.rotation, lightColor, false);
                if (Head6 != null && Head6.active && Head6.ModNPC != null && Head2.ModNPC is YamataAHeadFake)
                    DrawHead(sb, HeadFTexture.Value, HeadFGlowTexture.Value, Head6.Center - screenPos, Head6.frame, Head6.rotation, lightColor, false);
                if (Head7 != null && Head7.active && Head7.ModNPC != null && Head2.ModNPC is YamataAHeadFake)
                    DrawHead(sb, HeadFTexture.Value, HeadFGlowTexture.Value, Head7.Center - screenPos, Head7.frame, Head7.rotation, lightColor, false);
            }

            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center + new Vector2(0f, NPC.gfxOffY - 68) + topVisualOffset - screenPos, null, lightColor, NPC.rotation, TextureAssets.Npc[NPC.type].Size() * 0.5f, NPC.scale, NPC.SpriteEffectDirection(), 0);
            //BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC.position + new Vector2(0f, NPC.gfxOffY) + topVisualOffset, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.spriteDirection, Main.npcFrameCount[NPC.type], NPC.frame, lightColor, false);

            spriteBatch.Draw(Glow.Value, NPC.Center + new Vector2(0f, NPC.gfxOffY - 68) + topVisualOffset - screenPos, null, AAColor.COLOR_WHITEFADE1, NPC.rotation, Glow.Size() * 0.5f, NPC.scale, NPC.SpriteEffectDirection(), 0);
            //BaseDrawing.DrawTexture(spriteBatch, Glow.Value, 0, NPC.position + new Vector2(0f, NPC.gfxOffY) + topVisualOffset, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.spriteDirection, Main.npcFrameCount[NPC.type], NPC.frame, AAColor.COLOR_WHITEFADE1, false);
            if (!NPC.IsABestiaryIconDummy)
            {
                DrawingUtils.DrawAfterimageWithVelocity(spriteBatch, Glow.Value, NPC.Center + new Vector2(0f, NPC.gfxOffY - 68) + topVisualOffset - screenPos, NPC.velocity, 4, null, AAColor.COLOR_WHITEFADE1, NPC.scale, [NPC.rotation], Glow.Size() * 0.5f, NPC.SpriteEffectDirection(), 0.8f);
                //BaseDrawing.DrawAfterimage(spriteBatch, Glow.Value, 0, NPC, 0.8f, 1f, 4, false, 0f, 0f, AAColor.COLOR_WHITEFADE1);
            }

            if (NPC.IsABestiaryIconDummy)
                DrawHead(sb, HeadTex.Value, HeadGlowTexture.Value, NPC.Center - (Vector2.UnitY * 240f * NPC.scale), HeadTex.Frame(1, 3), 0, lightColor, false);
            else if (TrueHead != null && TrueHead.active && TrueHead.ModNPC != null && TrueHead.ModNPC is YamataAHead)
                DrawHead(sb, HeadTex.Value, HeadGlowTexture.Value, TrueHead.Center - screenPos, TrueHead.frame, TrueHead.rotation, lightColor, false);
        }

        public bool spawnHaruka = false;

        public override void FindFrame(int frameHeight)
        {
            //npc.frameCounter++;
            if (NPC.frameCounter < 5)
            {
                NPC.frame.Y = 0 * frameHeight;
            }
            else if (NPC.frameCounter < 10)
            {
                NPC.frame.Y = 1 * frameHeight;
            }
            else if (NPC.frameCounter < 15)
            {
                NPC.frame.Y = 2 * frameHeight;
            }
            else if (NPC.frameCounter < 20)
            {
                NPC.frame.Y = 3 * frameHeight;
            }
            else if (NPC.frameCounter < 25)
            {
                NPC.frame.Y = 4 * frameHeight;
            }
            else if (NPC.frameCounter < 30)
            {
                NPC.frame.Y = 5 * frameHeight;
            }
            else if (NPC.frameCounter < 35)
            {
                NPC.frame.Y = 6 * frameHeight;
            }
            else
            {
                NPC.frameCounter = 0;
            }
        }
    }
}