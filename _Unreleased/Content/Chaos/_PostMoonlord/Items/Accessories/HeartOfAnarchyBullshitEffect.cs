using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;

namespace AAModClassic._Unreleased.Content.Chaos._PostMoonlord.Items.Accessories
{
    public class HeartOfAnarchyBullshitEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            if (player.statLife > player.statLifeMax * (2 / 3))
            {
                player.moveSpeed += 1f;
                player.manaRegenBonus += 6;
            }
        }
    }
}