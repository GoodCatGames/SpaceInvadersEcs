using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Components.Body.Bullet;
using SpaceInvadersLeoEcs.Components.Body.Gun;
using SpaceInvadersLeoEcs.Components.Body.Mob;
using SpaceInvadersLeoEcs.Components.Events.UnityEvents;
using SpaceInvadersLeoEcs.Components.Requests;
using SpaceInvadersLeoEcs.Extensions.Components;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Systems.Model
{
    internal sealed class BulletCollisionSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<ContainerComponents<OnCollisionEnter2DEvent>, ContainerDamageComponent, IsBulletComponent> _filter =
            null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var bullet = ref _filter.GetEntity(i);
                ref var bulletHealthCurrent = ref bullet.Get<HealthCurrentComponent>().Value;

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

        private int GetCalculateBulletHealthCurrent(in EcsEntity bullet, in int healthCurrent)
        {
            var damage  = bullet.Has<MakeDamageRequest>() ? bullet.Get<MakeDamageRequest>().Damage : 0;
            return Mathf.Clamp(healthCurrent - damage, 0, int.MaxValue);
        }

        private void ProcessBulletCollision(in EcsEntity bullet, in EcsEntity otherEntity)
        {
            if (!otherEntity.IsAlive()) return;

            var damage = bullet.Get<ContainerDamageComponent>().DamageRequest.Damage;

            // Add DamageRequest to OtherEntity
            otherEntity.Get<MakeDamageRequest>().Damage += damage;

            // Add DamageRequest to Bullet (Bullet destruction)
            var resistance = otherEntity.Has<BulletResistanceComponent>() ? otherEntity.Get<BulletResistanceComponent>().Value : 0;
            bullet.Get<MakeDamageRequest>().Damage += resistance;
        }
    }
}