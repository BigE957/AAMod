using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAModClassic.Globals;
using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Weapons;
using AAModClassic._Content.Acropolis._PostMoonlord.Items.Materials;
using AAModClassic._Content.Acropolis.Projectiles;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Rarities;

namespace AAModClassic._Content.Acropolis._PostMoonlord.Items._BossAthenaA.Weapons
{
    public class Windfury : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Ranged";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Windfury");
            // Tooltip.SetDefault("Replaces wooden arrows with gale arrows with high knockback and infinite piercing");
        }

        public override void SetDefaults()
        {
            Item.damage = 140; 
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 26;
            Item.height = 50;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.useAmmo = AmmoID.Arrow;
            Item.knockBack = 0;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shootSpeed = 10f;
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
        }

        

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
            {
                Projectile.NewProjectile(player.GetSource_ItemUse(Item), position, velocity, ModContent.ProjectileType<GaleArrow>(), damage, knockback * 5, player.whoAmI, 0f, 0f);
                return false;
            }
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<RazorwindLongbow>(), 1);
            recipe.AddIngredient(ModContent.ItemType<StormSphere>(), 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}