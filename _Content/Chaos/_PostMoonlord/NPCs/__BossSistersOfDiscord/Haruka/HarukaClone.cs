using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;

namespace AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossSistersOfDiscord.Haruka
{
    [AutoloadBossHead]
    public class HarukaClone : ModNPC
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Haruka Yamata");
            this.HideFromBestiary();
        }

        public override void SetDefaults()
        {
            NPC.width = 50;
            NPC.height = 60;
            NPC.friendly = false;
            NPC.dontTakeDamage = true;
            NPC.damage = 150;
            NPC.defense = 9999;
            NPC.lifeMax = 150000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.knockBackResist = 0f;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.lavaImmune = true;
            NPC.netAlways = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.BossBar = Main.BigBossProgressBar.NeverValid;
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * balance);
            NPC.damage = (int)(NPC.damage * 0.9f);
        }

        public override void AI()
        {
            NPC Haruka = Main.npc[(int)NPC.ai[0]];
            if(!Main.npc[(int)NPC.ai[0]].active || Main.npc[(int)NPC.ai[0]].life <= 0)
            {
                NPC.life = 0;
                NPC.active = false;
            }
            if(((Haruka)Haruka.ModNPC).internalAI[0] != 4)
            {
                NPC.boss = false;
                NPC.life = 0;
                NPC.active = false;
            }

            //Unfuck the scaling
            NPC.damage = 150;
        }


        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (NPC.velocity != Vector2.Zero)
            {
                BaseDrawing.DrawAfterimage(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, 1.5f, 1f, 3, false, 0f, 0f, Color.Navy);
            }
            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos, NPC.frame, NPC.GetAlpha(drawColor), NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, SpriteEffects.None, 0);
            return false;
        }
    }
}