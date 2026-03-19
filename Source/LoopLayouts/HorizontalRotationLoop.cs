using System;
using UnityEngine;

namespace Nyxpiri.ULTRAKILL.Cyberloop
{
    public class HorizontalRotationLoop : LoopLayout
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
            var portalA = CreatePortalAcrossAxis(ArenaInfo.Center + Vector3.up * 200, (ArenaInfo.Width + Cyberloop.SmallGapSize), Vector3.forward, 80.0f, 500.0f, (int)(1 * Options.PortalMaxRecursionScalar.Value), true);
            var portalB = CreatePortalAcrossAxis(ArenaInfo.Center + Vector3.up * 200, (ArenaInfo.Width + Cyberloop.SmallGapSize), Vector3.right, 80.0f, 500.0f, (int)(1 * Options.PortalMaxRecursionScalar.Value), true);
            portalA.supportInfiniteRecursion = Options.PortalPreferSupportInfiniteRecursion.Value;
            portalB.supportInfiniteRecursion = Options.PortalPreferSupportInfiniteRecursion.Value;
            var rotationA = portalA.exit.rotation;
            var positionA = portalA.exit.position;
            var rotationB = portalB.exit.rotation;
            var positionB = portalB.exit.position;
            portalB.exit.rotation = rotationA;
            portalA.exit.rotation = rotationB;
            portalB.exit.position = positionA;
            portalA.exit.position = positionB;
        }

        protected override void OnClear()
        {
        }
    }
}