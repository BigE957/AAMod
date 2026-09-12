using AAModClassic._Content._Misc.__Hardmode.Items.Consumables;
using AAModClassic.Conversions;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Unofficial.Bunny.Items
{
    public class CorruptCarrot : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Consumables";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Corrupt Carrot");
            // Tooltip.SetDefault("That... Can't be healthy.");

            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<Carrot>();
        }

        public override void SetDefaults()
        {
            Item.UseSound = SoundID.Item2;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Lime;
            Item.buffType = ModContent.BuffType<CorruptGastroenteritis_Buff>();
            Item.buffTime = 52000;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            player.AddBuff(BuffID.NightOwl, 52000);
            return base.UseItem(player);
        }
    }

    public class CorruptCarrot_Proj : ModProjectile
    {
    }

    public class CorruptGastroenteritis_Buff : ModBuff
    {
    }

    public class CorruptCarrot_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileCut[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.addTile(Type);
            RegisterItemDrop(ModContent.ItemType<CorruptCarrot>());
            DustType = DustID.Corruption;
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
            if (conversionType == BiomeConversionID.Purity || conversionType == BiomeConversionID.Purity)
                WorldGen.ConvertTile(i, j, ModContent.TileType<Carrot_Tile>());
        }
    }
}
