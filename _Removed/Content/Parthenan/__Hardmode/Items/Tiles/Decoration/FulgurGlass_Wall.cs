using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Tiles.Decoration
{
	public class FulgurGlass_Wall : ModWall
	{
        //public Texture2D glowTex;
		//public bool glow = true;

		public override void SetStaticDefaults()
		{
            Main.wallHouse[Type] = true;
            HitSound = SoundID.Shatter;
            Main.wallHouse[Type] = true;
            Main.wallLight[Type] = true;
            WallID.Sets.Transparent[Type] = true;
            AddMapEntry(new Color(40, 0, 50));
		}
    }
}