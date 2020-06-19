using System;
using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Components.Body.Mob;
using SpaceInvadersLeoEcs.Extensions.Blueprints;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Blueprints
{
    [CreateAssetMenu(fileName = "Mob", menuName = "SpaceInvadersLeoEcs/Mob", order = 10)]
    [Serializable]
    public class MobBlueprint : Blueprint
    {
        [SerializeField] private float speed = default;
        [SerializeField] private HealthBaseComponent healthBaseComponent = default;
        [SerializeField] private BulletResistanceComponent bulletResistanceComponent = default;

        public override EcsEntity CreateEntity(EcsWorld world)
        {
            var entity = world.NewEntity();
            entity.Get<IsMobComponent>();
            entity.Get<PowerGameDesignBaseComponent>();
            entity.Get<PowerGameDesignCurrentComponent>();
            entity.Get<MoveComponent>() = new MoveComponent() {Direct = Vector2.down, Speed = speed};
            entity.Get<HealthBaseComponent>() = healthBaseComponent;
            entity.Get<BulletResistanceComponent>() = bulletResistanceComponent;
            entity.Get<HealthCurrentComponent>().Value = healthBaseComponent.Value;
            return entity;
        }
    }
}