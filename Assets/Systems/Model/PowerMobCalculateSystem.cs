using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Components.Body.Mob;
using SpaceInvadersLeoEcs.Services;

namespace SpaceInvadersLeoEcs.Systems.Model
{
    internal sealed class PowerMobCalculateSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EvaluateService _evaluateService = null;
        private readonly EcsFilter<PowerGameDesignBaseComponent, PowerGameDesignCurrentComponent, IsMobComponent> _filter = null;
        
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var mob = ref _filter.GetEntity(i);
                ref var powerGameDesignBase = ref _filter.Get1(i);
                if (powerGameDesignBase.Power == default)
                {
                    powerGameDesignBase.Power = _evaluateService.EvaluateGameDesignPower(mob);
                }

                var power = _evaluateService.EvaluateGameDesignPower(mob);
                _filter.Get2(i).Power = power;
            }
        }
    }
}