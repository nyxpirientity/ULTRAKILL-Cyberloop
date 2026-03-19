using System;
using UnityEngine;

namespace Nyxpiri.ULTRAKILL.Cyberloop
{
    public class HorizontalPongLoop : LoopLayout
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
            CreatePongPortal(((ArenaInfo.Center + Vector3.up * 200) + (Vector3.forward * (ArenaInfo.Width + Cyberloop.SmallGapSize))), Quaternion.LookRotation(Vector3.forward), ArenaInfo.Width * 2.0f, 500.0f, (int)(1 * Options.PortalMaxRecursionScalar.Value));
            CreatePongPortal(((ArenaInfo.Center + Vector3.up * 200) + (Vector3.back * (ArenaInfo.Width + Cyberloop.SmallGapSize))), Quaternion.LookRotation(Vector3.back), ArenaInfo.Width * 2.0f, 500.0f, (int)(1 * Options.PortalMaxRecursionScalar.Value));
            CreatePongPortal(((ArenaInfo.Center + Vector3.up * 200) + (Vector3.left * (ArenaInfo.Width + Cyberloop.SmallGapSize))), Quaternion.LookRotation(Vector3.left), ArenaInfo.Width * 2.0f, 500.0f, (int)(1 * Options.PortalMaxRecursionScalar.Value));
            CreatePongPortal(((ArenaInfo.Center + Vector3.up * 200) + (Vector3.right * (ArenaInfo.Width + Cyberloop.SmallGapSize))), Quaternion.LookRotation(Vector3.right), ArenaInfo.Width * 2.0f, 500.0f, (int)(1 * Options.PortalMaxRecursionScalar.Value));
        }

        protected override void OnClear()
        {
        }
    }
}