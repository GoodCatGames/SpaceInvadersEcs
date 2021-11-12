using Leopotam.Ecs;
using Model.Components.Body;
using Model.Components.Body.UI;
using SpaceInvadersLeoEcs.Extensions.Components;
using UnityEngine.UI;

namespace SpaceInvadersLeoEcs.View.Helpers
{
    internal static class FilterExtensions
    {
        public static Text GetIndicator(this
                EcsFilter<UnityComponent<Text>, PlayerOwner, GunIndicator> indicators,
            in int numberPlayer)
        {
            foreach (var i in indicators)
            {
                ref var ownerPlayerComponent = ref indicators.Get2(i);
                ref var playerComponent = ref ownerPlayerComponent.PlayerEntity.Get<Player>();
                if (playerComponent.Number != numberPlayer) continue;
                ref var text = ref indicators.Get1(i);
                return text.Value;
            }

            return null;
        }
    }
}