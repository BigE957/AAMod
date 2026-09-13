using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.World.Tiles
{
    public class AbyssGrass_Tile : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
			Main.tileBlendAll[Type] = true;
            
            TileID.Sets.Grass[Type] = true;
            TileUtils.Merge(Type, TileID.Mud);

            TileID.Sets.NeedsGrassFraming[Type] = true;
            TileID.Sets.JungleSpecial[Type] = true;
            DustType = ModContent.DustType<Dusts.YamataADust>();
			AddMapEntry(new Color(100, 0, 30));
		}

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return false;
        }

        public override bool CanReplace(int i, int j, int tileTypeBeingPlaced) => false;

        public override bool CanExplode(int i, int j)
        {
            return false;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)   //light colors
        {
            r = 0.51f;
            g = 0;
            b = 0f;
        }
	}
}