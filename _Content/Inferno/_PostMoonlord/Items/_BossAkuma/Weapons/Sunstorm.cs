using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using AAModClassic.Globals;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Rarities;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;

namespace AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Weapons
{
    public class Sunstorm : BaseAAItem, ILocalizedModType
  {
        public new string LocalizationCategory => "Items.Weapons.Magic";
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sunstorm");
			/* Tooltip.SetDefault(@"Summons orbiting fireballs which home to enemies after some time
Right click and hold to release and aim manually"); */
        }

        public override void SetDefaults()
        {
            Item.autoReuse = true;
            Item.mana = 15;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.damage = 450;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.width = 40;
            Item.height = 40;
            Item.shoot = ModContent.ProjectileType<Sunstorm_Fireball>();
            Item.shootSpeed = 20f;
            Item.knockBack = 4.5f;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.DamageType = DamageClass.Magic;
            Item.rare = ModContent.RarityType<AncientsRarity>();
            Item.noMelee = true;
            Item.UseSound = SoundID.Item124;
        }

        

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		
		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            bool AnyOrbiters = AAGlobalProjectile.AnyProjectiles(ModContent.ProjectileType<Sunstorm_Fireball>());
            for (int Loops = 0; Loops < 4; Loops++)
            {
                Projectile.NewProjectile(player.GetSource_ItemUse(Item), position, velocity, type, damage, knockback, Main.myPlayer, 0, 0);
            }

            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<DaybreakIncineriteBar>(), 5);
            recipe.AddIngredient(ModContent.ItemType<CrucibleScale>(), 5);
            recipe.AddIngredient(ItemID.LunarFlareBook, 1);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
        }
    }
}
