using System.Collections.Generic;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Weapons   //where is located
{
    public class ReignOfFire : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Melee";
        
        public override void SetStaticDefaults()
        {
            
            // DisplayName.SetDefault("Reign of Fire");
            /* Tooltip.SetDefault(@"Rains fire and fury upon your foes
Inflicts Daybroken"); */
        }

        
        public override void SetDefaults()
        {
            Item.damage = 380;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 86;
            Item.height = 86;
            Item.useTime = 60;
            Item.useAnimation = 60;     
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6.5f;
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

		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			if (player.whoAmI == Main.myPlayer && Main.rand.NextBool(10))
			{
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
				Projectile.NewProjectile(player.GetSource_ItemUse(Item), vector2.X, vector2.Y, speedX5, speedY6, ModContent.ProjectileType<ReignOfFire_Meteor>(), Item.damage/2, Item.knockBack, Main.myPlayer, Main.rand.Next(1, 6));
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
            recipe.AddIngredient(ModContent.ItemType<DaybreakIncineriteBar>(), 5);
            recipe.AddIngredient(ModContent.ItemType<CrucibleScale>(), 5);
            recipe.AddIngredient(ItemID.StarWrath);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
        }
    }
}
