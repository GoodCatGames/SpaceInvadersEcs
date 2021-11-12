using Leopotam.Ecs;
using Model.Components.Body.Gun;
using Model.Components.Events;
using SpaceInvadersLeoEcs.Extensions.Components;
using SpaceInvadersLeoEcs.MappingUnityToModel;

namespace SpaceInvadersLeoEcs.View.Systems
{
    internal sealed class GunAudioSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<UnityComponent<GunAudioUnityComponent>, ShootIsPossible, GunReloadStartEvent> _gunsStartReload = null;
        private readonly EcsFilter<UnityComponent<GunAudioUnityComponent>, ShootIsPossible, GunReloadEndEvent> _gunsEndReload = null;
        private readonly EcsFilter<UnityComponent<GunAudioUnityComponent>, ShootIsPossible, ShotMadeEvent> _gunsMadeShot = null;

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