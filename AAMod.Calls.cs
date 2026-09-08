using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.Awakened;
using AAModClassic._Content.Hoard._PostMoonlord.Items._BossGreedA.Weapons;
using AAModClassic._Unreleased.Content.Void._PostMoonLord.NPCs.InfinityZero;
using AAModClassic.Music;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic
{
    public partial class AAMod : Mod
    {
        public override object Call(params object[] args)
        {
            try
            {
                if (args.Length <= 0 || args[0] is not string)
                    return new Exception("Ancients Awakened Call Error: No method name. First parameter must be a string.");

                string methodName = (string)args[0];

                switch (methodName)
                {
                    case "Downed": //returns a Func which will return a downed value based on player and name.
                        if (args.Length <= 1)
                        {
                            Logger.Error($"Ancients Awakened Call Error: {args.Length} is invalid for {methodName}. Must be at least 1");
                            return null;
                        }
                        if (args[1] is not string)
                        {
                            Logger.Error($"Ancients Awakened Call Error: {args[1].GetType().Name} is invalid for the first argument of {methodName}. Must be a string");
                            return null;
                        }

                        string name = (string)args[1];
                        if (this.TryFind<ModNPC>(name, out var npc))
                            return npc.BeenKilled();
                        else
                        {
                            Logger.Error($"Ancients Awakened Call Error: An NPC named {name} could not be found.");
                            return null;
                        }
                    case "InZone": //returns a Func which will return a zone value based on player and name.
                        if (args.Length <= 2)
                        {
                            Logger.Error($"Ancients Awakened Call Error: {args.Length} is invalid for {methodName}. Must be at least 2");
                            return null;
                        }
                        if (args[1] is not string)
                        {
                            Logger.Error($"Ancients Awakened Call Error: {args[1].GetType().Name} is invalid for the first argument of {methodName}. Must be a string");
                            return null;
                        }
                        if (args[2] is not Player)
                        {
                            Logger.Error($"Ancients Awakened Call Error: {args[2].GetType().Name} is invalid for the second argument of {methodName}. Must be a Player");
                            return null;
                        }

                        name = ((string)args[1]).ToLower();
                        ZAAPlayer aap = ((Player)args[2]).GetModPlayer<ZAAPlayer>();

                        return name switch
                        {
                            "mire" => aap.ZoneMire,
                            "lake" => aap.ZoneRisingMoonLake,
                            "inferno" => aap.ZoneInferno,
                            "pagoda" => aap.ZoneRisingSunPagoda,
                            "ship" => aap.ZoneShip,
                            "storm" => aap.ZoneStorm,
                            "void" => aap.ZoneVoid,
                            "mush" => aap.ZoneMush,
                            "terrarium" => aap.ZoneTerrarium,
                            _ => false,
                        };
                    case "ReplaceTrack":
                        if (args.Length <= 2)
                        {
                            Logger.Error($"Ancients Awakened Call Error: {args.Length} is invalid for {methodName}. Must be at least 2");
                            return null;
                        }
                        if(args[1] is not string)
                        {
                            Logger.Error($"Ancients Awakened Call Error: {args[1].GetType().Name} is invalid for the first argument of {methodName}. Must be a string");
                            return null;
                        }
                        if(args[2] is not short)
                        {
                            Logger.Error($"Ancients Awakened Call Error: {args[2].GetType().Name} is invalid for the second argument of {methodName}. Must be a short");
                            return null;
                        }

                        string key = (string)args[1];
                        short slot = (short)args[2];
                        return MusicManagementSystem.ReplaceTrack(key, slot);
                    case "AddShenDialogue":
                        if (args.Length <= 4)
                        {
                            Logger.Error($"Ancients Awakened Call Error: {args.Length} is invalid for {methodName}. Must be at least 4");
                            return null;
                        }
                        if (args[1] is not string dKey)
                        {
                            Logger.Error($"Ancients Awakened Call Error: {args[1].GetType().Name} is invalid for the first argument of {methodName}. Must be a string");
                            return null;
                        }
                        if (args[2] is not LocalizedText text)
                        {
                            Logger.Error($"Ancients Awakened Call Error: {args[2].GetType().Name} is invalid for the first argument of {methodName}. Must be a LocalizedText");
                            return null;
                        }
                        if (args[1] is not Func<bool> condition)
                        {
                            Logger.Error($"Ancients Awakened Call Error: {args[3].GetType().Name} is invalid for the first argument of {methodName}. Must be a Func<bool>");
                            return null;
                        }
                        
                        return ShenDoragonUtils.AddShenCrossmodDialogue(dKey, text, condition);
                    case "AddInfinityZeroDialogue":
                        if (args.Length <= 4)
                        {
                            Logger.Error($"Ancients Awakened Call Error: {args.Length} is invalid for {methodName}. Must be at least 4");
                            return null;
                        }
                        if (args[1] is not string diKey)
                        {
                            Logger.Error($"Ancients Awakened Call Error: {args[1].GetType().Name} is invalid for the first argument of {methodName}. Must be a string");
                            return null;
                        }
                        if (args[2] is not LocalizedText dText)
                        {
                            Logger.Error($"Ancients Awakened Call Error: {args[2].GetType().Name} is invalid for the first argument of {methodName}. Must be a LocalizedText");
                            return null;
                        }
                        if (args[1] is not Func<bool> dCondition)
                        {
                            Logger.Error($"Ancients Awakened Call Error: {args[3].GetType().Name} is invalid for the first argument of {methodName}. Must be a Func<bool>");
                            return null;
                        }

                        return Oblivion.AddInfinityZeroCrossmodDialogue(diKey, dText, dCondition);
                    case "AddAltarBlockingTile":
                        if (args.Length <= 2)
                        {
                            Logger.Error($"Ancients Awakened Call Error: {args.Length} is invalid for {methodName}. Must be at least 1");
                            return null;
                        }
                        if (args[1] is not int tileType)
                        {
                            Logger.Error($"Ancients Awakened Call Error: {args[1].GetType().Name} is invalid for the first argument of {methodName}. Must be a int");
                            return null;
                        }
                        AAWorld.DontSpawnAltarsOn.Add(tileType);
                        return null;
                    case "AddOreProjectileData":
                        if (args.Length < 3)
                        {
                            Logger.Error($"Ancients Awakened Call Error: ...");
                            return null;
                        }
                        if (args[1] is not int oreID)
                        {
                            Logger.Error($"...");
                            return null;
                        }
                        if (args[2] is not int dustType)
                        {
                            Logger.Error($"...");
                            return null;
                        }

                        if (OreCannonSystem.OreData.ContainsKey(oreID))
                        {
                            Logger.Error($"Ore ID {oreID} already registered.");
                            return null;
                        }

                        Action<Projectile> oreEffect = null;
                        Action<Projectile> extraAI = null;
                        OnHitDelegate onHit = null;
                        Action<Projectile> onKill = null;
                        Action<Projectile, Color> extraDraw = null;
                        Action<Projectile> onSpawn = null;

                        for (int i = 3; i < args.Length; i += 2)
                        {
                            if (args[i] is not string myKey)
                            {
                                Logger.Error($"Expected string key at position {i}, got {args[i]?.GetType().Name ?? "null"}.");
                                return null;
                            }
                            if (i + 1 >= args.Length)
                            {
                                Logger.Error($"Missing value for key '{myKey}'.");
                                return null;
                            }

                            object value = args[i + 1];
                            switch (myKey)
                            {
                                case "OreEffect":
                                    if (value is Action<Projectile> effectAction)
                                        oreEffect = effectAction;
                                    else
                                    { 
                                        Logger.Error($"Ancients Awakened Call Error: {args[i].GetType().Name} is invalid for argument {i} of {methodName}. 'OreEffect' must be Action<Projectile>."); 
                                        return false;
                                    }
                                    break;
                                case "ExtraAI":
                                    if (value is Action<Projectile> aiAction) 
                                        extraAI = aiAction;
                                    else
                                    { 
                                        Logger.Error($"Ancients Awakened Call Error: {args[i].GetType().Name} is invalid for argument {i} of {methodName}. 'ExtraAI' must be Action<Projectile>."); 
                                        return false;
                                    }
                                    break;
                                case "OnHit":
                                    if (value is OnHitDelegate hitAction)
                                        onHit = hitAction;
                                    else
                                    {
                                        Logger.Error($"Ancients Awakened Call Error: {args[i].GetType().Name} is invalid for argument {i} of {methodName}. 'OnHit' must be OnHitDelegate.");
                                        return false;
                                    }
                                    break;
                                case "OnKill":
                                    if (value is Action<Projectile> killAction) 
                                        onKill = killAction;
                                    else
                                    { 
                                        Logger.Error($"Ancients Awakened Call Error: {args[i].GetType().Name} is invalid for argument {i} of {methodName}. 'OnKill' must be Action<Projectile>."); 
                                        return false;
                                    }
                                    break;
                                case "ExtraDraw":
                                    if (value is Action<Projectile, Color> drawAction) 
                                        extraDraw = drawAction;
                                    else
                                    { 
                                        Logger.Error($"Ancients Awakened Call Error: {args[i].GetType().Name} is invalid for argument {i} of {methodName}. 'ExtraDraw' must be Action<Projectile, Color>."); 
                                        return false;
                                    }
                                    break;
                                case "OnSpawn":
                                    if (value is Action<Projectile> spawnAction)
                                        onSpawn = spawnAction;
                                    else
                                    { 
                                        Logger.Error($"Ancients Awakened Call Error: {args[i].GetType().Name} is invalid for argument {i} of {methodName}. 'OnSpawn' must be Action<Projectile>.");
                                        return false;
                                    }
                                    break;
                                default:
                                    Logger.Error($"Ancients Awakened Call Error: Unknown optional key '{myKey}'.");
                                    return false;
                            }
                        }

                        OreCannonSystem.OreData.Add(oreID, new OreProjectileData(dustType, oreEffect, extraAI, onHit, onKill, extraDraw, onSpawn));
                        return true;
                    case "WorldType.Unreleased":
                        return WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased);
                    case "WorldType.Removed":
                        return WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Removed);
                    case "WorldType.Unofficial":
                        return WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial);
                    default:
                        Logger.Error($"Ancients Awakened Call Error: {methodName} does not exist.");
                        return null;
                }
            }
            catch(Exception e)
            {
                Logger.Error("Ancients Awakened Call Error: " + e.Message + "\n" + e.StackTrace);
                return null;
            }
        }
    }
}
