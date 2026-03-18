using System;
using UnityEngine;

namespace Nyxpiri.ULTRAKILL.Cyberloop
{
    public class VerticalPongLoop : LoopLayout
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
            CreatePongPortal(((ArenaInfo.Center) + (Vector3.down * ((ArenaInfo.InvDeathHeight * 0.5f) - 0.5f))), Quaternion.LookRotation(Vector3.down), 500.0f, 500.0f);
            CreatePongPortal(((ArenaInfo.Center) + (Vector3.up * ArenaInfo.Width * 2.5f)), Quaternion.LookRotation(Vector3.up), 500.0f, 500.0f);
            CreateFallDeathSafetyPortals(200.0f, render: false);
        }

        protected override void OnClear()
        {
        }
    }
}