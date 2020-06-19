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
    [CreateAssetMenu(fileName = "Bullet", menuName = "SpaceInvadersLeoEcs/Bullet", order = 10)]
    [Serializable]
    public class BulletBlueprint : Blueprint
    {
        [SerializeField] private Vector2 direction = Vector2.up;
        [SerializeField] private HealthBaseComponent healthBaseComponent = new HealthBaseComponent() { Value = 1};
        [SerializeField] private int damage = 1;
        
        public override EcsEntity CreateEntity(EcsWorld world)
        {
            var entity = world.NewEntity();
            entity.Get<IsBulletComponent>();
            entity.Get<MoveComponent>().Direct = direction;
            entity.Get<HealthBaseComponent>() = healthBaseComponent;
            entity.Get<HealthCurrentComponent>().Value = healthBaseComponent.Value;
            entity.Get<ContainerDamageComponent>().DamageRequest = new MakeDamageRequest() {Damage = damage};
            return entity;
        }
    }
}