using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Content.RedMushroom.World.Tiles
{
    public class Mushroom_Tile : ModTile
	{
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileCut[Type] = true;
            Main.tileLavaDeath[Type] = true;

            Main.tileMergeDirt[Type] = true;
            TileID.Sets.SwaysInWindBasic[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.RandomStyleRange = 5;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.addTile(Type);
            RegisterItemDrop(ItemID.Mushroom);
            DustType = ModContent.DustType<Dusts.MushDust>();
            HitSound = SoundID.Grass;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = 10;
            
        }

        public override void RandomUpdate(int i, int j)
        {
            if (WorldGen.genRand.NextBool(20))
            {
                bool isPlayerNear = WorldGen.PlayerLOS(i, j);
                bool success = GrowMushroomTree(i, j);
                if (success && isPlayerNear)
                {
                    WorldGen.TreeGrowFXCheck(i, j);
                }
            }
        }

        public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
        {
            if (i % 2 == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
        }

        public static bool GrowMushroomTree(int i, int y)
        {
            int j;
            int type = ModContent.TileType<Mushroom_Tile>();
            for (j = y; Main.tile[i, j].TileType == type; j++)
            {
            }

            if ((Main.tile[i - 1, j - 1].LiquidAmount != 0 || Main.tile[i, j - 1].LiquidAmount != 0 || Main.tile[i + 1, j - 1].LiquidAmount != 0) && !WorldGen.notTheBees)
                return false;

            if (!Main.tile[i, j].IsActuated && !Main.tile[i, j].IsHalfBlock && Main.tile[i, j].Slope == 0 && WorldGen.IsTileTypeFitForTree(Main.tile[i, j].TileType) && ((Main.remixWorld && (double)j > Main.worldSurface) || Main.tile[i, j - 1].WallType == WallID.None || WorldGen.DefaultTreeWallTest(Main.tile[i, j - 1].WallType)) && ((Main.tile[i - 1, j].HasTile && WorldGen.IsTileTypeFitForTree(Main.tile[i - 1, j].TileType)) || (Main.tile[i + 1, j].HasTile && WorldGen.IsTileTypeFitForTree(Main.tile[i + 1, j].TileType))))
            {
                TileColorCache cache = Main.tile[i, j].BlockColorAndCoating();
                if (Main.tenthAnniversaryWorld && !WorldGen.gen)
                    cache.Color = (byte)WorldGen.genRand.Next(1, 13);

                int num = 2;
                int num2 = WorldGen.genRand.Next(5, 17);
                int num3 = num2 + 4;
                if (Main.tile[i, j].TileType == TileID.JungleGrass)
                    num3 += 5;

                for (int cx = i - num; cx <= i + num; cx++)
                {
                    for (int cy = j - num3; cy <= j; cy++)
                    {
                        if (Main.tile[cx, cy].HasTile && Main.tile[cx, cy].TileType == TileID.Trees)
                            return false;
                    }
                }

                bool flag = false;
                if (Main.tile[i, j].TileType == type && WorldGen.EmptyTileCheck(i - num, i + num, j - num3, j - 3, type) && WorldGen.EmptyTileCheck(i - 1, i + 1, j - 2, j - 1, type))
                    flag = true;

                if (WorldGen.EmptyTileCheck(i - num, i + num, j - num3, j - 1, type))
                    flag = true;

                if (flag)
                {
                    bool flag2 = Main.remixWorld && (double)j < Main.worldSurface;
                    bool flag3 = false;
                    bool flag4 = false;
                    int num4;
                    for (int k = j - num2; k < j; k++)
                    {
                        Main.tile[i, k].ResetToType(TileID.Trees);
                        Main.tile[i, k].Get<TileWallWireStateData>().TileFrameNumber = ((byte)WorldGen.genRand.Next(3));
                        Main.tile[i, k].UseBlockColors(cache);
                        num4 = WorldGen.genRand.Next(3);
                        int num5 = WorldGen.genRand.Next(10);
                        if (k == j - 1 || k == j - num2)
                            num5 = 0;

                        while (((num5 == 5 || num5 == 7) && flag3) || ((num5 == 6 || num5 == 7) && flag4))
                        {
                            num5 = WorldGen.genRand.Next(10);
                        }

                        flag3 = false;
                        flag4 = false;
                        if (num5 == 5 || num5 == 7)
                            flag3 = true;

                        if (num5 == 6 || num5 == 7)
                            flag4 = true;

                        switch (num5)
                        {
                            case 1:
                                if (num4 == 0)
                                {
                                    Main.tile[i, k].TileFrameX = 0;
                                    Main.tile[i, k].TileFrameY = 66;
                                }
                                if (num4 == 1)
                                {
                                    Main.tile[i, k].TileFrameX = 0;
                                    Main.tile[i, k].TileFrameY = 88;
                                }
                                if (num4 == 2)
                                {
                                    Main.tile[i, k].TileFrameX = 0;
                                    Main.tile[i, k].TileFrameY = 110;
                                }
                                break;
                            case 2:
                                if (num4 == 0)
                                {
                                    Main.tile[i, k].TileFrameX = 22;
                                    Main.tile[i, k].TileFrameY = 0;
                                }
                                if (num4 == 1)
                                {
                                    Main.tile[i, k].TileFrameX = 22;
                                    Main.tile[i, k].TileFrameY = 22;
                                }
                                if (num4 == 2)
                                {
                                    Main.tile[i, k].TileFrameX = 22;
                                    Main.tile[i, k].TileFrameY = 44;
                                }
                                break;
                            case 3:
                                if (num4 == 0)
                                {
                                    Main.tile[i, k].TileFrameX = 44;
                                    Main.tile[i, k].TileFrameY = 66;
                                }
                                if (num4 == 1)
                                {
                                    Main.tile[i, k].TileFrameX = 44;
                                    Main.tile[i, k].TileFrameY = 88;
                                }
                                if (num4 == 2)
                                {
                                    Main.tile[i, k].TileFrameX = 44;
                                    Main.tile[i, k].TileFrameY = 110;
                                }
                                break;
                            case 4:
                                if (num4 == 0)
                                {
                                    Main.tile[i, k].TileFrameX = 22;
                                    Main.tile[i, k].TileFrameY = 66;
                                }
                                if (num4 == 1)
                                {
                                    Main.tile[i, k].TileFrameX = 22;
                                    Main.tile[i, k].TileFrameY = 88;
                                }
                                if (num4 == 2)
                                {
                                    Main.tile[i, k].TileFrameX = 22;
                                    Main.tile[i, k].TileFrameY = 110;
                                }
                                break;
                            case 5:
                                if (num4 == 0)
                                {
                                    Main.tile[i, k].TileFrameX = 88;
                                    Main.tile[i, k].TileFrameY = 0;
                                }
                                if (num4 == 1)
                                {
                                    Main.tile[i, k].TileFrameX = 88;
                                    Main.tile[i, k].TileFrameY = 22;
                                }
                                if (num4 == 2)
                                {
                                    Main.tile[i, k].TileFrameX = 88;
                                    Main.tile[i, k].TileFrameY = 44;
                                }
                                break;
                            case 6:
                                if (num4 == 0)
                                {
                                    Main.tile[i, k].TileFrameX = 66;
                                    Main.tile[i, k].TileFrameY = 66;
                                }
                                if (num4 == 1)
                                {
                                    Main.tile[i, k].TileFrameX = 66;
                                    Main.tile[i, k].TileFrameY = 88;
                                }
                                if (num4 == 2)
                                {
                                    Main.tile[i, k].TileFrameX = 66;
                                    Main.tile[i, k].TileFrameY = 110;
                                }
                                break;
                            case 7:
                                if (num4 == 0)
                                {
                                    Main.tile[i, k].TileFrameX = 110;
                                    Main.tile[i, k].TileFrameY = 66;
                                }
                                if (num4 == 1)
                                {
                                    Main.tile[i, k].TileFrameX = 110;
                                    Main.tile[i, k].TileFrameY = 88;
                                }
                                if (num4 == 2)
                                {
                                    Main.tile[i, k].TileFrameX = 110;
                                    Main.tile[i, k].TileFrameY = 110;
                                }
                                break;
                            default:
                                if (num4 == 0)
                                {
                                    Main.tile[i, k].TileFrameX = 0;
                                    Main.tile[i, k].TileFrameY = 0;
                                }
                                if (num4 == 1)
                                {
                                    Main.tile[i, k].TileFrameX = 0;
                                    Main.tile[i, k].TileFrameY = 22;
                                }
                                if (num4 == 2)
                                {
                                    Main.tile[i, k].TileFrameX = 0;
                                    Main.tile[i, k].TileFrameY = 44;
                                }
                                break;
                        }

                        if (num5 == 5 || num5 == 7)
                        {
                            Main.tile[i - 1, k].ResetToType(TileID.Trees);
                            Main.tile[i - 1, k].UseBlockColors(cache);
                            num4 = WorldGen.genRand.Next(3);
                            if (WorldGen.genRand.Next(3) < 2 && !flag2)
                            {
                                if (num4 == 0)
                                {
                                    Main.tile[i - 1, k].TileFrameX = 44;
                                    Main.tile[i - 1, k].TileFrameY = 198;
                                }

                                if (num4 == 1)
                                {
                                    Main.tile[i - 1, k].TileFrameX = 44;
                                    Main.tile[i - 1, k].TileFrameY = 220;
                                }

                                if (num4 == 2)
                                {
                                    Main.tile[i - 1, k].TileFrameX = 44;
                                    Main.tile[i - 1, k].TileFrameY = 242;
                                }
                            }
                            else
                            {
                                if (num4 == 0)
                                {
                                    Main.tile[i - 1, k].TileFrameX = 66;
                                    Main.tile[i - 1, k].TileFrameY = 0;
                                }

                                if (num4 == 1)
                                {
                                    Main.tile[i - 1, k].TileFrameX = 66;
                                    Main.tile[i - 1, k].TileFrameY = 22;
                                }

                                if (num4 == 2)
                                {
                                    Main.tile[i - 1, k].TileFrameX = 66;
                                    Main.tile[i - 1, k].TileFrameY = 44;
                                }
                            }
                        }

                        if (num5 != 6 && num5 != 7)
                            continue;

                        Main.tile[i + 1, k].ResetToType(TileID.Trees);
                        Main.tile[i + 1, k].UseBlockColors(cache);
                        num4 = WorldGen.genRand.Next(3);
                        if (WorldGen.genRand.Next(3) < 2 && !flag2)
                        {
                            if (num4 == 0)
                            {
                                Main.tile[i + 1, k].TileFrameX = 66;
                                Main.tile[i + 1, k].TileFrameY = 198;
                            }

                            if (num4 == 1)
                            {
                                Main.tile[i + 1, k].TileFrameX = 66;
                                Main.tile[i + 1, k].TileFrameY = 220;
                            }

                            if (num4 == 2)
                            {
                                Main.tile[i + 1, k].TileFrameX = 66;
                                Main.tile[i + 1, k].TileFrameY = 242;
                            }
                        }
                        else
                        {
                            if (num4 == 0)
                            {
                                Main.tile[i + 1, k].TileFrameX = 88;
                                Main.tile[i + 1, k].TileFrameY = 66;
                            }

                            if (num4 == 1)
                            {
                                Main.tile[i + 1, k].TileFrameX = 88;
                                Main.tile[i + 1, k].TileFrameY = 88;
                            }

                            if (num4 == 2)
                            {
                                Main.tile[i + 1, k].TileFrameX = 88;
                                Main.tile[i + 1, k].TileFrameY = 110;
                            }
                        }
                    }
                    int num6 = WorldGen.genRand.Next(3);
                    bool flag5 = false;
                    bool flag6 = false;
                    if (Main.tile[i - 1, j].IsActuated && !Main.tile[i - 1, j].IsHalfBlock && Main.tile[i - 1, j].Slope == 0 && WorldGen.IsTileTypeFitForTree(Main.tile[i - 1, j].TileType))
                        flag5 = true;

                    if (Main.tile[i + 1, j].IsActuated && !Main.tile[i + 1, j].IsHalfBlock && Main.tile[i + 1, j].Slope == 0 && WorldGen.IsTileTypeFitForTree(Main.tile[i + 1, j].TileType))
                        flag6 = true;

                    if (!flag5)
                    {
                        if (num6 == 0)
                            num6 = 2;

                        if (num6 == 1)
                            num6 = 3;
                    }

                    if (!flag6)
                    {
                        if (num6 == 0)
                            num6 = 1;

                        if (num6 == 2)
                            num6 = 3;
                    }

                    if (flag5 && !flag6)
                        num6 = 2;

                    if (flag6 && !flag5)
                        num6 = 1;

                    if (num6 == 0 || num6 == 1)
                    {
                        Main.tile[i + 1, j - 1].ResetToType(TileID.Trees);
                        Main.tile[i + 1, j - 1].UseBlockColors(cache);
                        num4 = WorldGen.genRand.Next(3);
                        if (num4 == 0)
                        {
                            Main.tile[i + 1, j - 1].TileFrameX = 22;
                            Main.tile[i + 1, j - 1].TileFrameY = 132;
                        }

                        if (num4 == 1)
                        {
                            Main.tile[i + 1, j - 1].TileFrameX = 22;
                            Main.tile[i + 1, j - 1].TileFrameY = 154;
                        }

                        if (num4 == 2)
                        {
                            Main.tile[i + 1, j - 1].TileFrameX = 22;
                            Main.tile[i + 1, j - 1].TileFrameY = 176;
                        }
                    }

                    if (num6 == 0 || num6 == 2)
                    {
                        Main.tile[i - 1, j - 1].ResetToType(TileID.Trees);
                        Main.tile[i - 1, j - 1].UseBlockColors(cache);
                        num4 = WorldGen.genRand.Next(3);
                        if (num4 == 0)
                        {
                            Main.tile[i - 1, j - 1].TileFrameX = 44;
                            Main.tile[i - 1, j - 1].TileFrameY = 132;
                        }

                        if (num4 == 1)
                        {
                            Main.tile[i - 1, j - 1].TileFrameX = 44;
                            Main.tile[i - 1, j - 1].TileFrameY = 154;
                        }

                        if (num4 == 2)
                        {
                            Main.tile[i - 1, j - 1].TileFrameX = 44;
                            Main.tile[i - 1, j - 1].TileFrameY = 176;
                        }
                    }

                    Main.tile[i, j - 1].ResetToType(TileID.Trees);
                    Main.tile[i, j - 1].UseBlockColors(cache);
                    num4 = WorldGen.genRand.Next(3);
                    switch (num6)
                    {
                        case 0:
                            if (num4 == 0)
                            {
                                Main.tile[i, j - 1].TileFrameX = 88;
                                Main.tile[i, j - 1].TileFrameY = 132;
                            }
                            if (num4 == 1)
                            {
                                Main.tile[i, j - 1].TileFrameX = 88;
                                Main.tile[i, j - 1].TileFrameY = 154;
                            }
                            if (num4 == 2)
                            {
                                Main.tile[i, j - 1].TileFrameX = 88;
                                Main.tile[i, j - 1].TileFrameY = 176;
                            }
                            break;
                        case 1:
                            if (num4 == 0)
                            {
                                Main.tile[i, j - 1].TileFrameX = 0;
                                Main.tile[i, j - 1].TileFrameY = 132;
                            }
                            if (num4 == 1)
                            {
                                Main.tile[i, j - 1].TileFrameX = 0;
                                Main.tile[i, j - 1].TileFrameY = 154;
                            }
                            if (num4 == 2)
                            {
                                Main.tile[i, j - 1].TileFrameX = 0;
                                Main.tile[i, j - 1].TileFrameY = 176;
                            }
                            break;
                        case 2:
                            if (num4 == 0)
                            {
                                Main.tile[i, j - 1].TileFrameX = 66;
                                Main.tile[i, j - 1].TileFrameY = 132;
                            }
                            if (num4 == 1)
                            {
                                Main.tile[i, j - 1].TileFrameX = 66;
                                Main.tile[i, j - 1].TileFrameY = 154;
                            }
                            if (num4 == 2)
                            {
                                Main.tile[i, j - 1].TileFrameX = 66;
                                Main.tile[i, j - 1].TileFrameY = 176;
                            }
                            break;
                    }

                    if (WorldGen.genRand.Next(13) != 0 && !flag2)
                    {
                        num4 = WorldGen.genRand.Next(3);
                        if (num4 == 0)
                        {
                            Main.tile[i, j - num2].TileFrameX = 22;
                            Main.tile[i, j - num2].TileFrameY = 198;
                        }

                        if (num4 == 1)
                        {
                            Main.tile[i, j - num2].TileFrameX = 22;
                            Main.tile[i, j - num2].TileFrameY = 220;
                        }

                        if (num4 == 2)
                        {
                            Main.tile[i, j - num2].TileFrameX = 22;
                            Main.tile[i, j - num2].TileFrameY = 242;
                        }
                    }
                    else
                    {
                        num4 = WorldGen.genRand.Next(3);
                        if (num4 == 0)
                        {
                            Main.tile[i, j - num2].TileFrameX = 0;
                            Main.tile[i, j - num2].TileFrameY = 198;
                        }

                        if (num4 == 1)
                        {
                            Main.tile[i, j - num2].TileFrameX = 0;
                            Main.tile[i, j - num2].TileFrameY = 220;
                        }

                        if (num4 == 2)
                        {
                            Main.tile[i, j - num2].TileFrameX = 0;
                            Main.tile[i, j - num2].TileFrameY = 242;
                        }
                    }

                    WorldGen.RangeFrame(i - 2, j - num2 - 1, i + 2, j + 1);
                    if (Main.netMode == NetmodeID.Server)
                        NetMessage.SendTileSquare(-1, i - 1, j - num2, 3, num2);

                    return true;
                }
            }

            return false;
        }
    }
}