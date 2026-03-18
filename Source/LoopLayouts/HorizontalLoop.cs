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
            CreatePortalAcrossAxis(ArenaInfo.Center + Vector3.up * 200, (ArenaInfo.Width + 0.0001f), Vector3.forward, 80.0f, 500.0f, 1, false);
            CreatePortalAcrossAxis(ArenaInfo.Center + Vector3.up * 200, (ArenaInfo.Width + 0.0001f), Vector3.right, 80.0f, 500.0f, 1, false);
        }

        protected override void OnClear()
        {
        }
    }
}