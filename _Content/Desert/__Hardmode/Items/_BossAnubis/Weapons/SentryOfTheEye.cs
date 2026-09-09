using AAModClassic._Content.Desert.___PreHardmode.Items._BossDesertDjinn.Weapons;
using AAModClassic._Content.Desert.__Hardmode.Items.Materials;
using AAModClassic.UI.World;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Desert.__Hardmode.Items._BossAnubis.Weapons
{
    public class SentryOfTheEye : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Summon";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sentry of the Eye");
            // Tooltip.SetDefault(@"Summons an eye sentry");
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shootSpeed = 14f;
            Item.shoot = ModContent.ProjectileType<SentryOfTheEye_TheEye>();
            Item.damage = 65;
            Item.width = 50;
            Item.height = 50;
            Item.UseSound = SoundID.Item44;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.noMelee = true;
            Item.value = Item.sellPrice(0, 0, 27, 0);
            Item.knockBack = 7.5f;
            Item.rare = ItemRarityID.LightPurple;
            Item.DamageType = DamageClass.Summon;
            Item.mana = 30;
            Item.sentry = true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int i = Main.myPlayer;
            int num74 = Item.shoot;
            int num76 = Item.damage;
            float num77 = Item.knockBack;
            int num155 = (int)(Main.mouseY + Main.screenPosition.Y) / 16;
            if (player.gravDir == -1f)
            {
                num155 = (int)(Main.screenPosition.Y + Main.screenHeight - Main.mouseY) / 16;
            }
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                Projectile.NewProjectile(player.GetSource_ItemUse(Item), Main.mouseX + Main.screenPosition.X, Main.mouseY + Main.screenPosition.Y, 0f, 0f, num74, num76, num77, i, 0f, 0f);
                player.UpdateMaxTurrets();
            }
            else
            {
                if (player.ownedProjectileCounts[num74] < player.maxTurrets)
                {
                    Projectile.NewProjectile(player.GetSource_ItemUse(Item), Main.mouseX + Main.screenPosition.X, Main.mouseY + Main.screenPosition.Y, 0f, 0f, num74, num76, num77, i, 0f, 0f);
                }
                if (player.ownedProjectileCounts[num74] == player.maxTurrets)
                {
                    foreach (Projectile p in Main.ActiveProjectiles)
                    {
                        if (p.type == num74)
                        {
                            p.Kill();
                            break;
                        }
                    }
                    Projectile.NewProjectile(player.GetSource_ItemUse(Item), Main.mouseX + Main.screenPosition.X, num155 * 16 - 24, 0f, 0f, num74, num76, num77, i, 0f, 0f);
                }
            }

                return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SandScepter>(), 1);
            recipe.AddIngredient(ModContent.ItemType<ForsakenFragment>(), 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}