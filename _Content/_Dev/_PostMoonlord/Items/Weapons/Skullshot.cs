using AAModClassic._Content.Mire.___PreHardmode.Items.Weapons;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev._PostMoonlord.Items.Weapons
{
    public class Skullshot : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Ranged";
        public override Color GlowmaskDrawColor => new Color(255, 128, 0);

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Super Skullshot");
            /* Tooltip.SetDefault(@"fires a massive spread of bullets at your foes
Right click to fire spinning bones at your foe
Uses Bullets and Bones as ammo
'I have an irrational hatred for gods`
-Gibs"); */

            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<GibsFemur>();
            ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<GibsFemur>()] = Type;
        }

        public override void SetDefaults()
        {
            Item.autoReuse = true;
            Item.knockBack = 7f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 34;
            Item.useTime = 34;
            Item.width = 46;
            Item.height = 20;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.useAmmo = AmmoID.Bullet;
            Item.UseSound = SoundID.Item36;
            Item.damage = 90;
            Item.shootSpeed = 6f;
            Item.noMelee = true;
            Item.value = 100000;
            Item.rare = ItemRarityID.Cyan;
            Item.DamageType = DamageClass.Ranged;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(255, 128, 0);
                }
            }
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useAnimation = 15;
                Item.useTime = 5;
                Item.reuseDelay = 17;
                Item.useAmmo = ItemID.Bone;
                Item.damage = 375;
            }
            else
            {
                Item.useAnimation = 28;
                Item.useTime = 28;
                Item.reuseDelay = 0;
                Item.useAmmo = AmmoID.Bullet;
                Item.damage = 95;
            }
            return base.CanUseItem(player);
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return !(player.itemAnimation < Item.useAnimation - 2) || player.altFunctionUse != 2;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse != 2)
            {
                float spread = Main.rand.Next(20, 30) * 0.0174f;
                float baseSpeed = (float)Math.Sqrt((velocity.X * velocity.X) + (velocity.Y * velocity.Y));
                double startAngle = Math.Atan2(velocity.X, velocity.Y) - .1d;
                double deltaAngle = spread / 6f;
                double offsetAngle;
                for (int i = 0; i < Main.rand.Next(5, 11); i++)
                {
                    float randomSpeed = baseSpeed + Main.rand.NextFloat() * 1.5f;
                    offsetAngle = startAngle + (deltaAngle * i);
                    Projectile.NewProjectile(player.GetSource_ItemUse(Item), position.X, position.Y, randomSpeed * (float)Math.Sin(offsetAngle), randomSpeed * (float)Math.Cos(offsetAngle), type, damage, knockback, Main.myPlayer);
                }
            }
            else
            {
                int proj = Projectile.NewProjectile(player.GetSource_ItemUse(Item), position, velocity, ProjectileID.BoneGloveProj, damage, knockback, Main.myPlayer, 0f, 0f);
                Main.projectile[proj].DamageType = DamageClass.Ranged;
            }
            return false;
        }
    }
}
