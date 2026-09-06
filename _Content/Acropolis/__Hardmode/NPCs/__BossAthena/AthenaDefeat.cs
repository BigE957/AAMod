using AAModClassic._Content.Acropolis._PostMoonlord.NPCs.__BossAthenaA;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Effects;
using AAModClassic.Globals;
using AAModClassic.Music;
using AAModClassic.UI.Titles;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Acropolis.__Hardmode.NPCs.__BossAthena
{
    public class AthenaDefeat : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 15;
            this.HideFromBestiary();
        }

        public override void SetDefaults()
        {
            NPC.width = 132;
            NPC.height = 104;
            NPC.npcSlots = 1000;
            NPC.aiStyle = -1;
            NPC.defense = 1;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.boss = true;
            NPC.lifeMax = 1;
            NPC.dontTakeDamage = true;
            NPC.damage = 0;
            NPC.value = 0;
            NPC.noTileCollide = true;
            Music = MusicManagementSystem.MusicSlots["Silence"];
            NPC.boss = true;
        }

        public override void AI()
        {
            
            Vector2 Acropolis = new Vector2(AAWorld.acropolisPos.X + 80 * 16, AAWorld.acropolisPos
                .Y + 79 * 16);
            NPC.TargetClosest();
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (Vector2.Distance(NPC.Center, Acropolis) < 5 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.velocity.X *= 0;
                    NPC.ai[1] = 1;
                    NPC.noTileCollide = false;
                    NPC.noGravity = false;
                    NPC.netUpdate = true;
                }
                if (NPC.ai[1] == 0)
                {
                    MoveToPoint(Acropolis);
                }
                else
                {
                    NPC.ai[0]++;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        if (NPC.ai[2] == 0)
                        {
                            if (NPC.ai[0] == 120)
                            {
                                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Athena.Transition.1"), Color.CornflowerBlue);
                                NPC.netUpdate = true;
                            }
                            else if (NPC.ai[0] == 240)
                            {
                                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Athena.Transition.2"), Color.CornflowerBlue);
                                NPC.netUpdate = true;
                            }
                            else if (NPC.ai[0] == 360)
                            {
                                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat("...", Color.CornflowerBlue);
                                NPC.netUpdate = true;
                            }
                            else if (NPC.ai[0] == 480)
                            {
                                Music = MusicManagementSystem.MusicSlots["Athena_Awakened"];
                                if (Main.netMode != NetmodeID.MultiplayerClient) 
                                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Athena.Transition.3"), Color.CornflowerBlue);
                                NPC.netUpdate = true;
                            }
                            else if (NPC.ai[0] == 600)
                            {
                                if (Main.netMode != NetmodeID.MultiplayerClient) 
                                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Athena.Transition.4"), Color.CornflowerBlue);
                                NPC.netUpdate = true;
                            }
                            else if (NPC.ai[0] == 720)
                            {
                                if (Main.netMode != NetmodeID.MultiplayerClient) 
                                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Athena.Transition.5"), Color.CornflowerBlue);
                                NPC.netUpdate = true;
                            }
                            else if (NPC.ai[0] == 840)
                            {
                                if (Main.netMode != NetmodeID.MultiplayerClient) 
                                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Athena.Transition.6"), Color.CornflowerBlue);
                                NPC.netUpdate = true;
                            }
                            else if (NPC.ai[0] == 960)
                            {
                                if (Main.netMode != NetmodeID.MultiplayerClient) 
                                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Athena.Transition.7"), Color.CornflowerBlue);
                                NPC.netUpdate = true;
                            }
                            else if (NPC.ai[0] == 1080)
                            {
                                if (Main.netMode != NetmodeID.MultiplayerClient) 
                                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Athena.Transition.8"), Color.CornflowerBlue);
                                NPC.netUpdate = true;
                            }
                            else if (NPC.ai[0] >= 1200)
                            {
                                if (Main.netMode != NetmodeID.MultiplayerClient)
                                {
                                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Athena.Transition.9"), Color.CornflowerBlue);
                                    AAModGlobalNPC.SpawnBoss(Main.player[NPC.target], ModContent.NPCType<AthenaA>(), false, NPC.Center);
                                }
                                NPC.GetGlobalNPC<TitleGlobalNPC>().ShowTitle = true;

                                int b = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0f, 0f, ModContent.ProjectileType<ShockwaveBoom>(), 0, 1, Main.myPlayer);
                                Main.projectile[b].Center = NPC.Center;

                                NPC.active = false;
                                NPC.netUpdate = true;
                            }
                        }
                        else
                        {
                            if (NPC.ai[0] == 120)
                            {
                                if (Main.netMode != NetmodeID.MultiplayerClient) 
                                    BaseUtility.Chat("...", Color.CornflowerBlue);
                                NPC.netUpdate = true;
                            }
                            else if (NPC.ai[0] == 240)
                            {
                                if (Main.netMode != NetmodeID.MultiplayerClient) 
                                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Athena.Awakened.Defeat.1"), Color.CornflowerBlue);
                                NPC.netUpdate = true;
                            }
                            else if (NPC.ai[0] == 360)
                            {
                                if (Main.netMode != NetmodeID.MultiplayerClient) 
                                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Athena.Awakened.Defeat.2"), Color.CornflowerBlue);
                                NPC.netUpdate = true;
                            }
                            else if (NPC.ai[0] == 480)
                            {
                                if (Main.netMode != NetmodeID.MultiplayerClient) 
                                    BaseUtility.Chat("...", Color.CornflowerBlue);
                                NPC.netUpdate = true;
                            }
                            else if (NPC.ai[0] == 600)
                            {
                                int pCount = 0;
                                foreach (var p in Main.ActivePlayers)
                                    pCount++;

                                if (Main.netMode != NetmodeID.MultiplayerClient) 
                                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Athena.Awakened.Defeat.3." + (pCount > 1 ? "Multiplayer" : "Singleplayer")), Color.CornflowerBlue);
                                NPC.netUpdate = true;
                            }
                            else if (NPC.ai[0] == 720)
                            {
                                if (Main.netMode != NetmodeID.MultiplayerClient) 
                                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Athena.Awakened.Defeat.4"), Color.CornflowerBlue);
                                NPC.netUpdate = true;
                            }
                            else if (NPC.ai[0] == 840)
                            {
                                if (Main.netMode != NetmodeID.MultiplayerClient) 
                                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Athena.Awakened.Defeat.5"), Color.CornflowerBlue);
                                NPC.netUpdate = true;
                            }
                            else if (NPC.ai[0] == 960)
                            {
                                if (Main.netMode != NetmodeID.MultiplayerClient) 
                                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Athena.Awakened.Defeat.6"), Color.CornflowerBlue);
                                NPC.netUpdate = true;
                            }
                            else if (NPC.ai[0] == 1080)
                            {
                                if (Main.netMode != NetmodeID.MultiplayerClient) 
                                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Athena.Awakened.Defeat.7"), Color.CornflowerBlue);
                                NPC.netUpdate = true;
                            }
                            else if (NPC.ai[0] >= 1200)
                            {
                                if (Main.netMode != NetmodeID.MultiplayerClient)
                                {
                                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Athena.Awakened.Defeat.8"), Color.CornflowerBlue);
                                    AAModGlobalNPC.SpawnBoss(Main.player[NPC.target], ModContent.NPCType<AthenaFlee>(), false, NPC.Center);
                                }
                                NPC.active = false;
                                NPC.netUpdate = true;
                            }
                        }
                    }
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.ai[1] == 0)
            {
                if (NPC.frameCounter >= 6)
                {
                    NPC.frame.Y += frameHeight;
                    NPC.frameCounter = 0;
                    if (NPC.frame.Y >= frameHeight * 7)
                    {
                        NPC.frame.Y = 0;
                    }
                }
            }
            else
            {
                if (NPC.ai[2] == 0)
                {
                    if (NPC.ai[0] < 480)
                    {
                        if (NPC.frameCounter >= 15)
                        {
                            NPC.frame.Y += frameHeight;
                            NPC.frameCounter = 0;
                            if (NPC.frame.Y >= frameHeight * 10 || NPC.frame.Y < frameHeight * 7)
                            {
                                NPC.frame.Y = frameHeight * 7;
                            }
                        }
                    }
                    else if (NPC.ai[0] >= 480 && NPC.ai[0] < 720)
                    {
                        NPC.frame.Y = frameHeight * 10;
                    }
                    else if (NPC.ai[0] >= 720)
                    {
                        if (NPC.frameCounter >= 15)
                        {
                            NPC.frame.Y += frameHeight;
                            NPC.frameCounter = 0;
                            if (NPC.frame.Y < frameHeight * 11 || NPC.frame.Y >= frameHeight * 15)
                            {
                                NPC.frame.Y = frameHeight * 11;
                            }
                        }
                    }
                }
                else
                {
                    if (NPC.ai[0] < 270)
                    {
                        if (NPC.frameCounter >= 15)
                        {
                            NPC.frame.Y += frameHeight;
                            NPC.frameCounter = 0;
                            if (NPC.frame.Y >= frameHeight * 10 || NPC.frame.Y < frameHeight * 7)
                            {
                                NPC.frame.Y = frameHeight * 7;
                            }
                        }
                    }
                    else if (NPC.ai[0] >= 270 && NPC.ai[0] < 450)
                    {
                        NPC.frame.Y = frameHeight * 10;
                    }
                    else if (NPC.ai[0] >= 450)
                    {
                        if (NPC.frameCounter >= 15)
                        {
                            NPC.frame.Y += frameHeight;
                            NPC.frameCounter = 0;
                            if (NPC.frame.Y < frameHeight * 11 || NPC.frame.Y >= frameHeight * 15)
                            {
                                NPC.frame.Y = frameHeight * 11;
                            }
                        }
                    }
                }
            }
        }

        public void MoveToPoint(Vector2 point)
        {
            float moveSpeed = 14f;
            if (moveSpeed == 0f || NPC.Center == point) return; //don't move if you have no move speed
            float velMultiplier = 1f;
            Vector2 dist = point - NPC.Center;
            float length = dist == Vector2.Zero ? 0f : dist.Length();
            if (length < moveSpeed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
            }
            if (length < 200f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 100f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 50f)
            {
                moveSpeed *= 0.5f;
            }
            NPC.velocity = length == 0f ? Vector2.Zero : Vector2.Normalize(dist);
            NPC.velocity *= moveSpeed;
            NPC.velocity *= velMultiplier;
        }
    }
}