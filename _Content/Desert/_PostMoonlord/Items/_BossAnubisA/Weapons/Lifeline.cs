using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAModClassic._Content.Desert.__Hardmode.Items._BossAnubis.Weapons;
using AAModClassic._Content.Desert._PostMoonlord.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Desert._PostMoonlord.Items._BossAnubisA.Weapons
{
    public class Lifeline : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Ranged";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Lifeline");
            /* Tooltip.SetDefault(@"Shoots 2 enchanced Mummy arrows alongside with normal
Shoots ``Forsaken arrows`` burst if 2 enchanted arrows hit the target
Forsaken arrows lower enemy contact damage"); */
        }

        public override void SetDefaults()
        {
            Item.damage = 92;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 42;
            Item.height = 60;
            Item.useTime = 14;
            Item.useAnimation = 14;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.useAmmo = AmmoID.Arrow;
            Item.knockBack = 2;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shootSpeed = 25f;
            Item.rare = ItemRarityID.Purple;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			Projectile.NewProjectile(player.GetSource_ItemUse(Item), position, velocity, type, damage, knockback, player.whoAmI); 
			float numberProjectiles = 2;
			float rotation = MathHelper.ToRadians(3);
			position += Vector2.Normalize(velocity) * 45f;
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 1f;
				if (i == 0)
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(Item), position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<Lifeline_EnchancedMummyArrowD>(), damage, knockback, player.whoAmI);
				}
				if (i == 1)
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(Item), position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<Lifeline_EnchancedMummyArrow>(), damage, knockback, player.whoAmI);
				}
			}
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<NeithsString>(), 1);
            recipe.AddIngredient(ModContent.ItemType<SoulFragment>(), 5);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
