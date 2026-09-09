using AAModClassic._Content.Inferno.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic.Gores.Leaf;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.World.Tiles.Trees
{
    public class RazewoodPalmTree_Tile : ModPalmTree
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
            GrowsOnTileId = [ModContent.TileType<Torchsand_Tile>()];
        }

        public override int DropWood() => ModContent.ItemType<Razewood>();

        public override Asset<Texture2D> GetTexture() => ModContent.Request<Texture2D>("AAModClassic/_Content/Inferno/World/Tiles/Trees/RazewoodPalmTree_Tile");

        public override Asset<Texture2D> GetTopTextures() => ModContent.Request<Texture2D>("AAModClassic/_Content/Inferno/World/Tiles/Trees/RazewoodPalmTree_Tile_Top");

        public override Asset<Texture2D> GetOasisTopTextures() => ModContent.Request<Texture2D>("AAModClassic/_Content/Inferno/World/Tiles/Trees/RazewoodPalmTree_Tile_Top_Oasis");

        public override int SaplingGrowthType(ref int style)
        {
            style = 0;
            return ModContent.TileType<RazewoodPalmSapling_Tile>();
        }

        public override int TreeLeaf() => ModContent.GoreType<RazewoodPalmLeafGore>();
    }
}
