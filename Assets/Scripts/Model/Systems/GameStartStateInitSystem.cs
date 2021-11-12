using Leopotam.Ecs;
using Model.AppData;
using Model.Components.Events;
using Model.Extensions;

namespace Model.Systems
{
    public class GameStartStateInitSystem : IEcsInitSystem
    {
        private readonly GameContext _gameContext = null;
        private readonly EcsWorld _world = null;
        
        void IEcsInitSystem.Init()
        {
            _gameContext.GameState = GameStateEnum.Pause;
            _world.SendMessage(new GameStateChangeEvent {State = GameStateEnum.Pause});
        }
    }
}