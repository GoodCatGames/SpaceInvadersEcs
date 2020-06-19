using System.Collections.Generic;
using SpaceInvadersLeoEcs.Blueprints;
using SpaceInvadersLeoEcs.Components.Requests;
using UnityEngine;

namespace SpaceInvadersLeoEcs.AppData
{
    internal class GameContext
    {
        public GameStates GameState = default;
        public Vector2 MaxBorderGameField = default;
        public Vector2 MinBorderGameField = default;

        // Mobs - Powers
        public Dictionary<MobBlueprint, float> MobBlueprintPowers = default;
    }
}