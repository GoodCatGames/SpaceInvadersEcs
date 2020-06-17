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
        private readonly GameContext _gameContext = null;
        public void Init()
        {
            var entity = _world.NewEntity();
            entity.Replace(new Score());

            var wrapper = new WrapperUnityObject<Text>() {Value = _gameContext.ScoreText};
            entity.Replace(wrapper);
        }
    }
}