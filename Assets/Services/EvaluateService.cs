using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Components.Body.Mob;
using SpaceInvadersLeoEcs.Extensions.Enitities;

namespace SpaceInvadersLeoEcs.Services
{
    public class EvaluateService
    {
        public float EvaluateGameDesignPower(EcsEntity entity)
        {
            entity.TryGet<MoveComponent>(out var moveComponent);
            entity.TryGet<HealthCurrent>(out var healthCurrent);
            entity.TryGet<BulletResistance>(out var resistance);
            
            var power = moveComponent.Speed + healthCurrent.Value + resistance.Value;
            return power;
        }
    }
}