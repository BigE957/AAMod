using AAModClassic._Content.Desert.___PreHardmode.Items._BossDesertDjinn.Weapons;
using AAModClassic._Content.Desert.__Hardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Desert.__Hardmode.Items._BossAnubis.Weapons
{
	public class Judgment : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Melee";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Judgement");
			// Tooltip.SetDefault("Releases enchanted sand rain on enemy hit");
		}

		public override void SetDefaults()
		{
			Item.damage = 32;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.crit = 10;
			Item.width = 52;
			Item.height = 52;
			Item.useTime = 21;
			Item.useAnimation = 21;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = 100000;
			Item.rare = ItemRarityID.LightPurple;
            Item.knockBack = 4;
            Item.autoReuse = true;
			Item.UseSound = SoundID.Item1;
			Item.scale = 1.1f;
		}
		
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			int damage = damageDone;
			Vector2 vel1 = new Vector2(0, -1);
			vel1 *= 11f;
			Projectile.NewProjectile(player.GetSource_ItemUse(Item), target.Center.X, target.Center.Y-20, vel1.X, vel1.Y, ModContent.ProjectileType<Judgment_EnchantedSand>(), damage/3, 0, Main.myPlayer);
			Projectile.NewProjectile(player.GetSource_ItemUse(Item), target.Center.X-5, target.Center.Y-20, vel1.X, vel1.Y, ModContent.ProjectileType<Judgment_EnchantedSand>(), damage/3, 0, Main.myPlayer);
			Projectile.NewProjectile(player.GetSource_ItemUse(Item), target.Center.X-10, target.Center.Y-18, vel1.X, vel1.Y, ModContent.ProjectileType<Judgment_EnchantedSand>(), damage/3, 0, Main.myPlayer);
			Projectile.NewProjectile(player.GetSource_ItemUse(Item), target.Center.X-15, target.Center.Y-16, vel1.X, vel1.Y, ModContent.ProjectileType<Judgment_EnchantedSand>(), damage/3, 0, Main.myPlayer);
			Projectile.NewProjectile(player.GetSource_ItemUse(Item), target.Center.X-20, target.Center.Y-14, vel1.X, vel1.Y, ModContent.ProjectileType<Judgment_EnchantedSand>(), damage/3, 0, Main.myPlayer);
			Projectile.NewProjectile(player.GetSource_ItemUse(Item), target.Center.X-25, target.Center.Y-12, vel1.X, vel1.Y, ModContent.ProjectileType<Judgment_EnchantedSand>(), damage/3, 0, Main.myPlayer);
			Projectile.NewProjectile(player.GetSource_ItemUse(Item), target.Center.X+5, target.Center.Y-20, vel1.X, vel1.Y, ModContent.ProjectileType<Judgment_EnchantedSand>(), damage/3, 0, Main.myPlayer);
			Projectile.NewProjectile(player.GetSource_ItemUse(Item), target.Center.X+10, target.Center.Y-18, vel1.X, vel1.Y, ModContent.ProjectileType<Judgment_EnchantedSand>(), damage/3, 0, Main.myPlayer);
			Projectile.NewProjectile(player.GetSource_ItemUse(Item), target.Center.X+15, target.Center.Y-16, vel1.X, vel1.Y, ModContent.ProjectileType<Judgment_EnchantedSand>(), damage/3, 0, Main.myPlayer);
			Projectile.NewProjectile(player.GetSource_ItemUse(Item), target.Center.X+20, target.Center.Y-14, vel1.X, vel1.Y, ModContent.ProjectileType<Judgment_EnchantedSand>(), damage/3, 0, Main.myPlayer);
			Projectile.NewProjectile(player.GetSource_ItemUse(Item), target.Center.X+25, target.Center.Y-12, vel1.X, vel1.Y, ModContent.ProjectileType<Judgment_EnchantedSand>(), damage/3, 0, Main.myPlayer);
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<SultansScimitar>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ForsakenFragment>(), 5);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}
