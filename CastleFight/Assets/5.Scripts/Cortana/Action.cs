using System;
using UnityEngine;

namespace Cortana
{
    public class Action : MonoBehaviour
    {
        public void StartReceivingVoice()
        {
            Interop.StartReceivingVoice();
        }
    }
}
