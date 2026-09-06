using AAModClassic._Content._Dev.World.Biomes;
using AAModClassic._Content.Acropolis._PostMoonlord.Items.Tiles.Decoration;
using AAModClassic._Content.Acropolis._PostMoonlord.NPCs;
using AAModClassic._Content.Acropolis._PostMoonlord.NPCs.__BossAthenaA;
using AAModClassic._Content.Acropolis.World.Biomes;
using AAModClassic._Content.Chaos.___PreHardmode.NPCs.__BossGripsOfChaos;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.Awakened;
using AAModClassic._Content.Desert.___PreHardmode.Items.Materials;
using AAModClassic._Content.Desert._PostMoonlord.NPCs.__BossAnubisA;
using AAModClassic._Content.Dungeon.___PreHardmode.Items.Weapons;
using AAModClassic._Content.Hallow.__Hardmode.Items.Materials;
using AAModClassic._Content.Hell.World.Biomes;
using AAModClassic._Content.Hoard.World.Biomes;
using AAModClassic._Content.Hoard.World.Tiles;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno.___PreHardmode.NPCs.__BossBroodmother;
using AAModClassic._Content.Inferno.__Hardmode.Items.Consumables;
using AAModClassic._Content.Inferno.__Hardmode.Items.Materials;
using AAModClassic._Content.Inferno.__Hardmode.Items.Weapons;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Materials;
using AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma;
using AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma.Awakened;
using AAModClassic._Content.Inferno.World.BiomeChest;
using AAModClassic._Content.Inferno.World.Biomes;
using AAModClassic._Content.Inferno.World.Tiles;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic._Content.Mire.___PreHardmode.NPCs.__BossHydra;
using AAModClassic._Content.Mire.__Hardmode.Items.Consumables;
using AAModClassic._Content.Mire.__Hardmode.Items.Materials;
using AAModClassic._Content.Mire.__Hardmode.Items.Weapons;
using AAModClassic._Content.Mire._PostMoonlord.Items.Materials;
using AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata;
using AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata.Awakened;
using AAModClassic._Content.Mire.World.BiomeChest.Tiles;
using AAModClassic._Content.Mire.World.Biomes;
using AAModClassic._Content.Mire.World.Tiles;
using AAModClassic._Content.RedMushroom.World.Tiles;
using AAModClassic._Content.Snow.___PreHardmode.Items.Materials;
using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic._Content.Stars.World.Altar;
using AAModClassic._Content.Stars.World.Biomes;
using AAModClassic._Content.Terrarium.World.Biomes;
using AAModClassic._Content.Terrarium.World.Tiles;
using AAModClassic._Content.Underground.___PreHardmode.Items.Materials;
using AAModClassic._Content.Void.___PreHardmode.Items.Materials;
using AAModClassic._Content.Void.___PreHardmode.Items.Tiles.Decoration.OuroborosWoodFurniture;
using AAModClassic._Content.Void.___PreHardmode.Items.Weapons;
using AAModClassic._Content.Void.__Hardmode.Items.Weapons;
using AAModClassic._Content.Void._PostMoonlord.Items.Materials;
using AAModClassic._Content.Void._PostMoonlord.NPCs.__BossZero;
using AAModClassic._Content.Void._PostMoonlord.NPCs.__BossZero.Awakened;
using AAModClassic._Content.Void.World.BiomeChest;
using AAModClassic._Content.Void.World.Biomes;
using AAModClassic._Content.Void.World.Tiles;
using AAModClassic._Content.Void.World.Tiles.Trees;
using AAModClassic._CrossMod;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Materials;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.NPCs.__BossOrthrusX;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.NPCs.__BossRaiderUltima;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.NPCs.__BossRetriever;
using AAModClassic._Unreleased;
using AAModClassic._Unreleased.Content.LostKeep.World.Biomes;
using AAModClassic._Unreleased.Content.LostKeep.World.Tiles;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Conversions;
using AAModClassic.Dusts;
using AAModClassic.Globals;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.Chat;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace AAModClassic
{
    public class AAWorld : ModSystem
    {
        #region Variables
        public static int SmashDragonEgg = 2;
        public static int SmashHydraPod = 2;
        public static int OpenedChest = 2;
        //tile ints
        public static int mireTiles = 0;
        public static int infernoTiles = 0;
        public static int voidTiles = 0;
        public static int mushTiles = 0;
        public static int terraTiles = 0;
        public static int keepTiles = 0;
        public static int pagodaTiles = 0;
        public static int lakeTiles = 0;
        public static int shipTiles = 0;
        public static int Radium = 0;
        public static int EquinoxAltar = 0;
        public static int Darkmatter = 0;
        public static int DiscoBall = 0;
        public static int HoardTiles = 0;
        public static int CloudTiles = 0;
        //Worldgen
        public static bool TerrariumEnemies;
        public static bool Luminite;
        public static bool DarkMatter;
        public static bool HallowedOre;
        public static bool FulguriteOre;
        public static bool DjinnSerpent;
        public static bool ChaosOres;
        public static bool RadiumOre;
        public static bool AltarSmashed;
        public static int ChaosAltarsSmashed;
        public static int OreCount;
        public static bool DiscordOres;
        public static bool ChaosStripes;
        public int infernoSide = 0;
        public Vector2 infernoPos = new(0, 0);
        public Vector2 mirePos = new(0, 0);
        public Vector2 InfernoCenter = -Vector2.One;
        public Vector2 MireCenter = -Vector2.One;
        public static Vector2 shipPos = new(0, 0);
        public static Point acropolisPos = Point.Zero;
        public static Point terrariumCenter = Point.Zero;
        public string nums = "1234567890";
        public static bool ModContentGenerated;

        //Messages
        public static bool AMessage;
        public static bool Empowered;
        //Boss Bools
        public static bool Ancients;
        public static bool downedGrips;

        public static bool downedDB;
        public static bool downedNC;
        public static bool downedEquinox;

        public static bool downedAncient => downedAkuma || downedYamata || downedZero;
        public static bool downedSAncient => downedShen || AAWorld_Unreleased.DownedIZ || AAWorld_Unreleased.DownedSoC;
        public static bool downedAnySiegeUnits => NPCExtensions.BeenKilled<Retriever>() || NPCExtensions.BeenKilled<OrthrusXBody>() || NPCExtensions.BeenKilled<RaiderUltima>();
        public static bool downedAllSiegeUnits => NPCExtensions.BeenKilled<Retriever>() && NPCExtensions.BeenKilled<OrthrusXBody>() && NPCExtensions.BeenKilled<RaiderUltima>();
        public static bool downedAkuma => (NPCExtensions.BeenKilled<AkumaHead>() && !Main.expertMode) || NPCExtensions.BeenKilled<AkumaAHead>();
        public static bool downedYamata => (NPCExtensions.BeenKilled<YamataBody>() && !Main.expertMode) || NPCExtensions.BeenKilled<YamataABody>();
        public static bool zeroUS;
        public static bool downedZero => (NPCExtensions.BeenKilled<Zero>() && !Main.expertMode) || NPCExtensions.BeenKilled<ZeroA>();
        public static bool downedAllAncients => downedAkuma && downedYamata && downedZero;
        private static bool previousDownedAllAncients = false;
        public static bool ShenSummoned;
        public static bool downedShen => (NPCExtensions.BeenKilled<ShenDoragon>() && !Main.expertMode) || NPCExtensions.BeenKilled<ShenDoragonA>();

        public static bool downedAshe;
        public static bool downedHaruka;     
        public static bool downedSisters;
        public static bool SistersSummoned;

        public static bool AthenaHerald;

        public static bool downedAABoss;
        /*
        public static bool downedLucifer;
        public static bool downedKraken;
        */
        public static bool AnubisAwakened;
        public static bool AthenaAwakened;
        public static bool GreedAwakened;

        public static bool WormActive;
        public static bool StarActive;
        public static bool GravActive;

        public static bool Terra1 => NPCExtensions.BeenKilled<Broodmother>() || NPCExtensions.BeenKilled<HydraBody>() || NPC.downedBoss2;
		public static bool Terra2 => NPC.downedPlantBoss;
		public static bool Terra3 => downedShen;

        public static bool spawnGrips;
        //Points
        public static Point WHERESDAVOIDAT;

        //Squid Lady
        public static int squid1 = 0;
        public static int squid2 = 0;
        public static int squid3 = 0;
        public static int squid4 = 0;
        public static int squid5 = 0;
        public static int squid6 = 0;
        public static int squid7 = 0;
        public static int squid8 = 0;
        public static int squid9 = 0;
        public static int squid10 = 0;
        public static int squid11 = 0;
        public static int squid12 = 0;
        public static int squid13 = 0;
        public static int squid14 = 0;
        public static int squid15 = 0;
        public static int squid16 = 0;

        //Other
        public static bool Suncaller = false;
        public static bool Mooncaller = false;
        public static int RabbitKills = 0;
        public static bool TimeStopped = false;
        public static double PausedTime = 0;
        #endregion

        #region Save/Load
        public override void PreWorldGen()
        {
            //Bosses
            downedGrips = false;
            downedEquinox = false;
            zeroUS = false;
            ShenSummoned = false;
            downedAshe = false ;
            downedHaruka = false;
            downedSisters = false;
            SistersSummoned = false;
            AthenaHerald = false;
            downedAABoss = false;
            //downedLucifer = false;

            AnubisAwakened = false;
            WormActive = false;
            StarActive = false;
            GravActive = false;

            spawnGrips = false;
            //World Changes
            TerrariumEnemies = NPC.downedBoss2;
            ChaosOres = downedGrips;
            DjinnSerpent = NPC.downedBoss3;
            HallowedOre = NPC.downedMechBossAny;
            FulguriteOre = downedAnySiegeUnits;
            AMessage = NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3;
            Luminite = NPC.downedMoonlord;
            RadiumOre = downedEquinox;
            DiscordOres = downedSisters;
            ChaosStripes = Main.hardMode;
            ModContentGenerated = false;
            Empowered = downedShen;
            mirePos = new Vector2(0, 0);
            infernoPos = new Vector2(0, 0);
            InfernoCenter = -Vector2.One;
            MireCenter = -Vector2.One;
            SmashDragonEgg = 2;
            SmashHydraPod = 2;
            //Squid Lady
            squid1 = 0;
            squid2 = 0;
            squid3 = 0;
            squid4 = 0;
            squid5 = 0;
            squid6 = 0;
            squid7 = 0;
            squid8 = 0;
            squid9 = 0;
            squid10 = 0;
            squid11 = 0;
            squid12 = 0;
            squid13 = 0;
            squid14 = 0;
            squid15 = 0;
            squid16 = 0;

            acropolisPos = Point.Zero;
        }

        public static int Raycast(int x, int y)
        {
            while (!TileValid(x, y))
                y++;
            return y;
        }

        public static bool TileValid(int i, int j)
        {
            bool valid = false;
            try
            {
                valid = Main.tile[i, j].HasTile && Main.tileSolid[Main.tile[i, j].TileType];
            }
            catch (Exception e)
            {
                AAMod.instance.Logger.Error($"{e} \n{i}, {j}");
            }
            return valid;
        }

        public override void SaveWorldData(TagCompound tag)/* tModPorter Suggestion: Edit tag parameter instead of returning new TagCompound */
        {
            var downed = new List<string>();
            if (downedGrips) downed.Add("GrabbyHands");
            if (AMessage) downed.Add("AMessage");
            if (downedEquinox) downed.Add("Equinox");
            if (Ancients) downed.Add("AA");
            if (ShenSummoned) downed.Add("ShenS");
            if (ChaosStripes) downed.Add("IStripe");
            if (downedAshe) downed.Add("BetterDragonWaifu");
            if (downedHaruka) downed.Add("TrashDragonWaifu");
            if (downedSisters) downed.Add("Sisters");
            if (ModContentGenerated) downed.Add("WorldGenned");
            if (SistersSummoned) downed.Add("Summoned");
            if (zeroUS) downed.Add("ZUS");
            if (AthenaHerald) downed.Add("BitchBird");
            //if (downedLucifer) downed.Add("L");

            if (AnubisAwakened) downed.Add("AnuA");
            if (WormActive) downed.Add("WormA");
            if (StarActive) downed.Add("StarA");
            if (GravActive) downed.Add("GravA");

            tag.Add("downed", downed);

            tag.Add("MCenter", MireCenter);
            tag.Add("ICenter", InfernoCenter);


            //Squid Lady

            tag.Add("squid1", squid1);
            tag.Add("squid2", squid2);
            tag.Add("squid3", squid3);
            tag.Add("squid4", squid4);
            tag.Add("squid5", squid5);
            tag.Add("squid6", squid6);
            tag.Add("squid7", squid7);
            tag.Add("squid8", squid8);
            tag.Add("squid9", squid9);
            tag.Add("squid10", squid10);
            tag.Add("squid11", squid11);
            tag.Add("squid12", squid12);
            tag.Add("squid13", squid13);
            tag.Add("squid14", squid14);
            tag.Add("squid15", squid15);
            tag.Add("squid16", squid16);
            tag.Add("Bunny", RabbitKills);
            tag.Add("Egg", SmashDragonEgg);
            tag.Add("Pod", SmashHydraPod);

            tag.Add("acropolisPos", acropolisPos);
        }

        public override void LoadWorldData(TagCompound tag)
        {
            var downed = tag.GetList<string>("downed");
            //bosses
            downedGrips = downed.Contains("GrabbyHands");
            AMessage = downed.Contains("AMessage");
            downedEquinox = downed.Contains("Equinox");
            Ancients = downed.Contains("AA");
            ShenSummoned = downed.Contains("ShenS");
            downedAshe = downed.Contains("BetterDragonWaifu");
            downedHaruka = downed.Contains("TrashDragonWaifu");
            downedSisters = downed.Contains("Sisters");
            SistersSummoned = downed.Contains("Summoned");
            zeroUS = downed.Contains("ZUS");
            AthenaHerald = downed.Contains("BitchBird");

            AnubisAwakened = downed.Contains("AnuA");
            WormActive = downed.Contains("WormA");
            StarActive = downed.Contains("StarA");
            GravActive = downed.Contains("GravA");

            //World Changes
            ChaosOres = downedGrips;
            DjinnSerpent = NPC.downedBoss3;
            HallowedOre = NPC.downedMechBossAny;
            FulguriteOre = downedAnySiegeUnits;
            AMessage = NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3;
            Luminite = NPC.downedMoonlord;
            RadiumOre = downedEquinox;
            DiscordOres = downedSisters;
            ChaosStripes = downed.Contains("IStripe");
            ModContentGenerated = downed.Contains("WorldGenned");

            if (tag.ContainsKey("MCenter")) // check if the altar coordinates exist in the save file
            {
                MireCenter = tag.Get<Vector2>("MCenter");
            }
            if (tag.ContainsKey("ICenter")) // check if the altar coordinates exist in the save file
            {
                InfernoCenter = tag.Get<Vector2>("ICenter");
            }

            //Squid Lady
            squid1 = tag.GetInt("squid1");
            squid2 = tag.GetInt("squid2");
            squid3 = tag.GetInt("squid3");
            squid4 = tag.GetInt("squid4");
            squid5 = tag.GetInt("squid5");
            squid6 = tag.GetInt("squid6");
            squid7 = tag.GetInt("squid7");
            squid8 = tag.GetInt("squid8");
            squid9 = tag.GetInt("squid9");
            squid10 = tag.GetInt("squid10");
            squid11 = tag.GetInt("squid11");
            squid12 = tag.GetInt("squid12");
            squid13 = tag.GetInt("squid13");
            squid14 = tag.GetInt("squid14");
            squid15 = tag.GetInt("squid15");
            squid16 = tag.GetInt("squid16");

            RabbitKills = tag.GetInt("Bunny");
            SmashDragonEgg = tag.GetInt("Egg");
            SmashHydraPod = tag.GetInt("Pod");

            acropolisPos = tag.Get<Point>("acropolisPos");
            if(acropolisPos == Point.Zero)
                acropolisPos = new Point((int)(Main.maxTilesX * 0.65f), 100);

            TerrariumEnemies = NPC.downedBoss2;
            previousDownedAllAncients = downedAllAncients;
            AMessage = NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3;
        }
        //Idt this is actually needed
        public override void NetSend(BinaryWriter writer)
        {
            BitsByte flags = new();
            flags[0] = StarActive;
            flags[1] = downedGrips;
            flags[2] = ModContentGenerated;
            flags[3] = zeroUS;
            flags[4] = downedAshe;
            flags[5] = downedHaruka;
            flags[6] = SistersSummoned;
            flags[7] = AnubisAwakened;
            writer.Write(flags);

            BitsByte flags2 = new();
            flags2[0] = downedSisters;
            flags2[1] = downedEquinox;
            flags2[2] = ChaosStripes;
            flags2[3] = GravActive;
            flags2[4] = Ancients;
            flags2[5] = ShenSummoned;
            flags2[6] = AthenaHerald;
            flags2[7] = WormActive;
            writer.Write(flags2);

            writer.WriteVector2(MireCenter);
            writer.WriteVector2(InfernoCenter);

            writer.Write(squid1);
            writer.Write(squid2);
            writer.Write(squid3);
            writer.Write(squid4);
            writer.Write(squid5);
            writer.Write(squid6);
            writer.Write(squid7);
            writer.Write(squid8);
            writer.Write(squid9);
            writer.Write(squid10);
            writer.Write(squid11);
            writer.Write(squid12);
            writer.Write(squid13);
            writer.Write(squid14);
            writer.Write(squid15);
            writer.Write(squid16);
            writer.Write(RabbitKills);
            writer.Write(SmashDragonEgg);
            writer.Write(SmashHydraPod);

            writer.Write(acropolisPos.X);
            writer.Write(acropolisPos.Y);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            StarActive = flags[0];
            downedGrips = flags[1];
            ModContentGenerated = flags[2];
            zeroUS = flags[3];
            downedAshe = flags[4];
            downedHaruka = flags[5];
            SistersSummoned = flags[6];
            AnubisAwakened = flags[7];

            BitsByte flags2 = reader.ReadByte();
            downedSisters = flags2[0];
            downedEquinox = flags2[1];
            ChaosStripes = flags2[2];
            GravActive = flags2[3];
            Ancients = flags2[4];
            ShenSummoned = flags2[5];
            AthenaHerald = flags2[6];
            WormActive = flags2[7];

            MireCenter = reader.ReadVector2();
			InfernoCenter = reader.ReadVector2();		

            squid1 = reader.ReadInt32();
            squid2 = reader.ReadInt32();
            squid3 = reader.ReadInt32();
            squid4 = reader.ReadInt32();
            squid5 = reader.ReadInt32();
            squid6 = reader.ReadInt32();
            squid7 = reader.ReadInt32();
            squid8 = reader.ReadInt32();
            squid9 = reader.ReadInt32();
            squid10 = reader.ReadInt32();
            squid11 = reader.ReadInt32();
            squid12 = reader.ReadInt32();
            squid13 = reader.ReadInt32();
            squid14 = reader.ReadInt32();
            squid15 = reader.ReadInt32();
            squid16 = reader.ReadInt32();
            RabbitKills = reader.ReadInt32();
            SmashHydraPod = reader.ReadInt32();
            SmashDragonEgg = reader.ReadInt32();

            acropolisPos = new(reader.ReadInt32(), reader.ReadInt32());
        }
        #endregion

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            
            int shiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
            if(shiniesIndex > -1)
            {
                tasks.Insert(shiniesIndex + 1, new PassLegacy("Prisms", delegate (GenerationProgress progress, GameConfiguration config)
                {
                    GenPrisms(progress);
                }));
                tasks.Insert(shiniesIndex + 2, new PassLegacy("Abyssium", delegate (GenerationProgress progress, GameConfiguration config)
                {
                    GenAbyssium();
                }));
                tasks.Insert(shiniesIndex + 3, new PassLegacy("Incinerite", delegate (GenerationProgress progress, GameConfiguration config)
                {
                    GenIncinerite();
                }));
                tasks.Insert(shiniesIndex + 4, new PassLegacy("Everleaf", delegate (GenerationProgress progress, GameConfiguration config)
                {
                    GenEverleaf();
                }));
                tasks.Insert(shiniesIndex + 5, new PassLegacy("Relic", delegate (GenerationProgress progress, GameConfiguration config)
                {
                    GenRelicOre();
                }));
            }

            int liquidsIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Settle Liquids Again"));
            if (liquidsIndex != -1)
            {
                tasks.Insert(liquidsIndex + 1, new PassLegacy("Reserve Lost Keep", delegate (GenerationProgress progress, GameConfiguration config)
                {
                    ReserveLostKeep(progress);
                }));

                tasks.Insert(liquidsIndex + 1, new PassLegacy("Reserve Terrarium", delegate (GenerationProgress progress, GameConfiguration config)
                {
                    ReserveTerrarium(progress);
                }));
            }

            int ChaosIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Micro Biomes"));
            if(ChaosIndex > -1)
            {
                tasks.Insert(ChaosIndex + 1, new PassLegacy("Mire and Inferno", delegate (GenerationProgress progress, GameConfiguration config)
                {
                    MireAndInferno(progress);
                }));
            }

            int shiniesIndex1 = tasks.FindIndex(genpass => genpass.Name.Equals("Larva"));

            if (shiniesIndex1 > -1)
            {
                tasks.Insert(ChaosIndex + 2, new PassLegacy("The Pit", delegate (GenerationProgress progress, GameConfiguration config)
                {
                    ThePit(progress);
                }));
            }

            int shiniesIndex2 = tasks.FindIndex(genpass => genpass.Name.Equals("Final Cleanup"));
            if(shiniesIndex2 > -1)
            {

                tasks.Insert(shiniesIndex2, new PassLegacy("Ender", delegate (GenerationProgress progress, GameConfiguration config)
                {
                    EnderShrine();
                }));

                tasks.Insert(shiniesIndex2 + 1, new PassLegacy("LivingBogwoodConvert", delegate (GenerationProgress progress, GameConfiguration config)
                {
                    BogwoodConvert(progress);
                }));

                tasks.Insert(shiniesIndex2 + 2, new PassLegacy("Terrarium", delegate (GenerationProgress progress, GameConfiguration config)
                {
                    Terrarium(progress);
                }));

                tasks.Insert(shiniesIndex2 + 3, new PassLegacy("Lost Keep", delegate (GenerationProgress progress, GameConfiguration config)
                {
                    LostKeep(progress);
                }));

                tasks.Insert(shiniesIndex2 + 4, new PassLegacy("Hoard", delegate (GenerationProgress progress, GameConfiguration config)
                {
                    Hoard(progress);
                }));

                tasks.Insert(shiniesIndex2 + 5, new PassLegacy("Acropolis", delegate (GenerationProgress progress, GameConfiguration config)
                {
                    Acropolis(progress);
                }));

                tasks.Insert(shiniesIndex2 + 6, new PassLegacy("Void Islands", delegate (GenerationProgress progress, GameConfiguration config)
                {
                    VoidIslands(progress);
                }));

                tasks.Insert(shiniesIndex2 + 7, new PassLegacy("Chaos Altars", delegate (GenerationProgress progress, GameConfiguration config)
                {
                    Altars(progress);
                }));

                tasks.Insert(shiniesIndex2 + 8, new PassLegacy("Equinox", delegate (GenerationProgress progress, GameConfiguration config)
                {
                    EquinoxAlt(progress);
                }));
            }

            int DungeonChests = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Dungeon"));
            if (DungeonChests >= 0)
            {
                tasks.Insert(DungeonChests + 1, new PassLegacy("InfernoChest", delegate (GenerationProgress progress, GameConfiguration config)
                {
                    bool placed = false;
                    int Minimum = 50;
                    int Maximum = Main.maxTilesX / 2;
                    if (Main.dungeonX > Maximum)
                    {
                        Minimum = Maximum;
                        Maximum = Main.maxTilesX - 50;
                    }
                    while (!placed)
                    {
                        int PlaceHere = WorldGen.genRand.Next(Minimum, Maximum);
                        int PlacementHeight = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY - 200);
                        if (Main.wallDungeon[Main.tile[PlaceHere, PlacementHeight].WallType] && !Main.tile[PlaceHere, PlacementHeight].HasTile)
                        {
                            while (PlacementHeight < Main.maxTilesY - 200)
                            {
                                PlacementHeight++;
                                if (WorldGen.SolidTile(PlaceHere, PlacementHeight))
                                {
                                    int PlacementSuccess = WorldGen.PlaceChest(PlaceHere, PlacementHeight - 1, (ushort)ModContent.TileType<InfernoChest_Tile>(), false, 1);
                                    if (PlacementSuccess >= 0)
                                    {
                                        Chest chest = Main.chest[PlacementSuccess];
                                        chest.item[0].SetDefaults(ModContent.ItemType<DragonsPike>(), false);
                                        chest.item[1].SetDefaults(Utils.Next(WorldGen.genRand, new int[]
                                        { ModContent.ItemType<RadiantIncineriteBar>() }), false);
                                        chest.item[1].stack = WorldGen.genRand.Next(11, 20);
                                        Item item = chest.item[2];
                                        UnifiedRandom genRand = WorldGen.genRand;
                                        int[] array = new int[]
                                        { ModContent.ItemType<FlaskOfDragonfire>() };
                                        item.SetDefaults(Utils.Next(genRand, array), false);
                                        chest.item[2].stack = WorldGen.genRand.Next(1, 4);
                                        Item item2 = chest.item[3];
                                        UnifiedRandom genRand2 = WorldGen.genRand;
                                        int[] array2 = new int[]
                                        { 302, 2327, 2351, 304, 2329 };
                                        item2.SetDefaults(Utils.Next(genRand2, array2), false);
                                        chest.item[3].stack = WorldGen.genRand.Next(1, 3);
                                        chest.item[4].SetDefaults(Utils.Next(WorldGen.genRand, new int[]
                                        { 282, 286 }), false);
                                        chest.item[4].stack = WorldGen.genRand.Next(15, 31);
                                        chest.item[5].SetDefaults(ItemID.GoldCoin, false);
                                        chest.item[5].stack = WorldGen.genRand.Next(1, 3);
                                        placed = true ;
                                        break;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }));

                tasks.Insert(DungeonChests + 2, new PassLegacy("MireChest", delegate (GenerationProgress progress, GameConfiguration config)
                {
                    bool placed = false;
                    int Minimum = 50;
                    int Maximum = Main.maxTilesX / 2;
                    if (Main.dungeonX > Maximum)
                    {
                        Minimum = Maximum;
                        Maximum = Main.maxTilesX - 50;
                    }
                    while (!placed)
                    {
                        int PlaceHere = WorldGen.genRand.Next(Minimum, Maximum);
                        int PlacementHeight = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY - 200);
                        if (Main.wallDungeon[Main.tile[PlaceHere, PlacementHeight].WallType] && !Main.tile[PlaceHere, PlacementHeight].HasTile)
                        {
                            while (PlacementHeight < Main.maxTilesY - 200)
                            {
                                PlacementHeight++;
                                if (WorldGen.SolidTile(PlaceHere, PlacementHeight))
                                {
                                    int PlacementSuccess = WorldGen.PlaceChest(PlaceHere, PlacementHeight - 1, (ushort)ModContent.TileType<MireChest_Tile>(), false, 1);
                                    if (PlacementSuccess >= 0)
                                    {
                                        Chest chest = Main.chest[PlacementSuccess];
                                        chest.item[0].SetDefaults(ModContent.ItemType<BogBomb>(), false);
                                        chest.item[1].SetDefaults(Utils.Next(WorldGen.genRand, new int[]
                                        { ModContent.ItemType<DeepAbyssiumBar>() }), false);
                                        chest.item[1].stack = WorldGen.genRand.Next(11, 20);
                                        Item item = chest.item[2];
                                        UnifiedRandom genRand = WorldGen.genRand;
                                        int[] array = new int[]
                                        { ModContent.ItemType<FlaskOfHydratoxin>() };
                                        item.SetDefaults(Utils.Next(genRand, array), false);
                                        chest.item[2].stack = WorldGen.genRand.Next(1, 4);
                                        Item item2 = chest.item[3];
                                        UnifiedRandom genRand2 = WorldGen.genRand;
                                        int[] array2 = new int[]
                                        { 302, 2327, 2351, 304, 2329 };
                                        item2.SetDefaults(Utils.Next(genRand2, array2), false);
                                        chest.item[3].stack = WorldGen.genRand.Next(1, 3);
                                        chest.item[4].SetDefaults(Utils.Next(WorldGen.genRand, new int[]
                                        { 282, 286 }), false);
                                        chest.item[4].stack = WorldGen.genRand.Next(15, 31);
                                        chest.item[5].SetDefaults(ItemID.GoldCoin, false);
                                        chest.item[5].stack = WorldGen.genRand.Next(1, 3);
                                        placed = true;
                                        break;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }));


                tasks.Insert(DungeonChests + 3, new PassLegacy("VoidChest", delegate (GenerationProgress progress, GameConfiguration config)
                {
                    bool placed = false;
                    int Minimum = 50;
                    int Maximum = Main.maxTilesX / 2;
                    if (Main.dungeonX > Maximum)
                    {
                        Minimum = Maximum;
                        Maximum = Main.maxTilesX - 50;
                    }
                    while (!placed)
                    {
                        int PlaceHere = WorldGen.genRand.Next(Minimum, Maximum);
                        int PlacementHeight = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY - 200);
                        if (Main.wallDungeon[Main.tile[PlaceHere, PlacementHeight].WallType] && !Main.tile[PlaceHere, PlacementHeight].HasTile)
                        {
                            while (PlacementHeight < Main.maxTilesY - 200)
                            {
                                PlacementHeight++;
                                if (WorldGen.SolidTile(PlaceHere, PlacementHeight))
                                {
                                    int PlacementSuccess = WorldGen.PlaceChest(PlaceHere, PlacementHeight - 1, (ushort)ModContent.TileType<DoomsdayChest_Tile>(), false, 1);
                                    if (PlacementSuccess >= 0)
                                    {
                                        Chest chest = Main.chest[PlacementSuccess];
                                        chest.item[0].SetDefaults(ModContent.ItemType<SingularityCannon>(), false);
                                        chest.item[1].SetDefaults(Utils.Next(WorldGen.genRand, new int[]
                                        { ModContent.ItemType<DoomiteScrap>() }), false);
                                        chest.item[1].stack = WorldGen.genRand.Next(11, 20);
                                        Item item = chest.item[2];
                                        UnifiedRandom genRand = WorldGen.genRand;
                                        int[] array = new int[]
                                        { ModContent.ItemType<DoomiteBar>() };
                                        item.SetDefaults(Utils.Next(genRand, array), false);
                                        chest.item[2].stack = WorldGen.genRand.Next(1, 4);
                                        Item item2 = chest.item[3];
                                        UnifiedRandom genRand2 = WorldGen.genRand;
                                        int[] array2 = new int[]
                                        { 302, 2327, 2351, 304, 2329 };
                                        item2.SetDefaults(Utils.Next(genRand2, array2), false);
                                        chest.item[3].stack = WorldGen.genRand.Next(1, 3);
                                        chest.item[4].SetDefaults(Utils.Next(WorldGen.genRand, new int[]
                                        { 282, 286 }), false);
                                        chest.item[4].stack = WorldGen.genRand.Next(15, 31);
                                        chest.item[5].SetDefaults(ItemID.GoldCoin, false);
                                        chest.item[5].stack = WorldGen.genRand.Next(1, 3);
                                        placed = true;
                                        break;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }));
            }
            
            ModContentGenerated = true;
        }

        private static void GenIncinerite()
        {
            if (ContentReplacementSystem.NeedToReplaceContent)
                return;

            int x = Main.maxTilesX;
            int y = Main.maxTilesY;
            for (int k = 0; k < (int)(x * y * 15E-05); k++)
            {
                int tilesX = WorldGen.genRand.Next(0, Main.maxTilesX);
                int tilesY = WorldGen.genRand.Next((int)GenVars.rockLayerLow, Main.maxTilesY);
                if (Main.tile[tilesX, tilesY].TileType == TileID.Stone)
                {
                    WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(2, 4), WorldGen.genRand.Next(3, 6), (ushort)ModContent.TileType<IncineriteOre_Tile>());
                }
            }
        }

        private static void GenEverleaf()
        {
            //I do not know what an 'EverleafRoot' is.
            /*
            int x = Main.maxTilesX;
            int y = Main.maxTilesY;
            for (int k = 0; k < (int)(x * y * 15E-05); k++)
            {
                int tilesX = WorldGen.genRand.Next(0, x);
                int tilesY = WorldGen.genRand.Next(0, y);
                if (Main.tile[tilesX, tilesY].TileType == TileID.Mud)
                {
                    WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(3, 8), (ushort)ModContent.TileType<EverleafRoot_Tile>());
                }
            }
            */
        }

        private static void GenAbyssium()
        {
            int x = Main.maxTilesX;
            int y = Main.maxTilesY;
            for (int k = 0; k < (int)(x * y * 15E-05); k++)
            {
                int tilesX = WorldGen.genRand.Next(0, Main.maxTilesX);
                int tilesY = WorldGen.genRand.Next((int)GenVars.rockLayerLow, Main.maxTilesY);
                if (Main.tile[tilesX, tilesY].TileType == TileID.Mud)
                {
                    WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(2, 4), WorldGen.genRand.Next(3, 6), (ushort)ModContent.TileType<AbyssiumOre_Tile>());
                }
            }
        }

        private static void GenRelicOre()
        {
            int x = Main.maxTilesX;
            int y = Main.maxTilesY;
            for (int k = 0; k < (int)(x * y * 15E-04); k++)
            {
                int tilesX = WorldGen.genRand.Next(0, Main.maxTilesX);
                int tilesY = WorldGen.genRand.Next(0, Main.maxTilesY);
                if (Main.tile[tilesX, tilesY].TileType == TileID.IceBlock)
                {
                    WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(2, 4), WorldGen.genRand.Next(3, 6), (ushort)ModContent.TileType<VikingRelic_Tile>());
                }
            }
        }

        private static void GenPrisms(GenerationProgress progress)
        {
            progress.Message = Language.GetTextValue("LegacyWorldGen.23");
            int amount = (int)(Main.maxTilesX * 0.4f * 0.2f);
            for (int k = 0; k < amount; k++)
            {
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                int y = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY);
                while (Main.tile[x, y].TileType != TileID.Stone)
                {
                    x = WorldGen.genRand.Next(0, Main.maxTilesX);
                    y = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY);
                }
                WorldGen.TileRunner(x, y, WorldGen.genRand.Next(2, 6), WorldGen.genRand.Next(3, 7), ModContent.TileType<PrismOre_Tile>());
            }
        }

        public void VoidIslands(GenerationProgress progress)
        {
            progress.Message = $"0{Main.rand.Next(2)}0{Main.rand.Next(2)}0{Main.rand.Next(2)}0{Main.rand.Next(2)}0{Main.rand.Next(2)}0{Main.rand.Next(2)}0{Main.rand.Next(2)}0{Main.rand.Next(2)}0{Main.rand.Next(2)}0";

            int VoidHeight = 90;
            int IslandNumber = 2;
            if (WorldGenUtils.GetWorldSize() != 1)
            {
                IslandNumber = 4;
                VoidHeight = 120;
            }
            Point center = new((Main.maxTilesX / 15 * 14) + (Main.maxTilesX / 15 / 2) - 100, center.Y = VoidHeight);
            WHERESDAVOIDAT = center;
            Point oldposition = new(1, 1);
            List<Point> posIslands = new();
            

            for (int i = 0; i < IslandNumber; i++)
            {
                Point position = new(
                    center.X + (WorldGen.genRand.Next(35, 55) * (WorldGen.genRand.NextBool() ? -1 : 1)),
                    center.Y + (WorldGen.genRand.Next(35, 55) * (WorldGen.genRand.NextBool() ? -1 : 1)));

                while (posIslands.Any(x => Vector2.Distance(x.ToVector2(), position.ToVector2()) < 35))
                {
                    for (int k = 0; k < posIslands.Count; ++k)
                    {
                        while ((int)Vector2.Distance(posIslands[k].ToVector2(), position.ToVector2()) < 35)
                        {
                            position = new Point(center.X + (WorldGen.genRand.Next(35, 45) * (WorldGen.genRand.NextBool() ? -1 : 1)),
                              center.Y + (WorldGen.genRand.Next(35, 45) * (WorldGen.genRand.NextBool() ? -1 : 1)));
                        }
                    }
                }
                MiniIsland(position, 60);
                posIslands.Add(position);
                oldposition = position;
                for (int k = 0; k < posIslands.Count; ++k)
                {
                    for (int FuckWorldGen = 0; FuckWorldGen < 6; ++FuckWorldGen)
                    {
                        Point randompoint = new(
                            posIslands[k].X + WorldGen.genRand.Next(-30, 31),
                            posIslands[k].Y + WorldGen.genRand.Next(7, 42));
                        WorldGen.TileRunner(randompoint.X, randompoint.Y, WorldGen.genRand.Next(5, 8), WorldGen.genRand.Next(6, 13), ModContent.TileType<ApocalyptiteOre_Tile>(), false, 0f, 0f, false, true);
                    }
                }

                progress.Set(MathHelper.Lerp(0f, 0.5f, i / (float)(IslandNumber - 1)));
            }
            ChestNumber = 0;
            for (int j = 0; j < posIslands.Count; ++j)
            {
                Point position = posIslands[j];
                position.X -= 4;
                position.Y -= 11;
                VoidHouses(position.X, position.Y, (ushort)ModContent.TileType<DoomiteScrap_Tile>(), 10, 7);

                progress.Set(MathHelper.Lerp(0.5f, 1f, j / (float)(posIslands.Count - 1)));
            }
        }

        public static int BlockLining(double x, double y, int repeats, int tileType, bool random, int max, int min = 3)
        {
            for (double i = x; i < x + repeats; i++)
            {
                if (random)
                {
                    for (double k = y; k < y + Main.rand.Next(min, max); k++)
                    {
                        WorldGen.PlaceTile((int)i, (int)k, tileType);
                    }
                }
                else
                {
                    for (double k = y; k < y + max; k++)
                    {
                        WorldGen.PlaceTile((int)i, (int)k, tileType);
                    }
                }
            }
            return repeats;
        }

        private static void MiniIsland(Point position, int size)
        {
            for (int i = -size / 2; i < size / 2; ++i)
            {
                int repY = (size / 2) - Math.Abs(i);
                int offset = repY / 5;
                repY += WorldGen.genRand.Next(4);
                for (int j = -offset; j < repY; ++j)
                {
                    WorldGen.PlaceTile(position.X + i, position.Y + j, ModContent.TileType<Doomstone_Tile>());
                }
                int y = Raycast(position.X + i, position.Y - 5);
                WorldGen.PlaceObject(position.X + i, y, ModContent.TileType<OuroborosSapling_Tile>());
                WorldGen.GrowTree(position.X + i, y);
            }

            int halfSize = size / 2;
            WorldGenUtils.AddProtectedStructure(new(position.X - halfSize, position.Y - halfSize, size, size), 20);
        }

        public static readonly HashSet<int> DontSpawnAltarsOn =
        [
            ModContent.TileType<KeepBrick_Tile>(),
            ModContent.TileType<TerraCrystal_Tile>(),
            ModContent.TileType<GreedBrick_Tile>(),
        ];

        private static void Altars(GenerationProgress progress)
        {
            progress.Message = Language.GetTextValue("Mods.AAModClassic.Common.AAWorldBuildAltars");
            for (int num = 0; num < Main.maxTilesX / 390; num++)
            {
                int xAxis = WorldGen.genRand.Next(200, Main.maxTilesX - 200);
                int yAxis = WorldGen.genRand.Next((int)GenVars.rockLayer + 150, Main.maxTilesY - 250);
                for (int AltarX = xAxis - 45; AltarX < xAxis + 45; AltarX++)
                {
                    for (int AltarY = yAxis - 45; AltarY < yAxis + 45; AltarY++)
                    {
                        Tile tile = Main.tile[AltarX, AltarY];

                        if (DontSpawnAltarsOn.Contains(tile.TileType))
                            continue;

                        int Altar;
                        if (Main.rand.NextBool())
                            Altar = ModContent.TileType<AbyssAltarUnsafe_Tile>();
                        else
                            Altar = ModContent.TileType<DragonAltarUnsafe_Tile>();

                        if (Main.rand.NextBool(15))
                        {
                            if ((tile.TileType == ModContent.TileType<Torchstone_Tile>() ||
                                tile.TileType == ModContent.TileType<Torchsand_Tile>() ||
                                tile.TileType == ModContent.TileType<Torchice_Tile>() ||
                                tile.TileType == ModContent.TileType<Torchsandstone_Tile>() ||
                                tile.TileType == ModContent.TileType<Torchsand_Tile>() ||
                                tile.TileType == ModContent.TileType<InfernoGrass_Tile>())  
                                && Altar == ModContent.TileType<AbyssAltarUnsafe_Tile>())
                            {
                                Altar = ModContent.TileType<DragonAltarUnsafe_Tile>();
                            }
                            if ((tile.TileType == ModContent.TileType<Depthstone_Tile>() || 
                                tile.TileType == ModContent.TileType<Depthsand_Tile>() || 
                                tile.TileType == ModContent.TileType<IndigoIce_Tile>() ||
                                tile.TileType == ModContent.TileType<Depthsandstone_Tile>() ||
                                tile.TileType == ModContent.TileType<Depthsand_Tile>() ||
                                tile.TileType == ModContent.TileType<MireGrass_Tile>()) 
                                && Altar == ModContent.TileType<DragonAltarUnsafe_Tile>())
                            {
                                Altar = ModContent.TileType<AbyssAltarUnsafe_Tile>();
                            }
                            WorldGen.PlaceObject(AltarX, AltarY - 1, Altar);
                        }
                    }
                }
            }
        }
        
        public int ChestNumber = 0;

        public void VoidHouses(int X, int Y, int type = 30, int sizeX = 10, int sizeY = 7)
        {
            int wallID = (ushort)ModContent.WallType<DoomitePlatingWall_Wall>();
            //Clear area
            for (int i = X; i < X + sizeX - 1; ++i)
            {
                for (int j = Y - 1; j < Y + sizeY; ++j)
                {
                    WorldGen.KillTile(i, j);
                }
            }
            //Wall Placement
            for (int i = X + 1; i < X + sizeX - 2; ++i)
            {
                for (int j = Y + 1; j < Y + sizeY - 1; ++j)
                {
                    if (WorldGen.genRand.Next(5) >= 1)
                    {
                        WorldGen.KillWall(i, j);
                        WorldGen.PlaceWall(i, j, wallID);
                    }
                }
            };
            //Side placements
            for (int i = Y; i < Y + sizeY - 1; ++i)
            {
                WorldGen.PlaceTile(X, i, type);
                WorldGen.PlaceTile(X + (sizeX - 2), i, (ushort)ModContent.TileType<DoomiteScrap_Tile>());
            }
            //Roof-floor placements
            for (int i = X; i < X + sizeX - 2; ++i)
            {
                WorldGen.PlaceTile(i, Y, type);
                WorldGen.PlaceTile(i, Y + (sizeY - 1), (ushort)ModContent.TileType<DoomiteScrap_Tile>());
            }
            WorldGen.PlaceTile(X + sizeX - 2, Y + sizeY - 1, (ushort)ModContent.TileType<DoomiteScrap_Tile>());

            int PlacementSuccess = WorldGen.PlaceChest(X + ((sizeX - 1) / 2), Y + sizeY - 2, (ushort)ModContent.TileType<OuroborosWoodChest_Tile>(), true);
            if (PlacementSuccess >= 0)
            {
                Chest chest = Main.chest[PlacementSuccess];
                if (ChestNumber == 0)
                {
                    VoidLoot(ModContent.ItemType<Voidsaber>(), chest);
                }
                else if (ChestNumber == 1)
                {
                    VoidLoot(ModContent.ItemType<DoomPistol>(), chest);
                }
                else if (ChestNumber == 2)
                {
                    VoidLoot(ModContent.ItemType<DoomStaff>(), chest);

                }
                else if (ChestNumber == 3)
                {
                    VoidLoot(ModContent.ItemType<VoidProbeControlUnit>(), chest);
                }
                ChestNumber += 1;
            }
            //Side holes
            for (int i = Y + sizeY - 4; i > Y + sizeY; --i)
            {
                WorldGen.KillTile(X, i);
            }
        }

        public static void VoidLoot(int Item, Chest chest)
        {
            chest.item[0].SetDefaults(Item, false);
            chest.item[1].SetDefaults(ModContent.ItemType<DoomiteScrap>(), false);
            chest.item[1].stack = WorldGen.genRand.Next(4, 6);
            Item item = chest.item[2];
            UnifiedRandom genRand = WorldGen.genRand;
            int[] array2 = new int[]
            { 302, 2327, 2351, 304, 2329 };
            item.SetDefaults(Utils.Next(genRand, array2), false);
            chest.item[2].stack = WorldGen.genRand.Next(1, 3);
            chest.item[3].SetDefaults(Utils.Next(WorldGen.genRand, new int[]
            { 282, 286 }), false);
            chest.item[3].stack = WorldGen.genRand.Next(15, 31);
            chest.item[4].SetDefaults(ItemID.GoldCoin, false);
            chest.item[4].stack = WorldGen.genRand.Next(1, 3);
        }

        public override void PostWorldGen()
        {
            int[] itemsToPlaceInDungeonChests = new int[] { ModContent.ItemType<SkullWand>() };
            int itemsToPlaceInDungeonChestsChoice = 0;
            for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest != null && Main.tile[chest.x, chest.y].TileType == TileID.Containers && Main.tile[chest.x, chest.y].TileFrameX == 2 * 36)
                {
                    if (Main.rand.NextBool(3))
                    {
                        for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                        {
                            if (chest.item[inventoryIndex].type == ItemID.None)
                            {
                                chest.item[inventoryIndex].SetDefaults(itemsToPlaceInDungeonChests[itemsToPlaceInDungeonChestsChoice]);
                                itemsToPlaceInDungeonChestsChoice = (itemsToPlaceInDungeonChestsChoice + 1) % itemsToPlaceInDungeonChests.Length;
                                break;
                            }
                        }
                    }
                }
            }
        }

        public override void OnWorldLoad()
        {
            if (downedZero)
                VoidSky.Alpha = 0f;
        }

        public Vector2 Origin = new Vector2((int)(Main.maxTilesX * 0.65f), 100) * 16;
        public int HeraldTimer = 1200;

        public override void PostUpdateWorld()
        {
            if (NPCExtensions.BeenKilled<AnubisA>() && !AthenaHerald && !NPCExtensions.BeenKilled<AthenaA>())
            {
                if (HeraldTimer > 0)
                {
                    HeraldTimer--;
                }
                else
                {
                    Player player = Main.player[BaseAI.GetPlayer(new Vector2(Main.maxTilesX / 2, Main.maxTilesY / 2), -1)];
                    Vector2 spawnpoint = player.Center - new Vector2(250, 200);
                    int Seraph = NPC.NewNPC(NPC.GetSource_NaturalSpawn(), (int)spawnpoint.X, (int)spawnpoint.Y, ModContent.NPCType<SeraphHerald>());
                    NPC Seraph1 = Main.npc[Seraph];
                    for (int i = 0; i < 5; i++)
                    {
                        Dust.NewDust(Seraph1.position, Seraph1.height, Seraph1.width, ModContent.DustType<FeatherDust>(), Main.rand.Next(-1, 2), 1, 0);
                    }
                    AthenaHerald = true;
                }
            }

            if (!ContentReplacementSystem.NeedToReplaceContent && !Main.dayTime)
            {
                if (!Main.IsFastForwardingTime()/* tModPorter Note: _Unreleased. Suggestion: IsFastForwardingTime(), fastForwardTimeToDawn or fastForwardTimeToDusk */)
                {
                    if (Main.time == 1 && !WorldGen.spawnEye)
                    {
                        if (!downedGrips && Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            bool flag3 = false;
                            for (int n = 0; n < 255; n++)
                            {
                                if (Main.player[n].active && Main.player[n].statLifeMax >= 200 && Main.player[n].statDefense > 10)
                                {
                                    flag3 = true;
                                    break;
                                }
                            }
                            if (flag3 && Main.rand.NextBool(3))
                            {
                                int num8 = 0;
                                for (int num9 = 0; num9 < 200; num9++)
                                {
                                    if (Main.npc[num9].active && Main.npc[num9].townNPC)
                                    {
                                        num8++;
                                    }
                                }
                                if (num8 >= 4)
                                {
                                    spawnGrips = true;
                                    if (Main.netMode == NetmodeID.SinglePlayer)
                                    {
                                        Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossSummonsInfo.GripsAwoken"), 50, 255, 130);
                                    }
                                    else if (Main.netMode == NetmodeID.Server)
                                    {
                                        NetMessage.SendData(MessageID.ChatText, -1, -1, null, 255, 50f, 255f, 130f, 0, 0, 0);
                                    }
                                }
                            }
                        }
                    }
                    if (spawnGrips && Main.netMode != NetmodeID.MultiplayerClient && Main.time > 4860.0)
                    {
                        for (int k = 0; k < 255; k++)
                        {
                            if (Main.player[k].active && !Main.player[k].dead && Main.player[k].position.Y < Main.worldSurface * 16.0)
                            {
                                if (Main.netMode == NetmodeID.SinglePlayer) { if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.Grips.GripsofChaosAwoken"), 175, 75, 255, false); }
                                else if (Main.netMode == NetmodeID.Server)
                                    if (Main.netMode == NetmodeID.SinglePlayer) { if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.Grips.GripsofChaosAwoken"), 175, 75, 255, false); }
                                    else if (Main.netMode == NetmodeID.Server)
                                    {
                                        ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(Language.GetTextValue("Mods.AAModClassic.Common.Grips.GripsofChaosAwoken")), new Color(175, 75, 255), -1);
                                    }
                                AAModGlobalNPC.SpawnBoss(Main.player[k], ModContent.NPCType<GripOfChaosMire>(), false, 1, 0);
                                AAModGlobalNPC.SpawnBoss(Main.player[k], ModContent.NPCType<GripOfChaosInferno>(), false, -1, 0);
                                spawnGrips = false;
                                break;
                            }
                        }
                    }
                }
            }

            if (downedEquinox)
            {
                if (RadiumOre == false)
                {
                    RadiumOre = true;
                    if (Main.netMode != NetmodeID.MultiplayerClient) 
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.downedEquinoxInfo"), Color.Violet);
                    for (int i = 0; i < Main.maxTilesX / 50; ++i)
                    {
                        int X = WorldGen.genRand.Next(Main.maxTilesX / 10 * 2, (int)(Main.maxTilesX / 10 * 4.5f));
                        int Y = WorldGen.genRand.Next(50, 150); //Y position, centre.
                        int radius = WorldGen.genRand.Next(2, 6); //Radius.
                        for (int x = X - radius; x <= X + radius; x++)
                        {
                            for (int y = Y - radius; y <= Y + radius; y++)
                            {
                                if (Vector2.Distance(new Vector2(X, Y), new Vector2(x, y)) <= radius) //Checks if coords are within a circle position
                                {
                                    WorldGen.PlaceTile(x, y, ModContent.TileType<RadiumOre_Tile>(), true); //Places tile of type InsertTypeHere at the specified coords
                                }
                            }
                        }
                    }
                    for (int i = 0; i < Main.maxTilesX / 50; ++i)
                    {
                        int X = WorldGen.genRand.Next((int)(Main.maxTilesX / 10 * 5.5f), Main.maxTilesX / 10 * 8);
                        int Y = WorldGen.genRand.Next(50, 150); //Y position, centre.
                        int radius = WorldGen.genRand.Next(2, 6); //Radius.
                        for (int x = X - radius; x <= X + radius; x++)
                        {
                            for (int y = Y - radius; y <= Y + radius; y++)
                            {
                                if (Vector2.Distance(new Vector2(X, Y), new Vector2(x, y)) <= radius) //Checks if coords are within a circle position
                                {
                                    WorldGen.PlaceTile(x, y, ModContent.TileType<RadiumOre_Tile>(), true); //Places tile of type InsertTypeHere at the specified coords
                                }
                            }
                        }
                    }
                }
            }
            if (NPC.downedMoonlord)
            {
                if (Ancients == false)
                {
                    Ancients = true;
                    if (Main.netMode != NetmodeID.MultiplayerClient) 
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.downedMoonlordInfo1"), Color.ForestGreen);
                }
                if (Luminite == false)
                {
                    Luminite = true;
                    if (Main.netMode != NetmodeID.MultiplayerClient) 
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.downedMoonlordInfo2"), Color.DarkSeaGreen);
                    for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 8E-05); k++)
                    {
                        WorldGen.OreRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 200), WorldGen.genRand.Next(5, 9), WorldGen.genRand.Next(6, 10), TileID.LunarOre);
                    }
                    return;
                }
            }
            if (NPC.downedMechBossAny)
            {
                if (HallowedOre == false)
                {
                    HallowedOre = true;
                    if (Main.netMode != NetmodeID.MultiplayerClient) 
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.downedMechBossAnyInfo"), Color.Goldenrod);
                    int x = Main.maxTilesX;
                    int y = Main.maxTilesY;
                    for (int k = 0; k < (int)(x * y * 15E-05); k++)
                    {
                        int tilesX = WorldGen.genRand.Next(0, x);
                        int tilesY = WorldGen.genRand.Next((int)(y * .3f), (int)(y * .75f));
                        WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(4, 9), (ushort)ModContent.TileType<HallowedOre_Tile>());
                    }
                }
            }

            if (downedAnySiegeUnits)
            {
                if (FulguriteOre == false)
                {
                    FulguriteOre = true;
                    if (Main.netMode != NetmodeID.MultiplayerClient) 
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.downedSiegeUnitAnyInfo"), Color.MediumPurple);
                    for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 6E-05); k++)
                        WorldGen.OreRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 200), WorldGen.genRand.Next(10, 11), WorldGen.genRand.Next(10, 11), (ushort)ModContent.TileType<FulguriteShard_Tile>());
                }
            }

            if (downedSisters)
            {
                if (!DiscordOres)
                {
                    DiscordOres = true;
                    if (Main.netMode != NetmodeID.MultiplayerClient) 
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.downedSistersInfo"), Color.Magenta);
                    int x = Main.maxTilesX;
                    int y = Main.maxTilesY;
                    for (int k = 0; k < (int)(x * y * 15E-05); k++)
                    {
                        int tilesX = WorldGen.genRand.Next(0, x);
                        int tilesY = WorldGen.genRand.Next((int)(y * .3f), (int)(y * .75f));
                        if (Main.tile[tilesX, tilesY].TileType == TileID.Mud)
                        {
                            WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(5, 6), WorldGen.genRand.Next(10, 11), (ushort)ModContent.TileType<EventideAbyssiumOre_Tile>());
                        }
                    }
                    for (int k = 0; k < (int)(x * y * 15E-05); k++)
                    {
                        int tilesX = WorldGen.genRand.Next(0, x);
                        int tilesY = WorldGen.genRand.Next((int)(y * .3f), (int)(y * .75f));
                        if (Main.tile[tilesX, tilesY].TileType == TileID.Stone)
                        {
                            WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(5, 6), WorldGen.genRand.Next(10, 11), (ushort)ModContent.TileType<DaybreakIncineriteOre_Tile>());
                        }
                    }
                }
            }
            if (NPC.downedBoss2)
            {
                if (!TerrariumEnemies)
                {
                    TerrariumEnemies = true;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.downedBoss2Info1"), Color.LimeGreen);
                    }
                }
            }
            if (NPC.downedBoss3)
            {
                if (!DjinnSerpent)
                {
                    DjinnSerpent = true;

                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.downedBoss3Info1"), Color.Orange);
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.downedBoss3Info2"), Color.Cyan.R, Color.Cyan.G, Color.Cyan.B);

                        int x = Main.maxTilesX;
                        int y = Main.maxTilesY;
                        for (int k = 0; k < (int)(x * y * 15E-05); k++)
                        {
                            int tilesX = WorldGen.genRand.Next(0, x);
                            int tilesY = WorldGen.genRand.Next((int)(y * 0.1f), (int)(y * 0.8f));
                            int type = Main.tile[tilesX, tilesY].TileType;
                            if (type == TileID.HardenedSand || type == TileID.Sandstone || type == TileID.DesertFossil)
                            {
                                WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(5, 6), WorldGen.genRand.Next(10, 11), (ushort)ModContent.TileType<DynaskullFossil_Tile>());
                            }
                        }

                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.downedBoss3Info3"), Color.DarkOrange.R, Color.DarkOrange.G, Color.DarkOrange.B);
                    }
                }
            }
            if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
            {
                if (!AMessage)
                {
                    AMessage = true;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.downedMechBossInfo1"), Color.Gold.R, Color.Gold.G, Color.Gold.B);
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.downedMechBossInfo2"), Color.Orange.R, Color.Orange.G, Color.Orange.B);
                    }
                }
                
            }       

            if (downedAllAncients && !previousDownedAllAncients)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.downedAllAncientsInfo1"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased))
                    {
                        // im removing this one bcuz its for iz who isnt post-ancients hes post-zero specifically
                        //Main.NewText("You feel as if you are being watched by something...malicious...", new Color(158, 3, 32));
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.downedAllAncientsInfo2"), Color.Cyan);
                    }
                }
            }

            previousDownedAllAncients = downedAllAncients;

            if (Main.hardMode)
            {
                if (ChaosStripes == false)
                {
                    if (!ContentReplacementSystem.NeedToReplaceContent)
                    {
                        ConversionHandler.ConvertDownBoth((int)MireCenter.X, (int)InfernoCenter.X, 0, 120);
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.hardModeInfo"), Color.Magenta.R, Color.Magenta.G, Color.Magenta.B);
                    }
                    ChaosStripes = true;
                }
            }
        }

        public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
        {
            Main.SceneMetrics.SandTileCount += tileCounts[ModContent.TileType<Torchsand_Tile>()] + tileCounts[ModContent.TileType<Torchsandstone_Tile>()] + tileCounts[ModContent.TileType<TorchsandHardened_Tile>()] + tileCounts[ModContent.TileType<Depthsand_Tile>()] + tileCounts[ModContent.TileType<Depthsandstone_Tile>()] + tileCounts[ModContent.TileType<DepthsandHardened_Tile>()];
            Main.SceneMetrics.SnowTileCount += tileCounts[ModContent.TileType<Torchice_Tile>()] + tileCounts[ModContent.TileType<IndigoIce_Tile>()] + tileCounts[ModContent.TileType<TorchAsh_Tile>()];
            mireTiles = tileCounts[ModContent.TileType<MireGrass_Tile>()]+ tileCounts[ModContent.TileType<Depthstone_Tile>()] + tileCounts[ModContent.TileType<Depthsand_Tile>()] + tileCounts[ModContent.TileType<Depthsandstone_Tile>()] + tileCounts[ModContent.TileType<DepthsandHardened_Tile>()] + tileCounts[ModContent.TileType<IndigoIce_Tile>()] + tileCounts[ModContent.TileType<LivingBogleaf_Tile>()] + tileCounts[ModContent.TileType<LivingBogwood_Tile>()];
            infernoTiles = tileCounts[ModContent.TileType<InfernoGrass_Tile>()]+ tileCounts[ModContent.TileType<Torchstone_Tile>()] + tileCounts[ModContent.TileType<Torchsand_Tile>()] + tileCounts[ModContent.TileType<Torchsandstone_Tile>()] + tileCounts[ModContent.TileType<TorchsandHardened_Tile>()] + tileCounts[ModContent.TileType<Torchice_Tile>()] + tileCounts[ModContent.TileType<TorchAsh_Tile>()] + tileCounts[ModContent.TileType<LivingRazeleaves_Tile>()] + tileCounts[ModContent.TileType<LivingRazewood_Tile>()];
            voidTiles = tileCounts[ModContent.TileType<Doomstone_Tile>()] + tileCounts[ModContent.TileType<ApocalyptiteOre_Tile>()] + tileCounts[ModContent.TileType<DoomGrass_Tile>()] + tileCounts[ModContent.TileType<DoomstoneB_Tile>()];
            mushTiles = tileCounts[ModContent.TileType<Mycelium_Tile>() ];
            Main.SceneMetrics.JungleTileCount += mireTiles;
            pagodaTiles = tileCounts[ModContent.TileType<ScorchedDynastyWoodUnsafe_Tile>()];
            lakeTiles = tileCounts[ModContent.TileType<Darkmud_Tile>()] + tileCounts[ModContent.TileType<AbyssGrass_Tile>()] + tileCounts[ModContent.TileType<AbyssWood_Tile>()] + tileCounts[ModContent.TileType<AbyssWoodSolid_Tile>()];
            terraTiles = tileCounts[ModContent.TileType<TerraCrystal_Tile>()];
            keepTiles = tileCounts[ModContent.TileType<KeepBrick_Tile>()] + tileCounts[ModContent.TileType<KeepPlatform_Tile>()] + tileCounts[ModContent.TileType<TerraPillar_Tile>()];
            Radium = tileCounts[ModContent.TileType<RadiumOre_Tile>()];
            EquinoxAltar = tileCounts[ModContent.TileType<DaybringerBrick_Tile>()] + tileCounts[ModContent.TileType<NightcrawlerBrick_Tile>()];
            HoardTiles = tileCounts[ModContent.TileType<GreedBrick_Tile>()] + tileCounts[ModContent.TileType<GreedStone_Tile>()];
            CloudTiles = tileCounts[ModContent.TileType<SkymarbleBrick_Tile>()] + tileCounts[ModContent.TileType<SkycrystalBrick_Tile>()];
        }

        private static int RollInfernoX(int side)
        {
            return (Main.maxTilesX >= 8000)
                ? (side == 1 ? WorldGen.genRand.Next(2000, 2300) : (Main.maxTilesX - WorldGen.genRand.Next(2000, 2300)))
                : (side == 1 ? WorldGen.genRand.Next(1500, 1700) : (Main.maxTilesX - WorldGen.genRand.Next(1500, 1700)));
        }

        private static int RollMireX(int infernoSide)
        {
            return (Main.maxTilesX >= 8000)
                ? (infernoSide != 1 ? WorldGen.genRand.Next(2000, 2300) : (Main.maxTilesX - WorldGen.genRand.Next(2000, 2300)))
                : (infernoSide != 1 ? WorldGen.genRand.Next(1500, 1700) : (Main.maxTilesX - WorldGen.genRand.Next(1500, 1700)));
        }

        private static int FindBiomeSurfaceY(int x)
        {
            int y = (int)GenVars.worldSurfaceLow - 30;
            while (Main.tile[x, y] != null && !Main.tile[x, y].HasTile)
                y++;

            for (int l = x - 25; l < x + 25; l++)
            {
                for (int m = y - 6; m < y + 90; m++)
                {
                    if (Main.tile[l, m] != null && Main.tile[l, m].HasTile)
                    {
                        int type = Main.tile[l, m].TileType;
                        if (type == TileID.Cloud || type == TileID.RainCloud || type == TileID.Sunplate)
                        {
                            y++;
                        }
                    }
                }
            }

            return y;
        }

        private static Rectangle GetInfernoFootprint(Point origin)
        {
            int worldSize = WorldGenUtils.GetWorldSize();
            int biomeRadius = worldSize == 3 ? 240 : worldSize == 2 ? 200 : 180;

            int texWidth = InfernoTexGenAssets.VolcanoTileData.Width;
            int texHeight = InfernoTexGenAssets.VolcanoTileData.Height;

            int halfWidth = Math.Max(biomeRadius, texWidth / 2);
            int top = origin.Y - biomeRadius;
            int bottom = Math.Max(origin.Y + biomeRadius, origin.Y - 80 + texHeight);

            return new Rectangle(origin.X - halfWidth, top, halfWidth * 2, bottom - top);
        }

        private static Rectangle GetMireFootprint(Point origin)
        {
            int worldSize = WorldGenUtils.GetWorldSize();
            int biomeRadius = worldSize == 3 ? 240 : worldSize == 2 ? 200 : 180;

            int texWidth = MireTexGenAssets.LakeTileData.Width;
            int texHeight = MireTexGenAssets.LakeTileData.Height;

            int halfWidth = Math.Max(biomeRadius, texWidth / 2);
            int top = origin.Y - biomeRadius;
            int bottom = Math.Max(origin.Y + biomeRadius, origin.Y - 30 + texHeight);

            return new Rectangle(origin.X - halfWidth, top, halfWidth * 2, bottom - top);
        }

        private static (Point SurfacePoint, Point PlacementOrigin) FindSafeBiomeOrigin(Func<int> rollX, Func<Point, Rectangle> getFootprint, StructureMap structures, string biomeNameForLog, int maxAttempts = 300)
        {
            Point fallbackSurface = default;
            Point fallbackOrigin = default;

            for (int attempt = 0; attempt < maxAttempts; attempt++)
            {
                int x = rollX();
                int surfaceY = FindBiomeSurfaceY(x);
                Point surfacePoint = new Point(x, surfaceY);

                Point placementOrigin = surfacePoint;
                placementOrigin.Y = WorldGenUtils.GetFirstTileFloor(placementOrigin.X, placementOrigin.Y, true);

                if (attempt == 0)
                {
                    fallbackSurface = surfacePoint;
                    fallbackOrigin = placementOrigin;
                }

                Rectangle footprint = getFootprint(placementOrigin);

                if (structures.CanPlace(footprint, WorldGenUtils.AllTilesAllowed, 0))
                {
                    AAMod.instance.Logger.Info(biomeNameForLog + " placed successfully after " + attempt + " attempt(s).");
                    return (surfacePoint, placementOrigin);
                }
            }

            AAMod.instance.Logger.Warn(biomeNameForLog + " could not find a clear location after " + maxAttempts + " attempts; falling back to the first candidate, which may overlap another structure.");
            return (fallbackSurface, fallbackOrigin);
        }

        private void MireAndInferno(GenerationProgress progress)
        {
            if (ContentReplacementSystem.NeedToReplaceContent)
                return;

            infernoSide = (Main.dungeonX > Main.maxTilesX / 2) ? (-1) : 1;

            progress.Message = Language.GetTextValue("Mods.AAModClassic.Common.AAWorldBuildChaos");
            progress.Message = Language.GetTextValue("Mods.AAModClassic.Common.AAWorldBuildInferno");

            var (infernoSurface, infernoOrigin) = FindSafeBiomeOrigin(() => RollInfernoX(infernoSide), GetInfernoFootprint, GenVars.structures, "Inferno");

            infernoPos.X = infernoSurface.X;
            infernoPos.Y = infernoSurface.Y;
            InfernoCenter = infernoPos;

            InfernoGeneration infBiome = new();
            InfernoDelete infDelete = new();
            infDelete.Place(infernoOrigin, GenVars.structures);
            infBiome.Place(infernoOrigin, GenVars.structures);
            WorldGenUtils.AddProtectedStructure(GetInfernoFootprint(infernoOrigin), 20);

            progress.Message = Language.GetTextValue("Mods.AAModClassic.Common.AAWorldBuildMire");

            var (mireSurface, mireOrigin) = FindSafeBiomeOrigin(() => RollMireX(infernoSide), GetMireFootprint, GenVars.structures, "Mire");

            mirePos.X = mireSurface.X;
            mirePos.Y = mireSurface.Y;
            MireCenter = mirePos;

            MireDelete mireDelete = new();
            MireGeneration mireBiome = new();
            mireDelete.Place(mireOrigin, GenVars.structures);
            mireBiome.Place(mireOrigin, GenVars.structures);
            WorldGenUtils.AddProtectedStructure(GetMireFootprint(mireOrigin), 20);
        }

        private void BogwoodConvert(GenerationProgress progress)
        {
            if (ContentReplacementSystem.NeedToReplaceContent)
                return;

            progress.Message = Language.GetTextValue("Mods.AAModClassic.Common.AAWorldBuildMire");
            Point origin = new((int)mirePos.X, (int)mirePos.Y);
            BogwoodCon biome = new();
            biome.Place(origin, GenVars.structures);
        }

        private static void ReserveTerrarium(GenerationProgress progress)
        {
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased) && !WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                return;
            progress.Message = Language.GetTextValue("Mods.AAModClassic.Common.AAWorldBuildTerrarium");
            Point origin = new((int)(Main.maxTilesX * 0.5f), (int)(Main.maxTilesY * 0.4f));

            if (ModLoader.HasMod("Spooky"))
                origin.Y += 150;

            TexGenData Terrasphere;
            if (WorldGenUtils.GetWorldSize() == 1)
                Terrasphere = TerrariumTexGenAssets.TerrariumSmallDeletionData;
            else
                Terrasphere = TerrariumTexGenAssets.TerrariumMediumDeletionData;

            WorldGenUtils.AddProtectedStructure(new Rectangle(origin.X, origin.Y, Terrasphere.Width, Terrasphere.Height), 20);

            terrariumCenter = origin;
        }

        private static void Terrarium(GenerationProgress progress)
        {
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased) && !WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                return;
            progress.Message = Language.GetTextValue("Mods.AAModClassic.Common.AAWorldBuildTerrarium");

            new TerrariumDelete().Place(terrariumCenter, GenVars.structures);
            new TerrariumGeneration().Place(terrariumCenter, GenVars.structures);
        }

        private static void ReserveLostKeep(GenerationProgress progress)
        {
            Point origin = new((int)(Main.maxTilesX * 0.35f), (int)((float)Main.maxTilesY * 0.38f));
            if (Main.dungeonX < Main.maxTilesX / 2)
                origin = new((int)(Main.maxTilesX * 0.65f), (int)((float)Main.maxTilesY * 0.38f));

            AAWorld_Unreleased.lostKeepOrigin = LostKeepGeneration.FindValidLostKeepPosition(origin, GenVars.structures);
            WorldGenUtils.AddProtectedStructure(new Rectangle(AAWorld_Unreleased.lostKeepOrigin.X, AAWorld_Unreleased.lostKeepOrigin.Y, LostKeepTexGenAssets.KeepTileData.Width, LostKeepTexGenAssets.KeepTileData.Height), 20);
        }

        private static void LostKeep(GenerationProgress progress)
        {
            if (!WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased))
                return;
            progress.Message = Language.GetTextValue("Mods.AAModClassic.Common.AAWorldBuildLostKeep");

            new LostKeepGeneration().Place(AAWorld_Unreleased.lostKeepOrigin, GenVars.structures);
        }

        private static void Acropolis(GenerationProgress progress)
        {
            progress.Message = Language.GetTextValue("Mods.AAModClassic.Common.AAWorldBuildAcropolis");
            int height = 100;
            if(WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                height = WorldGenUtils.GetWorldSize() == 1 ? 40 : ModLoader.HasMod("Remnants") ? 75 : 100;
            Point origin = new((int)(Main.maxTilesX * 0.65f), height);
            AcropolisGeneration biome = new AcropolisGeneration();
            biome.Place(origin, GenVars.structures);
        }

        private static void Hoard(GenerationProgress progress)
        {
            progress.Message = Language.GetTextValue("Mods.AAModClassic.Common.AAWorldBuildHoard");
            Point origin = new((int)(Main.maxTilesX * (ModLoader.HasMod("Remnants") ? 0.275f : 0.3f)), (int)(Main.maxTilesY * (ModLoader.HasMod("Remnants") ?  0.75f : 0.65f)));
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased) && Main.dungeonX > Main.maxTilesX / 2)
                origin.X = (int)(Main.maxTilesX * (ModLoader.HasMod("Remnants") ? 0.675f : 0.7f));
            HoardGeneration biome = new();
            biome.Place(origin, GenVars.structures);
        }

        private static void EquinoxAlt(GenerationProgress progress)
        {
            progress.Message = Language.GetTextValue("Mods.AAModClassic.Common.AAWorldBuildEquinoxAlt");
            Point origin = new((int)(Main.maxTilesX * 0.15f), 100);
            EquinoxShrineGeneration biome = new();
            biome.Place(origin, GenVars.structures);
        }

        private static void EnderShrine()
        {
            Point origin = new((int)(Main.maxTilesX * 0.2f), (int)(Main.maxTilesY * 0.75f));
            if (Main.dungeonX > Main.maxTilesX / 2)
            {
                origin = new Point((int)(Main.maxTilesX * 0.8f), (int)(Main.maxTilesY * 0.75f));
            }
            CrystalOfMemoriesGeneration biome = new();
            biome.Place(origin, GenVars.structures);
        }

        private static void ThePit(GenerationProgress progress)
        {
            progress.Message = "Sinking the Pit";

            //Dodge Azafure, Profaned Temple and Eye Valley
            int offset = 500;
            bool dungeonRight = GenVars.dungeonX > Main.maxTilesX / 2;
            if ((ModLoader.HasMod("CalamityMod") && dungeonRight) || (ModLoader.HasMod("Spooky") && !dungeonRight) || ModLoader.HasMod("InfernumMode"))
                offset = WorldGenUtils.GetWorldSize() == 2 ? 1600 : 2000;

            if (!WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased))
            {
                Point origin = new(Main.maxTilesX - offset, Main.maxTilesY - 170);
                new PitTeaserGeneration().Place(origin, GenVars.structures);
            }
            else
            {
                Point origin = new(Main.maxTilesX - offset, Main.maxTilesY - 200);
                new PitGeneration().Place(origin, GenVars.structures);
            }
        }

        public override void ResetNearbyTileEffects()
        {
            ZAAPlayer modPlayer = Main.LocalPlayer.GetModPlayer<ZAAPlayer>();
            modPlayer.VoidUnit = false;
            modPlayer.SunAltar = false;
            modPlayer.MoonAltar = false;
            modPlayer.AkumaAltar = false;
            modPlayer.YamataAltar = false;
        }
    }
}
