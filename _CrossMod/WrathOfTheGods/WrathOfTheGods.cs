using Terraria.ModLoader;

namespace AAModClassic._CrossMod.WrathOfTheGods
{
    public class WrathOfTheGods : ModSystem
    {
        internal static Mod NoxusBoss = null;
        public override void Load()
        {
            if (!ModLoader.TryGetMod("NoxusBoss", out NoxusBoss))
                NoxusBoss = null;
        }

        public static bool IsEnabled => NoxusBoss != null;

        public static object Call(params object[] args) => NoxusBoss?.Call(args);
    }
}
