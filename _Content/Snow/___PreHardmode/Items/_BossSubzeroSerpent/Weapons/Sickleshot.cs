using Microsoft.Xna.Framework;
using Terraria;
using System;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Snow.___PreHardmode.Items._BossSubzeroSerpent.Weapons
{
    public class Sickleshot : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Ranged";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sickleshot");
            // Tooltip.SetDefault("Shoots 2 ice arrows with high velocity ");
        }

        public override void SetDefaults()
        {

            Item.damage = 16;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 40;
            Item.height = 62;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ModContent.ProjectileType<Sickleshot_IceArrow>();
            Item.useAmmo = AmmoID.Arrow;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shootSpeed = 30f;

        }

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
		    float spread = 15f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double startAngle = Math.Atan2(velocity.X, velocity.Y) - .1d;
            double deltaAngle = spread / 6f;
		    double offsetAngle;
		    for (int i = 0; i < 2; i++)
		    {
		    	offsetAngle = startAngle + deltaAngle * i;
		    	Projectile.NewProjectile(player.GetSource_ItemUse(Item), position.X, position.Y, baseSpeed*(float)Math.Sin(offsetAngle), baseSpeed*(float)Math.Cos(offsetAngle), Item.shoot, damage, knockback, Main.myPlayer);
		    }
		    return false;
		}
    }
}
