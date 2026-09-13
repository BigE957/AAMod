using System.Collections.Generic;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Mire._PostMoonlord.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.Weapons
{
    public class Darksprayer : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Ranged";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Darksprayer");
            /* Tooltip.SetDefault(@"'Spouts of dark, leaves its mark'
Inflicts Moonrazed"); */           
        }

        public override void SetDefaults()
        {
            Item.damage = 425;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 44;
            Item.height = 34;
            Item.useTime = 19;
            Item.useAnimation = 19;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAmmo = AmmoID.Rocket;
            Item.knockBack = 8f;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.UseSound = SoundID.Item38;      
            Item.autoReuse = true;   
            Item.shootSpeed = 20f;
            Item.shoot = ModContent.ProjectileType<Darksprayer_Moonblow>();
            Item.rare = ModContent.RarityType<AncientsRarity>();
            Item.noMelee = true;
        }

        

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-12, 0);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(player.GetSource_ItemUse(Item), position, velocity, ModContent.ProjectileType<Darksprayer_Moonblow>(), damage, knockback, player.whoAmI, 0, 1);
            return false;
        }
	
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EventideAbyssiumBar>(), 5);
            recipe.AddIngredient(ModContent.ItemType<DreadScale>(), 5);
            recipe.AddIngredient(ItemID.SnowmanCannon);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
        }
    }
}
