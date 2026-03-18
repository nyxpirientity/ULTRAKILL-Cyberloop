using System;
using UnityEngine;

namespace Nyxpiri.ULTRAKILL.Cyberloop
{
    public class FromEverywhereLoop : LoopLayout
    {
        public override bool IsBelowArenaSafe => true;

        protected override void OnDisable()
        {
            
        }

        public override void FixedUpdate()
        {
        }

        public override void Generate()
        {
            var portalSize = 150.0f;
            var portalA = CreatePortalAcrossAxis(ArenaInfo.Center, portalSize * 0.5f, Vector3.forward, portalSize, portalSize, 1, true);
            var portalB = CreatePortalAcrossAxis(ArenaInfo.Center, portalSize * 0.5f, Vector3.right, portalSize, portalSize, 1, true);
            var portalC = CreatePortalAcrossAxis(ArenaInfo.Center, portalSize * 0.5f, Vector3.up, portalSize, portalSize, 1, true);
            CreateFallDeathSafetyPortals(500.0f, false);

            var rotationA = portalA.exit.rotation;
            var positionA = portalA.exit.position;
            var rotationB = portalB.exit.rotation;
            var positionB = portalB.exit.position;
            var rotationC = portalC.exit.rotation;
            var positionC = portalC.exit.position;

            portalA.exit.position = positionC;
            portalA.exit.rotation = rotationC;

            portalB.exit.position = positionA;
            portalB.exit.rotation = rotationA;

            portalC.exit.position = positionB;
            portalC.exit.rotation = rotationB;
        }

        protected override void OnClear()
        {
        }
    }
}