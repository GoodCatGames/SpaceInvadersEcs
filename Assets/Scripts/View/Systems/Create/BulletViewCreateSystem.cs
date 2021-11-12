using Leopotam.Ecs;
using Model.Components.Body;
using Model.Components.Body.Gun;
using Model.Components.Requests;
using SpaceInvadersLeoEcs.Extensions.Pooling;
using SpaceInvadersLeoEcs.Extensions.Systems;
using SpaceInvadersLeoEcs.View.Services;
using UnityEngine;

namespace SpaceInvadersLeoEcs.View.Systems.Create
{
    internal sealed class BulletViewCreateSystem : ViewCreateSystem<ViewCreateRequest, Bullet>
    {
        // auto-injected fields.
        private readonly PoolsObject _poolsObject = null;

        protected override Transform GetTransform(in EcsEntity entity, in ViewCreateRequest data)
        {
            var poolObject = (PoolObjectExt)_poolsObject.Bullets.Get();
            var transform = poolObject.PoolTransform;
            transform.localPosition = data.StartPosition;
            transform.gameObject.SetActive(true);

            var rigidbody2D = poolObject.Rigidbody2D;
            entity.Get<ViewObjectComponent>().ViewObject = new ViewObjectUnity(transform, rigidbody2D, poolObject);

            return transform;
        }
    }
}