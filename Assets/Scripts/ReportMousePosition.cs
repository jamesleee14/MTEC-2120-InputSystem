using UnityEngine;
using UnityEngine.InputSystem;

// Essentially this is the new Input System Equivalent of Get Key,
// Get Button and Input.mousePosition and is an easy way to get the
// new Input System working quickly.
public class ReportMousePosition : MonoBehaviour
{
    void Update()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            Debug.Log("A key was pressed");
        }
        if (Gamepad.current.aButton.wasPressedThisFrame)
        {
            Debug.Log("A button was pressed");
        }
    }
}
