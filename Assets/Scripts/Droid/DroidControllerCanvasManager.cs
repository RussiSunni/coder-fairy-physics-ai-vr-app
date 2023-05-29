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
}
