using System;
using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Blueprints;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Components.Body.Gun;
using SpaceInvadersLeoEcs.Components.Body.WrappersMonoBehaviour;
using SpaceInvadersLeoEcs.Components.Requests;
using SpaceInvadersLeoEcs.Extensions.Blueprints;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Systems.Model.Weapon
{
    public class BulletsShootSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<BlueprintRefComponent<BulletBlueprint>, Shooting, OwnerPlayerComponent, BulletSpeed> _filter = null;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                var blueprintRefComponent = _filter.Get1(i);
                var ownerComponent = _filter.Get3(i);
                var bulletSpeed = _filter.Get4(i);

                if(!ownerComponent.PlayerEntity.Has<ViewObjectComponent>()) throw new Exception();

                var viewObjectComponent = ownerComponent.PlayerEntity.Get<ViewObjectComponent>();

                var positionGun = viewObjectComponent.ViewObject.Position;
                CreateBullet(blueprintRefComponent.Value, positionGun, bulletSpeed.Value);

                var gun = _filter.GetEntity(i);
                MessageShotMade(gun);
            }
        }

        private void CreateBullet(BulletBlueprint bulletBlueprint, Vector2 position, float speed)
        {
            var entity = bulletBlueprint.CreateEntity(_world);

            ref var moveComponent = ref entity.Get<MoveComponent>();
            moveComponent.Speed = speed;
            entity.Replace(new CreateViewRequest() {StartPosition = position});
        }

        private void MessageShotMade(EcsEntity gun) => gun.Replace(new IsShotMakeRequest());
    }
}