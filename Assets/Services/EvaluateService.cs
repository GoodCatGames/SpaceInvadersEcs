using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Components.Body.Mob;

namespace SpaceInvadersLeoEcs.Services
{
    public class EvaluateService
    {
        public float EvaluateGameDesignPower(EcsEntity entity)
        {
            var speed = entity.Has<MoveComponent>() ? entity.Get<MoveComponent>().Speed : 0f;
            var healthCurrent = entity.Has<HealthCurrentComponent>() ? entity.Get<HealthCurrentComponent>().Value : 0f;
            var resistance = entity.Has<MoveComponent>() ? entity.Get<BulletResistanceComponent>().Value : 0f;

            var power = speed + healthCurrent + resistance;
            return power;
        }
    }
}