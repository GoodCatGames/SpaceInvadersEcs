using Leopotam.Ecs;
using LeopotamGroup.Globals;
using SpaceInvadersLeoEcs.AppData;
using SpaceInvadersLeoEcs.Components.Events;
using SpaceInvadersLeoEcs.Components.Events.InputEvents;
using SpaceInvadersLeoEcs.Components.Events.UnityEvents;
using SpaceInvadersLeoEcs.Components.Requests;
using SpaceInvadersLeoEcs.Extensions.Components;
using SpaceInvadersLeoEcs.Extensions.Systems.CreateView;
using SpaceInvadersLeoEcs.Extensions.Systems.Transform;
using SpaceInvadersLeoEcs.Services;
using SpaceInvadersLeoEcs.Systems;
using SpaceInvadersLeoEcs.Systems.Blueprints;
using SpaceInvadersLeoEcs.Extensions;
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
        EcsWorld _world;
        EcsSystems _systems;

        void Start()
        {
            var emitter = new Emitter();
            Service<Emitter>.Set(emitter);
            
            PrepareBlueprints();

            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif
            _systems

                // Controller (UiEvents, InputEvents, Init GameObjects on Scene(Create Entities)) 
                .Add(new SetScreenBordersSystem())
                .Add(new InitGameManagerSystem())
                .Add(emitter)
                .Add(new PlayerInitSystem())
                .Add(new InputSystem())
                .Add(new InputGameStateSystem())
                
                // Model 
                .Add(new InitGameManagerSystem())
                
                .Add(new InputMoveSystem())
                .Add(new InputShootSystem())
                
                .Add(new DeniedShootTimeBetweenShootsSystem())
                .Add(new DeniedShootNoAmmoSystem())
                .Add(new DeniedShootReload())
                
                .Add(new BulletsShootSystem())

                .Add(new AmmoUsedSystem())
                
                .Add(new StartTimersAfterShoot())
                .Add(new StartReloadGunSystem())
                
                .Add(new TimerTickSystem())
                
                .Add(new ReloadGunExecutedSystem())
                
                .Add(new MoveSystem())
                .Add(new BulletCollisionSystem())
                .Add(new DamageToPlayerSystem())
                .Add(new DamageHealthSystem())
                .Add(new DeathSystem())
                
                .Add(new CalculatePowerSystem())
                .Add(new ScenaristSystem())
                .Add(new MobsSpawnSystem())

                .Add(new ScoreSystem())
                .Add(new DestroyChildrenDestroyedOwner())                
                .Add(new DestroyEntitySystem())

                .Add(new GameOverSystem())
                .Add(new GameStateChangeSystem())
                
                // Viewer
                .Add(new MoveBorderPlayerSystem())
                .Add(new OnBecameInvisibleSystem())
                
                .Add(new CreateGunIndicatorViewSystem())
                .Add(new CreateBulletViewSystem())
                .Add(new CreateMobsViewSystem())
                
                .Add(new ChangeMobsViewSystem())
                
                .Add(new LaserRayForGunSystem())
                
                .Add(new IndicatorAmmoViewSystem())

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
                .OneFrame<HealthChangeEvent>()
                
                .OneFrame<CreateMobsRequest>()
                
                .OneFrame<CreateViewRequest>()
                .OneFrame<DestroyEntityRequest>()
                
                .OneFrame<IsViewCreatedEvent>()
                
                .OneFrame<ContainerComponents<OnBecameInvisibleEvent>>()
                .OneFrame<OnBecameInvisibleEvent>()
                .OneFrame<ContainerComponents<OnCollisionEnter2DEvent>>()
                .OneFrame<OnCollisionEnter2DEvent>()
               
                // inject 
                .Inject(new PoolsObject())
                .Inject(GetComponent<GameContext>())
                .Inject(GetComponent<AudioService>())
                .Inject(new EvaluateService())
                .Init();
            
            _world.SendMessage(new ChangeGameStateRequest() {State = GameStates.Pause});
        }

        private void PrepareBlueprints()
        {
            var worldBlueprints = new EcsWorld();
            var systemsBlueprints = new EcsSystems(worldBlueprints);
            systemsBlueprints
                .Add(new LoadBlueprintsSystem())
                .Add(new CalculatePowerSystem())
                .Add(new SaveMobPowersSystem())
                .Inject(GetComponent<GameContext>())
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