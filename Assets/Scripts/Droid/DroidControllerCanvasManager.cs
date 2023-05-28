using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DroidControllerCanvasManager : MonoBehaviour
{
    // Show / Hide UI.
    public Toggle canvasToggle;
    public CanvasGroup droidControllerUICanvasGroup;

    // Shoot / Turns.
    public TMP_Text moveCounter;
    private int _moveCounter = 0;
    bool triggerValue;

    void Start()
    {
        canvasToggle.onValueChanged.AddListener(delegate
        {
            CanvasToggleValueChanged(canvasToggle);
        });
    }

    void CanvasToggleValueChanged(Toggle canvasToggle)
    {
        if (canvasToggle.isOn)
        {
            droidControllerUICanvasGroup.alpha = 1;
            droidControllerUICanvasGroup.interactable = true;
        }
        else
        {
            droidControllerUICanvasGroup.alpha = 0;
            droidControllerUICanvasGroup.interactable = false;
        }
    }

    public void CountMoves()
    {
        _moveCounter++;
        moveCounter.text = _moveCounter.ToString();
    }

    void LateUpdate()
    {
        var rightHandedControllers = new List<UnityEngine.XR.InputDevice>();
        var desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, rightHandedControllers);
        foreach (var device in rightHandedControllers)
        {
            // Shoot (droid shoots forward).
            if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
            {
                CountMoves();
            }
        }
    }
}
