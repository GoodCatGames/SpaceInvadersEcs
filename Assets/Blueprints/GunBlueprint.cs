using System;
using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body.Gun;
using SpaceInvadersLeoEcs.Extensions.Blueprints;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Blueprints
{
    [CreateAssetMenu(fileName = "Gun", menuName = "SpaceInvadersLeoEcs/Gun", order = 10)]
    [Serializable]
    public class GunBlueprint : Blueprint
    {
        [SerializeField] private BulletBlueprint bulletBlueprint = default;
        [SerializeField] private BulletSpeedComponent bulletSpeedComponent = new BulletSpeedComponent() {Value = 100f};
        [SerializeField] private AmmoCapacityComponent ammoCapacityComponent = new AmmoCapacityComponent() {Value = 6};

        [SerializeField] private TimeBetweenShotsSetupComponent timeBetweenShotsSetupComponent =
            new TimeBetweenShotsSetupComponent() {TimeSec = 0.15f};

        [SerializeField] private ReloadGunComponent reloadGunComponent = new ReloadGunComponent() {TimeReloadSec = 2};

        public override EcsEntity CreateEntity(EcsWorld world)
        {
            var gun = world.NewEntity();
            gun.Get<IsCanShootComponent>();
            gun.Get<BulletSpeedComponent>() = bulletSpeedComponent;
            gun.Get<TimeBetweenShotsSetupComponent>() = timeBetweenShotsSetupComponent;
            gun.Get<AmmoCapacityComponent>() = ammoCapacityComponent;
            gun.Get<ReloadGunComponent>() = reloadGunComponent;
            gun.Get<BlueprintRefComponent<BulletBlueprint>>().Value = bulletBlueprint;
            return gun;
        }
    }
}