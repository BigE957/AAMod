using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using AAModClassic.Globals;
using AAModClassic._Content.Void._PostMoonlord.Items.Materials;
using AAModClassic._Content._Misc.__Hardmode.Items.Weapons;
using AAModClassic._Content.Void.Projectiles;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Rarities;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;

namespace AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Weapons
{
    public class Neutralizer : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Ranged";
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Neutralizer");
            /* Tooltip.SetDefault(@"Left click to fire a bouncing laser that gets more powerful as it bounces off walls
Right click to fire normal arrows"); */
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        }

        public override void SetDefaults()
		{
			Item.damage = 420;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 34;
			Item.height = 58;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 0;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.UseSound = SoundID.Item75;
            Item.autoReuse = true;
            Item.useAmmo = AmmoID.Arrow;
            Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 8f;
            Item.rare = ModContent.RarityType<AncientsRarity>();
        }

        

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                Item.useTime = 10;
                Item.useAnimation = 10;
                Item.shootSpeed = 8f;
                Item.UseSound = SoundID.Item75;
            }
            else
            {
                Item.useTime = 17;
                Item.useAnimation = 17;
                Item.shootSpeed = 14;
                Item.UseSound = SoundID.Item5;
            }
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse != 2)
            {
                Projectile.NewProjectile(player.GetSource_ItemUse(Item), position, velocity, ModContent.ProjectileType<DeathBeam>(), damage, knockback, Main.myPlayer);
                
                return false;
            }
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            float num117 = 0.314159274f;
            int num118 = Main.rand.Next(2, 5);
            Vector2 vector7 = velocity;
            vector7.Normalize();
            vector7 *= 40f;
            bool flag11 = Collision.CanHit(vector2, 0, 0, vector2 + vector7, 0, 0);
            for (int num119 = 0; num119 < num118; num119++)
            {
                float num120 = num119 - (num118 - 1f) / 2f;
                Vector2 value9 = vector7.RotatedBy(num117 * num120);
                if (!flag11)
                {
                    value9 -= vector7;
                }
                int num121 = Projectile.NewProjectile(player.GetSource_ItemUse(Item), vector2.X + value9.X, vector2.Y + value9.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
                Main.projectile[num121].noDropItem = true;
            }
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-3, 0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ApocalyptitePlate>(), 5);
            recipe.AddIngredient(ModContent.ItemType<UnstableSingularity>(), 5);
            recipe.AddIngredient(ModContent.ItemType<ApollosWrath>(), 1);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
        }
    }
}
