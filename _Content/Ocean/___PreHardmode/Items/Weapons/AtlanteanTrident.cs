using AAModClassic._Content.Desert.___PreHardmode.Items._BossDesertDjinn.Weapons;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Weapons;
using AAModClassic._Content.Mire.___PreHardmode.Items.Weapons;
using AAModClassic._Content.Snow.___PreHardmode.Items._BossSubzeroSerpent.Weapons;
using AAModClassic._Content.Void.___PreHardmode.Items._BossSagittarius.Weapons;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Ocean.___PreHardmode.Items.Weapons
{
	public class AtlanteanTrident : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Magic";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Atlantean Trident");
			// Tooltip.SetDefault("Fires off a tri-shot of water bolts");
			Item.staff[Item.type] = true;
		}

		public override void SetDefaults()
		{
			Item.damage = 30;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 8;
			Item.width = 68;
			Item.height = 68;
			Item.useTime = 35;
			Item.useAnimation = 35;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 4;
			Item.value = 500000;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item21;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.WaterBolt;
			Item.shootSpeed = 10f;
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
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<OceanTrident>());
			recipe.AddIngredient(ModContent.ItemType<BlazePike>());
			recipe.AddIngredient(ModContent.ItemType<SandLamp>());
			recipe.AddIngredient(ModContent.ItemType<NeutronRod>());
			recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
			
			recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<OceanTrident>());
			recipe.AddIngredient(ModContent.ItemType<SludgeShot>());
			recipe.AddIngredient(ModContent.ItemType<Icicle>());
			recipe.AddIngredient(ModContent.ItemType<NeutronRod>());
			recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
		}
	}
}