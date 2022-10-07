using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerBinding : MonoBehaviour
{
    /// <summary>
    /// Controller Options available to bind buttons to via Inspector. You can use GetControllerBindingValue() to determine if that button has been pressed.
    /// </summary>
    public enum GamepadBinding
    {
        None,
        AButton,
        AButtonDown,
        BButton,
        BButtonDown,
        XButton,
        XButtonDown,
        YButton,
        YButtonDown,
        StartButton,
        StartButtonDown,
        BackButton,
        BackButtonDown
    }

    [SerializeField]
    [Tooltip("Specify an InputActionSet for when using the Unity Input system. These actions will be enabled on load.")]
    public UnityEngine.InputSystem.InputActionAsset actionSet;

    [Header("Buttons")]
    /// <summary>
    /// Is the A button currently being held down
    /// </summary>
    public bool AButton = false;

    /// <summary>
    /// Returns true if the A Button was pressed down this frame but not last
    /// </summary>
    [Tooltip("Returns true if the A Button was pressed down this frame but not last")]
    public bool AButtonDown = false;

    // A Button Up this frame but down the last
    public bool AButtonUp = false;

    /// <summary>
    /// Is the B button currently being held down
    /// </summary>
    public bool BButton = false;

    /// <summary>
    /// Returns true if the B Button was pressed down this frame but not last
    /// </summary>
    [Tooltip("Returns true if the B Button was pressed down this frame but not last")]
    public bool BButtonDown = false;

    // B Button Up this frame but down the last
    public bool BButtonUp = false;

    public bool XButton = false;

    /// <summary>
    /// Returns true if the X Button was pressed down this frame but not last
    /// </summary>
    [Tooltip("Returns true if the X Button was pressed down this frame but not last")]
    public bool XButtonDown = false;

    // X Button Up this frame but down the last
    public bool XButtonUp = false;

    public bool YButton = false;
    /// <summary>
    /// Returns true if the Y Button was pressed down this frame but not last
    /// </summary>
    public bool YButtonDown = false;
    public bool YButtonUp = false;

    public bool StartButton = false;
    public bool StartButtonDown = false;
    public bool BackButton = false;
    public bool BackButtonDown = false;


    UnityEngine.InputSystem.InputAction aButton;
    UnityEngine.InputSystem.InputAction bButton;
    UnityEngine.InputSystem.InputAction xButton;
    UnityEngine.InputSystem.InputAction yButton;

    UnityEngine.InputSystem.InputAction startButton;
    UnityEngine.InputSystem.InputAction backButton;



    void OnEnable()
    {
        CreateUnityInputActions();
        EnableActions();
    }

    public virtual void EnableActions()
    {
        if (actionSet != null)
        {
            foreach (var map in actionSet.actionMaps)
            {
                foreach (var action in map)
                {
                    action.Enable();
                }
            }
        }
    }

    public virtual void CreateUnityInputActions()
    {
        aButton = CreateInputAction("aButton", "<Gamepad>/buttonWest", false);
        bButton = CreateInputAction("bButton", "<Gamepad>/buttonNorth", false);
        xButton = CreateInputAction("xButton", "<Gamepad>/buttonSouth", false);
        yButton = CreateInputAction("yButton", "<Gamepad>/buttonEast", false);

        startButton = CreateInputAction("startButton", "<Gamepad>/{Start}", false);
        backButton = CreateInputAction("backButton", "<Gamepad>/{Select}", false);

    }

    public UnityEngine.InputSystem.InputAction CreateInputAction(string actionName, string binding, bool valueType)
    {
        var act = new UnityEngine.InputSystem.InputAction(actionName,
            valueType ? UnityEngine.InputSystem.InputActionType.Value : UnityEngine.InputSystem.InputActionType.Button,
            binding);

        // Automatically enable this binding
        act.Enable();

        return act;
    }
}
