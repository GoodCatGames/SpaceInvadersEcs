using System;
using System.Collections.Generic;
using SpaceInvadersLeoEcs.Blueprints;
using SpaceInvadersLeoEcs.Components.Requests;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceInvadersLeoEcs.AppData
{
    internal class GameContext
    {
        public GameStates GameState;
        public Vector2 MaxBorderGameField = default;
        public Vector2 MinBorderGameField = default;

        // Mobs - Powers
        public Dictionary<MobBlueprint, float> MobBlueprintPowers = default;
    }
}