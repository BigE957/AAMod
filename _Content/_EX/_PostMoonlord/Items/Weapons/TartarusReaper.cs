using AAModClassic._Content._Dev._PostMoonlord.Items.Weapons;
using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons
{
    public class TartarusReaper : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Melee";
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Tartarus Reaper");
            /* Tooltip.SetDefault(@"Spins a cursed scythe around you that shreds through enemies
Right click to swing the scythe
Cursed Sickle EX"); */			
		}

		public override void SetDefaults()
		{
            Item.width = 40;
            Item.height = 40;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.UseSound = SoundID.Item71;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.damage = 280;
            Item.knockBack = 4;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<TartarusReaper_Holdout>();
            Item.shootSpeed = 0.1f;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.expert = true;
		}

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {

            if (player.altFunctionUse == 2)
            {
                Item.noMelee = false;
                Item.noUseGraphic = false;
                Item.shoot = ModContent.ProjectileType<TartarusReaper_Proj>();
                Item.shootSpeed = 7f;
            }
            else
            {
                Item.noMelee = true;
                Item.noUseGraphic = true;
                Item.shoot = ModContent.ProjectileType<TartarusReaper_Holdout>();
                Item.shootSpeed = 0.1f;
            }
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                return true;
            }
            for (int k = 0; k < 2; k++)
			{
				Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<CursedSickleEffect>(), damage, knockback, player.whoAmI, k, 0f);
			}
			return true;
		}

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ModContent.ItemType<CursedSickle>());
                recipe.AddIngredient(ModContent.ItemType<EXSoul>());
                recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
                recipe.Register();
            }
        }
    }
}