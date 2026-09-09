using System;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Weapons
{
    public class YearOfTheDragon : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Ranged";


        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Year of the Dragon");
            /* Tooltip.SetDefault(@"Fires dazzling fireworks to amaze your friends and blow your enemies away!
'Akuma brand fireworks is not responsible for damage done by shooting fireworks point-blank into enemies' faces.'"); */
        }

        public override void SetDefaults()
        {
            Item.crit = 14;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.useAmmo = AmmoID.Rocket;
            Item.width = 50;
            Item.height = 20;
            Item.shoot = ModContent.ProjectileType<YearOfTheDragon_Proj>();
            Item.UseSound = SoundID.Item11;
            Item.damage = 600;
            Item.shootSpeed = 30f;
            Item.noMelee = true;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.knockBack = 2f;
            Item.rare = ModContent.RarityType<AncientsRarity>();
            Item.DamageType = DamageClass.Ranged;
        }

        

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float num72 = Item.shootSpeed;
            int num112 = 3;
            for (int num113 = 0; num113 < num112; num113++)
            {
                Vector2 vector2 = new Vector2(player.position.X + player.width * 0.5f + Main.rand.Next(201) * -(float)player.direction + (Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - - 600f);
                vector2.X = (vector2.X + player.Center.X) / 2f + Main.rand.Next(-200, 201);
                vector2.Y -= 100 * num113;
                float num78 = Main.mouseX + Main.screenPosition.X - vector2.X + Main.rand.Next(-40, 41) * 0.03f;
                float num79 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
                if (num79 < 0f)
                {
                    num79 *= -1f;
                }
                if (num79 < 20f)
                {
                    num79 = 20f;
                }
                float num80 = (float)Math.Sqrt(num78 * num78 + num79 * num79);
                num80 = num72 / num80;
                num78 *= num80;
                num79 *= num80;
                float num114 = num78;
                float num115 = num79 + Main.rand.Next(-40, 41) * 0.02f;
                Projectile.NewProjectile(player.GetSource_ItemUse(Item), vector2.X, vector2.Y, num114 * 0.75f, num115 * -0.75f, ModContent.ProjectileType<YearOfTheDragon_Proj>(), damage/2, knockback, player.whoAmI, 0f, -0.5f + (float)Main.rand.NextDouble() * 0.3f);
            }
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1, 0);
        }

        public override void AddRecipes()  //How to craft this sword
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DaybreakIncineriteBar>(), 5);
            recipe.AddIngredient(ModContent.ItemType<CrucibleScale>(), 5);
            recipe.AddIngredient(ItemID.FireworksLauncher);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
        }
    }
}
