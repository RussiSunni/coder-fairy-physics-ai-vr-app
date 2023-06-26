using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InformationPanelManager : MonoBehaviour
{
    public TMP_Text gravityValue;

    void Start()
    {
        gravityValue.text = Physics.gravity.magnitude.ToString();        
    }


    void Update()
    {
        
    }
}
