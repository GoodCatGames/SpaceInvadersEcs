using Leopotam.Ecs;
using Model.Components.Body;
using Model.Components.Body.UI;
using Model.Components.Requests;
using SpaceInvadersLeoEcs.Extensions.Components;
using SpaceInvadersLeoEcs.Extensions.Systems;
using SpaceInvadersLeoEcs.MappingUnityToModel;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceInvadersLeoEcs.View.Systems.Create
{
    internal sealed class GunIndicatorViewCreateSystem : ViewCreateSystem<ViewCreateRequest, GunIndicator> 
    {
        // auto-injected fields.
        private readonly AppConfiguration _appConfiguration = null;
        private readonly SceneData _sceneData = null;

        protected override Transform GetTransform(in EcsEntity entity, in ViewCreateRequest data)
        {
            var gunIndicatorPrefab = _appConfiguration.GunIndicatorPrefab;
            
            var instantiate = Object.Instantiate(gunIndicatorPrefab, _sceneData.Canvas.transform);
            var transform = instantiate.transform;
            transform.localPosition = data.StartPosition;
            
            entity.Get<ViewObjectComponent>().ViewObject = new ViewObjectUnity(transform);
            
            var text = transform.GetComponent<Text>();
            entity.Get<UnityComponent<Text>>().Value = text;
            
            return transform;
        }
    }
}