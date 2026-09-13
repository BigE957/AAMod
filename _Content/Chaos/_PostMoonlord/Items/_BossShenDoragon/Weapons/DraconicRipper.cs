using AAModClassic._Content.Chaos._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossShenDoragon.Weapons
{
    public class DraconicRipper : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Ranged";
		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.autoReuse = true;
			Item.useAnimation = 2;
			Item.useTime = 2;
			Item.width = 72;
            Item.height = 34;
            Item.shoot = ProjectileID.Bullet;
			Item.UseSound = SoundID.Item41;
			Item.damage = 65;
			Item.shootSpeed = 16f;
			Item.noMelee = true;
			Item.value = Item.sellPrice(0, 30, 0, 0);
			Item.knockBack = 3f;
			Item.DamageType = DamageClass.Ranged;
            Item.useAmmo = AmmoID.Bullet;
            Item.rare = ModContent.RarityType<SuperancientsRarity>();
        }

        

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Draconic Ripper");
			/* Tooltip.SetDefault(@"Shoots dozens of high-caliber teeth
Ignores enemy defense
50% chance to not consume ammo"); */
        }
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, -2);
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() >= .5f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < 3; i++)
            {
                int tooth;

                if(i == 0)
                {
                    tooth = ModContent.ProjectileType<DraconicRipper_ShenDoragonTooth>();
                }
                else if(i == 1)
                {
                    tooth = ModContent.ProjectileType<DraconicRipper_AkumaTooth>();
                    knockback += 2;
                    damage -= 10;
                }
                else
                {
                    tooth = ModContent.ProjectileType<DraconicRipper_YamataTooth>();
                    knockback -= 2;
                    damage += 10;
                }
                Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(10));
                int p = Projectile.NewProjectile(player.GetSource_ItemUse(Item), position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, tooth, damage, knockback, player.whoAmI);
                Main.projectile[p].DamageType = DamageClass.Ranged;
            }
            return false;
        }

        public override void HoldItem(Player player)
		{
			player.GetArmorPenetration(DamageClass.Generic) += 500;
		}
		
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<DiscordiumBar>(), 5);
            recipe.AddIngredient(ModContent.ItemType<ChaosScale>(), 5);
			recipe.AddIngredient(ItemID.ChainGun);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
			recipe.Register();
		}
	}
}
