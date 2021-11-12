using Leopotam.Ecs;
using Model.Components.Body.Gun;
using Model.Components.Events;

namespace Model.Systems.Weapon
{
    public sealed class AmmoUsedSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<Ammo, ShotMadeEvent> _filter = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var gun = ref _filter.GetEntity(i);
                ref var ammo = ref _filter.Get1(i);
                ammo.Value--;

                if (ammo.Value == 0)
                {
                    gun.Del<Ammo>();
                }

                gun.Get<ShotMadeEvent>();
            }
        }
    }
}