using AAModClassic.UI.World;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Content.Mire.World.Tiles
{
    public class MireGrass_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileBlendAll[Type] = true;

            TileID.Sets.Conversion.Grass[Type] = true;
            TileID.Sets.Grass[Type] = true;
            TileID.Sets.CanBeDugByShovel[Type] = true;
            TileID.Sets.JungleSpecial[Type] = true;

            Main.tileMergeDirt[Type] = true;        
            TileID.Sets.NeedsGrassFraming[Type] = true;
            
            DustType = ModContent.DustType<Dusts.AbyssiumDust>();
            AddMapEntry(new Color(0, 50, 140));
            RegisterItemDrop(ItemID.MudBlock);

            if (TileObjectData.GetTileData(TileID.Sunflower, 0) is TileObjectData data && data.AnchorValidTiles != null)
                data.AnchorValidTiles = [.. data.AnchorValidTiles, Type];
        }

        public override bool IsTileBiomeSightable(int i, int j, ref Color sightColor)
        {
            sightColor = Color.BlueViolet;
            return true;
        }

        public override void RandomUpdate(int i, int j)
        {
            if (TileUtils.TrySpread(i, j, Type, 4, TileID.Mud) && Main.netMode != NetmodeID.SinglePlayer)
                NetMessage.SendTileSquare(-1, i, j, 3, TileChangeType.None);
        }

        public override void NumDust(int i, int j, bool fail, ref int num) => num = 3;

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (!effectOnly)
            {
                fail = true;
                WorldGen.KillTile_MakeTileDust(i, j, Main.tile[i, j]);
                Framing.GetTileSafely(i, j).TileType = TileID.Mud;
            }
        }

        public override bool CanExplode(int i, int j)
        {
            WorldGen.KillTile(i, j, false, false, true);
            return true;
        }
    }
}