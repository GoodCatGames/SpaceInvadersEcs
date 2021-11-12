using System;
using Leopotam.Ecs;
using Model.Components.Body.Gun;
using Model.Extensions.EntityFactories;
using SpaceInvadersLeoEcs.Extensions;
using UnityEngine;
using UnityEngine.Serialization;

namespace SpaceInvadersLeoEcs.MappingUnityToModel.EntityFactoriesFromSo
{
    [CreateAssetMenu(fileName = "Gun", menuName = "SpaceInvadersLeoEcs/Gun", order = 10)]
    [Serializable]
    public class GunEntityFactoryFromSo : EntityFactoryFromSo
    {
        [SerializeField] private BulletEntityFactoryFromSo bulletEntityFactoryFromSo = default;
        [SerializeField] private BulletSpeed bulletSpeed = new BulletSpeed {Value = 100f};
        [SerializeField] private AmmoCapacity ammoCapacity = new AmmoCapacity {Value = 6};

        [SerializeField] private TimeBetweenShotsSetup timeBetweenShotsSetup =
            new TimeBetweenShotsSetup {TimeSec = 0.15f};

        [SerializeField] private GunReload gunReload = new GunReload {TimeReloadSec = 2};

        public override EcsEntity CreateEntity(EcsWorld world)
        {
            var gun = world.NewEntity();
            gun.Get<ShootIsPossible>();
            gun.Get<BulletSpeed>() = bulletSpeed;
            gun.Get<TimeBetweenShotsSetup>() = timeBetweenShotsSetup;
            gun.Get<AmmoCapacity>() = ammoCapacity;
            gun.Get<GunReload>() = gunReload;
            gun.Get<EntityFactoryRef<IEntityFactory>>().Value = bulletEntityFactoryFromSo;
            return gun;
        }
    }
}