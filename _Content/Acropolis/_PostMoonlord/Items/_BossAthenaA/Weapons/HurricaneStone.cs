using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using AAModClassic.Globals;
using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Weapons;
using AAModClassic._Content.Acropolis._PostMoonlord.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Rarities;

namespace AAModClassic._Content.Acropolis._PostMoonlord.Items._BossAthenaA.Weapons
{
    public class HurricaneStone : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Summon";
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hurricane Stone");
            // Tooltip.SetDefault(@"Summons a charged owl rune sentry");
        }

        
        public override void SetDefaults()
        {
            Item.mana = 10;
            Item.damage = 200;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shootSpeed = 14f;
            Item.shoot = ModContent.ProjectileType<HurricaneStone_OwlRuneCharged>();
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
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
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
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<DivineWindStone>(), 1);
            recipe.AddIngredient(ModContent.ItemType<StormSphere>(), 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
