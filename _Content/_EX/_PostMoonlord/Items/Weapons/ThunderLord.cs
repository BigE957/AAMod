using AAModClassic._Content._Dev._PostMoonlord.Items.Weapons;
using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Stars._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons
{
    public class ThunderLord : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Ranged";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Thunder Lord");
            /* Tooltip.SetDefault(@"Fires off Thundershots and has a rare chance to shoot a Supercharged Thundershot that calls down Thunder from the sky
Storm Rifle EX"); */
        }

        public override void SetDefaults()
        {
            Item.damage = 375;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged; 
            Item.width = 90; 
            Item.height = 30;
            Item.useTime = 2; 
            Item.useAnimation = 6; 
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ModContent.ProjectileType<ThunderLord_SuperchargedThunderbullet>();
            Item.knockBack = 3;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Cyan;
            Item.UseSound = new SoundStyle("AAModClassic/Sounds/Thunderlord");
            Item.autoReuse = true; 
            Item.shootSpeed = 9f;
            Item.useAmmo = AmmoID.Bullet;
            Item.crit = 10; 
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1, 0);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            type = Main.rand.NextBool(20) ? ModContent.ProjectileType<ThunderLord_SuperchargedThunderbullet>() : ModContent.ProjectileType<ThunderLord_Thundershot>();
            Projectile.NewProjectile(player.GetSource_ItemUse(Item), position, velocity, type, damage, knockback, player.whoAmI, 2f, 2f);
            return false;
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<StormRifle>(), 1);
            recipe.AddIngredient(ModContent.ItemType<EXSoul>(), 1);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
        }
    }
}
