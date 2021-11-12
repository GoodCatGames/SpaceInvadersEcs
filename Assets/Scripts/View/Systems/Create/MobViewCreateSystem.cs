using Leopotam.Ecs;
using Model.Components.Body;
using Model.Components.Body.Mob;
using Model.Components.Requests;
using SpaceInvadersLeoEcs.Extensions.Components;
using SpaceInvadersLeoEcs.Extensions.Systems;
using SpaceInvadersLeoEcs.View.Services;
using UnityEngine;

namespace SpaceInvadersLeoEcs.View.Systems.Create
{
    internal sealed class MobViewCreateSystem : ViewCreateSystem<ViewCreateRequest, Mob>
    {
        // auto-injected fields.
        private readonly PoolsObject _poolsObject = null;
       
        protected override Transform GetTransform(in EcsEntity entity, in ViewCreateRequest data)
        {
            var poolObject = _poolsObject.Mobs.Get();
            var transform = poolObject.PoolTransform;
            transform.localPosition = data.StartPosition;
            transform.gameObject.SetActive(true);

            var rigidbody2D = transform.GetComponent<Rigidbody2D>();
            entity.Get<ViewObjectComponent>().ViewObject = new ViewObjectUnity(transform, rigidbody2D, poolObject);

            var spriteRenderer = transform.GetComponent<SpriteRenderer>();
            entity.Get<UnityComponent<SpriteRenderer>>().Value = spriteRenderer;
            
            return transform;
        }
    }
}