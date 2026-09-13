using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content._Dev._PostMoonlord.Items.Weapons
{
    public class SockStaff : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Summon";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sock Puppet Staff");
            // Tooltip.SetDefault(@"Summons a sock puppet to fight with you");
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shootSpeed = 14f;
            Item.shoot = ModContent.ProjectileType<SockStaff_SockPuppet>();
            Item.damage = 130;
            Item.width = 60;
            Item.height = 56;
            Item.UseSound = SoundID.Item44;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.noMelee = true;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.knockBack = 5f;
            Item.rare = ItemRarityID.Yellow;
            Item.DamageType = DamageClass.Summon;
            Item.mana = 20;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(89, 119, 71);
                }
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int i = Main.myPlayer;
            float num74 = knockback;
            num74 = player.GetWeaponKnockback(Item, num74);
            player.itemTime = Item.useTime;
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            vector2.X = Main.mouseX + Main.screenPosition.X;
            vector2.Y = Main.mouseY + Main.screenPosition.Y;
            Projectile.NewProjectile(player.GetSource_ItemUse(Item), vector2.X, vector2.Y, 0, 0, ModContent.ProjectileType<SockStaff_SockPuppet>(), damage, num74, i, 0f, 0f);
            return false;
        }
    }
}