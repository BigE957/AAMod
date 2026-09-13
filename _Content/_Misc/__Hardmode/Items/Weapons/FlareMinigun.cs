using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Misc.__Hardmode.Items.Weapons
{
    public class FlareMinigun : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Ranged";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Flare Minigun");
			/* Tooltip.SetDefault("Shoots dozens of flares in rapid succession"
			+"\n33% chance not to consume flares"
			+"\nRight-click to disable all flares"); */
        }

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.ChainGun);
			Item.damage = 46;
			Item.DamageType = DamageClass.Ranged;
			Item.knockBack = 1;
			Item.width = 62;
			Item.height = 24;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.value = 200000;
			Item.rare = ItemRarityID.Pink;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.Flare;
			Item.useAmmo = AmmoID.Flare;
			Item.UseSound = SoundID.Item11;
		}
		
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		
		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
                foreach (Projectile p in Main.ActiveProjectiles)
                {
					if (p.type == ProjectileID.Flare || 
						p.type == ProjectileID.BlueFlare ||
                        p.type == ProjectileID.SpelunkerFlare ||
                        p.type == ProjectileID.CursedFlare || 
						p.type == ProjectileID.RainbowFlare || 
						p.type == ProjectileID.ShimmerFlare)
					{
						p.Kill();
					}
				}
				return false;
			}
			else
			{
				return true;
			}
		}
		
		public override bool CanConsumeAmmo(Item ammo, Player player)
		{
		    return Main.rand.NextFloat() >= .33;
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
			Vector2 perturbedSpeed2 = velocity.RotatedByRandom(MathHelper.ToRadians(5));
			Vector2 perturbedSpeed3 = velocity.RotatedByRandom(MathHelper.ToRadians(5));
			Vector2 perturbedSpeed4 = velocity.RotatedByRandom(MathHelper.ToRadians(8));

            Projectile p = Projectile.NewProjectileDirect(source, vector, perturbedSpeed2, type, damage, knockback, player.whoAmI);
			p.DamageType = DamageClass.Ranged;
            p = Projectile.NewProjectileDirect(source, vector, velocity, type, damage, knockback, player.whoAmI);
            p.DamageType = DamageClass.Ranged;
            p = Projectile.NewProjectileDirect(source, vector, perturbedSpeed3, type, damage, knockback, player.whoAmI);
            p.DamageType = DamageClass.Ranged;
            p = Projectile.NewProjectileDirect(source, vector, perturbedSpeed4, type, damage, knockback, player.whoAmI);
            p.DamageType = DamageClass.Ranged;
            return false;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.FlareGun);
			recipe.AddIngredient(ItemID.Minishark);
			recipe.AddIngredient(ItemID.IllegalGunParts);
			recipe.AddIngredient(ItemID.SoulofSight, 5);
			recipe.AddIngredient(ItemID.SoulofMight, 5);
			recipe.AddIngredient(ItemID.SoulofFright, 5);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}
