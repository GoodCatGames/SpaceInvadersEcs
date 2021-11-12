using Leopotam.Ecs;
using Model.Components.Body;
using Model.Components.Body.Mob;

namespace Model.Services
{
    public class EvaluateService
    {
        public float EvaluateGameDesignPower(in EcsEntity entity)
        {
            var speed = entity.Has<Move>() ? entity.Get<Move>().Speed : 0f;
            var healthCurrent = entity.Has<Health>() ? entity.Get<Health>().Current : 0f;
            var resistance = entity.Has<Move>() ? entity.Get<BulletResistance>().Value : 0f;

            var power = speed + healthCurrent + resistance;
            return power;
        }
    }
}