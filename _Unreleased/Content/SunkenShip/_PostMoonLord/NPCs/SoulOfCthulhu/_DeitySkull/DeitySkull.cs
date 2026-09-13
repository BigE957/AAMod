using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._Cthulhu;
using AAModClassic._Unreleased.Content.SunkenShip.World.Biomes;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Dusts;
using AAModClassic.Globals;
using AAModClassic.Music;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeitySkull
{
    [AutoloadBossHead]
    public class DeitySkull : ModNPC
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Rhan-Tegoth");
            NPCID.Sets.BossBestiaryPriority.Add(Type);
        }
        public override void SetDefaults()
        {
            NPC.width = 80;
            NPC.height = 102;
            NPC.aiStyle = -1;
            NPC.damage = 100;
            NPC.defense = 80;
            NPC.lifeMax = 150000;
            NPC.knockBackResist = 0.0f;
            NPC.noTileCollide = true;
            NPC.noGravity = true;
            NPC.npcSlots = 6f;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.boss = true;
            NPC.netAlways = true;
            NPC.buffImmune[20] = true;
            NPC.buffImmune[24] = true;
            NPC.buffImmune[39] = true;
            Music = NPC.AnyNPCs(ModContent.NPCType<Cthulhu>()) ? MusicManagementSystem.MusicSlots["Cthulhu"] : MusicManagementSystem.MusicSlots["SoulOfCthulhu"];
            for (int m = 0; m < NPC.buffImmune.Length; m++) NPC.buffImmune[m] = true;
            NPC.lavaImmune = true;
            if (!NPC.IsABestiaryIconDummy)
                NPC.alpha = 255;
            SpawnModBiomes = [ModContent.GetInstance<SunkenShipBiome>().Type];
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(NPC.localAI[0]);
            writer.Write(HandTimer);
        }
    
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            NPC.localAI[0] = reader.ReadSingle();
            HandTimer = reader.ReadInt32();
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                //SoulOfCthulhu.ComeBack = true;
            }
        }

        public override void ModifyHitByItem(Player player, Item item, ref NPC.HitModifiers modifiers)
        {
            if (!AAConfigClient.Instance.DisableAnticheat)
            {
                if (modifiers.GetDamage(item.damage, true) > NPC.lifeMax / 8)
                {
                    Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.Anticheat"), Color.DarkCyan);
                    modifiers.TargetDamageMultiplier *= 0;
                }
            }
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if (!AAConfigClient.Instance.DisableAnticheat)
            {
                if (modifiers.GetDamage(projectile.damage, true) > NPC.lifeMax / 8)
                {
                    Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.Anticheat"), Color.DarkCyan);
                    modifiers.TargetDamageMultiplier *= 0;
                }
            }
        }

        /*
        public override bool PreKill()
        {
            return false;
        }
        */

        public int HandTimer = 120;

        public override void AI()
        {
            NPC.damage = NPC.defDamage;
            NPC.defense = NPC.defDefense;
            bool expert = Main.expertMode;
            HandTimer--;

            bool BossAlive = NPC.AnyNPCs(ModContent.NPCType<SoulOfCthulhu>()) || NPC.AnyNPCs(ModContent.NPCType<Cthulhu>());

            if (!BossAlive)
            {
                NPC.velocity *= .8f;
                for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                {
                    int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, ModContent.DustType<CthulhuDust>(), 0f, 0f, 100, default(Color), 2f);
                    Main.dust[num935].noGravity = true;
                    Main.dust[num935].noLight = true;
                }
                NPC.alpha += 12;
                if (NPC.alpha > 255)
                {
                    NPC.active = false;
                }
            }

            if (NPC.alpha != 0)
            {
                for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                {
                    int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, ModContent.DustType<CthulhuDust>(), 0f, 0f, 100, default(Color), 2f);
                    Main.dust[num935].noGravity = true;
                    Main.dust[num935].noLight = true;
                }
            }
            NPC.alpha -= 12;
            if (NPC.alpha < 0)
            {
                NPC.alpha = 0;
            }

            if (NPC.type == ModContent.NPCType<DeitySkull>() && (!NPC.AnyNPCs(ModContent.NPCType<DeitySkull_Hand>()) && !NPC.AnyNPCs(ModContent.NPCType<DeitySkull_Hand1>()) && !NPC.AnyNPCs(ModContent.NPCType<DeitySkull_Hand2>()) && !NPC.AnyNPCs(ModContent.NPCType<DeitySkull_Hand3>()) || !NPC.AnyNPCs(ModContent.NPCType<DeitySkull_Hand4>()) && !NPC.AnyNPCs(ModContent.NPCType<DeitySkull_Hand5>())) && HandTimer <= 0)
            {
                NPC.life = 0;
            }
            else
            {
                NPC.dontTakeDamage = true;
            }
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (NPC.ai[0] == 0)
                {
                    NPC.TargetClosest(true);
                    NPC.ai[0]++;
                    int index1 = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<DeitySkull_Hand>(), NPC.whoAmI);
                    Main.npc[index1].ai[0] = -1f;
                    Main.npc[index1].ai[1] = NPC.whoAmI;
                    Main.npc[index1].target = NPC.target;
                    Main.npc[index1].netUpdate = true;
                    int index2 = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<DeitySkull_Hand1>(), NPC.whoAmI);
                    Main.npc[index2].ai[0] = -1f;
                    Main.npc[index2].ai[1] = NPC.whoAmI;
                    Main.npc[index2].target = NPC.target;
                    Main.npc[index2].ai[3] = 150f;
                    Main.npc[index2].netUpdate = true;
                    int index3 = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<DeitySkull_Hand2>(), NPC.whoAmI);
                    Main.npc[index3].ai[0] = -1f;
                    Main.npc[index3].ai[1] = NPC.whoAmI;
                    Main.npc[index3].target = NPC.target;
                    Main.npc[index3].ai[3] = 150f;
                    Main.npc[index3].netUpdate = true;
                    int index4 = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<DeitySkull_Hand3>(), NPC.whoAmI);
                    Main.npc[index4].ai[0] = 1f;
                    Main.npc[index4].ai[1] = NPC.whoAmI;
                    Main.npc[index4].target = NPC.target;
                    Main.npc[index4].netUpdate = true;
                    Main.npc[index4].ai[3] = 150f;
                    int index5 = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<DeitySkull_Hand4>(), NPC.whoAmI);
                    Main.npc[index5].ai[0] = 1f;
                    Main.npc[index5].ai[1] = NPC.whoAmI;
                    Main.npc[index5].target = NPC.target;
                    Main.npc[index5].netUpdate = true;
                    Main.npc[index5].ai[3] = 150f;
                    int index6 = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<DeitySkull_Hand5>(), NPC.whoAmI);
                    Main.npc[index6].ai[0] = 1f;
                    Main.npc[index6].ai[1] = NPC.whoAmI;
                    Main.npc[index6].ai[3] = 150f;
                    Main.npc[index6].target = NPC.target;
                    Main.npc[index6].netUpdate = true;
                }
            }
            if (Main.player[NPC.target].dead || Math.Abs(NPC.position.X - Main.player[NPC.target].position.X) > 6000.0 || Math.Abs(NPC.position.Y - Main.player[NPC.target].position.Y) > 6000.0)
            {
                NPC.TargetClosest(true);
                if (Main.player[NPC.target].dead || Math.Abs(NPC.position.X - Main.player[NPC.target].position.X) > 6000.0 || Math.Abs(NPC.position.Y - Main.player[NPC.target].position.Y) > 6000.0)
                    NPC.ai[1] = 3f;
            }
            if (NPC.ai[1] == 0.0)
            {
                ++NPC.ai[2];
                if (NPC.ai[2] >= 600.0)
                {
                    NPC.ai[2] = 0.0f;
                    NPC.ai[1] = 1f;
                    NPC.TargetClosest(true);
                    NPC.netUpdate = true;
                }
                NPC.rotation = NPC.velocity.X / 15f;
                if (NPC.position.Y > Main.player[NPC.target].position.Y - 200.0)
                {
                    if (NPC.velocity.Y > 0.0)
                        NPC.velocity.Y *= 0.98f;
                    NPC.velocity.Y -= 0.1f;
                    if (NPC.velocity.Y > 2.0)
                        NPC.velocity.Y = 2f;
                }
                else if (NPC.position.Y < Main.player[NPC.target].position.Y - 500.0)
                {
                    if (NPC.velocity.Y < 0.0)
                        NPC.velocity.Y *= 0.98f;
                    NPC.velocity.Y += 0.1f;
                    if (NPC.velocity.Y < -2.0)
                        NPC.velocity.Y = -2f;
                }
                if (NPC.position.X + (double)(NPC.width / 2) > Main.player[NPC.target].position.X + (double)(Main.player[NPC.target].width / 2) + 100.0)
                {
                    if (NPC.velocity.X > 0.0)
                        NPC.velocity.X *= 0.98f;
                    NPC.velocity.X -= 0.1f;
                    if (NPC.velocity.X > 8.0)
                        NPC.velocity.X = 8f;
                }


                if (NPC.position.X + (double)(NPC.width / 2) >= Main.player[NPC.target].position.X + (double)(Main.player[NPC.target].width / 2) - 100.0)
                    return;
                if (NPC.velocity.X < 0.0)
                    NPC.velocity.X *= 0.98f;
                NPC.velocity.X += 0.1f;
                if (NPC.velocity.X >= -8.0)
                    return;
                NPC.velocity.X = -8f;



                if (Main.netMode == NetmodeID.MultiplayerClient || !expert || NPC.ai[3] != 6)
                    return;
                ++NPC.localAI[0];
                if (NPC.localAI[0] <= 150.0)
                    return;
                NPC.localAI[0] = 0.0f;
                Vector2 vector2_6 = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
                float num41 = Main.player[NPC.target].position.X + Main.player[NPC.target].width / 2 - vector2_6.X;
                float num42 = Main.player[NPC.target].position.Y + Main.player[NPC.target].height / 2 - vector2_6.Y;
                float num43 = (float)Math.Sqrt(num41 * (double)num41 + num42 * (double)num42);
                float num4 = 8f;
                int Damage = 15;
                int Type = 258;
                float num5 = num4 / num43;
                float num6 = num41 * num5;
                float num7 = num42 * num5;
                float SpeedX = num6 + Main.rand.Next(-5, 6) * 0.05f;
                float SpeedY = num7 + Main.rand.Next(-5, 6) * 0.05f;
                vector2_6.X += SpeedX * 6f;
                vector2_6.Y += SpeedY * 6f;
                Projectile.NewProjectile(NPC.GetSource_FromThis(), vector2_6.X, vector2_6.Y, SpeedX, SpeedY, Type, Damage, 0.0f, Main.myPlayer, 0.0f, 0.0f);
            }
            else if (NPC.ai[1] == 1.0)
            {
                NPC.defense *= 2;
                NPC.damage *= 2;
                ++NPC.ai[2];
                if (NPC.ai[2] == 2.0)
                    SoundEngine.PlaySound(SoundID.Roar, NPC.position);
                if (NPC.ai[2] >= 400.0)
                {
                    NPC.ai[2] = 0.0f;
                    NPC.ai[1] = 0.0f;
                }
                NPC.rotation += NPC.direction * 0.3f;
                Vector2 vector2 = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
                float num1 = Main.player[NPC.target].position.X + Main.player[NPC.target].width / 2 - vector2.X;
                float num2 = Main.player[NPC.target].position.Y + Main.player[NPC.target].height / 2 - vector2.Y;
                float num3 = 2f / (float)Math.Sqrt(num1 * (double)num1 + num2 * (double)num2);
                NPC.velocity.X = num1 * num3;
                NPC.velocity.Y = num2 * num3;

            }
            else if (NPC.ai[1] == 2.0)
            {
                NPC.damage = 1000;
                NPC.defense = 9999;
                NPC.rotation += NPC.direction * 0.3f;
                Vector2 vector2 = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
                float num1 = Main.player[NPC.target].position.X + Main.player[NPC.target].width / 2 - vector2.X;
                float num2 = Main.player[NPC.target].position.Y + Main.player[NPC.target].height / 2 - vector2.Y;
                float num3 = (float)Math.Sqrt(num1 * (double)num1 + num2 * (double)num2);
                float num4 = 10f + num3 / 100f;
                if (num4 < 8.0)
                    num4 = 8f;
                if (num4 > 32.0)
                    num4 = 32f;
                float num5 = num4 / num3;
                NPC.velocity.X = num1 * num5;
                NPC.velocity.Y = num2 * num5;
            }
            else
            {
                if (NPC.ai[1] != 3.0)
                    return;
                NPC.velocity.Y += 0.1f;
                if (NPC.velocity.Y < 0.0)
                    NPC.velocity.Y *= 0.95f;
                NPC.velocity.X *= 0.95f;
                if (NPC.timeLeft <= 500)
                    return;
                NPC.timeLeft = 500;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D currentTex = TextureAssets.Npc[NPC.type].Value;
            Texture2D GlowTex = ModContent.Request<Texture2D>(Texture + "_Glow").Value;

            spriteBatch.Draw(currentTex, NPC.Center - screenPos, NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);

            //draw glow/glow afterimage
            spriteBatch.Draw(GlowTex, NPC.Center - screenPos, NPC.frame, AAColor.Cthulhu2, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            BaseDrawing.DrawAfterimage(spriteBatch, GlowTex, 0, NPC, 0.8f, 1f, 6, false, 0f, 0f, AAColor.Cthulhu2);

            return false;
        }
    }
}