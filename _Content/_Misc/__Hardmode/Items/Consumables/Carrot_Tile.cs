using AAModClassic._Unofficial.Bunny.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Content._Misc.__Hardmode.Items.Consumables
{
    public class Carrot_Tile : ModTile
	{
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileCut[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.addTile(Type);
            RegisterItemDrop(ModContent.ItemType<Carrot>());
            DustType = ModContent.DustType<Dusts.CarrotDust>();
            HitSound = SoundID.Grass;
        }

        public override bool IsTileDangerous(int i, int j, Player player)
        {
            return true;
        }

        public override bool CreateDust(int i, int j, ref int type)
        {
            return false;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = 10;
        }

        public override void Convert(int i, int j, int conversionType)
        {
            if (conversionType == BiomeConversionID.Corruption)
                WorldGen.ConvertTile(i, j, ModContent.TileType<CorruptCarrot_Tile>());
            else if (conversionType == BiomeConversionID.Crimson)
                WorldGen.ConvertTile(i, j, ModContent.TileType<CrimsonCarrot_Tile>());
        }
    }
}