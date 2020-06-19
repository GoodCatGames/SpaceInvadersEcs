using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Components.Body.Gun;
using SpaceInvadersLeoEcs.Components.Body.Player;
using SpaceInvadersLeoEcs.Components.Body.UI;
using SpaceInvadersLeoEcs.Extensions.Components;
using UnityEngine.UI;

namespace SpaceInvadersLeoEcs.Helpers
{
    internal static class FilterExtensions
    {
        public static EcsEntity GetGunOfPlayer(this EcsFilter<IsCanShootComponent, OwnerPlayerComponent> guns,
            in int playerNumber)
        {
            foreach (var i in guns)
            {
                ref var ownerPlayerComponent = ref guns.Get2(i);
                var ownerPlayer = ownerPlayerComponent.PlayerEntity;
                ref var playerComponent = ref ownerPlayer.Get<PlayerComponent>();
                if (playerComponent.Number == playerNumber)
                {
                    return guns.GetEntity(i);
                }
            }

            return EcsEntity.Null;
        }

        public static Text GetIndicator(this
                EcsFilter<WrapperUnityObjectComponent<Text>, OwnerPlayerComponent, IsGunIndicatorComponent> indicators,
            in int numberPlayer)
        {
            foreach (var i in indicators)
            {
                ref var ownerPlayerComponent = ref indicators.Get2(i);
                ref var playerComponent = ref ownerPlayerComponent.PlayerEntity.Get<PlayerComponent>();
                if (playerComponent.Number != numberPlayer) continue;
                ref var text = ref indicators.Get1(i);
                return text.Value;
            }

            return null;
        }
    }
}