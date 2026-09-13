using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Mire.__Hardmode.Items.Weapons;
using AAModClassic._Content.Mire._PostMoonlord.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.Weapons
{
    public class AbyssalEruption : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Ranged";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Abyssal Eruption");
			// Tooltip.SetDefault(@"spews out abyssal acid that will cause enamys to explode of killed by its visious acid, also uses gel");
		}

	    public override void SetDefaults()
	    {
            Item.damage = 350;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 76;
            Item.height = 34;
            Item.useTime = 2;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true; //so the item's animation doesn't do damage
            Item.knockBack = 3.25f;
            Item.UseSound = SoundID.Item34;
            Item.value = 1000000;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<AbyssalEruption_AcidFlame>(); //idk why but all the guns in the vanilla source have this
            Item.shootSpeed = 20f;
            Item.useAmmo = 23;
            Item.rare = ModContent.RarityType<AncientsRarity>();
            Item.consumeAmmoOnFirstShotOnly = true;
        }

        

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-3, 0);
        }

        int shoot = 0;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < 3; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(10));
                Projectile.NewProjectile(player.GetSource_ItemUse(Item), position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<AbyssalEruption_AcidFlame>(), damage, knockback, player.whoAmI);
            }
            shoot++;

            if (shoot % 6 != 0) return false;

            if (shoot >= 6)
            {
                Projectile.NewProjectile(player.GetSource_ItemUse(Item), position, velocity, ModContent.ProjectileType<AbyssalEruption_AcidFlame>(), damage * 2, knockback, player.whoAmI);
                shoot = 0;
            }
            shoot = 0;
            return false;
        }

	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ModContent.ItemType<EventideAbyssiumBar>(), 5);
	        recipe.AddIngredient(ModContent.ItemType<DreadScale>(), 5);
            recipe.AddIngredient(ModContent.ItemType<Toxithrower>());
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
	    }
	}
}
