using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Tiles.Decoration
{
    public class FulgurGlass_Tile : ModTile
    {
        //public Texture2D glowTex;
        //public bool glow = true;
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = false;
            TileID.Sets.DrawsWalls[Type] = true;
            TileID.Sets.ChecksForMerge[Type] = true;
            HitSound = SoundID.Shatter;
            DustType = ModContent.DustType<FulguriteDust>();
            AddMapEntry(new Color(90, 20, 120));
			MinPick = 200;
        }
    }
}