using Leopotam.Ecs;
using Model.Components.Body.Mob;
using Model.Components.Events;
using Model.Components.Requests;
using Model.Services;

namespace Model.Systems.Mobs
{
    public sealed class MobPowerCalculateSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EvaluateService _evaluateService = null;
        
        private readonly EcsFilter<PowerGameDesign, Components.Body.Mob.Mob, HealthChangeEvent> _mobsChangeHealth = null;
        private readonly EcsFilter<PowerGameDesign, Components.Body.Mob.Mob, EntityDestroyRequest> _mobsDied = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _mobsChangeHealth)
            {
                ref var mob = ref _mobsChangeHealth.GetEntity(i);
                ref var powerGameDesign = ref _mobsChangeHealth.Get1(i);
                SetPower(mob, ref powerGameDesign);
            }

            foreach (var i in _mobsDied)
            {
                ref var mob = ref _mobsDied.GetEntity(i);
                ref var powerGameDesignCurrent = ref _mobsDied.Get1(i);
                SetPower(mob, ref powerGameDesignCurrent);
            }
        }

        private void SetPower(in EcsEntity mob,
            ref PowerGameDesign powerGameDesign)
        {
            var power = _evaluateService.EvaluateGameDesignPower(mob);
            powerGameDesign.Current = power;
        }
    }
}