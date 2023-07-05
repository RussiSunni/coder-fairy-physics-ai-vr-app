using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorDroidInfoPanelManager : MonoBehaviour
{
    public TMP_Text gravityValue;
    public TMP_Text densityValue;

    void Start()
    {
        gravityValue.text = Physics.gravity.magnitude.ToString();
        densityValue.text = "1.204 kg/m3";
    }   
}
