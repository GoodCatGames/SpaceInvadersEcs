using Leopotam.Ecs;
using SpaceInvadersLeoEcs.AppData;
using SpaceInvadersLeoEcs.Components.Events.InputEvents;
using SpaceInvadersLeoEcs.Components.Requests;
using SpaceInvadersLeoEcs.Extensions;

namespace SpaceInvadersLeoEcs.Systems.Controller
{
    sealed class InputSystem : IEcsInitSystem, IEcsDestroySystem
    {
        // auto-injected fields.
        private readonly EcsWorld _world = null;
        private readonly GameContext _gameContext = null;
        
        private InputControls _inputControls;
        
        public void Init()
        {
            _inputControls = new InputControls();
            _inputControls.Enable();

            // Common
            _inputControls.Common.PauseQuit.performed += context => _world.SendMessage(new InputPauseQuitEvent());
            _inputControls.Common.AnyKey.performed += context => _world.SendMessage(new InputAnyKeyEvent());
            
            // Move
            _inputControls.Player1.Move.started += context => 
                SendMessageInGame(new InputMoveStartedEvent() {PlayerNumber = 1, Axis = context.ReadValue<float>()});

            _inputControls.Player2.Move.started += context => 
                SendMessageInGame(new InputMoveStartedEvent() {PlayerNumber = 2, Axis = context.ReadValue<float>()});
            
            _inputControls.Player1.Move.canceled += context => 
                SendMessageInGame(new InputMoveCanceledEvent() {PlayerNumber = 1});

            _inputControls.Player2.Move.canceled += context => 
                SendMessageInGame(new InputMoveCanceledEvent() {PlayerNumber = 2});
            
            // Shoot
            _inputControls.Player1.Shoot.started += context =>
                SendMessageInGame(new InputShootStartedEvent() {PlayerNumber = 1});

            _inputControls.Player2.Shoot.started += context => 
                SendMessageInGame(new InputShootStartedEvent() {PlayerNumber = 2});
            
            _inputControls.Player1.Shoot.canceled += context => 
                SendMessageInGame(new InputShootCanceledEvent() {PlayerNumber = 1});

            _inputControls.Player2.Shoot.canceled += context => 
                SendMessageInGame(new InputShootCanceledEvent() {PlayerNumber = 2});
            
            // Reload
            _inputControls.Player1.Reload.performed += context =>
                SendMessageInGame(new InputReloadGunEvent() {PlayerNumber = 1});
            
            _inputControls.Player2.Reload.performed += context =>
                SendMessageInGame(new InputReloadGunEvent() {PlayerNumber = 2});

        }

        private void SendMessageInGame<T>(T messageEvent)
            where T : struct
        {
            if(_gameContext.GameState != GameStates.Play) return;
            _world.SendMessage(messageEvent);
        }

        public void Destroy()
        {
            _inputControls.Disable();
        }
    }
    
}