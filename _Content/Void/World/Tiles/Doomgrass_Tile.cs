using AAModClassic.UI.World;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.World.Tiles
{
    public class DoomGrass_Tile : ModTile
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

            DustType = ModContent.DustType<Dusts.DoomDust>();
            AddMapEntry(new Color(50, 50, 50));
            RegisterItemDrop(ItemID.DirtBlock);
        }

        public override void RandomUpdate(int i, int j)
        {
            if (TileUtils.TrySpread(i, j, Type, 4, TileID.Dirt) && Main.netMode != NetmodeID.SinglePlayer)
                NetMessage.SendTileSquare(-1, i, j, 3, TileChangeType.None);
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