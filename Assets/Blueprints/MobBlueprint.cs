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
        [SerializeField] private float speed;
        [SerializeField] private HealthBase healthBase;
        [SerializeField] private BulletResistance bulletResistance;
        
        public override EcsEntity CreateEntity(EcsWorld world)
        {
            var entity = world.NewEntity();
            entity.Replace(new IsMob());
            entity.Replace(new PowerGameDesignBase());
            entity.Replace(new PowerGameDesignCurrent());
            
            entity.Replace(new MoveComponent() {Direct = Vector2.down, Speed = speed});

            entity.Replace(healthBase);
            entity.Replace(bulletResistance);
            entity.Replace(new HealthCurrent() {Value = healthBase.Value});
            return entity;
        }
    }
}