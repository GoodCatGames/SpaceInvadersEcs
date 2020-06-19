using Leopotam.Ecs;
using LeopotamGroup.Globals;
using SpaceInvadersLeoEcs.AppData;
using SpaceInvadersLeoEcs.Components.Events;
using SpaceInvadersLeoEcs.Components.Events.InputEvents;
using SpaceInvadersLeoEcs.Components.Events.UnityEvents;
using SpaceInvadersLeoEcs.Components.Requests;
using SpaceInvadersLeoEcs.Extensions.Components;
using SpaceInvadersLeoEcs.Extensions.Systems.Transform;
using SpaceInvadersLeoEcs.Extensions.Systems.ViewCreate;
using SpaceInvadersLeoEcs.Services;
using SpaceInvadersLeoEcs.Systems;
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
    sealed class GameStartup : MonoBehaviour
    {
        public GameConfiguration GameConfiguration = null;

        EcsWorld _world;
        EcsSystems _systems;

        void Start()
        {
            var gameContext = new GameContext();
            CalculateStartPowerMobs(gameContext, GameConfiguration);

            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif
            
            _systems

                // Controller (UiEvents, InputEvents, Init GameObjects on Scene(Create Entities)) 
                .Add(new GameInitSystem())
                .Add(new GameFieldBordersInitSystem())
                .Add(new GameManagerInitSystem())
                .Add(Service<Emitter>.Get(true))
                .Add(new PlayerInitSystem())
                .Add(new InputSystem())
                .Add(new GameStateInputSystem())

                // Model 
                .Add(new GameManagerInitSystem())
                .Add(new MoveInputSystem())
                
                .Add(new ShootInputSystem())
                .Add(new ShootDeniedTimeBetweenShotsSystem())
                .Add(new ShootDeniedNoAmmoSystem())
                .Add(new ShootDeniedReloadInProcessSystem())
                .Add(new ShootExecuteSystem())
                
                .Add(new AmmoUsedSystem())
                .Add(new GunTimerBetweenShotsStart())
                .Add(new GunReloadStartSystem())
                
                .Add(new TimerTickSystem())
                .Add(new GunReloadExecutedSystem())
                
                .Add(new MoveSystem())
                .Add(new BulletCollisionSystem())
                .Add(new PlayerTakeDamageSystem())
                .Add(new HealthTakeDamageSystem())
                .Add(new EntityDeathSystem())
                
                .Add(new PowerMobCalculateSystem())
                .Add(new ScenaristSystem())
                .Add(new MobSpawnSystem())
                
                .Add(new ScoreSystem())
                .Add(new EntityDestroyChildrenDestroyedPlayer())
                .Add(new DestroyEntitySystem())
                .Add(new GameOverSystem())
                .Add(new GameStateChangeSystem())

                // Viewer
                .Add(new PlayerMoveBorderSystem())
                .Add(new UnityEventOnBecameInvisibleSystem())
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
                .Inject(GameConfiguration)
                .Inject(GetComponent<SceneData>())
                .Inject(gameContext)
                .Inject(new PoolsObject())
                .Inject(GetComponent<AudioService>())
                .Inject(new EvaluateService())
                .Init();
        }

        private void CalculateStartPowerMobs(GameContext gameContext, GameConfiguration gameConfiguration)
        {
            var worldCalculateStartPowerMobs = new EcsWorld();
            var systemsBlueprints = new EcsSystems(worldCalculateStartPowerMobs);
            systemsBlueprints
                .Add(new BlueprintLoadSystem())
                .Add(new PowerMobCalculateSystem())
                .Add(new MobPowerSaveSystem())
                .Inject(gameContext)
                .Inject(gameConfiguration)
                .Inject(new EvaluateService())
                .Init();

            systemsBlueprints.Run();
        }

        void Update()
        {
            _systems?.Run();
        }

        void OnDestroy()
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