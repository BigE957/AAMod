using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Mire.__Hardmode.Items.Weapons;
using AAModClassic._Content.Mire._PostMoonlord.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.UI.World;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.Weapons        //We need this to basically indicate the folder where it is to be read from, so you the texture will load correctly
{
    public class AbyssalBomb : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Magic";
        public override void SetDefaults()
        {

            Item.damage = 250;                      
            Item.DamageType = DamageClass.Magic;  
            Item.width = 32;     
            Item.height = 28;    
            Item.useTime = 26; 
            Item.useAnimation = 26; 
            Item.useStyle = ItemUseStyleID.Shoot;        
            Item.noMelee = true;   
            Item.knockBack = 1; 
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.mana = 9;
            Item.UseSound = SoundID.Item20; 
            Item.autoReuse = true; 
            Item.shoot = ModContent.ProjectileType<AbyssalBomb_SoulBombSmall>();  
            Item.shootSpeed = 20f;
            Item.rare = ModContent.RarityType<AncientsRarity>();
        }

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Abyssal Bomb");
			/* Tooltip.SetDefault(@"Fires off explosive spirit bombs
Small chance to fire an awakened bomb that explodes into abyss souls"); */
		}

        

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Main.rand.NextBool(3) && WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                type = ModContent.ProjectileType<AbyssalBomb_SoulBomb>();
            Projectile.NewProjectile(player.GetSource_ItemUse(Item), position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI);
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<EventideAbyssiumBar>(), 5);
            recipe.AddIngredient(ModContent.ItemType<DreadScale>(), 5);
            recipe.AddIngredient(ModContent.ItemType<BogBomb>(), 1); 
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
        }
    }
}
