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

        internal static void Initialize(BaseUnityPlugin plugin)
        {
            Assert.IsNotNull(plugin);

            _config = plugin.Config;

            _configFileManager = plugin.gameObject.AddComponent<ConfigFileManager>();
            _configFileManager.Initialize(_config);
            _configFileManager.OnReload += Reload;

            PortalMaxRecursionScalar = _config.Bind("Performance", "PortalMaxRecursionScalar", 1.0f);
            PortalPreferSupportInfiniteRecursion = _config.Bind("Performance", "PortalPreferSupportInfiniteRecursion", false);
        }

        private static void Reload()
        {
        }

        private static ConfigFile _config = null;
        private static ConfigFileManager _configFileManager;
    }
}
