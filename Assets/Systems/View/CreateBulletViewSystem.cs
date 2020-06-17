using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body.Gun;
using SpaceInvadersLeoEcs.Components.Body.WrappersMonoBehaviour;
using SpaceInvadersLeoEcs.Extensions.Systems.CreateView;
using SpaceInvadersLeoEcs.Pooling;
using SpaceInvadersLeoEcs.Services;
using SpaceInvadersLeoEcs.Systems.Model.Data;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Systems.View
{
    internal sealed class CreateBulletViewSystem : CreateViewSystem<IsBulletComponent>
    {
        // auto-injected fields.
        private readonly AudioService _audioService = null;
        private readonly PoolsObject _poolsObject = null;

        protected override void CreateView(ref EcsEntity entity, Vector3 startPosition)
        {
            var poolObject = (PoolObjectExt)_poolsObject.Bullets.Get();
            var transform = poolObject.PoolTransform;
            transform.localPosition = startPosition;
            transform.gameObject.SetActive(true);

            ref var viewObjectComponent = ref entity.Get<ViewObjectComponent>();
            var rigidbody2D = poolObject.Rigidbody2D;
            viewObjectComponent.ViewObject = new ViewObjectUnity(transform, rigidbody2D, poolObject);

            _audioService.PlayShoot();
        }
    }
}