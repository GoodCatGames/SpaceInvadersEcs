using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body.Gun;
using SpaceInvadersLeoEcs.Components.Events;
using SpaceInvadersLeoEcs.Extensions.Components;
using SpaceInvadersLeoEcs.UnityComponents;

namespace SpaceInvadersLeoEcs.Systems.View
{
    internal sealed class GunAudioSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<WrapperUnityObjectComponent<GunAudioUnityComponent>, IsCanShootComponent, IsReloadStartEvent> _gunsStartReload = null;
        private readonly EcsFilter<WrapperUnityObjectComponent<GunAudioUnityComponent>, IsCanShootComponent, IsReloadEndEvent> _gunsEndReload = null;
        private readonly EcsFilter<WrapperUnityObjectComponent<GunAudioUnityComponent>, IsCanShootComponent, IsShotMadeEvent> _gunsMadeShot = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _gunsMadeShot)
            {
                var audioUnityComponent = _gunsMadeShot.Get1(i).Value;
                audioUnityComponent.PlayShoot();
            }

            foreach (var i in _gunsStartReload)
            {
                var audioUnityComponent = _gunsStartReload.Get1(i).Value;
                audioUnityComponent.StartPlayReload();
            }
            
            foreach (var i in _gunsEndReload)
            {
                var audioUnityComponent = _gunsEndReload.Get1(i).Value;
                audioUnityComponent.StopPlayReload();
            }
        }
    }
}