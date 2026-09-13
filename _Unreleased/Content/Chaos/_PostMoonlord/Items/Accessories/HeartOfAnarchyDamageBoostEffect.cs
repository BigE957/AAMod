using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.Chaos._PostMoonlord.Items.Accessories
{
    public class HeartOfAnarchyDamageBoostEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 1 - player.statLife / player.statLifeMax2;
        }
    }
}