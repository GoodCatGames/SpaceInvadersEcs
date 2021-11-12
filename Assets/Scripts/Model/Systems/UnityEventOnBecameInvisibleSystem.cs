using Leopotam.Ecs;
using Model.Components.Body;
using Model.Components.Events.UnityEvents;
using Model.Components.Requests;
using Model.Extensions.Components;

namespace Model.Systems
{
    public class UnityEventOnBecameInvisibleSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ViewObjectComponent, ContainerComponents<OnBecameInvisibleEvent>> _filter = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                _filter.GetEntity(i).Get<EntityDestroyRequest>();
            }
        }
    }
}