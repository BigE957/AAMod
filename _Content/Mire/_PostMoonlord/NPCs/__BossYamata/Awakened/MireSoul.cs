using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic._Content.Mire.Buffs;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.NPCs;
using AAModClassic.Utilities;

namespace AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata.Awakened
{
    public class MireSoul : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mire Soul");
            Main.npcFrameCount[NPC.type] = 6;

            /*
            NPCID.Sets.NPCBestiaryDrawModifiers value = new()
            {
                PortraitPositionXOverride = 0,
                Position = new Vector2(-12, 0),
                Direction = 1
            };
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
            */

            this.HideFromBestiary();
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.ShadowFlameApparition);
            AIType = NPCID.ShadowFlameApparition;
            AnimationType = NPCID.ShadowFlameApparition;
            NPC.npcSlots = 0;
            NPC.value = Item.buyPrice(0, 0, 0, 0);
            NPC.aiStyle = NPCAIStyleID.AncientVision;
            NPC.lifeMax = 1;
            NPC.dontTakeDamage = true;
            NPC.noGravity = true;
            NPC.damage = 200;
            if (!NPC.IsABestiaryIconDummy)
                NPC.alpha = 255;

        }
        public override void AI()
        {
            Lighting.AddLight(NPC.Center, AAColor.YamataA.R / 255f, AAColor.YamataA.G / 255f, AAColor.YamataA.B / 255f);
            AAAI.AIShadowflameGhost(NPC, ref NPC.ai, false, 660f, 0.3f, 15f, 0.2f, 8f, 5f, 10f, 0.4f, 0.4f, 0.95f, 5f);
            if (!NPC.AnyNPCs(ModContent.NPCType<YamataABody>()))
            {
                NPC.life = 0;
            }
            if (NPC.alpha != 0)
            {
                for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                {
                    int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, ModContent.DustType<Dusts.YamataAuraDust>(), 0f, 0f, 100, default, 2f);
                    Main.dust[num935].noGravity = true;
                    Main.dust[num935].noLight = true;
                }
            }
            NPC.alpha -= 12;
            if (NPC.alpha < 0)
            {
                NPC.alpha = 0;
            }
        }

        public static Color GetGlowAlpha()
        {
            return new Color(200, 0, 50) * (Main.mouseTextColor / 255f);
        }
        public float auraPercent = 0f;
        public bool auraDirection = true;

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D glowTex = TextureAssets.Npc[Type].Value;
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }

            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos, NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.SpriteEffectDirection(true), 0);
            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos, NPC.frame, GetGlowAlpha(), NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.SpriteEffectDirection(true), 0);
            if(!NPC.IsABestiaryIconDummy)
                DrawingUtils.DrawAfterimageWithVelocity(spriteBatch, glowTex, NPC.Center - screenPos, NPC.velocity, 4, NPC.frame, Color.White, NPC.scale, [NPC.rotation], NPC.frame.Size() * 0.5f, NPC.SpriteEffectDirection(true), 0.8f);
            return false;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(ModContent.BuffType<HydraToxin_Buff>(), 600);
        }
    }
}
