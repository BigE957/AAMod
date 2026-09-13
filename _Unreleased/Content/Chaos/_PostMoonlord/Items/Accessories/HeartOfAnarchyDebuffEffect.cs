using AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Accessories;
using AAModClassic._Content.Inferno.Buffs;
using AAModClassic._Content.Mire.Buffs;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.Chaos._PostMoonlord.Items.Accessories
{
    public class HeartOfAnarchyDebuffEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<HeartOfPassionDebuffPlayer>().effect = true;
        }
    }

    public class HeartOfAnarchyDebuffPlayer : EquipmentEffectPlayer
    {
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (effect)
            {
                if (Player.statLife > Player.statLifeMax / 3)
                {
                    target.AddBuff(ModContent.BuffType<DragonFire_Buff>(), 600);
                    target.AddBuff(ModContent.BuffType<HydraToxin_Buff>(), 600);
                }
                else if (Player.statLife < Player.statLifeMax / 3)
                {
                    target.AddBuff(BuffID.Daybreak, 600);
                    target.AddBuff(ModContent.BuffType<Moonraze_Buff>(), 600);
                }
            }
        }
    }
}