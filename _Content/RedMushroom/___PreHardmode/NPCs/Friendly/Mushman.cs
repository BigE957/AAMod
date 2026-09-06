using AAModClassic._Content._Misc.__Hardmode.Items.Materials.Mushrooms;
using AAModClassic._Content._Misc._PostMoonlord.Items.Consumables;
using AAModClassic._Content.RedMushroom.___PreHardmode.Items.Consumables;
using AAModClassic._Content.RedMushroom.___PreHardmode.Items.Quest;
using AAModClassic._Content.RedMushroom.___PreHardmode.NPCs.__BossMushroomMonarch;
using AAModClassic._Content.RedMushroom.World.Biomes;
using AAModClassic._Content.RedMushroom.World.Tiles;
using AAModClassic.Achievements;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace AAModClassic._Content.RedMushroom.___PreHardmode.NPCs.Friendly
{
    [AutoloadHead]
    public class Mushman : ModNPC, ILocalizedModType
    {
        public new string LocalizationCategory => "NPCs.TownNPCs";
        //public override bool IsLoadingEnabled(Mod mod)
        //{
        //    name = "Mushman";
        //    return Mod.Properties/* tModPorter Note: _Unreleased. Instead, assign the properties directly (ContentAutoloadingEnabled, GoreAutoloadingEnabled, MusicAutoloadingEnabled, and BackgroundAutoloadingEnabled) */.Autoload;
        //}

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 26;
            NPCID.Sets.ExtraFramesCount[NPC.type] = 7;
            NPCID.Sets.AttackFrameCount[NPC.type] = 3;
            NPCID.Sets.DangerDetectRange[NPC.type] = 700;
            NPCID.Sets.AttackType[NPC.type] = 0;
            NPCID.Sets.AttackTime[NPC.type] = 40;
            NPCID.Sets.AttackAverageChance[NPC.type] = 20;
            NPCID.Sets.HatOffsetY[NPC.type] = -3;
            NPC.Happiness
                .SetBiomeAffection<RedMushroomBiome>(AffectionLevel.Love)
                .SetBiomeAffection<MushroomBiome>(AffectionLevel.Hate)
                .SetNPCAffection(NPCID.PartyGirl, AffectionLevel.Like)
                .SetNPCAffection(NPCID.Truffle, AffectionLevel.Hate);
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = NPCAIStyleID.Passive;
            NPC.damage = 40;
            NPC.defense = 38;
            NPC.lifeMax = 600;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            AnimationType = NPCID.Truffle;
            SpawnModBiomes = [ModContent.GetInstance<RedMushroomBiome>().Type];
        }

        public override bool CheckConditions(int left, int right, int top, int bottom)
        {
            if (WorldGen.roomY2 > Main.worldSurface)
            {
                return false;
            }
            int num = 0;
            int num2 = WorldGen.roomX1 - left / 2 / 16 - 1 - Lighting.OffScreenTiles;
            int num3 = WorldGen.roomX2 + left / 2 / 16 + 1 + Lighting.OffScreenTiles;
            int num4 = WorldGen.roomY1 - top / 2 / 16 - 1 - Lighting.OffScreenTiles;
            int num5 = WorldGen.roomY2 + top / 2 / 16 + 1 + Lighting.OffScreenTiles;
            if (num2 < 0)
            {
                num2 = 0;
            }
            if (num3 >= Main.maxTilesX)
            {
                num3 = Main.maxTilesX - 1;
            }
            if (num4 < 0)
            {
                num4 = 0;
            }
            if (num5 > Main.maxTilesX)
            {
                num5 = Main.maxTilesX;
            }
            for (int i = num2 + 1; i < num3; i++)
            {
                for (int j = num4 + 2; j < num5 + 2; j++)
                {
                    if (Main.tile[i, j].HasTile && (Main.tile[i, j].TileType == ModContent.TileType<Mycelium_Tile>() || Main.tile[i, j].TileType == ModContent.TileType<Mushroom_Tile>() || Main.tile[i, j].TileType == ModContent.TileType<MadnessMushroom_Tile>()))
                    {
                        num++;
                    }
                }
            }
            return num >= 100;
        }

        public override bool CanTownNPCSpawn(int numTownNPCs)/* tModPorter Suggestion: Copy the implementation of NPC.SpawnAllowed_Merchant in vanilla if you to count money, and be sure to set a flag when unlocked, so you don't count every tick. */
        {
            if (!AAConfigClient.Instance.NoAATownNPC && NPCExtensions.BeenKilled<MushroomMonarch>())
                return true;
            return false;
        }

        public override List<string> SetNPCNameList()/* tModPorter Suggestion: Return a list of names */
        {
            return null;
        }

        public override string GetChat()
        {
            MushmanEncountered.Condition.Complete();

            WeightedRandom<string> chat = new();

            int Truffle = NPC.FindFirstNPC(NPCID.Truffle);
            if (Truffle >= 0 && Main.rand.NextBool(4))
            {
                chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Mushman.MushmanChat1"));
            }
            int WitchDoctor = NPC.FindFirstNPC(NPCID.WitchDoctor);
            if (WitchDoctor >= 0 && Main.rand.NextBool(4))
            {
                return Main.npc[WitchDoctor].GivenName + Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Mushman.MushmanChat2");
            }
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Mushman.MushmanChat3"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Mushman.MushmanChat4"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Mushman.MushmanChat5"));
            int Clothier = NPC.FindFirstNPC(NPCID.Clothier);
            if (Clothier >= 0 && Main.rand.NextBool(4))
            {
                return Main.npc[Clothier].GivenName + Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Mushman.MushmanChat6");
            }
            return chat; 
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Mushman.button1");
            button2 = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Mushman.button2");
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
            {
                shopName = "Shop";
            }

            if (!firstButton)
            {
                SoundEngine.PlaySound(SoundID.MenuTick);

                Player player = Main.LocalPlayer;

                int Special = player.FindItem(ModContent.ItemType<MadnessMushroom>());
                int Item = player.FindItem(ItemID.StrangePlant1);
                int Item2 = player.FindItem(ItemID.StrangePlant2);
                int Item3 = player.FindItem(ItemID.StrangePlant3);
                int Item4 = player.FindItem(ItemID.StrangePlant4);

                int DyeRed = player.FindItem(ItemID.RedHusk);
                int DyeOrange = player.FindItem(ItemID.OrangeBloodroot);
                int DyeYellow = player.FindItem(ItemID.YellowMarigold);
                int DyeGreen1 = player.FindItem(ItemID.GreenMushroom);
                int DyeGreen2 = player.FindItem(ItemID.LimeKelp);
                int DyeGreen3 = player.FindItem(ItemID.TealMushroom);
                int DyeBlue1 = player.FindItem(ItemID.CyanHusk);
                int DyeBlue2 = player.FindItem(ItemID.SkyBlueFlower);
                int DyeBlue3 = player.FindItem(ItemID.BlueBerries);
                int DyePurple1 = player.FindItem(ItemID.PurpleMucos);
                int DyePurple2 = player.FindItem(ItemID.VioletHusk);
                int DyePink = player.FindItem(ItemID.PinkPricklyPear);
                int DyeGray = player.FindItem(ItemID.BlackInk);

                int[] lootTable = 
                { 
                    ModContent.ItemType<RedAlchemicalMushroom>(),
                    ModContent.ItemType<OrangeAlchemicalMushroom>(),
                    ModContent.ItemType<YellowAlchemicalMushroom>(),
                    ModContent.ItemType<GreenAlchemicalMushroom>(),
                    ModContent.ItemType<BlueAlchemicalMushroom>(),
                    ModContent.ItemType<PurpleAlchemicalMushroom>(),
                    ModContent.ItemType<GrayAlchemicalMushroom>(),
                    ModContent.ItemType<PinkAlchemicalMushroom>(),
                    ModContent.ItemType<BrownAlchemicalMushroom>(),
                };
                int loot = Main.rand.Next(lootTable.Length);

                if (Special >= 0)
                {
                    player.inventory[Special].stack--;
                    if (player.inventory[Special].stack <= 0)
                    {
                        player.inventory[Special] = new Item();
                    }

                    Main.npcChatText = SpecialChat();
                    player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ModContent.ItemType<RainbowMushroom>(), 5);

                    SoundEngine.PlaySound(SoundID.Chat);
                    return;
                }
                else if (Item >= 0)
                {
                    player.inventory[Item].stack--;
                    if (player.inventory[Item].stack <= 0)
                    {
                        player.inventory[Item] = new Item();
                    }

                    Main.npcChatText = MushroomChat();
                    player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), lootTable[loot], 5);

                    SoundEngine.PlaySound(SoundID.Chat);
                    return;
                }
                else if (Item2 >= 0)
                {
                    player.inventory[Item2].stack--;
                    if (player.inventory[Item2].stack <= 0)
                    {
                        player.inventory[Item2] = new Item();
                    }

                    Main.npcChatText = MushroomChat();
                    player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), lootTable[loot], 5);

                    SoundEngine.PlaySound(SoundID.Chat);
                    return;
                }
                else if (Item3 >= 0)
                {
                    player.inventory[Item3].stack--;
                    if (player.inventory[Item3].stack <= 0)
                    {
                        player.inventory[Item3] = new Item();
                    }

                    Main.npcChatText = MushroomChat();
                    player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), lootTable[loot], 5);

                    SoundEngine.PlaySound(SoundID.Chat);
                    return;
                }
                else if (Item4 >= 0)
                {
                    player.inventory[Item4].stack--;
                    if (player.inventory[Item4].stack <= 0)
                    {
                        player.inventory[Item4] = new Item();
                    }

                    Main.npcChatText = MushroomChat();
                    player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), lootTable[loot], 5);

                    SoundEngine.PlaySound(SoundID.Chat);
                    return;
                }
                else if (DyeRed >= 0)
                {
                    player.inventory[DyeRed].stack--;
                    if (player.inventory[DyeRed].stack <= 0)
                    {
                        player.inventory[DyeRed] = new Item();
                    }

                    Main.npcChatText = MushroomChat();
                    player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ModContent.ItemType<RedAlchemicalMushroom>(), 5);

                    SoundEngine.PlaySound(SoundID.Chat);
                    return;
                }
                else if (DyeOrange >= 0)
                {
                    player.inventory[DyeOrange].stack--;
                    if (player.inventory[DyeOrange].stack <= 0)
                    {
                        player.inventory[DyeOrange] = new Item();
                    }

                    Main.npcChatText = MushroomChat();
                    player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ModContent.ItemType<OrangeAlchemicalMushroom>(), 5);

                    SoundEngine.PlaySound(SoundID.Chat);
                    return;
                }
                else if (DyeYellow >= 0)
                {
                    player.inventory[DyeYellow].stack--;
                    if (player.inventory[DyeYellow].stack <= 0)
                    {
                        player.inventory[DyeYellow] = new Item();
                    }

                    Main.npcChatText = MushroomChat();
                    player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ModContent.ItemType<YellowAlchemicalMushroom>(), 5);

                    SoundEngine.PlaySound(SoundID.Chat);
                    return;
                }
                else if (DyeGreen1 >= 0)
                {
                    player.inventory[DyeGreen1].stack--;
                    if (player.inventory[DyeGreen1].stack <= 0)
                    {
                        player.inventory[DyeGreen1] = new Item();
                    }

                    Main.npcChatText = MushroomChat();
                    player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ModContent.ItemType<GreenAlchemicalMushroom>(), 5);

                    SoundEngine.PlaySound(SoundID.Chat);
                    return;
                }
                else if (DyeGreen2 >= 0)
                {
                    player.inventory[DyeGreen2].stack--;
                    if (player.inventory[DyeGreen2].stack <= 0)
                    {
                        player.inventory[DyeGreen2] = new Item();
                    }

                    Main.npcChatText = MushroomChat();
                    player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ModContent.ItemType<GreenAlchemicalMushroom>(), 5);

                    SoundEngine.PlaySound(SoundID.Chat);
                    return;
                }
                else if (DyeGreen3 >= 0)
                {
                    player.inventory[DyeGreen3].stack--;
                    if (player.inventory[DyeGreen3].stack <= 0)
                    {
                        player.inventory[DyeGreen3] = new Item();
                    }

                    Main.npcChatText = MushroomChat();
                    player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ModContent.ItemType<GreenAlchemicalMushroom>(), 5);

                    SoundEngine.PlaySound(SoundID.Chat);
                    return;
                }
                else if (DyeBlue1 >= 0)
                {
                    player.inventory[DyeBlue1].stack--;
                    if (player.inventory[DyeBlue1].stack <= 0)
                    {
                        player.inventory[DyeBlue1] = new Item();
                    }

                    Main.npcChatText = MushroomChat();
                    player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ModContent.ItemType<BlueAlchemicalMushroom>(), 5);

                    SoundEngine.PlaySound(SoundID.Chat);
                    return;
                }
                else if (DyeBlue2 >= 0)
                {
                    player.inventory[DyeBlue2].stack--;
                    if (player.inventory[DyeBlue2].stack <= 0)
                    {
                        player.inventory[DyeBlue2] = new Item();
                    }

                    Main.npcChatText = MushroomChat();
                    player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ModContent.ItemType<BlueAlchemicalMushroom>(), 5);

                    SoundEngine.PlaySound(SoundID.Chat);
                    return;
                }
                else if (DyeBlue3 >= 0)
                {
                    player.inventory[DyeBlue3].stack--;
                    if (player.inventory[DyeBlue3].stack <= 0)
                    {
                        player.inventory[DyeBlue3] = new Item();
                    }

                    Main.npcChatText = MushroomChat();
                    player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ModContent.ItemType<BlueAlchemicalMushroom>(), 5);

                    SoundEngine.PlaySound(SoundID.Chat);
                    return;
                }
                else if (DyePurple1 >= 0)
                {
                    player.inventory[DyePurple1].stack--;
                    if (player.inventory[DyePurple1].stack <= 0)
                    {
                        player.inventory[DyePurple1] = new Item();
                    }

                    Main.npcChatText = MushroomChat();
                    player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ModContent.ItemType<PurpleAlchemicalMushroom>(), 5);

                    SoundEngine.PlaySound(SoundID.Chat);
                    return;
                }
                else if (DyePurple2 >= 0)
                {
                    player.inventory[DyePurple2].stack--;
                    if (player.inventory[DyePurple2].stack <= 0)
                    {
                        player.inventory[DyePurple2] = new Item();
                    }

                    Main.npcChatText = MushroomChat();
                    player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ModContent.ItemType<PurpleAlchemicalMushroom>(), 5);

                    SoundEngine.PlaySound(SoundID.Chat);
                    return;
                }
                else if (DyeGray >= 0)
                {
                    player.inventory[DyeGray].stack--;
                    if (player.inventory[DyeGray].stack <= 0)
                    {
                        player.inventory[DyeGray] = new Item();
                    }

                    Main.npcChatText = MushroomChat();
                    player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ModContent.ItemType<GrayAlchemicalMushroom>(), 5);

                    SoundEngine.PlaySound(SoundID.Chat);
                    return;
                }
                else if (DyePink >= 0)
                {
                    player.inventory[DyePink].stack--;
                    if (player.inventory[DyePink].stack <= 0)
                    {
                        player.inventory[DyePink] = new Item();
                    }

                    Main.npcChatText = MushroomChat();
                    player.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ModContent.ItemType<PinkAlchemicalMushroom>(), 5);

                    SoundEngine.PlaySound(SoundID.Chat);
                    return;
                }
                else
                {
                    Main.npcChatText = NoMushroomChat();
                    Main.npcChatCornerItem = 0;
                    SoundEngine.PlaySound(SoundID.MenuTick);
                }
            }
        }

        public static string NoMushroomChat()
        {
            WeightedRandom<string> chat = new();
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Mushman.NoMushroomChat1"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Mushman.NoMushroomChat2"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Mushman.NoMushroomChat3"));
            return chat;
        }

        public static string SpecialChat()
        {
            WeightedRandom<string> chat = new();
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Mushman.SpecialChat1"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Mushman.SpecialChat2"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Mushman.SpecialChat3"));
            return chat;
        }

        public static string MushroomChat()
        {
            WeightedRandom<string> chat = new();
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Mushman.MushroomChat1"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Mushman.MushroomChat2"));
            chat.Add(Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.Mushman.MushroomChat3"));
            return chat;
        }

        public override void AddShops()
        {
            NPCShop shop = new(Type);
            shop.Add(ItemID.Mushroom);
            shop.Add(ItemID.GlowingMushroom);
            shop.Add<SporeBag>();
            shop.Add(ItemID.RecallPotion);
            shop.Add(ItemID.MushroomGrassSeeds);
            shop.Add<MyceliumSeeds>();
            shop.Add(ItemID.LesserHealingPotion);
            shop.Add(ItemID.LesserManaPotion);

            shop.Add(ItemID.HealingPotion, Condition.DownedSkeletron);
            shop.Add(ItemID.ManaPotion, Condition.DownedSkeletron);

            shop.Add(ItemID.GreaterHealingPotion, Condition.Hardmode);
            shop.Add(ItemID.GreaterManaPotion, Condition.Hardmode);

            shop.Add(ItemID.SuperHealingPotion, Condition.DownedMoonLord);
            shop.Add(ItemID.SuperManaPotion, Condition.DownedMoonLord);

            //TODO: Give these text
            shop.Add<GrandHealingPotion>(AAConditions.DownedAnyLateAncient);
            shop.Add<GrandManaPotion>(AAConditions.DownedAnyLateAncient);

            shop.Add<TheBigOne>(AAConditions.DownedAnySuperancient);

            shop.Register();
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Mushroom));
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			damage = 20;
			knockback = 4f;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			cooldown = 20;
			randExtraCooldown = 20;
		}

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ModContent.ProjectileType<Mushman_Throwshroom>();
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)

        {
            multiplier = 4f;

            randomOffset = 2f;

        }
    }
}