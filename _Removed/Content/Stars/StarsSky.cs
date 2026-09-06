using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Stars
{
    public class StarsSky : CustomSky
    {
        private struct Star
        {
            public Vector2 Position;

            public float Depth;

            public int TextureIndex;

            public float SinOffset;

            public float AlphaFrequency;

            public float AlphaAmplitude;
        }

        //private UnifiedRandom random = new UnifiedRandom();

        public static Asset<Texture2D>[] starTextures;

        private bool isActive;

        private Star[] stars;

        private float fadeOpacity;

        public override void OnLoad()
        {
            if (Main.dedServ)
                return;

            starTextures = new Asset<Texture2D>[2];
            for (int i = 0; i < starTextures.Length; i++)
            {
                starTextures[i] = ModContent.Request<Texture2D>("AAModClassic/_Removed/Content/Stars/Star" + i);// mod.GetTexture("Backgrounds/Star " + i);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (isActive)
            {
                fadeOpacity = Math.Min(1f, 0.01f + fadeOpacity);
                return;
            }
            fadeOpacity = Math.Max(0f, fadeOpacity - 0.01f);
        }

        public override Color OnTileColor(Color inColor)
        {
            Vector4 value = inColor.ToVector4();
            return new Color(Vector4.Lerp(value, Vector4.One, fadeOpacity * 0.5f));
        }

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            int num = -1;
            int num2 = 0;
            for (int i = 0; i < stars.Length; i++)
            {
                float depth = stars[i].Depth;
                if (num == -1 && depth < maxDepth)
                {
                    num = i;
                }
                if (depth <= minDepth)
                {
                    break;
                }
                num2 = i;
            }
            if (num == -1)
            {
                return;
            }

            float scale = Math.Min(1f, (2400f - Main.screenPosition.Y) / 1000f);
            Vector2 value3 = Main.screenPosition + new Vector2((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
            Rectangle rectangle = new(-1000, -1000, 4000, 4000);
            for (int j = num; j < num2; j++)
            {
                Vector2 value4 = new Vector2(1f / stars[j].Depth, 1.1f / stars[j].Depth);
                Vector2 position = (stars[j].Position - value3) * value4 + value3 - Main.screenPosition;
                if (rectangle.Contains((int)position.X, (int)position.Y))
                {
                    float opacity = (float)Math.Sin((stars[j].AlphaFrequency * Main.GlobalTimeWrappedHourly + stars[j].SinOffset)) * stars[j].AlphaAmplitude + stars[j].AlphaAmplitude;
                    float invOpacity = (float)Math.Sin((stars[j].AlphaFrequency * Main.GlobalTimeWrappedHourly * 5f + stars[j].SinOffset)) * 0.1f - 0.1f;
                    opacity = MathHelper.Clamp(opacity, 0f, 1f);

                    Texture2D texture2D = starTextures[stars[j].TextureIndex].Value;
                    Color StarColor = Color.BlueViolet;
                    if (Main.dayTime)
                        StarColor = Color.Goldenrod;

                    spriteBatch.Draw(texture2D, position, null, StarColor * scale * opacity * 0.8f * (1f - invOpacity) * fadeOpacity, 0f, new Vector2((texture2D.Width >> 1), (texture2D.Height >> 1)), (value4.X * 0.5f + 0.5f) * (opacity * 0.3f + 0.7f), SpriteEffects.None, 0f);
                }
            }
        }

        public override float GetCloudAlpha()
        {
            return (1f - fadeOpacity) * 0.3f + 0.7f;
        }

        public override void Activate(Vector2 position, params object[] args)
        {
            fadeOpacity = 0.002f;
            isActive = true;
            int num = 200;
            int num2 = 10;
            stars = new Star[num * num2];
            int num3 = 0;
            for (int i = 0; i < num; i++)
            {
                float num4 = (float)i / (float)num;
                for (int j = 0; j < num2; j++)
                {
                    float num5 = (float)j / (float)num2;
                    stars[num3].Position.X = num4 * (float)Main.maxTilesX * 16f;
                    stars[num3].Position.Y = num5 * ((float)Main.worldSurface * 16f + 2000f) - 1000f;
                    stars[num3].Depth = Main.rand.NextFloat() * 8f + 1.5f;
                    stars[num3].TextureIndex = Main.rand.Next(starTextures.Length);
                    stars[num3].SinOffset = Main.rand.NextFloat() * 6.28f;
                    stars[num3].AlphaAmplitude = Main.rand.NextFloat() * 5f;
                    stars[num3].AlphaFrequency = Main.rand.NextFloat() + 1f;
                    num3++;
                }
            }
            Array.Sort<Star>(stars, new Comparison<Star>(SortMethod));
        }

        private int SortMethod(Star meteor1, Star meteor2)
        {
            return meteor2.Depth.CompareTo(meteor1.Depth);
        }

        public override void Deactivate(params object[] args)
        {
            isActive = false;
        }

        public override void Reset()
        {
            isActive = false;
        }

        public override bool IsActive()
        {
            return isActive || fadeOpacity > 0.001f;
        }
    }
}