using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.Weapons;
using AAModClassic._Content.Stars._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons
{
    public class EternalTwilight : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Ranged";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Eternal Twilight");
            // Tooltip.SetDefault("Falling Twilight EX");
        }

        public override void SetDefaults()
        {
            Item.damage = 237;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 44;
            Item.height = 76;
            Item.useAnimation = 17;
            Item.useTime = 5;
            Item.reuseDelay = 7;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 2.5f;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Arrow;
            Item.expert = true;
            Item.rare = ItemRarityID.Cyan;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return !(player.itemAnimation < Item.useAnimation - 1);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (!(player.itemAnimation == 1))
            {
                float SpeedX = velocity.X + Main.rand.Next(-25, 26) * 0.05f;
                float SpeedY = velocity.Y + Main.rand.Next(-25, 26) * 0.05f;
                Projectile.NewProjectile(player.GetSource_ItemUse(Item), position, velocity, ModContent.ProjectileType<EternalTwilight_YamataPhantom>(), damage, knockback, player.whoAmI, 0.0f, 0.0f);
            }
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FallingTwilight>());
            recipe.AddIngredient(ModContent.ItemType<EXSoul>());
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
        }
    }
}
