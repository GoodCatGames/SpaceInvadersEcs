using Leopotam.Ecs;
using SpaceInvadersLeoEcs.AppData;
using SpaceInvadersLeoEcs.Components.Body.GameManager;
using SpaceInvadersLeoEcs.Extensions.Components;
using UnityEngine.UI;

namespace SpaceInvadersLeoEcs.Systems.Model
{
    internal class ScoreInitSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        private readonly SceneData _sceneData = null;

        void IEcsInitSystem.Init()
        {
            _world.NewEntity().Get<ScoreComponent>();
            _world.NewEntity().Get<WrapperUnityObjectComponent<Text>>().Value = _sceneData.ScoreText;
        }
    }
}