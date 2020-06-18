using Leopotam.Ecs;
using SpaceInvadersLeoEcs.AppData;
using SpaceInvadersLeoEcs.Components.Body.UI;
using SpaceInvadersLeoEcs.Components.Body.WrappersMonoBehaviour;
using SpaceInvadersLeoEcs.Extensions.Components;
using SpaceInvadersLeoEcs.Extensions.Systems.CreateView;
using SpaceInvadersLeoEcs.Systems.Model.Data;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceInvadersLeoEcs.Systems.View
{
    internal sealed class CreateGunIndicatorViewSystem : CreateViewSystem<IsGunIndicatorComponent>
    {
        // auto-injected fields.
        private readonly SceneData _sceneData = null;
        private readonly GameConfiguration _gameConfiguration = null;
        
        protected override void CreateView(ref EcsEntity entity, Vector3 startPosition)
        {
            var gunIndicatorPrefab = _gameConfiguration.GunUndicatorPrefab;
            
            var instantiate = Object.Instantiate(gunIndicatorPrefab, _sceneData.Canvas.transform);
            var transform = instantiate.transform;
            transform.localPosition = startPosition;
            
            entity.Replace(new ViewObjectComponent() {ViewObject = new ViewObjectUnity(transform)});
            
            var text = transform.GetComponent<Text>();
            entity.Replace(new WrapperUnityObject<Text>() { Value = text});
        }
    }
}