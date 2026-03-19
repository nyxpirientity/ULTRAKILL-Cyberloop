using UnityEngine;
using BepInEx;
using Nyxpiri.ULTRAKILL.NyxLib;
using UnityEngine.SceneManagement;
using System;
using ULTRAKILL.Portal;
using System.Collections.Generic;
using ULTRAKILL.Portal.Geometry;
using TMPro;
using Nyxpiri.Unity.Collections;
using HarmonyLib;

namespace Nyxpiri.ULTRAKILL.Cyberloop
{

    public static class Cheats
    {
        public const string Cyberloop = "nyxpiri.cyber-loop";
    }

    [BepInPlugin("nyxpiri.ultrakill.cyber-loop", "Cyberloop", "0.0.0.0")]
    [BepInDependency("nyxpiri.ultrakill.nyxlib", BepInDependency.DependencyFlags.HardDependency)]
    [BepInProcess("ULTRAKILL.exe")]
    public class Cyberloop : BaseUnityPlugin
    {
        internal static float SmallGapSize = 0.001f;

        protected void Awake()
        {
            Log.Initialize(Logger);
            Options.Initialize(this);
            LevelQuickLoader.AddQuickLoadLevel("Level 8-1");
            ScenesEvents.OnSceneWasLoaded += OnSceneLoaded;
            NyxLib.Cheats.ReadyForCheatRegistration += RegisterCheats;
            Cybergrind.PostCybergrindNextWave += (cancelInfo, endlessGrid) => 
            { 
                if (cancelInfo.Cancelled) 
                { 
                    return; 
                }

                if (!Looping)
                {
                    return;
                }
                
                NextLoop(); 
            };

            Harmony.CreateAndPatchAll(GetType().Assembly);
        }

        private void NextLoop()
        {
            if (!IsCheatActive)
            {
                DestroyLoop();
                return;
            }

            if (Layouts.Count == 0)
            {
                DestroyLoop();
                return;
            }

            _currentLoop += 1;
            _currentLoop %= Layouts.Count;
            DestroyLoop();

            if (_currentLoop == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    Layouts.Shuffle();
                    
                    if (Layouts[0] != PrevLayout)
                    {
                        break;
                    }           
                }
            }

            if (NewMovement.Instance.transform.position.y < 0.0f)
            {
                _currentLoop = Layouts.FindIndex(_currentLoop, (layout) => layout.IsBelowArenaSafe);

                if (_currentLoop == -1)
                {
                    Layouts.Shuffle();
                    _currentLoop = Layouts.FindIndex(0, (layout) => layout.IsBelowArenaSafe);
                }
            }
            
            if (_currentLoop == -1)
            {
                return;
            }

            CurrentLayout = Layouts[_currentLoop];

            CurrentLayout.Generate();
        }

        private void RegisterCheats(CheatsManager cheatsManager)
        {
            cheatsManager.RegisterCheat(new ToggleCheat(
                "Cyberloop", 
                Cheats.Cyberloop,
                onEnable: (cheat, cheatsManager) =>
                {
                    if (!Cybergrind.IsInCybergrindLevel)
                    {
                        return;
                    }
                    
                    ActivateLooping();
                },
                onDisable: (cheats) =>
                {
                    if (!Cybergrind.IsInCybergrindLevel)
                    {
                        return;
                    }

                    DeactivateLooping();
                }
            ), "CYBERGRIND");
        }

        private void DeactivateLooping()
        {
            if (!Looping)
            {
                return;
            }

            Looping = false;
            DestroyLoop();
        }

        private void DestroyLoop()
        {
            CurrentLayout?.Disable();
            PrevLayout = CurrentLayout;
            CurrentLayout = null;
        }

        private void ActivateLooping()
        {
            if (Looping)
            {
                return;
            }

            if (InfPortalPrefab == null)
            {
                return;
            }

            Looping = true;
            
            LoadLayouts();
        }

        private void LoadLayouts()
        {
            //DestroyLoop();
            Layouts.Clear();
            
            if (Options.UseVerticalLoop.Value)
            {
                Layouts.Add(new VerticalLoop());
            }
            
            if (Options.UseVerticalPongLoop.Value)
            {
                Layouts.Add(new VerticalPongLoop());
            }

            if (Options.UseHorizontalLoop.Value)
            {
                Layouts.Add(new HorizontalLoop());
            }

            if (Options.UseHorizontalPongLoop.Value)
            {
                Layouts.Add(new HorizontalPongLoop());
            }

            if (Options.UseHorizontalRotationLoop.Value)
            {
                Layouts.Add(new HorizontalRotationLoop());
            }

            if (Options.UseFromEverywhereLoop.Value)
            {
                Layouts.Add(new FromEverywhereLoop());
            }
            
            if (Options.UseCrossingThoseManyIslandsLoop.Value)
            {
                Layouts.Add(new CrossingThoseManyIslandsLoop());
            }

            if (Options.UseCrossingThoseIslandsLoop.Value)
            {
                Layouts.Add(new CrossingThoseIslandsLoop());
            }

            Layouts.Shuffle();
        }

        private void OnSceneLoaded(Scene scene, string sceneName)
        {
            CurrentLayout?.Clear();
            CurrentLayout = null;
            Looping = false;

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

        protected void Start()
        {
        }

        protected void Update()
        {
            /*if (Input.GetKeyDown(KeyCode.F9))
            {
                Log.Message($"Player pos is {NewMovement.Instance.transform.position}");
            }*/
        }

        protected void LateUpdate()
        {
            
        }

        protected void FixedUpdate()
        {
            if (NyxLib.Cheats.IsCheatEnabled(Cheats.Cyberloop) && Cybergrind.IsInCybergrindLevel)
            {
                ActivateLooping();
            }
            else
            {
                DeactivateLooping();
            }
        }

        internal static GameObject InfPortalPrefab = null;
        LoopLayout PrevLayout = null;
        LoopLayout CurrentLayout = null;
        List<LoopLayout> Layouts = new List<LoopLayout>();
        private int _currentLoop = -1;

        public bool Looping { get; private set; } = false;
        public bool IsCheatActive { get => NyxLib.Cheats.IsCheatEnabled(Cheats.Cyberloop); }
    }
}
