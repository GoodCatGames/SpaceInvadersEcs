using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body.Mob;
using SpaceInvadersLeoEcs.Components.Body.WrappersMonoBehaviour;
using SpaceInvadersLeoEcs.Extensions.Components;
using SpaceInvadersLeoEcs.Extensions.Systems.CreateView;
using SpaceInvadersLeoEcs.Services;
using SpaceInvadersLeoEcs.Systems.Model.Data;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Systems.View
{
    internal sealed class CreateMobsViewSystem : CreateViewSystem<IsMob>
    {
        // auto-injected fields.
        private readonly PoolsObject _poolsObject = null;
       
        protected override void CreateView(ref EcsEntity entity, Vector3 startPosition)
        {
            var poolObject = _poolsObject.Mobs.Get();
            var transform = poolObject.PoolTransform;
            transform.localPosition = startPosition;
            transform.gameObject.SetActive(true);

            var rigidbody2D = transform.GetComponent<Rigidbody2D>();
            entity.Replace(new ViewObjectComponent() {ViewObject = new ViewObjectUnity(transform, rigidbody2D, poolObject)});

            var spriteRenderer = transform.GetComponent<SpriteRenderer>();
            entity.Replace(new WrapperUnityObject<SpriteRenderer>() {Value = spriteRenderer});
        }
    }
}