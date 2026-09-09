using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using AAModClassic.Globals;
using AAModClassic._Content.Mire._PostMoonlord.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Rarities;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;

namespace AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.Weapons
{
    public class Flairdra : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Melee";
        
        public override void SetStaticDefaults()
        {

            // DisplayName.SetDefault("Flairdra");
            /* Tooltip.SetDefault(@"Be the hydra.
Inflicts Moonraze"); */
            ItemID.Sets.ToolTipDamageMultiplier[Type] = 2f;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 22;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 12;
            Item.useTime = 12;
            Item.knockBack = 3.5f;
            Item.damage = 160;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<Flairdra_Holdout>();
            Item.shootSpeed = 24f;
            Item.UseSound = SoundID.Item21;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.autoReuse = true;
            Item.channel = true;
            Item.rare = ModContent.RarityType<AncientsRarity>();
        }

        

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {           
            for (int i = 0; i < 7; i++)
            {
                int proj = Projectile.NewProjectile(player.GetSource_ItemUse(Item), position, velocity, type, damage, knockback, Main.myPlayer, ai2: i - 3);
                Main.projectile[proj].DamageType = DamageClass.Melee;
                Main.projectile[proj].localAI[1] = i * 60f / 35f; //* 60 / 5 / 7
            }
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<EventideAbyssiumBar>(), 5);
            recipe.AddIngredient(ModContent.ItemType<DreadScale>(), 5);
            recipe.AddIngredient(ItemID.Flairon, 1);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
        }
    }
}