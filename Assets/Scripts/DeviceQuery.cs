using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;

// DeviceQuery.cs - This script shows how to search devices by a pattern
// matching with a product name and a channel number.

sealed class DeviceQuery : MonoBehaviour
{
    // Search by a product name (regex-able)
    [SerializeField] string _productName = null;


    InputDevice Search()
    {
        // Matcher object with Minis devices
        var match = new InputDeviceMatcher().WithInterface("HID");

        // Product name specifier
        if (!string.IsNullOrEmpty(_productName))
            match = match.WithProduct(_productName);

        // Scan all the devices found in the input system.
        foreach (var dev in InputSystem.devices)
            if (match.MatchPercentage(dev.description) > 0)
                return dev;

        return null;
    }

    System.Collections.IEnumerator Start()
    {
        while (true)
        {
            var device = Search();

            if (device != null)
            {
                Debug.Log("Device found: " + device.description);
                break;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}
