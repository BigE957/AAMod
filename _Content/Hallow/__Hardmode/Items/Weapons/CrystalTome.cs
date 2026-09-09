using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hallow.__Hardmode.Items.Weapons
{
    public class CrystalTome : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Magic";
        public override void SetDefaults()
        {

            Item.damage = 33;                   
            Item.DamageType = DamageClass.Magic;   
            Item.width = 24;
            Item.height = 28;
            Item.useTime = 14;     
            Item.useAnimation = 14; 
            Item.useStyle = ItemUseStyleID.Shoot;      
            Item.noMelee = true;    
            Item.knockBack = 1;  
            Item.value = Item.sellPrice(0, 5, 0, 0); 
            Item.rare = ItemRarityID.Lime;   
            Item.mana = 9;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true; 
            Item.shoot = ProjectileID.CrystalBullet;    
            Item.shootSpeed = 8f;    
        }

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Crystal Tome");
			// Tooltip.SetDefault("Casts crystals that shatter into pieces");
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
              int numberProjectiles = 1 + Main.rand.Next(3); 
              for (int i = 0; i < numberProjectiles; i++)
              {
                  Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(20)); 
                  int p = Projectile.NewProjectile(player.GetSource_ItemUse(Item), position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
                  Main.projectile[p].DamageType = DamageClass.Magic;
              }
              return false;
        }  

		public override void AddRecipes()  
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PixieDust, 18);   
			recipe.AddIngredient(ItemID.CrystalShard, 16);
            recipe.AddIngredient(ItemID.CrystalStorm, 1);
            recipe.AddTile(TileID.Bookcases);   
            recipe.Register();
        }
    }

    public class CrystalTomeGlobalProjectile : GlobalProjectile
    {
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if(projectile.type == ProjectileID.CrystalShard && source is EntitySource_Parent { Entity: Projectile proj })
            {
                if (proj.DamageType == DamageClass.Magic)
                    projectile.DamageType = DamageClass.Magic;
            }
        }
    }
}
