using AAModClassic._Content.Inferno.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic._Unreleased.Content.Inferno.___PreHardmode.Items;
using AAModClassic.Gores.Leaf;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;


namespace AAModClassic._Content.Inferno.World.Tiles.Trees
{
    public class RazewoodTree_Tile : ModTree
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
            GrowsOnTileId = [ModContent.TileType<InfernoGrass_Tile>(), ModContent.TileType<TorchAsh_Tile>(), ModContent.TileType<TorchMoss_Tile>()];
        }

        public override int DropWood()
        {
            return ModContent.ItemType<Razewood>();
        }

        public override Asset<Texture2D> GetTexture()
        {
            return ModContent.Request<Texture2D>("AAModClassic/_Content/Inferno/World/Tiles/Trees/RazewoodTree_Tile");
        }

        public override Asset<Texture2D> GetBranchTextures()
        {
            return ModContent.Request<Texture2D>("AAModClassic/_Content/Inferno/World/Tiles/Trees/RazewoodTree_Tile_Branches");
        }

        public override Asset<Texture2D> GetTopTextures()
        {
            return ModContent.Request<Texture2D>("AAModClassic/_Content/Inferno/World/Tiles/Trees/RazewoodTree_Tile_Top");
        }

        public override int SaplingGrowthType(ref int style)/* tModPorter Note: _Unreleased. Use ModTree.SaplingGrowthType */
        {
            style = 0;
            return ModContent.TileType<RazewoodSapling_Tile>();
        }

        public override int TreeLeaf() => ModContent.GoreType<RazewoodLeafGore>();

        public override bool Shake(int x, int y, ref bool createLeaves)
        {
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                if (Main.rand.NextBool(300))
                {
                    Vector2 offset = this.GetRandomTreePosition(x, y);
                    Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), new Vector2(x, y) * 16 + offset, ModContent.ItemType<LivingRazewoodWand>(), 1);
                }
                else if (Main.rand.NextBool(300))
                {
                    Vector2 offset = this.GetRandomTreePosition(x, y);
                    Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), new Vector2(x, y) * 16 + offset, ModContent.ItemType<LivingRazeleafWand>(), 1);
                }
            }
            return true;
        }
    }
}
