using AAModClassic._Content.Mire.___PreHardmode.NPCs.__BossHydra;
using AAModClassic._Content.Void.___PreHardmode.Items._BossSagittarius.Weapons;
using AAModClassic._Content.Void.___PreHardmode.NPCs;
using AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Ammo;
using AAModClassic._Content.Void._PostMoonlord.Items._BossZero.BossStandard;
using AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Pets;
using AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Tools;
using AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Weapons;
using AAModClassic._Content.Void._PostMoonlord.Items.Materials;
using AAModClassic._Content.Void._PostMoonlord.NPCs.__BossZero.Awakened;
using AAModClassic._Content.Void.World.Biomes;
using AAModClassic._CrossMod.CalamityMod.LoreItems;
using AAModClassic._Removed.Content._Tinker._PostMoonlord.Items.Accessories;
using AAModClassic.Achievements;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Effects;
using AAModClassic.Globals;
using AAModClassic.Music;
using AAModClassic.UI.Core;
using AAModClassic.UI.Titles;
using AAModClassic.Utilities;
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
using Terraria.GameContent.UI.BigProgressBar;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items.AAConditions;


namespace AAModClassic._Content.Void._PostMoonlord.NPCs.__BossZero
{
    [AutoloadBossHead]
    public class Zero : ModNPC
    {
        public int damage = 0;

        public static Asset<Texture2D> Glowmask;
        public static Asset<Texture2D> ShieldTex;
        public static Asset<Texture2D> ShieldRing;
        public static Asset<Texture2D> ShieldRingGlowmask;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Zero");
            Main.npcFrameCount[NPC.type] = 5;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new()
            {
                PortraitPositionYOverride = 20,
                PortraitScale = 0.9f
            };
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;

            Glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
            ShieldTex = ModContent.Request<Texture2D>(Texture + "_Shield");
            ShieldRing = ModContent.Request<Texture2D>(Texture + "_ShieldRing");
            ShieldRingGlowmask = ModContent.Request<Texture2D>(Texture + "_ShieldRing_Glow");
            NPCID.Sets.BossBestiaryPriority.Add(Type);
        }

        public override void SetDefaults()
        {
            NPC.damage = 50;
            NPC.defense = 150;
            NPC.lifeMax = 350000;
            if (Main.expertMode)
            {
                NPC.value = 0;
            }
            else
            {
                NPC.value = Item.buyPrice(0, 30, 0, 0);
            }
            NPC.width = 198;
            NPC.height = 198;
            NPC.aiStyle = -1;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCHit4;
            NPC.noGravity = true;
            Music = MusicManagementSystem.MusicSlots["Zero"];
            NPC.noTileCollide = true;
            NPC.knockBackResist = -1f;
            NPC.boss = true;
            NPC.friendly = false;
            NPC.npcSlots = 100;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
            NPC.lavaImmune = true;
            NPC.netAlways = true;
            SceneEffectPriority = SceneEffectPriority.BossHigh;
            SpawnModBiomes = [ModContent.GetInstance<VoidBiome>().Type];
            NPC.BossBar = new ZeroBossBar();
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
                new ColoredFlavorTextBestiaryInfoElement("Mods.AAModClassic.Bestiary.Zero", AAColor.OblivionDialogue)
            ]);
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.damage = (int)(NPC.damage * .7f);
            NPC.lifeMax = (int)(NPC.lifeMax * 0.5f * balance);
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= (int)(NPC.lifeMax * .66f) && !RespawnArms1 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                WeaponCount += 1;
                NPC.ai[1] = 0;
                RespawnArms1 = true;

                RespawnArms();
                if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Zero.Rearm"), Color.Red, false);
                NPC.netUpdate = true;
            }
            if (NPC.life <= (int)(NPC.lifeMax * .33f) && !RespawnArms2 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                WeaponCount += 1;
                NPC.ai[1] = 0;
                RespawnArms2 = true;
                RespawnArms();
                if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Zero.Rearm"), Color.Red, false);
                NPC.netUpdate = true;
            }

            if (NPC.life <= 0 && NPC.type == ModContent.NPCType<Zero>())
            {
                if (!Main.dedServ)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ZeroGore1").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ZeroGore1").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ZeroGore1").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ZeroGore1").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ZeroGore2").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ZeroGore3").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ZeroGore3").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ZeroGore3").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ZeroGore3").Type, 1f);
                }
                if (!Main.expertMode)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Zero.Defeat.NotExpert"), Color.Red.R, Color.Red.G, Color.Red.B);
                }
            }
        }

        bool hasArms = false;
        public void RespawnArms()
        {
            hasArms = NPC.AnyNPCs(ModContent.NPCType<ZeroVoidStar>()) ||
                   NPC.AnyNPCs(ModContent.NPCType<ZeroGigataser>()) ||
                   NPC.AnyNPCs(ModContent.NPCType<ZeroRealityCannon>()) ||
                   NPC.AnyNPCs(ModContent.NPCType<ZeroRiftShredder>()) ||
                   NPC.AnyNPCs(ModContent.NPCType<ZeroNeutralizer>()) ||
                   NPC.AnyNPCs(ModContent.NPCType<ZeroOmegaVolley>()) ||
                   NPC.AnyNPCs(ModContent.NPCType<ZeroNovaFocus>()) ||
                   NPC.AnyNPCs(ModContent.NPCType<ZeroGenocideCannon>());

            if (Main.netMode != NetmodeID.MultiplayerClient && !hasArms)
            {
                NPC.ai[0] = 10f;

                List<int> choices =
                [
                    ModContent.NPCType<ZeroGenocideCannon>(),
                    ModContent.NPCType<ZeroNeutralizer>(),
                    ModContent.NPCType<ZeroNovaFocus>(),
                    ModContent.NPCType<ZeroOmegaVolley>(),
                    ModContent.NPCType<ZeroRealityCannon>(),
                    ModContent.NPCType<ZeroRiftShredder>(),
                    ModContent.NPCType<ZeroGigataser>(),
                    ModContent.NPCType<ZeroVoidStar>(),
                ];

                for (int m = 0; m < WeaponCount; m++)
                {
                    int choice = Main.rand.Next(choices.Count);

                    int npcID = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, choices[choice], 0, m);
                    choices.Remove(choice);
                    Main.npc[npcID].Center = NPC.Center;
                    Main.npc[npcID].velocity = new Vector2(MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()), MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()));
                    Main.npc[npcID].velocity *= 8f;
                    Main.npc[npcID].netUpdate2 = true; Main.npc[npcID].netUpdate = true;
                }

                internalAI[3] = 1;
                Distance = 0;
                NPC.netUpdate = true;
            }
        }

        public override bool PreKill()
        {
            if (Main.expertMode)
                NPC.boss = false;
            return true;
        }

        public override void OnKill()
        {
            if (Main.expertMode)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) ChatUtils.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Zero.Defeat.Expert"), Color.Red.R, Color.Red.G, Color.Red.B);
                if (NPC.BeenKilled(true))
                {
                    int z = NPC.NewNPC(NPC.GetSource_Death(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<ZeroA>(), 0, 0, 0, 0, 0, NPC.target);
                    Main.npc[z].Center = NPC.Center;

                    int b = Projectile.NewProjectile(NPC.GetSource_Death(), NPC.Center.X, NPC.Center.Y, 0f, 0f, ModContent.ProjectileType<ShockwaveBoom>(), 0, 1, Main.myPlayer, 0, 0);
                    Main.projectile[b].Center = NPC.Center;
                }
                else
                {
                    int z = NPC.NewNPC(NPC.GetSource_Death(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<ZeroTransition>(), 0, 0, 0, 0, 0, NPC.target);
                    Main.npc[z].Center = NPC.Center;
                }

                NPC.netUpdate = true;
            }
            else
            {
                if (!NPC.BeenKilled(true))
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) 
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Zero.Defeat.Status"), Color.PaleVioletRed);
                    VoidSky.Alpha = 0f;
                }
                else if (VoidSky.Alpha == -1)
                    VoidSky.Alpha = 0;

                if (NPC.playerInteraction[Main.myPlayer])
                    ZeroKilled.Condition.Complete();
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ApocalyptitePlate>(), 1, 2, 4));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ZeroTrophy>(), 10));

            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

            notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(10, [ModContent.ItemType<ZeroCore>(), ModContent.ItemType<PortaProbe>()]));

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ZeroMask>(), 7));

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<UnstableSingularity>(), 1, 25, 35));

            int[] lootTable =
            {
                ModContent.ItemType<UnstablePowerCell>(),
                ModContent.ItemType<SingularityArrow>(),
                ModContent.ItemType<TheVortex>(),
                ModContent.ItemType<EventHorizon>(),
                ModContent.ItemType<RealityCannon>(),
                ModContent.ItemType<RiftShredder>(),
                ModContent.ItemType<VoidStar>(),
                ModContent.ItemType<BrokenZeroWeapon>(),
                ModContent.ItemType<DoomsdayTerratool>(),
                ModContent.ItemType<DoomPortal>(),
                ModContent.ItemType<Gigataser>(),
                ModContent.ItemType<OmegaVolley>(),
                ModContent.ItemType<GenocideCannon>(),
                ModContent.ItemType<BlackHoleBlaster>(),
                ModContent.ItemType<Neutralizer>(),
                ModContent.ItemType<NovaFocus>()
            };
            notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, lootTable));

            //TODO: BRING HIM BACK PLEASEEEEEEE
            //if (Main.rand.Next(50) == 0 && AAWorld.downedAllAncients)
            //    Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<RealityStone>());

            LeadingConditionRule loreCondition = new(new LoreItemDropCondition<Zero>());
            notExpertRule.OnSuccess(loreCondition.OnSuccess(new PerPlayerDropRule(ModContent.ItemType<ZeroLore>(), 1)));

            npcLoot.Add(notExpertRule);

            LeadingConditionRule anceintsDownAndRemoved = new(new PostLateAncientsAndRemovedWorldAndNotExpert());

            anceintsDownAndRemoved.OnSuccess(ItemDropRule.Common(ModContent.ItemType<RealityStone>(), 50));

            npcLoot.Add(anceintsDownAndRemoved);
        }

        public override void BossLoot(ref int potionType)
        {
            if (!Main.expertMode)
                potionType = ItemID.SuperHealingPotion;   //boss drops
            else
                potionType = 0;
        }

        public static Color GetGlowAlpha()
        {
            return AAColor.ZeroShield * (Main.mouseTextColor / 255f);
        }

        public int NormalFrame;
        public int SwitchingModesFrame;

        public override void FindFrame(int frameHeight)
        {
            int frameWidth = TextureAssets.Npc[NPC.type].Width() / 4;
            NPC.frame.Width = frameWidth;

            if (NPC.IsABestiaryIconDummy)
            {
                NPC.ai[2]++;
                if (NPC.ai[2] >= 320)
                {
                    NPC.ai[2] = 0;
                    if (NPC.ai[1] == 1)
                        NPC.ai[3] = 3;
                    else
                        NPC.ai[3] = 0;
                }

                if (NPC.ai[1] <= 0)
                    NPC.ai[1] = 1;
            }


            if (NPC.ai[1] == 1)
            {
                if (NPC.ai[3] == 3)
                {
                    NPC.frameCounter = 0;
                    NPC.ai[1]++;
                }
                else
                {
                    NPC.frameCounter++;
                    if (NPC.frameCounter > 4)
                    {
                        NormalFrame++;
                        NPC.frameCounter = 0;
                    }
                    if (NormalFrame >= 5)
                    {
                        NormalFrame = 0;
                    }
                }
            }
            else if (NPC.ai[1] == 2 && NPC.ai[2] >= 5)
            {
                NPC.frameCounter++;
                if (NPC.frameCounter > 4)
                {
                    SwitchingModesFrame++;
                    NPC.frameCounter = 0;
                }
                if (SwitchingModesFrame >= 5)
                {
                    SwitchingModesFrame = 0;
                    NPC.ai[1]++;
                }
            }
            else if (NPC.ai[1] == 2 && NPC.ai[2] < 5)
            {
                NPC.frameCounter++;
                if (NPC.frameCounter > 4)
                {
                    NormalFrame++;
                    NPC.frameCounter = 0;
                }
                if (NormalFrame >= 5)
                {
                    NormalFrame = 0;
                }
            }
            else if (NPC.ai[1] == 3)
            {
                if (NPC.ai[3] == 3)
                {
                    NPC.frameCounter++;
                    if (NPC.frameCounter > 4)
                    {
                        NormalFrame++;
                        NPC.frameCounter = 0;
                    }
                    if (NormalFrame >= 5)
                    {
                        NormalFrame = 0;
                    }
                }
                else
                {
                    NPC.frameCounter++;
                    if (NPC.frameCounter > 4)
                    {
                        SwitchingModesFrame++;
                        NPC.frameCounter = 0;
                    }
                    if (SwitchingModesFrame >= 5)
                    {
                        SwitchingModesFrame = -1;
                        NPC.ai[1] = 1;
                    }
                }
            }

            NPC.frame.Y = NormalFrame * frameHeight;
            NPC.frame.X = frameWidth * (int)NPC.ai[1];

            if ((NPC.ai[1] == 2 && NPC.ai[2] >= 5) || (NPC.ai[1] == 3 && NPC.ai[3] != 3))
            {
                NPC.frame.Y = SwitchingModesFrame * frameHeight;
                NPC.frame.X = frameWidth * 2;
                if (NPC.ai[1] == 3 && NPC.ai[3] != 3)
                {
                    NPC.frame.Y = TextureAssets.Npc[NPC.type].Height() - NPC.frame.Y;
                }
            }
            else if (NPC.ai[1] == 2 && NPC.ai[2] < 5)
            {
                NPC.frame.Y = NormalFrame * frameHeight;
                NPC.frame.X = frameWidth;
            }

            if (NPC.ai[1] == 0)
            {
                NPC.frame.X = 0;
                NPC.frame.Y = 0;
                NPC.frameCounter = 0;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D glowTex = Glowmask.Value;
            Texture2D Shield = ShieldTex.Value;
            Texture2D Ring = ShieldRing.Value;
            Texture2D RingGlow = ShieldRingGlowmask.Value;

            Main.spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos, NPC.frame, drawColor * ((255 - NPC.alpha) / 255f), NPC.rotation, NPC.frame.Size() / 2, NPC.scale, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(glowTex, NPC.Center - screenPos, NPC.frame, AAColor.COLOR_WHITEFADE1, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, NPC.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);

            if (ShieldScale > 0)
            {
                BaseDrawing.DrawTexture(spriteBatch, Shield, 0, NPC.position, NPC.width, NPC.height, ShieldScale, 0, 0, 1, new Rectangle(0, 0, Shield.Width, Shield.Height), GetGlowAlpha(), true);
                BaseDrawing.DrawTexture(spriteBatch, Ring, 0, NPC.position, NPC.width, NPC.height, ShieldScale * 2, RingRoatation, 0, 1, new Rectangle(0, 0, Ring.Width, Ring.Height), drawColor, true);
                BaseDrawing.DrawTexture(spriteBatch, RingGlow, 0, NPC.position, NPC.width, NPC.height, ShieldScale * 2, RingRoatation, 0, 1, new Rectangle(0, 0, Ring.Width, Ring.Height), AAColor.COLOR_WHITEFADE1, true);
            }

            return false;
        }

        public int MinionTimer = 0;
        public float Distance = 0;
        public float[] internalAI = new float[5];

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(internalAI[0]);
                writer.Write(internalAI[1]);
                writer.Write(internalAI[2]);
                writer.Write(internalAI[3]);
                writer.Write(internalAI[4]);
                writer.Write(Distance);
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
                internalAI[4] = reader.ReadSingle();
                Distance = reader.ReadSingle();
            }
        }


        public bool saythelinezero = false;
        public float ShieldScale = 0.5f;
        public float RingRoatation = 0;
        public int WeaponCount = Main.expertMode ? 6 : 4;
        bool RespawnArms1;
        bool RespawnArms2;

        public override void AI()
        {
            NPC.GetGlobalNPC<TitleGlobalNPC>().ShowTitle = true;

            damage = 25;

            if (NPC.ai[0] > 0)
            {
                NPC.ai[0]--;
            }

            NPC.TargetClosest();

            if (Main.netMode != NetmodeID.MultiplayerClient && internalAI[3] == 0 && NPC.ai[1] == 0)
            {
                RespawnArms();
                NPC.netUpdate = true;
            }

            if (NPC.AnyNPCs(ModContent.NPCType<ZeroVoidStar>()) ||
                NPC.AnyNPCs(ModContent.NPCType<ZeroGigataser>()) ||
                NPC.AnyNPCs(ModContent.NPCType<ZeroRealityCannon>()) ||
                NPC.AnyNPCs(ModContent.NPCType<ZeroRiftShredder>()) ||
                NPC.AnyNPCs(ModContent.NPCType<ZeroNeutralizer>()) ||
                NPC.AnyNPCs(ModContent.NPCType<ZeroOmegaVolley>()) ||
                NPC.AnyNPCs(ModContent.NPCType<ZeroNovaFocus>()) ||
                NPC.AnyNPCs(ModContent.NPCType<ZeroGenocideCannon>()))
            {
                NPC.ai[1] = 0;
            }
            else
            {
                if (NPC.ai[1] == 0)
                {
                    NPC.ai[1] = 1;
                }
            }

            if (Distance < 160f)
            {
                Distance += 5f;
            }
            else
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Distance = 160f;
                    NPC.netUpdate = true;
                }
            }

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                AAWorld.zeroUS = false;
            }

            Player player = Main.player[NPC.target];

            RingRoatation += 0.03f;

            if (player.dead || !player.active || Math.Abs(NPC.position.X - Main.player[NPC.target].position.X) > 6000f || Math.Abs(NPC.position.Y - Main.player[NPC.target].position.Y) > 6000f)
            {
                NPC.TargetClosest();
                if (player.dead || !player.active || Math.Abs(NPC.position.X - Main.player[NPC.target].position.X) > 6000f || Math.Abs(NPC.position.Y - Main.player[NPC.target].position.Y) > 6000f)
                {
                    NPC.Transform(ModContent.NPCType<ZeroDeactivated>());
                }
                return;
            }
            if (ShieldScale < .5f)
            {
                ShieldScale += .05f;
            }

            if (ShieldScale > .5f)
            {
                ShieldScale = .5f;
            }

            if (internalAI[1] == 0)
            {
                NPC.velocity.Y += 0.003f;
                if (NPC.velocity.Y > .3f)
                {
                    internalAI[1] = 1f;
                    NPC.netUpdate = true;
                }
            }
            else if (internalAI[1] == 1)
            {
                NPC.velocity.Y -= 0.003f;
                if (NPC.velocity.Y < -.3f)
                {
                    internalAI[1] = 0f;
                    NPC.netUpdate = true;
                }
            }

            if (NPC.ai[1] == 0)
            {
                NPC.dontTakeDamage = true;
                NPC.chaseable = false;
                NPC.damage = 0;
                saythelinezero = false;
            }
            else
            {
                NPC.dontTakeDamage = false;
                NPC.chaseable = true;
                NPC.damage = 160;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.ai[2]++;
                }

                if (NPC.ai[3] == 3)
                {
                    NPC.defense = 75;
                }
                else
                {
                    NPC.defense = 150;
                }

                if (NPC.ai[3] == 0)
                {
                    if (NPC.ai[2] % 20 == 0)
                    {
                        float Speed = 16f;
                        Vector2 vector8 = new Vector2(NPC.position.X + NPC.width / 2, NPC.position.Y + NPC.height / 2);
                        int type = ModContent.ProjectileType<Zero_VoidBeam>();
                        SoundEngine.PlaySound(SoundID.Item33, NPC.position);
                        float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + player.height * 0.5f), vector8.X - (player.position.X + player.width * 0.5f));
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), vector8.X, vector8.Y, (float)(Math.Cos(rotation) * Speed * -1), (float)(Math.Sin(rotation) * Speed * -1), type, damage, 0f, 0);
                    }
                    if (NPC.ai[2] >= 141 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.ai[2] = 0;
                        NPC.ai[3] = 3;
                    }
                }
                else if (NPC.ai[3] == 1)
                {
                    if (NPC.ai[2] % 30 == 0)
                    {
                        SoundEngine.PlaySound(SoundID.Item74, NPC.position);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0 + Main.rand.Next(-14, 14), 0 + Main.rand.Next(-14, 14), ModContent.ProjectileType<ZeroRocket>(), damage, 3); //Originally 85 damage
                    }
                    if (NPC.ai[2] >= 151 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.ai[2] = 0;
                        NPC.ai[3] = 3;
                    }
                }
                else if (NPC.ai[3] == 2)
                {
                    if (Collision.CanHit(NPC.position, NPC.width, NPC.height, player.Center, player.width, player.height))
                    {
                        int[] array4 = new int[5];
                        Vector2[] array5 = new Vector2[5];
                        int num838 = 0;
                        float num839 = 2000f;
                        for (int num840 = 0; num840 < 255; num840++)
                        {
                            if (Main.player[num840].active && !Main.player[num840].dead)
                            {
                                Vector2 center9 = Main.player[num840].Center;
                                float num841 = Vector2.Distance(center9, NPC.Center);
                                if (num841 < num839 && Collision.CanHit(NPC.Center, 1, 1, center9, 1, 1))
                                {
                                    array4[num838] = num840;
                                    array5[num838] = center9;
                                    if (++num838 >= array5.Length)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        if (Main.rand.Next(10) == 10)
                        {
                            for (int num842 = 0; num842 < num838; num842++)
                            {
                                Vector2 vector82 = array5[num842] - NPC.Center;
                                float ai = Main.rand.Next(100);
                                Vector2 vector83 = Vector2.Normalize(vector82.RotatedByRandom(0.78539818525314331)) * 14f;
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, vector83.X, vector83.Y, ModContent.ProjectileType<ZeroGigataser_TaserShock>(), damage, 0f, Main.myPlayer, vector82.ToRotation(), ai);
                            }
                        }
                    }
                    if (NPC.ai[2] >= 180 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.ai[2] = 0;
                        NPC.ai[3] = 3;
                    }
                }
                else if (NPC.ai[3] == 3)
                {
                    if (NPC.ai[2] == 5)
                    {
                        int TeleportPos = Main.rand.Next(5);
                        int VoidHeight = 140;
                        Point spawnTilePos = new Point(Main.maxTilesX / 15 * 14 + Main.maxTilesX / 15 / 2 - 100, VoidHeight);
                        Vector2 Origin = new Vector2(spawnTilePos.X * 16, spawnTilePos.Y * 16);

                        switch (TeleportPos)
                        {
                            case 0:
                                NPC.position = Origin;
                                break;
                            case 1:
                                NPC.position = Origin + new Vector2(0, 640);
                                break;
                            case 2:
                                NPC.position = Origin + new Vector2(0, -640);
                                break;
                            case 3:
                                NPC.position = Origin + new Vector2(640, 0);
                                break;
                            case 4:
                                NPC.position = Origin + new Vector2(-640, 0);
                                break;
                        }
                    }
                    if (NPC.life > NPC.lifeMax * (2 / 3))
                    {
                        if (NPC.ai[2] == 80 || NPC.ai[2] == 240) // + lasers
                        {
                            SoundEngine.PlaySound(SoundID.Item73, NPC.position);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(0f, -12f), ModContent.ProjectileType<Zero_VoidRay>(), damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(0f, 12f), ModContent.ProjectileType<Zero_VoidRay>(), damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-12f, 0f), ModContent.ProjectileType<Zero_VoidRay>(), damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(12f, 0f), ModContent.ProjectileType<Zero_VoidRay>(), damage, 3);
                        }
                        if (NPC.ai[2] == 160 || NPC.ai[2] == 320) // x lasers
                        {
                            SoundEngine.PlaySound(SoundID.Item73, NPC.position);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(8f, 8f), ModContent.ProjectileType<Zero_VoidRay>(), damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(8f, -8f), ModContent.ProjectileType<Zero_VoidRay>(), damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-8f, 8f), ModContent.ProjectileType<Zero_VoidRay>(), damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-8f, -8f), ModContent.ProjectileType<Zero_VoidRay>(), damage, 3);
                        }
                    }
                    else if (NPC.life > NPC.lifeMax / 3)
                    {
                        if (NPC.ai[2] == 80) // + lasers
                        {
                            SoundEngine.PlaySound(SoundID.Item73, NPC.position);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(0f, -12f), ModContent.ProjectileType<Zero_VoidRay>(), damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(0f, 12f), ModContent.ProjectileType<Zero_VoidRay>(), damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-12f, 0f), ModContent.ProjectileType<Zero_VoidRay>(), damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(12f, 0f), ModContent.ProjectileType<Zero_VoidRay>(), damage, 3);
                        }
                        else if (NPC.ai[2] == 160)
                        {
                            SoundEngine.PlaySound(SoundID.Item73, NPC.position);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(0f, -12f), ModContent.ProjectileType<Zero_VoidRay>(), damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(0f, 12f), ModContent.ProjectileType<Zero_VoidRay>(), damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-12f, 0f), ModContent.ProjectileType<Zero_VoidRay>(), damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(12f, 0f), ModContent.ProjectileType<Zero_VoidRay>(), damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(8f, 8f), ModContent.ProjectileType<Zero_VoidRay>(), damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(8f, -8f), ModContent.ProjectileType<Zero_VoidRay>(), damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-8f, 8f), ModContent.ProjectileType<Zero_VoidRay>(), damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-8f, -8f), ModContent.ProjectileType<Zero_VoidRay>(), damage, 3);
                        }
                        else if (NPC.ai[2] == 240)
                        {
                            SoundEngine.PlaySound(SoundID.Item73, NPC.position);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(8f, 8f), ModContent.ProjectileType<Zero_VoidRay>(), damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(8f, -8f), ModContent.ProjectileType<Zero_VoidRay>(), damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-8f, 8f), ModContent.ProjectileType<Zero_VoidRay>(), damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-8f, -8f), ModContent.ProjectileType<Zero_VoidRay>(), damage, 3);
                        }
                        else if (NPC.ai[2] == 320)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(0f, -12f), ModContent.ProjectileType<Zero_VoidRay>(), damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(0f, 12f), ModContent.ProjectileType<Zero_VoidRay>(), damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-12f, 0f), ModContent.ProjectileType<Zero_VoidRay>(), damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(12f, 0f), ModContent.ProjectileType<Zero_VoidRay>(), damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(8f, 8f), ModContent.ProjectileType<Zero_VoidRay>(), damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(8f, -8f), ModContent.ProjectileType<Zero_VoidRay>(), damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-8f, 8f), ModContent.ProjectileType<Zero_VoidRay>(), damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-8f, -8f), ModContent.ProjectileType<Zero_VoidRay>(), damage, 3);
                        }
                    }
                    else
                    {
                        if (NPC.ai[2] == 80) // + lasers
                        {
                            SoundEngine.PlaySound(SoundID.Item73, NPC.position);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(0f, -12f), ModContent.ProjectileType<Zero_DoomDeathray>(), damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(0f, 12f), ModContent.ProjectileType<Zero_DoomDeathray>(), damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-12f, 0f), ModContent.ProjectileType<Zero_DoomDeathray>(), damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(12f, 0f), ModContent.ProjectileType<Zero_DoomDeathray>(), damage, 3);
                        }
                        else if (NPC.ai[2] == 160)
                        {
                            SoundEngine.PlaySound(SoundID.Item73, NPC.position);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(8f, 8f), ModContent.ProjectileType<Zero_DoomDeathray>(), damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(8f, -8f), ModContent.ProjectileType<Zero_DoomDeathray>(), damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-8f, 8f), ModContent.ProjectileType<Zero_DoomDeathray>(), damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-8f, -8f), ModContent.ProjectileType<Zero_DoomDeathray>(), damage, 3);
                        }
                        else if (NPC.ai[2] == 240)
                        {
                            SoundEngine.PlaySound(SoundID.Item73, NPC.position);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(0f, -12f), ModContent.ProjectileType<Zero_DoomDeathray>(), damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(0f, 12f), ModContent.ProjectileType<Zero_DoomDeathray>(), damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-12f, 0f), ModContent.ProjectileType<Zero_DoomDeathray>(), damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(12f, 0f), ModContent.ProjectileType<Zero_DoomDeathray>(), damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(8f, 8f), ModContent.ProjectileType<Zero_DoomDeathray>(), damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(8f, -8f), ModContent.ProjectileType<Zero_DoomDeathray>(), damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-8f, 8f), ModContent.ProjectileType<Zero_DoomDeathray>(), damage, 3);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-8f, -8f), ModContent.ProjectileType<Zero_DoomDeathray>(), damage, 3);
                        }
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            if (NPC.ai[2] >= 320)
                            {
                                NPC.ai[3] = Main.rand.Next(3);
                                NPC.ai[2] = 0;
                                NPC.netUpdate = true;
                            }
                        }
                    }

                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        if (NPC.ai[2] >= 400)
                        {
                            NPC.ai[3] = Main.rand.Next(3);
                            NPC.ai[2] = 0;
                            NPC.netUpdate = true;
                        }
                    }
                }

                if (ShieldScale > 0)
                {
                    ShieldScale -= .07f;
                }
                if (ShieldScale < 0)
                {
                    ShieldScale = 0;
                }
            }
        }
    }

    public class ZeroBossBar : ModBossBar
    {
        private float _shieldMax = 0f;
        private Asset<Texture2D> _icon = ModContent.Request<Texture2D>("AAModClassic/_Content/Void/_PostMoonlord/NPCs/__BossZero/Zero_Head_Boss");

        // combine all arm's life as shield
        public override bool? ModifyInfo(ref BigProgressBarInfo info, ref float life, ref float lifeMax, ref float shield, ref float shieldMax)
        {
            if (Main.npc.Length < info.npcIndexToAimAt)
                return false;

            NPC npc = Main.npc[info.npcIndexToAimAt];

            if (npc == null || !npc.active)
                return false;

            if (npc.ModNPC is not Zero zero)
                return false;

            life = npc.life;
            lifeMax = npc.lifeMax;

            if (!npc.dontTakeDamage)
            {
                _shieldMax = 0;
                return true;
            }

            shield = 0;
            shieldMax = 0;

            foreach (NPC arm in Main.npc)
            {
                if (
                    arm.active && 
                        (
                            arm.type == ModContent.NPCType<ZeroVoidStar>() ||
                            arm.type == ModContent.NPCType<ZeroGigataser>() ||
                            arm.type == ModContent.NPCType<ZeroRealityCannon>() ||
                            arm.type == ModContent.NPCType<ZeroRiftShredder>() ||
                            arm.type == ModContent.NPCType<ZeroNeutralizer>() ||
                            arm.type == ModContent.NPCType<ZeroOmegaVolley>() ||
                            arm.type == ModContent.NPCType<ZeroNovaFocus>() ||
                            arm.type == ModContent.NPCType<ZeroGenocideCannon>()
                        )
                    )
                {
                    shieldMax += arm.lifeMax;
                    shield += arm.life;
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
