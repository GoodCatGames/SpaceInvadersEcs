using Leopotam.Ecs;
using Model.Components.Body;
using Model.Components.Body.Mob;
using Model.Components.Events;
using Model.Components.Requests;
using UnityEngine;

namespace Model.Systems
{
    public sealed class ScoreCalculateSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<PowerGameDesign, Mob, EntityDestroyRequest>
            _filterDeathMobs = null;

        private readonly EcsFilter<Score> _filterScore = null;

        void IEcsRunSystem.Run()
        {
            if (_filterDeathMobs.IsEmpty() == false)
            {
                var sumPower = GetPowerDiedMobs();
                ref var entity = ref _filterScore.GetEntity(0);
                ref var score = ref _filterScore.Get1(0);
                score.Value += Mathf.RoundToInt(sumPower);
                entity.Get<ViewUpdateRequest>();
            }
        }

        private float GetPowerDiedMobs()
        {
            float sum = 0;
            foreach (var i in _filterDeathMobs)
            {
                sum += _filterDeathMobs.Get1(i).Initial;
            }

            return sum;
        }
    }
}