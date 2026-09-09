using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Materials;
using AAModClassic._Content.Inferno.Projectiles;
using AAModClassic._Content.Underground.__Hardmode.Items.Weapons;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Weapons   //where is located
{
    public class DragonShiv : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Melee";
        
        public override void SetStaticDefaults()
        {
            
            // DisplayName.SetDefault("Dragon Shiv");
            /* Tooltip.SetDefault(@"Slow, but hits like a celestial body
Inflicts Daybroken"); */
            
        }

        public override void SetDefaults()
        {
            Item.damage = 1200;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 42;
            Item.height = 52;
            Item.useTime = 30;
            Item.useAnimation = 30;     
            Item.useStyle = ItemUseStyleID.Thrust;
            Item.knockBack = 20f;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.UseSound = SoundID.Item20; 
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.rare = ModContent.RarityType<AncientsRarity>();
        }

        

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextFloat() < 1f)
            {
                Dust dust;
                dust = Main.dust[Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<Dusts.AkumaDust>(), 0f, 0f, 46, new Color(255, 75, 0), 1.381579f)];
                dust.noGravity = true;
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        { 
            target.AddBuff(BuffID.Daybreak, 200);
            Projectile.NewProjectile(player.GetSource_ItemUse(Item), (int)target.position.X, (int)target.position.Y, 0, 0, ModContent.ProjectileType<AkumaExplosionBlue>(), Item.damage, 20, Main.myPlayer);
        }
        
        public override void AddRecipes()  //How to craft this sword
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DaybreakIncineriteBar>(), 5);
            recipe.AddIngredient(ModContent.ItemType<CrucibleScale>(), 5);
            recipe.AddIngredient(ModContent.ItemType<TrueCopperShortsword>());
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
        }
    }
}
