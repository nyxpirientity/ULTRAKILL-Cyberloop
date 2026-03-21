using System;
using UnityEngine;
using BepInEx;
using Nyxpiri.ULTRAKILL.NyxLib;
using UnityEngine.SceneManagement;
using ULTRAKILL.Portal;
using System.Collections.Generic;
using ULTRAKILL.Portal.Geometry;

namespace Nyxpiri.ULTRAKILL.Cyberloop
{
    public abstract class LoopLayout
    {
        public abstract bool IsBelowArenaSafe { get; }
        public abstract void Generate();
        public abstract void FixedUpdate();
        protected abstract void OnDisable();
        protected abstract void OnClear();
        
        public void Disable()
        {
            OnDisable();

            foreach (var portal in Portals)
            {
                if (portal == null || portal.gameObject == null)
                {
                    continue;
                }
                
                UnityEngine.Object.Destroy(portal.gameObject);
            }

            Clear();
        }

        protected Portal CreatePortalAcrossAxis(Vector3 center, float dist, Vector3 axis, float width, float height, int maxRecursions, bool canSeeItself)
        {
            var portalGo = UnityEngine.Object.Instantiate(Assets.InfPortalPrefab);

            var entryPosition = center + (axis * dist);
            var entryRotation = Quaternion.LookRotation(axis);
            var exitPosition = center + (axis * -dist);
            var exitRotation = Quaternion.LookRotation(-axis);
            var canSeePortalLayer = true;
            var supportInfiniteRecursion = false;
            var enterOffset = 1.5f;
            var exitOffset = 1.5f;

            var portal = CreatePortal(width, height, maxRecursions, canSeeItself, portalGo, entryPosition, entryRotation, exitPosition, exitRotation, canSeePortalLayer, supportInfiniteRecursion, enterOffset, exitOffset, PortalSideFlags.Enter | PortalSideFlags.Exit);
            return portal;
        }

        protected Portal CreatePortal(float width, float height, int maxRecursions, bool canSeeItself, GameObject portalGo, Vector3 entryPosition, Quaternion entryRotation, Vector3 exitPosition, Quaternion exitRotation, bool canSeePortalLayer, bool supportInfiniteRecursion, float enterOffset, float exitOffset, PortalSideFlags renderSettings)
        {
            var portal = portalGo.GetComponent<Portal>();
            portal.entry.position = entryPosition;
            portal.entry.rotation = entryRotation;
            portal.exit.position = exitPosition;
            portal.exit.rotation = exitRotation;
            var shape = (PlaneShape)portal.shape;
            shape.width = width;
            shape.height = height;
            portal.shape = shape;
            portal.enterOffset = enterOffset;
            portal.exitOffset = exitOffset;
            portal.supportInfiniteRecursion = supportInfiniteRecursion;
            portal.maxRecursions = maxRecursions;
            portal.canSeeItself = canSeeItself;
            portal.canSeePortalLayer = canSeePortalLayer;
            portal.renderSettings = renderSettings;
            Portals.Add(portal);
            portal.UpdateTransformData();
            portalGo.SetActive(true);

            return portal;
        }

        protected void CreateFallDeathSafetyPortals(float spacing, bool render = true)
        {
            var portal = CreatePortalAcrossAxis(ArenaInfo.Center + ((Vector3.up * ((ArenaInfo.DeathHeight + 0.5f) + (spacing * 0.5f)))), spacing * 0.5f, Vector3.up, 500.0f, 500.0f, 1, false);
            
            if (!render)
            {
                portal.renderSettings = PortalSideFlags.None;
            }
        }

        protected Portal CreatePongPortal(Vector3 position, Quaternion rotation, float width, float height, int maxRecursions = 1)
        {
            var portalGo = UnityEngine.Object.Instantiate(Assets.InfPortalPrefab);

            var portal = portalGo.GetComponent<Portal>();
            Portals.Add(portal);
            portal.entry.position = position;
            portal.entry.rotation = rotation;
            portal.exit.position = position;
            portal.exit.rotation = rotation;
            var shape = (PlaneShape)portal.shape;
            shape.width = width;
            shape.height = height;
            portal.shape = shape;
            portal.enterOffset = 1.5f;
            portal.exitOffset = 1.5f;
            portal.maxRecursions = maxRecursions;
            portal.supportInfiniteRecursion = false;
            portal.canSeeItself = false;
            portal.renderSettings = PortalSideFlags.Exit;
            portal.canSeePortalLayer = false;
            portal.UpdateTransformData();
            portalGo.SetActive(true);
            PongPortals.Add(portal);
            return portal;
        }

        public void Clear()
        {
            Portals.Clear();
            PongPortals.Clear();
            OnClear();
        }

        public List<Portal> Portals { get; private set; } = new List<Portal>();
        public List<Portal> PongPortals { get; private set; } = new List<Portal>();
    }
}