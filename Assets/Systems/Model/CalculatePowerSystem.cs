using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Components.Body.Mob;
using SpaceInvadersLeoEcs.Services;

namespace SpaceInvadersLeoEcs.Systems.Model
{
    internal sealed class CalculatePowerSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EvaluateService _evaluateService = null;
        private readonly EcsFilter<PowerGameDesignBase, PowerGameDesignCurrent, IsMob> _filter = null;
        
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                var entity = _filter.GetEntity(i);
                ref var powerGameDesignBase = ref _filter.Get1(i);
                if (powerGameDesignBase.Power == default)
                {
                    powerGameDesignBase.Power = _evaluateService.EvaluateGameDesignPower(entity);
                }
                
                ref var powerGameDesignCurrent = ref _filter.Get2(i);
                var power = _evaluateService.EvaluateGameDesignPower(entity);
                powerGameDesignCurrent.Power = power;
            }
        }
    }
}