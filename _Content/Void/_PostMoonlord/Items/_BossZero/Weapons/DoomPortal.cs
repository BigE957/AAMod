using AAModClassic._Content._EX._PostMoonlord.Items.Weapons;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Corruption.___PreHardmode.Items.Weapons;
using AAModClassic._Content.Mire._PostMoonlord.Items.Materials;
using AAModClassic._Content.Void.___PreHardmode.Items.Weapons;
using AAModClassic._Content.Void.__Hardmode.Items.Weapons;
using AAModClassic._Content.Void._PostMoonlord.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Weapons
{
    public class DoomPortal : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Summon";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Doom Portal");
            // Tooltip.SetDefault(@"Summons a small Doom Protocol to fight for you");
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(5, 4));
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            ItemID.Sets.AnimatesAsSoul[Type] = true;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Oblivion;
        }

        public override void SetDefaults()
        {
            Item.damage = 200;
            Item.DamageType = DamageClass.Summon;
            Item.mana = 10;
            Item.width = 24;
            Item.height = 24;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.UseSound = SoundID.Item44;
            Item.shoot = ModContent.ProjectileType<DoomPortal_D00MPR0T0C0L>();
            Item.shootSpeed = 10f;
            Item.buffType = ModContent.BuffType<DoomPortal_Buff>();
            Item.autoReuse = true;
            Item.rare = ModContent.RarityType<AncientsRarity>();
            Item.value = Item.sellPrice(0, 30, 0, 0);
        }

        
		
		public override void UseStyle(Player player, Rectangle heldItemFrame)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(Item.buffType, 3600, true);
			}
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int i = Main.myPlayer;
            float num72 = Item.shootSpeed;
            int num73 = damage;
            float num74 = knockback;
            num74 = player.GetWeaponKnockback(Item, num74);
            player.itemTime = Item.useTime;
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            float num78 = Main.mouseX + Main.screenPosition.X - vector2.X;
            float num79 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
            if (player.gravDir == -1f)
            {
                num79 = Main.screenPosition.Y + Main.screenHeight - Main.mouseY - vector2.Y;
            }
            float num80 = (float)Math.Sqrt(num78 * num78 + num79 * num79);
            float num81 = num80;
            if (float.IsNaN(num78) && float.IsNaN(num79) || num78 == 0f && num79 == 0f)
            {
                num78 = player.direction;
                num79 = 0f;
                num80 = num72;
            }
            else
            {
                num80 = num72 / num80;
            }
            num78 = 0f;
            num79 = 0f;
            vector2.X = Main.mouseX + Main.screenPosition.X;
            vector2.Y = Main.mouseY + Main.screenPosition.Y;
            Projectile.NewProjectile(player.GetSource_ItemUse(Item), vector2.X, vector2.Y, num78, num79, ModContent.ProjectileType<DoomPortal_D00MPR0T0C0L>(), num73, num74, i, 0f, 0f);
            return false;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, AAColor.Oblivion.ToVector3() * 0.55f * Main.essScale);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ApocalyptitePlate>(), 5);
            recipe.AddIngredient(ModContent.ItemType<UnstableSingularity>(), 5);
            recipe.AddIngredient(ModContent.ItemType<VoidProbeControlUnit>());
            recipe.AddIngredient(ModContent.ItemType<OverloadedDoomiteRadio>());
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
        }
    }
}