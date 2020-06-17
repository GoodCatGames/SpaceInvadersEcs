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
using SpaceInvadersLeoEcs.Systems.Model.Data;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Systems.Controller.Init
{
    sealed class PlayerInitSystem : IEcsInitSystem
    {
        // auto-injected fields.
        private readonly EcsWorld _world = null;
        private readonly GameContext _gameContext = null;

        public void Init()
        {
            if(_gameContext.Player1Config != null) InitPlayer(_gameContext.Player1Config.Transform, 1, _gameContext.Player1Config.GunPlayer);
            if(_gameContext.Player2Config != null) InitPlayer(_gameContext.Player2Config.Transform, 2, _gameContext.Player2Config.GunPlayer);
        }

        private void InitPlayer(Transform playerTransform, int numberPlayer, GunBlueprint gunBlueprint)
        {
            var player = CreatePlayer(playerTransform, numberPlayer);
            SetStartGun(player, gunBlueprint);
            CreateGunIndicator(player);
        }
        
        private EcsEntity CreatePlayer(Transform playerTransform, int numberPlayer)
        {
            if (playerTransform == null) return default;

                var entity = _world.NewEntity();

                ref var transformComponent = ref entity.Get<ViewObjectComponent>();
                var rigidBody2D = playerTransform.GetComponent<Rigidbody2D>();
                transformComponent.ViewObject = new ViewObjectUnity(playerTransform, rigidBody2D);

                var lineRenderer = playerTransform.GetComponent<LineRenderer>();
                entity.Replace(new WrapperUnityObject<LineRenderer>(){Value = lineRenderer});

                ref var playerComponent = ref entity.Get<PlayerComponent>();
                playerComponent.Number = numberPlayer;
                
                entity.Get<MoveComponent>();
                
                entity.Replace(new HealthBase() { Value = 1});
                entity.Replace(new HealthCurrent() { Value = 1});
                
                return entity;
        }
       
        private void SetStartGun(EcsEntity player, GunBlueprint gunBlueprint)
        {
            var gun = gunBlueprint.CreateEntity(_world);
            ref var ownerComponent = ref gun.Get<OwnerComponent>();
            ownerComponent.Entity = player;
            gun.Get<IsCanShootComponent>();
        }

        private void CreateGunIndicator(EcsEntity player)
        {
            var entity = _world.NewEntity();
            entity.Get<IsGunIndicatorComponent>();
            entity.Get<CreateViewRequest>();
            entity.Replace(new OwnerComponent() {Entity = player});
        }
    }
}