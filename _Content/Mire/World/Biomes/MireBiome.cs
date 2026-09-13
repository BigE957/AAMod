using AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata;
using AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata.Awakened;
using AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata.Awakened.Skies;
using AAModClassic._Content.Mire.World.Biomes.Water;
using AAModClassic._CrossMod;
using AAModClassic._CrossMod.WrathOfTheGods;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Music;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria.GameContent;
using Terraria.GameContent.Events;
using Terraria.GameInput;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.World.Biomes
{
    public class MireBiome : ModBiome
    {
        public override string MapBackground => "AAModClassic/_Content/Mire/World/Biomes/Backgrounds/MireBiome_MapBG";

        public override string BackgroundPath => "AAModClassic/_Content/Mire/World/Biomes/Backgrounds/MireBiome_MapBG";

        public override bool IsBiomeActive(Player player) => AAWorld.mireTiles > 100 || BaseAI.GetNPC(player.Center, ModContent.NPCType<YamataBody>(), 5000) != -1 || BaseAI.GetNPC(player.Center, ModContent.NPCType<YamataABody>(), 5000) != -1;

        public override void SpecialVisuals(Player player, bool isActive)
        {
            bool rllyActive = (!ContentReplacementSystem.NeedToReplaceContent && (isActive && player.Center.Y <= Main.worldSurface * 16) || player.GetModPlayer<ZAAPlayer>().MoonAltar) && !ModContent.GetInstance<YamataASkyScene>().IsSceneEffectActive(player);
            player.ManageSpecialBiomeVisuals("AAModClassic:MireSky", rllyActive);
        }

        public override int Music =>
            Main.LocalPlayer.ZoneRockLayerHeight ? MusicManagementSystem.MusicSlots["Mire_Underground"] :
            (AAWorld.downedAllAncients && !AAWorld.downedShen) ? MusicManagementSystem.MusicSlots["Chaos_PreShen"] :
            Main.dayTime ? MusicManagementSystem.MusicSlots["Mire_Day"] :
            MusicManagementSystem.MusicSlots["Mire_Surface"];

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;

        public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle => Main.LocalPlayer.ZoneDesert ? ModContent.GetInstance<MireDesertBgStyle>() : !Main.LocalPlayer.ZoneSnow ? ModContent.GetInstance<MireSurfaceBgStyle>() : null;

        public override ModUndergroundBackgroundStyle UndergroundBackgroundStyle => ModContent.GetInstance<MireUgBgStyle>();

        public override ModWaterStyle WaterStyle => Main.dayTime && !AAWorld.downedYamata && Main.LocalPlayer.position.Y < Main.worldSurface * 16.0 && !Main.LocalPlayer.buffImmune[ModContent.BuffType<Buffs.Clueless_Buff>()] ? ModContent.GetInstance<FogWaterStyle>() : ModContent.GetInstance<MireWaterStyle>();
    }

    public class UndergroundMireBiome : ModBiome
    {
        public override string BackgroundPath => "AAModClassic/_Content/Mire/World/Biomes/Backgrounds/MireBiome_MapBG";
    }

    public class MireSky : CustomSky
    {
        public bool Active;
        public float Intensity;

        public static Asset<Texture2D> MoonTex;
        public static Asset<Texture2D> SkyTex;

        public override void OnLoad()
        {
            MoonTex = ModContent.Request<Texture2D>("AAModClassic/_Content/Mire/World/Biomes/Backgrounds/MireBiome_Moon");
            SkyTex = ModContent.Request<Texture2D>("AAModClassic/_Content/Mire/World/Biomes/Backgrounds/MireBiome_Sky");
        }

        public override void Update(GameTime gameTime)
        {
            if (Active)
                Intensity = Math.Min(1f, 0.01f + Intensity);
            else
                Intensity = Math.Max(0f, Intensity - 0.01f);
        }

        public override Color OnTileColor(Color inColor)
        {
            bool SilouetteMode = !Main.gameMenu && Main.LocalPlayer.GetModPlayer<ZAAPlayer>().Clueless;
            if (SilouetteMode)
                return new(1, 1, 1);

            return Color.Lerp(inColor, Color.White, Intensity * 0.5f);
        }

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            if (AAMod.instance == null)
                return;

            Texture2D moon = MoonTex.Value;
            Texture2D sky = SkyTex.Value;
            if (maxDepth >= 3.40282347E+38f && minDepth < 3.40282347E+38f)
            {
                if (Main.gameMenu || !Main.dayTime || Main.LocalPlayer.GetModPlayer<ZAAPlayer>().MoonAltar)
                {
                    if (!WrathOfTheGods.IsEnabled || !(bool)WrathOfTheGods.Call("GetRiftEclipseActive"))
                        spriteBatch.Draw(sky, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.White * Intensity);

                    if (WrathOfTheGods.IsEnabled && (bool)WrathOfTheGods.Call("GetRiftEclipseActive"))
                        return;

                    double bgTop = 0;
                    if (!Main.gameMenu)
                    {
                        float clampedScreenH = Math.Min(PlayerInput.RealScreenHeight, Main.LogicCheckScreenHeight);
                        float screenMidY = Main.screenPosition.Y + Main.screenHeight / 2f - clampedScreenH / 2f;
                        bgTop = (int)((-screenMidY) / (Main.worldSurface * 16.0 - 600.0) * 200.0);
                    }

                    int moonX = (int)(Main.time / 32400.0 * (Main.screenWidth + TextureAssets.Moon[Main.moonType].Width() * 2)) - TextureAssets.Moon[Main.moonType].Width();
                    int moonY = 0;
                    float moonScale = 1f;
                    float rotation2 = (float)(Main.time / 32400.0) * 2f - 7.3f;
                    if (!Main.dayTime)
                    {
                        double timeMult;
                        if (Main.time < 16200.0)
                        {
                            timeMult = Math.Pow(1.0 - Main.time / 32400.0 * 2.0, 2.0);
                            moonY = (int)(bgTop + timeMult * 250.0 + 180.0);
                        }
                        else
                        {
                            timeMult = Math.Pow((Main.time / 32400.0 - 0.5) * 2.0, 2.0);
                            moonY = (int)(bgTop + timeMult * 250.0 + 180.0);
                        }
                        moonScale = (float)(1.2 - timeMult * 0.4);
                    }

                    float moonOpacity = 1f - Main.cloudAlpha * 1.5f;
                    if (moonOpacity < 0f)
                        moonOpacity = 0f;

                    var yamataSky = ModContent.GetInstance<YamataASky>();
                    if (!Main.gameMenu && (yamataSky == null || !yamataSky.IsActive()))
                        moonScale = MathHelper.Lerp(0.25f, moonScale, Intensity);

                    Vector2 drawPos = new(moonX, moonY + Main.moonModY);
                    spriteBatch.Draw(moon, drawPos, null, Color.White * moonOpacity * Intensity, rotation2, moon.Size() / 2f, moonScale, SpriteEffects.None, 0f);
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
        }

        public override void Deactivate(params object[] args)
        {
            if ((!Main.gameMenu && !Main.LocalPlayer.GetModPlayer<ZAAPlayer>().ZoneMire && !Main.LocalPlayer.GetModPlayer<ZAAPlayer>().MoonAltar && !Main.LocalPlayer.GetModPlayer<ZAAPlayer>().YamataAltar) || (args.Length > 0 && (bool)args[0] == true))
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

    public class MireSkyData : ScreenShaderData
    {
        public MireSkyData(string passName) : base(passName)
        {
        }

        private static void UpdateMireSky()
        {
            if (AAWorld.mireTiles < 100)
            {
                return;
            }
        }

        public override void Apply()
        {
            UpdateMireSky();
            base.Apply();
        }
    }

    public class MireSurfaceBgStyle : ModSurfaceBackgroundStyle
    {
        readonly ScreenFog mireBGFog = new(true);

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
            return -1;// BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/Mire/World/Biomes/Backgrounds/MireBiome_SurfaceBackground");
        }
        public override int ChooseMiddleTexture()
        {
            return -1;// BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/Mire/World/Biomes/Backgrounds/MireBiome_SurfaceBackground");
        }
        public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
        {
            return BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/Mire/World/Biomes/Backgrounds/MireBiome_SurfaceForeground1");
        }

        public override bool PreDrawCloseBackground(SpriteBatch spriteBatch)
        {
            Color DefaultFog = new Color(120, 120, 200);
            Color YamataFog = new Color(200, 100, 100);

            bool YamataA = NPC.AnyNPCs(ModContent.NPCType<YamataABody>());

            mireBGFog.Update(ModContent.Request<Texture2D>("AAModClassic/_Content/Mire/World/Biomes/Backgrounds/FogTex").Value);
            mireBGFog.Draw(ModContent.Request<Texture2D>("AAModClassic/_Content/Mire/World/Biomes/Backgrounds/FogTex").Value, true, YamataA ? YamataFog : DefaultFog);

            if (!Main.dayTime)
            {
                // scAdj - uses clamped height
                float clampedScreenH = Math.Min(PlayerInput.RealScreenHeight, Main.LogicCheckScreenHeight);
                float num2 = Main.screenPosition.Y + (float)(Main.screenHeight / 2) - clampedScreenH / 2f;
                float scAdj = (float)(Main.worldSurface * 16.0) / (num2 + clampedScreenH);
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

                int textureSlot = BackgroundTextureLoader.GetBackgroundSlot(AAMod.instance, "_Content/Mire/World/Biomes/Backgrounds/MireBiome_SurfaceBackground");
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
                    bgTopY = (int)screenOff - 80;
                else
                    bgTopY -= 96;

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

                return true;
            }

            return false;
        }
    }

    public class ScreenFog(bool bg)
    {
        public int fogOffsetX = 0;
        public float fadeOpacity = 0f;
        public float dayTimeOpacity = 0f;
        public bool backgroundFog = bg;

        public void Update(Texture2D texture)
        {
            if (Main.netMode == NetmodeID.Server || Main.dedServ || Main.gameMenu) return; //BEGONE SERVER HEATHENS! UPDATE ONLY CLIENTSIDE!

            Player player = Main.LocalPlayer;
            bool inMire = Main.LocalPlayer.GetModPlayer<ZAAPlayer>().ZoneMire;
            if (!backgroundFog && (BasePlayer.HasAccessory(player, AAMod.instance.Find<ModItem>("Lantern").Type, true, false) || AAWorld.downedYamata)) inMire = false;

            fogOffsetX += 1;
            if (fogOffsetX >= texture.Width) fogOffsetX = 0;
            if (inMire)
            {
                fadeOpacity += 0.05f;
                if (fadeOpacity > 1f) fadeOpacity = 1f;
            }
            else
            {
                fadeOpacity -= 0.05f;
                if (fadeOpacity < 0f) fadeOpacity = 0f;
            }
            if (!backgroundFog)
            {
                dayTimeOpacity = Main.dayTime ? BaseUtility.MultiLerp((float)Main.time / 52000f, 0.5f, 1f, 1f, 1f, 1f, 1f, 0.5f) : 0.5f;
                dayTimeOpacity *= 0.7f; //make it fadier as it's in the background
            }
            else
            {
                dayTimeOpacity = Main.dayTime ? BaseUtility.MultiLerp((float)Main.time / 52000f, 1f, 1f, 1f, 1f, 1f, 1f, 1f) : 0.3f;
                dayTimeOpacity *= Main.dayTime ? 3f : 1f;
            }
        }

        public void Draw(Texture2D texture, bool dir, Color defaultColor, bool setSB = false)
        {
            if (fadeOpacity == 0f) return; //don't draw if no fog
            if (setSB) Main.spriteBatch.Begin();
            Player player = Main.LocalPlayer;

            Color DefaultFog = new Color(62, 68, 100);
            Color YamataFog = new Color(100, 38, 62);

            bool YamataA = NPC.AnyNPCs(ModContent.NPCType<YamataABody>());

            Color fogColor = GetAlpha(YamataA ? YamataFog : DefaultFog, 0.4f * fadeOpacity * dayTimeOpacity);

            //ensure we cover the whole screen first
            // Main.spriteBatch.Draw(texture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), null, bgColor, 0f, Vector2.Zero, SpriteEffects.None, 0f);	

            //overlap a little so you cannot see edges

            int minX = -texture.Width;
            int minY = -texture.Height;
            int maxX = Main.screenWidth + texture.Width;
            int maxY = Main.screenHeight + texture.Height;


            for (int i = minX; i < maxX; i += texture.Width)
            {
                for (int j = minY; j < maxY; j += texture.Height)
                {
                    if (player.position.Y < Main.worldSurface * 16.0)
                    {
                        Main.spriteBatch.Draw(texture, new Rectangle(i + (dir ? -fogOffsetX : fogOffsetX), j, texture.Width, texture.Height), null, fogColor, 0f, Vector2.Zero, SpriteEffects.None, 0f);
                    }
                }
            }
            if (setSB) Main.spriteBatch.End();
        }

        public static Color GetAlpha(Color newColor, float alph)
        {
            int alpha = 255 - (int)(255 * alph);
            float alphaDiff = (255 - alpha) / 255f;
            int newR = (int)(newColor.R * alphaDiff);
            int newG = (int)(newColor.G * alphaDiff);
            int newB = (int)(newColor.B * alphaDiff);
            int newA = newColor.A - alpha;
            if (newA < 0) newA = 0;
            if (newA > 255) newA = 255;
            return new Color(newR, newG, newB, newA);
        }
    }

    public class MireUgBgStyle : ModUndergroundBackgroundStyle
    {
        public override void FillTextureArray(int[] textureSlots)
        {
            textureSlots[0] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/Mire/World/Biomes/Backgrounds/MireBiome_UndergroundTop");
            textureSlots[1] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/Mire/World/Biomes/Backgrounds/MireBiome_Underground");
            textureSlots[2] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/Mire/World/Biomes/Backgrounds/MireBiome_CavernTop");
            textureSlots[3] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/Mire/World/Biomes/Backgrounds/MireBiome_Cavern");
        }
    }

    public class MireDesertBgStyle : ModSurfaceBackgroundStyle
    {
        readonly ScreenFog mireBGFog = new ScreenFog(true);

        public override void Load()
        {
            On_Sandstorm.ShouldSandstormDustPersist += AllowSandstormVisualsInMireDesert;
        }

        private bool AllowSandstormVisualsInMireDesert(On_Sandstorm.orig_ShouldSandstormDustPersist orig)
        {
            if (orig())
                return true;

            if (Sandstorm.Happening && Main.LocalPlayer.ZoneSandstorm && Main.bgStyle == Slot)
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
            return BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/Mire/World/Biomes/Backgrounds/MireBiome_SurfaceBackground_Desert");
        }

        public override bool PreDrawCloseBackground(SpriteBatch spriteBatch)
        {
            Color DefaultFog = new Color(120, 120, 200);
            Color YamataFog = new Color(200, 100, 100);

            bool YamataA = NPC.AnyNPCs(ModContent.NPCType<YamataABody>());

            mireBGFog.Update(ModContent.Request<Texture2D>("AAModClassic/_Content/Mire/World/Biomes/Backgrounds/FogTex").Value);
            mireBGFog.Draw(ModContent.Request<Texture2D>("AAModClassic/_Content/Mire/World/Biomes/Backgrounds/FogTex").Value, true, YamataA ? YamataFog : DefaultFog);
            return Main.dayTime ? false : true;
        }

    }
}
