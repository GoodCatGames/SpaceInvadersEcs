using LeopotamGroup.Globals;
using SpaceInvadersLeoEcs.Components.Events.UnityEvents;
using SpaceInvadersLeoEcs.Extensions.Systems.Transform;
using UnityEngine;

namespace SpaceInvadersLeoEcs.UnityComponents
{
    public class EcsUnityNotifier : MonoBehaviour
    {
        private void OnBecameInvisible()
        {
            var onBecameInvisibleEvent = new OnBecameInvisibleEvent();
            transform.AddEcsEvent(onBecameInvisibleEvent);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var otherEntityExist = Service<Emitter>.Get().TryGetEntity(other.transform, out var entityOther);
            if(!otherEntityExist) return;
            var collisionEnter2DEvent = new OnCollisionEnter2DEvent() {Other = entityOther};
            transform.AddEcsEvent(collisionEnter2DEvent);
        }
    }
}