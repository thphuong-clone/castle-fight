using System;
using System.Collections.Generic;
using UnityEngine;

class TestCortana : MonoBehaviour
{
    public UnityEngine.UI.Text cortanaText;

    void Start()
    {
        cortanaText.text = "Press the button and tell me something (✿◕‿◕)";
        cortanaText.alignment = TextAnchor.MiddleCenter;
    }

    void Update()
    {
        if (Cortana.Interop.receivingVoice)
        {
            cortanaText.text = "Listening";
        }
        else
        {
            if (!string.IsNullOrEmpty(Cortana.Interop.cortanaReceivedText))
            {
                cortanaText.text = Cortana.Interop.cortanaReceivedText;
            }
        }
    }
}

