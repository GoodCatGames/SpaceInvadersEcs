using System;
using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Blueprints;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Components.Body.Gun;
using SpaceInvadersLeoEcs.Components.Body.WrappersMonoBehaviour;
using SpaceInvadersLeoEcs.Components.Events;
using SpaceInvadersLeoEcs.Components.Requests;
using SpaceInvadersLeoEcs.Extensions.Blueprints;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Systems.Model.Weapon
{
    public class ShootExecuteSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<BlueprintRefComponent<BulletBlueprint>, ShootingComponent, OwnerPlayerComponent, BulletSpeedComponent> _filter = null;
        
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var blueprintRefComponent =  ref _filter.Get1(i);
                ref var ownerComponent = ref _filter.Get3(i);
                ref var bulletSpeed = ref _filter.Get4(i);

                if(!ownerComponent.PlayerEntity.Has<ViewObjectComponent>()) throw new Exception();

                ref var viewObjectComponent = ref ownerComponent.PlayerEntity.Get<ViewObjectComponent>();

                var positionGun = viewObjectComponent.ViewObject.Position;
                CreateBullet(blueprintRefComponent.Value, positionGun, bulletSpeed.Value);

                ref var gun = ref _filter.GetEntity(i);
                MessageShotMade(gun);
            }
        }

        private void CreateBullet(BulletBlueprint bulletBlueprint, in Vector2 position, in float speed)
        {
            var entity = bulletBlueprint.CreateEntity(_world);
            entity.Get<MoveComponent>().Speed = speed;
            entity.Get<CreateViewRequest>().StartPosition = position;
        }

        private void MessageShotMade(in EcsEntity gun) => gun.Get<IsShotMadeEvent>();
    }
}