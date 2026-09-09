using AAModClassic._Content.Inferno.World.Tiles.Trees;
using AAModClassic._Content.Mire.World.Tiles.Trees;
using AAModClassic._Content.Void.World.Tiles.Trees;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Globals
{
    public class FertilizerGlobalProjectile : GlobalProjectile
    {
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation) => entity.type == ProjectileID.Fertilizer;

        public override void AI(Projectile projectile)
        {
            Point start = projectile.TopLeft.ToTileCoordinates();
            Point end = projectile.BottomRight.ToTileCoordinates();

            for (int x = start.X; x <= end.X; x++)
            {
                for (int y = start.Y; y <= end.Y; y++)
                {
                    if (!WorldGen.InWorld(x, y))
                        continue;

                    var t = Main.tile[x, y];

                    if (t.TileType == ModContent.TileType<RazewoodSapling_Tile>() ||
                        t.TileType == ModContent.TileType<RazewoodPalmSapling_Tile>() ||
                        t.TileType == ModContent.TileType<BogwoodSapling_Tile>() ||
                        t.TileType == ModContent.TileType<BogwoodPalmTreeSapling_Tile>() ||
                        t.TileType == ModContent.TileType<OuroborosSapling_Tile>())
                    {
                        if(WorldGen.GrowTree(x, y) && WorldGen.PlayerLOS(x, y))
                            WorldGen.TreeGrowFXCheck(x, y);
                    }
                }
            }
        }
    }
}
