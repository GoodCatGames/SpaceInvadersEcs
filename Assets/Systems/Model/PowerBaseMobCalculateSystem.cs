using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Components.Body.Mob;
using SpaceInvadersLeoEcs.Services;

namespace SpaceInvadersLeoEcs.Systems.Model
{
    internal sealed class PowerBaseMobCalculateSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EvaluateService _evaluateService = null;
        private readonly EcsFilter<PowerGameDesignBaseComponent, IsMobComponent> _mobs = null;
        
        void IEcsRunSystem.Run()
        {
            foreach (var i in _mobs)
            {
                ref var mob = ref _mobs.GetEntity(i);
                ref var powerGameDesignBase = ref _mobs.Get1(i);
                powerGameDesignBase.Power = _evaluateService.EvaluateGameDesignPower(mob);
            }
        }
    }
}