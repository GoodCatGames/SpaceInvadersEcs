using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body.Gun;

namespace SpaceInvadersLeoEcs.Systems.Model.Weapon
{
    internal sealed class DeniedShootReload : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<Shooting, IsReloadGunInProcess> _filter = null;
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                var entity = _filter.GetEntity(i);
                entity.Del<Shooting>();
            }
        }
    }
}