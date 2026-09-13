using AAModClassic._Content.Mire.World.Tiles;
using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using SpiritReforged.Common.TileCommon;
using SpiritReforged.Common.TileCommon.Conversion;
using SpiritReforged.Content.Savanna.Tiles;
using System.Collections.Generic;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._CrossMod.SpiritReforged
{
    [DrawOrder(DrawOrderAttribute.Layer.NonSolid, DrawOrderAttribute.Layer.OverPlayers)]
    public class ElephantGrassMire : ElephantGrass
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.HasMod("SpiritReforged");

        public override void Load()
        {
            base.Load();

            SpiritReforgedManager.Call("RegisterConversionSet", "ElephantGrass", new Dictionary<int, int>() { { ModContent.TileType<MireGrass_Tile>(), Type } });
        }

        public override void PreAddObjectData()
        {
            TileObjectData.newTile.AnchorValidTiles = [ModContent.TileType<MireGrass_Tile>()];

            AddMapEntry(new Color(0, 32, 137));
            DustType = ModContent.DustType<MireDust>();
        }
    }
}
