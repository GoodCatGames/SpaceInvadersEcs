using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Components.Body.Bullet;
using SpaceInvadersLeoEcs.Components.Body.Gun;
using SpaceInvadersLeoEcs.Components.Body.Mob;
using SpaceInvadersLeoEcs.Components.Events.UnityEvents;
using SpaceInvadersLeoEcs.Components.Requests;
using SpaceInvadersLeoEcs.Extensions.Components;
using SpaceInvadersLeoEcs.Extensions.Enitities;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Systems.Model
{
    internal sealed class BulletCollisionSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<ContainerComponents<OnCollisionEnter2DEvent>, ContainerDamage, IsBulletComponent> _filter =
            null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                var bullet = _filter.GetEntity(i);
                var bulletHealthCurrent = bullet.Get<HealthCurrent>().Value;

                var collisions = _filter.Get1(i).List;

                foreach (var collision in collisions)
                {
                    var calculateBulletHealthCurrent = GetCalculateBulletHealthCurrent(bullet, bulletHealthCurrent);
                    if(calculateBulletHealthCurrent == 0) break;
                    var otherEntity = collision.Other;
                    ProcessBulletCollision(bullet, otherEntity);
                }
            }
        }

        private int GetCalculateBulletHealthCurrent(EcsEntity bullet, int healthCurrent)
        {
            var damage = bullet.TryGet<MakeDamageRequest>(out var makeDamageRequest) ? makeDamageRequest.Damage : 0;
            return Mathf.Clamp(healthCurrent - damage, 0, int.MaxValue);
        }

        private void ProcessBulletCollision(EcsEntity bullet, EcsEntity otherEntity)
        {
            if (!otherEntity.IsAlive()) return;

            var containerDamage = bullet.Get<ContainerDamage>();
            var damage = containerDamage.DamageRequest.Damage;

            // Add DamageRequest to OtherEntity
            ref var makeDamageRequest = ref otherEntity.Get<MakeDamageRequest>();
            makeDamageRequest.Damage += damage;

            // Add DamageRequest to Bullet (Bullet destruction)
            var resistance = otherEntity.Has<BulletResistance>() ? otherEntity.Get<BulletResistance>().Value : 0;
            ref var damageRequest = ref bullet.Get<MakeDamageRequest>();
            damageRequest.Damage += resistance;
        }
    }
}