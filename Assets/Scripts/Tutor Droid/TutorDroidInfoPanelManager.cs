using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorDroidInfoPanelManager : MonoBehaviour
{
    public TMP_Text gravityValue;

    void Start()
    {
        gravityValue.text = Physics.gravity.magnitude.ToString();        
    }   
}
