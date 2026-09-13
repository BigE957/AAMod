using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Weapons
{
    public class FlameVortexStaff : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Summon";
        public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Flame Vortex Staff");
            /* Tooltip.SetDefault(@"Conjures flaming spheres that increase your minion damage
Each sphere takes up 1 minion slot
You must have at least 2 open slots for the first summon"); */	
		}		

        public override void SetDefaults()
        {
            Item.width = 45;
            Item.height = 18;
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
            Item.value = Item.sellPrice(0, 20, 0, 0);
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 35;
            Item.useTime = 35;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Summon;
            Item.shoot = ModContent.ProjectileType<FlameVortexStaff_FireOrbiter>();
            Item.shootSpeed = 5;
        }

        

        public override void UseStyle(Player player, Rectangle heldItemFrame)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(ModContent.BuffType<FlameVortexStaff_Buff>(), 2, true);
			}
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            bool AnyOrbiters = AAGlobalProjectile.AnyProjectiles(ModContent.ProjectileType<FlameVortexStaff_FireOrbiter>());
            int SummonCount = 2;
            if (AnyOrbiters)
            {
                SummonCount = 1;
            }
            for (int Loops = 0; Loops < SummonCount; Loops++)
            {
                Projectile.NewProjectile(player.GetSource_ItemUse(Item), position, velocity, type, damage, knockback, Main.myPlayer, 0, 0);
            }

            return false;
        }
    }
}