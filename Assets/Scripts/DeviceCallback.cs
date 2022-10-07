using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;

// DeviceCallback.cs - This script shows how to define a callback to get
// notified on device additions and removals.

sealed class DeviceCallback : MonoBehaviour
{
    void Start()
    {
        InputSystem.onDeviceChange += (device, change) =>
        {

            Debug.Log(string.Format("{0} {1} ",
                device.description.product, change));
        };
    }
}
