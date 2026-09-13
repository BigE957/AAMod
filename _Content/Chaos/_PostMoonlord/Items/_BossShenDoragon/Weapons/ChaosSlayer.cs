using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using AAModClassic.Globals;
using AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.Weapons;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Materials;
using AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Weapons;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Rarities;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossShenDoragon.Weapons
{
    public class ChaosSlayer : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Melee";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Chaos Slayer");
            /* Tooltip.SetDefault(@"Unleashes blades of chaos to smite your foes
blades go through tiles
'Shatter all sanity'"); */
        }

        public override void SetDefaults()
        {
            Item.width = 85;
            Item.height = 85;
            Item.value = Item.sellPrice(1, 50, 0, 0);
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.UseSound = SoundID.Item103;
            Item.damage = 400;
            Item.knockBack = 12;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<ChaosSlayer_BladeOfChaos>();
			Item.shootSpeed = 5;
            Item.useTurn = true;
            Item.rare = ModContent.RarityType<SuperancientsRarity>();
        }

        

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			Projectile.NewProjectile(player.GetSource_ItemUse(Item), position, velocity, type, damage, knockback, player.whoAmI);
			for (int m = 0; m < 2; m++)
			{
				Projectile.NewProjectile(player.GetSource_ItemUse(Item), position, velocity, m == 0 ? ModContent.ProjectileType<ChaosSlayer_BladeOfWrath>() : ModContent.ProjectileType<ChaosSlayer_BladeOfFury>(), damage, knockback, player.whoAmI);
			}
			return false;
		}

        public override void AddRecipes()  //How to craft this sword
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ReignOfFire>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Amenomuraku>(), 1);
            recipe.AddIngredient(ModContent.ItemType<ChaosScale>(), 5);
            recipe.AddIngredient(ModContent.ItemType<DiscordiumBar>(), 5);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
        }
    }
}