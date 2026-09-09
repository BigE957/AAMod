using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Stars._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items._BossEmperorFishron.Weapons
{
	public class UltibladeTyphoon : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Magic";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ultiblade Typhoon");
			/* Tooltip.SetDefault(@"Casts 3 fast homing razorwheels
Razorblade Typhoon EX"); */
		}

		public override void SetDefaults()
		{
			Item.mana = 16;
			Item.damage = 175;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shootSpeed = 6f;
			Item.shoot = ProjectileID.Typhoon;
			Item.width = 26;
			Item.height = 28;
			Item.UseSound = SoundID.Item84;
			Item.useAnimation = 30;
			Item.useTime = 15;
			Item.autoReuse = true;
			Item.rare = ItemRarityID.Cyan;
			Item.noMelee = true;
			Item.knockBack = 6f;
			Item.scale = 0.9f;
			Item.value = Item.sellPrice(0, 25, 0, 0);
			Item.DamageType = DamageClass.Magic;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			float numberProjectiles = 3;
			float rotation = MathHelper.ToRadians(10);
			position += Vector2.Normalize(velocity) * 45f;
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = (velocity * 5).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
				int proj = Projectile.NewProjectile(player.GetSource_ItemUse(Item), position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage*2, knockback, player.whoAmI);
				Main.projectile[proj].penetrate = 10;
				Main.projectile[proj].usesLocalNPCImmunity = true;
				Main.projectile[proj].localNPCHitCooldown = 1;
			}
			return false;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();      
			recipe.AddIngredient(ItemID.RazorbladeTyphoon);
			recipe.AddIngredient(ModContent.ItemType<EXSoul>(), 1);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
		}
	}
}