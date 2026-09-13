using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic._CrossMod.CalamityMod.LoreItems;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.Items.SoulOfCthulhu.BossStandard;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityBrain;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityEater;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityEye;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityLeviathan;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityRose;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeitySkull;
using AAModClassic._Unreleased.Content.SunkenShip.World.Biomes;
using AAModClassic.Achievements;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Music;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._Cthulhu
{
    [AutoloadBossHead]
    public class Cthulhu : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cthulhu, Cosmic Calamity");
            NPCID.Sets.BossBestiaryPriority.Add(Type);

        }
        public override void SetDefaults()
        {
            NPC.width = 222;
            NPC.height = 228;
            if (!NPC.IsABestiaryIconDummy)
                NPC.alpha = 255;
            NPC.damage = 0;
            Music = MusicManagementSystem.MusicSlots["Cthulhu"];
            NPC.lifeMax = 1500000;
            NPC.dontTakeDamage = false;
            NPC.noGravity = true;
            NPC.aiStyle = -1;
            NPC.boss = true;
            NPC.scale *= 1.2f;
            NPC.HitSound = SoundID.NPCHit54;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
            NPC.knockBackResist = 0f;
            SpawnModBiomes = [ModContent.GetInstance<SunkenShipBiome>().Type];
        }

        //public float[] shootAI = new float[4];

        public float[] customAI = new float[4];
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(customAI[0]);
            writer.Write(customAI[1]);
            writer.Write(customAI[2]);
            writer.Write(customAI[3]);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            customAI[0] = reader.ReadSingle();
            customAI[1] = reader.ReadSingle();
            customAI[2] = reader.ReadSingle();
            customAI[3] = reader.ReadSingle();
        }

        //public int Speechtimer = 0;

        //Client Side
        public float ShieldScale = 0;
        public float ShieldRotation = 0;

        public override void AI()
        {
            Player player = Main.player[NPC.target];
            float EyeSummon = NPC.lifeMax * .85f;
            float BrainSummon = NPC.lifeMax * .70f;
            float EaterSummon = NPC.lifeMax * .55f;
            float SkullSummon = NPC.lifeMax * .40f;
            float RoseSummon = NPC.lifeMax * .25f;
            float LeviathanSummon = NPC.lifeMax * .15f;

            bool BossAlive = NPC.AnyNPCs(ModContent.NPCType<DeityEye>()) || NPC.AnyNPCs(ModContent.NPCType<DeityEater>()) || NPC.AnyNPCs(ModContent.NPCType<DeityBrain>()) || NPC.AnyNPCs(ModContent.NPCType<DeitySkull>()) || NPC.AnyNPCs(ModContent.NPCType<DeityLeviathan>()) || NPC.AnyNPCs(ModContent.NPCType<DeityRose>());

            Vector2 Explosion = new Vector2(Main.rand.Next((int)NPC.position.X + NPC.width), Main.rand.Next((int)NPC.position.Y - NPC.height));

            ShieldRotation += .05f;

            if (BossAlive)
            {
                NPC.dontTakeDamage = true;
                if (ShieldScale < 1f)
                {
                    ShieldScale += .05f;
                }
                if (ShieldScale >= 1f)
                {
                    ShieldScale = 1f;
                }
            }
            else
            {
                if (ShieldScale > 0)
                {
                    ShieldScale -= .05f;
                }
                if (ShieldScale <= 0)
                {
                    NPC.dontTakeDamage = false;
                    ShieldScale = 0;
                }
            }

            if (NPC.ai[1] == 1f)
            {
                NPC.dontTakeDamage = true;
                NPC.ai[3]++;
                if (NPC.ai[3] % 60 == 0)
                {
                    Projectile.NewProjectile(NPC.GetSource_Death(), Explosion, new Vector2(0, 0), ModContent.ProjectileType<CthulhuDeathBoom>(), 0, 0);
                }

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    if (NPC.ai[3] == 40)
                    {
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.Awakened.Defeat.1"), Color.DarkCyan);
                    }

                    if (NPC.ai[3] == 100)
                    {
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.Awakened.Defeat.2"), Color.DarkCyan);
                    }

                    if (NPC.ai[3] == 160)
                    {
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.Awakened.Defeat.3"), Color.DarkCyan);
                    }

                    if (NPC.ai[3] == 220)
                    {
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.Awakened.Defeat.4"), Color.DarkCyan);
                    }

                    if (NPC.ai[3] == 280)
                    {
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.Awakened.Defeat.5"), Color.DarkCyan);
                    }

                    if (NPC.ai[3] == 340)
                    {
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.Awakened.Defeat.6"), Color.DarkCyan);
                    }

                    if (NPC.ai[3] == 400)
                    {
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.Awakened.Defeat.7"), Color.DarkCyan);
                    }

                    if (NPC.ai[3] == 460)
                    {
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.Awakened.Defeat.8"), Color.DarkCyan);
                    }
                }

                if (NPC.ai[3] == 520)
                {
                    Projectile.NewProjectile(NPC.GetSource_Death(), NPC.Center, new Vector2(0, 0), ModContent.ProjectileType<CthulhuDeath>(), 0, 0);
                    if(!Main.dedServ)
                        Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("CthulhuGore").Type, 1.2f);
                    NPC.dontTakeDamage = false;
                    NPC.StrikeInstantKill();
                }
                return;
            }

            BaseAI.AISpaceOctopus(NPC, ref customAI, .1f, 1, 0f, 120f, FireMagic);

            if (NPC.life < EyeSummon && NPC.ai[2] == 0)
            {
                NPC.ai[2] = 1;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.Awakened.Summoning.Eye"), Color.DarkCyan);
                    NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<DeityEye>());
                }
            }

            //Both eater and brain get summoned at the same time due to faulty logic here, but its kinda aura so im leaving it
            if (NPC.life < EaterSummon && NPC.ai[2] == 1)
            {
                NPC.ai[2] = 2;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.Awakened.Summoning.Eater"), Color.DarkCyan);
                    NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<DeityEater>());
                }
            }

            if (NPC.life < BrainSummon && NPC.ai[2] == 2)
            {
                NPC.ai[2] = 3;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.Awakened.Summoning.Brain"), Color.DarkCyan);
                    NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<DeityBrain>());
                }
            }

            if (NPC.life < SkullSummon && NPC.ai[2] == 3)
            {
                NPC.ai[2] = 4;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.Awakened.Summoning.Skull"), Color.DarkCyan);
                    NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<DeitySkull>());
                }
            }

            if (NPC.life < RoseSummon && NPC.ai[2] == 4)
            {
                NPC.ai[2] = 5;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.Awakened.Summoning.Rose"), Color.DarkCyan);
                    NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<DeityRose>());
                }
            }

            if (NPC.life < LeviathanSummon && NPC.ai[2] == 5)
            {
                NPC.ai[2] = 6;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.Awakened.Summoning.Leviathan"), Color.DarkCyan);
                    NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<DeityLeviathan>());
                }
            }

            if (NPC.life <= NPC.lifeMax / 10)
            {
                if(!AAConfigClient.Instance.DisablePinchThemes)
                    Music = MusicManagementSystem.MusicSlots["Superancients_Pinch"];
                if (NPC.ai[2] == 6)
                {
                    NPC.ai[2] = 7;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.Awakened.LastStand"), Color.DarkCyan);
                }
            }
        }

        public override void OnKill()
        {
            if (!Main.expertMode)
                BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.Awakened.Defeat.Cheater"), Color.DarkCyan);
            else
            {
                if (NPC.playerInteraction[Main.myPlayer])
                    SoulOfCthulhuKilled.Condition.Complete();

                if (!NPC.BeenKilled(true))
                    NPC.OnGameEventClearedForTheFirstTime(-1);
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule expert = new(new Conditions.IsExpert());

            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<SoulOfCthulhuTreasureBag>()));

            expert.OnSuccess(ItemDropRule.Common(ModContent.ItemType<EXSoul>()));

            LeadingConditionRule loreCondition = new(new LoreItemDropCondition<Cthulhu>());
            expert.OnSuccess(loreCondition).OnSuccess(new PerPlayerDropRule(ModContent.ItemType<SoulOfCthulhuLore>(), 1));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SoulOfCthulhuTrophy>(), 10));

            LeadingConditionRule masterMode = new(new AAConditions.RevOrMaster());

            masterMode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<SoulOfCthulhuRelic>()));

            npcLoot.Add(masterMode);

            npcLoot.Add(expert);
        }

        int ShootThis;
        int Loop;

        public void FireMagic(NPC npc, Vector2 velocity)
        {
            Player player = Main.player[npc.target];
            int num429 = 1;
            if (npc.position.X + (npc.width / 2) < Main.player[npc.target].position.X + Main.player[npc.target].width)
            {
                num429 = -1;
            }
            Vector2 PlayerDistance = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
            float PlayerPosX = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) + (num429 * 180) - PlayerDistance.X;
            float PlayerPosY = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - PlayerDistance.Y;
            float PlayerPos = (float)Math.Sqrt((PlayerPosX * PlayerPosX) + (PlayerPosY * PlayerPosY));
            float num433 = 6f;
            PlayerPosX = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - PlayerDistance.X;
            PlayerPosY = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - PlayerDistance.Y;
            PlayerPos = (float)Math.Sqrt((PlayerPosX * PlayerPosX + PlayerPosY * PlayerPosY));
            PlayerPos = num433 / PlayerPos;
            PlayerPosX *= PlayerPos;
            PlayerPosY *= PlayerPos;
            PlayerPosY += Main.rand.Next(-40, 41) * 0.01f;
            PlayerPosX += Main.rand.Next(-40, 41) * 0.01f;
            PlayerPosY += npc.velocity.Y * 0.5f;
            PlayerPosX += npc.velocity.X * 0.5f;
            PlayerDistance.X -= PlayerPosX * 1f;
            PlayerDistance.Y -= PlayerPosY * 1f;
            Vector2 spawnAt = npc.Center + new Vector2(0f, npc.height / 2f);
            npc.ai[0] += 1;
            if (npc.ai[0] == 1 || npc.ai[0] == 4 || npc.ai[0] == 6 || npc.ai[0] == 15 || npc.ai[0] == 20)
            {
                ShootThis = ModContent.ProjectileType<CthulhuNuke>();
            }
            if (npc.ai[0] == 2 || npc.ai[0] == 3 || npc.ai[0] == 9 || npc.ai[0] == 17 || npc.ai[0] == 22)
            {
                NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, Mod.Find<ModNPC>("Portal").Type, 0, -npc.velocity.X * 1.2f, -npc.velocity.Y * 1.2f);
                return;
            }
            if (npc.ai[0] == 5 || npc.ai[0] == 12 || npc.ai[0] == 16 || npc.ai[0] == 21 || npc.ai[0] == 25)
            {
                ShootThis = ModContent.ProjectileType<CthulhuShot>();
            }
            if (npc.ai[0] == 7 || npc.ai[0] == 11 || npc.ai[0] == 14 || npc.ai[0] == 18 || npc.ai[0] == 24)
            {
                ShootThis = ModContent.ProjectileType<Watcher>();
                Loop = 5;
            }
            if (npc.ai[0] == 8 || npc.ai[0] == 10 || npc.ai[0] == 13 || npc.ai[0] == 19 || npc.ai[0] == 23)
            {
                ShootThis = ModContent.ProjectileType<Watcher>();
                Loop = 9;
            }
            if (ShootThis == ModContent.ProjectileType<Watcher>())
            {
                float spread = 45f * 0.0174f;
                float baseSpeed = (float)Math.Sqrt((PlayerPosX * PlayerPosX) + (PlayerPosY * PlayerPosY));
                double startAngle = Math.Atan2(PlayerPosX, PlayerPosY) - .1d;
                double deltaAngle = spread / 6f;
                double offsetAngle;
                for (int i = 0; i < Loop; i++)
                {
                    offsetAngle = startAngle + (deltaAngle * i);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), npc.Center.X, npc.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), ShootThis, 50, 0f);
                }
            }
            else
            {
                Projectile.NewProjectile(NPC.GetSource_FromThis(), PlayerDistance.X, PlayerDistance.Y, PlayerPosX * 2, PlayerPosY * 2, ShootThis, 80, 0f);
            }
            if (npc.ai[0] > 25)
            {
                npc.ai[0] = 0;
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && NPC.ai[1] != 1 && Main.expertMode)
            {
                NPC.ai[1] = 1f;
                NPC.life = NPC.lifeMax;
                NPC.netUpdate = true;
                NPC.dontTakeDamage = true;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            bool BossAlive = !NPC.IsABestiaryIconDummy && (NPC.AnyNPCs(ModContent.NPCType<DeityEye>()) || NPC.AnyNPCs(ModContent.NPCType<DeityEater>()) || NPC.AnyNPCs(ModContent.NPCType<DeityBrain>()) || NPC.AnyNPCs(ModContent.NPCType<DeitySkull>()) || NPC.AnyNPCs(ModContent.NPCType<DeityLeviathan>()) || NPC.AnyNPCs(ModContent.NPCType<DeityRose>()));
            Texture2D currentTex = TextureAssets.Npc[NPC.type].Value;
            Texture2D GlowTex = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
            Texture2D Barrier = ModContent.Request<Texture2D>(Texture + "Barrier").Value;
            Texture2D Shield = ModContent.Request<Texture2D>(Texture + "Shield").Value;

            spriteBatch.Draw(currentTex, NPC.Center - screenPos, new Rectangle?(new Rectangle(0, 0, currentTex.Width, currentTex.Height)), drawColor, NPC.rotation, new Vector2(currentTex.Width / 2f, currentTex.Height / 2f), NPC.scale, SpriteEffects.None, 0f);

            //draw glow/glow afterimage
            spriteBatch.Draw(GlowTex, NPC.Center - screenPos, NPC.frame, AAColor.Cthulhu2, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.SpriteEffectDirection(), 0);
            DrawingUtils.DrawAfterimageWithVelocity(spriteBatch, GlowTex, NPC.Center - screenPos, NPC.velocity, 6, NPC.frame, AAColor.Cthulhu2, NPC.scale, [NPC.rotation], NPC.frame.Size() * 0.5f, NPC.SpriteEffectDirection(), 0.8f);

            //Draw Shield
            if (BossAlive)
            {
                int shader = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingOceanDye);
                DrawingUtils.DrawWithVanillaShader(spriteBatch, shader, (sb) =>
                {
                    float opacity = 1f;
                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                        opacity = MathHelper.Lerp(0.25f, 0.75f, MathF.Sin(Main.GlobalTimeWrappedHourly * 5f) / 2f + 0.5f);
                    spriteBatch.Draw(Shield, NPC.Center - screenPos, null, AAColor.Cthulhu * opacity, ShieldRotation, Shield.Size() * 0.5f, ShieldScale, 0, 0);
                });
                spriteBatch.Draw(Barrier, NPC.Center - screenPos, null, Color.White, ShieldRotation, Barrier.Size() * 0.5f, ShieldScale, 0, 0);
            }
            return false;
        }
    }
}