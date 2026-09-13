using AAModClassic.Music;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.World.Biomes
{
    public class RisingSunPagodaBiome : ModBiome
    {
        public override bool IsBiomeActive(Player player) => AAWorld.keepTiles == 0 && AAWorld.pagodaTiles >= 1;

        public override int Music =>
            (AAWorld.downedAllAncients && !AAWorld.downedShen) ? MusicManagementSystem.MusicSlots["Chaos_PreShen"] :
            (NPC.downedMoonlord && Main.dayTime) ? MusicManagementSystem.MusicSlots["Inferno_Pagoda"] : -1;

        public override SceneEffectPriority Priority => (NPC.downedMoonlord && Main.dayTime) ? SceneEffectPriority.Environment : SceneEffectPriority.None;

        public override float GetWeight(Player player) => 0f;
    }
}