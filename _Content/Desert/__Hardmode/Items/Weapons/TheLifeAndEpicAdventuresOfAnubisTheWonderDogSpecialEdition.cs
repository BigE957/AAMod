using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Desert.__Hardmode.Items.Weapons
{
    public class TheLifeAndEpicAdventuresOfAnubisTheWonderDogSpecialEdition : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Magic";
        public override void SetDefaults()
        {
            Item.damage = 130;                        
            Item.DamageType = DamageClass.Magic;                     
            Item.width = 24;
            Item.height = 28;
            Item.useTime = 90;
            Item.useAnimation = 90;
            Item.useStyle = ItemUseStyleID.Shoot;        
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.knockBack = 8;
            Item.mana = 20;             
            Item.UseSound = SoundID.Item21;            
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<TheLifeAndEpicAdventuresOfAnubisTheWonderDogSpecialEdition_BlockHorizontal>();  
            Item.shootSpeed = 11f;
            Item.rare = ItemRarityID.Yellow;
        }   

        public override void SetStaticDefaults()
        {
          /* DisplayName.SetDefault(
@"The Life And Epic Adventures
of Anubis the Wonder Dog
~Special Edition~"); */
          /* Tooltip.SetDefault(@"Left click to summon blocks that crush at your cursor's position Horizontally
Right click for vertical blocks instead"); */
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<TheLifeAndEpicAdventuresOfAnubisTheWonderDogSpecialEdition_BlockHorizontal>()] >= 1 || player.ownedProjectileCounts[ModContent.ProjectileType<TheLifeAndEpicAdventuresOfAnubisTheWonderDogSpecialEdition_BlockVertical>()] >= 1)
            {
                return false;
            }
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float X = Main.mouseX + Main.screenPosition.X;

            float Y = Main.mouseY + Main.screenPosition.Y;
            if (player.gravDir == -1f)
            {
                Y = Main.screenPosition.Y + Main.screenHeight - Main.mouseY;
            }

            if (player.altFunctionUse != 2)
            {
                int l = Projectile.NewProjectile(player.GetSource_ItemUse(Item), new Vector2(X - 600, Y), Vector2.Zero, ModContent.ProjectileType<TheLifeAndEpicAdventuresOfAnubisTheWonderDogSpecialEdition_BlockHorizontal>(), damage, knockback, Main.myPlayer, 0, 0);
                int r = Projectile.NewProjectile(player.GetSource_ItemUse(Item), new Vector2(X + 600, Y), Vector2.Zero, ModContent.ProjectileType<TheLifeAndEpicAdventuresOfAnubisTheWonderDogSpecialEdition_BlockHorizontal>(), damage, knockback, Main.myPlayer, 1, 0);
                Main.projectile[l].ai[1] = r;
                Main.projectile[l].Center = new Vector2(X - 600, Y);
                Main.projectile[r].ai[1] = l;
                Main.projectile[r].Center = new Vector2(X + 600, Y);
            }
            else
            {
                int u = Projectile.NewProjectile(player.GetSource_ItemUse(Item), new Vector2(X, Y - 600), Vector2.Zero, ModContent.ProjectileType<TheLifeAndEpicAdventuresOfAnubisTheWonderDogSpecialEdition_BlockVertical>(), damage, knockback, Main.myPlayer, 0, 0);
                int d = Projectile.NewProjectile(player.GetSource_ItemUse(Item), new Vector2(X, Y + 600), Vector2.Zero, ModContent.ProjectileType<TheLifeAndEpicAdventuresOfAnubisTheWonderDogSpecialEdition_BlockVertical>(), damage, knockback, Main.myPlayer, 1, 0);
                Main.projectile[u].ai[1] = d;
                Main.projectile[u].Center = new Vector2(X, Y - 600);
                Main.projectile[d].ai[1] = u;
                Main.projectile[d].Center = new Vector2(X, Y + 600);
            }
            return false;
        }
    }
}
