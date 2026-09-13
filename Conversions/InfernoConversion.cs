using AAModClassic._Content.Inferno.World.Tiles;
using AAModClassic._Unreleased.Content.Inferno.World.Tiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Conversions
{
    public class InfernoConversion : ModBiomeConversion
    {
        public override void PostSetupContent()
        {
            TileLoader.RegisterConversion(TileID.Grass, Type, ModContent.TileType<InfernoGrass_Tile>());
            WallLoader.RegisterConversion(WallID.Grass, Type, ModContent.WallType<InfernoGrassWall_Wall>());
            WallLoader.RegisterConversion(WallID.GrassUnsafe, Type, ModContent.WallType<InfernoGrassWall_Wall>());
            
            TileLoader.RegisterConversion(TileID.Stone, Type, ModContent.TileType<Torchstone_Tile>());
            WallLoader.RegisterConversion(WallID.Stone, Type, ModContent.WallType<TorchstoneWall_Wall>());
            
            TileLoader.RegisterConversion(TileID.Sand, Type, ModContent.TileType<Torchsand_Tile>());
            TileLoader.RegisterConversion(TileID.HardenedSand, Type, ModContent.TileType<TorchsandHardened_Tile>());
            WallLoader.RegisterConversion(WallID.HardenedSand, Type, ModContent.WallType<TorchsandHardenedWall_Wall>());
            TileLoader.RegisterConversion(TileID.Sandstone, Type, ModContent.TileType<Torchsandstone_Tile>());
            WallLoader.RegisterConversion(WallID.Sandstone, Type, ModContent.WallType<TorchsandstoneWall_Wall>());
            
            TileLoader.RegisterConversion(TileID.SnowBlock, Type, ModContent.TileType<TorchAsh_Tile>());
            WallLoader.RegisterConversion(WallID.SnowWallUnsafe, Type, ModContent.WallType<TorchAshWall_Wall>());
            TileLoader.RegisterConversion(TileID.IceBlock, Type, ModContent.TileType<Torchice_Tile>());
            
            TileLoader.RegisterConversion(TileID.LivingWood, Type, ModContent.TileType<LivingRazewood_Tile>());
            TileLoader.RegisterConversion(TileID.LivingMahogany, Type, ModContent.TileType<LivingRazewood_Tile>());
            
            TileLoader.RegisterConversion(TileID.LeafBlock, Type, ModContent.TileType<LivingRazeleaves_Tile>());
            TileLoader.RegisterConversion(TileID.LivingMahoganyLeaves, Type, ModContent.TileType<LivingRazeleaves_Tile>());

            TileLoader.RegisterConversion(TileID.Vines, Type, ModContent.TileType<InfernoVines_Tile>());
            TileLoader.RegisterConversion(TileID.JungleVines, Type, ModContent.TileType<InfernoVines_Tile>());
            //TileLoader.RegisterConversion(TileID.CorruptVines, Type, ModContent.TileType<InfernoVines_Tile>());
            //TileLoader.RegisterConversion(TileID.CrimsonVines, Type, ModContent.TileType<InfernoVines_Tile>());
            //TileLoader.RegisterConversion(TileID.HallowedVines, Type, ModContent.TileType<InfernoVines_Tile>());

            TileLoader.RegisterConversion(TileID.Plants, Type, ModContent.TileType<InfernoFoliage_Tile>());
            TileLoader.RegisterConversion(TileID.JunglePlants, Type, ModContent.TileType<InfernoFoliage_Tile>());
            //TileLoader.RegisterConversion(TileID.CorruptPlants, Type, ModContent.TileType<MireFoliage_Tile>());
            //TileLoader.RegisterConversion(TileID.CrimsonPlants, Type, ModContent.TileType<MireFoliage_Tile>());
            //TileLoader.RegisterConversion(TileID.HallowedPlants, Type, ModContent.TileType<MireFoliage_Tile>());
        }
    }
}
