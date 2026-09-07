using AAModClassic._Unreleased.Content.Parthenan.World.Biomes;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.NPCs.__BossRaiderUltima
{
    [AutoloadBossHead]
    public class RaiderEgg : ModNPC
    {
        public static Asset<Texture2D> Glowmask;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Raider Egg");

            this.HideFromBestiary();

            Glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
        }
        public override void SetDefaults()
        {
            NPC.width = 34;
            NPC.height = 34;
            NPC.aiStyle = NPCAIStyleID.FaceClosestPlayer;
            NPC.damage = 0;
            NPC.defense = 30;
            NPC.lavaImmune = true;
            NPC.lifeMax = 50;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.value = 0f;
            NPC.knockBackResist = 2f;
            NPC.npcSlots = 0f;
            SpawnModBiomes = [ModContent.GetInstance<ParthenanBiome>().Type];
            NPC.BossBar = Main.BigBossProgressBar.NeverValid;
        }

        public Color color;

        public override void OnKill()
        {
            if (!Main.dedServ)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("RaidEggGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("RaidEggGore2").Type, 1f);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {

            Texture2D glowTex = Glowmask.Value;
            color = BaseUtility.MultiLerpColor(((int)(Main.GlobalTimeWrappedHourly * 60)) % 100 / 100f, drawColor, drawColor, Color.Violet, drawColor, Color.Violet, drawColor);
            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos, NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            spriteBatch.Draw(glowTex, NPC.Center - screenPos, NPC.frame, color, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            return false;
        }
        
        public override void AI()
        {
            if (NPC.velocity.Y == 0f)
            {
                NPC.velocity.X = NPC.velocity.X * 0.9f;
                NPC.rotation += NPC.velocity.X * 0.02f;
            }
            else
            {
                NPC.velocity.X = NPC.velocity.X * 0.99f;
                NPC.rotation += NPC.velocity.X * 0.04f;
            }
            int num1326 = 900;
            if (Main.expertMode)
            {
                num1326 = 600;
            }
            if (NPC.justHit)
            {
                NPC.ai[0] -= Main.rand.Next(10, 21);
                if (!Main.expertMode)
                {
                    NPC.ai[0] -= Main.rand.Next(10, 21);
                }
            }
            NPC.ai[0] += 1f;
            if (NPC.ai[0] >= num1326 || NPC.velocity.Y == 0)
            {
                Projectile.NewProjectile(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y, 0, 0, ModContent.ProjectileType<RaiderUltima_RaiderExplosion>(), 30, 10, Main.myPlayer, 0, 0);
                NPC.Transform(ModContent.NPCType<Raidmini>());
            }
            if (Main.netMode != NetmodeID.MultiplayerClient && NPC.velocity.Y == 0f && Math.Abs(NPC.velocity.X) < 0.2 && NPC.ai[0] >= num1326 * 0.75)
            {
                float num1327 = NPC.ai[0] - num1326 * 0.75f;
                num1327 /= num1326 * 0.25f;
                if (Main.rand.Next(-10, 120) < num1327 * 100f)
                {
                    NPC.velocity.Y = NPC.velocity.Y - Main.rand.Next(20, 40) * 0.025f;
                    NPC.velocity.X = NPC.velocity.X + Main.rand.Next(-20, 20) * 0.025f;
                    NPC.velocity *= 1f + num1327 * 2f;
                    NPC.netUpdate = true;
                    return;
                }
            }
        }
    }
}