using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Requests;
using SpaceInvadersLeoEcs.Extensions;

namespace SpaceInvadersLeoEcs
{
    internal class GameInitSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        
        public void Init()
        {
            _world.SendMessage(new ChangeGameStateRequest() {State = GameStates.Pause});

        }
    }
}