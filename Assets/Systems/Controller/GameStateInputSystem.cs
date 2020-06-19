using Leopotam.Ecs;
using SpaceInvadersLeoEcs.AppData;
using SpaceInvadersLeoEcs.Components.Events.InputEvents;
using SpaceInvadersLeoEcs.Components.Requests;
using SpaceInvadersLeoEcs.Extensions;

namespace SpaceInvadersLeoEcs.Systems.Controller
{
    internal sealed class GameStateInputSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsWorld _world = null;
        private readonly GameContext _gameContext = null;
        
        private readonly EcsFilter<InputPauseQuitEvent> _filterPauseQuit = null;
        private readonly EcsFilter<InputAnyKeyEvent> _filterAnyKey = null;

        void IEcsRunSystem.Run()
        {
            if (!_filterPauseQuit.IsEmpty())
            {
                if(_gameContext.GameState == GameStates.Pause) SetGameState(GameStates.Exit); 
                if(_gameContext.GameState == GameStates.Play) SetGameState(GameStates.Pause);
            }
            else
            {
                if (!_filterAnyKey.IsEmpty())
                {
                    if(_gameContext.GameState == GameStates.Pause) SetGameState(GameStates.Play);
                    if(_gameContext.GameState == GameStates.GameOver) SetGameState(GameStates.Restart);
                }
            }
        }

        private void SetGameState(in GameStates gameState) => _world.SendMessage(new ChangeGameStateRequest() {State = gameState});
    }
}