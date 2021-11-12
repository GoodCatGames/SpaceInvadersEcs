using System.Collections.Generic;
using Model.Components.Events;
using Model.Extensions.EntityFactories;
using UnityEngine;

namespace Model.AppData
{
    public class GameContext
    {
        public GameStateEnum GameState = default;
        public Vector2 MaxBorderGameField = default;
        public Vector2 MinBorderGameField = default;

        // Mobs - Powers
        public Dictionary<IEntityFactory, float> MobFactoryToPowers = default;
    }
}