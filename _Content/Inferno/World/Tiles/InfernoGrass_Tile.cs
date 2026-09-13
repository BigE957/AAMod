using AAModClassic._Unreleased.Content.Inferno.World.Tiles;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Content.Inferno.World.Tiles
{
    public class InfernoGrass_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileBlendAll[Type] = true;

            Main.tileLighted[Type] = true;

            TileID.Sets.Conversion.Grass[Type] = true;
            TileID.Sets.Grass[Type] = true;
            TileID.Sets.CanBeDugByShovel[Type] = true;

            TileID.Sets.NeedsGrassFraming[Type] = true;
            Main.tileMergeDirt[Type] = true;

            DustType = ModContent.DustType<Dusts.RazeleafDust>();
            AddMapEntry(new Color(255, 153, 51));
            RegisterItemDrop(ItemID.DirtBlock);

            if (TileObjectData.GetTileData(TileID.Sunflower, 0) is TileObjectData data && data.AnchorValidTiles != null)
                data.AnchorValidTiles = [.. data.AnchorValidTiles, Type];
        }

        public override void RandomUpdate(int i, int j)
        {
            if (TileUtils.TrySpread(i, j, Type, 4, TileID.Dirt) && Main.netMode != NetmodeID.SinglePlayer)
                NetMessage.SendTileSquare(-1, i, j, 3, TileChangeType.None);

            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) && !Framing.GetTileSafely(i, j - 1).HasTile)
            {
                if (WorldGen.IsFitToPlaceFlowerIn(i, j, TileID.Plants))
                {
                    if (Main.tile[i, j - 1].WallType >= WallID.None && WallID.Sets.AllowsPlantsToGrow[Main.tile[i, j - 1].WallType] && Main.tile[i, j].WallType >= WallID.None && Main.tile[i, j].WallType < WallLoader.WallCount && WallID.Sets.AllowsPlantsToGrow[Main.tile[i, j].WallType])
                    {
                        if (WorldGen.genRand.NextBool(50) || WorldGen.genRand.NextBool(40)) 
                        {
                            int style = 23; // mushroom
                            if (WorldGen.PlaceObject(i, j - 1, ModContent.TileType<InfernoFoliage_Tile>(), false, style))
                                NetMessage.SendObjectPlacement(-1, i, j - 1, ModContent.TileType<InfernoFoliage_Tile>(), style, 0, -1, -1);
                        }
                        else if (Main.rand.NextBool(35) || (Main.tile[i, j].WallType >= WallID.GrassUnsafe && Main.tile[i, j].WallType <= WallID.HallowedGrassUnsafe))
                        {
                            int style = Main.rand.Next(16) + 5; // flowers
                            if (WorldGen.PlaceObject(i, j - 1, ModContent.TileType<InfernoFoliage_Tile>(), false, style))
                                NetMessage.SendObjectPlacement(-1, i, j - 1, ModContent.TileType<InfernoFoliage_Tile>(), style, 0, -1, -1);
                        }
                        else
                        {
                            int style = Main.rand.Next(6); // grass
                            if (WorldGen.PlaceObject(i, j - 1, ModContent.TileType<InfernoFoliage_Tile>(), false, style))
                                NetMessage.SendObjectPlacement(-1, i, j - 1, ModContent.TileType<InfernoFoliage_Tile>(), style, 0, -1, -1);
                        }
                    }
                }
            }
            else if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased) &&!Framing.GetTileSafely(i, j - 1).HasTile && Main.rand.NextBool(40))
            {
                int style = Main.rand.Next(23);
                if (WorldGen.PlaceObject(i, j - 1, ModContent.TileType<InfernoFoliage_Tile>(), false, style))
                    NetMessage.SendObjectPlacement(-1, i, j - 1, ModContent.TileType<InfernoFoliage_Tile>(), style, 0, -1, -1);
            }

            if (!Framing.GetTileSafely(i, j - 1).HasTile && Main.rand.NextBool(1500))
            {
                int style = 23; // mushroom
                if (WorldGen.PlaceObject(i, j - 1, ModContent.TileType<InfernoFoliage_Tile>(), false, style))
                    NetMessage.SendObjectPlacement(-1, i, j - 1, ModContent.TileType<InfernoFoliage_Tile>(), style, 0, -1, -1);

            }
        }

        public override void NumDust(int i, int j, bool fail, ref int num) => num = 3;
        
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (!effectOnly)
            {
                fail = true;
                WorldGen.KillTile_MakeTileDust(i, j, Main.tile[i, j]);
                Framing.GetTileSafely(i, j).TileType = TileID.Dirt;
            }
        }

        public override bool CanExplode(int i, int j)
        {
            WorldGen.KillTile(i, j, false, false, true);
            return true;
        }
    }
}