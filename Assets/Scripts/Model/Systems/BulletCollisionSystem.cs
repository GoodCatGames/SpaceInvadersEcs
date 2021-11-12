using Leopotam.Ecs;
using Model.Components.Body;
using Model.Components.Body.Bullet;
using Model.Components.Body.Gun;
using Model.Components.Body.Mob;
using Model.Components.Events.UnityEvents;
using Model.Components.Requests;
using Model.Extensions.Components;
using UnityEngine;

namespace Model.Systems
{
    public sealed class BulletCollisionSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<ContainerComponents<OnCollisionEnter2DEvent>, DamageContainer, Bullet> _filter =
            null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var bullet = ref _filter.GetEntity(i);
                ref var bulletHealthCurrent = ref bullet.Get<Health>().Current;

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
            var damage  = bullet.Has<DamageRequest>() ? bullet.Get<DamageRequest>().Damage : 0;
            return Mathf.Clamp(healthCurrent - damage, 0, int.MaxValue);
        }

        private void ProcessBulletCollision(in EcsEntity bullet, in EcsEntity otherEntity)
        {
            if (!otherEntity.IsAlive()) return;

            var damage = bullet.Get<DamageContainer>().DamageRequest.Damage;

            // Add DamageRequest to OtherEntity
            otherEntity.Get<DamageRequest>().Damage += damage;

            // Add DamageRequest to Bullet (Bullet destruction)
            var resistance = otherEntity.Has<BulletResistance>() ? otherEntity.Get<BulletResistance>().Value : 0;
            bullet.Get<DamageRequest>().Damage += resistance;
        }
    }
}