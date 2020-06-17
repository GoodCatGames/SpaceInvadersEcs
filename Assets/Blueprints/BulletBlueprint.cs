using System;
using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Components.Body.Bullet;
using SpaceInvadersLeoEcs.Components.Body.Gun;
using SpaceInvadersLeoEcs.Components.Requests;
using SpaceInvadersLeoEcs.Extensions.Blueprints;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Blueprints
{
    [CreateAssetMenu(fileName = "Bullet", menuName = "SpaceInvandersLeoEcs/Bullet", order = 0)]
    [Serializable]
    public class BulletBlueprint : Blueprint
    {
        [SerializeField] private Vector2 direction = Vector2.up;
        [SerializeField] private HealthBase healthBase = new HealthBase() { Value = 1};
        [SerializeField] private int damage = 1;
        
        public override EcsEntity CreateEntity(EcsWorld world)
        {
            var entity = world.NewEntity();
            
            entity.Get<IsBulletComponent>();

            ref var moveComponent = ref entity.Get<MoveComponent>();
            moveComponent.Direct = direction;
            
            entity.Replace(healthBase);
            entity.Replace(new HealthCurrent() {Value = healthBase.Value});
            entity.Replace(new ContainerDamage() {DamageRequest = new MakeDamageRequest() {Damage = damage}});
            return entity;
        }
    }
}