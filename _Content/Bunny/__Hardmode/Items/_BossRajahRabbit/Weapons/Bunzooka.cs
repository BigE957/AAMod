using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny.__Hardmode.Items._BossRajahRabbit.Weapons
{

    public class Bunzooka : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Ranged";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bunzooka");
            // Tooltip.SetDefault("Fires Rabbit Rockets");
        }

        public override void SetDefaults()
        {
            Item.damage = 40;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 66;
            Item.height = 28;
            Item.useTime = 32;
            Item.useAnimation = 32;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true; //so the item's animation doesn't do damage
            Item.knockBack = 7.5f;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.shootSpeed = 24f;
            Item.shoot = ModContent.ProjectileType<Bunzooka_RajahRocket>();
            Item.useAmmo = AmmoID.Rocket;
            Item.rare = ItemRarityID.Yellow;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20, -6);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            Projectile.NewProjectile(player.GetSource_ItemUse(Item), position, velocity, ModContent.ProjectileType<Bunzooka_RajahRocket>(), damage, knockback, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
    }
}