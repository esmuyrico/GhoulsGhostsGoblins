//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/Scripts/PlayerMovement.inputactions
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

public partial class @PlayerMove: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerMove()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerMovement"",
    ""maps"": [
        {
            ""name"": ""PlayerMoves"",
            ""id"": ""e641db0e-846d-4c72-8bc4-757aec47c61a"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""2980766f-0c71-467f-8837-f6a274d04d26"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Dive"",
                    ""type"": ""Button"",
                    ""id"": ""2925433f-a87e-4033-9493-9401db21d4cc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""55101583-13a8-470d-8db0-0488322259ee"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MoveForward"",
                    ""type"": ""PassThrough"",
                    ""id"": ""3a10cf4d-b9fe-446c-9e8a-78986f9a7407"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MoveLeft"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b781b6e4-4a21-4e9f-83e9-4ab6c32b44de"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MoveRight"",
                    ""type"": ""PassThrough"",
                    ""id"": ""4eba0d03-93e2-46e7-9bad-6dc2d3ab13fc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MoveBack"",
                    ""type"": ""PassThrough"",
                    ""id"": ""62b8ba41-ab55-4c01-85bd-c20981821d4d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold"",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Walk"",
                    ""id"": ""7921958c-1e13-4ae0-ae88-0530b7ffe375"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""8bbf9088-8e53-4e78-bd2d-e0fb6d037c34"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""080bce32-859c-48b7-9aef-72ab5f56137f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""19f4603e-e38f-4f49-889a-84acec09766d"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9e36a811-589c-4186-9888-c031d820c32c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7092d16e-fdd1-42e6-8c19-e7e7b05db907"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dive"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""96e60565-64d2-4193-92f5-2561bccd5491"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0770910c-8ccd-496e-90ff-e8c3c1bd59a8"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveForward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4f9b6278-1408-4678-8d16-39f31265c0be"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""03448f2e-625e-4d75-90f6-eaaa94c7a37d"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""be95e8af-5311-4f4c-92ee-419df4684f9e"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveBack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerMoves
        m_PlayerMoves = asset.FindActionMap("PlayerMoves", throwIfNotFound: true);
        m_PlayerMoves_Movement = m_PlayerMoves.FindAction("Movement", throwIfNotFound: true);
        m_PlayerMoves_Dive = m_PlayerMoves.FindAction("Dive", throwIfNotFound: true);
        m_PlayerMoves_Jump = m_PlayerMoves.FindAction("Jump", throwIfNotFound: true);
        m_PlayerMoves_MoveForward = m_PlayerMoves.FindAction("MoveForward", throwIfNotFound: true);
        m_PlayerMoves_MoveLeft = m_PlayerMoves.FindAction("MoveLeft", throwIfNotFound: true);
        m_PlayerMoves_MoveRight = m_PlayerMoves.FindAction("MoveRight", throwIfNotFound: true);
        m_PlayerMoves_MoveBack = m_PlayerMoves.FindAction("MoveBack", throwIfNotFound: true);
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

    // PlayerMoves
    private readonly InputActionMap m_PlayerMoves;
    private List<IPlayerMovesActions> m_PlayerMovesActionsCallbackInterfaces = new List<IPlayerMovesActions>();
    private readonly InputAction m_PlayerMoves_Movement;
    private readonly InputAction m_PlayerMoves_Dive;
    private readonly InputAction m_PlayerMoves_Jump;
    private readonly InputAction m_PlayerMoves_MoveForward;
    private readonly InputAction m_PlayerMoves_MoveLeft;
    private readonly InputAction m_PlayerMoves_MoveRight;
    private readonly InputAction m_PlayerMoves_MoveBack;
    public struct PlayerMovesActions
    {
        private @PlayerMove m_Wrapper;
        public PlayerMovesActions(@PlayerMove wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerMoves_Movement;
        public InputAction @Dive => m_Wrapper.m_PlayerMoves_Dive;
        public InputAction @Jump => m_Wrapper.m_PlayerMoves_Jump;
        public InputAction @MoveForward => m_Wrapper.m_PlayerMoves_MoveForward;
        public InputAction @MoveLeft => m_Wrapper.m_PlayerMoves_MoveLeft;
        public InputAction @MoveRight => m_Wrapper.m_PlayerMoves_MoveRight;
        public InputAction @MoveBack => m_Wrapper.m_PlayerMoves_MoveBack;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMoves; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMovesActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerMovesActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerMovesActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerMovesActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @Dive.started += instance.OnDive;
            @Dive.performed += instance.OnDive;
            @Dive.canceled += instance.OnDive;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @MoveForward.started += instance.OnMoveForward;
            @MoveForward.performed += instance.OnMoveForward;
            @MoveForward.canceled += instance.OnMoveForward;
            @MoveLeft.started += instance.OnMoveLeft;
            @MoveLeft.performed += instance.OnMoveLeft;
            @MoveLeft.canceled += instance.OnMoveLeft;
            @MoveRight.started += instance.OnMoveRight;
            @MoveRight.performed += instance.OnMoveRight;
            @MoveRight.canceled += instance.OnMoveRight;
            @MoveBack.started += instance.OnMoveBack;
            @MoveBack.performed += instance.OnMoveBack;
            @MoveBack.canceled += instance.OnMoveBack;
        }

        private void UnregisterCallbacks(IPlayerMovesActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @Dive.started -= instance.OnDive;
            @Dive.performed -= instance.OnDive;
            @Dive.canceled -= instance.OnDive;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @MoveForward.started -= instance.OnMoveForward;
            @MoveForward.performed -= instance.OnMoveForward;
            @MoveForward.canceled -= instance.OnMoveForward;
            @MoveLeft.started -= instance.OnMoveLeft;
            @MoveLeft.performed -= instance.OnMoveLeft;
            @MoveLeft.canceled -= instance.OnMoveLeft;
            @MoveRight.started -= instance.OnMoveRight;
            @MoveRight.performed -= instance.OnMoveRight;
            @MoveRight.canceled -= instance.OnMoveRight;
            @MoveBack.started -= instance.OnMoveBack;
            @MoveBack.performed -= instance.OnMoveBack;
            @MoveBack.canceled -= instance.OnMoveBack;
        }

        public void RemoveCallbacks(IPlayerMovesActions instance)
        {
            if (m_Wrapper.m_PlayerMovesActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerMovesActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerMovesActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerMovesActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerMovesActions @PlayerMoves => new PlayerMovesActions(this);
    public interface IPlayerMovesActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnDive(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnMoveForward(InputAction.CallbackContext context);
        void OnMoveLeft(InputAction.CallbackContext context);
        void OnMoveRight(InputAction.CallbackContext context);
        void OnMoveBack(InputAction.CallbackContext context);
    }
}
