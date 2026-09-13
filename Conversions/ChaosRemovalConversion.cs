using AAModClassic._Content.Inferno.World.Tiles;
using AAModClassic._Content.Mire.World.Tiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Conversions
{
    public class ChaosRemovalConversion : ModBiomeConversion
    {
        public override void PostSetupContent()
        {
            //Tiles
            TileLoader.RegisterConversion(ModContent.TileType<InfernoGrass_Tile>(), Type, TileID.Grass);
            //TileLoader.RegisterConversion(ModContent.TileType<DoomGrass_Tile>(), Type, TileID.Grass); //Was a part of order solution, but seems unintentional, especially with the lack of doomstone

            TileLoader.RegisterConversion(ModContent.TileType<Torchstone_Tile>(), Type, TileID.Stone);
            TileLoader.RegisterConversion(ModContent.TileType<Depthstone_Tile>(), Type, TileID.Stone);

            TileLoader.RegisterConversion(ModContent.TileType<MireGrass_Tile>(), Type, TileID.JungleGrass);

            TileLoader.RegisterConversion(ModContent.TileType<TorchAsh_Tile>(), Type, TileID.SnowBlock);

            TileLoader.RegisterConversion(ModContent.TileType<Torchsand_Tile>(), Type, TileID.Sand);
            TileLoader.RegisterConversion(ModContent.TileType<Depthsand_Tile>(), Type, TileID.Sand);

            TileLoader.RegisterConversion(ModContent.TileType<TorchsandHardened_Tile>(), Type, TileID.HardenedSand);
            TileLoader.RegisterConversion(ModContent.TileType<DepthsandHardened_Tile>(), Type, TileID.HardenedSand);

            TileLoader.RegisterConversion(ModContent.TileType<Torchsandstone_Tile>(), Type, TileID.Sandstone);
            TileLoader.RegisterConversion(ModContent.TileType<Depthsandstone_Tile>(), Type, TileID.Sandstone);

            TileLoader.RegisterConversion(ModContent.TileType<Torchice_Tile>(), Type, TileID.IceBlock);
            TileLoader.RegisterConversion(ModContent.TileType<IndigoIce_Tile>(), Type, TileID.IceBlock);

            TileLoader.RegisterConversion(ModContent.TileType<InfernoVines_Tile>(), Type, TileID.Vines);
            TileLoader.RegisterConversion(ModContent.TileType<MireVines_Tile>(), Type, TileID.Vines);

            //Walls
            WallLoader.RegisterConversion(ModContent.WallType<TorchstoneWall_Wall>(), Type, WallID.Stone);
            WallLoader.RegisterConversion(ModContent.WallType<DepthstoneWall_Wall>(), Type, WallID.Stone);

            WallLoader.RegisterConversion(ModContent.WallType<InfernoGrassWall_Wall>(), Type, WallID.GrassUnsafe);
            WallLoader.RegisterConversion(ModContent.WallType<MireGrassWall_Wall>(), Type, WallID.JungleUnsafe);

            WallLoader.RegisterConversion(ModContent.WallType<TorchsandHardenedWall_Wall>(), Type, WallID.HardenedSand);
            WallLoader.RegisterConversion(ModContent.WallType<DepthsandHardenedWall_Wall>(), Type, WallID.HardenedSand);

            WallLoader.RegisterConversion(ModContent.WallType<TorchsandstoneWall_Wall>(), Type, WallID.Sandstone);
            WallLoader.RegisterConversion(ModContent.WallType<DepthsandstoneWall_Wall>(), Type, WallID.Sandstone);

            WallLoader.RegisterConversion(ModContent.WallType<LivingBogwoodWall_Wall>(), Type, WallID.LivingWood);

            WallLoader.RegisterConversion(ModContent.WallType<TorchAshWall_Wall>(), Type, WallID.SnowWallUnsafe);
        }
    }
}
