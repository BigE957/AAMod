using AAModClassic._Content.Mire.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic.Gores.Leaf;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.World.Tiles.Trees
{
    public class BogwoodPalmTree_Tile : ModPalmTree
    {
        public override TreePaintingSettings TreeShaderSettings => new()
        {
            UseSpecialGroups = true,
            SpecialGroupMinimalHueValue = 11f / 72f,
            SpecialGroupMaximumHueValue = 0.25f,
            SpecialGroupMinimumSaturationValue = 0.88f,
            SpecialGroupMaximumSaturationValue = 1f
        };
        public override void SetStaticDefaults()
        {
            GrowsOnTileId = [ModContent.TileType<Depthsand_Tile>()];
        }

        public override int DropWood() => ModContent.ItemType<Bogwood>();

        public override Asset<Texture2D> GetTexture() => ModContent.Request<Texture2D>("AAModClassic/_Content/Mire/World/Tiles/Trees/BogwoodPalmTree_Tile");

        public override Asset<Texture2D> GetTopTextures() => ModContent.Request<Texture2D>("AAModClassic/_Content/Mire/World/Tiles/Trees/BogwoodPalmTree_Tile_Tops");

        public override Asset<Texture2D> GetOasisTopTextures() => ModContent.Request<Texture2D>("AAModClassic/_Content/Mire/World/Tiles/Trees/BogwoodPalmTree_Tile_Top_Oasis");

        public override int SaplingGrowthType(ref int style)/* tModPorter Note: _Unreleased. Use ModTree.SaplingGrowthType */
        {
            style = 0;
            return ModContent.TileType<BogwoodPalmTreeSapling_Tile>();
        }

        public override int TreeLeaf() => ModContent.GoreType<BogwoodPalmLeafGore>();
    }
}
