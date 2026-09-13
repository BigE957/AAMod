using AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Accessories;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Materials;
using AAModClassic._Content.Mire._PostMoonlord.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.Chaos._PostMoonlord.Items.Accessories
{
    public class HeartOfAnarchy : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Heart of Anarchy");
            /* Tooltip.SetDefault(@"'The Sisters' hearts beat as one in this antique china'"); */
        }
        public override void SetDefaults()
        {
            Item.width = 66;
            Item.height = 78;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = ItemRarityID.Purple;
            Item.accessory = true;
            Item.expert = true;
            Item.defense = 6;
        }

        public override void RegisterEquipEffects()
        {
            AddEffect<HeartOfAnarchyDamageBoostEffect>();
            AddEffect<HeartOfAnarchyBullshitEffect>();
            AddEffect<HeartOfAnarchyDebuffEffect>();
        }

        // if u wnanna stack effects go the fuck ahead man
        /*
        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if (equippedItem.type == ModContent.ItemType<HeartOfSorrow>())
                return false;

            return true;
        }
        */

        //TODO: make this crafted only in unofficial
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<HeartOfPassion>());
            recipe.AddIngredient(ModContent.ItemType<HeartOfSorrow>());
            recipe.AddIngredient(ModContent.ItemType<CrucibleScale>(), 15);
            recipe.AddIngredient(ModContent.ItemType<DreadScale>(), 15);
            recipe.AddIngredient(ModContent.ItemType<SearingSpark>(), 6);
            recipe.AddIngredient(ModContent.ItemType<TerrorSoul>(), 6);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
        }
    }
}