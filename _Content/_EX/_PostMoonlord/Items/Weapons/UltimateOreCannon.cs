using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Hoard._PostMoonlord.Items._BossGreedA.Weapons;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons
{
    public class UltimateOreCannon : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Ranged";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ultimate Ore Cannon");
            /* Tooltip.SetDefault(@"Uses Any Ore as Ammunition
Certain ores have special effects when shot
Legendary Weapon
OreCannonEX"); */
        }

        public override void SetDefaults()
        {

            Item.damage = 700;
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
            int p = Projectile.NewProjectile(player.GetSource_ItemUse(Item), position, velocity, ModContent.ProjectileType<OreChunk>(), damage + Damage(), knockback, player.whoAmI, ai1: projType);
            Main.projectile[p].TriggerOreOnSpawn();
            return false;
		}

        public int Damage()
        {
            int orevalue = 0;
            if(AALuckyConfig.LuckyOre.TryGetValue(projType, out orevalue))
            {
                return (int)Math.Exp(orevalue * 0.94/100);
            }
            else if(projType == ItemID.Hellstone)
            {
                return (int)Math.Exp(500 * 0.94/100);
            }
            else
            {
                return (int)Math.Exp(100 * 0.94/100);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<OreCannon>(), 1);
            recipe.AddIngredient(ModContent.ItemType<EXSoul>(), 1);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
        }
    }
}
