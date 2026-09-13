using AAModClassic._Content.Desert.___PreHardmode.NPCs.__Friendly;
using AAModClassic._Content.Desert.__Hardmode.Items._BossAnubis.BossStandard;
using AAModClassic._Content.Desert.__Hardmode.Items._BossAnubis.Weapons;
using AAModClassic._Content.Desert.__Hardmode.Items.Materials;
using AAModClassic._Content.Desert._PostMoonlord.NPCs.__BossAnubisA;
using AAModClassic._CrossMod.CalamityMod.LoreItems;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.UI.Titles;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Desert.__Hardmode.NPCs.__BossAnubis
{
    [AutoloadBossHead]
    public class Anubis : ModNPC
    {
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Anubis Legendscribe");
            Main.npcFrameCount[NPC.type] = 11;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new()
            {
                Position = new(0, 48),
                PortraitPositionYOverride = 0,
            };
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
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
            Music = MusicLoader.GetMusicSlot("AAModMusic/Music/Anubis");
            //bossBag/* tModPorter Note: _Unreleased. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = ModContent.ItemType<AnubisBag>();
            NPC.value = Item.buyPrice(0, 1, 0, 0);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,
            ]);
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

            if (internalAI[0] != 1)
            {
                NPC.noGravity = false;
                Preamble();
                return;
            }

            NPC.dontTakeDamage = false;
            NPC.noGravity = true;

            if (internalAI[3] == 0)
            {
                NPC.velocity.Y += 0.002f;
                if (NPC.velocity.Y > .1f)
                {
                    internalAI[3] = 1f;
                    NPC.netUpdate = true;
                }
            }
            else
            if (internalAI[3] == 1)
            {
                NPC.velocity.Y -= 0.002f;
                if (NPC.velocity.Y < -.1f)
                {
                    internalAI[3] = 0f;
                    NPC.netUpdate = true;
                }
            }

            if (NPC.life < NPC.lifeMax / 3 && internalAI[2] == 0)
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
                internalAI[2] = 1;
            }

            NPC.ai[1]++;

            switch (NPC.ai[0])
            {
                case 0:
                    if (!AliveCheck(player))
                        break;
                    int proj = Main.rand.NextBool(50) ? ModContent.ProjectileType<Anubis_Pumpkin>() : ModContent.ProjectileType<Anubis_Runeblast>();

                    int damage = 16;
                    if (NPC.ai[3] == 0 && proj == ModContent.ProjectileType<Anubis_Pumpkin>())
                    {
                        CombatText.NewText(NPC.Hitbox, Color.Gold, Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Anubis.Combat.PumpkinThrow"), true); 
                        damage = 300;
                    }

                    if (NPC.ai[1] == 20)
                    {
                        CombatText.NewText(NPC.Hitbox, Color.Gold, Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Anubis.Combat.Runeblast"), true);
                    }

                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        BaseAI.ShootPeriodic(NPC, player.position, player.width, player.height, proj, ref NPC.ai[3], 80, damage, 10, true);

                    if (NPC.ai[3] == 40)
                    {
                        Teleport();
                    }

                    if (NPC.ai[1] >= 260)
                    {
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        NPC.ai[3] = 0;
                        Teleport();
                    }
                    break;
                case 1:
                    if (!AliveCheck(player))
                        break;
                    if (NPC.ai[1] == 10)
                    {
                        CombatText.NewText(NPC.Hitbox, Color.Gold, Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Anubis.Combat.Summons"), true);

                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            if (Main.rand.NextBool(2) && NPC.life < NPC.lifeMax * (2 / 3))
                            {
                                if (NPC.life < NPC.lifeMax / 3)
                                {
                                    int a = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.position, Vector2.Zero, ModContent.ProjectileType<Anubis_EyeSentrySummon>(), 0, 0, -1, NPC.Center.X - 200, NPC.Center.Y);
                                    Main.npc[a].Center = NPC.Center;
                                    int b = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.position, Vector2.Zero, ModContent.ProjectileType<Anubis_EyeSentrySummon>(), 0, 0, -1, NPC.Center.X + 200, NPC.Center.Y);
                                    Main.npc[b].Center = NPC.Center;
                                    int c = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.position, Vector2.Zero, ModContent.ProjectileType<Anubis_EyeSentrySummon>(), 0, 0, -1, NPC.Center.X, NPC.Center.Y - 200);
                                    Main.npc[c].Center = NPC.Center;
                                }
                                else
                                {
                                    int a = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.position, Vector2.Zero, ModContent.ProjectileType<Anubis_EyeSentrySummon>(), 0, 0, -1, NPC.Center.X - 200, NPC.Center.Y);
                                    Main.npc[a].Center = NPC.Center;
                                    int b = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.position, Vector2.Zero, ModContent.ProjectileType<Anubis_EyeSentrySummon>(), 0, 0, -1, NPC.Center.X + 200, NPC.Center.Y);
                                    Main.npc[b].Center = NPC.Center;
                                }
                            }
                            else
                            {
                                int m = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X + 100, (int)NPC.position.Y, ModContent.NPCType<MinionRitual>());
                                Main.npc[m].Center = new Vector2(NPC.Center.X + 100, NPC.Center.Y);

                                int n = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X - 100, (int)NPC.position.Y, ModContent.NPCType<MinionRitual>());
                                Main.npc[n].Center = new Vector2(NPC.Center.X - 100, NPC.Center.Y);

                                if (NPC.life < NPC.lifeMax / 2)
                                {
                                    int o = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y + 100, ModContent.NPCType<MinionRitual>());
                                    Main.npc[o].Center = new Vector2(NPC.Center.X, NPC.Center.Y + 100);

                                    int p = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y - 100, ModContent.NPCType<MinionRitual>());
                                    Main.npc[p].Center = new Vector2(NPC.Center.X, NPC.Center.Y - 100);
                                }
                            }
                        }
                    }

                    if (NPC.ai[1] >= 160)
                    {
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        NPC.ai[3] = 0;
                        Teleport();
                    }
                    break;
                case 2:
                    if (!AliveCheck(player))
                        break;

                    if (NPC.ai[1] == 20)
                    {
                        CombatText.NewText(NPC.Hitbox, Color.Gold, Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Anubis.Combat.ScepterThrow"), true);
                    }
                    if (NPC.ai[1] == 120)
                    {
                        BaseAI.FireProjectile(player.position, NPC.position, ModContent.ProjectileType<Anubis_Scepter>(), 27, 14, 10, -1);
                    }
                    if (NPC.ai[1] == 160)
                    {
                        ScepterTeleport();
                    }

                    if (NPC.ai[1] > 140 && !AAGlobalProjectile.AnyProjectiles(ModContent.ProjectileType<Anubis_Scepter>()))
                    {
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        NPC.ai[3] = 0;
                        Teleport();
                    }

                    break;

                case 3:
                    if (!AliveCheck(player))
                        break;

                    if (NPC.ai[1] == 20)
                    {
                        CombatText.NewText(NPC.Hitbox, Color.Gold, Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Anubis.Combat.BlockCrush"), true);
                    }

                    if (NPC.life > NPC.lifeMax * (2/3))
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient && NPC.ai[1] == 60)
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

                        if (NPC.ai[1] > 120 && !AAGlobalProjectile.AnyProjectiles(ModContent.ProjectileType<Anubis_BlockVertical>()))
                        {
                            NPC.ai[0]++;
                            NPC.ai[1] = 0;
                            NPC.ai[2] = 0;
                            NPC.ai[3] = 0;
                            Teleport();
                        }
                    }
                    else if (NPC.life < NPC.lifeMax * (2 / 3))
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            if (NPC.ai[1] == 50)
                            {
                                int l = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(-800, 0), Vector2.Zero, ModContent.ProjectileType<Anubis_BlockVertical>(), 18, 7, -1, 0, 0);
                                int r = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(800, 0), Vector2.Zero, ModContent.ProjectileType<Anubis_BlockVertical>(), 18, 7, -1, 1, 0);
                                Main.projectile[l].ai[1] = r;
                                Main.projectile[l].Center = player.Center + new Vector2(-800, 0);
                                Main.projectile[r].ai[1] = l;
                                Main.projectile[r].Center = player.Center + new Vector2(800, 0);
                            }
                            if (NPC.ai[1] == 100)
                            {
                                int u = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(0, -800), Vector2.Zero, ModContent.ProjectileType<Anubis_BlockHorizontal>(), 18, 7, -1, 0, 0);
                                int d = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position + new Vector2(0, 800), Vector2.Zero, ModContent.ProjectileType<Anubis_BlockHorizontal>(), 18, 7, -1, 1, 0);
                                Main.projectile[u].ai[1] = d;
                                Main.projectile[u].Center = player.Center + new Vector2(0, -800);
                                Main.projectile[d].ai[1] = u;
                                Main.projectile[d].Center = player.Center + new Vector2(0, 800);
                            }
                        }
                        if (NPC.ai[1] > 180 && !AAGlobalProjectile.AnyProjectiles(ModContent.ProjectileType<Anubis_BlockVertical>()))
                        {
                            NPC.ai[0]++;
                            NPC.ai[1] = 0;
                            NPC.ai[2] = 0;
                            NPC.ai[3] = 0;
                            Teleport();
                        }
                    }
                    else if (NPC.life < NPC.lifeMax / 3)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient && NPC.ai[1] % 40 == 0)
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

                        if (NPC.ai[1] > 270 && !AAGlobalProjectile.AnyProjectiles(ModContent.ProjectileType<Anubis_BlockVertical>()))
                        {
                            NPC.ai[0]++;
                            NPC.ai[1] = 0;
                            NPC.ai[2] = 0;
                            NPC.ai[3] = 0;
                            Teleport();
                        }
                    }
                    break;
                default:
                    NPC.ai[0] = 0;
                    goto case 0;
            }
        }

        int deathtimer = 0;

        public bool AliveCheck(Player player)
        {
            if (!player.active || player.dead || Vector2.Distance(NPC.Center, player.Center) > 5000f || !player.ZoneDesert)
            {
                NPC.TargetClosest();
                if (!Main.player[NPC.target].active || Main.player[NPC.target].dead || Vector2.Distance(NPC.Center, Main.player[NPC.target].Center) > 5000f || !Main.player[NPC.target].ZoneDesert)
                {
                    deathtimer++;
                    if (Main.netMode != NetmodeID.MultiplayerClient && deathtimer > 240)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Anubis.Kill"), Color.Gold);
                        int a = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<Legendscribe>());
                        Main.npc[a].Center = NPC.Center;
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
            bool beenKilled = NPCExtensions.BeenKilled<Anubis>(true);
            if (!beenKilled)
                NPC.OnGameEventClearedForTheFirstTime(-1);

            if (NPC.downedMoonlord && beenKilled)
                NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<AnubisForsakenTransition>());
            else
                NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<Legendscribe>());
        }

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

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter > 6)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;
            }
            if (internalAI[0] == 0)
            {
                if (internalAI[1] >= 240 && internalAI[1] < 320)
                {
                    if (NPC.frame.Y < frameHeight * 9)
                    {
                        NPC.frame.Y = 9;
                    }
                    if (NPC.frame.Y >= 10)
                    {
                        NPC.frame.Y = 10;
                    }
                }
                if (NPC.velocity.Y == 0)
                {
                    if (NPC.frame.Y < frameHeight * 4 || NPC.frame.Y > frameHeight * 8)
                    {
                        NPC.frame.Y = frameHeight * 4;
                    }
                }
                else
                {
                    if (NPC.frame.Y > frameHeight * 3)
                    {
                        NPC.frame.Y = 0;
                    }
                }
            }
            else
            {
                if (NPC.frame.Y > frameHeight * 3)
                {
                    NPC.frame.Y = 0;
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

            NPC.ai[3] = 39;
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Music = MusicLoader.GetMusicSlot("AAModClassic/Music/silence");
                if (NPC.velocity.Y == 0)
                {
                    if (internalAI[1]++ < 420)
                    {
                        if (!NPCExtensions.BeenKilled<Anubis>())
                        {
                            if (internalAI[1] == 60)
                            {
                                int activePlayers = 0;
                                foreach (Player p in Main.ActivePlayers)
                                    activePlayers++;
                                string s = activePlayers > 1 ? "Multiplayer" : "Singleplayer";
                                if (Main.netMode != NetmodeID.MultiplayerClient) 
                                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Anubis.Intro.1." + s), Color.Gold);
                            }

                            if (internalAI[1] == 150)
                            {
                                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Anubis.Intro.2"), Color.Gold);
                            }

                            if (internalAI[1] == 240)
                            {
                                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Anubis.Intro.3"), Color.Gold);
                            }

                            if (internalAI[1] == 320)
                            {
                                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Anubis.Intro.4"), Color.Gold);
                            }

                            if (internalAI[1] >= 410)
                            {
                                Music = MusicLoader.GetMusicSlot("AAModClassic/Music/Anubis");
                                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Anubis.Intro.5"), Color.Gold);
                                internalAI[0] = 1;
                                NPC.GetGlobalNPC<TitleGlobalNPC>().ShowTitle = true;
                                Teleport();
                                NPC.netUpdate = true;
                            }
                        }
                        else
                        {
                            Music = MusicLoader.GetMusicSlot("AAModClassic/Music/Anubis");
                            if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Anubis.Intro.Rematch"), Color.Gold);
                            internalAI[0] = 1;
                            NPC.GetGlobalNPC<TitleGlobalNPC>().ShowTitle = true;
                            Teleport();
                            NPC.netUpdate = true;
                        }
                    }
                }
            }
        }
    }
}