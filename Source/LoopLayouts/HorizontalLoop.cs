using System;
using UnityEngine;

namespace Nyxpiri.ULTRAKILL.Cyberloop
{
    public class HorizontalLoop : LoopLayout
    {
        public override bool IsBelowArenaSafe => false;

        protected override void OnDisable()
        {
            
        }

        public override void FixedUpdate()
        {
        }

        public override void Generate()
        {
            var portalA = CreatePortalAcrossAxis(ArenaInfo.Center + Vector3.up * 200, (ArenaInfo.Width + Cyberloop.SmallGapSize), Vector3.forward, 80.0f, 500.0f, (int)(1 * Options.PortalMaxRecursionScalar.Value), false);
            var portalB = CreatePortalAcrossAxis(ArenaInfo.Center + Vector3.up * 200, (ArenaInfo.Width + Cyberloop.SmallGapSize), Vector3.right, 80.0f, 500.0f, (int)(1 * Options.PortalMaxRecursionScalar.Value), false);
            portalA.supportInfiniteRecursion = Options.PortalPreferSupportInfiniteRecursion.Value;
            portalB.supportInfiniteRecursion = Options.PortalPreferSupportInfiniteRecursion.Value;
        }

        protected override void OnClear()
        {
        }
    }
}