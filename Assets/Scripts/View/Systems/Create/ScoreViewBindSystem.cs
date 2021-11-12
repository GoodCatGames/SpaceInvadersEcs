using Leopotam.Ecs;
using Model.Components.Body;
using Model.Components.Requests;
using SpaceInvadersLeoEcs.Extensions.Components;
using SpaceInvadersLeoEcs.Extensions.Systems;
using SpaceInvadersLeoEcs.MappingUnityToModel;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceInvadersLeoEcs.View.Systems.Create
{
    public class ScoreViewBindSystem : ViewCreateSystem<ViewCreateRequest, Score>
    {
        private readonly SceneData _sceneData = null;
        
        protected override Transform GetTransform(in EcsEntity entity, in ViewCreateRequest data)
        {
            entity.Get<UnityComponent<Text>>().Value = _sceneData.ScoreText;
            return _sceneData.ScoreText.transform;
        }
    }
}