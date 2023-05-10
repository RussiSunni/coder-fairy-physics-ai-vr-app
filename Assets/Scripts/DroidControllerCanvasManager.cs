using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DroidControllerCanvasManager : MonoBehaviour
{
    public Toggle canvasToggle;
    public CanvasGroup droidControllerUICanvasGroup;
    public TMP_Text moveCounter;
    private int _moveCounter = 0;

    void Start()
    {
        canvasToggle.onValueChanged.AddListener(delegate {
            CanvasToggleValueChanged(canvasToggle);
        });
    }

     void CanvasToggleValueChanged (Toggle canvasToggle)
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
}
