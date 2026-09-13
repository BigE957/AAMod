using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev._PostMoonlord.Items.Weapons
{
    public class UmbralReaper : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Magic";
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Umbral Reaper");
            // Tooltip.SetDefault("Left clicking shoots homing spears \n" + "Right clicking shoots a wave of void energy \n" + "'I never touched Valkyrie' \n'" + "-CMD");
		}

		public override void SetDefaults()
		{
			Item.damage = 130;
            Item.mana = 8;
			Item.DamageType = DamageClass.Magic;
			Item.width = 72;
			Item.height = 72;
			Item.useTime = 26;
			Item.useAnimation = 26;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = 100000;
			Item.rare = ItemRarityID.Cyan;
			Item.UseSound = SoundID.Item43;
			Item.autoReuse = true;
            Item.noMelee = true;
            Item.shootSpeed = 12f;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(55, 20, 122);
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
                Item.staff[Item.type] = false;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.shoot = ModContent.ProjectileType<UmbralReaper_VoidWave>();
            }
            else
            {
                Item.staff[Item.type] = true;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.shoot = ModContent.ProjectileType<UmbralReaper_VoidSpear>();
            }
            return base.CanUseItem(player);
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                return true;
            }
            float spread = 20f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt((velocity.X * velocity.X) + (velocity.Y * velocity.Y));
            double startAngle = Math.Atan2(velocity.X, velocity.Y) - .1d;
            double deltaAngle = spread / 6f;
            double offsetAngle;
            for (int i = 0; i < 3; i++)
            {
                offsetAngle = startAngle + (deltaAngle * i);
                Projectile.NewProjectile(player.GetSource_ItemUse(Item), position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), type, damage, knockback, Main.myPlayer);
            }
            return false;
        }
    }
}