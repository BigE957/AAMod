using AAModClassic._Content.Desert._PostMoonlord.Items.Materials;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Quest
{
    public class WormIdol : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Worm Idol");
            /* Tooltip.SetDefault(@"An ancient statue depicting some form of worm god.
It looks like it hasn't been touched in years"); */
        }
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Purple;
        }

        public override void HoldItem(Player player)
        {
            player.GetModPlayer<IdolPointer>().effect = true;

            if (player.whoAmI == Main.myPlayer)
            {
                if (player.ownedProjectileCounts[ModContent.ProjectileType<WormIdol_Pointer>()] < 1)
                {
                    Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<WormIdol_Pointer>(), 0, 0f, Main.myPlayer, 0f, 0f);
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.MechanicalWorm, 1);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddIngredient(ItemID.FragmentSolar, 5);
            recipe.AddIngredient(ItemID.FragmentStardust, 5);
            recipe.AddIngredient(ModContent.ItemType<SoulFragment>(), 5);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }

    public class IdolPointer : ModPlayer
    {
        public bool effect;

        public override void ResetEffects()
        {
            effect = false;
        }
    }
}