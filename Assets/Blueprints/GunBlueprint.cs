using System;
using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body.Gun;
using SpaceInvadersLeoEcs.Components.Body.Timers;
using SpaceInvadersLeoEcs.Extensions.Blueprints;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Blueprints
{
    [CreateAssetMenu(fileName = "Gun", menuName = "SpaceInvadersLeoEcs/Gun", order = 10)]
    [Serializable]
    public class GunBlueprint : Blueprint
    {
        [SerializeField] private BulletBlueprint _bulletBlueprint;
        [SerializeField] private BulletSpeed _bulletSpeed = new BulletSpeed() {Value = 100f};
        [SerializeField] private AmmoCapacity _ammoCapacity = new AmmoCapacity() {Value = 6};
        [SerializeField] private TimeBetweenShotsSetupComponent _timeBetweenShotsSetupComponent =
            new TimeBetweenShotsSetupComponent() {TimeSec = 0.15f};
        
        [SerializeField] private ReloadGunComponent _reloadGunComponent = new ReloadGunComponent() {TimeReloadSec = 2};
        public override EcsEntity CreateEntity(EcsWorld world)
        {
            var gun = world.NewEntity();
            gun.Get<IsCanShootComponent>();

            gun.Replace(_bulletSpeed);
            gun.Replace(_timeBetweenShotsSetupComponent);
            gun.Replace(_ammoCapacity);
            gun.Replace(_reloadGunComponent);
            gun.Replace(new BlueprintRefComponent<BulletBlueprint>() {Value = _bulletBlueprint});
            return gun;
        }
    }
}