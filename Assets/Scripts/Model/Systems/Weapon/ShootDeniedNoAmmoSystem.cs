using Leopotam.Ecs;
using Model.Components.Body.Gun;

namespace Model.Systems.Weapon
{
    public sealed class ShootDeniedNoAmmoSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<AmmoCapacity>.Exclude<Ammo> _filter = null; 
        
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                _filter.GetEntity(i).Del<Shooting>();
            }
        }
    }
}