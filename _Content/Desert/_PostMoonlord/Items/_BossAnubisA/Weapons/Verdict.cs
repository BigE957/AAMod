using AAModClassic._Content.Desert.__Hardmode.Items._BossAnubis.Weapons;
using AAModClassic._Content.Desert._PostMoonlord.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Desert._PostMoonlord.Items._BossAnubisA.Weapons
{
	public class Verdict : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Melee";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Verdict");
			/* Tooltip.SetDefault(@"Releases enchanted sand rain on enemy hit
Creates 2 forsaken phantom blades which hit enemy horizontally as well"); */
		}

		public override void SetDefaults()
		{
			Item.damage = 80;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.crit = 10;
			Item.width = 96;
			Item.height = 92;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 4;
            Item.autoReuse = true;
			Item.UseSound = SoundID.Item1;
			Item.rare = ItemRarityID.Purple;
		}

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			int damage = damageDone;
			Vector2 vel1 = new Vector2(0, -1);
			vel1 *= 8f;
			Projectile.NewProjectile(player.GetSource_ItemUse(Item), target.Center.X, target.Center.Y-20, vel1.X, vel1.Y, ModContent.ProjectileType<Verdict_ForsakenSand>(), damage/3, 0, Main.myPlayer);
			Projectile.NewProjectile(player.GetSource_ItemUse(Item), target.Center.X-10, target.Center.Y-18, vel1.X, vel1.Y, ModContent.ProjectileType<Verdict_ForsakenSand>(), damage/3, 0, Main.myPlayer);
			Projectile.NewProjectile(player.GetSource_ItemUse(Item), target.Center.X-20, target.Center.Y-14, vel1.X, vel1.Y, ModContent.ProjectileType<Verdict_ForsakenSand>(), damage/3, 0, Main.myPlayer);
			Projectile.NewProjectile(player.GetSource_ItemUse(Item), target.Center.X-30, target.Center.Y-10, vel1.X, vel1.Y, ModContent.ProjectileType<Verdict_ForsakenSand>(), damage/2, 0, Main.myPlayer);
			Projectile.NewProjectile(player.GetSource_ItemUse(Item), target.Center.X+10, target.Center.Y-18, vel1.X, vel1.Y, ModContent.ProjectileType<Verdict_ForsakenSand>(), damage/3, 0, Main.myPlayer);
			Projectile.NewProjectile(player.GetSource_ItemUse(Item), target.Center.X+20, target.Center.Y-14, vel1.X, vel1.Y, ModContent.ProjectileType<Verdict_ForsakenSand>(), damage/3, 0, Main.myPlayer);
			Projectile.NewProjectile(player.GetSource_ItemUse(Item), target.Center.X+30, target.Center.Y-10, vel1.X, vel1.Y, ModContent.ProjectileType<Verdict_ForsakenSand>(), damage/2, 0, Main.myPlayer);
			Vector2 vel2 = new Vector2(-1, 0);
			vel2 *= 16f;
			Vector2 vel3 = new Vector2(1, 0);
			vel3 *= 16f;
			Projectile.NewProjectile(player.GetSource_ItemUse(Item), target.Center.X + 600, target.Center.Y, vel2.X, vel2.Y, ModContent.ProjectileType<Verdict_ForsakenPhantomBlade>(), damage/2, 0, Main.myPlayer);
			Projectile.NewProjectile(player.GetSource_ItemUse(Item), target.Center.X - 600, target.Center.Y, vel3.X, vel3.Y, ModContent.ProjectileType<Verdict_ForsakenPhantomBlade>(), damage/2, 0, Main.myPlayer);
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<Judgment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SoulFragment>(), 5);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
	}
}
