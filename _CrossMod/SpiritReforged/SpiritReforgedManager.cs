using Terraria.ModLoader;

namespace AAModClassic._CrossMod.SpiritReforged
{
    public class SpiritReforgedManager : ModSystem
    {
        internal static Mod spiritReforged = null;

        public static bool IsEnabled => spiritReforged != null;

        public static object Call(params object[] args) => spiritReforged?.Call(args);

        public override void Load()
        {
            ModLoader.TryGetMod("SpiritReforged", out spiritReforged);
        }
    }
}
