//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/My Controls.inputactions
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

public partial class @MyControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @MyControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""My Controls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""97f4e99f-eeb5-493d-8a56-8190c68cf013"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""16997941-d020-49de-aa5d-affdb9b0b5a3"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""96018fc2-fa33-4d8e-beb8-f8aad3ed6a07"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RotateCenter"",
                    ""type"": ""PassThrough"",
                    ""id"": ""19bc92b5-6244-4507-a3d9-a2a9a2ee6ad5"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""1404df4f-fbad-4998-afd5-4f9f0e83585d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TouchSeed"",
                    ""type"": ""Button"",
                    ""id"": ""fb7dc23b-5898-4816-8b03-97320897bf35"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TypeChange"",
                    ""type"": ""Button"",
                    ""id"": ""4cf312ad-f69d-4d60-b3ef-b3491a549fdf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Setting"",
                    ""type"": ""Button"",
                    ""id"": ""551cb77f-747f-4d93-b74d-2f5693007781"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""DialogeSkip"",
                    ""type"": ""Button"",
                    ""id"": ""52e2c39a-4e35-495e-a662-78ba6e12e1fb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""VideoSkip"",
                    ""type"": ""Button"",
                    ""id"": ""242c84bb-a632-4eec-a196-5f8cbfc3cd55"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TutorSkip"",
                    ""type"": ""Button"",
                    ""id"": ""536c5ac0-70af-4700-be4b-37c7757eb125"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""bdec5ec8-bdf4-439c-bd85-56e8b50c2bfc"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""1b2d6431-ad95-4a80-a852-b6c6ecce88b6"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""6fc14973-b0ff-4c5e-8c59-a033325b1d5a"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""19549e07-2ff3-47ad-9326-c1b1dc770186"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""ad695237-23e0-42ff-9c1e-4c6a618f7ccd"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""1b548f3c-4090-4855-a526-688a7b0ee47e"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""50da2e81-c025-404b-a9a6-e03042830023"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8b925640-2385-4a36-bdd4-451d1a25117e"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d8c7f508-e3bd-4ee7-b15e-17ff31ca0050"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""992b9a30-d1d4-46a7-8e76-57fe38f83523"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c2e17b9b-5f57-47a5-a46f-52b48785dac4"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateCenter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""86a6b5c9-0f4e-4b98-952e-6ad4e175793c"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateCenter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""36444e15-fe69-4933-90cd-09d05ce1d417"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchSeed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e9c80ab6-2dac-4379-ae14-1436482e2d52"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchSeed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b875aabb-437e-4b55-804a-257fa435f74e"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TypeChange"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e536b74c-acf1-4922-bf85-0f773b0dbad4"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TypeChange"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a094c899-62de-400b-b060-3307d044f533"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Setting"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ddecbd2d-9810-452f-9bf0-2e8fe5efe652"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Setting"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""28c58334-8127-4c7e-89fc-b89d73df257e"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DialogeSkip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0210eb90-bf23-43f7-a90e-9b8dcc4cd92a"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DialogeSkip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""92e626ba-653a-4534-bb59-4432e62813a3"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""VideoSkip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0d116797-5e19-498a-9d3b-67d56f924219"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""VideoSkip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ff65a12a-18c4-445a-8bcd-b277c6736d2c"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TutorSkip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f2643ba1-1343-42dc-b993-8b6d6f54fc57"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TutorSkip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_RotateCenter = m_Player.FindAction("RotateCenter", throwIfNotFound: true);
        m_Player_Attack = m_Player.FindAction("Attack", throwIfNotFound: true);
        m_Player_TouchSeed = m_Player.FindAction("TouchSeed", throwIfNotFound: true);
        m_Player_TypeChange = m_Player.FindAction("TypeChange", throwIfNotFound: true);
        m_Player_Setting = m_Player.FindAction("Setting", throwIfNotFound: true);
        m_Player_DialogeSkip = m_Player.FindAction("DialogeSkip", throwIfNotFound: true);
        m_Player_VideoSkip = m_Player.FindAction("VideoSkip", throwIfNotFound: true);
        m_Player_TutorSkip = m_Player.FindAction("TutorSkip", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_RotateCenter;
    private readonly InputAction m_Player_Attack;
    private readonly InputAction m_Player_TouchSeed;
    private readonly InputAction m_Player_TypeChange;
    private readonly InputAction m_Player_Setting;
    private readonly InputAction m_Player_DialogeSkip;
    private readonly InputAction m_Player_VideoSkip;
    private readonly InputAction m_Player_TutorSkip;
    public struct PlayerActions
    {
        private @MyControls m_Wrapper;
        public PlayerActions(@MyControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @RotateCenter => m_Wrapper.m_Player_RotateCenter;
        public InputAction @Attack => m_Wrapper.m_Player_Attack;
        public InputAction @TouchSeed => m_Wrapper.m_Player_TouchSeed;
        public InputAction @TypeChange => m_Wrapper.m_Player_TypeChange;
        public InputAction @Setting => m_Wrapper.m_Player_Setting;
        public InputAction @DialogeSkip => m_Wrapper.m_Player_DialogeSkip;
        public InputAction @VideoSkip => m_Wrapper.m_Player_VideoSkip;
        public InputAction @TutorSkip => m_Wrapper.m_Player_TutorSkip;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @RotateCenter.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotateCenter;
                @RotateCenter.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotateCenter;
                @RotateCenter.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotateCenter;
                @Attack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @TouchSeed.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTouchSeed;
                @TouchSeed.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTouchSeed;
                @TouchSeed.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTouchSeed;
                @TypeChange.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTypeChange;
                @TypeChange.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTypeChange;
                @TypeChange.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTypeChange;
                @Setting.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSetting;
                @Setting.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSetting;
                @Setting.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSetting;
                @DialogeSkip.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDialogeSkip;
                @DialogeSkip.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDialogeSkip;
                @DialogeSkip.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDialogeSkip;
                @VideoSkip.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnVideoSkip;
                @VideoSkip.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnVideoSkip;
                @VideoSkip.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnVideoSkip;
                @TutorSkip.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTutorSkip;
                @TutorSkip.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTutorSkip;
                @TutorSkip.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTutorSkip;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @RotateCenter.started += instance.OnRotateCenter;
                @RotateCenter.performed += instance.OnRotateCenter;
                @RotateCenter.canceled += instance.OnRotateCenter;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @TouchSeed.started += instance.OnTouchSeed;
                @TouchSeed.performed += instance.OnTouchSeed;
                @TouchSeed.canceled += instance.OnTouchSeed;
                @TypeChange.started += instance.OnTypeChange;
                @TypeChange.performed += instance.OnTypeChange;
                @TypeChange.canceled += instance.OnTypeChange;
                @Setting.started += instance.OnSetting;
                @Setting.performed += instance.OnSetting;
                @Setting.canceled += instance.OnSetting;
                @DialogeSkip.started += instance.OnDialogeSkip;
                @DialogeSkip.performed += instance.OnDialogeSkip;
                @DialogeSkip.canceled += instance.OnDialogeSkip;
                @VideoSkip.started += instance.OnVideoSkip;
                @VideoSkip.performed += instance.OnVideoSkip;
                @VideoSkip.canceled += instance.OnVideoSkip;
                @TutorSkip.started += instance.OnTutorSkip;
                @TutorSkip.performed += instance.OnTutorSkip;
                @TutorSkip.canceled += instance.OnTutorSkip;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnRotateCenter(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnTouchSeed(InputAction.CallbackContext context);
        void OnTypeChange(InputAction.CallbackContext context);
        void OnSetting(InputAction.CallbackContext context);
        void OnDialogeSkip(InputAction.CallbackContext context);
        void OnVideoSkip(InputAction.CallbackContext context);
        void OnTutorSkip(InputAction.CallbackContext context);
    }
}
