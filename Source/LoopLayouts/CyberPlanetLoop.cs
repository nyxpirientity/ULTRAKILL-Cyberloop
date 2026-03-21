using System;
using UnityEngine;

namespace Nyxpiri.ULTRAKILL.Cyberloop
{
    public class CybergrindPlanetLoop : LoopLayout
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
            var portalA = CreatePortalAcrossAxis(ArenaInfo.Center + Vector3.up * 200, (ArenaInfo.Width + Cyberloop.SmallGapSize), Vector3.forward, 80.0f, 500.0f, 1, false);
        }

        protected override void OnClear()
        {
        }
    }
}