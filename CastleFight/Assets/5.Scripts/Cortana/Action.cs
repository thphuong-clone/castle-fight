using System;
using UnityEngine;

namespace Cortana
{
    public class Action : MonoBehaviour
    {
        /// <summary>
        /// Start Cortana
        /// </summary>
        public void StartReceivingVoice()
        {
            Interop.StartReceivingVoice();
        }
    }
}
