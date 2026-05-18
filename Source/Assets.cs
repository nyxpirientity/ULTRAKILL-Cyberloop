using Nyxpiri.ULTRAKILL.NyxLib;
using ULTRAKILL.Portal;
using ULTRAKILL.Portal.Geometry;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nyxpiri.ULTRAKILL.Cyberloop
{
    public static class Assets
    {
        public static GameObject InfPortalPrefab = null;
        
        internal static void Initialize()
        {
            ScenesEvents.OnSceneWasLoaded += OnSceneLoaded;
        }

        private static void OnSceneLoaded(Scene scene, string levelName, string sceneName)
        {
            InfPortalPrefab = new GameObject();
            var portal = InfPortalPrefab.AddComponent<Portal>();
            InfPortalPrefab.SetActive(false);
            GameObject.DontDestroyOnLoad(InfPortalPrefab);
            GameObject entry = new GameObject();
            entry.transform.parent = InfPortalPrefab.gameObject.transform;
            GameObject exit = new GameObject();
            exit.transform.parent = InfPortalPrefab.gameObject.transform;
            portal.exit = exit.transform;
            portal.entry = entry.transform;
            portal.shape = new PlaneShape();
        }
    }
}