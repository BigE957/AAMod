using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using AAModClassic._Content.Acropolis.__Hardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Weapons
{
    public class DivineWindStone : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Summon";
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Divine Wind Stone");
            // Tooltip.SetDefault(@"Summons an owl rune sentry");
        }

        
        public override void SetDefaults()
        {
            Item.mana = 10;
            Item.damage = 100;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shootSpeed = 14f;
            Item.shoot = ModContent.ProjectileType<DivineWindStone_OwlRune>();
            Item.width = 64;
            Item.height = 64;
            Item.UseSound = SoundID.Item78;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.noMelee = true;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.knockBack = 5f;
            Item.DamageType = DamageClass.Summon;
            Item.sentry = true;
            Item.rare = ItemRarityID.Yellow;
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
            Projectile.NewProjectile(player.GetSource_ItemUse(Item), Main.mouseX + Main.screenPosition.X, num155 * 16 - 24, 0f, 15f, num74, num76, num77, i, 0f, 0f);
            player.UpdateMaxTurrets();

            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe;
            recipe = CreateRecipe();
            recipe.AddRecipeGroup("AAModClassic:SilverBar", 25);
            recipe.AddIngredient(ModContent.ItemType<GoddessFeather>(), 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
