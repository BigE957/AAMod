using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Mire.___PreHardmode.Items.Weapons;
using AAModClassic._Content.Mire._PostMoonlord.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.Weapons
{
    public class Sevenshot : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Ranged";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Hydra Sevenshot");
			/* Tooltip.SetDefault(@"Fires 6 bullets and a destructive moon blast
50% chance to not consume ammo"); */
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 130;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 60;
	        Item.height = 26;
	        Item.useTime = 16;
	        Item.useAnimation = 16;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 4.5f;
	        Item.value = Item.sellPrice(0, 30, 0, 0);
	        Item.UseSound = SoundID.Item36;
	        Item.autoReuse = true;
	        Item.shoot = ProjectileID.PurificationPowder;
	        Item.shootSpeed = 20f;
	        Item.useAmmo = 97;
            Item.rare = ModContent.RarityType<AncientsRarity>();
        }

        

        public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
		    float SpeedX = velocity.X + Main.rand.Next(-25, 26) * 0.05f;
		    float SpeedY = velocity.Y + Main.rand.Next(-25, 26) * 0.05f;
		    Projectile.NewProjectile(player.GetSource_ItemUse(Item), position, velocity, ModContent.ProjectileType<Darksprayer_Moonblow>(), (int)(damage * 1.5f), knockback, player.whoAmI, 0.0f, 0.0f);
		    for (int i = 0; i <= 6; i++)
		    {
		    	float SpeedNewX = velocity.X + Main.rand.Next(-45, 46) * 0.05f;
		    	float SpeedNewY = velocity.Y + Main.rand.Next(-45, 46) * 0.05f;
		    	Projectile.NewProjectile(player.GetSource_ItemUse(Item), position.X, position.Y, SpeedNewX, SpeedNewY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
		    }
		    return false;
		}
	    
	    public override bool CanConsumeAmmo(Item ammo, Player player)
	    {
	    	if (Main.rand.Next(0, 100) <= 50)
	    		return false;
	    	return true;
	    }
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ModContent.ItemType<EventideAbyssiumBar>(), 5);
	        recipe.AddIngredient(ModContent.ItemType<DreadScale>(), 5);
            recipe.AddIngredient(ModContent.ItemType<HydraTrishot>());
            recipe.AddIngredient(ItemID.OnyxBlaster);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
	    }
	}
}