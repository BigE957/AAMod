using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic._Content._Misc._PostMoonlord.Items.Consumables;
using AAModClassic._Content.Void.World.Biomes;
using AAModClassic._CrossMod.CalamityMod.LoreItems;
using AAModClassic._Unofficial.Content.Void._PostMoonlord.Items._BossInfinityZero.BossStandard;
using AAModClassic._Unreleased.Content.Void._PostMoonLord.Items._BossInfinityZero;
using AAModClassic._Unreleased.Content.Void._PostMoonLord.Items._BossInfinityZero.BossStandard;
using AAModClassic._Unreleased.Content.Void._PostMoonLord.Items._BossInfinityZero.Weapons;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Music;
using AAModClassic.UI.Core;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
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
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace AAModClassic._Unreleased.Content.Void._PostMoonLord.NPCs.InfinityZero
{
	[AutoloadBossHead]
	public class InfinityZero : ModNPC
	{
        public static Asset<Texture2D> glowTex;

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Infinity Zero; Mechanical Malice");
            glowTex = ModContent.Request<Texture2D>(Texture + "_Glow");
            Main.npcFrameCount[NPC.type] = 4;
            NPCID.Sets.BossBestiaryPriority.Add(Type);

            NPCID.Sets.NPCBestiaryDrawModifiers value = new()
            {
                Position = new(0, 262),
                PortraitPositionYOverride = 240,
                Scale = 2f, // whenever we properly upscale the sprite this should just be 1x
                PortraitScale = 2f // ditto
            };
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
        }
		public override void SetDefaults()
		{
			NPC.damage = 0;
            NPC.width = 420; 			
            NPC.height = 342;
            NPC.npcSlots = 100;
            NPC.scale = 1.4f;
            NPC.dontTakeDamage = true;
			NPC.lifeMax = 2500000;
			NPC.knockBackResist = 0f;
			NPC.aiStyle = -1;
			NPC.value = Item.buyPrice(30, 0, 0, 0);
			NPC.boss = true;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.netAlways = true;
			NPC.chaseable = true;
			Music = MusicManagementSystem.MusicSlots["InfinityZero"];
			NPC.HitSound = SoundID.NPCHit44;
			NPC.DeathSound = new SoundStyle("AAModClassic/_Unreleased/Sounds/IZRoar");
            SpawnModBiomes = [ModContent.GetInstance<VoidBiome>().Type];
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
                new ColoredFlavorTextBestiaryInfoElement("Mods.AAModClassic.Bestiary.InfinityZero.1", AAColor.OblivionDialogue),
                new ColoredFlavorTextBestiaryInfoElement("Mods.AAModClassic.Bestiary.InfinityZero.2", AAColor.OblivionDialogue)
            ]);
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
            NPC.damage = (int)(NPC.damage * 1.1f); 
        }

        public float[] customAI = new float[4];
        public NPC Zero1 => zeroIndex1 == -1 ? null : Main.npc[zeroIndex1];
        private short zeroIndex1 = -1;
        public NPC Zero2 => zeroIndex2 == -1 ? null : Main.npc[zeroIndex2];
        private short zeroIndex2 = -1;
        public NPC Zero3 => zeroIndex3 == -1 ? null : Main.npc[zeroIndex3];
        private short zeroIndex3 = -1;
        public NPC Zero4 => zeroIndex4 == -1 ? null : Main.npc[zeroIndex4];
        private short zeroIndex4 = -1;
        public NPC Zero5 => zeroIndex5 == -1 ? null : Main.npc[zeroIndex5];
        private short zeroIndex5 = -1;
        public NPC Zero6 => zeroIndex6 == -1 ? null : Main.npc[zeroIndex6];
        private short zeroIndex6 = -1;
        //public NPC Core;

        private int testime = 60;
        public int CoreTimer = 600;

        public bool ZerosSpawned = false;
        public bool Reseting = false;
        public bool quarterHealth = false;
        public bool threeQuarterHealth = false;
        public bool HalfHealth = false;
        public bool tenthHealth = false;
        public bool OpenCore = false;
        public bool FirstCoreLine = false;

        //Server Side
        private int StormTimer = 0;

        //Client Side
        public int roarTimer = 200;
        public float auraPercent = 0f;
        public bool auraDirection = true;
        private int CoreFrame = 2;
        public bool[] roared = new bool[3];

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(customAI[0]);
            writer.Write(customAI[1]);
            writer.Write(customAI[2]);
            writer.Write(customAI[3]);

            writer.Write(zeroIndex1);
            writer.Write(zeroIndex2);
            writer.Write(zeroIndex3);
            writer.Write(zeroIndex4);
            writer.Write(zeroIndex5);
            writer.Write(zeroIndex6);

            writer.Write(testime);
            writer.Write(CoreTimer);

            writer.WriteFlags(ZerosSpawned, Reseting, tenthHealth, OpenCore, FirstCoreLine, quarterHealth, threeQuarterHealth, HalfHealth);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            customAI[0] = reader.ReadSingle();
            customAI[1] = reader.ReadSingle();
            customAI[2] = reader.ReadSingle();
            customAI[3] = reader.ReadSingle();

            zeroIndex1 = reader.ReadInt16();
            zeroIndex2 = reader.ReadInt16();
            zeroIndex3 = reader.ReadInt16();
            zeroIndex4 = reader.ReadInt16();
            zeroIndex5 = reader.ReadInt16();
            zeroIndex6 = reader.ReadInt16();

            testime = reader.ReadInt32();
            CoreTimer = reader.ReadInt32();

            reader.ReadFlags(out ZerosSpawned, out Reseting, out tenthHealth, out OpenCore, out FirstCoreLine, out quarterHealth, out threeQuarterHealth, out HalfHealth);
        }

        public override void AI()
		{
            NPC.timeLeft = 200;
            if (testime > 0)
                testime--;

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                StormTimer++;
                if (StormTimer >= 750)
                {
                    StormTimer = 0;
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, NPC.velocity.X * 2f, NPC.velocity.Y * 2f, ModContent.ProjectileType<InfinityZero_InfinityStorm>(), 60, 0, -1);
                }
            }

            if (!Main.dedServ)
			{
				int ThreeQuartersHealth = NPC.lifeMax * (int).75f;
				int HalfHealth = NPC.lifeMax * (int).5f;
				int QuarterHealth = NPC.lifeMax * (int).25f;
				
				if(roarTimer > -1) roarTimer--;
				if (NPC.life <= ThreeQuartersHealth && !roared[0])
				{
					roared[0] = true;
					roarTimer = 200;
				}
				if (NPC.life <= HalfHealth && !roared[1])
				{
					roared[1] = true;
					roarTimer = 200;
				}
				if (NPC.life <= QuarterHealth && !roared[2])
				{
					roared[2] = true;
					roarTimer = 200;
				}
				if (roarTimer == 180)
				{
					SoundEngine.PlaySound(new SoundStyle("AAModClassic/_Unreleased/Sounds/IZRoar"), NPC.Center);
				}
			}

            Player player = Main.player[NPC.target];
            float movementMax = 1.5f;
            bool teleporting = false;
            if (player != null && player.active && !player.dead)
            {
                if (NPC.DistanceSQ(player.Center) > 1440000) //trigger teleporting stuff
                {
                    teleporting = true;
                    NPC.dontTakeDamage = true;
                    NPC.alpha += 10;
                    if (NPC.alpha >= 255) //teleport, you're invisible!
                    {
                        NPC.alpha = 254; //don't let it hit 255 or it will despawn!
                        Vector2 tele = new Vector2(player.Center.X, player.Center.Y);
                        NPC.Center = tele;
                        NPC.netOffset = Vector2.Zero;
                        NPC.dontTakeDamage = false;
                        foreach (NPC n in Main.ActiveNPCs)
                            if (n.type == ModContent.NPCType<InfinityZeroHand1>() || n.type == ModContent.NPCType<InfinityZeroHand2>() || n.type == ModContent.NPCType<InfinityCore>())
                                n.Center = tele;
                        SoundEngine.PlaySound(new SoundStyle("AAModClassic/_Unreleased/Sounds/IZRoar"), NPC.Center);
                    }
                }
                else //you're close to the player, so make sure you're visible!
                {
                    NPC.dontTakeDamage = false; //to prevent you from being indestructible if the teleport is interrupted
                    NPC.alpha -= 25;
                    if (NPC.alpha <= 0)
                    {
                        NPC.alpha = 0;
                    }
                }
            }
            else
            {
                NPC.TargetClosest();
                player = Main.player[NPC.target];

                if (player != null || !player.active || player.dead) //All players dead. Fuck off.
                {
                    NPC.alpha += 10;
                    if (NPC.alpha >= 255)
                    {
                        NPC.active = false;
                        foreach (NPC n in Main.ActiveNPCs)
                            if (n.type == ModContent.NPCType<InfinityZeroHand1>() || n.type == ModContent.NPCType<InfinityZeroHand2>() || n.type == ModContent.NPCType<InfinityCore>())
                                n.active = false;
                        return;
                    }
                }
            }

            if (!teleporting && player != null && player.active && !player.dead)
            {
                NPC.alpha -= 10;
                if (NPC.alpha <= 0)
                {
                    NPC.alpha = 0;
                }
                movementMax = MathHelper.Lerp(1f, 4f, Math.Min(1f, Math.Max(0f, Vector2.Distance(NPC.Center, player.Center) / 1000f)));
            }

			//customAI is used here because the original ai and localAI are both used elsewhere. It is synced above.
            BaseAI.AIElemental(NPC, ref customAI, false, 0, false, false, 800f, 600f, 60, movementMax);
            if (!ZerosSpawned)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int latestNPC;

                    latestNPC = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y - 100, ModContent.NPCType<InfinityZeroHand2>(), NPC.whoAmI, NPC.whoAmI, ai3: 3);
                    zeroIndex4 = (short)latestNPC;
                    latestNPC = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y - 100, ModContent.NPCType<InfinityZeroHand1>(), latestNPC, NPC.whoAmI, ai3: 0);
                    zeroIndex1 = (short)latestNPC;

                    latestNPC = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y - 100, ModContent.NPCType<InfinityZeroHand2>(), latestNPC, NPC.whoAmI, ai3: 4);
                    zeroIndex5 = (short)latestNPC;
                    latestNPC = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y - 100, ModContent.NPCType<InfinityZeroHand1>(), latestNPC, NPC.whoAmI, ai3: 1);
                    zeroIndex2 = (short)latestNPC;

                    latestNPC = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y - 100, ModContent.NPCType<InfinityZeroHand2>(), latestNPC, NPC.whoAmI, ai3: 5);
                    zeroIndex6 = (short)latestNPC;
                    latestNPC = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y - 100, ModContent.NPCType<InfinityZeroHand1>(), latestNPC, NPC.whoAmI, ai3: 2);
                    zeroIndex3 = (short)latestNPC;

                    //latestNPC = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y - 100, ModContent.NPCType<InfinityCore>(), 0, NPC.whoAmI);
                    //Main.npc[latestNPC].realLife = NPC.whoAmI;
                    //Core = Main.npc[latestNPC];
                }
                ZerosSpawned = true;
                NPC.netUpdate = true;
            }
            
            if (testime == 0 && (Zero1 == null || Zero2 == null || Zero3 == null || Zero4 == null || Zero5 == null || Zero6 == null || !Zero1.active || !Zero2.active || !Zero3.active || !Zero4.active || !Zero5.active || !Zero6.active))
            {
                Reseting = true;
                testime = 60;
                NPC.netUpdate = true;
            }
            if ((Zero1 == null || !Zero1.active) && (Zero2 == null || !Zero2.active) && (Zero3 == null || !Zero3.active) && (Zero4 == null || !Zero4.active) && (Zero5 == null || !Zero5.active) && (Zero6 == null || !Zero6.active))
            {
                ZerosSpawned = false;
                NPC.netUpdate = true;
            }
        }

        public override void OnKill()
		{
            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<Oblivion>(), 0, 0);
            ZAAPlayer.IZKills += 1;

            if (!NPC.BeenKilled(true))
                NPC.OnGameEventClearedForTheFirstTime(-1);
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            int[] lootTable =
            {
                ModContent.ItemType<Genocide>(),
                ModContent.ItemType<Nova>(),
                ModContent.ItemType<SagittariusA>(),
                ModContent.ItemType<TotalDestruction>(),
                ModContent.ItemType<Annihilator>(),
                ModContent.ItemType<InfinityBlade>()
            };

            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<InfinityZeroTreasureBag>()));

            npcLoot.AddLoreItemDrop<InfinityZero>(ModContent.ItemType<InfinityZeroLore>());

            LeadingConditionRule unofficialRule = new(new AAConditions.UnofficialNotExpert());

            unofficialRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<InfinityZeroMask>(), 7));

            npcLoot.Add(unofficialRule);

            LeadingConditionRule masterMode = new(new AAConditions.RevOrMaster());

            masterMode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<InfinityZeroRelic>()));

            npcLoot.Add(masterMode);

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<InfinityZeroTrophy>(), 10));

            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Infinitium>(), 1, 25, 35));
            notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, lootTable));

            LeadingConditionRule expertRule = new(new Conditions.IsExpert());

            expertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<EXSoul>()));

            npcLoot.Add(notExpertRule);
            npcLoot.Add(expertRule);
        }

        public override void BossLoot(ref int potionType)
		{
			potionType = ModContent.ItemType<GrandHealingPotion>();
        }
		
		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			scale = 3f;
			return null;
		}
		
		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return false;
		}
		
        public override void HitEffect(NPC.HitInfo hit)
		{
            if (OpenCore)
                NPC.defense = 50;
            else if (NPC.life <= NPC.lifeMax / 10)
                NPC.defense = 175;
            else if (NPC.life <= NPC.lifeMax / 2)
                NPC.defense = 225;
            else
                NPC.defense = 275;

            if (NPC.life <= NPC.lifeMax / 4 * 3 && threeQuarterHealth == false)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Health.75"), new Color(158, 3, 32));
                threeQuarterHealth = true;
                roarTimer = 200;
            }
            if (NPC.life <= NPC.lifeMax / 2 && HalfHealth == false)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Health.50"), new Color(158, 3, 32));
                HalfHealth = true;
                roarTimer = 200;
            }
            if (NPC.life <= NPC.lifeMax / 4 && quarterHealth == false)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Health.25"), new Color(158, 3, 32));
                quarterHealth = true;
                roarTimer = 200;
            }
            if (NPC.life <= NPC.lifeMax / 10 && !tenthHealth)
            {
                tenthHealth = true;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Health.10"), new Color(158, 3, 32));
                roarTimer = 200;
            }
            if (NPC.ai[3] == 6)
            {
                CoreTimer--;
                OpenCore = true;
                if (Main.netMode != NetmodeID.MultiplayerClient && !FirstCoreLine)
                {
                    FirstCoreLine = true;
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Zeroes.Defeated"), new Color(158, 3, 32));
                }
                if (CoreTimer <= 0)
                {
                    if(Main.netMode != NetmodeID.MultiplayerClient)
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Zeroes.Revived"), new Color(158, 3, 32));
                    NPC.ai[3] = 0;
                    OpenCore = false;
                    CoreTimer = 600;
                    foreach(NPC n in Main.npc)
                    {
                        if (n.ModNPC == null || n.ModNPC is not InfinityZeroHand1 hand)
                            continue;
                        hand.RepairMode = false;
                    }
                }

            }
            if (!AAConfigClient.Instance.DisablePinchThemes && NPC.life <= NPC.lifeMax / 10)
            {
                Music = MusicManagementSystem.MusicSlots["Superancients_Pinch"];
            }
			if (NPC.life <= 0)
			{
                if (!Main.dedServ)
                {
                    float randomSpread = Main.rand.Next(-50, 50) / 100;
                    Gore.NewGore(NPC.GetSource_FromAI(), NPC.Center, NPC.velocity * randomSpread * Main.rand.NextFloat(), Mod.Find<ModGore>("IZGore1").Type, 1.4f);
                    Gore.NewGore(NPC.GetSource_FromAI(), NPC.Center, NPC.velocity * randomSpread * Main.rand.NextFloat(), Mod.Find<ModGore>("IZGore2").Type, 1.4f);
                    Gore.NewGore(NPC.GetSource_FromAI(), NPC.Center, NPC.velocity * randomSpread * Main.rand.NextFloat(), Mod.Find<ModGore>("IZGore3").Type, 1.4f);
                    Gore.NewGore(NPC.GetSource_FromAI(), NPC.Center, NPC.velocity * randomSpread * Main.rand.NextFloat(), Mod.Find<ModGore>("IZGore4").Type, 1.4f);
                    Gore.NewGore(NPC.GetSource_FromAI(), NPC.Center, NPC.velocity * randomSpread * Main.rand.NextFloat(), Mod.Find<ModGore>("IZGore5").Type, 1.4f);
                }

                NPC.position.X = NPC.position.X + NPC.width / 2;
				NPC.position.Y = NPC.position.Y + NPC.height / 2;
				NPC.width = 400;
				NPC.height = 350;
				NPC.position.X = NPC.position.X - NPC.width / 2;
				NPC.position.Y = NPC.position.Y - NPC.height / 2;
				for (int num621 = 0; num621 < 60; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, ModContent.DustType<Dusts.VoidDust_Unreleased>(), 0f, 0f, 100, default, 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.NextBool(2))
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 90; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, ModContent.DustType<Dusts.VoidDust_Unreleased>(), 0f, 0f, 100, default, 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.CopperCoin, 0f, 0f, 100, default, 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}

        public static void DrawCore(SpriteBatch spriteBatch, string coreTex, NPC core, Color drawColor, bool DrawUnder)
        {
            if (core != null && core.active)
            {
                Texture2D tex = ModContent.Request<Texture2D>(coreTex).Value;
                spriteBatch.Draw(tex, core.Center - Main.screenPosition, core.frame, drawColor, core.rotation, core.frame.Size() * 0.5f, core.scale, core.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : 0, 0);
            }
        }

        public static readonly Color infinityGlowRed = new(233, 53, 53);

        public static Color GetGlowAlpha(bool aura) => (aura ? infinityGlowRed : Color.White) * (Main.mouseTextColor / 255f);

        public static Color GetRedAlpha() => new Color(233, 53, 53) * (Main.mouseTextColor / 255f);

		public static Vector2 GetConnectionPoint(int handType)
		{
			float offsetX = 0, offsetY = 0;
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                switch (handType)
                {
                    case 0: offsetX = -78; offsetY = -42; break;
                    case 1: offsetX = -48; offsetY = -6; break;
                    case 2: offsetX = -62; offsetY = 12; break;
                    case 3: offsetX = 78; offsetY = -42; break;
                    case 4: offsetX = 48; offsetY = -6; break;
                    case 5: offsetX = 62; offsetY = 12; break;
                }
            }
            else
            {
                switch (handType)
                {
                    case 0: offsetX = -62; offsetY = -80; break;
                    case 1: offsetX = -32; offsetY = -44; break;
                    case 2: offsetX = -46; offsetY = -20; break;
                    case 3: offsetX = 62; offsetY = -80; break;
                    case 4: offsetX = 32; offsetY = -44; break;
                    case 5: offsetX = 46; offsetY = -20; break;
                }
            }
			offsetX *= 2f;
			offsetY *= 2f;
			return new Vector2(offsetX, offsetY);
		}

        public override void DrawBehind(int index)
        {
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                NPC.hide = true;
                Main.instance.DrawCacheNPCsMoonMoon.Add(index);
            }
            else
                NPC.hide = false;
        }

        public override void FindFrame(int frameHeight)
        {
            bool unofficial = WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial);
            NPC.frame.Height = unofficial ? 455 : frameHeight;
            if (!NPC.IsABestiaryIconDummy && roarTimer > -1)
            {
                NPC.frame.Y = unofficial ? NPC.frame.Height : 2 * NPC.frame.Height;
            }
            else
            {
                NPC.frame.Y = 0;
            }

            int coreFrameRate = 5;

            if (!unofficial)
            {
                if (OpenCore)
                    NPC.frame.Y += NPC.frame.Height;
            }
            else
            {
                NPC.frame.Y += NPC.frame.Height * 2;
                if (OpenCore)
                {
                    if (CoreFrame != -1)
                    {
                        if (NPC.frameCounter >= coreFrameRate)
                        {
                            CoreFrame--;
                            NPC.frameCounter = 0;
                        }
                        NPC.frameCounter++;
                    }
                    else
                        NPC.frameCounter = 0;
                }
                else
                {
                    if (CoreFrame != 2)
                    {
                        if (NPC.frameCounter >= coreFrameRate)
                        {
                            CoreFrame++;
                            NPC.frameCounter = 0;
                        }
                        NPC.frameCounter++;
                    }
                    else
                        NPC.frameCounter = 0;
                }
            }
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

            bool unofficialWorld = WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial);
            //if(!unofficialWorld)
            //    DrawCore(spriteBatch, ModContent.GetInstance<InfinityCore>().Texture, Core, AAColor.Oblivion, false);

            string respritePath = Texture + "_Resprite";
            Texture2D texture = !unofficialWorld ? TextureAssets.Npc[NPC.type].Value : ModContent.Request<Texture2D>(respritePath).Value;
            Texture2D glow = !unofficialWorld ? glowTex.Value : ModContent.Request<Texture2D>(respritePath + "_Glow").Value;

            if (tenthHealth)
            {
                spriteBatch.Draw(texture, NPC.Center - screenPos, NPC.frame, Color.White, NPC.rotation, unofficialWorld ? NPC.frame.Size() * 0.5f : Vector2.Zero, NPC.scale, 0, 0);
                BaseDrawing.DrawAura(spriteBatch, glow, 0, NPC, auraPercent, 1f, 0f, 0f, GetRedAlpha(), unofficialWorld);
                BaseDrawing.DrawTexture(spriteBatch, glow, 0, NPC, GetRedAlpha(), unofficialWorld);
                if(unofficialWorld)
                {
                    if (CoreFrame != -1)
                    {
                        Texture2D core = ModContent.Request<Texture2D>(respritePath + "_Core").Value;
                        Rectangle frame = core.Frame(3, 1, CoreFrame, 0);
                        spriteBatch.Draw(core, NPC.Center - new Vector2(3, 15) - Main.screenPosition, frame, Color.White, NPC.rotation, frame.Size() * 0.5f, NPC.scale, 0, 0);
                    }
                    Texture2D eye = ModContent.Request<Texture2D>(respritePath + "_Eye").Value;
                    float maxDist = 9f;
                    Vector2 drawPos = NPC.Center - new Vector2(7, 190);
                    Vector2 eyeOffset = drawPos.DirectionTo(Main.LocalPlayer.Center) * drawPos.Distance(Main.LocalPlayer.Center) / 48f;
                    drawPos += new Vector2(MathHelper.Clamp(eyeOffset.X, -maxDist, maxDist), MathHelper.Clamp(eyeOffset.Y, -maxDist, maxDist));
                    BaseDrawing.DrawAura(spriteBatch, eye, 0, drawPos, eye.Width, eye.Height, auraPercent, 1, NPC.scale, 0f, 1, 1, eye.Frame(), 0, 0, GetRedAlpha(), true);
                    BaseDrawing.DrawTexture(spriteBatch, eye, 0, drawPos, eye.Width, eye.Height, NPC.scale, 0f, 1, 1, eye.Frame(), GetRedAlpha(), true);
                }
            }
            else
            {
                Color color = BaseUtility.ColorClamp(BaseDrawing.GetNPCColor(NPC, NPC.Center + new Vector2(0, -30), true, 0f), GetGlowAlpha(true));
                spriteBatch.Draw(texture, NPC.Center - screenPos, NPC.frame, color, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, 0, 0);
                BaseDrawing.DrawAura(spriteBatch, glow, 0, NPC, auraPercent, 1f, 0f, 0f, GetGlowAlpha(true), true);
                spriteBatch.Draw(glow, NPC.Center - screenPos, NPC.frame, GetGlowAlpha(false), NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, 0, 0);
                if (unofficialWorld)
                {
                    if (CoreFrame != -1)
                    {
                        Texture2D core = ModContent.Request<Texture2D>(respritePath + "_Core").Value;
                        Rectangle frame = core.Frame(3, 1, CoreFrame, 0);
                        spriteBatch.Draw(core, NPC.Center - new Vector2(3, 15) - Main.screenPosition, frame, color, NPC.rotation, frame.Size() * 0.5f, NPC.scale, 0, 0);
                    }
                    Texture2D eye = ModContent.Request<Texture2D>(respritePath + "_Eye").Value;
                    float maxDist = 8.5f;
                    Vector2 drawPos = NPC.Center - (new Vector2(0, 130) * NPC.scale);
                    Vector2 lookAt = NPC.IsABestiaryIconDummy ? Main.MouseScreen : Main.LocalPlayer.Center;
                    Vector2 eyeOffset = drawPos.DirectionTo(lookAt) * drawPos.Distance(lookAt) / 48f;
                    drawPos += new Vector2(MathHelper.Clamp(eyeOffset.X, -maxDist, maxDist), MathHelper.Clamp(eyeOffset.Y, -maxDist, maxDist));
                    BaseDrawing.DrawAura(spriteBatch, eye, 0, drawPos - new Vector2(7, 9), eye.Width, eye.Height, auraPercent, 1, NPC.scale, 0f, 1, 1, eye.Frame(), 0, 0, GetGlowAlpha(false), unofficialWorld);
                    spriteBatch.Draw(eye, drawPos - screenPos, null, Color.White, NPC.rotation, eye.Size() * 0.5f, NPC.scale, 0, 0);
                }
            }

            //bottom arms
            DrawZero(spriteBatch, Zero6);
	        DrawZero(spriteBatch, Zero3);	
            //middle arms
	        DrawZero(spriteBatch, Zero5);		
            DrawZero(spriteBatch, Zero2);
			//top arms
			DrawZero(spriteBatch, Zero4);		
			DrawZero(spriteBatch, Zero1);
            return false;
        }

        private readonly float[] currentCurveIntensities = new float[6];

        public void DrawZero(SpriteBatch spriteBatch, NPC Zero)
        {
            if (Zero != null && Zero.active && Zero.ModNPC != null && (Zero.ModNPC is InfinityZeroHand1 || Zero.ModNPC is InfinityZeroHand2))
            {
				InfinityZeroHand1 handNPC = (InfinityZeroHand1)Zero.ModNPC;
                Vector2 start = new Vector2(NPC.Center.X, NPC.Center.Y) + GetConnectionPoint(handNPC.HandType);
                Vector2 end = Zero.Center;
                if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                {
                    string texPath = Texture + "_Resprite";
                    Texture2D ArmTex2D = ModContent.Request<Texture2D>(texPath + "_Arm").Value;

                    Vector2 direction = end - start;
                    float curveIntensity = MathHelper.Clamp(8000f / direction.Length(), 0f, 200f);
                    float intensityDelta = MathHelper.Clamp(curveIntensity - currentCurveIntensities[handNPC.HandType], -30, 30);
                    currentCurveIntensities[handNPC.HandType] = currentCurveIntensities[handNPC.HandType] + intensityDelta;

                    Vector2 perpindicular = Vector2.UnitY * currentCurveIntensities[handNPC.HandType];

                    Vector2 controlPoint1 = start + (direction * 0.25f) + perpindicular;
                    Vector2 controlPoint2 = start + (direction * 0.75f) + perpindicular;

                    BezierCurve path = new(start, controlPoint1, controlPoint2, end);

                    float dist = start.Distance(controlPoint1) + controlPoint1.Distance(controlPoint2) + controlPoint2.Distance(end);

                    int count = (int)(dist / (ArmTex2D.Height - 7));
                    for (int i = 0; i < count; i++)
                    {
                        Rectangle frame = ArmTex2D.Frame(4, 1, i >= 3 ? 0 : 3 - i, 0);
                        float ratio = i / (float)count;
                        float nextRatio = (i + 1) / (float)count;
                        Vector2 myStart = i == 0 ? start : path.Evaluate(ratio);
                        Vector2 myEnd = i == count - 1 ? end : path.Evaluate(nextRatio);
                        float rotation = myStart.AngleTo(myEnd) - MathHelper.PiOver2;
                        float scale = 1f;// (myStart.Distance(myEnd) / ArmTex2D.Height) + 0.25f;
                        spriteBatch.Draw(ArmTex2D, myStart - Main.screenPosition, frame, Lighting.GetColor(((myStart + myEnd) / 2f).ToTileCoordinates()), rotation, frame.Size() * 0.5f, new Vector2(1, scale), 0, 0);
                    }
                }
                else
                {
                    string ArmTex = Texture + "_Arm";
                    Texture2D ArmTex2D = ModContent.Request<Texture2D>(ArmTex).Value;

                    Vector2 dir = start.DirectionTo(end);
                    float length = Vector2.Distance(start, end);
                    for (int i = 0; i < length; i += (ArmTex2D.Height - 10))
                    {
                        Vector2 drawPos = start + dir * i;
                        spriteBatch.Draw(ArmTex2D, drawPos - Main.screenPosition, null, Lighting.GetColor(drawPos.ToTileCoordinates()), dir.ToRotation() - MathHelper.PiOver2, ArmTex2D.Size() * 0.5f, 1f, 0, 0);
                    }
                }
            }
        }
    }
	
}
