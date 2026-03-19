using System;
using System.Collections.Generic;
using System.IO;
using BepInEx;
using BepInEx.Configuration;
using Nyxpiri.ULTRAKILL.NyxLib;

namespace Nyxpiri.ULTRAKILL.Cyberloop
{
    public static class Options
    {
        public static ConfigEntry<float> PortalMaxRecursionScalar { get; private set; } = null;
        public static ConfigEntry<bool> PortalPreferSupportInfiniteRecursion { get; private set; } = null;
        
        public static ConfigEntry<bool> UseCrossingThoseIslandsLoop { get; private set; } = null;
        public static ConfigEntry<bool> UseCrossingThoseManyIslandsLoop { get; private set; } = null;
        public static ConfigEntry<bool> UseFromEverywhereLoop { get; private set; } = null;
        public static ConfigEntry<bool> UseHorizontalLoop { get; private set; } = null;
        public static ConfigEntry<bool> UseHorizontalPongLoop { get; private set; } = null;
        public static ConfigEntry<bool> UseHorizontalRotationLoop { get; private set; } = null;
        public static ConfigEntry<bool> UseVerticalLoop { get; private set; } = null;
        public static ConfigEntry<bool> UseVerticalPongLoop { get; private set; } = null;

        internal static void Initialize(BaseUnityPlugin plugin)
        {
            Assert.IsNotNull(plugin);

            _config = plugin.Config;

            _configFileManager = plugin.gameObject.AddComponent<ConfigFileManager>();
            _configFileManager.Initialize(_config);
            _configFileManager.OnReload += Reload;

            PortalMaxRecursionScalar = _config.Bind("Performance", "PortalMaxRecursionScalar", 1.0f);
            PortalPreferSupportInfiniteRecursion = _config.Bind("Performance", "PortalPreferSupportInfiniteRecursion", false);

            UseCrossingThoseIslandsLoop = _config.Bind("Loops", "UseCrossingThoseIslandsLoop", true, "Toggles the 'Crossing Those Islands' loop. This loop is slow.");
            UseCrossingThoseManyIslandsLoop = _config.Bind("Loops", "UseCrossingThoseManyIslandsLoop", false, "Toggles the 'Crossing Those Many Islands' loop. This loop is CATACLYSMICALLY slow.");
            UseFromEverywhereLoop = _config.Bind("Loops", "UseFromEverywhereLoop", false, "Toggles the 'From Everywhere' loop. This loop is very slow.");
            UseHorizontalLoop = _config.Bind("Loops", "UseHorizontalLoop", true, "Toggles the 'Horizontal' Loop. This loop is slow.");
            UseHorizontalPongLoop = _config.Bind("Loops", "UseHorizontalPongLoop", true, "Toggles the 'Horizontal Pong' Loop. This loop is slow, slower than 'Horizontal' Loop.");
            UseHorizontalRotationLoop = _config.Bind("Loops", "UseHorizontalRotationLoop", true, "Toggles the 'Horizontal Rotation' Loop. This loop is slow.");
            UseVerticalLoop = _config.Bind("Loops", "UseVerticalLoop", true, "Toggles the 'Vertical' Loop. This loop has a moderately high cost to performance.");
            UseVerticalPongLoop = _config.Bind("Loops", "UseVerticalPongLoop", true, "Toggles the 'Vertical Pong' Loop. This loop has a moderately high-er cost to performance.");
        }

        private static void Reload()
        {
        }

        private static ConfigFile _config = null;
        private static ConfigFileManager _configFileManager;
    }
}
