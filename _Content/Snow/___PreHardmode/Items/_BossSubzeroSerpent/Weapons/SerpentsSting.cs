using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Snow.___PreHardmode.Items._BossSubzeroSerpent.Weapons
{
    public class SerpentsSting : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Ranged";
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Serpent's Sting");
			// Tooltip.SetDefault("Turns bullets into snow shots");
		}

		public override void SetDefaults()
		{
			Item.damage = 60;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 52;
			Item.height = 24;
			Item.useAnimation = 40;
			Item.useTime = 40;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 2;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item40;
			Item.autoReuse = false;
			Item.shootSpeed = 16f;
			Item.useAmmo = AmmoID.Bullet;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.crit = 3;
		}


        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(player.GetSource_ItemUse(Item), position, velocity, ModContent.ProjectileType<Sting>(), damage, knockback, player.whoAmI, 0f, 0f); //This is spawning a projectile of type FrostburnArrow using the original stats
            return false;
        }

        public override Vector2? HoldoutOffset()
		{
			return new Vector2(-2, 4);
		}
	}
}
