using Leopotam.Ecs;
using SpaceInvadersLeoEcs.AppData;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Components.Body.Player;
using SpaceInvadersLeoEcs.Components.Body.UI;
using SpaceInvadersLeoEcs.Components.Events;
using SpaceInvadersLeoEcs.Extensions.Components;
using SpaceInvadersLeoEcs.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceInvadersLeoEcs.Systems.View
{
    internal sealed class LaserRayForGunUpdateSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly GameContext _gameContext = null;

        private readonly EcsFilter<WrapperUnityObjectComponent<LineRenderer>, ChangePositionEvent, PlayerComponent> _filterLaserRays = null;
        private readonly EcsFilter<WrapperUnityObjectComponent<Text>, OwnerPlayerComponent, IsGunIndicatorComponent> _filterIndicators = null;  
        
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filterLaserRays)
            {
                ref var wrapper = ref _filterLaserRays.Get1(i);
                ref var changePositionEvent = ref _filterLaserRays.Get2(i);
                ref var playerComponent = ref _filterLaserRays.Get3(i);
                SetLaser(wrapper.Value, changePositionEvent, out var positionIndicator);
                var indicator = _filterIndicators.GetIndicator(playerComponent.Number);
                SetIndicatorToPosition(indicator, positionIndicator);
            }
        }

        private void SetLaser(LineRenderer lineRenderer, in ChangePositionEvent changePositionEvent, out Vector2 positionIndicator)
        {
            var startPositionLaser = changePositionEvent.positionNew;
            var endPositionLaser = new Vector2(startPositionLaser.x, _gameContext.MaxBorderGameField.y);

            var layer = LayerMask.GetMask("Mobs");
            var raycastHit2D = Physics2D.Raycast(startPositionLaser, UnityEngine.Vector2.up, Mathf.Infinity, layer);
            if (raycastHit2D.collider != null)
            {
                endPositionLaser = raycastHit2D.point;
                positionIndicator = endPositionLaser;
            }
            else
            {
                positionIndicator = new Vector2(startPositionLaser.x, 0);
            }
            
            lineRenderer.SetPosition(0, startPositionLaser);
            lineRenderer.SetPosition(1, endPositionLaser);
        }
      
        private void SetIndicatorToPosition(Text indicator, Vector2 position) =>
            indicator.transform.position = position + Vector2.down * 0.5f;
    }
}