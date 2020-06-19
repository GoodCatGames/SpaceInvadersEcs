using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body.Mob;
using SpaceInvadersLeoEcs.Components.Body.WrappersMonoBehaviour;
using SpaceInvadersLeoEcs.Extensions.Components;
using SpaceInvadersLeoEcs.Extensions.Systems.ViewCreate;
using SpaceInvadersLeoEcs.Services;
using SpaceInvadersLeoEcs.Systems.Model.Data;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Systems.View
{
    internal sealed class MobViewCreateSystem : ViewCreateSystem<IsMobComponent>
    {
        // auto-injected fields.
        private readonly PoolsObject _poolsObject = null;
       
        protected override Transform CreateView(in EcsEntity entity, in Vector3 startPosition)
        {
            var poolObject = _poolsObject.Mobs.Get();
            var transform = poolObject.PoolTransform;
            transform.localPosition = startPosition;
            transform.gameObject.SetActive(true);

            var rigidbody2D = transform.GetComponent<Rigidbody2D>();
            entity.Get<ViewObjectComponent>().ViewObject = new ViewObjectUnity(transform, rigidbody2D, poolObject);

            var spriteRenderer = transform.GetComponent<SpriteRenderer>();
            entity.Get<WrapperUnityObjectComponent<SpriteRenderer>>().Value = spriteRenderer;
            
            return transform;
        }
    }
}