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
            CreatePongPortal(((ArenaInfo.Center + Vector3.up * 200) + (Vector3.forward * (ArenaInfo.Width + 0.0001f))), Quaternion.LookRotation(Vector3.forward), ArenaInfo.Width * 2.0f, 500.0f);
            CreatePongPortal(((ArenaInfo.Center + Vector3.up * 200) + (Vector3.back * (ArenaInfo.Width + 0.0001f))), Quaternion.LookRotation(Vector3.back), ArenaInfo.Width * 2.0f, 500.0f);
            CreatePongPortal(((ArenaInfo.Center + Vector3.up * 200) + (Vector3.left * (ArenaInfo.Width + 0.0001f))), Quaternion.LookRotation(Vector3.left), ArenaInfo.Width * 2.0f, 500.0f);
            CreatePongPortal(((ArenaInfo.Center + Vector3.up * 200) + (Vector3.right * (ArenaInfo.Width + 0.0001f))), Quaternion.LookRotation(Vector3.right), ArenaInfo.Width * 2.0f, 500.0f);
        }

        protected override void OnClear()
        {
        }
    }
}