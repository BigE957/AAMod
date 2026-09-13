using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAModClassic.Globals;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Rarities;

namespace AAModClassic._Content.Bunny._PostMoonlord.Items._BossRajahRabbitA.Weapons
{
    public class FluffyFury : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Ranged";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fluffy Fury");
            /* Tooltip.SetDefault(@"Converts arrows into splitting Carrows
Potential lag warning"); */
        }

        public override void SetDefaults()
        {
            Item.damage = 400;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 44;
            Item.height = 76;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 3f;
            Item.value = Item.sellPrice(0, 60, 0, 0);
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Arrow;
            Item.rare = ModContent.RarityType<SuperancientsRarity>();
        }

        

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			type = ModContent.ProjectileType<FluffyFury_Carrow>();
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            float num117 = 0.314159274f;
            int num118 = Main.rand.Next(2,5);
            Vector2 vector7 = velocity;
            vector7.Normalize();
            vector7 *= 40f;
            bool flag11 = Collision.CanHit(vector2, 0, 0, vector2 + vector7, 0, 0);
            for (int num119 = 0; num119 < num118; num119++)
            {
                float num120 = num119 - (num118 - 1f) / 2f;
                Vector2 value9 = vector7.RotatedBy(num117 * num120);
                if (!flag11)
                {
                    value9 -= vector7;
                }
                int num121 = Projectile.NewProjectile(player.GetSource_ItemUse(Item), vector2.X + value9.X, vector2.Y + value9.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
                Main.projectile[num121].noDropItem = true;
            }
            return false;
        }
    }
}
