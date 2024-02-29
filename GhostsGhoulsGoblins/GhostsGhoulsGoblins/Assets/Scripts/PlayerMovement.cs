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

public partial class @PlayerMovement: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerMovement()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerMovement"",
    ""maps"": [
        {
            ""name"": ""PlayerMoves"",
            ""id"": ""762e3350-8fce-4d0c-af97-d41eb6ed411d"",
            ""actions"": [
                {
                    ""name"": ""PlayerControls"",
                    ""type"": ""PassThrough"",
                    ""id"": ""f27c0f57-0a9a-4ccf-af55-a42d76e0f17b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""444ca5d3-3e93-4bc7-bcde-a18e8c75afb9"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerControls"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""84a3829a-5a60-4363-94c1-a7dbbd6e5c1b"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerControls"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c820ff7a-dc42-4b53-ab92-2785c1b83133"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerControls"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e880156b-5691-41f7-9479-e7aceea76273"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerControls"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""dcf60879-bda1-46f3-baab-37b1ff33a0e3"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerControls"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerMoves
        m_PlayerMoves = asset.FindActionMap("PlayerMoves", throwIfNotFound: true);
        m_PlayerMoves_PlayerControls = m_PlayerMoves.FindAction("PlayerControls", throwIfNotFound: true);
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
    private readonly InputAction m_PlayerMoves_PlayerControls;
    public struct PlayerMovesActions
    {
        private @PlayerMovement m_Wrapper;
        public PlayerMovesActions(@PlayerMovement wrapper) { m_Wrapper = wrapper; }
        public InputAction @PlayerControls => m_Wrapper.m_PlayerMoves_PlayerControls;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMoves; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMovesActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerMovesActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerMovesActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerMovesActionsCallbackInterfaces.Add(instance);
            @PlayerControls.started += instance.OnPlayerControls;
            @PlayerControls.performed += instance.OnPlayerControls;
            @PlayerControls.canceled += instance.OnPlayerControls;
        }

        private void UnregisterCallbacks(IPlayerMovesActions instance)
        {
            @PlayerControls.started -= instance.OnPlayerControls;
            @PlayerControls.performed -= instance.OnPlayerControls;
            @PlayerControls.canceled -= instance.OnPlayerControls;
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
        void OnPlayerControls(InputAction.CallbackContext context);
    }
}
