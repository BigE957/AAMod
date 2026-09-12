using AAModClassic._Content.Mire.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic._Content.Mire.World.Tiles;
using AAModClassic._CrossMod.SpiritReforged;
using AAModClassic._Unreleased.Content.Mire.World.Tiles;
using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Conversions
{
    public class MireConversion : ModBiomeConversion
    {
        public override void Load()
        {
            if (SpiritReforgedManager.IsEnabled)
            {
                int tileType = ModContent.TileType<MireGrass_Tile>();
                Func<int[]> tilesFunc = () => [tileType];

                (bool success, int treeType) = ((bool success, int treeType))SpiritReforgedManager.Call("AddSavannaTree", "AAModClassic/_CrossMod/SpiritReforged/Textures/", "BogwoodAcaciaTree_Tile", tilesFunc, Mod);
                if (success)
                {
                    SpiritReforgedManager.Call("RegisterConversionSet", "AcaciaTree", new Dictionary<int, int>() { { tileType, treeType } });
                    ModContent.GetModTile(treeType).RegisterItemDrop(ModContent.ItemType<Bogwood>());
                }
            }
        }

        public override void PostSetupContent()
        {
            TileLoader.RegisterConversion(TileID.Grass, Type, ModContent.TileType<MireGrass_Tile>());
            TileLoader.RegisterConversion(TileID.JungleGrass, Type, ModContent.TileType<MireGrass_Tile>());
            WallLoader.RegisterConversion(WallID.Grass, Type, ModContent.WallType<MireGrassWall_Wall>());
            
            TileLoader.RegisterConversion(TileID.Dirt, Type, TileID.Mud); //Unofficial: This just makes sense to me
            WallLoader.RegisterConversion(WallID.DirtUnsafe, Type, WallID.MudUnsafe);
            WallLoader.RegisterConversion(WallID.DirtUnsafe1, Type, WallID.MudUnsafe);
            WallLoader.RegisterConversion(WallID.DirtUnsafe2, Type, WallID.MudUnsafe);
            WallLoader.RegisterConversion(WallID.DirtUnsafe3, Type, WallID.MudUnsafe);
            WallLoader.RegisterConversion(WallID.DirtUnsafe4, Type, WallID.MudUnsafe);

            WallLoader.RegisterConversion(WallID.GrassUnsafe, Type, ModContent.WallType<MireGrassWall_Wall>());
            WallLoader.RegisterConversion(WallID.JungleUnsafe, Type, ModContent.WallType<MireGrassWall_Wall>());
            WallLoader.RegisterConversion(WallID.JungleUnsafe1, Type, ModContent.WallType<MireGrassWall_Wall>());
            WallLoader.RegisterConversion(WallID.JungleUnsafe2, Type, ModContent.WallType<MireGrassWall_Wall>());
            WallLoader.RegisterConversion(WallID.JungleUnsafe3, Type, ModContent.WallType<MireGrassWall_Wall>());
            WallLoader.RegisterConversion(WallID.JungleUnsafe4, Type, ModContent.WallType<MireGrassWall_Wall>());

            TileLoader.RegisterConversion(TileID.Vines, Type, ModContent.TileType<MireVines_Tile>());
            TileLoader.RegisterConversion(TileID.JungleVines, Type, ModContent.TileType<MireVines_Tile>());
            //TileLoader.RegisterConversion(TileID.CorruptVines, Type, ModContent.TileType<MireVines_Tile>());
            //TileLoader.RegisterConversion(TileID.CrimsonVines, Type, ModContent.TileType<MireVines_Tile>());
            //TileLoader.RegisterConversion(TileID.HallowedVines, Type, ModContent.TileType<MireVines_Tile>());

            TileLoader.RegisterConversion(TileID.Plants, Type, ModContent.TileType<MireFoliage_Tile>());
            TileLoader.RegisterConversion(TileID.JunglePlants, Type, ModContent.TileType<MireFoliage_Tile>());
            //TileLoader.RegisterConversion(TileID.CorruptPlants, Type, ModContent.TileType<MireFoliage_Tile>());
            //TileLoader.RegisterConversion(TileID.CrimsonPlants, Type, ModContent.TileType<MireFoliage_Tile>());
            //TileLoader.RegisterConversion(TileID.HallowedPlants, Type, ModContent.TileType<MireFoliage_Tile>());

            TileLoader.RegisterConversion(TileID.Stone, Type, ModContent.TileType<Depthstone_Tile>());
            WallLoader.RegisterConversion(WallID.Stone, Type, ModContent.WallType<DepthstoneWall_Wall>());
            
            TileLoader.RegisterConversion(TileID.Sand, Type, ModContent.TileType<Depthsand_Tile>());
            TileLoader.RegisterConversion(TileID.HardenedSand, Type, ModContent.TileType<DepthsandHardened_Tile>());
            WallLoader.RegisterConversion(WallID.HardenedSand, Type, ModContent.WallType<DepthsandHardenedWall_Wall>());
            TileLoader.RegisterConversion(TileID.Sandstone, Type, ModContent.TileType<Depthsandstone_Tile>());
            WallLoader.RegisterConversion(WallID.Sandstone, Type, ModContent.WallType<DepthsandstoneWall_Wall>());
            
            TileLoader.RegisterConversion(TileID.IceBlock, Type, ModContent.TileType<IndigoIce_Tile>());
            
            TileLoader.RegisterConversion(TileID.LivingWood, Type, ModContent.TileType<LivingBogwood_Tile>());
            WallLoader.RegisterConversion(WallID.LivingWood, Type, ModContent.WallType<LivingBogwoodWall_Wall>());
            WallLoader.RegisterConversion(WallID.LivingWoodUnsafe, Type, ModContent.WallType<LivingBogwoodWall_Wall>());
            TileLoader.RegisterConversion(TileID.LeafBlock, Type, ModContent.TileType<LivingBogleaf_Tile>());
            WallLoader.RegisterConversion(WallID.LivingLeaf, Type, ModContent.WallType<LivingBogleafWall_Wall>());

            TileLoader.RegisterConversion(TileID.LivingMahogany, Type, ModContent.TileType<LivingBogwood_Tile>());
            TileLoader.RegisterConversion(TileID.LivingMahoganyLeaves, Type, ModContent.TileType<LivingBogleaf_Tile>());

            if(ModLoader.TryGetMod("SpiritReforged", out var spirit))
            {
                TileLoader.RegisterConversion(spirit.Find<ModTile>("SavannaGrass").Type, Type, ModContent.TileType<MireGrass_Tile>());
                TileLoader.RegisterConversion(spirit.Find<ModTile>("SavannaGrassCorrupt").Type, Type, ModContent.TileType<MireGrass_Tile>());
                TileLoader.RegisterConversion(spirit.Find<ModTile>("SavannaGrassCrimson").Type, Type, ModContent.TileType<MireGrass_Tile>());
                TileLoader.RegisterConversion(spirit.Find<ModTile>("SavannaGrassHallow").Type, Type, ModContent.TileType<MireGrass_Tile>());

                TileLoader.RegisterConversion(spirit.Find<ModTile>("SavannaFoliage").Type, Type, ModContent.TileType<MireFoliage_Tile>());
                TileLoader.RegisterConversion(spirit.Find<ModTile>("SavannaFoliageCorrupt").Type, Type, ModContent.TileType<MireFoliage_Tile>());
                TileLoader.RegisterConversion(spirit.Find<ModTile>("SavannaFoliageCrimson").Type, Type, ModContent.TileType<MireFoliage_Tile>());
                TileLoader.RegisterConversion(spirit.Find<ModTile>("SavannaFoliageHallow").Type, Type, ModContent.TileType<MireFoliage_Tile>());

                TileLoader.RegisterConversion(spirit.Find<ModTile>("ElephantGrass").Type, Type, ModContent.TileType<ElephantGrassMire>());
                TileLoader.RegisterConversion(spirit.Find<ModTile>("ElephantGrassCorrupt").Type, Type, ModContent.TileType<ElephantGrassMire>());
                TileLoader.RegisterConversion(spirit.Find<ModTile>("ElephantGrassCrimson").Type, Type, ModContent.TileType<ElephantGrassMire>());
                TileLoader.RegisterConversion(spirit.Find<ModTile>("ElephantGrassHallow").Type, Type, ModContent.TileType<ElephantGrassMire>());

                TileLoader.RegisterConversion(spirit.Find<ModTile>("SavannaDirt").Type, Type, TileID.Mud);


                WallLoader.RegisterConversion(spirit.Find<ModWall>("SavannaDirtWall").Type, Type, WallID.MudWallEcho);
                WallLoader.RegisterConversion(spirit.Find<ModWall>("SavannaDirtWallUnsafe").Type, Type, WallID.MudUnsafe);

                TileLoader.RegisterConversion(spirit.Find<ModTile>("LivingBaobab").Type, Type, ModContent.TileType<LivingBogwood_Tile>());
                TileLoader.RegisterConversion(spirit.Find<ModTile>("LivingBaobabLeaf").Type, Type, ModContent.TileType<LivingBogleaf_Tile>());
                WallLoader.RegisterConversion(spirit.Find<ModWall>("LivingBaobabWall").Type, Type, ModContent.WallType<LivingBogwoodWall_Wall>());
                WallLoader.RegisterConversion(spirit.Find<ModWall>("LivingBaobabLeafWall").Type, Type, ModContent.WallType<LivingBogleafWall_Wall>());
            }
        }
    }
}
