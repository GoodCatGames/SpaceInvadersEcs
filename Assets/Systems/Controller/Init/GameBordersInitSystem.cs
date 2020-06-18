using Leopotam.Ecs;
using SpaceInvadersLeoEcs.AppData;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Systems.Controller.Init
{
    internal sealed class GameBordersInitSystem : IEcsInitSystem
    {
        private readonly SceneData _sceneData = null;
        private readonly GameContext _gameContext = null;
        public void Init() => SetGameBorders();

        private void SetGameBorders()
        {
            var current = _sceneData.Camera;
            _gameContext.MaxBorderGameField = current.ViewportToWorldPoint(new Vector2(1, 1));
            _gameContext.MinBorderGameField = current.ViewportToWorldPoint(new Vector2(0, 0));
        }
    }
}