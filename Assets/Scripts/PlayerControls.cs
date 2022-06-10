//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Scripts/PlayerControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""PC Input"",
            ""id"": ""af044b05-7646-40db-a158-d0da75faaca3"",
            ""actions"": [
                {
                    ""name"": ""Horizontal"",
                    ""type"": ""PassThrough"",
                    ""id"": ""6532f8ac-936e-41c4-8fe5-99518e5363d3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Vertical"",
                    ""type"": ""PassThrough"",
                    ""id"": ""13b9670f-fd25-4fae-ada0-6a8e86e48b9b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""PassThrough"",
                    ""id"": ""2da49222-d594-4305-9a8c-4f4c1fbc6628"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Guard"",
                    ""type"": ""PassThrough"",
                    ""id"": ""8a654975-e76c-4860-bf66-3094e190562b"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Blast"",
                    ""type"": ""PassThrough"",
                    ""id"": ""5240ae7c-7082-4bc7-923f-eb405b0f46cc"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""PassThrough"",
                    ""id"": ""4c656aaf-0a2a-4db3-be09-325837be244d"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Keyboard A & D"",
                    ""id"": ""bbf72c40-5cf1-4e01-a60c-37ae94ac27c5"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""d6db4e98-ad61-4640-a92b-353c3c179b51"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""da1b2162-c5a5-4dbc-a164-5db247c4a5bf"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard Left & Right"",
                    ""id"": ""9a551cba-edbb-4e3b-960e-01ce04b47974"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""4f93b932-7309-489d-8ea3-e1cd4ef1cd37"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""fe22b99f-d6f9-4579-b3eb-d235a4c39f01"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard W & S"",
                    ""id"": ""23760446-452a-4e86-ad61-1283d67bba3b"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""36ebab3e-edb2-496c-8c9f-da6adf746fc3"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""4871517f-71bf-4300-8390-cb4180f0cacd"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard Up & Down"",
                    ""id"": ""927d0fc9-b7bc-4571-9154-3beaf776ef73"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""f1e89497-cb75-4ba1-9ad6-8bb5fb7205c2"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""b6e09776-3f25-47cb-88b7-2fabf243f95c"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""206a5a68-c879-4d11-b205-bf8694d8ca0c"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b2cf8a79-7c63-4f78-b467-a940f36f3e03"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Guard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fea8ea18-e2b5-434b-960f-a7e52d014099"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Blast"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ddadd86c-c22e-4b0c-acb9-678eaf28f47c"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad input"",
            ""id"": ""4ee3292b-987b-4db8-ab38-870df77f693d"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""e028fb52-d15d-4d52-9aa7-45ae2413c4da"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""f8accf4c-0acc-48d4-ad90-ec7363b45e17"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Blast"",
                    ""type"": ""Button"",
                    ""id"": ""fe6e0724-08c0-4a46-a1c7-5901e7a0e038"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Guard"",
                    ""type"": ""Button"",
                    ""id"": ""4b3b98ba-f5bc-4d4b-b213-b2cbfe71cd92"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""336cde41-834f-4561-871c-560760bf0036"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""afbd72e4-a3c9-40af-a5c3-3e098e50ca4d"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2844b811-ac8a-4fd2-9c1d-c1554c36beca"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Blast"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aa3e06ab-0a42-4e16-b3b0-4f2cd7012dd1"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Guard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PC Input
        m_PCInput = asset.FindActionMap("PC Input", throwIfNotFound: true);
        m_PCInput_Horizontal = m_PCInput.FindAction("Horizontal", throwIfNotFound: true);
        m_PCInput_Vertical = m_PCInput.FindAction("Vertical", throwIfNotFound: true);
        m_PCInput_Shoot = m_PCInput.FindAction("Shoot", throwIfNotFound: true);
        m_PCInput_Guard = m_PCInput.FindAction("Guard", throwIfNotFound: true);
        m_PCInput_Blast = m_PCInput.FindAction("Blast", throwIfNotFound: true);
        m_PCInput_Pause = m_PCInput.FindAction("Pause", throwIfNotFound: true);
        // Gamepad input
        m_Gamepadinput = asset.FindActionMap("Gamepad input", throwIfNotFound: true);
        m_Gamepadinput_Move = m_Gamepadinput.FindAction("Move", throwIfNotFound: true);
        m_Gamepadinput_Shoot = m_Gamepadinput.FindAction("Shoot", throwIfNotFound: true);
        m_Gamepadinput_Blast = m_Gamepadinput.FindAction("Blast", throwIfNotFound: true);
        m_Gamepadinput_Guard = m_Gamepadinput.FindAction("Guard", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // PC Input
    private readonly InputActionMap m_PCInput;
    private IPCInputActions m_PCInputActionsCallbackInterface;
    private readonly InputAction m_PCInput_Horizontal;
    private readonly InputAction m_PCInput_Vertical;
    private readonly InputAction m_PCInput_Shoot;
    private readonly InputAction m_PCInput_Guard;
    private readonly InputAction m_PCInput_Blast;
    private readonly InputAction m_PCInput_Pause;
    public struct PCInputActions
    {
        private @PlayerControls m_Wrapper;
        public PCInputActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Horizontal => m_Wrapper.m_PCInput_Horizontal;
        public InputAction @Vertical => m_Wrapper.m_PCInput_Vertical;
        public InputAction @Shoot => m_Wrapper.m_PCInput_Shoot;
        public InputAction @Guard => m_Wrapper.m_PCInput_Guard;
        public InputAction @Blast => m_Wrapper.m_PCInput_Blast;
        public InputAction @Pause => m_Wrapper.m_PCInput_Pause;
        public InputActionMap Get() { return m_Wrapper.m_PCInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PCInputActions set) { return set.Get(); }
        public void SetCallbacks(IPCInputActions instance)
        {
            if (m_Wrapper.m_PCInputActionsCallbackInterface != null)
            {
                @Horizontal.started -= m_Wrapper.m_PCInputActionsCallbackInterface.OnHorizontal;
                @Horizontal.performed -= m_Wrapper.m_PCInputActionsCallbackInterface.OnHorizontal;
                @Horizontal.canceled -= m_Wrapper.m_PCInputActionsCallbackInterface.OnHorizontal;
                @Vertical.started -= m_Wrapper.m_PCInputActionsCallbackInterface.OnVertical;
                @Vertical.performed -= m_Wrapper.m_PCInputActionsCallbackInterface.OnVertical;
                @Vertical.canceled -= m_Wrapper.m_PCInputActionsCallbackInterface.OnVertical;
                @Shoot.started -= m_Wrapper.m_PCInputActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_PCInputActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_PCInputActionsCallbackInterface.OnShoot;
                @Guard.started -= m_Wrapper.m_PCInputActionsCallbackInterface.OnGuard;
                @Guard.performed -= m_Wrapper.m_PCInputActionsCallbackInterface.OnGuard;
                @Guard.canceled -= m_Wrapper.m_PCInputActionsCallbackInterface.OnGuard;
                @Blast.started -= m_Wrapper.m_PCInputActionsCallbackInterface.OnBlast;
                @Blast.performed -= m_Wrapper.m_PCInputActionsCallbackInterface.OnBlast;
                @Blast.canceled -= m_Wrapper.m_PCInputActionsCallbackInterface.OnBlast;
                @Pause.started -= m_Wrapper.m_PCInputActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PCInputActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PCInputActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_PCInputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Horizontal.started += instance.OnHorizontal;
                @Horizontal.performed += instance.OnHorizontal;
                @Horizontal.canceled += instance.OnHorizontal;
                @Vertical.started += instance.OnVertical;
                @Vertical.performed += instance.OnVertical;
                @Vertical.canceled += instance.OnVertical;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Guard.started += instance.OnGuard;
                @Guard.performed += instance.OnGuard;
                @Guard.canceled += instance.OnGuard;
                @Blast.started += instance.OnBlast;
                @Blast.performed += instance.OnBlast;
                @Blast.canceled += instance.OnBlast;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public PCInputActions @PCInput => new PCInputActions(this);

    // Gamepad input
    private readonly InputActionMap m_Gamepadinput;
    private IGamepadinputActions m_GamepadinputActionsCallbackInterface;
    private readonly InputAction m_Gamepadinput_Move;
    private readonly InputAction m_Gamepadinput_Shoot;
    private readonly InputAction m_Gamepadinput_Blast;
    private readonly InputAction m_Gamepadinput_Guard;
    public struct GamepadinputActions
    {
        private @PlayerControls m_Wrapper;
        public GamepadinputActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Gamepadinput_Move;
        public InputAction @Shoot => m_Wrapper.m_Gamepadinput_Shoot;
        public InputAction @Blast => m_Wrapper.m_Gamepadinput_Blast;
        public InputAction @Guard => m_Wrapper.m_Gamepadinput_Guard;
        public InputActionMap Get() { return m_Wrapper.m_Gamepadinput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GamepadinputActions set) { return set.Get(); }
        public void SetCallbacks(IGamepadinputActions instance)
        {
            if (m_Wrapper.m_GamepadinputActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_GamepadinputActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GamepadinputActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GamepadinputActionsCallbackInterface.OnMove;
                @Shoot.started -= m_Wrapper.m_GamepadinputActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_GamepadinputActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_GamepadinputActionsCallbackInterface.OnShoot;
                @Blast.started -= m_Wrapper.m_GamepadinputActionsCallbackInterface.OnBlast;
                @Blast.performed -= m_Wrapper.m_GamepadinputActionsCallbackInterface.OnBlast;
                @Blast.canceled -= m_Wrapper.m_GamepadinputActionsCallbackInterface.OnBlast;
                @Guard.started -= m_Wrapper.m_GamepadinputActionsCallbackInterface.OnGuard;
                @Guard.performed -= m_Wrapper.m_GamepadinputActionsCallbackInterface.OnGuard;
                @Guard.canceled -= m_Wrapper.m_GamepadinputActionsCallbackInterface.OnGuard;
            }
            m_Wrapper.m_GamepadinputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Blast.started += instance.OnBlast;
                @Blast.performed += instance.OnBlast;
                @Blast.canceled += instance.OnBlast;
                @Guard.started += instance.OnGuard;
                @Guard.performed += instance.OnGuard;
                @Guard.canceled += instance.OnGuard;
            }
        }
    }
    public GamepadinputActions @Gamepadinput => new GamepadinputActions(this);
    public interface IPCInputActions
    {
        void OnHorizontal(InputAction.CallbackContext context);
        void OnVertical(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnGuard(InputAction.CallbackContext context);
        void OnBlast(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
    public interface IGamepadinputActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnBlast(InputAction.CallbackContext context);
        void OnGuard(InputAction.CallbackContext context);
    }
}