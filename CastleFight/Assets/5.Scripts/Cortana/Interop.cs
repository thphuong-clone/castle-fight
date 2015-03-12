using System;
using UnityEngine;

namespace Cortana
{
    public class Interop : MonoBehaviour
    {
        public static bool receivingVoice;
        public static string cortanaReceivedText;

        public static event EventHandler SpeechRequested;
        public static void StartReceivingVoice()
        {
            if (SpeechRequested != null && !receivingVoice)
            {
                SpeechRequested(null, null);
            }
        }
    }
}
