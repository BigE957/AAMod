using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic._Content.Mire.World.Tiles;
using AAModClassic._CrossMod;
using AAModClassic.Dusts;
using AAModClassic.UI.World;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Unreleased.Content.Mire.World.Tiles
{
    //TODO: is this accurate to weeds in 1.4? can we make this support flower boots?
    [LegacyName("Darkshroom_Tile", "BlackLotus_Tile")]
    public class MireFoliage_Tile : ModTile
    {
        public static int ModernMireGrassTileID { get; private set; } = -1;

        public override void SetStaticDefaults()
        {
            Main.tileLighted[Type] = true;
            Main.tileCut[Type] = true;
            Main.tileSolid[Type] = false;
            Main.tileNoAttach[Type] = true;
            Main.tileNoFail[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileWaterDeath[Type] = true;
            Main.tileFrameImportant[Type] = true;

            TileID.Sets.ReplaceTileBreakUp[Type] = true;
            TileID.Sets.SwaysInWindBasic[Type] = true;
            TileID.Sets.IgnoredByGrowingSaplings[Type] = true;

            TileMaterials.SetForTileId(Type, TileMaterials._materialsByName["Plant"]);

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinateHeights = new int[]
            {
                20
            };
            TileObjectData.newTile.Style = 0;
            TileObjectData.newTile.CoordinatePadding = 2;

            if (!ContentReplacementSystem.NeedToReplaceContent)
            {
                TileObjectData.newTile.AnchorValidTiles = new int[]
                {
                    ModContent.TileType<MireGrass_Tile>(),
                    ModContent.TileType<DepthMoss_Tile>()
                };
            }
            else
            {
                ModernMireGrassTileID = ContentReplacementSystem.NewAA.Find<ModTile>("MireGrass").Type;

                TileObjectData.newTile.AnchorValidTiles = new int[]
                {
                    ModContent.TileType<MireGrass_Tile>(),
                    ModContent.TileType<DepthMoss_Tile>(),
                    ModernMireGrassTileID
                };
            }

            TileObjectData.addTile(Type);

            DustType = ModContent.DustType<BogwoodDust>();
            HitSound = SoundID.Grass;
            AddMapEntry(new Color(0, 32, 137));

            base.SetStaticDefaults();
        }

        public override void EmitParticles(int i, int j, Tile tile, short tileFrameX, short tileFrameY, Color tileLight, bool visible)
        {
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) && tileFrameX == 144 && Main.rand.Next(60) == 0)
            {
                int num37 = Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, ModContent.DustType<MireSporeDust>(), 0f, 0f, 250, default, 0.4f);
                Main.dust[num37].fadeIn = 0.7f;
            }
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) && Main.tile[i, j].TileFrameX == 144)
            {
                float num17 = 1f + (270 - Main.mouseTextColor) / 400f;
                float num18 = 0.8f - (270 - Main.mouseTextColor) / 400f;
                r = 0.82f * num18;
                g = 0.21f * num17;
                b = 0.72f * num18;
            }
        }

        public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
        {
            Tile tileBelow = Framing.GetTileSafely(i, j + 1);
            int type = -1;

            if (tileBelow.HasTile)
            {
                type = tileBelow.TileType;
            }

            if (type == ModContent.TileType<MireGrass_Tile>() || type == ModContent.TileType<DepthMoss_Tile>())
            {
                return true;
            }

            if (type == ModernMireGrassTileID)
            {
                if (Main.tile[i, j].TileFrameX != 162)
                {
                    WorldGen.KillTile(i, j, noItem: true);
                    return true;
                }
                return true;
            }

            WorldGen.KillTile(i, j);

            return true;
        }

        public override IEnumerable<Item> GetItemDrops(int i, int j)
        {
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                if (Main.tile[i, j].TileFrameX == 144)
                    yield return new Item(ItemID.JungleSpores, Main.rand.Next(1, 3));
                else if (Main.tile[i, j].TileFrameX == 414)
                    yield return new Item(ModContent.ItemType<LunarMushroom>());
                else if (Main.tile[i, j].TileFrameX == 162)
                    yield return new Item(ModContent.ItemType<BlackLotus>());
            }

            Vector2 worldPosition = new Vector2(i, j).ToWorldCoordinates();
            Player nearestPlayer = Main.player[Player.FindClosest(worldPosition, 16, 16)];
            if (nearestPlayer.active)
            {
                if (nearestPlayer.HeldItem.type == ItemID.Sickle)
                    yield return new Item(ItemID.Hay, Main.rand.Next(1, 2 + 1));
            }
        }

        public override bool IsTileBiomeSightable(int i, int j, ref Color sightColor)
        {
            sightColor = Color.BlueViolet;
            return true;
        }
    }

    public class MireFoliageGlobalTile : GlobalTile
    {
        public override void RandomUpdate(int i, int j, int type)
        {
            base.RandomUpdate(i, j, type);

            int modernGrassType = MireFoliage_Tile.ModernMireGrassTileID;

            if (ContentReplacementSystem.NeedToReplaceContent && modernGrassType != -1 && type == modernGrassType)
                TryGrowOnModernMireGrass(i, j);
            else if (type == ModContent.TileType<MireGrass_Tile>())
                TryGrowOnMireGrass(i, j);
            else if (type == ModContent.TileType<DepthMoss_Tile>())
                TryGrowOnDepthMoss(i, j);
        }

        private static void TryGrowOnModernMireGrass(int i, int j)
        {
            if (Framing.GetTileSafely(i, j - 1).HasTile)
                return;

            if (Main.rand.NextBool(10000))
            {
                TryPlaceFoliage(i, j - 1, style: 9, mute: true); // Black Lotus
            }
        }

        private static void TryGrowOnMireGrass(int i, int j)
        {
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) && !Framing.GetTileSafely(i, j - 1).HasTile)
            {
                Tile tile = Main.tile[i, j];
                Tile tileAbove = Main.tile[i, j - 1];

                // short grass
                if (WorldGen.genRand.NextBool(7))
                {
                    PlaceMireFoliageLikeJungleGrass(i, j);
                    if (tile.HasTile)
                    {
                        tileAbove.CopyPaintAndCoating(tile);
                    }
                    if (Main.netMode == NetmodeID.Server && tileAbove.HasTile)
                    {
                        NetMessage.SendTileSquare(-1, i, j);
                    }
                }
            }
            else if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased) && !Framing.GetTileSafely(i, j - 1).HasTile && Main.rand.NextBool(40))
            {
                int style = Main.rand.Next(23);
                if (style == 9) // dont be the lotus
                    style = 7;
                TryPlaceFoliage(i, j - 1, style, mute: false);
            }

            if (!WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                if (!Framing.GetTileSafely(i, j - 1).HasTile && Main.rand.NextBool(1500))
                {
                    TryPlaceFoliage(i, j - 1, style: 23, mute: true); // mushroom
                }

                if (!Framing.GetTileSafely(i, j - 1).HasTile && Main.rand.NextBool(10000))
                {
                    TryPlaceFoliage(i, j - 1, style: 9, mute: true); // black lotus
                }
            }
        }

        public static void PlaceMireFoliageLikeJungleGrass(int i, int j)
        {
            bool flag = (double)j > Main.rockLayer;
            if (!Framing.GetTileSafely(i, j - 1).HasTile)
            {
                /*
                 * mire thorns.................................
                if (Main.rand.Next(16) == 0 && (double)j > Main.worldSurface)
                {
                    tile.HasTile = true;
                    tile.type = 69;
                    SquareTileFrame(i, j);

                }
                else
                */
                if (!Main.dayTime && (Main.rand.NextBool(50) || WorldGen.genRand.NextBool(40))) // yes this is the vanilla logic
                {
                    int style = 24; // mushroom
                    if (WorldGen.PlaceObject(i, j - 1, ModContent.TileType<MireFoliage_Tile>(), true, style))
                        NetMessage.SendObjectPlacement(-1, i, j - 1, ModContent.TileType<MireFoliage_Tile>(), style, 0, -1, -1);
                }
                else if (Main.rand.NextBool(60) && flag)
                {
                    int style = 8; // spore
                    if (WorldGen.PlaceObject(i, j - 1, ModContent.TileType<MireFoliage_Tile>(), true, style))
                        NetMessage.SendObjectPlacement(-1, i, j - 1, ModContent.TileType<MireFoliage_Tile>(), style, 0, -1, -1);
                }
                else if (Main.rand.NextBool(230) && flag)
                {
                    int style = 9; // natures gift,but now its the thing
                    if (WorldGen.PlaceObject(i, j - 1, ModContent.TileType<MireFoliage_Tile>(), true, style))
                        NetMessage.SendObjectPlacement(-1, i, j - 1, ModContent.TileType<MireFoliage_Tile>(), style, 0, -1, -1);
                }
                else if (Main.rand.NextBool(15))
                {
                    int style; // jungle rose and vanity flowers

                    if (Main.rand.NextBool(3))
                        style = (short)(Main.rand.Next(2) + 6); // jungle rose, replaced by nothing
                    else
                        style = (short)(Main.rand.Next(13) + 10); // vanity flowers

                    if (WorldGen.PlaceObject(i, j - 1, ModContent.TileType<MireFoliage_Tile>(), true, style))
                        NetMessage.SendObjectPlacement(-1, i, j - 1, ModContent.TileType<MireFoliage_Tile>(), style, 0, -1, -1);
                }
                else
                {
                    int style = Main.rand.Next(6); // grass
                    if (WorldGen.PlaceObject(i, j - 1, ModContent.TileType<MireFoliage_Tile>(), false, style))
                        NetMessage.SendObjectPlacement(-1, i, j - 1, ModContent.TileType<MireFoliage_Tile>(), style, 0, -1, -1);
                }
            }
        }

        private static void TryGrowOnDepthMoss(int i, int j)
        {
            if (!Framing.GetTileSafely(i, j - 1).HasTile && Main.rand.NextBool(500))
            {
                TryPlaceFoliage(i, j - 1, style: 23, mute: true); // mushroom
            }

            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased) && !Framing.GetTileSafely(i, j - 1).HasTile && Main.rand.NextBool(40))
            {
                if (Main.rand.NextBool(20))
                {
                    int style = Main.rand.Next(23);
                    if (style == 9) // dont be the lotus
                        style = 7;
                    TryPlaceFoliage(i, j - 1, style, mute: false);
                }
            }
        }

        private static bool TryPlaceFoliage(int x, int y, int style, bool mute)
        {
            int type = ModContent.TileType<MireFoliage_Tile>();
            if (WorldGen.PlaceObject(x, y, type, mute, style))
            {
                NetMessage.SendObjectPlacement(-1, x, y, type, style, 0, -1, -1);
                return true;
            }
            return false;
        }
    }
}