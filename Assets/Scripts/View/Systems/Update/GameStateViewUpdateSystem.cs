using System;
using Leopotam.Ecs;
using Model.Components.Body;
using Model.Components.Events;
using SpaceInvadersLeoEcs.Extensions.Components;
using SpaceInvadersLeoEcs.MappingUnityToModel;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceInvadersLeoEcs.View.Systems.Update
{
    internal sealed class GameStateViewUpdateSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly SceneData _sceneData = null;

        private readonly EcsFilter<GameStateChangeEvent> _filter = null;
        private readonly EcsFilter<Score> _filterScore = null;
        private readonly EcsFilter<UnityComponent<GunAudioUnityComponent>> _filterAudio = null;
        
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var changeGameStateEvent = ref _filter.Get1(i);
                //_gameContext.GameState = changeGameStateEvent.State;
                switch (changeGameStateEvent.State)
                {
                    case GameStateEnum.Play:
                        Time.timeScale = 1f;
                        SetSplashScreen(false);
                        AudioUnPause();
                        break;

                    case GameStateEnum.Pause:
                    case GameStateEnum.GameOver:
                        Time.timeScale = 0f;
                        SetSplashScreen(true);
                        _sceneData.SplashScreenScore.text = GetScoreText();
                        AudioPause();
                        break;
                    
                    case GameStateEnum.Restart:
                        SceneManager.LoadScene(sceneBuildIndex: 0);
                        break;
                    
                    case GameStateEnum.Exit:
                        Application.Quit();
                        break;
                    
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void AudioPause()
        {
            foreach (var i in _filterAudio)
            {
                var audioUnityComponent = _filterAudio.Get1(i).Value;
                audioUnityComponent.Pause();
            }
        }
        
        private void AudioUnPause()
        {
            foreach (var i in _filterAudio)
            {
                var audioUnityComponent = _filterAudio.Get1(i).Value;
                audioUnityComponent.UnPause();
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