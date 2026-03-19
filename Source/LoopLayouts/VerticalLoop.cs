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
            float spacing = 260.0f;
            CreateFallDeathSafetyPortals(spacing);
        }

        protected override void OnClear()
        {
        }
    }
}