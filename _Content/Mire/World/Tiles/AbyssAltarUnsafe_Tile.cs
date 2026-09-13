using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using AAModClassic.Dusts;
using System.Collections.Generic;
using AAModClassic._Content.Chaos.__Hardmode.NPCs;
using AAModClassic.UI.World;

namespace AAModClassic._Content.Mire.World.Tiles
{
    public class AbyssAltarUnsafe_Tile : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = false;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.addTile(Type);
            Main.tileHammer[Type] = true;
			TileID.Sets.DisableSmartCursor[Type] = true;
			LocalizedText name = CreateMapEntryName();
			// name.SetDefault("Abyss Altar");
            DustType = ModContent.DustType<AbyssiumDust>();
            AddMapEntry(new Color(0, 0 ,100), name);
            AdjTiles = new int[] { TileID.DemonAltar };
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            Player player = Main.LocalPlayer;
            if (!Main.hardMode || player.HeldItem.hammer < 80)
            {
                if (blockDamaged == true)
                {
                    DamagePlayer(player);
                    blockDamaged = false;
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        public override bool CanReplace(int i, int j, int tileTypeBeingPlaced) => false;

        public override void ModifyLight(int x, int y, ref float r, ref float g, ref float b)
        {
            r = 0;
            g = 0.1f;
            b = 0.25f;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            HashSet<int> wraithIndexes = [];
            bool nonMPClient = Main.netMode != NetmodeID.MultiplayerClient;
            bool unofficial = WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial);

            if (nonMPClient && unofficial)
                foreach (NPC n in Main.ActiveNPCs)
                    if (n.type == NPCID.Wraith)
                        wraithIndexes.Add(n.whoAmI);

            WorldGen.SmashAltar(i, j);

            if (nonMPClient && unofficial)
                foreach (NPC n in Main.ActiveNPCs)
                    if (n.type == NPCID.Wraith && !wraithIndexes.Contains(n.whoAmI))
                        n.Transform(ModContent.NPCType<ChaosDragon>());
        }

        public static void DamagePlayer (Player player)
        {
            player.statLife -= player.statLifeMax / 10;
        }
	}
}