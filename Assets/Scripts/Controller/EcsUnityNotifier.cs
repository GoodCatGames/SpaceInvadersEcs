using Leopotam.Ecs;
using Model.Components.Events.UnityEvents;
using Model.Extensions;
using SpaceInvadersLeoEcs.Extensions.EntityToGameObject;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Controller
{
    public class EcsUnityNotifier : EcsUnityNotifierBase
    {
        private void OnBecameInvisible()
        {
            if(Entity.IsAlive() == false) 
                return;
            Entity.AddEventToStack<OnBecameInvisibleEvent>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if(Entity.IsAlive() == false) 
                return;
            
            var otherTransform = other.transform;
            if (otherTransform.HasProvider() == false) 
                return;

            var otherEntity = otherTransform.GetProvider().Entity;
            if (otherEntity.IsAlive() == false) 
                return;
            
            Entity.AddEventToStack(new OnCollisionEnter2DEvent() {Other = otherEntity});
        }
    }
}