using AAModClassic.Globals;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Weapons
{
    public class AbyssalKunai : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Ranged";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Abyssal Kunai");
        }

        public override void SetDefaults()
		{
			Item.damage = 140;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 14;
			Item.height = 34;
			Item.noUseGraphic = true;
			Item.useTime = 8;
			Item.useAnimation = 8;
			Item.shoot = ModContent.ProjectileType<AbyssalKunai_Proj>();
			Item.shootSpeed = 15f;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 0;
			Item.value = Item.sellPrice(0, 30, 0, 0);
			Item.rare = ItemRarityID.Purple;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

        

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float spread = 25f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double startAngle = Math.Atan2(velocity.X, velocity.Y) - .1d;
            double deltaAngle = spread / 6f;
            double offsetAngle;
            for (int i = 0; i < 3; i++)
            {
                offsetAngle = startAngle + deltaAngle * i;
                int proj = Projectile.NewProjectile(player.GetSource_ItemUse(Item), position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), type, damage, knockback, Main.myPlayer);
            }
            return false;
        }
    }
}
