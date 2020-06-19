using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body.Gun;
using SpaceInvadersLeoEcs.Components.Body.WrappersMonoBehaviour;
using SpaceInvadersLeoEcs.Extensions.Systems.ViewCreate;
using SpaceInvadersLeoEcs.Pooling;
using SpaceInvadersLeoEcs.Services;
using SpaceInvadersLeoEcs.Systems.Model.Data;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Systems.View
{
    internal sealed class BulletViewCreateSystem : ViewCreateSystem<IsBulletComponent>
    {
        // auto-injected fields.
        private readonly AudioService _audioService = null;
        private readonly PoolsObject _poolsObject = null;

        protected override void CreateView(in EcsEntity entity, Vector3 startPosition)
        {
            var poolObject = (PoolObjectExt)_poolsObject.Bullets.Get();
            var transform = poolObject.PoolTransform;
            transform.localPosition = startPosition;
            transform.gameObject.SetActive(true);

            var rigidbody2D = poolObject.Rigidbody2D;
            entity.Get<ViewObjectComponent>().ViewObject = new ViewObjectUnity(transform, rigidbody2D, poolObject);

            _audioService.PlayShoot();
        }
    }
}