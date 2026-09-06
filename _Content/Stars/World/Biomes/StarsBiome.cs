using AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma.Awakened.Skies;
using AAModClassic._CrossMod;
using AAModClassic.Achievements;
using AAModClassic.Music;
using AAModClassic.UI.World;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars.World.Biomes
{
    public class StarsBiome : ModBiome
    {
        public override bool IsBiomeActive(Player player)
        {
            bool active = AAWorld.EquinoxAltar >= 20;
            if (active && player.whoAmI == Main.myPlayer)
                EquinoxAltarDiscovered.Condition.Complete();
            return AAWorld.Radium + AAWorld.EquinoxAltar >= 20;
        }

        public override void SpecialVisuals(Player player, bool isActive)
        {
            bool rllyActive = isActive && WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Removed);

            if (SkyManager.Instance["AAModClassic:StarsSky"] != null && rllyActive != SkyManager.Instance["AAModClassic:StarsSky"].IsActive())
            {
                if (rllyActive)
                    SkyManager.Instance.Activate("AAModClassic:StarsSky", default);
                else
                    SkyManager.Instance.Deactivate("AAModClassic:StarsSky");
            }
        }

        public override int Music => AAWorld.EquinoxAltar > 0 ? MusicManagementSystem.MusicSlots["Equinox_Altar"] : MusicManagementSystem.MusicSlots["Stars"];

        public override SceneEffectPriority Priority => SceneEffectPriority.Event;
    }

}
