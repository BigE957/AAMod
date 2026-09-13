using AAModClassic._Content.Desert.__Hardmode.Items._BossAnubis.Weapons;
using AAModClassic._Content.Desert._PostMoonlord.Items.Materials;
using AAModClassic.UI.World;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Desert._PostMoonlord.Items._BossAnubisA.Weapons
{
    public class HorusCane : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Summon";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Horus' Cane");
            // Tooltip.SetDefault(@"Summons a Horus Eye sentry");
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shootSpeed = 14f;
            Item.shoot = ModContent.ProjectileType<HorusCane_HorusEye>();
            Item.damage = 200;
            Item.width = 50;
            Item.height = 50;
            Item.UseSound = SoundID.Item44;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.noMelee = true;
            Item.knockBack = 7.5f;
            Item.DamageType = DamageClass.Summon;
            Item.mana = 30;
            Item.sentry = true;
            Item.rare = ItemRarityID.Purple;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int i = Main.myPlayer;
            int num74 = Item.shoot;
            int num76 = Item.damage;
            float num77 = Item.knockBack;
            int num154 = (int)(Main.mouseX + Main.screenPosition.X) / 16;
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
            recipe.AddIngredient(ModContent.ItemType<SentryOfTheEye>(), 1);
            recipe.AddIngredient(ModContent.ItemType<SoulFragment>(), 5);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}