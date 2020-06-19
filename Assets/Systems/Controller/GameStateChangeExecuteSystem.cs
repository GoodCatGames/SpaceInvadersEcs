using System;
using Leopotam.Ecs;
using SpaceInvadersLeoEcs.AppData;
using SpaceInvadersLeoEcs.Components.Body.GameManager;
using SpaceInvadersLeoEcs.Components.Requests;
using SpaceInvadersLeoEcs.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceInvadersLeoEcs.Systems.Controller
{
    internal sealed class GameStateChangeExecuteSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly GameContext _gameContext = null;
        private readonly SceneData _sceneData = null;
        private readonly AudioService _audioService = null;

        private readonly EcsFilter<ChangeGameStateRequest> _filter = null;
        private readonly EcsFilter<ScoreComponent> _filterScore = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var changeGameStateEvent = ref _filter.Get1(i);
                _gameContext.GameState = changeGameStateEvent.State;
                switch (changeGameStateEvent.State)
                {
                    case GameStates.Play:
                        Time.timeScale = 1f;
                        SetSplashScreen(false);
                        _audioService.UnPause();
                        break;

                    case GameStates.Pause:
                    case GameStates.GameOver:
                        Time.timeScale = 0f;
                        SetSplashScreen(true);
                        _sceneData.SplashScreenScore.text = GetScoreText();
                        _audioService.Pause();
                        break;
                    
                    case GameStates.Restart:
                        SceneManager.LoadScene(sceneBuildIndex: 0);
                        break;
                    
                    case GameStates.Exit:
                        Application.Quit();
                        break;
                    
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void SetSplashScreen(bool setActive) => _sceneData.SplashScreen.gameObject.SetActive(setActive);

        private string GetScoreText()
        {
            var score = GetScore();
            return score == 0 ? "" : $"Score: {score}";
        }

        private int GetScore() => _filterScore.IsEmpty() ? 0 : _filterScore.Get1(0).Value;
    }
}