using AAModClassic._Content.Desert.__Hardmode.Items._BossAnubis.Weapons;
using AAModClassic._Content.Desert._PostMoonlord.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Desert._PostMoonlord.Items._BossAnubisA.Weapons
{
    public class CursedFlamefury : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Ranged";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cursed Flamefury");
			// Tooltip.SetDefault("50% chance to not consume gel");
		}

	    public override void SetDefaults()
	    {
			Item.damage = 70;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 80;
			Item.height = 38;
			Item.useTime = 5;
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 4f;
			Item.UseSound = SoundID.Item34;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<CursedFlamefury_ForsakenFlame>();
			Item.shootSpeed = 10f;
			Item.useAmmo = 23;
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
            Item.consumeAmmoOnFirstShotOnly = true;
        }

        

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	        for (int index = 0; index < 2; ++index)
	        {
	            float SpeedX = velocity.X + Main.rand.Next(-25, 26) * 0.05f;
	            float SpeedY = velocity.Y + Main.rand.Next(-25, 26) * 0.05f;
                Projectile.NewProjectile(player.GetSource_ItemUse(Item), position, velocity, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
            }
	    	return false;
		}

	    public override bool CanConsumeAmmo(Item ammo, Player player)
	    {
	    	if (Main.rand.Next(0, 100) < 50)
	    		return false;
	    	return true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<Sandthrower>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SoulFragment>(), 5);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
	}
}
