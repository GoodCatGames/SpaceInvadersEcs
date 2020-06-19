using Leopotam.Ecs;
using SpaceInvadersLeoEcs.AppData;
using SpaceInvadersLeoEcs.Components.Body.UI;
using SpaceInvadersLeoEcs.Components.Body.WrappersMonoBehaviour;
using SpaceInvadersLeoEcs.Extensions.Components;
using SpaceInvadersLeoEcs.Extensions.Systems.ViewCreate;
using SpaceInvadersLeoEcs.Systems.Model.Data;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceInvadersLeoEcs.Systems.View
{
    internal sealed class GunIndicatorViewCreateSystem : ViewCreateSystem<IsGunIndicatorComponent>
    {
        // auto-injected fields.
        private readonly SceneData _sceneData = null;
        private readonly GameConfiguration _gameConfiguration = null;
        
        protected override Transform CreateView(in EcsEntity entity, Vector3 startPosition)
        {
            var gunIndicatorPrefab = _gameConfiguration.GunUndicatorPrefab;
            
            var instantiate = Object.Instantiate(gunIndicatorPrefab, _sceneData.Canvas.transform);
            var transform = instantiate.transform;
            transform.localPosition = startPosition;
            
            entity.Get<ViewObjectComponent>().ViewObject = new ViewObjectUnity(transform);
            
            var text = transform.GetComponent<Text>();
            entity.Get<WrapperUnityObjectComponent<Text>>().Value = text;
            
            return transform;
        }
    }
}