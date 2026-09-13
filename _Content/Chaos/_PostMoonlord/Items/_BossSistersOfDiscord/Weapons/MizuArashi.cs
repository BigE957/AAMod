using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAModClassic.Globals;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Rarities;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Weapons
{
    public class MizuArashi : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Ranged";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mizu Arashi");
            /* Tooltip.SetDefault(@"Has a 1/15 chance to shoot a Mizu spirit
Spirits deal 2x damage, pierce up to 10 enemies and go through tiles
77% not to consume arrows"); */
        }

        public override void SetDefaults()
        {

            Item.damage = 110;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 52;
            Item.height = 20;
            Item.useTime = 2;
            Item.reuseDelay = 12;
            Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.useAmmo = AmmoID.Arrow;
            Item.knockBack = 4;
            Item.value = Item.sellPrice(0, 25, 0, 0);
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shootSpeed = 8f;
        }

        
		
		public override bool CanConsumeAmmo(Item ammo, Player player)
		{
			return Main.rand.NextFloat() >= .77f;
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			if (Main.rand.NextBool(15))
			{
                Projectile.NewProjectile(player.GetSource_ItemUse(Item), position, velocity, ModContent.ProjectileType<MizuArashi_MizuSpirit>(), damage * 2, knockback, player.whoAmI);
            }
			else
			{
				Projectile.NewProjectile(player.GetSource_ItemUse(Item), position, velocity, type, damage, knockback, player.whoAmI, 0f, 0f);
			}
            return false;
        }
    }
}
