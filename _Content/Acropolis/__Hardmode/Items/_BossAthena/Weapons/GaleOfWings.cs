using AAModClassic._Content.Acropolis.__Hardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Weapons
{
    public class GaleOfWings : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Magic";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gale of Wings");
            // Tooltip.SetDefault("");
        }

        public override void SetDefaults()
        {

            Item.damage = 50;                        
            Item.DamageType = DamageClass.Magic;                     
            Item.width = 24;
            Item.height = 28;
            Item.useStyle = ItemUseStyleID.Shoot;        
            Item.noMelee = true;
            Item.knockBack = 4;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Lime;
            Item.mana = 8;             
            Item.UseSound = SoundID.Item21;            
            Item.autoReuse = true;
            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.shoot = ModContent.ProjectileType<GaleOfWings_Tornado>();
            Item.shootSpeed = 9f;    
        }   

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.Center, velocity, Item.shoot, Item.damage, Item.knockBack, Main.myPlayer);
            return false;
        }

        public override void AddRecipes()  
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SpellTome, 1);
            recipe.AddIngredient(ModContent.ItemType<GoddessFeather>(), 10);
            recipe.AddTile(TileID.Bookcases);   
            recipe.Register();
        }
    }
}
