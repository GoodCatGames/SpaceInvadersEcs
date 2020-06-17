using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body.Player;
using SpaceInvadersLeoEcs.Components.Requests;
using SpaceInvadersLeoEcs.Extensions;

namespace SpaceInvadersLeoEcs.Systems
{
    internal sealed class GameOverSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<PlayerComponent> _filter = null;
        void IEcsRunSystem.Run()
        {
            if (_filter.IsEmpty())
            {
                _world.SendMessage(new ChangeGameStateRequest() {State = GameStates.GameOver});
            }
        }
    }
}