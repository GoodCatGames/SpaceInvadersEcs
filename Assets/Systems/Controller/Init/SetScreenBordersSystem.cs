using Leopotam.Ecs;
using SpaceInvadersLeoEcs.AppData;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Systems.Controller.Init
{
    sealed class SetScreenBordersSystem : IEcsInitSystem
    {
        private readonly GameContext _gameContext = null;
        public void Init() => SetScreenBorders();

        private void SetScreenBorders()
        {
            var current = _gameContext.Camera;
            _gameContext.MaxBorderScreen = current.ViewportToWorldPoint(new Vector2(1, 1));
            _gameContext.MinBorderScreen = current.ViewportToWorldPoint(new Vector2(0, 0));
        }
    }
}