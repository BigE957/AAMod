using AAModClassic._Content.Chaos.___PreHardmode.Items._BossGripsOfChaos.Weapons;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Materials;
using AAModClassic._Content.Mire._PostMoonlord.Items.Materials;
using AAModClassic._Content.Stars._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items.Weapons
{
    public class ChaosBaton : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Summon";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chaos Baton");
            // Tooltip.SetDefault(@"Summons a discordian claw to fight with you");
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shootSpeed = 14f;
            Item.shoot = ModContent.ProjectileType<ChaosBaton_Holdout>();
            Item.damage = 100;
            Item.width = 52;
            Item.noMelee = true;
            Item.height = 52;
            Item.UseSound = SoundID.Item44;
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.noMelee = true;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.knockBack = 5f;
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
            Item.DamageType = DamageClass.Summon;
            Item.mana = 5;
            Item.noUseGraphic = true;
        }

        

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                player.MinionNPCTargetAim(true);
                player.UpdateMinionTarget();
            }
            else
            {
                int shootMe = Main.rand.Next(2);
                {
                    switch (shootMe)
                    {
                        case 0:
                            shootMe = ModContent.ProjectileType<ChaosBaton_AbyssClaw>();
                            break;
                        default:
                            shootMe = ModContent.ProjectileType<ChaosBaton_BlazeClaw>();
                            break;
                    }
                }
                player.itemTime = Item.useTime;
                Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
                vector2.X = Main.mouseX + Main.screenPosition.X;
                vector2.Y = Main.mouseY + Main.screenPosition.Y;
                Projectile.NewProjectile(player.GetSource_ItemUse(Item), vector2.X, vector2.Y, 0, 0, shootMe, damage, 5, Item.playerIndexTheItemIsReservedFor, 0f, 0f);
                return true;
            }
            return false;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ClawBaton>(), 1);
            recipe.AddIngredient(ModContent.ItemType<DaybreakIncineriteBar>(), 5);
            recipe.AddIngredient(ModContent.ItemType<EventideAbyssiumBar>(), 5);
            recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
            recipe.Register();
        }
    }
}