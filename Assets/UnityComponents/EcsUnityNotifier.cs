using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Events.UnityEvents;
using SpaceInvadersLeoEcs.Extensions;
using SpaceInvadersLeoEcs.Extensions.UnityComponent;
using UnityEngine;

namespace SpaceInvadersLeoEcs.UnityComponents
{
    public class EcsUnityNotifier : EcsUnityNotifierBase
    {
        private void OnBecameInvisible()
        {
            if(!World.IsAlive()) return;
            Entity.AddEventToStack<OnBecameInvisibleEvent>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if(!World.IsAlive()) return;
            
            var otherTransform = other.transform;
            if (!otherTransform.HasProvider()) return;

            var otherEntity = otherTransform.GetProvider().Entity;
            if (!otherEntity.IsAlive()) return;
            Entity.AddEventToStack(new OnCollisionEnter2DEvent() {Other = otherEntity});
        }
    }
}