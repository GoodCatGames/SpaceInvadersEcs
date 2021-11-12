using System;
using Leopotam.Ecs;
using Model.Components.Body;
using Model.Components.Body.Mob;
using SpaceInvadersLeoEcs.Extensions;
using UnityEngine;
using UnityEngine.Serialization;

namespace SpaceInvadersLeoEcs.MappingUnityToModel.EntityFactoriesFromSo
{
    [CreateAssetMenu(fileName = "Mob", menuName = "SpaceInvadersLeoEcs/Mob", order = 10)]
    [Serializable]
    public class MobEntityFactoryFromSo : EntityFactoryFromSo
    {
        [SerializeField] private float speed = default;
        [SerializeField] private Health health = default;
        [SerializeField] private BulletResistance bulletResistance = default;

        public override EcsEntity CreateEntity(EcsWorld world)
        {
            var entity = world.NewEntity();
            entity.Get<Mob>();
            entity.Get<PowerGameDesign>();
            entity.Get<Move>() = new Move {Direct = Vector2.down, Speed = speed};
            entity.Get<Health>() = health;

            entity.Get<BulletResistance>() = bulletResistance;
            return entity;
        }
    }
}