using Leopotam.Ecs;
using SpaceInvadersLeoEcs.AppData;
using SpaceInvadersLeoEcs.Blueprints;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Components.Body.Gun;
using SpaceInvadersLeoEcs.Components.Body.Player;
using SpaceInvadersLeoEcs.Components.Body.UI;
using SpaceInvadersLeoEcs.Components.Body.WrappersMonoBehaviour;
using SpaceInvadersLeoEcs.Components.Requests;
using SpaceInvadersLeoEcs.Extensions.Components;
using SpaceInvadersLeoEcs.Extensions.UnityComponent;
using SpaceInvadersLeoEcs.Systems.Model.Data;
using SpaceInvadersLeoEcs.UnityComponents;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Systems.Controller.Init
{
    internal sealed class PlayerInitSystem : IEcsInitSystem
    {
        // auto-injected fields.
        private readonly EcsWorld _world = null;
        private readonly SceneData _sceneData = null;
        private readonly GameConfiguration _gameConfiguration = null;

        void IEcsInitSystem.Init()
        {
            if (_sceneData.Player1 != null) InitPlayer(_sceneData.Player1, 1, _gameConfiguration.Player1Gun);
            if (_sceneData.Player2 != null) InitPlayer(_sceneData.Player2, 2, _gameConfiguration.Player2Gun);
        }

        private void InitPlayer(Transform playerTransform, in int numberPlayer, GunBlueprint gunBlueprint)
        {
            var player = CreatePlayer(playerTransform, numberPlayer);
            SetStartGun(player, playerTransform, gunBlueprint);
            CreateGunIndicator(player);
        }

        private EcsEntity CreatePlayer(Transform playerTransform, in int numberPlayer)
        {
            if (playerTransform == null) return default;
            var rigidBody2D = playerTransform.GetComponent<Rigidbody2D>();
            
            var entity = _world.NewEntity();
            entity.Get<ViewObjectComponent>().ViewObject = new ViewObjectUnity(playerTransform, rigidBody2D);
            entity.Get<WrapperUnityObjectComponent<LineRenderer>>().Value = playerTransform.GetComponent<LineRenderer>();
            entity.Get<PlayerComponent>().Number = numberPlayer;
            entity.Get<MoveComponent>();
            entity.Get<HealthBaseComponent>().Value = 1;
            entity.Get<HealthCurrentComponent>().Value = 1;
            
            playerTransform.GetProvider().SetEntity(entity);
            
            return entity;
        }

        private void SetStartGun(in EcsEntity player, Transform playerTransform, GunBlueprint gunBlueprint)
        {
            var gun = gunBlueprint.CreateEntity(_world);
            gun.Get<OwnerPlayerComponent>().PlayerEntity = player;
            gun.Get<IsCanShootComponent>();
            gun.Get<WrapperUnityObjectComponent<GunAudioUnityComponent>>().Value = playerTransform.GetComponent<GunAudioUnityComponent>();
        }

        private void CreateGunIndicator(in EcsEntity player)
        {
            var entity = _world.NewEntity();
            entity.Get<IsGunIndicatorComponent>();
            entity.Get<CreateViewRequest>();
            entity.Get<OwnerPlayerComponent>().PlayerEntity = player;
        }
    }
}