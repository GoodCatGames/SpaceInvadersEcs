using Leopotam.Ecs;
using Model.AppData;
using Model.Components.Body;
using Model.Components.Body.UI;
using Model.Components.Events;
using SpaceInvadersLeoEcs.Extensions.Components;
using SpaceInvadersLeoEcs.View.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceInvadersLeoEcs.View.Systems.Update
{
    internal sealed class GunLaserRayViewUpdateSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly GameContext _gameContext = null;

        private readonly EcsFilter<UnityComponent<LineRenderer>, PositionChangeEvent, Player> _filterLaserRays = null;
        private readonly EcsFilter<UnityComponent<Text>, PlayerOwner, GunIndicator> _filterIndicators = null;  
        
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filterLaserRays)
            {
                ref var unityComponent = ref _filterLaserRays.Get1(i);
                ref var changePositionEvent = ref _filterLaserRays.Get2(i);
                ref var playerComponent = ref _filterLaserRays.Get3(i);
                SetLaser(unityComponent.Value, changePositionEvent, out var positionIndicator);
                var indicator = _filterIndicators.GetIndicator(playerComponent.Number);
                SetIndicatorToPosition(indicator, positionIndicator);
            }
        }

        private void SetLaser(LineRenderer lineRenderer, in PositionChangeEvent positionChangeEvent, out Vector2 positionIndicator)
        {
            var startPositionLaser = positionChangeEvent.positionNew;
            var endPositionLaser = new Vector2(startPositionLaser.x, _gameContext.MaxBorderGameField.y);

            var layer = LayerMask.GetMask("Mobs");
            var raycastHit2D = Physics2D.Raycast(startPositionLaser, Vector2.up, Mathf.Infinity, layer);
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