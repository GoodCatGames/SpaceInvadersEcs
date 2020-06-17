using System;
using System.Collections.Generic;
using SpaceInvadersLeoEcs.Blueprints;
using SpaceInvadersLeoEcs.Components.Requests;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceInvadersLeoEcs.AppData
{
    internal class GameContext : MonoBehaviour
    {
        [NonSerialized] public GameStates GameState;

        public PlayerConfig Player1Config = default;
        public PlayerConfig Player2Config = default;

        // Scene Objects
        public Camera Camera = default;
        public Canvas Canvas = default;

        public Canvas SplashScreen = default;
        public Text SplashScreenScore = default;

        public Text ScoreText = default;

        // Prefabs
        public GameObject GunUndicatorPrefab = default;

        // Blueprints
        public Dictionary<MobBlueprint, float> MobBlueprintPowers = default;

        [NonSerialized] public Vector2 MaxBorderScreen = default;
        [NonSerialized] public Vector2 MinBorderScreen = default;
    }
}