using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon;
using AAModClassic._Content.Hoard._PostMoonlord.Items._BossGreedA.Weapons;
using AAModClassic._Unreleased.Content.Void._PostMoonLord.NPCs.InfinityZero;
using AAModClassic.Music;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic
{
    public partial class AAMod : Mod
    {
        //Credit to QuestionMark on Team Spirit for the autoloaded mod call system

        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
        private class ModCallAttribute : Attribute;

        /// <summary> All mod call methods registered by name. </summary>
        private static readonly Dictionary<string, MethodInfo> CallMethods = [];

        public override object Call(params object[] arguments)
        {
            try
            {
                if (CallMethods.Count == 0) //Initialize local methods attributed by [ModCall]
                {
                    foreach (MethodInfo methodInfo in GetType().GetMethods(BindingFlags.NonPublic))
                    {
                        if (methodInfo.GetCustomAttribute<ModCallAttribute>() != null)
                            CallMethods.Add(methodInfo.Name, methodInfo);
                    }
                }

                if (arguments.Length == 0)
                    throw new ArgumentException("Call has no arguments.");

                if (arguments[0] is not string name)
                    throw new ArgumentException($"The leading argument must be a {typeof(string).Name} corresponding to a call.");

                if (CallMethods.TryGetValue(name, out MethodInfo info))
                {
                    arguments = arguments[1..];

                    ParameterInfo[] parameters = info.GetParameters();
                    int optionalCount = parameters.Where(x => x.IsOptional).Count(); //The number of optional parameters of this method
                    object[] namedObjects = new object[parameters.Length];

                    if (arguments.Length > parameters.Length)
                        throw new ArgumentException(name + ((optionalCount > 0)
                            ? $" requires at least {parameters.Length - optionalCount} arguments."
                            : $" requires exactly {parameters.Length} arguments."));

                    for (int c = 0; c < arguments.Length; c++)
                    {
                        object argument = arguments[c];
                        Type argumentType = parameters[c].GetType();

                        if (argument.GetType() == argumentType)
                        {
                            namedObjects[c] = argument;
                        }
                        else
                        {
                            throw new ArgumentException(name + (parameters[c].IsOptional
                                ? $" argument {c} ({parameters[c].Name}) optionally requires an object of type {argumentType.Name}."
                                : $" argument {c} ({parameters[c].Name}) requires an object of type {argumentType.Name}."));
                        }
                    }

                    return info.Invoke(null, namedObjects);
                }
                else
                {
                    throw new ArgumentException($"Call '{name}' is invalid.");
                }
            }
            catch (Exception e)
            {
                Logger.Error("Call Error: " + e.Message + "\n" + e.StackTrace);
            }

            return null;
        }

        //Calls
        [ModCall]
        private bool? Downed(string name)
        {
            if (this.TryFind<ModNPC>(name, out var npc))
                return npc.BeenKilled();
            else
            {
                Logger.Error($"Ancients Awakened Call Error: An NPC named {name} could not be found.");
                return null;
            }
        }

        [ModCall]
        private static bool InZone(string name, Player player)
        {
            ZAAPlayer aap = player.GetModPlayer<ZAAPlayer>();

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
        }

        [ModCall]
        private static bool ReplaceTrack(string key, short slot) => MusicManagementSystem.ReplaceTrack(key, slot);

        [ModCall]
        private static bool AddShenDialogue(string key, LocalizedText text, Func<bool> condition) => ShenDoragonUtils.AddShenCrossmodDialogue(key, text, condition);

        [ModCall]
        private static bool AddInfinityZeroDialogue(string key, LocalizedText text, Func<bool> condition) => Oblivion.AddInfinityZeroCrossmodDialogue(key, text, condition);

        [ModCall]
        private static void AddAltarBlockingTile(int tileType) => AAWorld.DontSpawnAltarsOn.Add(tileType);

        [ModCall]
        private bool AddOreProjectileData(int oreID, int dustType, Action<Projectile> oreEffect = null, Action<Projectile> extraAI = null, OnHitDelegate onHit = null, Action<Projectile> onKill = null, Action<Projectile, Color> extraDraw = null, Action<Projectile> onSpawn = null)
        {
            if (OreCannonSystem.OreData.ContainsKey(oreID))
            {
                Logger.Error($"Ore ID {oreID} already registered.");
                return false;
            }

            OreCannonSystem.OreData.Add(oreID, new OreProjectileData(dustType, oreEffect, extraAI, onHit, onKill, extraDraw, onSpawn));
            return true;
        }

        [ModCall]
        private static bool WorldType_Removed() => WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Removed);

        [ModCall]
        private static bool WorldType_Unreleased() => WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased);

        [ModCall]
        private static bool WorldType_Unofficial() => WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial);
    }
}
