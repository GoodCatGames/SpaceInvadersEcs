using Leopotam.Ecs;
using Model.AppData;
using Model.Components.Body.Timers;
using Model.Components.Events;
using Model.Components.Events.InputEvents;
using Model.Components.Events.UnityEvents;
using Model.Components.Requests;
using Model.Extensions.Components;
using Model.Extensions.Timers;
using Model.Services;
using Model.Systems;
using Model.Systems.Entities;
using Model.Systems.Input;
using Model.Systems.Mobs;
using Model.Systems.Move;
using Model.Systems.Weapon;
using SpaceInvadersLeoEcs.Controller;
using SpaceInvadersLeoEcs.MappingUnityToModel;
using SpaceInvadersLeoEcs.MappingUnityToModel.Systems;
using SpaceInvadersLeoEcs.View.Services;
using SpaceInvadersLeoEcs.View.Systems;
using SpaceInvadersLeoEcs.View.Systems.Create;
using SpaceInvadersLeoEcs.View.Systems.Update;
using UnityEngine;

namespace SpaceInvadersLeoEcs
{
    internal sealed class GameStartup : MonoBehaviour
    {
        [SerializeField] private AppConfiguration appConfiguration;
        [SerializeField] private GameConfigurationSo gameConfigurationSo = null;

        private EcsWorld _world;
        private EcsSystems _systems;

        private void Start()
        {
            var gameContext = new GameContext();
            CalculateStartPowerMobs(gameContext, gameConfigurationSo);

            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif

            _systems

                //// Initialization
                .Add(new GameFieldBordersInitSystem())
                .Add(new PlayerInitSystem())
                .Add(new GameStartStateInitSystem())

                //// Controller (UiEvents, InputEvents)  
                .Add(new InputInitSystem())

                //// Model
                // Input
                .Add(new GameStateInputSystem())
                .Add(new MoveInputSystem())
                .Add(new ShootInputSystem())

                // Create
                .Add(new ScoreCreateSystem())

                // Shoot
                .Add(new ShootDeniedTimeBetweenShotsSystem())
                .Add(new ShootDeniedNoAmmoSystem())
                .Add(new ShootDeniedReloadInProcessSystem())
                .Add(new ShootExecuteSystem())

                // After Shoot
                .Add(new AmmoUsedSystem())
                .Add(new GunTimerBetweenShotsStartSystem())
                .Add(new GunReloadStartSystem())

                // Timers
                .Add(new TimerSystem<TimerGunReload>())
                .Add(new TimerSystem<TimerBetweenShots>())
                
                .Add(new GunReloadExecutedSystem())
                .Add(new MoveExecuteSystem())
                
                .Add(new PlayerMoveOutBorderSystem())
                .Add(new UnityEventOnBecameInvisibleSystem())
                .Add(new BulletCollisionSystem())
                .Add(new PlayerTakeDamageSystem())
                .Add(new HealthTakeDamageSystem())
                .Add(new EntityWithZeroHealthDieSystem())

                // Scenarist creates mobs
                .Add(new MobPowerCalculateSystem())
                .Add(new ScenaristSystem())
                .Add(new MobSpawnSystem())
                
                .Add(new ScoreCalculateSystem())
                .Add(new EntityDestroyChildrenDestroyedPlayer())
                .Add(new EntityDestroySystem())
                .Add(new GameOverSystem())
                .Add(new GameStateViewUpdateSystem())

                //// Viewer
                // Bind (GameObjects from scene to Entities)
                .Add(new ScoreViewBindSystem())

                // Create
                .Add(new GunIndicatorViewCreateSystem())
                .Add(new BulletViewCreateSystem())
                .Add(new MobViewCreateSystem())

                // Update
                .Add(new MobViewUpdateSystem())
                .Add(new GunLaserRayViewUpdateSystem())
                .Add(new GunIndicatorViewUpdateSystem())
                .Add(new ScoreViewUpdateSystem())
                .OneFrame<ViewUpdateRequest>()
                .Add(new GunAudioSystem())

                // register one-frame components
                .OneFrame<InputAnyKeyEvent>()
                .OneFrame<InputPauseQuitEvent>()
                .OneFrame<InputMoveStartedEvent>()
                .OneFrame<InputMoveCanceledEvent>()
                .OneFrame<InputShootStartedEvent>()
                .OneFrame<InputShootCanceledEvent>()
                .OneFrame<InputGunReloadEvent>()
                .OneFrame<PositionChangeEvent>()
                .OneFrame<ShotMadeEvent>()
                .OneFrame<GunReloadStartEvent>()
                .OneFrame<GunReloadEndEvent>()
                .OneFrame<DamageRequest>()
                .OneFrame<HealthChangeEvent>()
                .OneFrame<MobsCreateRequest>()
                .OneFrame<EntityDestroyRequest>()
                .OneFrame<ContainerComponents<OnBecameInvisibleEvent>>()
                .OneFrame<OnBecameInvisibleEvent>()
                .OneFrame<ContainerComponents<OnCollisionEnter2DEvent>>()
                .OneFrame<OnCollisionEnter2DEvent>()

                // inject 
                .Inject(appConfiguration)
                .Inject(gameConfigurationSo.GameConfiguration)
                .Inject(GetComponent<SceneData>())
                .Inject(gameContext)
                .Inject(new PoolsObject())
                .Inject(new EvaluateService())
                .Init();
        }

        private void CalculateStartPowerMobs(GameContext gameContext, GameConfigurationSo gameConfig)
        {
            var worldCalculateStartPowerMobs = new EcsWorld();
            var systemsForCalculatePowerMobs = new EcsSystems(worldCalculateStartPowerMobs);
            systemsForCalculatePowerMobs
                .Add(new MobEntityFactoriesLoadSystem())
                .Add(new MobPowerBaseCalculateSystem())
                .Add(new MobPowerSaveSystem())
                .Inject(appConfiguration)
                .Inject(gameContext)
                .Inject(gameConfig)
                .Inject(new EvaluateService())
                .Init();

            systemsForCalculatePowerMobs.Run();
            systemsForCalculatePowerMobs.Destroy();
            worldCalculateStartPowerMobs.Destroy();
        }

        private void Update() => _systems?.Run();

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