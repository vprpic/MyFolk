using MyFolk;
using MyFolk.FlexibleUI;
using MyFolk.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks
{
    [System.Serializable]
    public abstract class EventInfo
    {
        public string EventDescription;
    }

    public class DebugEventInfo : EventInfo
    {
        public int VerbosityLevel;
    }
}