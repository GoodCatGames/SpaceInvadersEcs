using Leopotam.Ecs;
using Model.AppData;
using Model.Components.Events;
using Model.Components.Events.InputEvents;
using Model.Extensions;

namespace Model.Systems.Input
{
    public sealed class GameStateInputSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsWorld _world = null;
        private readonly GameContext _gameContext = null;
        
        private readonly EcsFilter<InputPauseQuitEvent> _filterPauseQuit = null;
        private readonly EcsFilter<InputAnyKeyEvent> _filterAnyKey = null;

        void IEcsRunSystem.Run()
        {
            if (_filterPauseQuit.IsEmpty() == false)
            {
                if(_gameContext.GameState == GameStateEnum.Pause) SetGameState(GameStateEnum.Exit); 
                if(_gameContext.GameState == GameStateEnum.Play) SetGameState(GameStateEnum.Pause);
            }
            else
            {
                if (_filterAnyKey.IsEmpty() == false)
                {
                    if(_gameContext.GameState == GameStateEnum.Pause) SetGameState(GameStateEnum.Play);
                    if(_gameContext.GameState == GameStateEnum.GameOver) SetGameState(GameStateEnum.Restart);
                }
            }
        }

        private void SetGameState(in GameStateEnum gameState)
        {
            _gameContext.GameState = gameState;
            _world.SendMessage(new GameStateChangeEvent { State = gameState });
        }
    }
}