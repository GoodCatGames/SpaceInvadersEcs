using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body.Gun;
using SpaceInvadersLeoEcs.Components.Events;
using SpaceInvadersLeoEcs.Components.Requests;

namespace SpaceInvadersLeoEcs.Systems.Model.Weapon
{
    internal sealed class AmmoUsedSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<Ammo, IsShotMakeRequest> _filter = null;
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                var gun = _filter.GetEntity(i);

                ref var ammo = ref _filter.Get1(i);
                ammo.Value--;
               
                if (ammo.Value == 0)
                {
                    gun.Del<Ammo>();
                }

                gun.Replace(new IsShotMadeEvent());
            }
        }
    }
}