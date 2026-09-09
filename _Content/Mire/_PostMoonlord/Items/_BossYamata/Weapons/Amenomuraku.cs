using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Corruption.___PreHardmode.Items.Weapons;
using AAModClassic._Content.Mire._PostMoonlord.Items.Materials;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.Weapons   //where is located
{
    public class Amenomuraku : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Melee";
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Amenomuraku");
            /* Tooltip.SetDefault(@"Used to defeat the multi-headed monstrosities of the abyss
Inflicts Moonrazed"); */
        }

        
        public override void SetDefaults()
        {
            Item.shoot = ModContent.ProjectileType<Amenomuraku_PhantomSword>();
            Item.damage = 220;            
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;            
            Item.width = 86;              
            Item.height = 86;             
            Item.useTime = 13;          
            Item.useAnimation = 13;     
            Item.useStyle = ItemUseStyleID.Swing;        
            Item.knockBack = 3f;      
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.UseSound = SoundID.Item20;      
            Item.autoReuse = true;   
            Item.useTurn = true;
            Item.shootSpeed = 20f;
            Item.rare = ModContent.RarityType<AncientsRarity>();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

            float numberProjectiles = 1; // This defines how many projectiles to shot
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                numberProjectiles = 2 + Main.rand.Next(3);
            }

            float rotation = MathHelper.ToRadians(60);
            for (int i = 0; i < numberProjectiles; i++)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    position = new Vector2(player.position.X - ((Main.rand.Next(61) + 40) * player.direction), player.position.Y - (Main.rand.Next(91) - 40)); //this defines the distance of the projectiles form the player when the projectile spawns
                    Vector2 perturbedSpeed = Vector2.Normalize(new Vector2((Main.MouseWorld.X - position.X) + (Main.rand.Next(41) - 20), (Main.MouseWorld.Y - position.Y) + (Main.rand.Next(41) - 20))) * 15f; // This defines the projectile roatation and speed. .4f == projectile speed
                    Projectile.NewProjectile(player.GetSource_ItemUse(Item), position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
                }
            }
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EventideAbyssiumBar>(), 5);
            recipe.AddIngredient(ModContent.ItemType<DreadScale>(), 5);
            recipe.AddIngredient(ItemID.Seedler);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
        }
    }
}