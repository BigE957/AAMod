using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Tiles.Decoration;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Tiles.Decoration.Ancient;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace AAModClassic._Unreleased.Content.Parthenan.World.Biomes
{
    public class ParthenanTexGenAssets : ModSystem
    {
        public static TexGenData ParthenanTileData;
        public static TexGenData ParthenanWallData;

        public override void OnModLoad()
        {
            ParthenanTileData = TexGen.GetTextureForGen("AAModClassic/_Unreleased/Content/Parthenan/World/Biomes/ParthenanGen");
            ParthenanWallData = TexGen.GetTextureForGen("AAModClassic/_Unreleased/Content/Parthenan/World/Biomes/ParthenanGen_Walls");
        }
    }

    public class ParthenanGen : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            int attempts = 0;
            int maxAttempts = 5000;
            Point placementPoint = origin;
            do
            {
                //AAMod.instance.Logger.Info("Attempting to Place Parthenan at: " + placementPoint);

                bool canGenerateInLocation = true;

                if (!structures.CanPlace(new Rectangle(placementPoint.X, placementPoint.Y, ParthenanTexGenAssets.ParthenanTileData.Width, ParthenanTexGenAssets.ParthenanTileData.Height), WorldGenUtils.AllTilesAllowed, 0))
                {
                    //AAMod.instance.Logger.Info("Parthenan Placement Failed, Encountered a Pre-Existing Structure");
                    canGenerateInLocation = false;
                }

                if (canGenerateInLocation)
                {
                    int fullX = placementPoint.X + ParthenanTexGenAssets.ParthenanTileData.Width;
                    int fullY = placementPoint.Y + ParthenanTexGenAssets.ParthenanTileData.Height;

                    for (int x = placementPoint.X; x < fullX; x++)
                    {
                        for (int y = placementPoint.Y; y < fullY; y++)
                        {
                            if (Framing.GetTileSafely(x, y).HasTile)//ShouldAvoidLocation(new Point(x, y), attempts > 1000, attempts > 4000))
                            {
                                canGenerateInLocation = false;
                                break;
                            }
                        }
                        if (!canGenerateInLocation)
                            break;
                    }
                }

                if (canGenerateInLocation)
                {
                    AAMod.instance.Logger.Info("Parthenan successfully placed after " + attempts + " attempts.");
                    break;
                }

                int radius = 200 + attempts / 2;
                int targetX = Math.Clamp(origin.X + WorldGen.genRand.Next(-radius, radius), 40, Main.maxTilesX - (40 + ParthenanTexGenAssets.ParthenanTileData.Width));
                int targetY = origin.Y;
                placementPoint = new Point(targetX, targetY);

            } while (attempts++ < maxAttempts);

            WorldGenUtils.AddProtectedStructure(new Rectangle(origin.X, origin.Y, ParthenanTexGenAssets.ParthenanTileData.Width, ParthenanTexGenAssets.ParthenanTileData.Height), 5);

            //this handles generating the actual tiles, but you still need to add things like treegen etc. I know next to nothing about treegen so you're on your own there, lol.
            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(0, 255, 0)] = ModContent.TileType<FulguritePlating_Tile>();
            colorToTile[new Color(255, 0, 0)] = ModContent.TileType<FulguriteBrick_Tile>();
            colorToTile[new Color(0, 0, 255)] = ModContent.TileType<StormCloud_Tile>();
            colorToTile[new Color(255, 0, 255)] = ModContent.TileType<FulgurGlass_Tile>();
            colorToTile[new Color(150, 150, 150)] = -2; //turn into air
            colorToTile[Color.Black] = -1; //don't touch when genning		

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
            colorToWall[new Color(0, 255, 0)] = ModContent.WallType<FulguritePlating_Wall>();
            colorToWall[new Color(255, 0, 255)] = ModContent.WallType<FulgurGlass_Wall>();
            colorToWall[Color.Black] = -1; //don't touch when genning				

            TexGen gen = TexGen.GetTexGenerator(ParthenanTexGenAssets.ParthenanTileData, colorToTile, ParthenanTexGenAssets.ParthenanWallData, colorToWall);

            gen.Generate(placementPoint.X, placementPoint.Y, true, true);
            WorldGen.PlaceObject(placementPoint.X + 37, placementPoint.Y + 47, (ushort)ModContent.TileType<AncientDataBank_Tile>());
            WorldGen.PlaceChest(placementPoint.X + 32, placementPoint.Y + 47, (ushort)ModContent.TileType<StormChest_Tile>());
            WorldGen.PlaceChest(placementPoint.X + 41, placementPoint.Y + 47, (ushort)ModContent.TileType<StormChest_Tile>());
            return true;
        }
    }
}
