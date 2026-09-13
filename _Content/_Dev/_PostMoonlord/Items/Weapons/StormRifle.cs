using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using AAModClassic.Globals;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content._Dev._PostMoonlord.Items.Weapons
{
    public class StormRifle : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Ranged";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Storm Rifle");
            /* Tooltip.SetDefault(@"Fires off static shots
'NUM'
-BlazenBreaker"); */
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            Projectile.NewProjectile(player.GetSource_ItemUse(Item), position, velocity, Terraria.ModLoader.ModContent.ProjectileType<StormRifle_ThunderSpark>(), damage, knockback, Main.myPlayer, 0, 0);
            return false;
        }

        public override void SetDefaults()
        {
            Item.damage = 175;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged; 
            Item.width = 70; 
            Item.height = 24;
            Item.useTime = 20; 
            Item.useAnimation = 20; 
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ModContent.ProjectileType<StormRifle_ThunderSpark>();
            Item.knockBack = 3;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Cyan;
            Item.UseSound = SoundID.Item92;
            Item.autoReuse = true; 
            Item.shootSpeed = 9f;
            Item.useAmmo = AmmoID.Bullet;

			customNameColor = new Color(0, 0, 255);			
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1, 0);
        }
    }
}