using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Requests;
using SpaceInvadersLeoEcs.Extensions;

namespace SpaceInvadersLeoEcs.Systems.Model
{
    internal class GameInitSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        
        void IEcsInitSystem.Init()
        {
            _world.SendMessage(new ChangeGameStateRequest() {State = GameStates.Pause});
        }
    }
}