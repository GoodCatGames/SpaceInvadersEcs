using System;
using Leopotam.Ecs;
using Model.Components.Body;
using Model.Components.Body.Bullet;
using Model.Components.Body.Gun;
using Model.Components.Requests;
using SpaceInvadersLeoEcs.Extensions;
using UnityEngine;
using UnityEngine.Serialization;

namespace SpaceInvadersLeoEcs.MappingUnityToModel.EntityFactoriesFromSo
{
    [CreateAssetMenu(fileName = "Bullet", menuName = "SpaceInvadersLeoEcs/Bullet", order = 10)]
    [Serializable]
    public class BulletEntityFactoryFromSo : EntityFactoryFromSo
    {
        [SerializeField] private Vector2 direction = Vector2.up;
        [SerializeField] private Health health = new Health() { Initial = 1, Current = 1};
        [SerializeField] private int damage = 1;
        
        public override EcsEntity CreateEntity(EcsWorld world)
        {
            var entity = world.NewEntity();
            entity.Get<Bullet>();
            entity.Get<Move>().Direct = direction;
            entity.Get<Health>() = health;
            entity.Get<Health>() = new Health { Initial = health.Initial, Current =  health.Initial };
            entity.Get<DamageContainer>().DamageRequest = new DamageRequest {Damage = damage};
            return entity;
        }
    }
}