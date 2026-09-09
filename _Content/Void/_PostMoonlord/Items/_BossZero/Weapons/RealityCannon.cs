using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using AAModClassic.Globals;
using AAModClassic._Content.Void._PostMoonlord.Items.Materials;
using AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Ammo;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Rarities;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;

namespace AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Weapons
{
    public class RealityCannon : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Ranged";
        
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Reality Cannon");
            // Tooltip.SetDefault("Rapidly Fires a spread of dark lasers");
        }

        public override void SetDefaults()
        {
            
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.shootSpeed = 16f;
            Item.knockBack = 0f;
            Item.width = 48;
            Item.height = 26;
            Item.damage = 300;
            Item.UseSound = SoundID.Item12;
            Item.shoot = ModContent.ProjectileType<UnstablePowerCell_Proj>();
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.noUseGraphic = false;
            Item.rare = ModContent.RarityType<AncientsRarity>();
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
                Projectile.NewProjectile(player.GetSource_ItemUse(Item), position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), type, damage, knockback, Main.myPlayer);
            }
            return false;
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<ApocalyptitePlate>(), 5);
			recipe.AddIngredient(ModContent.ItemType<UnstableSingularity>(), 5);
			recipe.AddIngredient(ItemID.StarCannon);
	        recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
	        recipe.Register();
		}
	}
}
