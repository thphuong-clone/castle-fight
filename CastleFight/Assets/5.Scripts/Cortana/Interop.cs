using System;
using UnityEngine;

namespace Cortana
{
    public class Interop : MonoBehaviour
    {
        public static bool receivingVoice;
        /// <summary>
        /// Text string converted from speech
        /// </summary>
        public static string cortanaReceivedText;

        public static event EventHandler SpeechRequested;
        /// <summary>
        /// Start Cortana
        /// </summary>
        public static void StartReceivingVoice()
        {
            if (SpeechRequested != null && !receivingVoice)
            {
                SpeechRequested(null, null);
            }
        }
    }
}
