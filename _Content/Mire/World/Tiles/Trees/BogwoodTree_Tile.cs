using AAModClassic._Content.Mire.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic._Unreleased.Content.Mire.___PreHardmode;
using AAModClassic.Gores.Leaf;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.World.Tiles.Trees
{
    public class BogwoodTree_Tile : ModTree
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
            GrowsOnTileId = [ModContent.TileType<AbyssGrass_Tile>(), ModContent.TileType<MireGrass_Tile>()];
        }

        public override int DropWood()
        {
            return ModContent.ItemType<Bogwood>();
        }

        public override Asset<Texture2D> GetTexture()
        {
            return ModContent.Request<Texture2D>("AAModClassic/_Content/Mire/World/Tiles/Trees/BogwoodTree_Tile");
        }

        public override Asset<Texture2D> GetBranchTextures()
        {
            return ModContent.Request<Texture2D>("AAModClassic/_Content/Mire/World/Tiles/Trees/BogwoodTree_Tile_Branches");
        }

        public override Asset<Texture2D> GetTopTextures()
        {
            return ModContent.Request<Texture2D>("AAModClassic/_Content/Mire/World/Tiles/Trees/BogwoodTree_Tile_Top");
        }

        public override int SaplingGrowthType(ref int style)/* tModPorter Note: _Unreleased. Use ModTree.SaplingGrowthType */
        {
            style = 0;
            return ModContent.TileType<BogwoodSapling_Tile>();
        }

        public override int TreeLeaf() => ModContent.GoreType<BogwoodLeafGore>();

        public override bool Shake(int x, int y, ref bool createLeaves)
        {
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                if (Main.rand.NextBool(300))
                {
                    Vector2 offset = this.GetRandomTreePosition(x, y);
                    Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), new Vector2(x, y) * 16 + offset, ModContent.ItemType<LivingBogwoodWand>(), 1);
                }
                else if (Main.rand.NextBool(300))
                {
                    Vector2 offset = this.GetRandomTreePosition(x, y);
                    Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), new Vector2(x, y) * 16 + offset, ModContent.ItemType<LivingBogleafWand>(), 1);
                }
            }
            return true;
        }
    }
}
