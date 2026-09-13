using AAModClassic._Content.Chaos.___PreHardmode.NPCs.__BossGripsOfChaos;
using AAModClassic._Content.Stars._PostMoonlord.Items._BossEquinoxWorms.BossStandard;
using AAModClassic._Content.Stars._PostMoonlord.Items._BossEquinoxWorms.Consumables;
using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic._Content.Stars._PostMoonlord.NPCs.__BossEquinoxWorms.Nightcrawler;
using AAModClassic._Content.Stars.World.Biomes;
using AAModClassic._CrossMod.CalamityMod;
using AAModClassic._CrossMod.CalamityMod.LoreItems;
using AAModClassic._Removed.Content._Tinker._PostMoonlord.Items.Accessories;
using AAModClassic.Achievements;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Music;
using AAModClassic.UI.Titles;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items.AAConditions;

namespace AAModClassic._Content.Stars._PostMoonlord.NPCs.__BossEquinoxWorms.Daybringer
{
    [AutoloadBossHead]	
	public class DaybringerHead : ModNPC
	{	
		public bool nightcrawler = false;

        public static Asset<Texture2D> DaybringerHeadBig;
        public static Asset<Texture2D> DaybringerBodyBig;
        public static Asset<Texture2D> DaybringerTailBig;
        public static Asset<Texture2D> NightcrawlerHeadBig;
        public static Asset<Texture2D> NightcrawlerBodyBig;
        public static Asset<Texture2D> NightcrawlerTailBig;

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Daybringer");
            //Main.npcFrameCount[NPC.type] = 1;
            string filePath = Texture.Remove(Texture.Length - 14);
            string filePath2 = ModContent.GetInstance<NightcrawlerHead>().Texture.Remove(ModContent.GetInstance<NightcrawlerHead>().Texture.Length - 16);
            DaybringerHeadBig = ModContent.Request<Texture2D>(filePath + "DaybringerHead_Big");
            DaybringerBodyBig = ModContent.Request<Texture2D>(filePath + "DaybringerBody_Big");
            DaybringerTailBig = ModContent.Request<Texture2D>(filePath + "DaybringerTail_Big");
            NightcrawlerHeadBig = ModContent.Request<Texture2D>(filePath2 + "NightcrawlerHead_Big");
            NightcrawlerBodyBig = ModContent.Request<Texture2D>(filePath2 + "NightcrawlerBody_Big");
            NightcrawlerTailBig = ModContent.Request<Texture2D>(filePath2 + "NightcrawlerTail_Big");

            NPCID.Sets.NPCBestiaryDrawModifiers value = new()
            {
                PortraitPositionXOverride = 24,
                Position = new Vector2(56, 36),
            };
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
        }

		public override void SetDefaults()
		{
            NPC.lifeMax = 100000;
            NPC.damage = 125;
            NPC.defense = 100;
            NPC.value = Item.buyPrice(0, 10, 0, 0);
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
            NPC.knockBackResist = 0f;
            NPC.width = 68;
            NPC.height = 68;
            NPC.boss = true;
            NPC.aiStyle = -1;
			NPC.timeLeft = 500;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.DeathSound = null;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
            Music = MusicManagementSystem.MusicSlots["Equinox"];
            SceneEffectPriority = SceneEffectPriority.BossHigh;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,
                new FlavorTextBestiaryInfoElement("Mods.AAModClassic.Bestiary.Daybringer")
            ]);
        }

        public float[] internalAI = new float[8];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                if(NPC.type == ModContent.NPCType<DaybringerHead>() || NPC.type == ModContent.NPCType<NightcrawlerHead>())
                {
                    writer.Write(internalAI[0]);
                    writer.Write(internalAI[1]);
                    writer.Write(internalAI[2]);
                    writer.Write(internalAI[3]);
                    writer.Write(internalAI[4]);
                    writer.Write(internalAI[5]);
                    writer.Write(internalAI[6]);
                    writer.Write(internalAI[7]);
                    
                    writer.Write(preShootingSun);
                    writer.Write(preDeathRay);
                    writer.Write(isDeathRay);
                    writer.Write(CloudCooldown);
                }

                writer.Write(initCustom);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                if(NPC.type == ModContent.NPCType<DaybringerHead>() || NPC.type == ModContent.NPCType<NightcrawlerHead>())
                {
                    internalAI[0] = reader.ReadSingle(); //DaybringerCounter
                    internalAI[1] = reader.ReadSingle(); //NightclawerCounter
                    internalAI[2] = reader.ReadSingle();
                    internalAI[3] = reader.ReadSingle();
                    internalAI[4] = reader.ReadSingle(); //DaybringerPosCheck
                    internalAI[5] = reader.ReadSingle(); //VelocitySave
                    internalAI[6] = reader.ReadSingle(); //VelocitySave
                    internalAI[7] = reader.ReadSingle();
                    
                    preShootingSun = reader.ReadBoolean();
                    preDeathRay = reader.ReadBoolean();
                    isDeathRay = reader.ReadBoolean();
                    CloudCooldown = reader.ReadInt32();
                }

                initCustom = reader.ReadBoolean();
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void BossHeadRotation(ref float rotation)
		{
			rotation = NPC.rotation;
		}

		public override bool CheckActive()
		{
			NPC.timeLeft--;
			return NPC.timeLeft < 50;
		}

        internal bool preDeathRay = false;
        internal bool isDeathRay = false;
        internal bool preShootingSun = false;
        internal bool prevWormStronger = false;
        internal bool initCustom = false;
        public int CloudCount = Main.expertMode ? 8 : 6;
        public int CloudCooldown = 400;

        public override bool PreAI()
        {
            if (!nightcrawler)
                NPC.GetGlobalNPC<TitleGlobalNPC>().ShowTitle = true;

            bool isHead = NPC.type == ModContent.NPCType<DaybringerHead>() || NPC.type == ModContent.NPCType<NightcrawlerHead>();
            if (Main.netMode != NetmodeID.MultiplayerClient && !initCustom)
            {
                initCustom = true;
                internalAI[7] += NPC.whoAmI % 7 * 12; //so it doesn't pew all at once
                NPC.velocity.X += 0.1f;
                NPC.velocity.Y -= 4f;
            }

            bool isDay = Main.dayTime;
            bool wormStronger = nightcrawler && !isDay || !nightcrawler && isDay;

            float wormDistance = -26f;
            int aiCount = 2;
            float moveSpeedMax = 16f;
            NPC.damage = 125;
            NPC.defense = 100;

            if (wormStronger != prevWormStronger)
            {
                int dustType = nightcrawler ? ModContent.DustType<Dusts.NightcrawlerDust>() : ModContent.DustType<Dusts.DaybringerDust>();
                for (int k = 0; k < 10; k++)
                {
                    int dustID = Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType, (int)(NPC.velocity.X * 0.2f), (int)(NPC.velocity.Y * 0.2f), 0, default, 1.5f);
                    Main.dust[dustID].noGravity = true;
                }
            }

            if (wormStronger)
            {
                if(Main.netMode == NetmodeID.SinglePlayer) 
                {
                    NPC.width = 136;
                    NPC.height = 136;
                    wormDistance = -52f;
                }
                aiCount = !nightcrawler ? 6 : 4;
                moveSpeedMax = !nightcrawler ? 15f : 12f;
                NPC.damage = 150;
                NPC.defense = !nightcrawler ? 120 : 150;
            }

            for (int m = 0; m < aiCount; m++)
            {
                int Length = nightcrawler ? 24 : 30;
                int[] wormTypes = nightcrawler ? new int[] { ModContent.NPCType<NightcrawlerHead>(), ModContent.NPCType<NightcrawlerBody>(), ModContent.NPCType<NightcrawlerTail>() } : new int[] { ModContent.NPCType<DaybringerHead>(), ModContent.NPCType<DaybringerBody>(), ModContent.NPCType<DaybringerTail>() };
                AAAI.AIWorm(NPC, wormTypes, Length, wormDistance, moveSpeedMax, 0.07f, true, false, false, false, false, false);
            }

            if (isHead) //prevents despawn and allows them to run away
            {
                bool foundTarget = TargetClosest();
                if (foundTarget)
                {
                    NPC.timeLeft = 300;
                }
                else
                {
                    if (NPC.timeLeft > 50) NPC.timeLeft = 50;
                    NPC.velocity.Y -= 0.2f;
                    if (NPC.velocity.Y < -20f) NPC.velocity.Y = -20f;
                    return false;
                }
            }
            else
            {
                NPC.timeLeft = 300; //pieces should not despawn naturally, only despawn when the head does
            }
            
            Player target = Main.player[NPC.target];
            
            if (NPC.type == ModContent.NPCType<NightcrawlerHead>())
            {
                if(isDeathRay)
                {
                    goto ExtraAI;
                }
                if(preDeathRay)
                {
                    NPC.defense = 9999;
                    if((NPC.Center - target.Center).Length() < 300f)
                    {
                        isDeathRay = true;
                        NPC.netUpdate = true;
                    }

                    if (NPC.Center.X < target.Center.X)
                    {
                        NPC.velocity.X += 0.5f;
                        if (NPC.velocity.X < 0)
                            NPC.velocity.X += 0.5f * 2;
                    }
                    else
                    {
                        NPC.velocity.X -= 0.5f;
                        if (NPC.velocity.X > 0)
                            NPC.velocity.X -= 0.5f * 2;
                    }
                    if (NPC.Center.Y < target.Center.Y)
                    {
                        NPC.velocity.Y += 0.5f;
                        if (NPC.velocity.Y < 0)
                            NPC.velocity.Y += 0.5f * 2;
                    }
                    else
                    {
                        NPC.velocity.Y -= 0.5f;
                        if (NPC.velocity.Y > 0)
                            NPC.velocity.Y -= 0.5f * 2;
                    }

                    if(NPC.velocity.X > 30f) NPC.velocity.X = 30f;
                    if(NPC.velocity.Y > 30f) NPC.velocity.Y = 30f;

                    internalAI[5] = NPC.velocity.X;
                    internalAI[6] = NPC.velocity.Y;
                }
            }
            if (NPC.type == ModContent.NPCType<DaybringerHead>())
            {
                if(preShootingSun)
                {
                    NPC.defense = 9999;
                    NPC.TargetClosest(false);
                    goto ExtraAI;
                }
            }

            if(!isHead)
            {
                NPC.defense = Main.npc[NPC.realLife].defense;
            }
            goto Normal;

            ExtraAI:
            if(NPC.type == ModContent.NPCType<NightcrawlerHead>())
            {
                NPC.defense = 9999;
                NPC.TargetClosest(false);
                NPC.velocity = new Vector2(internalAI[5], internalAI[6]);
                
                if(internalAI[2] < 120)
                {
                    Vector2 newvelocity = NPC.velocity + Vector2.Normalize(NPC.velocity.RotatedBy((float)Math.PI/2)) * 0.58f;
                    NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 1.57f;
                    NPC.velocity = Vector2.Normalize(newvelocity) * 16f;
                }
                else
                {
                    Vector2 newvelocity = NPC.velocity + Vector2.Normalize(NPC.velocity.RotatedBy((float)Math.PI/2)) * 0.03625f;
                    NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 1.57f;
                    NPC.velocity = Vector2.Normalize(newvelocity) * 4f;
                }

                if (internalAI[2]++ == 90)
                {
                    for (int i = 0; i < Main.maxNPCs; i+=2)
                    {
                        if (Main.npc[i].active && Main.npc[i].type == ModContent.NPCType<NightcrawlerBody>() && Main.npc[i].realLife == NPC.whoAmI)
                        {
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                Vector2 speed = Vector2.Normalize(new Vector2(1f, 0f).RotatedBy(Main.npc[i].rotation + 3.1415f)) * 8f;
                                Projectile.NewProjectile(Main.npc[i].GetSource_FromThis(), Main.npc[i].Center.X, Main.npc[i].Center.Y, speed.X, speed.Y, ModContent.ProjectileType<NightcrawlerHead_NightDeathraySmall>(), 65, 0, Main.myPlayer, 0, i);
                            }
                        }
                    }
                }
                if (internalAI[2] >= 90)
                {
                    for(int deathRay = 0; deathRay < Main.maxProjectiles; deathRay++)
                    {
                        if(Main.projectile[deathRay].active && Main.projectile[deathRay].type == ModContent.ProjectileType<NightcrawlerHead_NightDeathraySmall>() || Main.projectile[deathRay].type == ModContent.ProjectileType<NightcrawlerHead_NightDeathray>() && Main.projectile[deathRay].ai[1] == NPC.whoAmI)
                        {
                            return false;
                        }
                    }
                }

                internalAI[5] = NPC.velocity.X;
                internalAI[6] = NPC.velocity.Y;

                if(internalAI[2] > 400)
                {
                    internalAI[2] = 0;
                    isDeathRay = false;
                    preDeathRay = false;
                    NPC.netUpdate = true;
                }
            }
            if(NPC.type == ModContent.NPCType<DaybringerHead>())
            {
                NPC.defense = 9999;
                NPC.TargetClosest(false);
                NPC.velocity = new Vector2(internalAI[5], internalAI[6]);
                Vector2 targetpos = target.Center - new Vector2(0, 2000f);
                Vector2 targetpos2 = target.Center - new Vector2(1000f, 1000f);
                Vector2 targetpos3 = target.Center - new Vector2(-1000f, 1000f);

                if(internalAI[4] == 0)
                {
                    if(Math.Abs(NPC.Center.X - targetpos.X) + Math.Abs(NPC.Center.Y - targetpos.Y) < 100f)
                    {
                        internalAI[4] = 1f;
                    }
                }
                else if(internalAI[4] == 1)
                {
                    targetpos = targetpos2;
                    if(Math.Abs(NPC.Center.X - targetpos.X) + Math.Abs(NPC.Center.Y - targetpos.Y) < 100f)
                    {
                        internalAI[4] = 2f;
                        if(Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            for (int i = 0; i < Main.maxNPCs; i+= 3)
                            {
                                if (Main.npc[i].active && Main.npc[i].type == ModContent.NPCType<DaybringerBody>() && Main.npc[i].realLife == NPC.whoAmI && AAGlobalProjectile.CountProjectiles(ModContent.ProjectileType<DaybringerHead_DaySun>()) < 3)
                                {
                                    Vector2 speed = Vector2.Normalize(new Vector2(1f, 0f).RotatedBy(Main.npc[i].rotation + 3.1415f)) * 8f;
                                    Projectile.NewProjectile(Main.npc[i].GetSource_FromThis(), Main.npc[i].Center.X, Main.npc[i].Center.Y, -speed.X, -speed.Y, ModContent.ProjectileType<DaybringerHead_DaySun>(), 42, 1, -1);
                                }
                            }
                        }
                    }
                }
                else if(internalAI[4] == 2)
                {
                    targetpos = targetpos3;
                    if(Math.Abs(NPC.Center.X - targetpos.X) + Math.Abs(NPC.Center.Y - targetpos.Y) < 100f)
                    {
                        internalAI[4] = 1f;
                        if(Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            for (int i = 0; i < Main.maxNPCs; i+= 3)
                            {
                                if (Main.npc[i].active && Main.npc[i].type == ModContent.NPCType<DaybringerBody>() && Main.npc[i].realLife == NPC.whoAmI && AAGlobalProjectile.CountProjectiles(ModContent.ProjectileType<DaybringerHead_DaySun>()) < 3)
                                {
                                    Vector2 speed = Vector2.Normalize(new Vector2(1f, 0f).RotatedBy(Main.npc[i].rotation + 3.1415f)) * 8f;
                                    Projectile.NewProjectile(Main.npc[i].GetSource_FromThis(), Main.npc[i].Center.X, Main.npc[i].Center.Y, -speed.X, -speed.Y, ModContent.ProjectileType<DaybringerHead_DaySun>(), 42, 1, 255);
                                }
                            }
                        }
                    }
                }
                if (internalAI[3] % 200 == 60 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 speed = Vector2.Normalize(NPC.velocity) * 8f;
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, speed.X, speed.Y, ModContent.ProjectileType<DaybringerHead_DaySun>(), 65, 1, 255);
                }
                if (NPC.Center.X < targetpos.X)
                {
                    NPC.velocity.X += 0.5f;
                    if (NPC.velocity.X < 0)
                        NPC.velocity.X += 0.5f * 2;
                }
                else
                {
                    NPC.velocity.X -= 0.5f;
                    if (NPC.velocity.X > 0)
                        NPC.velocity.X -= 0.5f * 2;
                }
                if (NPC.Center.Y < targetpos.Y)
                {
                    NPC.velocity.Y += 0.5f;
                    if (NPC.velocity.Y < 0)
                        NPC.velocity.Y += 0.5f * 2;
                }
                else
                {
                    NPC.velocity.Y -= 0.5f;
                    if (NPC.velocity.Y > 0)
                        NPC.velocity.Y -= 0.5f * 2;
                }

                if(NPC.velocity.X > 30f) NPC.velocity.X = 30f;
                if(NPC.velocity.Y > 30f) NPC.velocity.Y = 30f;

                NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 1.57f;

                internalAI[5] = NPC.velocity.X;
                internalAI[6] = NPC.velocity.Y;

                if(internalAI[3]++ > 700)
                {
                    internalAI[3] = 0;
                    internalAI[5] = 0;
                    internalAI[6] = 0;
                    preShootingSun = false;
                    NPC.netUpdate = true;
                }
            }
            return false;

            Normal:

            NPC.spriteDirection = 1;
            prevWormStronger = wormStronger;

            if (NPC.type == ModContent.NPCType<NightcrawlerHead>() && NPC.CountNPCS(ModContent.NPCType<NightCloud>()) < CloudCount && CloudCooldown > 0 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                CloudCooldown--;

                if (CloudCooldown <= 0)
                {
                    CloudCooldown = 0;
                }
            }

            if(isDay && !preShootingSun)
            {
                if(isHead && NPC.type == ModContent.NPCType<DaybringerHead>())
                {
                    internalAI[0] += 1f;
                    if(internalAI[0] % 360 == 0)
                    {
                        for(int playerid = 0; playerid < 255; playerid++)
                        {
                            if(Main.player[playerid].active && !Main.player[playerid].dead && Main.player[playerid] != null && Main.player[playerid].ownedProjectileCounts[ModContent.ProjectileType<DaybringerHead_DayStar>()] <= 0)
                            {
                                if (NPC.life > NPC.lifeMax / 2)
                                {
                                    if (Main.rand.NextBool(2))
                                    {
                                        Projectile.NewProjectile(NPC.GetSource_FromThis(), Main.player[playerid].Center.X - 200f, Main.player[playerid].Center.Y + 200f, 0, 0, ModContent.ProjectileType<DaybringerHead_DayStar>(), 42, 5, playerid, -200f, playerid);
                                        Projectile.NewProjectile(NPC.GetSource_FromThis(), Main.player[playerid].Center.X, Main.player[playerid].Center.Y - 300f, 0, 0, ModContent.ProjectileType<DaybringerHead_DayStar>(), 42, 5, playerid, 0, playerid);
                                        Projectile.NewProjectile(NPC.GetSource_FromThis(), Main.player[playerid].Center.X + 200f, Main.player[playerid].Center.Y + 200f, 0, 0, ModContent.ProjectileType<DaybringerHead_DayStar>(), 42, 5, playerid, 200f, playerid);
                                    }
                                    else
                                    {
                                        Projectile.NewProjectile(NPC.GetSource_FromThis(), Main.player[playerid].Center.X + 200f, Main.player[playerid].Center.Y - 200f, 0, 0, ModContent.ProjectileType<DaybringerHead_DayStar>(), 42, 5, playerid, -200f, playerid);
                                        Projectile.NewProjectile(NPC.GetSource_FromThis(), Main.player[playerid].Center.X, Main.player[playerid].Center.Y + 300f, 0, 0, ModContent.ProjectileType<DaybringerHead_DayStar>(), 42, 5, playerid, 0, playerid);
                                        Projectile.NewProjectile(NPC.GetSource_FromThis(), Main.player[playerid].Center.X - 200f, Main.player[playerid].Center.Y - 200f, 0, 0, ModContent.ProjectileType<DaybringerHead_DayStar>(), 42, 5, playerid, 200f, playerid);
                                    }
                                }
                                else
                                {
                                    if (Main.rand.NextBool(2))
                                    {
                                        Projectile.NewProjectile(NPC.GetSource_FromThis(), Main.player[playerid].Center.X - 200f, Main.player[playerid].Center.Y + 200f, 0, 0, ModContent.ProjectileType<DaybringerHead_DayStar>(), 42, 5, playerid, -200f, playerid);
                                        Projectile.NewProjectile(NPC.GetSource_FromThis(), Main.player[playerid].Center.X + 200f, Main.player[playerid].Center.Y + 200f, 0, 0, ModContent.ProjectileType<DaybringerHead_DayStar>(), 42, 5, playerid, 200f, playerid);
                                        Projectile.NewProjectile(NPC.GetSource_FromThis(), Main.player[playerid].Center.X - 200f, Main.player[playerid].Center.Y - 200f, 0, 0, ModContent.ProjectileType<DaybringerHead_DayStar>(), 42, 5, playerid, -200f, playerid);
                                        Projectile.NewProjectile(NPC.GetSource_FromThis(), Main.player[playerid].Center.X + 200f, Main.player[playerid].Center.Y - 200f, 0, 0, ModContent.ProjectileType<DaybringerHead_DayStar>(), 42, 5, playerid, 200f, playerid);
                                    }
                                    else
                                    {
                                        Projectile.NewProjectile(NPC.GetSource_FromThis(), Main.player[playerid].Center.X, Main.player[playerid].Center.Y + 300f, 0, 0, ModContent.ProjectileType<DaybringerHead_DayStar>(), 42, 5, playerid, 0, playerid);
                                        Projectile.NewProjectile(NPC.GetSource_FromThis(), Main.player[playerid].Center.X, Main.player[playerid].Center.Y - 300f, 0, 0, ModContent.ProjectileType<DaybringerHead_DayStar>(), 42, 5, playerid, 0, playerid);
                                        Projectile.NewProjectile(NPC.GetSource_FromThis(), Main.player[playerid].Center.X + 300f, Main.player[playerid].Center.Y, 0, 0, ModContent.ProjectileType<DaybringerHead_DayStar>(), 42, 5, playerid, 0, playerid);
                                        Projectile.NewProjectile(NPC.GetSource_FromThis(), Main.player[playerid].Center.X - 300f, Main.player[playerid].Center.Y, 0, 0, ModContent.ProjectileType<DaybringerHead_DayStar>(), 42, 5, playerid, 0, playerid);
                                    }
                                }
                            }
                        }
                    }
                    if(internalAI[0] % 120 == 30 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        for (int i = 0; i < Main.maxNPCs; i += 2)
                        {
                            if (Main.npc[i].active && Main.npc[i].type == ModContent.NPCType<DaybringerBody>() && Main.npc[i].realLife == NPC.whoAmI)
                            {
                                Vector2 speed = Vector2.Normalize(new Vector2(1f, 0f).RotatedBy(Main.npc[i].rotation + 3.1415f)) * 12f;
                                Projectile.NewProjectile(Main.npc[i].GetSource_FromThis(), Main.npc[i].Center.X, Main.npc[i].Center.Y, speed.X, speed.Y, ModContent.ProjectileType<DaybringerHead_DayDart>(), 42, 0, Main.myPlayer);
                                speed = -speed;
                                Projectile.NewProjectile(Main.npc[i].GetSource_FromThis(), Main.npc[i].Center.X, Main.npc[i].Center.Y, speed.X, speed.Y, ModContent.ProjectileType<DaybringerHead_DayDart>(), 42, 0, Main.myPlayer);
                            }
                        }
                    }
                    if(internalAI[0] % 120 == 60 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        for (int i = 0; i < Main.maxNPCs; i+=4)
                        {
                            if (Main.npc[i].active && Main.npc[i].type == ModContent.NPCType<DaybringerBody>() && Main.npc[i].realLife == NPC.whoAmI && Main.rand.NextBool(15))
                            {
                                Vector2 speed = Vector2.Normalize(new Vector2(1f, 0f).RotatedBy(Main.npc[i].rotation + 3.1415f)) * 8f;
                                Projectile.NewProjectile(Main.npc[i].GetSource_FromThis(), Main.npc[i].Center.X, Main.npc[i].Center.Y, speed.X, speed.Y, ModContent.ProjectileType<DaybringerHead_DayOrb>(), 42, 0, Main.myPlayer, 0, NPC.whoAmI);
                                speed = -speed;
                                Projectile.NewProjectile(Main.npc[i].GetSource_FromThis(), Main.npc[i].Center.X, Main.npc[i].Center.Y, speed.X, speed.Y, ModContent.ProjectileType<DaybringerHead_DayOrb>(), 42, 0, Main.myPlayer, 0, NPC.whoAmI);
                            }
                        }
                    }
                    

                    if(internalAI[0] > 1200)
                    {
                        if(Main.expertMode) preShootingSun = true;
                        internalAI[0] = 0f;
                        NPC.netUpdate = true;
                    }
                }
            }
            if(!isDay && !preDeathRay)
            {
                if(isHead && NPC.type == ModContent.NPCType<NightcrawlerHead>())
                {
                    internalAI[1] += 1f;
                    if (Main.netMode != NetmodeID.MultiplayerClient && CloudCooldown <= 0)
                    {
                        for(int i = 0; i < 200; i++)
                        {
                            if(Main.npc[i].type == ModContent.NPCType<NightCloud>())
                            {
                                Main.npc[i].life = 0;
                                Main.npc[i].NPCLoot();
                                Main.npc[i].active = false;
                            } 
                        }
                        CloudCooldown = 400;
                        float rotation = 2f * (float)Math.PI / CloudCount;
                        for (int m = 0; m < CloudCount; m++)
                        {
                            int n = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<NightCloud>(), 0, 0, 0, 0, rotation * m);
                            if (Main.netMode == NetmodeID.Server && n < 200)
                                NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, n);
                        }
                    }
                    
                    if(internalAI[1] % 380 == 90 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        for (int i = 0; i < Main.maxNPCs; i+= 4)
                        {
                            if (Main.npc[i].active && Main.npc[i].type == ModContent.NPCType<NightcrawlerBody>() && Main.npc[i].realLife == NPC.whoAmI)
                            {
                                Vector2 speed = Vector2.Normalize(new Vector2(1f, 0f).RotatedBy(Main.npc[i].rotation + 3.1415f)) * .5f;
                                Projectile.NewProjectile(Main.npc[i].GetSource_FromThis(), Main.npc[i].Center.X, Main.npc[i].Center.Y, speed.X, speed.Y, ModContent.ProjectileType<NightcrawlerHead_NightScythe>(), 42, 0, Main.myPlayer, NPC.rotation, NPC.spriteDirection);
                                speed = -speed;
                                Projectile.NewProjectile(Main.npc[i].GetSource_FromThis(), Main.npc[i].Center.X, Main.npc[i].Center.Y, speed.X, speed.Y, ModContent.ProjectileType<NightcrawlerHead_NightScythe>(), 42, 0, Main.myPlayer, NPC.rotation, NPC.spriteDirection);
                            }
                        }
                    }

                    
                    if(internalAI[1] % 120 == 90 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        for (int i = 0; i < Main.maxNPCs; i++)
                        {
                            if (Main.npc[i].active && Main.npc[i].type == ModContent.NPCType<NightcrawlerBody>() && Main.npc[i].realLife == NPC.whoAmI && Main.rand.NextBool(10))
                            {
                                Vector2 speed = Vector2.Normalize(new Vector2(1f, 0f).RotatedBy(Main.npc[i].rotation + 3.1415f));
                                speed = (Main.rand.NextBool(2) ? 1: -1) * speed;
                                float ai = Main.rand.Next(120);
                                Vector2 speedR = Vector2.Normalize(speed.RotatedByRandom(0.6)) * 20f;
                                Projectile.NewProjectile(Main.npc[i].GetSource_FromThis(), Main.npc[i].Center.X, Main.npc[i].Center.Y, speedR.X, speedR.Y, ModContent.ProjectileType<NightcrawlerHead_NightLaser>(), 42, 0, Main.myPlayer, speed.ToRotation() + 1000f, ai);
                            }
                        }
                    }

                    if(internalAI[1] > 1200)
                    {
                        internalAI[1] = 0f;
                        if(Main.expertMode) preDeathRay = true;
                        NPC.netUpdate = true;
                    }
                }
            }
            return false;
        }

		public int playerTooFarDist = 16000; //1000 tile radius, these worms move fast!		
		public bool TargetClosest()
		{
			int[] players = BaseAI.GetPlayers(NPC.Center, Math.Min(20000f, playerTooFarDist * 3));
			float dist = 999999999f;
			int foundPlayer = -1;
			for (int m = 0; m < players.Length; m++)
			{
				Player p = Main.player[players[m]];
				if (Vector2.Distance(p.Center, NPC.Center) < dist)
				{
					dist = Vector2.Distance(p.Center, NPC.Center);
					foundPlayer = p.whoAmI;
				}
			}
			if (foundPlayer != -1)
			{
				BaseAI.SetTarget(NPC, foundPlayer);
				return true;
			}
			return false;
		}

        public override void BossLoot(ref int potionType)
        {
            potionType = ItemID.SuperHealingPotion;
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.65f * balance);
			NPC.damage = (int)(NPC.damage * 0.85f);
		}

		bool spawnedGore = false;
        public override void HitEffect(NPC.HitInfo hit)
        {
			int dustType = nightcrawler ? ModContent.DustType<Dusts.NightcrawlerDust>() : ModContent.DustType<Dusts.DaybringerDust>();
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType, hit.HitDirection, -1f, 0, default, 1.2f);
            }
            if (NPC.life <= 0 || NPC.life - hit.Damage <= 0)
            {			
				Main.dayRate = 1;
                Main.fastForwardTimeToDusk = false;
                Main.fastForwardTimeToDawn = false;	
				if(!spawnedGore)
				{
					spawnedGore = true;
					bool isHead = NPC.type == ModContent.NPCType<DaybringerHead>() || NPC.type == ModContent.NPCType<NightcrawlerHead>();
					bool isBody = NPC.type == ModContent.NPCType<DaybringerBody>() || NPC.type == ModContent.NPCType<NightcrawlerBody>();
                    if (!Main.dedServ)
                    {
                        if (nightcrawler)
                        {
                            if (isHead)
                            {
                                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("NCGore1").Type, 1f);
                                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("NCGore2").Type, 1f);
                            }
                            else if (isBody)
                            {
                                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("NCGore3").Type, 1f);
                            }
                            else
                            {
                                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("NCGore4").Type, 1f);
                            }
                        }
                        else
                        {
                            if (isHead)
                            {
                                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("DBGore1").Type, 1f);
                                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("DBGore2").Type, 1f);
                            }
                            else if (isBody)
                            {
                                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("DBGore3").Type, 1f);
                            }
                            else
                            {
                                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("DBGore4").Type, 1f);
                            }
                        }
                    }
					for (int k = 0; k < 15; k++)
					{
						Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType, hit.HitDirection, -1f, 0, default, 1.5f);
					}
				}
            }
        }

        public override void OnKill()
        {
            int otherWormAlive = nightcrawler ? ModContent.NPCType<DaybringerHead>() : ModContent.NPCType<NightcrawlerHead>();
            if (!nightcrawler)
            {
                AAWorld.downedDB = true;
            }
            else
            {
                AAWorld.downedNC = true;
            }
            if (NPC.CountNPCS(otherWormAlive) == 0)
            {
                NPC.SetEventFlagCleared(ref AAWorld.downedEquinox, -1);
                if (NPC.playerInteraction[Main.myPlayer])
                    EquinoxWormsKilled.Condition.Complete();
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<EquinoxWormsTreasureBag>()));

            LeadingConditionRule lastWorm = new(new LastWorm());
            LeadingConditionRule loreCondition = new(new LoreItemDropCondition(() => AAWorld.downedEquinox));
            lastWorm.OnSuccess(loreCondition).OnSuccess(new PerPlayerDropRule(ModContent.ItemType<EquinoxWormsLore>(), 1));

            npcLoot.Add(lastWorm);

            LeadingConditionRule masterMode = new(new LastWormInMaster());

            masterMode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<EquinoxWormsRelic>()));

            npcLoot.Add(masterMode);

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DaybringerTrophy>(), 10));

            LeadingConditionRule notExpert = new(new Conditions.NotExpert());

            notExpert.OnSuccess(ItemDropRule.Common(ModContent.ItemType<RadiantPhoton>(), 1, 30, 75));

            notExpert.OnSuccess(ItemDropRule.Common(ModContent.ItemType<DaybringerMask>(), 7));

            LeadingConditionRule starGenned = new(new RadiumStarsGenerated());

            starGenned.OnSuccess(ItemDropRule.Common(ModContent.ItemType<StarIdol>(), 4));

            npcLoot.Add(starGenned);
            npcLoot.Add(notExpert);

            LeadingConditionRule anceintsDownAndRemoved = new(new PostLateAncientsAndRemovedWorld());

            anceintsDownAndRemoved.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MindStone>(), 50));

            npcLoot.Add(anceintsDownAndRemoved);
        }

        public class LastWormInMaster : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                if (!Main.masterMode && !CalamityMod.IsRevengance)
                    return false;

                int type = ModContent.NPCType<NightcrawlerHead>();
                if (info.npc.type == ModContent.NPCType<NightcrawlerHead>())
                    type = ModContent.NPCType<DaybringerHead>();

                return !NPC.AnyNPCs(type);
            }

            public bool CanShowItemDropInUI() => Main.masterMode || CalamityMod.IsRevengance;
            public string GetConditionDescription() => CalamityMod.IsEnabled ? Language.GetTextValue("Mods.CalamityMod.Condition.RevOrMM") : Language.GetTextValue("Mods.AAModClassic.Common.Conditions.IsMaster");
        }

        public class LastWorm : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                int type = ModContent.NPCType<NightcrawlerHead>();
                if (info.npc.type == ModContent.NPCType<NightcrawlerHead>())
                    type = ModContent.NPCType<DaybringerHead>();

                return !NPC.AnyNPCs(type);
            }

            public bool CanShowItemDropInUI() => true;
            public string GetConditionDescription() => null;
        }

        public class RadiumStarsGenerated : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                return AAWorld.RadiumOre;
            }

            public bool CanShowItemDropInUI() => true;
            public string GetConditionDescription() => null;
        }

        public static Color GetAuraAlpha()
		{
			Color c = Color.White * (Main.mouseTextColor / 255f);
			//c.A = 255;
			return c;
		}

        public override void ModifyHitByItem(Player player, Item item, ref NPC.HitModifiers modifiers)
        {
            MakeSegmentsImmune(NPC, player.whoAmI);
            ModifyCritArea(NPC, ref modifiers);
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            MakeSegmentsImmune(NPC, projectile.owner);
            ModifyCritArea(NPC, ref modifiers);
            if (projectile.penetrate != 1)
            {
                for(int i = 0; i < Main.maxNPCs; i ++)
                {
                    if(Main.npc[i].active && (Main.npc[i].whoAmI == NPC.realLife || Main.npc[i].realLife >= 0 && Main.npc[i].realLife == NPC.realLife))
                    {
                        Main.npc[i].immune[projectile.owner] = 10;
                    }
                }
                modifiers.TargetDamageMultiplier *= .44f;
            }
            if (NPC.type != ModContent.NPCType<DaybringerHead>() && NPC.type != ModContent.NPCType<NightcrawlerHead>())
            {
                modifiers.TargetDamageMultiplier *= .78f; ;
            }
        }

        private static void ModifyCritArea(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if (npc.realLife >= 0)
            {
                if (npc.whoAmI == npc.realLife)
                {
                    modifiers.SetCrit();
                }
                if (npc.ai[0] == 0)
                {
                    modifiers.DisableCrit();
                }
            }
        }

        public override void UpdateLifeRegen(ref int damage)
        {
            if (NPC.realLife >= 0 && NPC.whoAmI != NPC.realLife)
            {
                damage = 0;
                NPC.lifeRegen = 0;
            }
        }

        public static void MakeSegmentsImmune(NPC npc, int id)
        {
            if (npc.realLife >= 0)
            {
                bool last = false;
                NPC parent = Main.npc[npc.realLife];
                parent.lifeRegen = npc.lifeRegen;
                int i = 0;
                while (parent.ai[0] > 0 || last)
                {
                    if (i++ > 200) { return; }
                    parent.immune[id] = npc.immune[id];
                    for (int j = 0; j < npc.buffType.Length; j++)
                    {
                        if (npc.buffType[j] > 0 && npc.buffTime[j] > 0)
                        {
                            parent.buffType[j] = npc.buffType[j];
                            parent.buffTime[j] = npc.buffTime[j];
                        }
                    }
                    if (last) { break; }
                    parent = Main.npc[(int)parent.ai[0]];
                    if (parent.ai[0] == 0) { last = true; }
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            bool wormStronger = nightcrawler && !Main.dayTime || !nightcrawler && Main.dayTime;
            Texture2D tex = TextureAssets.Npc[NPC.type].Value;
            NPC.width = 68;
            NPC.height = 68;
            if (!NPC.IsABestiaryIconDummy && wormStronger)
            {
                if (NPC.type == ModContent.NPCType<DaybringerHead>())
                    tex = DaybringerHeadBig.Value;
                else if (NPC.type == ModContent.NPCType<DaybringerBody>()) 
                    tex = DaybringerBodyBig.Value;
                else if (NPC.type == ModContent.NPCType<DaybringerTail>()) 
                    tex = DaybringerTailBig.Value;
                else if (NPC.type == ModContent.NPCType<NightcrawlerHead>())
                    tex = NightcrawlerHeadBig.Value;
                else if (NPC.type == ModContent.NPCType<NightcrawlerBody>())
                    tex = NightcrawlerBodyBig.Value;
                else if (NPC.type == ModContent.NPCType<NightcrawlerTail>())
                    tex = NightcrawlerTailBig.Value;

                int diff = Main.LocalPlayer.miscCounter % 50;
                float diffFloat = diff / 50f;
                float auraPercent = BaseUtility.MultiLerp(diffFloat, 0f, 1f, 0f); //did it this way so it's syncronized between all the segments
                DrawingUtils.DrawAura(spriteBatch, tex, NPC, auraPercent, 2f, 0f, 0f, GetAuraAlpha());
            }

            if (NPC.IsABestiaryIconDummy)
            {
                Texture2D head;
                Texture2D body;
                if (NPC.type == ModContent.NPCType<NightcrawlerHead>())
                {
                    head = wormStronger ? NightcrawlerHeadBig.Value : tex;
                    body = wormStronger ? NightcrawlerBodyBig.Value : TextureAssets.Npc[ModContent.NPCType<NightcrawlerBody>()].Value;
                }
                else
                {
                    head = wormStronger ? DaybringerHeadBig.Value : tex;
                    body = wormStronger ? DaybringerBodyBig.Value : TextureAssets.Npc[ModContent.NPCType<DaybringerBody>()].Value;
                }
                return DrawingUtils.DrawAnimatedBestiaryWorm(spriteBatch, NPC, drawColor, head, body, wormStronger ? 2 : 3, wormStronger ? 72 : 42, 0.25f, new Vector2(wormStronger ? 32 : 0, 0), wormStronger ? 3 : 2, 20, wormStronger ? -54: -24, flip: true);
            }

            spriteBatch.Draw(tex, NPC.Center - screenPos, NPC.frame, Color.White, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0); //GetAuraAlpha());				
            return false;
        }
    }

    public class EquinoxTimeSystem : ModSystem
    {
        public override void ModifyTimeRate(ref double timeRate, ref double tileUpdateRate, ref double eventUpdateRate)
        {
            int daybringerIndex = NPC.FindFirstNPC(ModContent.NPCType<DaybringerHead>());
            bool daybringerExists = daybringerIndex != -1;
            int nightcrawlerIndex = NPC.FindFirstNPC(ModContent.NPCType<NightcrawlerHead>());
            bool nightcrawlerExists = nightcrawlerIndex != -1;

            if (daybringerExists && nightcrawlerExists)
            {
                DaybringerHead daybringer = Main.npc[daybringerIndex].ModNPC as DaybringerHead;
                NightcrawlerHead nightcrawler = Main.npc[nightcrawlerIndex].ModNPC as NightcrawlerHead;

                if (Main.dayTime && !daybringer.preShootingSun || !Main.dayTime && !nightcrawler.preDeathRay)
                    timeRate *= Main.expertMode ? 20 : 15;
                else if (daybringer.preShootingSun || (nightcrawler.preDeathRay || nightcrawler.isDeathRay))
                    timeRate *= 0;
            }
            else if (daybringerExists && !nightcrawlerExists)
            {
                DaybringerHead daybringer = Main.npc[daybringerIndex].ModNPC as DaybringerHead;

                Main.dayTime = true;
                
                if (daybringer.preShootingSun)
                    timeRate *= 0;
            }
            else if (!daybringerExists && nightcrawlerExists)
            {
                NightcrawlerHead nightcrawler = Main.npc[nightcrawlerIndex].ModNPC as NightcrawlerHead;

                Main.dayTime = false;
                
                if (nightcrawler.preDeathRay || nightcrawler.isDeathRay)
                    timeRate *= 0;
            }
        }

    }
}