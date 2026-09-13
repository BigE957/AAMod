using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.GlowingMushroom.___PreHardmode.Items._BossTruffleToad.Weapons
{
    public class FrogLob : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Ranged";
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Frog Lob");
        }

		public override void SetDefaults()
		{
			Item.damage = 59;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.reuseDelay = 10;
            Item.shootSpeed = 8f;
            Item.knockBack = 3f;
            Item.width = 16;
            Item.height = 16;
            Item.damage = 15;
            Item.UseSound = SoundID.DD2_BetsysWrathShot;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(0, 0, 70, 0);
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<FrogLob_ToadGunk>();
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float spread = 45f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double startAngle = Math.Atan2(velocity.X, velocity.Y) - .1d;
            double deltaAngle = spread / 6f;
            double offsetAngle;
            for (int i = 0; i < 3; i++)
            {
                offsetAngle = startAngle + deltaAngle * i;
                Projectile.NewProjectile(player.GetSource_ItemUse(Item), position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), Item.shoot, damage, knockback, Main.myPlayer);
            }
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1, 0);
        }
    }
}
