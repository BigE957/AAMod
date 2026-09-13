using AAModClassic._Content._Dev._PostMoonlord.Items.Weapons;
using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Stars._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons
{
    public class SoulShredder : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Melee";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Soul Shredder");
            /* Tooltip.SetDefault(@"Left click to swing and release homing scythe
Right click to do dashing hit
You are immune during the dash and deal 15x damage in true melee
Dashing ability has 5 seconds CD
'Well, how many Grim Reapers have you met before, mate?'
-Gregg
Scythe of the Grim Reaper EX"); */
		}

		public override void SetDefaults()
		{
			Item.autoReuse = true;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useAnimation = 30;
			Item.useTime = 30;
			Item.knockBack = 6f;
			Item.width = 24;
			Item.height = 28;
			Item.damage = 225;
			Item.crit = 14;
			Item.scale = 1.15f;
			Item.UseSound = SoundID.Item71;
			Item.rare = ItemRarityID.Purple;
			Item.shoot = ModContent.ProjectileType<SoulShredder_Proj>();
			Item.shootSpeed = 16f;
			Item.value = 1000000;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		}
		
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		
		public override bool CanUseItem(Player player)
		{
			int side = player.direction;
			if (player.altFunctionUse != 2)
			{
				Item.shoot = ModContent.ProjectileType<SoulShredder_Proj>();
				return true;
			}
			if (player.altFunctionUse == 2 && !player.HasBuff(ModContent.BuffType<ScytheOfTheGrimReaper_ReaperScytheImmunityCooldown>()))
			{
				player.AddBuff(ModContent.BuffType<ScytheOfTheGrimReaper_ReaperScytheImmunity>(), 60);
				player.AddBuff(ModContent.BuffType<ScytheOfTheGrimReaper_ReaperScytheImmunityCooldown>(), 300);
				Item.shoot = ModContent.ProjectileType<ScytheOfTheGrimReaper_Hitbox>();
				player.velocity.X = 26f * side;
				return true;
			}
			else
			{
				return false;
			}
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (type == ModContent.ProjectileType<SoulShredder_Proj>())
			{
				float num121 = 0.99f;
				int num122 = 3;
				Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
				float num82 = Main.mouseX + Main.screenPosition.X - vector2.X;
				float num83 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
				Vector2 vector14 = velocity;
				vector14.Normalize();
				vector14 *= 40f;
				bool flag11 = Collision.CanHit(vector2, 0, 0, vector2 + vector14, 0, 0);
				for (int num123 = 0; num123 < num122; num123++)
				{
					float num124 = num123 - (num122 - 1f) / 2f;
					Vector2 vector15 = vector14.RotatedBy(num121 * num124, default);
					if (!flag11)
					{
						vector15 -= vector14;
					}
					if (type == ModContent.ProjectileType<SoulShredder_Proj>() && player.HasBuff(ModContent.BuffType<ScytheOfTheGrimReaper_ReaperScytheImmunity>()))
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(Item), vector2.X + vector15.X, vector2.Y + vector15.Y, num82, num83, type, damage/15, knockback, player.whoAmI);
					}
					else
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(Item), vector2.X + vector15.X, vector2.Y + vector15.Y, num82, num83, type, damage, knockback, player.whoAmI);
					}
				}
			}
			if (type == ModContent.ProjectileType<ScytheOfTheGrimReaper_Hitbox>())
			{
				return true;
			}
			return false;
		}
		
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<ScytheOfTheGrimReaper>(), 1);
            recipe.AddIngredient(ModContent.ItemType<EXSoul>(), 1);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
        }
	}
}
