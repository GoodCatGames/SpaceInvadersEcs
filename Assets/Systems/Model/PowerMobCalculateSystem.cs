using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Components.Body.Mob;
using SpaceInvadersLeoEcs.Components.Events;
using SpaceInvadersLeoEcs.Components.Requests;
using SpaceInvadersLeoEcs.Services;

namespace SpaceInvadersLeoEcs.Systems.Model
{
    internal sealed class PowerMobCalculateSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EvaluateService _evaluateService = null;

        private readonly EcsFilter<PowerGameDesignCurrentComponent, IsMobComponent, IsHealthChangeEvent>
            _mobsChangeHealth = null;

        private readonly EcsFilter<PowerGameDesignCurrentComponent, IsMobComponent, IsDestroyEntityRequest> _mobsDied =
            null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _mobsChangeHealth)
            {
                ref var mob = ref _mobsChangeHealth.GetEntity(i);
                ref var powerGameDesignCurrent = ref _mobsChangeHealth.Get1(i);
                SetPower(mob, ref powerGameDesignCurrent);
            }

            foreach (var i in _mobsDied)
            {
                ref var mob = ref _mobsDied.GetEntity(i);
                ref var powerGameDesignCurrent = ref _mobsDied.Get1(i);
                SetPower(mob, ref powerGameDesignCurrent);
            }
        }

        private void SetPower(in EcsEntity mob,
            ref PowerGameDesignCurrentComponent powerGameDesignCurrent)
        {
            var power = _evaluateService.EvaluateGameDesignPower(mob);
            powerGameDesignCurrent.Power = power;
        }
    }
}