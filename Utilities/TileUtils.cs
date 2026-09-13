using AAModClassic._Content.RedMushroom.World.Tiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.ID;

namespace AAModClassic.Utilities
{
    public static class TileUtils
    {
        public static bool TrySpread(int x, int y, int type, int chance, params int[] validAdjacentTypes)
        {
            if (Main.rand.NextBool(chance))
            {
                var adjacents = OpenAdjacents(x, y, true, validAdjacentTypes);

                if (adjacents.Count == 0)
                    return false;

                Point p = adjacents[Main.rand.Next(adjacents.Count)];
                Framing.GetTileSafely(p.X, p.Y).TileType = (ushort)type;

                if (Main.netMode == NetmodeID.Server)
                    NetMessage.SendTileSquare(-1, p.X, p.Y);

                return true;
            }

            return false;
        }

        public static List<Point> OpenAdjacents(int x, int y, bool requiresAir, params int[] types)
        {
            List<Point> list = [];

            for (int xOff = -1; xOff <= 1; ++xOff)
                for (int yOff = -1; yOff <= 1; ++yOff)
                {
                    if (yOff == 0 && xOff == 0)
                        continue;

                    Point p = new(x + xOff, y + yOff);
                    Tile t = Framing.GetTileSafely(p);

                    if (!t.HasTile || !types.Contains(t.TileType))
                        continue;

                    if (!requiresAir || WorldGen.TileIsExposedToAir(p.X, p.Y))
                        list.Add(new Point(p.X, p.Y));
                }

            return list;
        }

        public static bool Active(this Tile tile, int type) => tile.HasTile && tile.TileType == type;

        public static void Merge(int thisType, params int[] mergeTypes)
        {
            foreach (int type in mergeTypes)
            {
                Main.tileMerge[thisType][type] = true;
                Main.tileMerge[type][thisType] = true;
            }
        }
    }
}
