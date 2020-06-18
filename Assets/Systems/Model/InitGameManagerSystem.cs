using Leopotam.Ecs;
using SpaceInvadersLeoEcs.AppData;
using SpaceInvadersLeoEcs.Components.Body.GameManager;
using SpaceInvadersLeoEcs.Extensions.Components;
using UnityEngine.UI;

namespace SpaceInvadersLeoEcs.Systems.Model
{
    internal class InitGameManagerSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        private readonly SceneData _sceneData = null;
        public void Init()
        {
            var entity = _world.NewEntity();
            entity.Get<Score>();

            var wrapper = new WrapperUnityObject<Text>() {Value = _sceneData.ScoreText};
            entity.Get<WrapperUnityObject<Text>>() = wrapper;
        }
    }
}