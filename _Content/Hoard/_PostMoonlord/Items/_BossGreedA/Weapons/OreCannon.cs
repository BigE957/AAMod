using AAModClassic._Content.Hoard.__Hardmode.Items._BossGreed.Weapons;
using AAModClassic._Content.Hoard._PostMoonlord.Items.Materials;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Hoard._PostMoonlord.Items._BossGreedA.Weapons
{
    public class OreCannon : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Ranged";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ore Cannon");
            /* Tooltip.SetDefault(@"Uses Some Ores as Ammunition
Certain ores have special effects when shot"); */
        }

        public override void SetDefaults()
        {

            Item.damage = 300;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 50;
            Item.height = 20;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 0;
			Item.shoot = ProjectileID.PurificationPowder;
            Item.UseSound = SoundID.Item14;
            Item.shootSpeed = 14f;
            Item.expert = true; 
            Item.autoReuse = true;
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, -3);
        }

        public int projType = -1;

        public override bool CanUseItem(Player player)
        {
            int itemIndex = -1;
            if (player.itemAnimation == 0)
            {
                if (BasePlayer.HasItem(player, [.. OreCannonSystem.OreData.Keys], ref itemIndex, default, false, false))
                {
 					Item itemFired = player.inventory[itemIndex];
 					BasePlayer.ReduceSlot(player, itemIndex, 1);
                    projType = itemFired.type;
                    return true;
 				}
 			}
            return false;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
 		{
            int p = Projectile.NewProjectile(player.GetSource_ItemUse(Item), position, velocity, ModContent.ProjectileType<OreChunk>(), damage, knockback, player.whoAmI, 0, projType);
            Main.projectile[p].TriggerOreOnSpawn();
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<GildedGlock>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CovetiteBar>(), 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
