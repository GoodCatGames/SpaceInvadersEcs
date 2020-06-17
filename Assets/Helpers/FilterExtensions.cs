using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Components.Body.Gun;
using SpaceInvadersLeoEcs.Components.Body.Player;
using SpaceInvadersLeoEcs.Components.Body.UI;
using SpaceInvadersLeoEcs.Extensions.Components;
using SpaceInvadersLeoEcs.Extensions.Enitities;
using UnityEngine.UI;

namespace SpaceInvadersLeoEcs.Helpers
{
    internal static class FilterExtensions
    {
        public static EcsEntity GetGunOfPlayer(this EcsFilter<IsCanShootComponent, OwnerComponent> guns,
            int playerNumber)
        {
            foreach (var i in guns)
            {
                var ownerComponent = guns.Get2(i);
                var ownerPlayer = ownerComponent.Entity;
                if (ownerPlayer.TryGet<PlayerComponent>(out var gunPlayerComponent) && gunPlayerComponent.Number == playerNumber)
                {
                    return guns.GetEntity(i);
                }
            }
            return EcsEntity.Null;
        }
        
        public static Text GetIndicator(this 
            EcsFilter<WrapperUnityObject<Text>, OwnerComponent, IsGunIndicatorComponent> indicators, int numberPlayer)
        {
            foreach (var i in indicators)
            {
                var ownerComponent = indicators.Get2(i);
                if(ownerComponent.Entity.Get<PlayerComponent>().Number != numberPlayer) continue;
                var text = indicators.Get1(i);
                return text.Value;
            }
            return null;
        }
        
    }
}