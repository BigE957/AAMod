using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Snow.___PreHardmode.Items._BossSubzeroSerpent.Weapons
{
    public class SubzeroSlasher : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Melee";
        private static int shoot;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Subzero Slasher");
            // Tooltip.SetDefault("Has a chance to shoot a subzero projectile on swing");
        }

        public override void SetDefaults()
        {
            Item.damage = 25;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 56;
            Item.height = 56;
            Item.useTime = 21;
            Item.useAnimation = 21;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 5;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.CrystalBullet;
            Item.shootSpeed = 8f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            shoot++;
            if (shoot % 2 != 0)
                return false;
            shoot = 0;
            int p = Projectile.NewProjectile(player.GetSource_ItemUse(Item), position, velocity, type, damage, knockback);
            Main.projectile[p].DamageType = DamageClass.Melee;
            return true;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Chilled, 120);
        }
    }
}