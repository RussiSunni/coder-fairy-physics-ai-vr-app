using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DroidControllerCanvasManager : MonoBehaviour
{
    public Toggle canvasToggle;
    public CanvasGroup droidControllerUICanvasGroup;

    void Start()
    {
        canvasToggle.onValueChanged.AddListener(delegate {
            CanvasToggleValueChanged(canvasToggle);
        });
    }

     void CanvasToggleValueChanged (Toggle canvasToggle)
    {
        Debug.Log("test");

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
