using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Weapons;
using AAModClassic._Content.Stars._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons   //where is located
{
    public class DraconianFury : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Melee";
        
        public override void SetStaticDefaults()
        {
            
            // DisplayName.SetDefault("Draconian Fury");
            /* Tooltip.SetDefault(@"Rains fire and fury upon your foes
Inflicts Daybroken
Reign of Fire EX"); */
        }

        
        public override void SetDefaults()
        {
            Item.damage = 850;            
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;            
            Item.width = 86;              
            Item.height = 86;             
            Item.useTime = 60;          
            Item.useAnimation = 60;     
            Item.useStyle = ItemUseStyleID.Swing;        
            Item.knockBack = 6.5f;      
            Item.value = Item.sellPrice(3, 0, 0, 0);
			Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;   
            Item.expert = true;
            Item.rare = ItemRarityID.Cyan;
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

		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			if (player.whoAmI == Main.myPlayer && Main.rand.NextBool(8))
			{
				SoundEngine.PlaySound(SoundID.Item124, player.Center);
				SoundEngine.PlaySound(SoundID.Item124, player.Center);
				Vector2 vector12 = new Vector2(0,0);
				vector12 = new Vector2(Main.mouseX + Main.screenPosition.X, player.Center.Y);
				Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
				float num75 = 20f;
				float num119 = vector12.Y;
				if (num119 > player.Center.Y - 200f)
				{
					num119 = player.Center.Y - 200f;
				}
				vector2 = player.Center + new Vector2(-(float)Main.rand.Next(0, 401) * player.direction, -600f);
				vector2.Y -= 100;
				Vector2 vector13 = vector12 - vector2;
				if (vector13.Y < 0f)
				{
					vector13.Y *= -1f;
				}
				if (vector13.Y < 20f)
				{
					vector13.Y = 20f;
				}
				vector13.Normalize();
				vector13 *= num75;
				float num82 = vector13.X;
				float num83 = vector13.Y;
				float speedX5 = num82;
				float speedY6 = num83 + Main.rand.Next(-30, 30) * 0.02f;
				Projectile.NewProjectile(player.GetSource_ItemUse(Item), vector2.X, vector2.Y, speedX5, speedY6, ModContent.ProjectileType<DraconianFury_Meteor>(), Item.damage/2, Item.knockBack, Main.myPlayer, Main.rand.Next(1, 6));
			}
			return base.UseItem(player);
		}

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Daybreak, 600);
        }
        
        
        public override void AddRecipes()  //How to craft this sword
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ReignOfFire>());
            recipe.AddIngredient(ModContent.ItemType<EXSoul>());
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
        }
    }
}
