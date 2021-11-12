using Leopotam.Ecs;
using Model.Components.Body;
using Model.Components.Events;
using Model.Extensions;

namespace Model.Systems
{
    public sealed class GameOverSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<Player> _filter = null;
        void IEcsRunSystem.Run()
        {
            if (_filter.IsEmpty())
            {
                _world.SendMessage(new GameStateChangeEvent {State = GameStateEnum.GameOver});
            }
        }
    }
}