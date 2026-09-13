using AAModClassic._Content.Desert.__Hardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Desert.__Hardmode.Items._BossAnubis.Weapons
{
    public class Sandthrower : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Ranged";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Sandthrower");
			// Tooltip.SetDefault("30% chance to not consume gel");
		}

	    public override void SetDefaults()
	    {
			Item.damage = 30;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 80;
			Item.height = 38;
			Item.useTime = 3;
			Item.useAnimation = 5;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 4f;
			Item.UseSound = SoundID.Item34;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.LightPurple;
            Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<Sandthrower_Sandstorm>();
			Item.shootSpeed = 12f;
			Item.useAmmo = 23;
            Item.consumeAmmoOnFirstShotOnly = true;
        }

	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(player.GetSource_ItemUse(Item), position, velocity, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
            return false;
		}

	    public override bool CanConsumeAmmo(Item ammo, Player player)
	    {
	    	if (Main.rand.Next(0, 100) < 30)
	    		return false;
	    	return true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Sandgun, 1);
			recipe.AddIngredient(ModContent.ItemType<ForsakenFragment>(), 5);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}
