using Leopotam.Ecs;
using Model.Components.Body;
using Model.Components.Events;
using SpaceInvadersLeoEcs.Extensions.Components;
using SpaceInvadersLeoEcs.MappingUnityToModel;
using UnityEngine.UI;

namespace SpaceInvadersLeoEcs.View.Systems.Update
{
    internal class ScoreViewUpdateSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Score, UnityComponent<Text>, ViewUpdateRequest> _filterScoreUpdateRequest = null;

        void IEcsRunSystem.Run()
        {
            if (_filterScoreUpdateRequest.IsEmpty() == false)
            {
                ref var score = ref _filterScoreUpdateRequest.Get1(0);
                ref var textComponent = ref _filterScoreUpdateRequest.Get2(0);
                textComponent.Value.text = score.Value.ToString();
            }
        }
    }
}