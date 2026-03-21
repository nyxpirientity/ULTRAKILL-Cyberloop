using Nyxpiri.ULTRAKILL.NyxLib;
using ULTRAKILL.Portal;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nyxpiri.ULTRAKILL.Cyberloop
{
    public static class Assets
    {
        public  static GameObject InfPortalPrefab = null;
        
        internal static void Initialize()
        {
            ScenesEvents.OnSceneWasLoaded += OnSceneLoaded;
        }

        private static void OnSceneLoaded(Scene scene, string sceneName)
        {
            if (InfPortalPrefab == null)
            {
                var portals = UnityEngine.Object.FindObjectsByType<Portal>(FindObjectsInactive.Include, FindObjectsSortMode.None);

                foreach (var portal in portals)
                {
                    if (portal.supportInfiniteRecursion && !portal.mirror)
                    {
                        InfPortalPrefab = GameObject.Instantiate(portal.gameObject);
                        InfPortalPrefab.SetActive(false);
                        GameObject.DontDestroyOnLoad(InfPortalPrefab);
                        GameObject entry = new GameObject();
                        entry.transform.parent = InfPortalPrefab.gameObject.transform;
                        GameObject exit = new GameObject();
                        exit.transform.parent = InfPortalPrefab.gameObject.transform;
                        InfPortalPrefab.GetComponent<Portal>().exit = exit.transform;
                        InfPortalPrefab.GetComponent<Portal>().entry = entry.transform;
                        break;
                    }
                }
            }
        }
    }
}