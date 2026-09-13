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
    public class Soulsplitter : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Melee";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Soulsplitter");
            // Tooltip.SetDefault("Shoots out a trio of wall-piercing returning phantom blades on swing");
        }

		public override void SetDefaults()
		{
			Item.autoReuse = true;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.knockBack = 5f;
			Item.width = 24;
			Item.height = 28;
			Item.damage = 183;
			Item.UseSound = SoundID.Item71;
			Item.shoot = ModContent.ProjectileType<Soulsplitter_Proj>();
			Item.shootSpeed = 14f;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.noMelee = true;
            Item.noUseGraphic = true;
			Item.rare = ItemRarityID.Purple;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			float numberProjectiles = 3;
			float rotation = MathHelper.ToRadians(6);
			position += Vector2.Normalize(velocity) * 45f;
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 1f;
				Projectile.NewProjectile(player.GetSource_ItemUse(Item), position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
			}
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<JackalsWrath>(), 1);
            recipe.AddIngredient(ModContent.ItemType<SoulFragment>(), 5);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
