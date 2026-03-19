using System;
using UnityEngine;

namespace Nyxpiri.ULTRAKILL.Cyberloop
{
    public class CrossingThoseIslandsLoop : LoopLayout
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
            var portalSize = 120.0f;
            var portalA = CreatePortalAcrossAxis(ArenaInfo.Center, portalSize * 0.5f, Vector3.forward, portalSize, portalSize * 10.0f, (int)(1 * Options.PortalMaxRecursionScalar.Value), true);
            var portalB = CreatePortalAcrossAxis(ArenaInfo.Center, portalSize * 0.5f, Vector3.right, portalSize, portalSize * 10.0f, (int)(1 * Options.PortalMaxRecursionScalar.Value), true);
           
            portalA.supportInfiniteRecursion = Options.PortalPreferSupportInfiniteRecursion.Value;
            portalB.supportInfiniteRecursion = Options.PortalPreferSupportInfiniteRecursion.Value;

            var rotationA = portalA.exit.rotation;
            var positionA = portalA.exit.position;
            var rotationB = portalB.exit.rotation;
            var positionB = portalB.exit.position;

            portalA.exit.position = positionB;
            portalA.exit.rotation = rotationB;

            portalB.exit.position = positionA;
            portalB.exit.rotation = rotationA;
        }

        protected override void OnClear()
        {
        }
    }
}