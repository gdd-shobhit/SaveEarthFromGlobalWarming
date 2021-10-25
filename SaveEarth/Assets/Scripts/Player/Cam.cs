// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Player/Cam.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Cam : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Cam()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Cam"",
    ""maps"": [
        {
            ""name"": ""Keyboard"",
            ""id"": ""c70f5966-9789-4063-890c-975f5410fde8"",
            ""actions"": [
                {
                    ""name"": ""Keyboard"",
                    ""type"": ""Value"",
                    ""id"": ""37194f85-002e-4b14-8e34-a58d4fc58f53"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""ccc21a80-0755-4a8d-a1d9-8479fdddcbd8"",
                    ""path"": ""2DVector"",
                    ""interactions"": ""Hold(duration=0.2,pressPoint=0.2)"",
                    ""processors"": ""ScaleVector2(x=10,y=10)"",
                    ""groups"": """",
                    ""action"": ""Keyboard"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""729c55be-b196-4cdb-a2b8-3166caaea6a3"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Keyboard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""eb51a65e-d809-4adc-bd24-dd7af03b9f3c"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Keyboard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""76012de4-74a1-466e-a17d-234f33b5dc2a"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Keyboard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""4fca0b64-eeb7-4d99-b9be-85a3ea845220"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Keyboard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Keyboard
        m_Keyboard = asset.FindActionMap("Keyboard", throwIfNotFound: true);
        m_Keyboard_Keyboard = m_Keyboard.FindAction("Keyboard", throwIfNotFound: true);
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

    // Keyboard
    private readonly InputActionMap m_Keyboard;
    private IKeyboardActions m_KeyboardActionsCallbackInterface;
    private readonly InputAction m_Keyboard_Keyboard;
    public struct KeyboardActions
    {
        private @Cam m_Wrapper;
        public KeyboardActions(@Cam wrapper) { m_Wrapper = wrapper; }
        public InputAction @Keyboard => m_Wrapper.m_Keyboard_Keyboard;
        public InputActionMap Get() { return m_Wrapper.m_Keyboard; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KeyboardActions set) { return set.Get(); }
        public void SetCallbacks(IKeyboardActions instance)
        {
            if (m_Wrapper.m_KeyboardActionsCallbackInterface != null)
            {
                @Keyboard.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnKeyboard;
                @Keyboard.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnKeyboard;
                @Keyboard.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnKeyboard;
            }
            m_Wrapper.m_KeyboardActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Keyboard.started += instance.OnKeyboard;
                @Keyboard.performed += instance.OnKeyboard;
                @Keyboard.canceled += instance.OnKeyboard;
            }
        }
    }
    public KeyboardActions @Keyboard => new KeyboardActions(this);
    public interface IKeyboardActions
    {
        void OnKeyboard(InputAction.CallbackContext context);
    }
}
