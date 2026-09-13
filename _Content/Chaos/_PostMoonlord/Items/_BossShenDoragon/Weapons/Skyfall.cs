using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using AAModClassic.Globals;
using AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.Weapons;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Materials;
using AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Weapons;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Rarities;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossShenDoragon.Weapons
{
    public class Skyfall : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Ranged";
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Skyfall");
        }

        public override void SetDefaults()
        {
            Item.damage = 210;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 22;
            Item.height = 50;
            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.useAmmo = AmmoID.Arrow;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.channel = true;
            Item.knockBack = 5f;
            Item.value = Item.sellPrice(1, 50, 0, 0);
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Skyfall_Proj>();
            Item.shootSpeed = 14f;
            Item.UseSound = SoundID.Item124;
            Item.rare = ModContent.RarityType<SuperancientsRarity>();
        }

        

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float num72 = Item.shootSpeed;
            type = Main.rand.Next(3);

            switch (type)
            {
                case 0:
                    type = ModContent.ProjectileType<Skyfall_Proj>();
                    break;
                case 1:
                    type = ModContent.ProjectileType<Skyfall_ProjRed>();
                    break;
                default:
                    type = ModContent.ProjectileType<Skyfall_ProjBlue>();
                    break;
            }
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            float num78 = Main.mouseX + Main.screenPosition.X - vector2.X;
            float num79 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
            if (player.gravDir == -1f)
            {
                num79 = Main.screenPosition.Y + Main.screenHeight - Main.mouseY - vector2.Y;
            }
            float num80 = (float)Math.Sqrt(num78 * num78 + num79 * num79);
            float num81 = num80;
            if (float.IsNaN(num78) && float.IsNaN(num79) || num78 == 0f && num79 == 0f)
            {
                num78 = player.direction;
                num79 = 0f;
                num80 = num72;
            }
            else
            {
                num80 = num72 / num80;
            }
            num78 *= num80;
            num79 *= num80;
            vector2 = new Vector2(player.position.X + player.width * 0.5f + Main.rand.Next(201) * -(float)player.direction + (Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
            vector2.X = (vector2.X + player.Center.X) / 2f + Main.rand.Next(-200, 201);
            num78 = Main.mouseX + Main.screenPosition.X - vector2.X + Main.rand.Next(-40, 41) * 0.03f;
            num79 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
            if (num79 < 0f)
            {
                num79 *= -1f;
            }
            if (num79 < 20f)
            {
                num79 = 20f;
            }
            num80 = (float)Math.Sqrt(num78 * num78 + num79 * num79);
            num80 = num72 / num80;
            num78 *= num80;
            num79 *= num80;
            float num114 = num78;
            float num115 = num79 + Main.rand.Next(-40, 41) * 0.02f;
            Projectile.NewProjectile(player.GetSource_ItemUse(Item), vector2.X, vector2.Y, num114 * 0.75f, num115 * 0.75f, type, damage, knockback, player.whoAmI, 0f, 0.5f + (float)Main.rand.NextDouble() * 0.3f);
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RadiantDawn>(), 1);
            recipe.AddIngredient(ModContent.ItemType<FallingTwilight>(), 1);
            recipe.AddIngredient(ModContent.ItemType<ChaosScale>(), 5);
            recipe.AddIngredient(ModContent.ItemType<DiscordiumBar>(), 5);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
        }
    }
}