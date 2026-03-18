
using UnityEngine;

namespace Nyxpiri.ULTRAKILL.Cyberloop
{
    public static class ArenaInfo
    {
        public static Vector3 Center { get; private set; } = new Vector3(0.0f, 0.0f, 62.5f);
        public static float Width { get => 40.0f; }
        public static float InvDeathHeight { get => 110.0f; }
        public static float DeathHeight { get => -110.0f; }
    }
}