using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.World.Tiles
{
    public class TorchAshWall_Wall : ModWall
    {
        public override void SetStaticDefaults()
        {
            DustType = ModContent.DustType<Dusts.AshRain>();
            AddMapEntry(new Color(25, 12, 10));
            WallID.Sets.Conversion.Snow[Type] = true;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}
