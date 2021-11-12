using Leopotam.Ecs;
using Model.Components.Body.Mob;
using Model.Services;

namespace Model.Systems.Mobs
{
    public sealed class MobPowerBaseCalculateSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EvaluateService _evaluateService = null;
        private readonly EcsFilter<PowerGameDesign, Components.Body.Mob.Mob> _mobs = null;
        
        void IEcsRunSystem.Run()
        {
            foreach (var i in _mobs)
            {
                ref var mob = ref _mobs.GetEntity(i);
                ref var powerGameDesignBase = ref _mobs.Get1(i);
                powerGameDesignBase.Initial = _evaluateService.EvaluateGameDesignPower(mob);
            }
        }
    }
}