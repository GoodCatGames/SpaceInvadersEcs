// GENERATED AUTOMATICALLY FROM 'Assets/InputControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace SpaceInvadersLeoEcs
{
    public class @InputControls : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @InputControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputControls"",
    ""maps"": [
        {
            ""name"": ""Player1"",
            ""id"": ""140c6142-8630-4e35-b6e3-7815d702892e"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""9e630cbe-f78f-4728-a97b-8505d7c0f1f9"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""9c940e22-d42e-49b9-89f3-39a757be7a74"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""b1e64280-a409-467f-8f8a-5d2fbc5e9ea3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Arrows"",
                    ""id"": ""75134a5c-5786-493b-9a0d-c31721fc7b5e"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""0e87ba94-29c7-42af-ae39-72a818078fc6"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""99558b19-d0ea-4a93-9b90-b4a37995328d"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e2e44561-dc78-46b8-9e12-19c3c655cc27"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5e6565b7-5c90-42c1-8000-83b17cc4c444"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Player2"",
            ""id"": ""2170c69b-00b8-4972-9ac0-dd0b752e8c53"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""21dae765-e3b3-4c9d-afea-18377de53665"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""9d2daf2e-05f1-45df-82ef-f6b423fa6586"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""dcc129e6-a7c8-487b-9d1c-972ac0534327"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Arrows"",
                    ""id"": ""e75af9e1-6a3e-4464-85e8-e2d5268ab23f"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""e93f7e66-ef8b-4b7a-b99e-f2b43268dc33"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""843ee56b-1d6a-47f8-a82a-79bbc4e0c89f"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""6a75ea75-9a72-4523-9b58-148c9bce71e2"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c8884d82-f097-4481-8b19-9c7a64a74612"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Common"",
            ""id"": ""a1788ef3-a672-4387-889a-86c267db9b8a"",
            ""actions"": [
                {
                    ""name"": ""AnyKey"",
                    ""type"": ""Button"",
                    ""id"": ""1d14ed2a-21d1-4483-9ec0-79d03c174df9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PauseQuit"",
                    ""type"": ""Button"",
                    ""id"": ""4360035c-163a-4357-92f3-21ca3d592804"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""261c8200-4f18-47ae-9dc7-4432c05a6a91"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""AnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b5dd8f1f-3ed3-4f5f-9f97-1b3f318b9af3"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""PauseQuit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // Player1
            m_Player1 = asset.FindActionMap("Player1", throwIfNotFound: true);
            m_Player1_Move = m_Player1.FindAction("Move", throwIfNotFound: true);
            m_Player1_Shoot = m_Player1.FindAction("Shoot", throwIfNotFound: true);
            m_Player1_Reload = m_Player1.FindAction("Reload", throwIfNotFound: true);
            // Player2
            m_Player2 = asset.FindActionMap("Player2", throwIfNotFound: true);
            m_Player2_Move = m_Player2.FindAction("Move", throwIfNotFound: true);
            m_Player2_Shoot = m_Player2.FindAction("Shoot", throwIfNotFound: true);
            m_Player2_Reload = m_Player2.FindAction("Reload", throwIfNotFound: true);
            // Common
            m_Common = asset.FindActionMap("Common", throwIfNotFound: true);
            m_Common_AnyKey = m_Common.FindAction("AnyKey", throwIfNotFound: true);
            m_Common_PauseQuit = m_Common.FindAction("PauseQuit", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        // Player1
        private readonly InputActionMap m_Player1;
        private IPlayer1Actions m_Player1ActionsCallbackInterface;
        private readonly InputAction m_Player1_Move;
        private readonly InputAction m_Player1_Shoot;
        private readonly InputAction m_Player1_Reload;
        public struct Player1Actions
        {
            private readonly @InputControls m_Wrapper;
            public Player1Actions(@InputControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_Player1_Move;
            public InputAction @Shoot => m_Wrapper.m_Player1_Shoot;
            public InputAction @Reload => m_Wrapper.m_Player1_Reload;
            public InputActionMap Get() { return m_Wrapper.m_Player1; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(Player1Actions set) { return set.Get(); }
            public void SetCallbacks(IPlayer1Actions instance)
            {
                if (m_Wrapper.m_Player1ActionsCallbackInterface != null)
                {
                    @Move.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnMove;
                    @Shoot.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnShoot;
                    @Shoot.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnShoot;
                    @Shoot.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnShoot;
                    @Reload.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnReload;
                    @Reload.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnReload;
                    @Reload.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnReload;
                }
                m_Wrapper.m_Player1ActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @Shoot.started += instance.OnShoot;
                    @Shoot.performed += instance.OnShoot;
                    @Shoot.canceled += instance.OnShoot;
                    @Reload.started += instance.OnReload;
                    @Reload.performed += instance.OnReload;
                    @Reload.canceled += instance.OnReload;
                }
            }
        }
        public Player1Actions @Player1 => new Player1Actions(this);

        // Player2
        private readonly InputActionMap m_Player2;
        private IPlayer2Actions m_Player2ActionsCallbackInterface;
        private readonly InputAction m_Player2_Move;
        private readonly InputAction m_Player2_Shoot;
        private readonly InputAction m_Player2_Reload;
        public struct Player2Actions
        {
            private readonly @InputControls m_Wrapper;
            public Player2Actions(@InputControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_Player2_Move;
            public InputAction @Shoot => m_Wrapper.m_Player2_Shoot;
            public InputAction @Reload => m_Wrapper.m_Player2_Reload;
            public InputActionMap Get() { return m_Wrapper.m_Player2; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(Player2Actions set) { return set.Get(); }
            public void SetCallbacks(IPlayer2Actions instance)
            {
                if (m_Wrapper.m_Player2ActionsCallbackInterface != null)
                {
                    @Move.started -= m_Wrapper.m_Player2ActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_Player2ActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_Player2ActionsCallbackInterface.OnMove;
                    @Shoot.started -= m_Wrapper.m_Player2ActionsCallbackInterface.OnShoot;
                    @Shoot.performed -= m_Wrapper.m_Player2ActionsCallbackInterface.OnShoot;
                    @Shoot.canceled -= m_Wrapper.m_Player2ActionsCallbackInterface.OnShoot;
                    @Reload.started -= m_Wrapper.m_Player2ActionsCallbackInterface.OnReload;
                    @Reload.performed -= m_Wrapper.m_Player2ActionsCallbackInterface.OnReload;
                    @Reload.canceled -= m_Wrapper.m_Player2ActionsCallbackInterface.OnReload;
                }
                m_Wrapper.m_Player2ActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @Shoot.started += instance.OnShoot;
                    @Shoot.performed += instance.OnShoot;
                    @Shoot.canceled += instance.OnShoot;
                    @Reload.started += instance.OnReload;
                    @Reload.performed += instance.OnReload;
                    @Reload.canceled += instance.OnReload;
                }
            }
        }
        public Player2Actions @Player2 => new Player2Actions(this);

        // Common
        private readonly InputActionMap m_Common;
        private ICommonActions m_CommonActionsCallbackInterface;
        private readonly InputAction m_Common_AnyKey;
        private readonly InputAction m_Common_PauseQuit;
        public struct CommonActions
        {
            private readonly @InputControls m_Wrapper;
            public CommonActions(@InputControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @AnyKey => m_Wrapper.m_Common_AnyKey;
            public InputAction @PauseQuit => m_Wrapper.m_Common_PauseQuit;
            public InputActionMap Get() { return m_Wrapper.m_Common; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(CommonActions set) { return set.Get(); }
            public void SetCallbacks(ICommonActions instance)
            {
                if (m_Wrapper.m_CommonActionsCallbackInterface != null)
                {
                    @AnyKey.started -= m_Wrapper.m_CommonActionsCallbackInterface.OnAnyKey;
                    @AnyKey.performed -= m_Wrapper.m_CommonActionsCallbackInterface.OnAnyKey;
                    @AnyKey.canceled -= m_Wrapper.m_CommonActionsCallbackInterface.OnAnyKey;
                    @PauseQuit.started -= m_Wrapper.m_CommonActionsCallbackInterface.OnPauseQuit;
                    @PauseQuit.performed -= m_Wrapper.m_CommonActionsCallbackInterface.OnPauseQuit;
                    @PauseQuit.canceled -= m_Wrapper.m_CommonActionsCallbackInterface.OnPauseQuit;
                }
                m_Wrapper.m_CommonActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @AnyKey.started += instance.OnAnyKey;
                    @AnyKey.performed += instance.OnAnyKey;
                    @AnyKey.canceled += instance.OnAnyKey;
                    @PauseQuit.started += instance.OnPauseQuit;
                    @PauseQuit.performed += instance.OnPauseQuit;
                    @PauseQuit.canceled += instance.OnPauseQuit;
                }
            }
        }
        public CommonActions @Common => new CommonActions(this);
        private int m_KeyboardSchemeIndex = -1;
        public InputControlScheme KeyboardScheme
        {
            get
            {
                if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
                return asset.controlSchemes[m_KeyboardSchemeIndex];
            }
        }
        public interface IPlayer1Actions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnShoot(InputAction.CallbackContext context);
            void OnReload(InputAction.CallbackContext context);
        }
        public interface IPlayer2Actions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnShoot(InputAction.CallbackContext context);
            void OnReload(InputAction.CallbackContext context);
        }
        public interface ICommonActions
        {
            void OnAnyKey(InputAction.CallbackContext context);
            void OnPauseQuit(InputAction.CallbackContext context);
        }
    }
}
