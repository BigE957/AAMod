using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.World.Tiles
{
    public class AbyssVines_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = false;
            Main.tileSolidTop[Type] = false;
			Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = false;
            AddMapEntry(new Color(50, 0, 0));
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

        public override void PostDraw(int x, int y, SpriteBatch spriteBatch)
        {
            BaseDrawing.DrawTileTexture(spriteBatch, TextureAssets.Tile[Type].Value, x, y, true, false, false, null, AAGlobalTile.GetYamataColorDim);
        }
    }
}