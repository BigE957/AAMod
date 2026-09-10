using AAModClassic._Content._Misc.___PreHardmode.Items.Accessories.Vanity;
using AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma;
using AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma.Awakened;
using AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma.Awakened.Skies;
using AAModClassic._Content.Inferno.World.Biomes.Waters;
using AAModClassic._CrossMod;
using AAModClassic._CrossMod.WrathOfTheGods;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Music;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Reflection;
using Terraria.GameContent;
using Terraria.GameContent.Events;
using Terraria.GameInput;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace AAModClassic._Content.Inferno.World.Biomes
{
    public class InfernoBiome : ModBiome
    {
        public override string MapBackground => "AAModClassic/_Content/Inferno/World/Biomes/Backgrounds/InfernoMap";

        public override string BackgroundPath => "AAModClassic/_Content/Inferno/World/Biomes/Backgrounds/InfernoMap";

        internal static FieldInfo BackgroundTopY = null;

        public override void Load()
        {
            var field = typeof(Main).GetField("bgTopY", BindingFlags.Instance | BindingFlags.NonPublic);
            if (field != null)
                BackgroundTopY = field;
        }

        public override bool IsBiomeActive(Player player) => AAWorld.infernoTiles > 100 || BaseAI.GetNPC(player.Center, ModContent.NPCType<AkumaHead>(), 5000) != -1 || BaseAI.GetNPC(player.Center, ModContent.NPCType<AkumaAHead>(), 5000) != -1;

        public override void SpecialVisuals(Player player, bool isActive)
        {
            bool rllyActive = (!ContentReplacementSystem.NeedToReplaceContent && (isActive && player.Center.Y <= Main.worldSurface * 16) || player.GetModPlayer<ZAAPlayer>().SunAltar) && !ModContent.GetInstance<AkumaASkyScene>().IsSceneEffectActive(player);
            player.ManageSpecialBiomeVisuals("AAModClassic:InfernoSky", rllyActive);
            player.ManageSpecialBiomeVisuals("HeatDistortion", rllyActive && Main.UseHeatDistortion);
        }

        public override int Music =>
            Main.LocalPlayer.ZoneRockLayerHeight ? MusicManagementSystem.MusicSlots["Inferno_Underground"] :
            (AAWorld.downedAllAncients && !AAWorld.downedShen) ? MusicManagementSystem.MusicSlots["Chaos_PreShen"] :
            !Main.dayTime ? MusicManagementSystem.MusicSlots["Inferno_Night"] :
            MusicManagementSystem.MusicSlots["Inferno_Surface"];

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;

        public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle
        {
            get
            {
                if ((Main.LocalPlayer.ZoneDesert && Main.LocalPlayer.ZoneSnow) && ModLoader.TryGetMod("SpiritReforged", out var spirit))
                {
                    //Rectangle saltFlatsArea = (Rectangle)spirit.Call("GetSaltFlatsArea");
                    //bool playerInSaltFlats = saltFlatsArea.Contains(Main.LocalPlayer.Center.ToTileCoordinates());
                    //Main.NewText(saltFlatsArea);
                    //if (playerInSaltFlats)
                    return null;
                }

                return Main.LocalPlayer.ZoneDesert ? ModContent.GetInstance<InfernoDesertBgStyle>() : ModContent.GetInstance<InfernoSurfaceBgStyle>();
            }
        }

        public override ModUndergroundBackgroundStyle UndergroundBackgroundStyle => ModContent.GetInstance<InfernoUgBgStyle>();

        public override ModWaterStyle WaterStyle => ModContent.GetInstance<InfernoWaterStyle>();
    }

    public class UndergroundInfernoBiome : ModBiome
    {
        public override string BackgroundPath => "AAModClassic/_Content/Inferno/World/Biomes/Backgrounds/InfernoMap";
    }

    public class InfernoSky : CustomSky
    {
        public bool Active;
        public float Intensity;
        private struct Meteor
        {
            public Vector2 Position;

            public float Depth;

            public int FrameCounter;

            public float Scale;

            public float StartX;
        }
        private Meteor[] Meteors;

        private readonly UnifiedRandom _random = new UnifiedRandom();

        public static Asset<Texture2D> SunTex;
        public static Asset<Texture2D> DemonSunTex;
        public static Asset<Texture2D> MeteorTex;
        public static Asset<Texture2D> SkyTex;

        public override void OnLoad()
        {
            SunTex = ModContent.Request<Texture2D>("AAModClassic/_Content/Inferno/World/Biomes/Backgrounds/Sun");
            DemonSunTex = ModContent.Request<Texture2D>("AAModClassic/_Content/Inferno/World/Biomes/Backgrounds/DemonSun");
            MeteorTex = ModContent.Request<Texture2D>("AAModClassic/_Content/Inferno/World/Biomes/Backgrounds/Meteor");
            SkyTex = ModContent.Request<Texture2D>("AAModClassic/_Content/Inferno/World/Biomes/Backgrounds/SkyTex");
        }

        public override void Update(GameTime gameTime)
        {
            if (Active)
            {
                Intensity = Math.Min(1f, 0.01f + Intensity);
            }
            else
            {
                Intensity = Math.Max(0f, Intensity - 0.01f);
            }

            if (Intensity > 0 && WeakReferences.RealisticSky != null)
            {
                if (SkyManager.Instance["AAModClassic:AkumaASky"].IsActive())
                    WeakReferences.RealisticSky.Call("setsunbloomopacity", 0);
                else
                    WeakReferences.RealisticSky.Call("setsunbloomopacity", 1 - Intensity);
            }
        }

        public override Color OnTileColor(Color inColor)
        {
            Vector4 value = inColor.ToVector4();
            return new Color(Vector4.Lerp(value, Vector4.One, Intensity * 0.5f));
        }

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            if (AAMod.instance == null)
                return;

            Texture2D sunTex = SunTex.Value;
            Texture2D demonSun = DemonSunTex.Value;
            Texture2D MeteorTexture = MeteorTex.Value;
            Texture2D SkyTexture = SkyTex.Value;

            if (maxDepth >= 3.40282347E+38f && minDepth < 3.40282347E+38f)
            {
                if (Main.gameMenu || Main.dayTime || Main.LocalPlayer.GetModPlayer<ZAAPlayer>().SunAltar)
                {
                    if(!WrathOfTheGods.IsEnabled || !(bool)WrathOfTheGods.Call("GetRiftEclipseActive"))
                        spriteBatch.Draw(TextureAssets.BlackTile.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black * Intensity);
                    
                    if (Main.gameMenu)
                        spriteBatch.Draw(SkyTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.OrangeRed * Intensity);
                    else
                        spriteBatch.Draw(SkyTexture, new Rectangle(0, Math.Max(0, (int)((Main.worldSurface * 16.0 - Main.screenPosition.Y - 2400.0) * 0.10000000149011612)), Main.screenWidth, Main.screenHeight), Color.OrangeRed * Math.Min(1f, (Main.screenPosition.Y - 800f) / 1000f * Intensity));

                    if (WrathOfTheGods.IsEnabled && (bool)WrathOfTheGods.Call("GetRiftEclipseActive"))
                        return;

                    float sunOpacity = 1f;
                    sunOpacity -= Main.cloudAlpha * 1.5f;
                    if (sunOpacity < 0f)
                        sunOpacity = 0f;

                    int sunX = (int)(Main.time / 54000.0 * (Main.screenWidth + TextureAssets.Sun.Value.Width * 2)) - TextureAssets.Sun.Value.Width;
                    int sunY = 0;
                    float sunScale = 1f;
                    float rotation = (float)(Main.time / 54000.0) * 2f - 7.3f;

                    double bgTop = 0;
                    if (!Main.gameMenu)
                    {
                        float clampedScreenH = Math.Min(PlayerInput.RealScreenHeight, Main.LogicCheckScreenHeight);
                        float screenMidY = Main.screenPosition.Y + Main.screenHeight / 2f - clampedScreenH / 2f;
                        bgTop = (int)((-screenMidY) / (Main.worldSurface * 16.0 - 600.0) * 200.0);
                    }

                    if (Main.dayTime)
                    {
                        double timeMult;
                        if (Main.time < 27000.0)
                        {
                            timeMult = Math.Pow(1.0 - Main.time / 54000.0 * 2.0, 2.0);
                            sunY = (int)(bgTop + timeMult * 250.0 + 180.0);
                        }
                        else
                        {
                            timeMult = Math.Pow((Main.time / 54000.0 - 0.5) * 2.0, 2.0);
                            sunY = (int)(bgTop + timeMult * 250.0 + 180.0);
                        }
                        sunScale = (float)(1.2 - timeMult * 0.4);
                    }

                    AkumaASky akumaSky = ModContent.GetInstance<AkumaASky>();
                    if (!Main.gameMenu && (akumaSky == null || !akumaSky.IsActive()))
                        sunScale = MathHelper.Lerp(0.25f, sunScale, Intensity);

                    if (!Main.gameMenu && BasePlayer.HasAccessory(Main.LocalPlayer, ModContent.ItemType<HappySunSticker>(), true, true))
                        spriteBatch.Draw(demonSun, new Vector2(sunX, sunY + Main.sunModY), null, Color.White * sunOpacity * Intensity, rotation, sunTex.Size() / 2f, sunScale, SpriteEffects.None, 0f);
                    else
                        spriteBatch.Draw(sunTex, new Vector2(sunX, sunY + Main.sunModY), null, Color.White * sunOpacity * Intensity, rotation, sunTex.Size() / 2f, sunScale, SpriteEffects.None, 0f);
                }
            }
            int num = -1;
            int num2 = 0;
            if (NPC.AnyNPCs(ModContent.NPCType<AkumaHead>()))
            {
                for (int i = 0; i < Meteors.Length; i++)
                {
                    float depth = Meteors[i].Depth;
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
                float scale = Math.Min(1f, (Main.screenPosition.Y - 1000f) / 1000f);
                Vector2 value3 = Main.screenPosition + new Vector2(Main.screenWidth >> 1, Main.screenHeight >> 1);
                Rectangle rectangle = new Rectangle(-1000, -1000, 4000, 4000);
                for (int j = num; j < num2; j++)
                {
                    Vector2 value4 = new Vector2(1f / Meteors[j].Depth, 0.9f / Meteors[j].Depth);
                    Vector2 position = (Meteors[j].Position - value3) * value4 + value3 - Main.screenPosition;
                    int num3 = Meteors[j].FrameCounter / 3;
                    Meteors[j].FrameCounter = (Meteors[j].FrameCounter + 1) % 12;
                    if (rectangle.Contains((int)position.X, (int)position.Y))
                    {
                        spriteBatch.Draw(MeteorTexture, position, new Rectangle?(new Rectangle(0, num3 * (MeteorTexture.Height / 4), MeteorTexture.Width, MeteorTexture.Height / 4)), Color.White * scale * Intensity, 0f, Vector2.Zero, value4.X * 5f * Meteors[j].Scale, SpriteEffects.None, 0f);
                    }
                }
            }
        }

        public override float GetCloudAlpha()
        {
            return 1f - Intensity;
        }

        public override void Activate(Vector2 position, params object[] args)
        {
            Intensity = 0.002f;
            Active = true;
            Meteors = new Meteor[150];
            for (int i = 0; i < Meteors.Length; i++)
            {
                float num = i / (float)Meteors.Length;
                Meteors[i].Position.X = num * (Main.maxTilesX * 16f) + _random.NextFloat() * 40f - 20f;
                Meteors[i].Position.Y = _random.NextFloat() * -((float)Main.worldSurface * 16f + 10000f) - 10000f;
                if (_random.NextBool(3))
                {
                    Meteors[i].Depth = _random.NextFloat() * 3f + 1.8f;
                }
                else
                {
                    Meteors[i].Depth = _random.NextFloat() * 5f + 4.8f;
                }
                Meteors[i].FrameCounter = _random.Next(12);
                Meteors[i].Scale = _random.NextFloat() * 0.5f + 1f;
                Meteors[i].StartX = Meteors[i].Position.X;
            }
            Array.Sort(Meteors, new Comparison<Meteor>(SortMethod));
        }
        private int SortMethod(Meteor meteor1, Meteor meteor2)
        {
            return meteor2.Depth.CompareTo(meteor1.Depth);
        }

        public override void Deactivate(params object[] args)
        {
            if ((!Main.gameMenu && !Main.LocalPlayer.GetModPlayer<ZAAPlayer>().ZoneInferno && !Main.LocalPlayer.GetModPlayer<ZAAPlayer>().SunAltar && !Main.LocalPlayer.GetModPlayer<ZAAPlayer>().AkumaAltar) || (args.Length > 0 && (bool)args[0] == true))
                Active = false;
        }

        public override void Reset()
        {
            Active = false;
        }

        public override bool IsActive()
        {
            return Active || Intensity > 0.001f;
        }
    }

    public class InfernoSkyData(string passName) : ScreenShaderData(passName)
    {
        private static void UpdateInfernoSky()
        {
            if (AAWorld.infernoTiles < 100)
            {
                return;
            }
        }

        public override void Apply()
        {
            UpdateInfernoSky();
            base.Apply();
        }
    }

    public class InfernoSurfaceBgStyle : ModSurfaceBackgroundStyle
    {
        public override void ModifyFarFades(float[] fades, float transitionSpeed)
        {
            for (int i = 0; i < fades.Length; i++)
            {
                if (i == Slot)
                {
                    fades[i] += transitionSpeed;
                    if (fades[i] > 1f)
                    {
                        fades[i] = 1f;
                    }
                }
                else
                {
                    fades[i] -= transitionSpeed;
                    if (fades[i] < 0f)
                    {
                        fades[i] = 0f;
                    }
                }
            }
        }

        public override int ChooseFarTexture()
        {
            return -1;// BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/Inferno/World/Biomes/Backgrounds/InfernoBG");
        }

        public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
        {
            return BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/Inferno/World/Biomes/Backgrounds/InfernoBG");
        }

        public override int ChooseMiddleTexture()
        {
            return -1;//BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/Inferno/World/Biomes/Backgrounds/InfernoBG");
        }

        public override bool PreDrawCloseBackground(SpriteBatch spriteBatch)
        {
            // scAdj - uses clamped height
            float clampedScreenH = Math.Min(PlayerInput.RealScreenHeight, Main.LogicCheckScreenHeight);
            float num2 = Main.screenPosition.Y + (float)(Main.screenHeight / 2) - clampedScreenH / 2f;
            float scAdj = (float)(Main.worldSurface * 16.0) / (Main.gameMenu ? 1f : (num2 + clampedScreenH));
            float num3 = (float)Main.maxTilesY * 0.15f * 16f;
            num3 -= num2;
            if (num3 < 0f)
                num3 = 0f;
            num3 *= 0.00025f;
            float num4 = num3 * num3;
            scAdj *= 0.45f - num4;
            if (Main.maxTilesY <= 1200)
                scAdj *= -500f;
            else if (Main.maxTilesY <= 1800)
                scAdj *= -300f;
            else
                scAdj *= -150f;

            // backgroundTopMagicNumber - uses raw screenHeight for screenOff
            float screenOff = Main.screenHeight - 600f;
            float num3v = Main.screenPosition.Y + (float)(Main.screenHeight / 2) - 600f;
            float worldSurface = Main.worldSurface == 0 ? 1f : (float)Main.worldSurface;
            double backgroundTopMagicNumber = (0f - num3v + screenOff / 2f) / (worldSurface * 16f);

            int num6 = 0;
            if (Main.gameMenu)
                num6 += 180;
            int bump = 30;
            if (Main.gameMenu) bump = 0;
            if (WorldGen.drunkWorldGen) bump = -180;
            num6 += bump;

            int textureSlot = BackgroundTextureLoader.GetBackgroundSlot(AAMod.instance, "_Content/Inferno/World/Biomes/Backgrounds/InfernoBG");
            if (textureSlot < 0 || textureSlot >= TextureAssets.Background.Length)
                return false;

            Main.instance.LoadBackground(textureSlot);

            float bgScale = 1.85f;
            double bgParallax = 0.15;
            int bgWidthScaled = (int)(TextureAssets.Background[textureSlot].Width() * bgScale);
            int bgStartX = (int)(-Math.IEEERemainder((double)Main.screenPosition.X * bgParallax, bgWidthScaled) - (double)(bgWidthScaled / 2));
            if (bgWidthScaled == 0)
                bgWidthScaled = 1024;

            int bgLoops = Main.screenWidth / bgWidthScaled + 2;

            int bgTopY = (int)(backgroundTopMagicNumber * 1300.0 + 1090.0) + (int)scAdj + num6;
            if (Main.gameMenu)
                bgTopY = (int)screenOff - 160;
            else
                bgTopY -= 300;

            if (Main.screenPosition.Y >= Main.worldSurface * 16.0 + 16.0)
                return false;

            for (int k = 0; k < bgLoops; k++)
            {
                spriteBatch.Draw(
                    TextureAssets.Background[textureSlot].Value,
                    new Vector2(bgStartX + bgWidthScaled * k, bgTopY),
                    new Rectangle(0, 0, Main.backgroundWidth[textureSlot], Main.backgroundHeight[textureSlot]),
                    Main.ColorOfTheSkies * Main.bgAlphaFarBackLayer[Slot],
                    0f,
                    default,
                    bgScale,
                    SpriteEffects.None,
                    0f
                );
            }

            return false;
        }
    }

    public class InfernoUgBgStyle : ModUndergroundBackgroundStyle
    {
        public override void FillTextureArray(int[] textureSlots)
        {
            textureSlots[0] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/Inferno/World/Biomes/Backgrounds/InfernoUnderground1");
            textureSlots[1] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/Inferno/World/Biomes/Backgrounds/InfernoUnderground");
            textureSlots[2] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/Inferno/World/Biomes/Backgrounds/InfernoCavern1");
            textureSlots[3] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/Inferno/World/Biomes/Backgrounds/InfernoCavern");
        }
    }

    public class InfernoDesertBgStyle : ModSurfaceBackgroundStyle
    {
        public override void Load()
        {
            On_Sandstorm.ShouldSandstormDustPersist += AllowSandstormVisualsInInfernoDesert;
        }

        private bool AllowSandstormVisualsInInfernoDesert(On_Sandstorm.orig_ShouldSandstormDustPersist orig)
        {
            if (orig())
                return true;

            if(Sandstorm.Happening && Main.LocalPlayer.ZoneSandstorm && Main.bgStyle == Slot)
                return Main.bgDelay < 50;

            return false;
        }

        public override void ModifyFarFades(float[] fades, float transitionSpeed)
        {
            Sandstorm.HandleEffectAndSky(Sandstorm.Happening);

            for (int i = 0; i < fades.Length; i++)
            {
                if (i == Slot)
                {
                    fades[i] += transitionSpeed;
                    if (fades[i] > 1f)
                    {
                        fades[i] = 1f;
                    }
                }
                else
                {
                    fades[i] -= transitionSpeed;
                    if (fades[i] < 0f)
                    {
                        fades[i] = 0f;
                    }
                }
            }
        }

        public override int ChooseFarTexture()
        {
            return BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/Inferno/World/Biomes/Backgrounds/InfernoDesertBG");
        }

    }
}