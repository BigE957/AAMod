using AAModClassic._Content.RedMushroom.___PreHardmode.Items.Quest;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.RedMushroom.World.Tiles
{
    public class Mycelium_Tile : ModTile
	{
		public override void SetStaticDefaults()
		{
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileBlendAll[Type] = true;

            //TileID.Sets.Conversion.Grass[Type] = true;
            TileID.Sets.Grass[Type] = true;
            TileID.Sets.CanBeDugByShovel[Type] = true;

            TileID.Sets.NeedsGrassFramingDirt[Type] = TileID.Dirt;
            TileID.Sets.NeedsGrassFraming[Type] = true;
            Main.tileMergeDirt[Type] = true;

            DustType = ModContent.DustType<Dusts.MushDust>();
			AddMapEntry(new Color(100, 100, 0));
            RegisterItemDrop(ItemID.DirtBlock);
		}

        public override void RandomUpdate(int i, int j)
        {
            if (TileUtils.TrySpread(i, j, Type, 4, TileID.Dirt) && Main.netMode != NetmodeID.SinglePlayer)
                NetMessage.SendTileSquare(-1, i, j, 3, TileChangeType.None);

            Tile tileAbove = Framing.GetTileSafely(i, j - 1);

            if (!tileAbove.HasTile && Main.rand.NextBool(30))
            {
                int style = Main.rand.Next(5);
                if (!WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased))
                    style = 0;

                if (PlaceObject(i, j - 1, ModContent.TileType<Mushroom_Tile>(), false, style))
                    NetMessage.SendObjectPlacement(-1, i, j - 1, ModContent.TileType<Mushroom_Tile>(), style, 0, -1, -1);
            }

            if (!tileAbove.HasTile && Main.rand.NextBool(1000))
            {
                int style = Main.rand.Next(5);
                if (PlaceObject(i, j - 1, ModContent.TileType<MadnessMushroom_Tile>(), false, style))
                    NetMessage.SendObjectPlacement(-1, i, j - 1, ModContent.TileType<MadnessMushroom_Tile>(), style, 0, -1, -1);
            }
        }

        public static bool PlaceObject(int x, int y, int type, bool mute = false, int style = 0, int random = -1, int direction = -1)
        {
            if (!TileObject.CanPlace(x, y, type, style, direction, out TileObject toBePlaced, false))
            {
                return false;
            }
            toBePlaced.random = random;
            if (TileObject.Place(toBePlaced) && !mute)
            {
                WorldGen.SquareTileFrame(x, y, true);
            }
            return false;
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