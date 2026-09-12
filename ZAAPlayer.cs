using AAModClassic._Content._Dev.___PreHardmode.Items.Materials;
using AAModClassic._Content._Dev.__Hardmode.Items.Accessories;
using AAModClassic._Content._Dev.__Hardmode.Items.Armor.Vanity;
using AAModClassic._Content._Dev.__Hardmode.Items.Consumables;
using AAModClassic._Content._Dev.__Hardmode.Items.Mounts;
using AAModClassic._Content._Dev.__Hardmode.Items.Weapons;
using AAModClassic._Content._Dev._PostMoonlord.Items.Tools;
using AAModClassic._Content._Dev._PostMoonlord.Items.Weapons;
using AAModClassic._Content._EX._PostMoonlord.Items.Accessories;
using AAModClassic._Content._EX._PostMoonlord.Items.Weapons;
using AAModClassic._Content._Misc.___PreHardmode.Items.Accessories.Vanity;
using AAModClassic._Content._Misc.___PreHardmode.Items.Consumables;
using AAModClassic._Content._Tinker.___PreHardmode.Items.Armor;
using AAModClassic._Content._Tinker.__Hardmode.Items.Accessories;
using AAModClassic._Content._Tinker._PostMoonlord.Items.Accessories;
using AAModClassic._Content.Acropolis.World.Biomes;
using AAModClassic._Content.Bunny._PostMoonlord.Items.Armor;
using AAModClassic._Content.Chaos.__Hardmode.Items.Armor;
using AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Armor;
using AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Weapons;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossSistersOfDiscord.Ashe;
using AAModClassic._Content.Chaos.Buffs;
using AAModClassic._Content.Desert.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic._Content.Desert.__Hardmode.Items._BossAnubis.Accessories;
using AAModClassic._Content.Desert._PostMoonlord.Items._BossAnubisA.Accessories;
using AAModClassic._Content.Desert._PostMoonlord.Items._BossAnubisA.Weapons;
using AAModClassic._Content.Hell.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic._Content.Hoard.World.Biomes;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Consumables;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Quest;
using AAModClassic._Content.Inferno.__Hardmode.Items.Consumables;
using AAModClassic._Content.Inferno.__Hardmode.Items.Tiles.Functional;
using AAModClassic._Content.Inferno.__Hardmode.Items.Tools;
using AAModClassic._Content.Inferno.__Hardmode.NPCs._Underground;
using AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Accessories;
using AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma;
using AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma.Awakened;
using AAModClassic._Content.Inferno.Buffs;
using AAModClassic._Content.Inferno.World.Biomes;
using AAModClassic._Content.Mire.___PreHardmode.Items.Consumables;
using AAModClassic._Content.Mire.___PreHardmode.Items.Quest;
using AAModClassic._Content.Mire.__Hardmode.Items.Consumables;
using AAModClassic._Content.Mire.__Hardmode.Items.Tiles.Functional;
using AAModClassic._Content.Mire.__Hardmode.Items.Weapons;
using AAModClassic._Content.Mire.__Hardmode.NPCs._Underground;
using AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.Accessories;
using AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata;
using AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata.Awakened;
using AAModClassic._Content.Mire.Buffs;
using AAModClassic._Content.Mire.World.Biomes;
using AAModClassic._Content.RedMushroom.World.Biomes;
using AAModClassic._Content.Snow.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic._Content.Stars._PostMoonlord.NPCs.__BossEquinoxWorms.Daybringer;
using AAModClassic._Content.Stars._PostMoonlord.NPCs.__BossEquinoxWorms.Nightcrawler;
using AAModClassic._Content.Stars.World.Biomes;
using AAModClassic._Content.SunkenShip.__PreHardmode.Items.Tools;
using AAModClassic._Content.Terrarium.Buffs;
using AAModClassic._Content.Terrarium.World.Biomes;
using AAModClassic._Content.Void.___PreHardmode.Items._BossSagittarius.Accessories;
using AAModClassic._Content.Void.___PreHardmode.Items.Consumables;
using AAModClassic._Content.Void.___PreHardmode.Items.Quest;
using AAModClassic._Content.Void.__Hardmode.Items.Consumables;
using AAModClassic._Content.Void._PostMoonlord.Items.Accessories.Vanity;
using AAModClassic._Content.Void._PostMoonlord.NPCs.__BossZero.Awakened;
using AAModClassic._Content.Void.World.Biomes;
using AAModClassic._Unofficial.Content._Dev.__Hardmode.Items.Consumables;
using AAModClassic._Unreleased.Content.Parthenan.World.Biomes;
using AAModClassic._Unreleased.Content.SunkenShip.World.Biomes;
using AAModClassic._Unreleased.Content.Void.Buffs;
using AAModClassic.Achievements;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Buffs;
using AAModClassic.Dusts;
using AAModClassic.Globals;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.Attributes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Events;
using Terraria.GameInput;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;

namespace AAModClassic
{
    public partial class ZAAPlayer : ModPlayer
    {
        #region Variables

        #region Minions
        public bool FireSpirit = false;
        public bool ImpServant = false;
        public bool ImpSlave = false;
        public bool MoonBee = false;
        public bool enderMinion = false;
        public bool enderMinionEX = false;
        public bool LungMinion = false;
        public bool DragonMinion = false;
        public bool BabyPhoenix = false;
        public bool GripMinion = false;
        public bool ProbeMinion = false;
        public bool SkullMinion = false;
        public bool EaterMinion = false;
        public bool CrimeraMinion = false;
        public bool CrowMinion = false;
        public bool DemonMinion = false;
        public bool DevilMinion = false;
        public bool DoomiteProbe = false;
        public bool DoomiteProbeC = false;
        public bool TerraMinion = false;
        public bool HallowedPrism = false;
        public bool TrueHallowedPrism = false;
        public bool SnakeMinion = false;
        public bool dustDevil = false;
        public bool KrakenMinion = false;
        public bool Fishnado = false;
        public bool MadnessElemental = false;
        public bool Orbiters = false;
        public bool Protocol = false;
        public bool ScoutMinion = false;
        public bool SagOrbiter = false;
        public bool Rabbitcopter = false;
        public bool RabbitcopterR = false;
        public bool Sock = false;
        public bool Socc = false;
        public bool Squirrel = false;
        public bool DapperSquirrel = false;
        public bool CyberClaw = false;
        public bool ChaosClaw = false;
        public bool MiniZero = false;
        public bool TerraSummon = false;
        public bool DragonSpirit = false;
        public bool Xiao = false;
        public bool ChaosConstruct = false;
        #endregion

        #region Biome bools.
        public bool ZoneMire => Player.InModBiome<MireBiome>();
        public bool ZoneInferno => Player.InModBiome<InfernoBiome>();
        public bool ZoneVoid => Player.InModBiome<VoidBiome>();
        public bool ZoneMush => Player.InModBiome<RedMushroomBiome>();
        public bool ZoneStorm => Player.InModBiome<ParthenanBiome>();
        public bool ZoneRisingSunPagoda => Player.InModBiome<RisingSunPagodaBiome>();
        public bool ZoneRisingMoonLake => Player.InModBiome<RisingMoonLakeBiome>();
        public bool ZoneShip => Player.InModBiome<SunkenShipBiome>();
        public bool ZoneTerrarium => Player.InModBiome<TerrariumBiome>();
        public bool ZoneStars => Player.InModBiome<StarsBiome>();
        public bool ZoneHoard => Player.InModBiome<HoardBiome>();
        public bool ZoneAcropolis => Player.InModBiome<AcropolisBiome>();
        public bool VoidUnit = false;
        public bool SunAltar = false;
        public bool MoonAltar = false;
        public bool AkumaAltar = false;
        public bool YamataAltar = false;
        public bool AshCurse;
        public static int Ashes = 0;
        #endregion

        #region Armor bools.
        public bool MoonSet;
        public bool leatherSet; // TODO: WE NEED TO FIND THIS
        public bool darkmatterSetMe;
        public bool darkmatterSetRa;
        public bool darkmatterSetMa;
        public bool darkmatterSetSu;
        public bool darkmatterSetTh;
        public bool DarkmatterSet;
        public bool valkyrieSet;
        public bool Alpha;
        public bool Radium;

        public bool ChaosRa2 = false;
        public bool ChaosMa = false;

        public bool onoPrevious;
        public bool ono;
        public bool onoHideVanity;
        public bool onoForceVanity;

        public bool AsheFlame;
        public float AsheFlameScale = 0f;
        #endregion

        #region Accessory bools
        public int AbilityCD = 180;
        public bool DragonShell;
        public bool RStar;
        public bool DVoid;
        public bool DiscordShredder;
        public bool HeartA = false;

        public bool SpellBookofRagnarok;
        #endregion

        #region debuffs
        public bool CursedHellfire = false;
        public bool infinityOverload = false;
        public bool discordInferno = false;
        public bool dragonFire = false;
        public bool hydraToxin = false;
        public bool terraBlaze = false;
        public bool Clueless = false;
        public bool InfinityScorch = false;
        public bool LockedOn = false;
        public bool shroomed = false;
        public bool riftbent = false;
        public bool DestinedToDie = false;
        public bool YamataGravity = false;
        public bool YamataAGravity = false;
        public bool Hunted = false;
        public bool Spear = false;
        public bool AkumaPain = false;
        public bool FFlames = false;
        #endregion

        #region buffs

        public bool Ronin = false;

        public bool luckycalm = false;
        public bool luckythorns = false;
        public bool StripeCrasyLucky = false;
        public bool CrasyLucky = false;
        #endregion

        #region pets
        public bool Broodmini = false;
        public bool Raidmini = false;
        public bool MiniProbe = false;
        public bool Sharkron = false;
        public bool RoyalKitten = false;
        public bool Mudkip = false;
        public bool MudkipS = false;
        public bool BoomBoi = false;
        public bool DragonSoul = false;
        public bool Glowmoss = false;
        public bool Cerberus = false;
        public bool K9 = false;
        public bool Lunamini = false;
        public bool ZeroBab = false;
        #endregion

        #region Colors
        public static Color IncineriteColor = new Color((int)(242 * 0.7f), (int)(107 * 0.7f), 0);
        public static Color ZeroColor = new Color((int)(233 * 0.7f), (int)(53 * 0.7f), (int)(53 * 0.7f));
        public static Color groviteColor = new Color(138, (int)(39 * 0.7f), (int)(196 * 0.7f));
        public static bool[] groviteGlow = new bool[255];

        public static int IZKills = 0;
        #endregion

        #region Misc
        public Vector2 RiftPos = new Vector2(0, 0);
        public bool WorldgenReminder = false;
        public bool NewAAReminder = false;
        public bool DemonSun = false;
        public bool AnubisBook = false;
        public bool GivenAnuSummon = false;
        public bool GivenWormIdol = false;

        public float MaxMovespeedboost = 0;
        #endregion

        #endregion

        #region Save/Load
        public override void SaveData(TagCompound tag)/* tModPorter Suggestion: Edit tag parameter instead of returning new TagCompound */
        {
            var saved = new List<string>();
            if (AnubisBook) saved.Add("Book");
            if (GivenAnuSummon) saved.Add("Stick");
            if (GivenWormIdol) saved.Add("Idol");
            tag.Add("saved", saved);
            tag.Add("izKills", IZKills);
        }

        public override void LoadData(TagCompound tag)
        {
            var downed = tag.GetList<string>("saved");
            AnubisBook = downed.Contains("Book");
            GivenAnuSummon = downed.Contains("Stick");
            GivenWormIdol = downed.Contains("Idol");
            IZKills = tag.GetInt("izKills");
        }

        #endregion

        #region Reset Effects

        public override void ResetEffects()
        {
            ResetMinionEffect();
            ResetArmorEffect();
            ResetAccessoryEffect();
            ResetBuffEffect();
            ResetDebuffEffect();
            ResetPetsEffect();

            MaxMovespeedboost = 0;

            //EnemyChecks
            ResetMiscEffect();
        }

        private void ResetMiscEffect()
        {
            DemonSun = false;
            onoPrevious = ono;
            ono = onoHideVanity = onoForceVanity = false;
        }

        private void ResetMinionEffect()
        {
            FireSpirit = false;
            ImpServant = false;
            ImpSlave = false;
            MoonBee = false;
        
            enderMinion = false;
            enderMinionEX = false;
            BabyPhoenix = false;
            LungMinion = false;
            DragonMinion = false;
            GripMinion = false;
            ProbeMinion = false;
            SkullMinion = false;
            EaterMinion = false;
            CrimeraMinion = false;
            CrowMinion = false;
            DemonMinion = false;
            DevilMinion = false;
            DoomiteProbe = false;
            DoomiteProbeC = false;
            HallowedPrism = false;
            TrueHallowedPrism = false;
            TerraMinion = false;
            SnakeMinion = false;
            dustDevil = false;
            KrakenMinion = false;
            Fishnado = false;
            MadnessElemental = false;
            Orbiters = false;
            Protocol = false;
            ScoutMinion = false;
            SagOrbiter = false;
            Rabbitcopter = false;
            RabbitcopterR = false;
            Sock = false;
            Socc = false;
            Squirrel = false;
            DapperSquirrel = false;
            CyberClaw = false;
            ChaosClaw = false;
            MiniZero = false;
            TerraSummon = false;
            DragonSpirit = false;
            Xiao = false;
            ChaosConstruct = false;
        }

        private void ResetArmorEffect()
        {
            MoonSet = false;
            valkyrieSet = false;
            darkmatterSetMe = false;
            darkmatterSetRa = false;
            darkmatterSetMa = false;
            darkmatterSetSu = false;
            darkmatterSetTh = false;
            Alpha = false;
            DarkmatterSet = false;
            AsheFlame = false;
            ChaosRa2 = false;
            ChaosMa = false;
        }

        private void ResetAccessoryEffect()
        {
            AshCurse = !Main.dayTime && !AAWorld.downedAkuma;
            DiscordShredder = false;
            RStar = false;
            DVoid = false;
            HeartA = false;
            SpellBookofRagnarok = false;
        }

        private void ResetBuffEffect()
        {
            Ronin = false;
            luckycalm = false;
            luckythorns = false;
            CrasyLucky = false;
        }

        private void ResetDebuffEffect()
        {
            CursedHellfire = false;
            infinityOverload = false;
            discordInferno = false;
            dragonFire = false;
            hydraToxin = false;
            terraBlaze = false;
            Clueless = false;
            InfinityScorch = false;
            LockedOn = false;
            shroomed = false;
            riftbent = false;
            DestinedToDie = false;
            YamataGravity = false;
            YamataAGravity = false;
            Hunted = false;
            Spear = false;
            AkumaPain = false;
            FFlames = false;
        }

        private void ResetPetsEffect()
        {
            Broodmini = false;
            Raidmini = false;
            MiniProbe = false;
            Sharkron = false;
            RoyalKitten = false;
            Mudkip = false;
            MudkipS = false;
            BoomBoi = false;
            DragonSoul = false;
            Glowmoss = false;
            Cerberus = false;
            K9 = false;
            Lunamini = false;
            ZeroBab = false;
        }

        public override void Initialize()
        {
            AbilityCD = 0;
            WorldgenReminder = false;
            NewAAReminder = false;
        }

        #endregion

        #region Hit Effects

        public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers) 
		{
			if (npc.HasBuff(ModContent.BuffType<Lifeline_ForsakenWeak>()))
			{
                modifiers.FinalDamage.Flat -= modifiers.FinalDamage.Flat / 5;
			}

            if(luckythorns)
            {
                if (Player.whoAmI == Main.myPlayer && !Player.immune && !npc.dontTakeDamage)
                {
                    int RDamage = (int)(Player.GetDamage(DamageClass.Generic).ApplyTo(npc.damage) * 0.433f);
                    int direc = -1;
                    if (npc.position.X + npc.width / 2 < Player.position.X + Player.width / 2)
                    {
                        direc = 1;
                    }
                    Player.ApplyDamageToNPC(npc, RDamage, 10f, -direc, false);
                }
            }
		}

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)/* tModPorter If you don't need the Projectile, consider using OnHitNPC instead */
        {
            if (target.HasBuff(ModContent.BuffType<Lifeline_Forsaken>()) && proj.type == ModContent.ProjectileType<Lifeline_EnchancedMummyArrow>())
            {
				float num1 = 9f;
				Vector2 vector2 = new Vector2(Player.position.X + Player.width * 0.5f, Player.position.Y + Player.height * 0.5f);
				float f1 = target.Center.X - vector2.X;
				float f2 = target.Center.Y - vector2.Y;
				float num4 = (float)Math.Sqrt(f1 * (double)f1 + f2 * (double)f2);
				float num5;
				if (float.IsNaN(f1) && float.IsNaN(f2) || f1 == 0.0 && f2 == 0.0)
				{
					f1 = Player.direction;
					f2 = 0.0f;
					num5 = num1;
				}
				else
					num5 = num1 / num4;
				float SpeedX = f1 * num5;
				float SpeedY = f2 * num5;
                Vector2 velocity = new Vector2(SpeedX, SpeedY);

                float numberProjectiles = 3;
				float rotation = MathHelper.ToRadians(3);
				vector2 += Vector2.Normalize(velocity) * 45f;
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 1f;
					Projectile.NewProjectile(target.GetSource_OnHurt(proj), vector2.X, vector2.Y, perturbedSpeed.X*2, perturbedSpeed.Y*2, ModContent.ProjectileType<Lifeline_ForsakenArrow>(), damageDone / 2, proj.knockBack, Player.whoAmI);
				}
				target.buffImmune[ModContent.BuffType<Lifeline_Forsaken>()] = true;
			}
        }

        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            if (npc.type == NPCID.GoblinArcher
                || npc.type == NPCID.GoblinPeon
                || npc.type == NPCID.GoblinScout
                || npc.type == NPCID.GoblinSorcerer
                || npc.type == NPCID.GoblinSummoner
                || npc.type == NPCID.GoblinThief
                || npc.type == NPCID.GoblinWarrior
                || npc.type == NPCID.DD2GoblinBomberT1
                || npc.type == NPCID.DD2GoblinBomberT2
                || npc.type == NPCID.DD2GoblinBomberT3
                || npc.type == NPCID.DD2GoblinT1
                || npc.type == NPCID.DD2GoblinT2
                || npc.type == NPCID.DD2GoblinBomberT3
                || npc.type == NPCID.BoundGoblin
                || npc.type == NPCID.GoblinTinkerer)
            {
                Player.endurance += .8f;
            }
        }

        public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)/* tModPorter If you don't need the Item, consider using ModifyHitNPC instead */
        {
            if (valkyrieSet)
            {
                target.AddBuff(BuffID.Frostburn, 180);
                target.AddBuff(BuffID.Chilled, 180);
            }

            if (darkmatterSetMe)
            {
                target.AddBuff(ModContent.BuffType<Electrified_Buff>(), 500);
            }

            if (DiscordShredder)
            {
                Player.ApplyDamageToNPC(target, 30, 0, 0, false);
                target.AddBuff(ModContent.BuffType<DiscordianInferno_Buff>(), 300);
            }

            if (Alpha && !target.boss)
            {
                target.AddBuff(BuffID.Wet, 600);
            }

            if (Player.HasBuff(ModContent.BuffType<FlaskOfDragonfire_Buff>()))
            {
                target.AddBuff(ModContent.BuffType<DragonFire_Buff>(), 900);
            }

            if (Player.HasBuff(ModContent.BuffType<FlaskOfHydratoxin_Buff>()))
            {
                target.AddBuff(ModContent.BuffType<HydraToxin_Buff>(), 900);
            }
        }


        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)/* tModPorter If you don't need the Projectile, consider using ModifyHitNPC instead */
        {
            if (proj.CountsAsClass(DamageClass.Melee))
            {
                if (valkyrieSet)
                {
                    target.AddBuff(BuffID.Frostburn, 180);
                    target.AddBuff(BuffID.Chilled, 180);
                }

                if (darkmatterSetMe)
                {
                    target.AddBuff(ModContent.BuffType<Electrified_Buff>(), 500);
                }

                if (Player.HasBuff(ModContent.BuffType<FlaskOfDragonfire_Buff>()))
                {
                    target.AddBuff(ModContent.BuffType<DragonFire_Buff>(), 900);
                }

                if (Player.HasBuff(ModContent.BuffType<FlaskOfHydratoxin_Buff>()))
                {
                    target.AddBuff(ModContent.BuffType<HydraToxin_Buff>(), 900);
                }
            }

            if (proj.CountsAsClass(DamageClass.Ranged))
            {
                if (darkmatterSetRa)
                {
                    target.AddBuff(ModContent.BuffType<Electrified_Buff>(), 500);
                }
            }

            if (proj.CountsAsClass(DamageClass.Magic))
            {
                if (MoonSet)
                {
                    target.AddBuff(ModContent.BuffType<Moonraze_Buff>(), 300);
                }

                if (darkmatterSetMa)
                {
                    target.AddBuff(ModContent.BuffType<Electrified_Buff>(), 500);
                }

                if (ChaosMa)
                {
                    string buffName = Main.rand.NextBool(2) ? "DragonFire" : "HydraToxin";
                    target.AddBuff(Mod.Find<ModBuff>(buffName).Type, 180);
                }
            }

            if (proj.minion)
            {
                if (darkmatterSetSu)
                {
                    target.AddBuff(ModContent.BuffType<Electrified_Buff>(), 500);
                }
            }

            if (proj.CountsAsClass(DamageClass.Throwing))
            {
                if (darkmatterSetTh)
                {
                    target.AddBuff(ModContent.BuffType<Electrified_Buff>(), 500);
                }

                if (Alpha && Main.rand.NextBool(2) && !target.boss)
                {
                    target.AddBuff(BuffID.Wet, 500);
                }
            }

            if (DiscordShredder)
            {
                Player.ApplyDamageToNPC(target, 30, 0, 0, false);
                target.AddBuff(ModContent.BuffType<DiscordianInferno_Buff>(), 300);
            }
        }

        #endregion

        public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
        {
            int liquidType = 0;
            if (attempt.inLava)
                liquidType = 1;
            if (attempt.inHoney)
                liquidType = 2;

            if (npcSpawn > 0)
                return;

            if (itemDrop == ItemID.OldShoe || itemDrop == ItemID.FishingSeaweed || itemDrop == ItemID.TinCan || itemDrop == ItemID.JojaCola)
                return;

            if (attempt.crate)
            {
                if (attempt.rare)
                {
                    if ((liquidType == 0 || liquidType == 1) && Player.GetModPlayer<ZAAPlayer>().ZoneInferno)
                        itemDrop = Main.hardMode ? ModContent.ItemType<DaybreakCrate>() : ModContent.ItemType<InfernoCrate>();
                    if (liquidType == 0 && Player.GetModPlayer<ZAAPlayer>().ZoneMire)
                        itemDrop = Main.hardMode ? ModContent.ItemType<EventideCrate>() : ModContent.ItemType<MireCrate>();
                    if (liquidType == 0 && Player.GetModPlayer<ZAAPlayer>().ZoneVoid)
                        itemDrop = Main.hardMode ? ModContent.ItemType<NullCrate>() : ModContent.ItemType<VoidCrate>();
                    if (liquidType == 0 && Player.GetModPlayer<ZAAPlayer>().ZoneHoard)
                        itemDrop = ItemID.GoldenCrate; // intended. greed only likes gold
                }
                return;
            }

            if (attempt.questFish == ModContent.ItemType<TriHeadedKoi>() && Player.GetModPlayer<ZAAPlayer>().ZoneMire && attempt.uncommon)
                itemDrop = ModContent.ItemType<TriHeadedKoi>();
            if (attempt.questFish == ModContent.ItemType<Fishmother>() && Player.GetModPlayer<ZAAPlayer>().ZoneInferno && attempt.uncommon)
                itemDrop = ModContent.ItemType<Fishmother>();
            if (attempt.questFish == ModContent.ItemType<GlitchFish>() && Player.GetModPlayer<ZAAPlayer>().ZoneVoid && attempt.uncommon)
                itemDrop = ModContent.ItemType<GlitchFish>();

            if (Player.GetModPlayer<ZAAPlayer>().ZoneInferno)
            {
                if(attempt.legendary)
                {
                    if (!Main.hardMode || Main.rand.NextBool())
                        itemDrop = ModContent.ItemType<SharpeningLavaFish>();
                    else
                        itemDrop = ModContent.ItemType<ScorchShark>();
                }
            }

            if (Player.GetModPlayer<ZAAPlayer>().ZoneMire)
            {
                if (attempt.legendary)
                {
                    if (Main.rand.NextBool())
                        itemDrop = ModContent.ItemType<ToxinMonkfish>();
                    else
                        itemDrop = ModContent.ItemType<SwimmingHydra>();
                }
            }

            if ((Main.rand.NextBool(4096) && liquidType == 0 && Player.fishingSkill >= 100) || (Main.rand.NextBool(2048) && Player.accFishingLine && Player.accTackleBox))
            {
                itemDrop = ModContent.ItemType<ShinyCharmFish>();
            }
        }

        public int[] Charges = null;
        public int[] Spheres = null;

        public float ShieldScale = 0;
        public float RingRotation = 0;

        public float TimeScale = 0;

        public override void PostUpdate()
        {
            if (Ronin)
            {
                Player.immune = true;
                Player.immuneTime = 60;
            }

            DarkmatterSet = darkmatterSetMe || darkmatterSetRa || darkmatterSetMa || darkmatterSetSu || darkmatterSetTh;

            bool anyAkumaTransition = false;
            bool anyAkumaA = false;
            bool anyEquinox = false;
            bool anyZeroA = false;

            foreach (NPC n in Main.ActiveNPCs)
            {
                if (n.type == ModContent.NPCType<AkumaTransition>())
                    anyAkumaTransition = true;

                else if (n.type == ModContent.NPCType<AkumaAHead>())
                    anyAkumaA = true;

                else if (n.type == ModContent.NPCType<DaybringerHead>() || n.type == ModContent.NPCType<NightcrawlerHead>())
                    anyEquinox = true;

                else if (n.type == ModContent.NPCType<ZeroA>())
                    anyZeroA = true;
            }

            if (anyAkumaTransition)
            {
                int n = BaseAI.GetNPC(Player.Center, ModContent.NPCType<AkumaTransition>(), -1);
                NPC akuma = Main.npc[n];

                if (akuma.ai[0] >= 660)
                {
                    Player.AddBuff(ModContent.BuffType<AkumaAHead_ScorchingPain>(), 2);
                }
            }
            else if (anyAkumaA)
            {
                Player.AddBuff(ModContent.BuffType<AkumaAHead_ScorchingPain>(), 2);
            }

            if (BasePlayer.HasAccessory(Player, ModContent.ItemType<HappySunSticker>(), true, true))
            {
                TextureAssets.Sun = ModContent.Request<Texture2D>("AAModClassic/_Content/Inferno/World/Biomes/Backgrounds/DemonSun");
                TextureAssets.Sun3 = ModContent.Request<Texture2D>("AAModClassic/_Content/Inferno/World/Biomes/Backgrounds/DemonSunEclipse");
            }
            else
            {
                TextureAssets.Sun = ModContent.Request<Texture2D>("Terraria/Images/Sun");;
                TextureAssets.Sun3 = ModContent.Request<Texture2D>("Terraria/Images/Sun3");
            }

            if (Player.ZoneSandstorm && (ZoneInferno || ZoneMire))
            {
                EmitDust();
            }

            #region AsheFlameDrawMethod
            if (AsheFlame)
            {
                RingRotation += .05f;
                AsheFlameScale += .02f;
                if (AsheFlameScale >= 1f)
                {
                    AsheFlameScale = 1f;
                }
            }
            else
            {
                AsheFlameScale -= .02f;
                if (AsheFlameScale <= 0f)
                {
                    AsheFlameScale = 0f;
                }
            }

            if (AsheFlameScale > 0f)
            {
                RingRotation += .05f;
            }

            #endregion

            if (anyEquinox)
            {
                TimeScale = 0;
            }

            if (Orbiters)
            {
                Spheres = BaseAI.GetProjectiles(Player.Center, ModContent.ProjectileType<FlameVortexStaff_FireOrbiter>(), Main.myPlayer, 48);

                if (Player.ownedProjectileCounts[ModContent.ProjectileType<FlameVortexStaff_FireOrbiter>()] > 0)
                {
                    Player.GetDamage(DamageClass.Summon) += AAGlobalProjectile.CountProjectiles(ModContent.ProjectileType<FlameVortexStaff_FireOrbiter>()) * .1f;

                    if (Main.netMode != NetmodeID.Server && Main.LocalPlayer.miscCounter % 3 == 0)
                    {
                        for (int m = 0; m < Spheres.Length; m++)
                        {
                            Projectile projectile = Main.projectile[Spheres[m]];

                            if (projectile != null && projectile.active)
                            {
                                int dustID = Dust.NewDust(projectile.position, projectile.width, projectile.height, ModContent.DustType<Dusts.AkumaDustLight>());

                                Main.dust[dustID].position += Player.position - Player.oldPosition;
                                Main.dust[dustID].velocity = (Player.Center - projectile.Center) * 0.05f;
                                Main.dust[dustID].alpha = 100;
                                Main.dust[dustID].noGravity = true;
                            }
                        }
                    }
                }
            }

            if (AAWorld.ModContentGenerated || ZoneInferno || ZoneMire || ZoneVoid || ZoneTerrarium || ZoneMush)
            {
                AAWorld.ModContentGenerated = true;
                WorldgenReminder = true;
            }

            if (Main.netMode != NetmodeID.Server)
            {
                static bool IsModInstalled(string modName)
                {
                    var modOrganizerType = typeof(ModLoader).Assembly.GetType("Terraria.ModLoader.Core.ModOrganizer");

                    if (modOrganizerType == null)
                        return true;

                    var findModsMethod = modOrganizerType.GetMethod("FindMods", BindingFlags.NonPublic | BindingFlags.Static);

                    if (findModsMethod == null)
                        return true;

                    if (findModsMethod.Invoke(null, [false]) is not System.Collections.IEnumerable mods)
                        return true;

                    foreach (var mod in mods)
                    {
                        var nameProp = mod.GetType().GetProperty("Name");
                        if (nameProp?.GetValue(mod) as string == modName)
                            return true;
                    }

                    return false;
                }

                List<int> yappers = [0, 1, 2, 3, 4, 5, 6, 7, 8];
                if (!NewAAReminder && !ModContent.GetInstance<AAConfigClient>().DisableNewAAReminderMessage && !IsModInstalled("AAMod"))
                {
                    int yapper = yappers[Main.rand.Next(yappers.Count)];
                    switch (yapper)
                    {
                        case 0:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.Common.NewAAInfo1"), new Color(180, 41, 32));
                            break;
                        case 1:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.Common.NewAAInfo2"), AAColor.YamataDialogue);
                            break;
                        case 2:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.Common.NewAAInfo3"), new Color(255, 0, 0));
                            break;
                        case 3:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.Common.NewAAInfo4"), new Color(102, 20, 48));
                            break;
                        case 4:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.Common.NewAAInfo5"), new Color(72, 78, 117));
                            break;
                        case 5:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.Common.NewAAInfo6"), new Color(128, 0, 0));
                            break;
                        case 6:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.Common.NewAAInfo7"), new Color(216, 110, 40));
                            break;
                        case 7:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.Common.NewAAInfo8"), new Color(43, 46, 61));
                            break;
                        case 8:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.Common.NewAAInfo9"), Color.CadetBlue);
                            break;

                    }
                    yappers.Remove(yapper);
                }

                NewAAReminder = true;

                if (!WorldgenReminder)
                {
                    int yapper = yappers[Main.rand.Next(yappers.Count)];
                    switch (yapper)
                    {
                        case 0:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.Common.WorldgenReminderInfo1"), new Color(180, 41, 32));
                            break;
                        case 1:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.Common.WorldgenReminderInfo2"), AAColor.YamataDialogue);
                            break;
                        case 2:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.Common.WorldgenReminderInfo3"), new Color(255, 0, 0));
                            break;
                        case 3:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.Common.WorldgenReminderInfo4"), new Color(102, 20, 48));
                            break;
                        case 4:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.Common.WorldgenReminderInfo5"), new Color(72, 78, 117));
                            break;
                        case 5:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.Common.WorldgenReminderInfo6"), new Color(128, 0, 0));
                            break;
                        case 6:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.Common.WorldgenReminderInfo7"), new Color(216, 110, 40));
                            break;
                        case 7:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.Common.WorldgenReminderInfo8"), new Color(43, 46, 61));
                            break;
                        case 8:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.Common.WorldgenReminderInfo9"), Color.CadetBlue);
                            break;
                    }

                    WorldgenReminder = true;
                }
            }

            if (Player.GetModPlayer<ZAAPlayer>().ZoneMire || Player.GetModPlayer<ZAAPlayer>().ZoneRisingMoonLake)
            {
                if (Main.dayTime && !AAWorld.downedYamata)
                {
                    Player.AddBuff(ModContent.BuffType<Clueless_Buff>(), 5);
                }
            }

            if (Player.GetModPlayer<ZAAPlayer>().ZoneTerrarium)
            {
                Player.AddBuff(ModContent.BuffType<TerrasGuidance_Buff>(), 2);
                Player.AddBuff(BuffID.DryadsWard, 2);
            }

            if (anyZeroA)
            {
                if (!Filters.Scene["MoonLordShake"].IsActive())
                {
                    Filters.Scene.Activate("MoonLordShake", Player.position, new object[0]);
                }

                Filters.Scene["MoonLordShake"].GetShader().UseIntensity(1f);
            }

            if (Player.GetModPlayer<ZAAPlayer>().ZoneInferno || Player.GetModPlayer<ZAAPlayer>().ZoneRisingSunPagoda)
            {
                if (AshCurse)
                {
                    AshRain(Player);
                }
            }

            if (Player.GetModPlayer<ZAAPlayer>().ZoneRisingMoonLake || Player.GetModPlayer<ZAAPlayer>().ZoneRisingSunPagoda)
            {
                if (((!WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) && AAWorld.downedAllAncients) || (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) && AAWorld.downedAkuma && AAWorld.downedYamata)) && !AAWorld.downedShen)
                {
                    EmberRain(Player);
                }
            }



            if (Player.controlQuickHeal)
            {
                SpecialQuickHeal();
            }

            if (CrasyLucky)
            {
                Main.rand = new AAFakeRand();
                if(Main.raining)
                {
                    Main.rainTime = 300;
                    Main.maxRaining = .7f;
                }
            }
            else
            {
                Main.rand = new UnifiedRandom();
            }

            if (ZoneVoid)
            {
                Player.gravity = Player.defaultGravity + .1f;
            }
        }

        public void SpecialQuickHeal()
        {
            if (Player.noItems)
			{
				return;
			}
            Item item = new Item();
            for (int i = 0; i < 58; i++)
			{
                item = Player.inventory[i];
				if (item.type == ModContent.ItemType<RoninPotion>() && ItemLoader.CanUseItem(item, Player))
				{
                    break;
                }
            }
			if (item == null)
			{
				return;
			}
			if (Player.potionDelay > 0 || (Player.statLife == Player.statLifeMax2 && item.type != ModContent.ItemType<RoninPotion>()))
			{
				return;
			}
			SoundEngine.PlaySound(item.UseSound, Player.position);
			if (item.potion)
			{
				if (item.type == ItemID.RestorationPotion)
				{
					Player.potionDelay = Player.restorationDelayTime;
					Player.AddBuff(BuffID.PotionSickness, Player.potionDelay, true);
				}
				else
				{
					Player.potionDelay = Player.potionDelayTime;
					Player.AddBuff(BuffID.PotionSickness, Player.potionDelay, true);
				}
			}
			ItemLoader.UseItem(item, Player);
			Player.statLife += item.healLife;
			Player.statMana += item.healMana;
			if (Player.statLife > Player.statLifeMax2)
			{
				Player.statLife = Player.statLifeMax2;
			}
			if (Player.statMana > Player.statManaMax2)
			{
				Player.statMana = Player.statManaMax2;
			}
			if (item.healLife > 0 && Main.myPlayer == Player.whoAmI)
			{
				Player.HealEffect(item.healLife, true);
			}
			if (item.healMana > 0)
			{
				Player.AddBuff(BuffID.ManaSickness, Player.manaSickTime, true);
				if (Main.myPlayer == Player.whoAmI)
				{
					Player.ManaEffect(item.healMana);
				}
			}
			if (ItemLoader.ConsumeItem(item, Player))
			{
				item.stack--;
			}
			if (item.stack <= 0)
			{
				item.TurnToAir();
			}
			Recipe.FindRecipes();
        }

        public override void PostUpdateRunSpeeds()
        {
            float movespeedmax = 1f + MaxMovespeedboost;

            Player.maxRunSpeed *= movespeedmax;
        }

        #region Dust Effects

        public static void EmitDust()
        {
            if (Main.gamePaused)
            {
                return;
            }

            int sandTiles = Main.SceneMetrics.SandTileCount;
            Player player = Main.LocalPlayer;
            bool flag = Sandstorm.Happening && player.ZoneSandstorm && (Main.bgStyle == 2 || Main.bgStyle == 5) && Main.bgDelay < 50;
            Sandstorm.HandleEffectAndSky(flag && Main.UseStormEffects);

            if (sandTiles < 100 || player.position.Y > Main.worldSurface * 16.0 || player.ZoneBeach)
            {
                return;
            }

            if (!flag)
            {
                return;
            }

            int maxValue = 1;
            if (Main.rand.Next(maxValue) != 0)
            {
                return;
            }

            int num = Math.Sign(Main.WindForVisuals);
            float num2 = Math.Abs(Main.WindForVisuals);
            if (num2 < 0.01f)
            {
                return;
            }

            float num3 = num * MathHelper.Lerp(0.9f, 1f, num2);
            float num4 = 2000f / sandTiles;
            float num5 = 3f / num4;
            num5 = MathHelper.Clamp(num5, 0.77f, 1f);
            int num6 = (int)num4;
            float num7 = Main.screenWidth / (float)Main.maxScreenW;
            int num8 = (int)(1000f * num7);
            float num9 = 20f * Sandstorm.Severity;
            float num10 = num8 * (Main.gfxQuality * 0.5f + 0.5f) + num8 * 0.1f - Dust.SandStormCount;
            if (num10 <= 0f)
            {
                return;
            }

            float num11 = Main.screenWidth + 1000f;
            float num12 = Main.screenHeight;
            Vector2 value = Main.screenPosition + player.velocity;

            Color color = new(200, 160, 20, 180);
            if (player.ZoneHallow)
                color = new(213, 196, 197, 180);
            else if (player.ZoneCrimson)
                color = new(135, 43, 34, 180);
            else if (player.ZoneCorrupt)
                color = new(103, 98, 122, 180);

            float num13 = MathHelper.Lerp(0.2f, 0.35f, Sandstorm.Severity);
            float num14 = MathHelper.Lerp(0.5f, 0.7f, Sandstorm.Severity);
            int num15 = 0;

            while (num15 < num9)
            {
                if (Main.rand.Next(num6 / 4) == 0)
                {
                    Vector2 vector = new Vector2(Main.rand.NextFloat() * num11 - 500f, Main.rand.NextFloat() * -50f);

                    if (Main.rand.NextBool(3) && num == 1)
                    {
                        vector.X = Main.rand.Next(500) - 500;
                    }
                    else if (Main.rand.NextBool(3) && num == -1)
                    {
                        vector.X = Main.rand.Next(500) + Main.screenWidth;
                    }

                    if (vector.X < 0f || vector.X > Main.screenWidth)
                    {
                        vector.Y += Main.rand.NextFloat() * num12 * 0.9f;
                    }

                    vector += value;

                    int num16 = (int)vector.X / 16;
                    int num17 = (int)vector.Y / 16;

                    if (Main.tile[num16, num17] != null && Main.tile[num16, num17].WallType == WallID.None)
                    {
                        for (int i = 0; i < 1; i++)
                        {
                            Dust dust = Main.dust[Dust.NewDust(vector, 10, 10, DustID.Sandstorm, 0f, 0f, 0)];
                            dust.velocity.Y = 2f + Main.rand.NextFloat() * 0.2f;

                            Dust expr_460_cp_0 = dust;
                            expr_460_cp_0.velocity.Y *= dust.scale;

                            Dust expr_47A_cp_0 = dust;
                            expr_47A_cp_0.velocity.Y *= 0.35f;

                            dust.velocity.X = num3 * 5f + Main.rand.NextFloat() * 1f;

                            Dust expr_4B7_cp_0 = dust;
                            expr_4B7_cp_0.velocity.X += num3 * num14 * 20f;

                            dust.fadeIn += num14 * 0.2f;
                            dust.velocity *= 1f + num13 * 0.5f;
                            dust.color = color;
                            dust.velocity *= 1f + num13;
                            dust.velocity *= num5;
                            dust.scale = 0.9f;

                            num10 -= 1f;
                            if (num10 <= 0f)
                            {
                                break;
                            }
                        }

                        if (num10 <= 0f)
                        {
                            return;
                        }
                    }
                }

                num15++;
            }
        }

        public static void AshRain(Player player)
        {
            if (Main.gamePaused)
            {
                return;
            }
            
            if ((player.GetModPlayer<ZAAPlayer>().ZoneInferno || player.GetModPlayer<ZAAPlayer>().ZoneRisingSunPagoda) && player.GetModPlayer<ZAAPlayer>().AshCurse)
            {
                if (Main.LocalPlayer.position.Y < Main.worldSurface * 16 && (!WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) || Framing.GetTileSafely(player.Center.ToTileCoordinates()).WallType == WallID.None))
                {
                    player.AddBuff(ModContent.BuffType<BurningAsh_Buff>(), 5);
                }

                if (AAWorld.infernoTiles > 0 && Main.LocalPlayer.position.Y < Main.worldSurface * 16.0)
                {
                    int maxValue = 800 / AAWorld.infernoTiles;
                    float num = Main.screenWidth / (float)Main.maxScreenW;
                    int num2 = (int)(500f * num);
                    num2 = (int)(num2 * (1f + 2f * Main.cloudAlpha));
                    float num3 = 1f + 50f * Main.cloudAlpha;
                    int num4 = 0;

                    while (num4 < num3)
                    {
                        try
                        {
                            if (Ashes >= num2 * (Main.gfxQuality / 2f + 0.5f) + num2 * 0.1f)
                            {
                                break;
                            }

                            if (Main.rand.Next(maxValue) == 0)
                            {
                                int num5 = Main.rand.Next(Main.screenWidth + 1000) - 500;
                                int num6 = (int)Main.screenPosition.Y - Main.rand.Next(50);

                                if (Main.LocalPlayer.velocity.Y > 0f)
                                {
                                    num6 -= (int)Main.LocalPlayer.velocity.Y;
                                }

                                if (Main.rand.NextBool(5))
                                {
                                    num5 = Main.rand.Next(500) - 500;
                                }
                                else if (Main.rand.NextBool(5))
                                {
                                    num5 = Main.rand.Next(500) + Main.screenWidth;
                                }

                                if (num5 < 0 || num5 > Main.screenWidth)
                                {
                                    num6 += Main.rand.Next((int)(Main.screenHeight * 0.8)) + (int)(Main.screenHeight * 0.1);
                                }

                                num5 += (int)Main.screenPosition.X;

                                int num7 = num5 / 16;
                                int num8 = num6 / 16;

                                if (Main.tile[num7, num8] != null && Main.tile[num7, num8].WallType == WallID.None)
                                {
                                    int dust = Dust.NewDust(new Vector2(num5, num6), 10, 10, ModContent.DustType<Dusts.AshRain>(), 0f, 0f, 0);
                                    Main.dust[dust].velocity.Y = 3f + Main.rand.Next(30) * 0.1f;

                                    Dust expr_292_cp_0 = Main.dust[dust];
                                    expr_292_cp_0.velocity.Y *= Main.dust[dust].scale;

                                    if (!player.GetModPlayer<ZAAPlayer>().AshCurse)
                                    {
                                        Main.dust[dust].velocity.X = Main.rand.Next(-10, 10) * 0.1f;

                                        Dust expr_2EC_cp_0 = Main.dust[dust];
                                        expr_2EC_cp_0.velocity.X += Main.WindForVisuals * Main.cloudAlpha * 10f;
                                    }
                                    else
                                    {
                                        Main.dust[dust].velocity.X = (Main.cloudAlpha + 0.5f) * 25f + Main.rand.NextFloat() * 0.2f - 0.1f;

                                        Dust expr_370_cp_0 = Main.dust[dust];
                                        expr_370_cp_0.velocity.Y *= 0.5f;
                                    }

                                    Dust expr_38E_cp_0 = Main.dust[dust];
                                    expr_38E_cp_0.velocity.Y *= 1f + 0.3f * Main.cloudAlpha;

                                    Main.dust[dust].scale += Main.cloudAlpha * 0.2f;
                                    Main.dust[dust].velocity *= 1f + Main.cloudAlpha * 0.5f;
                                }
                            }
                        }
                        catch
                        {
                        }

                        num4++;
                    }
                }
            }
        }

        public static void EmberRain(Player player)
        {
            if (Main.gamePaused)
            {
                return;
            }

            if (Main.LocalPlayer.position.Y < Main.worldSurface * 16.0)
            {
                int maxValue = 8;
                float num = Main.screenWidth / (float)Main.maxScreenW;
                int num2 = (int)(500f * num);
                num2 = (int)(num2 * (1f + 2f * Main.cloudAlpha));
                float num3 = 1f + 50f * Main.cloudAlpha;
                if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                    num3 = 1f + 25f;
                int num4 = 0;

                while (num4 < num3)
                {
                    try
                    {
                        if (Ashes >= num2 * (Main.gfxQuality / 2f + 0.5f) + num2 * 0.1f)
                        {
                            break;
                        }

                        if (Main.rand.Next(maxValue) == 0)
                        {
                            int num5 = Main.rand.Next(Main.screenWidth + 1000) - 500;
                            int num6 = (int)Main.screenPosition.Y - Main.rand.Next(50);

                            if (Main.LocalPlayer.velocity.Y > 0f)
                            {
                                num6 -= (int)Main.LocalPlayer.velocity.Y;
                            }

                            if (Main.rand.NextBool(5))
                            {
                                num5 = Main.rand.Next(500) - 500;
                            }
                            else if (Main.rand.NextBool(5))
                            {
                                num5 = Main.rand.Next(500) + Main.screenWidth;
                            }

                            if (num5 < 0 || num5 > Main.screenWidth)
                            {
                                num6 += Main.rand.Next((int)(Main.screenHeight * 0.8)) + (int)(Main.screenHeight * 0.1);
                            }

                            num5 += (int)Main.screenPosition.X;

                            int num7 = num5 / 16;
                            int num8 = num6 / 16;

                            if (Main.tile[num7, num8] != null && Main.tile[num7, num8].WallType == WallID.None)
                            {
                                int dust = Dust.NewDust(new Vector2(num5, num6), 10, 10, ModContent.DustType<Discord_Dust>(), 0f, 0f, 0);
                                Dust expr_292_cp_0 = Main.dust[dust];

                                expr_292_cp_0.velocity.Y = 3f + Main.rand.Next(30) * 0.1f;
                                expr_292_cp_0.velocity.Y *= Main.dust[dust].scale;
                                expr_292_cp_0.velocity.X = Main.rand.Next(-10, 10) * 0.1f;

                                if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                                {
                                    expr_292_cp_0.velocity.X += Main.WindForVisuals * 10f;
                                    expr_292_cp_0.velocity.Y *= 0.8f;
                                    expr_292_cp_0.scale += 0.2f;
                                    expr_292_cp_0.velocity *= 1.5f;
                                }
                                else
                                {
                                    expr_292_cp_0.velocity.X += Main.WindForVisuals * Main.cloudAlpha * 10f;
                                    expr_292_cp_0.velocity.Y *= 1f + 0.3f * Main.cloudAlpha;
                                    expr_292_cp_0.scale += Main.cloudAlpha * 0.2f;
                                    expr_292_cp_0.velocity *= 1f + Main.cloudAlpha * 0.5f;
                                }
                            }
                        }
                    }
                    catch
                    {
                    }

                    num4++;
                }
            }
        }

        #endregion

        #region Dev Armor

        public void DropDevArmor(int dropType)
        {
            //0 = Pre-HM
            //1 = HM
            //2 = Post-Plant
            //3 = PML
            //4 = PA
            string addonEX = dropType == 4 ? "EX" : ""; //only include EX if it's a dropType 3 (ie from ancients)

            bool spawnedDevItems = false; //this prevents it from not dropping anything if the chance lands on something it cannot drop yet (for prehm/hm) as by this point it's past the 10% chance and thus should drop.
            while (!spawnedDevItems)
            {
                int choice = Main.rand.Next(40);

                switch (choice)
                {
                    case 0:

                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<HallamBag>());

                        if (dropType >= 4)
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<PrismeowSpectrum>());
                        else if (dropType >= 3)
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<Prismeow>());

                        spawnedDevItems = true;
                        break;
                    case 1:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<BigEBag>());
                        
                        if (dropType >= 4)
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<ExquisiteExtravagantGreatblade>());
                        else if (dropType >= 3)
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<ExtravagantLongsword>());

                        if (dropType >= 4)
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<ExtravagantTerratool>());
                        
                        spawnedDevItems = true;
                        break;
                    case 2:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<BegBag>());

                        if (dropType >= 4)
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<PoniumUltiscepter>());
                        else if (dropType >= 3)
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<PoniumStaff>());

                        if (dropType >= 1)
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<MonochromeApple>());

                        spawnedDevItems = true;
                        break;
                    case 3:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<MaskanoBag>());

                        spawnedDevItems = true;
                        break;
                    case 4:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<CharlieBag>());

                        if (dropType >= 3)
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<SoulSiphon>());

                        spawnedDevItems = true;
                        break;
                    case 5:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<TailsBag>());

                        if (dropType >= 4)
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<MobianBuster>());
                        else if (dropType >= 3)
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<FreedomStar>());

                        spawnedDevItems = true;
                        break;
                    case 6:
                        if (dropType >= 3)
                        {
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<SkrallStaff>());
                            spawnedDevItems = true;
                        }
                        break;
                    case 7:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<DellyBag>());

                        spawnedDevItems = true;
                        break;
                    case 8:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<DallinBag>());

                        if (dropType >= 4)
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<TimeTeller>());
                        else if (dropType >= 3)
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<Chronos>());

                        spawnedDevItems = true;
                        break;
                    case 9:
                        if (dropType >= 4)
                        {
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<TitanSlayer>());
                            spawnedDevItems = true;
                        }
                        else if (dropType >= 3)
                        {
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<TitanAxe>());
                            spawnedDevItems = true;
                        }

                        break;
                    case 10:
                        if (dropType >= 4)
                        {
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<ConflagrateScythe>());
                            spawnedDevItems = true;
                        }
                        else if (dropType >= 3)
                        {
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<ConflagrateStaff>());
                            spawnedDevItems = true;
                        }

                        break;
                    case 12:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<AvesBag>());

                        if (dropType >= 4)
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<DuckstepREMIX>());
                        else if (dropType >= 3)
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<DuckstepLauncher>());

                        spawnedDevItems = true;
                        break;
                    case 13:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<TiedBag>());

                        if (dropType >= 4)
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<GentlemansLongblade>());
                        else if (dropType >= 3)
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<GentlemansRapier>());

                        spawnedDevItems = true;
                        break;
                    case 14:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<MoonBag>());

                        if (dropType >= 4)
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<Light>());
                        else if (dropType >= 3)
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<Ethereal>());

                        spawnedDevItems = true;
                        break;
                    case 15:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<GroxBag>());

                        if (dropType >= 3)
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<SoccOnAStick>());
                        else if (dropType >= 3)
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<SockStaff>());

                        if (dropType >= 4)
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<GroviteTerratool>());

                        spawnedDevItems = true;
                        break;
                    case 16:

                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<CCBag>());

                        if (dropType >= 2)
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<APageOfTheRuneBook>());

                        spawnedDevItems = true;
                        break;
                    case 17:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<GibsBag>());

                        if (dropType >= 3)
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Main.rand.NextBool() ? ModContent.ItemType<Skullshot>() : ModContent.ItemType<GibsFemur>());

                        spawnedDevItems = true;
                        break;
                    case 18:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<ApawnBag>());

                        spawnedDevItems = true;
                        break;
                    case 19:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<UniverseHelmet>());
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<UniverseChestplate>());
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<UniverseLeggings>());

                        if (dropType >= 4)
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<TartarusReaper>());
                        else if (dropType >= 3)
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<CursedSickle>());

                        spawnedDevItems = true;
                        break;
                    case 20:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<MikpinBag>());

                        spawnedDevItems = true;
                        break;
                    case 21:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<FargoBag>());

                        if (dropType >= 3)
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Main.rand.NextBool() ? ModContent.ItemType<MagicAcorn>() : ModContent.ItemType<Placeholder>());

                        spawnedDevItems = true;
                        break;
                    case 22:

                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<BlazenBag>());

                        if (dropType >= 4)
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<ThunderLord>());
                        else if (dropType >= 3)
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<StormRifle>());

                        spawnedDevItems = true;
                        break;
                    case 23:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ItemID.ReaperHood);
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ItemID.ReaperRobe);

                        if (dropType >= 3)
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<SoulShredder>());
                        else if (dropType >= 3)
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<ScytheOfTheGrimReaper>());

                        spawnedDevItems = true;
                        break;
                    case 24:
                        if (dropType >= 2)
                        {
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<UmbralReaper>());

                            spawnedDevItems = true;
                        }
                        break;
                    case 25:
                        if (dropType >= 4)
                        {
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<FuryGreatforger>());

                            spawnedDevItems = true;
                        }
                        else if (dropType >= 2)
                        {
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<FuryForger>());

                            spawnedDevItems = true;
                        }

                        break;
                    case 26:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<CerberusBag>());

                        if (dropType >= 3)
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<AleisterStaff>());

                        spawnedDevItems = true;
                        break;
                    case 27:
                        if (dropType >= 2)
                        {
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<GameRaider>());

                            spawnedDevItems = true;
                        }

                        break;
                    case 28:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<PlutoBag>());

                        spawnedDevItems = true;
                        break;
                    case 29:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<VoidEyeBag>());

                        spawnedDevItems = true;
                        break;
                    case 30:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<AnarchyBag>());

                        spawnedDevItems = true;
                        break;
                    case 31:
                        if (dropType >= 4)
                        {
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<Umbra>());

                            spawnedDevItems = true;
                        }
                        else if (dropType >= 3)
                        {
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<BladeOfNight>());

                            spawnedDevItems = true;
                        }
                        break;
                    case 32:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<ShoxBag>());

                        spawnedDevItems = true;
                        break;
                    case 33:
                        if (dropType >= 1)
                        {
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<PineBreaker>());
                            if (Main.zenithWorld)
                                Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<PlanterrorBag>());
                            spawnedDevItems = true;
                        }

                        break;
                    default:
                        spawnedDevItems = false;
                        break;
                }
            }
        }

        public void PHMDevArmor()
        {
            DropDevArmor(0);
        }

        public void HMDevArmor()
        {
            DropDevArmor(1);
        }

        public void PPDevArmor()
        {
            DropDevArmor(2);
        }

        public void PMLDevArmor()
        {
            DropDevArmor(3);
        }

        public void SADevArmor()
        {
            DropDevArmor(4);
        }

        #endregion

        public override void PreUpdate()
        {
            groviteGlow[Player.whoAmI] = false;

            if (Player.GetModPlayer<ZAAPlayer>().ZoneVoid || Player.GetModPlayer<ZAAPlayer>().ZoneInferno || Player.GetModPlayer<ZAAPlayer>().ZoneRisingSunPagoda)
            {
                if (Main.raining)
                {
                    Main.rainTime = 0;
                    Main.raining = false;
                    Main.maxRaining = 0f;
                }
            }

            if (Player.GetModPlayer<ZAAPlayer>().ZoneMire || Player.GetModPlayer<ZAAPlayer>().ZoneRisingMoonLake)
            {
                if (Main.raining)
                {
                    if (Main.rand.NextBool(5))
                    {
                        Main.rainTime++;
                    }
                }
            }
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (Player.dead)
                return;

            if (AAMod.Rift.JustPressed)
            {
                RiftPos = Player.position;
                for (int m = 0; m < 58; m++)
                {
                    if (Player.inventory[m].type == ModContent.ItemType<RiftMirror>())
                    {
                        SoundEngine.PlaySound(SoundID.Item6, Player.position);
                        Player.Spawn(PlayerSpawnContext.RecallFromItem);
                    }
                }
            }

            if (AAMod.RiftReturn.JustPressed && RiftPos != new Vector2(0, 0))
            {
                for (int m = 0; m < 58; m++)
                {
                    if (Player.inventory[m].type == ModContent.ItemType<RiftMirror>())
                    {
                        SoundEngine.PlaySound(SoundID.Item6, Player.position);
                        LeaveDust(Player);
                        Player.velocity = Vector2.Zero;
                        Player.position = RiftPos;
                    }
                }
            }

            if (ChaosRa2)
            {
                if (AAMod.ArmorAbilityKey.JustPressed && AbilityCD == 0)
                {
                    AbilityCD = 180;

                    int damage = 70;
                    float knockback = 1;

                    Vector2 vector2 = Player.RotatedRelativePoint(Player.MountedCenter, true);
                    float speedX = Main.mouseX + Main.screenPosition.X - vector2.X;
                    float speedY = Main.mouseY + Main.screenPosition.Y - vector2.Y;

                    if (Player.gravDir == -1f)
                    {
                        speedY = Main.screenPosition.Y + Main.screenHeight - Main.mouseY - vector2.Y;
                    }

                    if ((float.IsNaN(speedX) && float.IsNaN(speedY)) || (speedX == 0f && speedY == 0f))
                    {
                        speedX = Player.direction;
                        speedY = 0f;
                    }

                    Vector2 velocity = new Vector2(speedX, speedY);

                    vector2.X = Main.mouseX + Main.screenPosition.X;
                    vector2.Y = Main.mouseY + Main.screenPosition.Y;

                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, velocity.X, velocity.Y, ModContent.ProjectileType<ChaosHelmetSummonerSetEffect_DragonShot>(), damage, knockback, Main.myPlayer, 0f, 0f);
                }
            }

            if (AbilityCD != 0)
            {
                AbilityCD--;
            }
        }

        private static void LeaveDust(Player player)
        {
            for (int index = 0; index < 70; ++index)
                Main.dust[Dust.NewDust(player.position, player.width, player.height, DustID.MagicMirror, player.velocity.X * 0.2f, player.velocity.Y * 0.2f, 150, Color.Cyan, 1.2f)].velocity *= 0.5f;
            Main.TeleportEffect(player.getRect(), 1);
            Main.TeleportEffect(player.getRect(), 3);
        }

        public bool ShinyCheck()
        {
            for (int i = 0; i < 58; i++)
            {
                Item item = Player.inventory[i];
                if (item.type == ModContent.ItemType<ShinyCharm>())
                {
                    if (Main.rand.NextBool(2048))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            if (Main.rand.NextBool(4096))
                return true;

            return false;
        }
        public int IZHoldTimer = 180;
        public bool InfZ = false;
        public int GetIZHealth = 2500000;
        public int EscapeLine = 180;
        public int RiftTimer;
        public int RiftDamage = 10;

        public override void UpdateBadLifeRegen()
        {
            if (Spear)
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }

                Player.lifeRegenTime = 0;
                Player.lifeRegen -= 2;
            }

            if (infinityOverload)
            {
                Player.lifeRegen -= 60;
            }

            if (YamataGravity || YamataAGravity)
            {
                if (Player.mount.CanFly())
                {
                    Player.mount.Dismount(Player);
                }

                Player.wingTimeMax /= 2;
                if (Player.wingTime > Player.wingTimeMax)
                    Player.wingTimeMax = Player.wingTimeMax;

                if (YamataAGravity)
                {
                    Player.moveSpeed *= .58f;
                }
            }

            if (FFlames)
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }

                Player.lifeRegenTime = 0;
                Player.lifeRegen -= 40 * (Player.statLife / Player.statLifeMax2);
            }


            if (CursedHellfire)
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }

                Player.lifeRegenTime = 0;
                Player.lifeRegen -= 30;
            }

            if (shroomed && Player.velocity.Y == 0)
            {
                Player.velocity.X *= .8f;
            }

            if (Hunted)
            {
                if (Player.rocketTimeMax > 30)
                {
                    Player.wingTimeMax = 30;
                }

                if (Player.accRunSpeed > 3f)
                {
                    Player.accRunSpeed = 3f;
                }

                Player.wingTimeMax /= 2;
                if (Player.wingTimeMax <= 0)
                {
                    Player.wingTimeMax = 0;
                }
            }

            if (terraBlaze)
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }

                Player.lifeRegenTime = 0;
                Player.lifeRegen -= 16;
            }

            if (dragonFire)
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }

                Player.lifeRegenTime = 0;
                Player.lifeRegen -= 8;
                
            }

            if (riftbent)
            {
                RiftTimer++;
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }

                Player.lifeRegenTime = 0;

                if (RiftTimer >= 120)
                {
                    RiftDamage += 10;
                    RiftTimer = 0;
                }

                if (RiftDamage >= 80)
                {
                    RiftDamage = 80;
                }

                Player.lifeRegen -= RiftDamage;
            }
            else
            {
                RiftDamage = 10;
                RiftTimer = 0;
            }

            if (hydraToxin)
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }

                Player.lifeRegen -= Math.Abs((int)Player.velocity.X);
            }


            if (discordInferno)
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }

                Player.lifeRegenTime = 0;
                Player.lifeRegen -= Math.Abs((int)Player.velocity.X) + 4;
                Player.GetDamage(DamageClass.Generic) *= 0.8f;
            }

            if (AkumaPain)
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }

                Player.lifeRegenTime = 0;

                if ((Player.onFire || Player.frostBurn || Player.onFire2 || dragonFire || discordInferno) && Player.lifeRegen < 0)
                {
                    Player.lifeRegen *= 2;
                }
            }
        }

        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
		{
            if (Ronin)
            {
                modifiers.Cancel();
                return;
            }
        }

        public override void UpdateDead()
        {
            infinityOverload = false;
            discordInferno = false;
            dragonFire = false;
            hydraToxin = false;
            terraBlaze = false;
            InfinityScorch = false;
            LockedOn = false;
            shroomed = false;
            riftbent = false;
            DestinedToDie = false;
            YamataGravity = false;
            YamataAGravity = false;
            Hunted = false;
            Spear = false;
            MaxMovespeedboost = 0;
        }

        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (FFlames)
            {
                if (Main.rand.NextBool(4) && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, ModContent.DustType<Dusts.ForsakenDust>(), Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default, 1.5f);

                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;

                    //Main.playerDrawDust.Add(dust);
                    r *= 0.1f;
                    g *= 0.7f;
                    b *= 0.1f;
                }
            }

            if (infinityOverload)
            {
                if (Main.rand.NextBool(4) && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, ModContent.DustType<Dusts.InfinityOverloadB>(), Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100);

                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;

                    //Main.playerDrawDust.Add(dust);
                }

                r *= 0.1f;
                g *= 0.3f;
                b *= 0.7f;

                if (Main.rand.NextBool(4) && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, ModContent.DustType<Dusts.InfinityOverloadR>(), Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100);

                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;

                    //Main.playerDrawDust.Add(dust);
                }

                r *= 0.7f;
                g *= 0.2f;
                b *= 0.2f;

                if (Main.rand.NextBool(4) && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, ModContent.DustType<Dusts.InfinityOverloadG>(), Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100);

                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;

                    //Main.playerDrawDust.Add(dust);
                }

                r *= 0.1f;
                g *= 0.7f;
                b *= 0.1f;

                if (Main.rand.NextBool(4) && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, ModContent.DustType<Dusts.InfinityOverloadY>(), Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100);

                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;

                    //Main.playerDrawDust.Add(dust);
                }

                r *= 0.5f;
                g *= 0.5f;
                b *= 0.1f;

                if (Main.rand.NextBool(4) && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, ModContent.DustType<Dusts.InfinityOverloadP>(), Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100);

                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;

                    //Main.playerDrawDust.Add(dust);
                }

                r *= 0.6f;
                g *= 0.1f;
                b *= 0.6f;

                if (Main.rand.NextBool(4) && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, ModContent.DustType<Dusts.InfinityOverloadO>(), Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100);

                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;

                    //Main.playerDrawDust.Add(dust);
                }

                r *= 0.8f;
                g *= 0.5f;
                b *= 0.1f;

                fullBright = true;
            }

            if (terraBlaze)
            {
                if (Main.rand.NextBool(4) && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, DustID.Terra, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100);

                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;

                    //Main.playerDrawDust.Add(dust);
                }

                r *= 0.1f;
                g *= 0.7f;
                b *= 0.2f;

                fullBright = true;
            }

            if (CursedHellfire)
            {
                if (Main.rand.NextBool(4))
                {
                    int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, DustID.CursedTorch, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100);

                    Main.dust[dust].scale = 3f;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;

                    //Main.playerDrawDust.Add(dust);
                }

                fullBright = true;
            }

            if (discordInferno)
            {
                for (int i = 0; i < 2; i++)
                {
                    int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width, Player.height, ModContent.DustType<Dusts.Discord_Dust>(), 0f, -2.5f, 0);

                    Main.dust[dust].alpha = 100;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale += Main.rand.NextFloat();
                }
            }

            if (shroomed)
            {
                for (int i = 0; i < 2; i++)
                {
                    int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width, Player.height, ModContent.DustType<Dusts.ShroomDust>(), 0f, -2.5f, 0);

                    Main.dust[dust].alpha = 100;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale += Main.rand.NextFloat();
                }

                Lighting.AddLight((int)(Player.Center.X / 16f), (int)(Player.Center.Y / 16f), 0f, 0f, 0.45f);
            }

            if (riftbent)
            {
                int Loops = RiftDamage / 10;
                for (int i = 0; i < Loops; i++)
                {
                    int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width, Player.height, ModContent.DustType<Dusts.CthulhuAuraDust>(), 0f, -2.5f, 0);

                    Main.dust[dust].alpha = 100;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale += Main.rand.NextFloat();
                }

                Lighting.AddLight((int)(Player.Center.X / 16f), (int)(Player.Center.Y / 16f), 0f, 0f, 0.45f);
            }
        }

        public delegate void PlayerActionDelegate(Player player);
        public static event PlayerActionDelegate ModifyDrawInfoEvent;

        public override void FrameEffects()
        {
            if (onoForceVanity && !onoHideVanity)
            {
                Player.legs = EquipLoader.GetEquipSlot(Mod, "onoLeg", EquipType.Legs);
                Player.body = EquipLoader.GetEquipSlot(Mod, "onoBody", EquipType.Body);
                Player.head = EquipLoader.GetEquipSlot(Mod, "onoHead", EquipType.Head);
            }
        }

        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            ModifyDrawInfoEvent?.Invoke(Player);
        }

        public override void UpdateVisibleVanityAccessories()
        {
            for (int n = 10; n < 18 + Player.extraAccessorySlots; n++)
            {
                Item item = Player.armor[n];
                if (item.type == ModContent.ItemType<StripemansLuckyChestplate>())
                {
                    Player.accWatch = 3;
                    Player.accDepthMeter = 1;
                    Player.accCompass = 1;
                    Player.accFishFinder = true;
                    Player.accWeatherRadio = true;
                    Player.accCalendar = true;
                    Player.accThirdEye = true;
                    Player.accJarOfSouls = true;
                    Player.accCritterGuide = true;
                    Player.accStopwatch = true;
                    Player.accOreFinder = true;
                    Player.accDreamCatcher = true;
                }
                if (item.type == ModContent.ItemType<Ono>())
                {
                    onoHideVanity = false;
                    onoForceVanity = true;
                }
                if (item.type == ModContent.ItemType<Equinox>())
                {
                    Player.hideWolf = false;
                    Player.forceWerewolf = true;
                    if ((Player.wet && !Player.lavaWet && (!Player.mount.Active || Player.mount.Type != MountID.Slime)) || !Player.forceWerewolf)
                    {
                        Player.hideMerman = false;
                        Player.forceMerman = true;
                    }
                }
            }
        }

        public override void TransformDrawData(ref PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;

            var tex = EquipLoader.GetEquipTexture(EquipType.Body, drawPlayer.body);
            if (tex != null && tex.Item != null)
            {
                int glowSlot = EquipLoader.GetEquipSlot(Mod, tex.Item.Name + "_Glow", EquipType.Body);
                if (glowSlot != -1)
                {
                    bool canDraw = true;
                    Color myColor = Color.White;
                    if (tex.Item != null && tex.Item is ICustomEquipGlow customGlow)
                    {
                        if (customGlow.Condition(drawPlayer))
                            myColor = customGlow.Color;
                        else
                            canDraw = false;
                    }

                    if (canDraw)
                    {
                        string path = EquipLoader.GetEquipTexture(EquipType.Body, glowSlot).Texture;

                        int indexAhead = 1;
                        List<(int index, DrawData data)> dataToAdd = [];
                        foreach (DrawData data in drawInfo.DrawDataCache)
                        {
                            if (data.texture == ModContent.Request<Texture2D>(tex.Texture, AssetRequestMode.ImmediateLoad).Value)
                            {
                                DrawData glow = new(
                                    texture: ModContent.Request<Texture2D>(path, AssetRequestMode.ImmediateLoad).Value,
                                    color: myColor * drawInfo.stealth * (1f - drawInfo.shadow),
                                    position: data.position,
                                    sourceRect: data.sourceRect,
                                    rotation: data.rotation,
                                    origin: data.origin,
                                    scale: data.scale,
                                    effect: data.effect
                                )
                                {
                                    shader = data.shader
                                };
                                //drawInfo.DrawDataCache.Add(glow);
                                dataToAdd.Add((indexAhead, glow));
                            }
                            indexAhead++;
                        }

                        dataToAdd.Reverse();

                        foreach (var data in dataToAdd)
                            drawInfo.DrawDataCache.Insert(data.index, data.data);
                    }
                }
            }

            tex = EquipLoader.GetEquipTexture(EquipType.Head, drawPlayer.head);
            if (tex != null && tex.Item != null)
            {
                int glowSlot = EquipLoader.GetEquipSlot(Mod, tex.Item.Name + "_Glow", EquipType.Head);
                if (glowSlot != -1)
                {
                    bool canDraw = true;
                    Color myColor = Color.White;
                    if (tex.Item != null && tex.Item is ICustomEquipGlow customGlow)
                    {
                        if (customGlow.Condition(drawPlayer))
                            myColor = customGlow.Color;
                        else
                            canDraw = false;
                    }

                    if (canDraw)
                    {
                        string path = EquipLoader.GetEquipTexture(EquipType.Head, glowSlot).Texture;

                        DrawData? headData = null;
                        int indexAhead = 1;
                        foreach (DrawData data in drawInfo.DrawDataCache)
                        {
                            if (data.texture == ModContent.Request<Texture2D>(tex.Texture, AssetRequestMode.ImmediateLoad).Value)
                            {
                                headData = data;
                                break;
                            }
                            else
                                indexAhead++;
                        }
                        if (headData.HasValue)
                        {
                            DrawData glow = new(
                                texture: ModContent.Request<Texture2D>(path, AssetRequestMode.ImmediateLoad).Value,
                                color: myColor * drawInfo.stealth * (1f - drawInfo.shadow),
                                position: headData.Value.position,
                                sourceRect: headData.Value.sourceRect,
                                rotation: headData.Value.rotation,
                                origin: headData.Value.origin,
                                scale: headData.Value.scale,
                                effect: headData.Value.effect,
                                inactiveLayerDepth: 0
                            )
                            {
                                shader = headData.Value.shader
                            };
                            drawInfo.DrawDataCache.Insert(indexAhead, glow);
                        }
                    }
                }
            }

            tex = EquipLoader.GetEquipTexture(EquipType.Legs, drawPlayer.legs);
            if (tex != null && tex.Item != null)
            {
                int glowSlot = EquipLoader.GetEquipSlot(Mod, tex.Item.Name + "_Glow", EquipType.Legs);
                if (glowSlot != -1)
                {
                    bool canDraw = true;
                    Color myColor = Color.White;
                    if (tex.Item != null && tex.Item is ICustomEquipGlow customGlow)
                    {
                        if (customGlow.Condition(drawPlayer))
                            myColor = customGlow.Color;
                        else
                            canDraw = false;
                    }

                    if (canDraw)
                    {
                        string path = EquipLoader.GetEquipTexture(EquipType.Legs, glowSlot).Texture;

                        DrawData? legData = null;
                        int indexAhead = 1;
                        foreach (DrawData data in drawInfo.DrawDataCache)
                        {
                            if (data.texture == ModContent.Request<Texture2D>(tex.Texture, AssetRequestMode.ImmediateLoad).Value)
                            {
                                legData = data;
                                break;
                            }
                            else
                                indexAhead++;
                        }
                        if (legData.HasValue)
                        {
                            DrawData glow = new(
                                texture: ModContent.Request<Texture2D>(path, AssetRequestMode.ImmediateLoad).Value,
                                color: myColor * drawInfo.stealth * (1f - drawInfo.shadow),
                                position: legData.Value.position,
                                sourceRect: legData.Value.sourceRect,
                                rotation: legData.Value.rotation,
                                origin: legData.Value.origin,
                                scale: legData.Value.scale,
                                effect: legData.Value.effect,
                                inactiveLayerDepth: 0
                            )
                            {
                                shader = legData.Value.shader
                            };
                            drawInfo.DrawDataCache.Insert(indexAhead, glow);
                        }
                    }
                }
            }
        }
    }

    public class MimicSummon : ModPlayer
    {
        int LastChest = 0;

        public override void PreUpdateBuffs()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (Player.chest == -1 && LastChest >= 0 && Main.chest[LastChest] != null)
                {
                    int x2 = Main.chest[LastChest].x;
                    int y2 = Main.chest[LastChest].y;
                    ChestItemSummonCheck(x2, y2, Mod);
                }
                LastChest = Player.chest;
            }
        }

        public override void UpdateAutopause()
        {
            LastChest = Player.chest;
        }

        public static void ChestItemSummonCheck(int x, int y, Mod mod)
        {
            if (!Main.hardMode || Main.netMode == NetmodeID.MultiplayerClient)
            {
                return;
            }

            int chestIndex = Chest.FindChest(x, y);
            if (chestIndex < 0)
            {
                return;
            }

            ushort tileType = Main.tile[Main.chest[chestIndex].x, Main.chest[chestIndex].y].TileType;
            int tileStyle = Main.tile[Main.chest[chestIndex].x, Main.chest[chestIndex].y].TileFrameX / 36;

            if (!TileID.Sets.BasicChest[tileType] || tileStyle == 5 || tileStyle == 6)
            {
                return;
            }

            bool hasInfernoKey = false;
            bool hasItems = false;

            for (int i = 0; i < 40; i++)
            {
                if (Main.chest[chestIndex].item[i] == null || Main.chest[chestIndex].item[i].type <= ItemID.None)
                {
                    continue;
                }

                if (hasItems || Main.chest[chestIndex].item[i].stack != 1)
                {
                    return;
                }

                hasItems = true;

                if (Main.chest[chestIndex].item[i].type == ModContent.ItemType<KeyOfSmite>())
                {
                    hasInfernoKey = true;
                }
                else if (Main.chest[chestIndex].item[i].type != ModContent.ItemType<KeyOfSpite>())
                {
                    return;
                }
            }

            if (!hasItems)
            {
                return;
            }

            for (int j = x; j <= x + 1; j++)
            {
                for (int k = y; k <= y + 1; k++)
                {
                    if (TileID.Sets.BasicChest[Main.tile[j, k].TileType])
                    {
                        Main.tile[j, k].ClearTile(); //previously tried setting .HasTile to false directly. Unsure if this is correct substitution
                    }
                }
            }

            for (int l = 0; l < 40; l++)
            {
                Main.chest[chestIndex].item[l] = new Item();
            }

            Chest.DestroyChest(x, y);
            NetMessage.SendData(MessageID.ChestUpdates, -1, -1, null, 1, x, y, 0f, chestIndex);
            NetMessage.SendTileSquare(-1, x, y, 3);

            int npcToSpawn = ModContent.NPCType<MireMimic>();
            if (hasInfernoKey)
            {
                npcToSpawn = ModContent.NPCType<InfernoMimic>();
            }

            int npcIndex = NPC.NewNPC(NPC.GetSource_NaturalSpawn(), x * 16 + 16, y * 16 + 32, npcToSpawn);
            Main.npc[npcIndex].whoAmI = npcIndex;
            NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npcIndex);
            Main.npc[npcIndex].BigMimicSpawnSmoke();
        }
    }

    public class AADrawLayers
    {
        public class GlowAfterWep : PlayerDrawLayer
        {
            public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.HeldItem);
            protected override void Draw(ref PlayerDrawSet drawInfo)
            {
                if(drawInfo.shadow != 0)
                {
                    return;
                }

                Player drawPlayer = drawInfo.drawPlayer;
                Item heldItem = drawPlayer.inventory[drawPlayer.selectedItem];
                BaseAAItem baseAAItem = null;

                if (heldItem.ModItem != null && heldItem.ModItem is BaseAAItem)
                {
                    baseAAItem = (BaseAAItem)heldItem.ModItem;
                }

                //TODO: this is cool. but make it use the new glowmask system
                /*
                if (baseAAItem != null && baseAAItem.glowmaskTexture != null && baseAAItem.glowmaskDrawType != BaseAAItem.GLOWMASKTYPE_NONE)
                {
                    Vector2? offsetNull = baseAAItem.HoldoutOffset();
                    Vector2 offset = (offsetNull != null) ? (Vector2)offsetNull : Vector2.Zero;

                    if (baseAAItem.glowmaskDrawType == BaseAAItem.GLOWMASKTYPE_SWORD)
                    {
                        BaseDrawing.DrawHeldSword(drawInfo, 0, drawPlayer, baseAAItem.GlowmaskDrawColor, 0f, (int)offset.X, (int)offset.Y, null, 1, ModContent.Request<Texture2D>("AAModClassic/" + baseAAItem.glowmaskTexture).Value);
                    }
                    else if (baseAAItem.glowmaskDrawType == BaseAAItem.GLOWMASKTYPE_GUN)
                    {
                        DrawHeldGun(drawInfo, 0, drawPlayer, baseAAItem.GlowmaskDrawColor, 0f, (int)offset.X, (int)offset.Y, false, false, 0f, 0f, null, 1, ModContent.Request<Texture2D>("AAModClassic/" + baseAAItem.glowmaskTexture).Value);
                    }
                }
                */
            }
        }

        private static void DrawHeldGun(PlayerDrawSet sb, int shader, Player drawPlayer, Color lightColor = default(Color), float scale = 0f, float xOffset = 0, float yOffset = 0, bool shakeX = false, bool shakeY = false, float shakeScalarX = 1.0f, float shakeScalarY = 1.0f, Rectangle? frame = null, int frameCount = 1, Texture2D overrideTex = null)
        {
            if (BaseDrawing.ShouldDrawHeldItem(drawPlayer))
            {
                Item item = drawPlayer.inventory[drawPlayer.selectedItem];
                Texture2D tex = overrideTex != null ? overrideTex : TextureAssets.Item[item.type].Value;
                int direction = drawPlayer.direction;
                Vector2 position = drawPlayer.itemLocation;
                float itemRotation = drawPlayer.itemRotation;
                float itemScale = scale <= 0f ? item.scale : scale;
                float gravDir = drawPlayer.gravDir;
                Color wepColor = item.color;

                if (frame == null)
                    frame = new Rectangle(0, 0, tex.Width, tex.Height);
                if (lightColor == default)
                    lightColor = Lighting.GetColor(position.ToTileCoordinates());
                SpriteEffects spriteEffect = direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                if (gravDir == -1f)
                {
                    yOffset *= -1;
                    spriteEffect = spriteEffect | SpriteEffects.FlipVertically;
                }

                Vector2 texOrigin = new((tex.Width / 2), (float)(tex.Height / 2) / frameCount);
                yOffset += drawPlayer.gfxOffY;

                Vector2 rotOrigin = new(-(float)xOffset, ((float)(tex.Height / 2) / frameCount) - yOffset);
                if (direction == -1)
                {
                    rotOrigin = new((float)(tex.Width + xOffset), ((float)(tex.Height / 2) / frameCount) - yOffset);
                }
                Vector2 pos = new((int)(position.X - Main.screenPosition.X + texOrigin.X), (int)(position.Y - Main.screenPosition.Y + texOrigin.Y));

                if (shakeX)
                    pos.X += shakeScalarX * (Main.rand.Next(-5, 6) / 9f);
                if (shakeY)
                    pos.Y += shakeScalarY * (Main.rand.Next(-5, 6) / 9f);

                DrawData dd = new(tex, pos, frame, item.GetAlpha(lightColor), itemRotation, rotOrigin, itemScale, spriteEffect, 0);
                dd.shader = shader;
                sb.DrawDataCache.Add(dd);

                if (wepColor != default)
                {
                    dd.shader = shader;
                    sb.DrawDataCache.Add(dd);
                }
            }
        }
        
        public class GlowAfterShield : PlayerDrawLayer// = new PlayerDrawLayer("AAMod", "glAfterShield", PlayerDrawLayer.ShieldAcc, delegate (PlayerDrawSet drawInfo)
        {
            public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.HeldItem);

            protected override void Draw(ref PlayerDrawSet drawInfo)
            {
                Mod mod = AAMod.instance;
                Player drawPlayer = drawInfo.drawPlayer;

                if (HasAndCanDraw(drawPlayer, ModContent.ItemType<TaiyangBaolei>()))
                {
                    string texturePath = Main.dayTime ? FilePathUtils.TexturePath<TaiyangBaolei>() + "A_Shield_Glow" : FilePathUtils.TexturePath<TaiyangBaolei>() + "_Shield_Glow";
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>(texturePath).Value, drawInfo.cShield, drawPlayer, drawInfo.Position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow), drawPlayer.bodyFrame);
                }
            }
        }

        public class GlowAfterFace : PlayerDrawLayer// = new PlayerDrawLayer("AAMod", "glAfterFace", PlayerDrawLayer.FaceAcc, delegate (PlayerDrawSet edi)
        {
            public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.FaceAcc);

            protected override void Draw(ref PlayerDrawSet drawInfo)
            {
                //TODO: This is fucked up
                /*
                Mod mod = AAMod.instance;
                Player drawPlayer = drawInfo.drawPlayer;

                if (HasAndCanDraw(drawPlayer, ModContent.ItemType<SoulStone>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawPlayer, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/SoulStone_Face_Glow").Value, drawInfo.cFace, drawPlayer, drawInfo.Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, drawInfo.shadow), drawPlayer.headFrame);
                }
                if (HasAndCanDraw(drawPlayer, ModContent.ItemType<SpaceStone>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawPlayer, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/SpaceStone_Face_Glow").Value, drawInfo.cFace, drawPlayer, drawInfo.Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, drawInfo.shadow), drawPlayer.headFrame);
                }
                if (HasAndCanDraw(drawPlayer, ModContent.ItemType<RealityStone>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawPlayer, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/RealityStone_Face_Glow").Value, drawInfo.cFace, drawPlayer, drawInfo.Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, drawInfo.shadow), drawPlayer.headFrame);
                }
                if (HasAndCanDraw(drawPlayer, ModContent.ItemType<TimeStone>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawPlayer, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/TimeStone_Face_Glow").Value, drawInfo.cFace, drawPlayer, drawInfo.Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, drawInfo.shadow), drawPlayer.headFrame);
                }
                if (HasAndCanDraw(drawPlayer, ModContent.ItemType<PowerStone>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawPlayer, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/PowerStone_Face_Glow").Value, drawInfo.cFace, drawPlayer, drawInfo.Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, drawInfo.shadow), drawPlayer.headFrame);
                }
                if (HasAndCanDraw(drawPlayer, ModContent.ItemType<MindStone>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawPlayer, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/MindStone_Face_Glow").Value, drawInfo.cFace, drawPlayer, drawInfo.Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, drawInfo.shadow), drawPlayer.headFrame);
                }
                */
            }
        }

        public class GlowAfterNeck : PlayerDrawLayer// = new PlayerDrawLayer("AAMod", "glAfterNeck", PlayerDrawLayer.NeckAcc, delegate (PlayerDrawSet edi)
        {
            public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.NeckAcc);

            protected override void Draw(ref PlayerDrawSet drawInfo)
            {
                Mod mod = AAMod.instance;
                Player drawPlayer = drawInfo.drawPlayer;

                if (HasAndCanDraw(drawPlayer, ModContent.ItemType<Naitokurosu>()))
                {
                    string texturePath = Main.dayTime ? FilePathUtils.TexturePath<Naitokurosu>() + "_Neck_Glow" : FilePathUtils.TexturePath<Naitokurosu>() + "A_Neck_Glow";
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>(texturePath).Value, drawInfo.cShield, drawPlayer, drawInfo.Position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow), drawPlayer.bodyFrame);
                } 
            }
        }

        public class GlowAfterHandOn : PlayerDrawLayer// = new PlayerDrawLayer("AAMod", "glAfterHandOn", PlayerDrawLayer.HandOnAcc, delegate (PlayerDrawSet edi)
        {
            public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.NeckAcc);

            protected override void Draw(ref PlayerDrawSet drawInfo)
            {
                Mod mod = AAMod.instance;
                Player drawPlayer = drawInfo.drawPlayer;

                if (HasAndCanDraw(drawPlayer, ModContent.ItemType<DemonGauntlet>()))
                {
                    Texture2D Glow = ModContent.Request<Texture2D>(FilePathUtils.TexturePath<DemonGauntlet>() + "_HandsOn_Glow").Value;
                    Color GlowColor = WorldGen.crimson ? AAColor.Ichor : AAColor.CursedInferno;

                    BaseDrawing.DrawPlayerTexture(drawInfo, Glow, drawInfo.cHandOn, drawPlayer, drawInfo.Position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(GlowColor, drawInfo.shadow), drawPlayer.bodyFrame);
                } 
            }
        }

        public class GlowAfterHandOff : PlayerDrawLayer// = new PlayerDrawLayer("AAMod", "glAfterHandOff", PlayerDrawLayer.HandOffAcc, delegate (PlayerDrawSet edi)
        {
            public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.OffhandAcc);

            protected override void Draw(ref PlayerDrawSet drawInfo)
            {  
                Mod mod = AAMod.instance;
                Player drawPlayer = drawInfo.drawPlayer;

                if (HasAndCanDraw(drawPlayer, ModContent.ItemType<DemonGauntlet>()))
                {
                    Texture2D Glow = ModContent.Request<Texture2D>(FilePathUtils.TexturePath<DemonGauntlet>() + "_HandsOff_Glow").Value;
                    Color GlowColor = WorldGen.crimson ? AAColor.Ichor : AAColor.CursedInferno;

                    BaseDrawing.DrawPlayerTexture(drawInfo, Glow, drawInfo.cHandOff, drawPlayer, drawInfo.Position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(GlowColor, drawInfo.shadow), drawPlayer.bodyFrame);
                }
            }
        }

        #region Grovite Layers
        public class glGroviteHead : PlayerDrawLayer// = new PlayerDrawLayer("AAMod", "glGroviteHead", PlayerDrawLayer.Head, delegate (PlayerDrawSet edi)
        {
            public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.OffhandAcc);

            protected override void Draw(ref PlayerDrawSet drawInfo)
            {
                if (drawInfo.drawPlayer.merman || drawInfo.drawPlayer.wereWolf || !ZAAPlayer.groviteGlow[drawInfo.drawPlayer.whoAmI])
                    return;

                Mod mod = AAMod.instance;
                Player drawPlayer = drawInfo.drawPlayer;

                if (drawInfo.shadow == 0 && HasAndCanDraw(drawPlayer, ModContent.ItemType<GroxHelmet>()))
                {
                    Texture2D tex = ModContent.Request<Texture2D>(FilePathUtils.TexturePath<GroxHelmet>() + "_Head_Glow").Value;
                    DrawFlickerTexture(0, drawInfo, drawInfo, tex, drawInfo.cHead, drawPlayer, drawPlayer.bodyFrame, drawPlayer.headRotation, drawPlayer.headPosition, drawInfo.headVect);
                }
            }
        }

        public class glGroviteBody : PlayerDrawLayer// = new PlayerDrawLayer("AAMod", "glGroviteBody", PlayerDrawLayer.Body, delegate (PlayerDrawSet edi)
        {
            public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.OffhandAcc);

            protected override void Draw(ref PlayerDrawSet drawInfo)
            {
                if (drawInfo.drawPlayer.merman || drawInfo.drawPlayer.wereWolf || !ZAAPlayer.groviteGlow[drawInfo.drawPlayer.whoAmI])
                    return;

                Mod mod = AAMod.instance;
                Player drawPlayer = drawInfo.drawPlayer;

                if (drawInfo.shadow == 0 && HasAndCanDraw(drawPlayer, ModContent.ItemType<GroxChestplate>()))
                {
                    Texture2D tex = ModContent.Request<Texture2D>("AAModClassic/GroviteCofferplateBodyGlow").Value;
                    DrawFlickerTexture(0, drawInfo, drawInfo, tex, drawInfo.cBody, drawPlayer, drawPlayer.bodyFrame, drawPlayer.bodyRotation, drawPlayer.bodyPosition, drawInfo.bodyVect);
                }
            }
        }

        public class glGroviteLegs : PlayerDrawLayer// = new PlayerDrawLayer("AAMod", "glGroviteLegs", PlayerDrawLayer.Legs, delegate (PlayerDrawSet edi)
        {
            public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.OffhandAcc);

            protected override void Draw(ref PlayerDrawSet drawInfo)
            {
                if (drawInfo.drawPlayer.merman || drawInfo.drawPlayer.wereWolf || !ZAAPlayer.groviteGlow[drawInfo.drawPlayer.whoAmI])
                    return;

                Mod mod = AAMod.instance;
                Player drawPlayer = drawInfo.drawPlayer;

                if (drawInfo.shadow == 0 && (!drawPlayer.mount.Active || drawPlayer.mount.Type != MountID.Minecart) && HasAndCanDraw(drawPlayer, ModContent.ItemType<GroxLeggings>()))
                {
                    Texture2D tex = ModContent.Request<Texture2D>(FilePathUtils.TexturePath<GroxLeggings>() + "_Legs_Glow").Value;
                    DrawFlickerTexture(0, drawInfo, drawInfo, tex, drawInfo.cLegs, drawPlayer, drawPlayer.legFrame, drawPlayer.legRotation, drawPlayer.legPosition, drawInfo.legVect);
                }
            }
        }

        public class glGroviteArm : PlayerDrawLayer// = new PlayerDrawLayer("AAMod", "glGroviteArm", PlayerDrawLayer.Arms, delegate (PlayerDrawSet edi)
        {
            public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.OffhandAcc);

            protected override void Draw(ref PlayerDrawSet drawInfo)
            {
                if (drawInfo.drawPlayer.merman || drawInfo.drawPlayer.wereWolf || !ZAAPlayer.groviteGlow[drawInfo.drawPlayer.whoAmI])
                    return;

                Mod mod = AAMod.instance;
                Player drawPlayer = drawInfo.drawPlayer;

                if (drawInfo.shadow == 0 && HasAndCanDraw(drawPlayer, ModContent.ItemType<GroxChestplate>()))
                {
                    Texture2D tex = ModContent.Request<Texture2D>(FilePathUtils.TexturePath<GroxChestplate>() + "_Arms_Glow").Value;
                    DrawFlickerTexture(0, drawInfo, drawInfo, tex, drawInfo.cBody, drawPlayer, drawPlayer.bodyFrame, drawPlayer.bodyRotation, drawPlayer.bodyPosition, drawInfo.bodyVect);
                }
            }
        }

        public class glGroviteWings : PlayerDrawLayer// = new PlayerDrawLayer("AAMod", "glGroviteWings", PlayerDrawLayer.Wings, delegate (PlayerDrawSet edi)
        {
            public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.OffhandAcc);

            protected override void Draw(ref PlayerDrawSet drawInfo)
            {
                if (drawInfo.drawPlayer.merman || drawInfo.drawPlayer.wereWolf || !ZAAPlayer.groviteGlow[drawInfo.drawPlayer.whoAmI])
                    return;

                Mod mod = AAMod.instance;
                Player drawPlayer = drawInfo.drawPlayer;

                int accSlot = 0;
                bool social = false;

                if (drawInfo.shadow == 0 && !drawPlayer.mount.Active && HasAndCanDraw(drawPlayer, ModContent.ItemType<GroxWings>(), ref social, ref accSlot))
                {
                    int dye = BaseDrawing.GetDye(drawPlayer, accSlot, social, true);
                    if (dye == -1)
                    {
                        dye = 0;
                    }

                    DrawFlickerTexture(1, drawInfo, drawInfo, ModContent.Request<Texture2D>(FilePathUtils.TexturePath<GroxWings>() + "_Wings_Glow").Value, dye, drawPlayer);
                }
            }
        }
        #endregion

        public class DrawAfterAll : PlayerDrawLayer// = new PlayerDrawLayer("AAMod", "glAfterAll", delegate (PlayerDrawSet edi)
        {
            public override Position GetDefaultPosition() => PlayerDrawLayers.AfterLastVanillaLayer;

            protected override void Draw(ref PlayerDrawSet drawInfo)
            { 
                Mod mod = AAMod.instance;
                Player drawPlayer = drawInfo.drawPlayer;

                if (drawPlayer.mount.Active)
                {
                    return;
                }

                if (drawPlayer.GetModPlayer<ZAAPlayer>().ShieldScale > 0)
                {
                    Texture2D Shield = ModContent.Request<Texture2D>("AAModClassic/_Content/Void/___PreHardmode/Items/_BossSagittarius/Accessories/SagittariusShield").Value;
                    BaseDrawing.DrawTexture(Main.spriteBatch, Shield, 0, drawPlayer.position, drawPlayer.width, drawPlayer.height, drawPlayer.GetModPlayer<ZAAPlayer>().ShieldScale, 0, 0, 1, new Rectangle(0, 0, Shield.Width, Shield.Height), AAColor.ZeroShield, true);

                    Texture2D Ring = ModContent.Request<Texture2D>("AAModClassic/_Content/Void/___PreHardmode/Items/_BossSagittarius/Accessories/SagittariusShield_Ring").Value;
                    BaseDrawing.DrawTexture(Main.spriteBatch, Ring, 0, drawPlayer.position, drawPlayer.width, drawPlayer.height, drawPlayer.GetModPlayer<ZAAPlayer>().ShieldScale, drawPlayer.GetModPlayer<ZAAPlayer>().RingRotation, 0, 1, new Rectangle(0, 0, Ring.Width, Ring.Height), BaseDrawing.GetLightColor(new Vector2(drawPlayer.position.X, drawPlayer.position.Y)), true);

                    Texture2D RingGlow = ModContent.Request<Texture2D>("AAModClassic/_Content/Void/___PreHardmode/Items/_BossSagittarius/Accessories/SagittariusShield_RingActive").Value;
                    BaseDrawing.DrawTexture(Main.spriteBatch, RingGlow, 0, drawPlayer.position, drawPlayer.width, drawPlayer.height, drawPlayer.GetModPlayer<ZAAPlayer>().ShieldScale, drawPlayer.GetModPlayer<ZAAPlayer>().RingRotation, 0, 1, new Rectangle(0, 0, RingGlow.Width, RingGlow.Height), ColorUtils.COLOR_GLOWPULSE, true);
                }

                if (drawPlayer.GetModPlayer<ZAAPlayer>().AsheFlameScale > 0)
                {
                    Texture2D Shield = ModContent.Request<Texture2D>(ModContent.GetInstance<AsheRune>().Texture).Value;
                    int red = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingFlameDye);
                    DrawingUtils.DrawWithVanillaShader(Main.spriteBatch, red, (sb) =>
                    {
                        sb.Draw(Shield, drawPlayer.Center - Main.screenPosition, null, Color.White, drawPlayer.GetModPlayer<ZAAPlayer>().RingRotation, Shield.Size() * 0.5f, drawPlayer.GetModPlayer<ZAAPlayer>().AsheFlameScale, 0, 0);
                    });
                }

                int cbuff = drawPlayer.GetModPlayer<ChampionHelmetMageSetPlayer>().CarrotBuff;

                if (cbuff > 0)
                {
                    Texture2D Shield = ModContent.Request<Texture2D>("AAModClassic/_Content/Bunny/_PostMoonlord/Items/Armor/ChampionHelmetMageSetEffect_ChampionBoost1_Aura").Value;
                    if (drawPlayer.HasBuff(ModContent.BuffType<ChampionHelmetMageSetEffect_ChampionBoost2>()))
                    {
                        Shield = ModContent.Request<Texture2D>("AAModClassic/_Content/Bunny/_PostMoonlord/Items/Armor/ChampionHelmetMageSetEffect_ChampionBoost2_Aura").Value;
                    }
                    if (drawPlayer.HasBuff(ModContent.BuffType<ChampionHelmetMageSetEffect_ChampionBoost3>()))
                    {
                        Shield = ModContent.Request<Texture2D>("AAModClassic/_Content/Bunny/_PostMoonlord/Items/Armor/ChampionHelmetMageSetEffect_ChampionBoost3_Aura").Value;
                    }
                    BaseDrawing.DrawTexture(Main.spriteBatch, Shield, 0, drawPlayer.position, drawPlayer.width, drawPlayer.height, drawPlayer.GetModPlayer<ZAAPlayer>().AsheFlameScale, drawPlayer.GetModPlayer<ZAAPlayer>().RingRotation, 0, 1, new Rectangle(0, 0, Shield.Width, Shield.Height), Main.DiscoColor, true);
                }
            }
        }

        public static bool HasAndCanDraw(Player player, int type)
        {
            int dum = 0;
            bool dummy = false;

            return HasAndCanDraw(player, type, ref dummy, ref dum);
        }

        public static bool HasAndCanDraw(Player player, int type, ref bool social, ref int slot)
        {
            if (player.wereWolf || player.merman)
            {
                return false;
            }

            ModItem mitem = ItemLoader.GetItem(type);
            if (mitem != null)
            {
                Item item = mitem.Item;
                if (item.headSlot > 0)
                {
                    return BasePlayer.HasHelmet(player, type) && BaseDrawing.ShouldDrawHelmet(player, type);
                }
                else if (item.bodySlot > 0)
                {
                    return BasePlayer.HasChestplate(player, type) && BaseDrawing.ShouldDrawChestplate(player, type);
                }
                else if (item.legSlot > 0)
                {
                    return BasePlayer.HasLeggings(player, type) && BaseDrawing.ShouldDrawLeggings(player, type);
                }
                else if (item.accessory)
                {
                    return BasePlayer.HasAccessory(player, type, true, true, ref social, ref slot) && BaseDrawing.ShouldDrawAccessory(player, type);
                }
            }

            return false;
        }

        public static void DrawFlickerTexture(int drawType, object sb, PlayerDrawSet edi, Texture2D tex, int shader, Player drawPlayer, Rectangle frame = default, float rotation = 0, Vector2 drawPos = default, Vector2 framePos = default)
        {
            if (drawPlayer == null || !drawPlayer.active || drawPlayer.dead)
            {
                return;
            }

            for (int j = 0; j < 7; j++)
            {
                Color color = new Color(110 - j * 10, 110 - j * 10, 110 - j * 10, 110 - j * 10);
                Vector2 vector = new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-5, 5)) * 0.4f;

                if (drawType == 2)
                {
                    BaseDrawing.DrawPlayerTexture(sb, tex, shader, drawPlayer, edi.Position, 1, -6f + vector.X, (drawPlayer.wings > 0 ? 0f : GetYOffset(drawPlayer)) + vector.Y, color, frame);
                }
                else
                {
                    bool wings = drawType == 1;
                    if (wings)
                    {
                        rotation = drawPlayer.bodyRotation;
                        frame = new Rectangle(0, TextureAssets.Wings[drawPlayer.wings].Height() / 4 * drawPlayer.wingFrame, TextureAssets.Wings[drawPlayer.wings].Width(), TextureAssets.Wings[drawPlayer.wings].Height() / 4);
                        framePos = new Vector2(TextureAssets.Wings[drawPlayer.wings].Width() / 2, TextureAssets.Wings[drawPlayer.wings].Height() / 8);
                    }

                    Vector2 pos;
                    int x;
                    int y;

                    if (wings)
                    {
                        x = (int)(edi.Position.X - Main.screenPosition.X + drawPlayer.width / 2 - 9 * drawPlayer.direction);
                        y = (int)(edi.Position.Y - Main.screenPosition.Y + drawPlayer.height / 2 + 2f * drawPlayer.gravDir);
                        pos = new Vector2(x, y);
                    }
                    else
                    {
                        x = (int)(edi.Position.X - Main.screenPosition.X - frame.Width / 2 + drawPlayer.width / 2);
                        y = (int)(edi.Position.Y - Main.screenPosition.Y + drawPlayer.height - frame.Height + 4f);
                        pos = new Vector2(x, y);
                    }

                    if (sb is List<DrawData>)
                    {
                        DrawData dd = new DrawData(tex, pos + drawPos + (wings ? default : framePos) + vector, new Rectangle?(frame), color, rotation, framePos, 1f, edi.playerEffect, 0)
                        {
                            shader = shader
                        };
                        ((List<DrawData>)sb).Add(dd);
                    }
                    else if (sb is SpriteBatch)
                    {
                        ((SpriteBatch)sb).Draw(tex, pos + drawPos + (wings ? default : framePos) + vector, new Rectangle?(frame), color, rotation, framePos, 1f, edi.playerEffect, 0);
                    }
                }
            }
        }

        public static float GetYOffset(Player player)
        {
            int frameID = (int)(player.bodyFrame.Y / player.bodyFrame.Height);
            if (frameID == 7 || frameID == 8 || frameID == 9 || frameID == 14 || frameID == 15 || frameID == 16)
            {
                return player.gravDir < 0f ? 2f : -2f;
            }
            return 0f;
        }
    }
}