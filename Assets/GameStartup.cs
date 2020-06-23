using Leopotam.Ecs;
using SpaceInvadersLeoEcs.AppData;
using SpaceInvadersLeoEcs.Components.Events;
using SpaceInvadersLeoEcs.Components.Events.InputEvents;
using SpaceInvadersLeoEcs.Components.Events.UnityEvents;
using SpaceInvadersLeoEcs.Components.Requests;
using SpaceInvadersLeoEcs.Extensions.Components;
using SpaceInvadersLeoEcs.Extensions.Systems.ViewCreate;
using SpaceInvadersLeoEcs.Services;
using SpaceInvadersLeoEcs.Systems.Blueprints;
using SpaceInvadersLeoEcs.Systems.Controller;
using SpaceInvadersLeoEcs.Systems.Controller.Init;
using SpaceInvadersLeoEcs.Systems.Model;
using SpaceInvadersLeoEcs.Systems.Model.Move;
using SpaceInvadersLeoEcs.Systems.Model.Weapon;
using SpaceInvadersLeoEcs.Systems.View;
using UnityEngine;

namespace SpaceInvadersLeoEcs
{
    internal sealed class GameStartup : MonoBehaviour
    {
        public GameConfiguration gameConfiguration = null;

        private EcsWorld _world;
        private EcsSystems _systems;

        private void Start()
        {
            var gameContext = new GameContext();
            CalculateStartPowerMobs(gameContext, gameConfiguration);

            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif
            
            _systems

                // Controller (UiEvents, InputEvents, Init GameObjects on Scene(Create Entities)) 
                .Add(new GameFieldBordersInitSystem())
                .Add(new ScoreInitSystem())
                .Add(new PlayerInitSystem())
                .Add(new InputInitSystem())
                
                // Model
                .Add(new GameStartStateInitSystem())
                
                .Add(new GameStateInputSystem())
                .Add(new MoveInputSystem())
                .Add(new ShootInputSystem())
                
                .Add(new ShootDeniedTimeBetweenShotsSystem())
                .Add(new ShootDeniedNoAmmoSystem())
                .Add(new ShootDeniedReloadInProcessSystem())
                .Add(new ShootExecuteSystem())
                
                .Add(new AmmoUsedSystem())
                .Add(new GunTimerBetweenShotsStartSystem())
                .Add(new GunReloadStartSystem())
                
                .Add(new TimerTickSystem())
                .Add(new GunReloadExecutedSystem())
                
                .Add(new MoveExecuteSystem())
                .Add(new PlayerMoveOutBorderSystem())
                .Add(new UnityEventOnBecameInvisibleSystem())
                
                .Add(new BulletCollisionSystem())
                .Add(new PlayerTakeDamageSystem())
                .Add(new HealthTakeDamageSystem())
                .Add(new EntityWithZeroHealthDieSystem())
                
                .Add(new PowerMobCalculateSystem())
                .Add(new ScenaristSystem())
                .Add(new MobSpawnSystem())
                
                .Add(new ScoreCalculateSystem())
                .Add(new EntityDestroyChildrenDestroyedPlayer())
                .Add(new EntityDestroySystem())
                .Add(new GameOverSystem())
                .Add(new GameStateChangeExecuteSystem())

                // Viewer
                .Add(new GunIndicatorViewCreateSystem())
                .Add(new BulletViewCreateSystem())
                .Add(new MobViewCreateSystem())
                
                .Add(new MobViewUpdateSystem())
                .Add(new LaserRayForGunUpdateSystem())
                .Add(new GunIndicatorViewUpdateSystem())

                // register one-frame components
                .OneFrame<InputAnyKeyEvent>()
                .OneFrame<InputPauseQuitEvent>()
                .OneFrame<InputMoveStartedEvent>()
                .OneFrame<InputMoveCanceledEvent>()
                .OneFrame<InputShootStartedEvent>()
                .OneFrame<InputShootCanceledEvent>()
                .OneFrame<InputReloadGunEvent>()
                .OneFrame<ChangePositionEvent>()
                .OneFrame<IsShotMakeRequest>()
                .OneFrame<IsShotMadeEvent>()
                .OneFrame<IsReloadStartEvent>()
                .OneFrame<IsReloadEndEvent>()
                .OneFrame<MakeDamageRequest>()
                .OneFrame<IsHealthChangeEvent>()
                .OneFrame<CreateMobsRequest>()
                .OneFrame<CreateViewRequest>()
                .OneFrame<IsDestroyEntityRequest>()
                .OneFrame<IsViewCreatedEvent>()
                .OneFrame<ContainerComponents<OnBecameInvisibleEvent>>()
                .OneFrame<OnBecameInvisibleEvent>()
                .OneFrame<ContainerComponents<OnCollisionEnter2DEvent>>()
                .OneFrame<OnCollisionEnter2DEvent>()

                // inject 
                .Inject(gameConfiguration)
                .Inject(GetComponent<SceneData>())
                .Inject(gameContext)
                .Inject(new PoolsObject())
                .Inject(GetComponent<AudioService>())
                .Inject(new EvaluateService())
                .Init();
        }

        private void CalculateStartPowerMobs(GameContext gameContext, GameConfiguration gameConfig)
        {
            var worldCalculateStartPowerMobs = new EcsWorld();
            var systemsBlueprints = new EcsSystems(worldCalculateStartPowerMobs);
            systemsBlueprints
                .Add(new BlueprintLoadSystem())
                .Add(new PowerBaseMobCalculateSystem())
                .Add(new MobPowerSaveSystem())
                .Inject(gameContext)
                .Inject(gameConfig)
                .Inject(new EvaluateService())
                .Init();

            systemsBlueprints.Run();
        }

        private void Update()
        {
            _systems?.Run();
        }

        private void OnDestroy()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
                _world.Destroy();
                _world = null;
            }
        }
    }
}