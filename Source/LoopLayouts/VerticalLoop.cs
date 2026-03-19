using System;
using UnityEngine;

namespace Nyxpiri.ULTRAKILL.Cyberloop
{
    public class VerticalLoop : LoopLayout
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
            float spacing = 150.0f;
            CreatePortalAcrossAxis(ArenaInfo.Center + ((Vector3.up * (((ArenaInfo.DeathHeight * 0.4f) + 0.5f) + (spacing * 0.5f)))), spacing * 0.5f, Vector3.up, 500.0f, 500.0f, 1, false);

            CreateFallDeathSafetyPortals(260, render: false);
        }

        protected override void OnClear()
        {
        }
    }
}