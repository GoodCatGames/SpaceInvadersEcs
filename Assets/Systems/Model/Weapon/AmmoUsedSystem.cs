using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body.Gun;
using SpaceInvadersLeoEcs.Components.Events;

namespace SpaceInvadersLeoEcs.Systems.Model.Weapon
{
    internal sealed class AmmoUsedSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<AmmoComponent, IsShotMadeEvent> _filter = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var gun = ref _filter.GetEntity(i);
                ref var ammo = ref _filter.Get1(i);
                ammo.Value--;

                if (ammo.Value == 0)
                {
                    gun.Del<AmmoComponent>();
                }

                gun.Get<IsShotMadeEvent>();
            }
        }
    }
}