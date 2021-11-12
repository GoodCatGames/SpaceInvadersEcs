using Leopotam.Ecs;
using Model.AppData;
using Model.Components.Body;
using Model.Components.Body.Gun;
using Model.Components.Body.UI;
using Model.Components.Requests;
using Model.Extensions.EntityFactories;
using SpaceInvadersLeoEcs.Extensions.Components;
using SpaceInvadersLeoEcs.Extensions.EntityToGameObject;
using SpaceInvadersLeoEcs.View.Systems;
using UnityEngine;

namespace SpaceInvadersLeoEcs.MappingUnityToModel.Systems
{
    internal sealed class PlayerInitSystem : IEcsInitSystem
    {
        // auto-injected fields.
        private readonly EcsWorld _world = null;
        private readonly SceneData _sceneData = null;
        private readonly AppConfiguration _appConfiguration = null;

        void IEcsInitSystem.Init()
        {
            if (_sceneData.Player1 != null) InitPlayer(_sceneData.Player1, 1, _appConfiguration.Player1Gun);
            if (_sceneData.Player2 != null) InitPlayer(_sceneData.Player2, 2, _appConfiguration.Player2Gun);
        }

        private void InitPlayer(Transform playerTransform, in int numberPlayer, IEntityFactory gunEntityFactoryFromSo)
        {
            var player = CreatePlayer(playerTransform, numberPlayer);
            SetStartGun(player, playerTransform, gunEntityFactoryFromSo);
            CreateGunIndicator(player);
        }

        private EcsEntity CreatePlayer(Transform playerTransform, in int numberPlayer)
        {
            if (playerTransform == null) return default;
            var rigidBody2D = playerTransform.GetComponent<Rigidbody2D>();
            
            var entity = _world.NewEntity();
            entity.Get<ViewObjectComponent>().ViewObject = new ViewObjectUnity(playerTransform, rigidBody2D);
            entity.Get<UnityComponent<LineRenderer>>().Value = playerTransform.GetComponent<LineRenderer>();
            entity.Get<Player>().Number = numberPlayer;
            entity.Get<Move>();
            entity.Get<Health>() = new Health { Initial = 1, Current = 1 };
            
            playerTransform.GetProvider().SetEntity(entity);
            return entity;
        }

        private void SetStartGun(in EcsEntity player, Transform playerTransform, IEntityFactory gunEntityFactoryFromSo)
        {
            var gun = gunEntityFactoryFromSo.CreateEntity(_world);
            gun.Get<PlayerOwner>().PlayerEntity = player;
            gun.Get<ShootIsPossible>();
            gun.Get<UnityComponent<GunAudioUnityComponent>>().Value = playerTransform.GetComponent<GunAudioUnityComponent>();
        }

        private void CreateGunIndicator(in EcsEntity player)
        {
            var entity = _world.NewEntity();
            entity.Get<GunIndicator>();
            entity.Get<ViewCreateRequest>();
            entity.Get<PlayerOwner>().PlayerEntity = player;
        }
    }
}