using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.XR;
using System;

public class HandPresence : MonoBehaviour
{
    private InputDevice targetDevice;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Debug.Log("START!");
        StartCoroutine(GetDevices(1.0f));
    }
    
    IEnumerator GetDevices(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);

        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        if(devices.Count > 0)
        {
            targetDevice = devices[0];
        }
    }
    // Update is called once per frame
    void Update()
    {
        targetDevice.TryGetFeatureValue(CommonUsages.gripButton, out bool primaryButtonValue);
        if (primaryButtonValue == true)
        {
            Debug.Log("RightButton Pressed");
        }
        targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        if (triggerValue > 0.5)
        {
            Debug.Log("Trigger Pressed");
        }
        targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue);
        if (primary2DAxisValue != Vector2.zero)
        {
            Debug.Log("Primary Touchpad: " + primary2DAxisValue);
        }
    }
}