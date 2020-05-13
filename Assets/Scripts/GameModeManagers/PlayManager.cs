using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFolk
{
    public class PlayManager
    {

        [HideInInspector]
        public ItemInteractionHandler itemInteractionHandler;

        public void Init()
        {
            itemInteractionHandler = new ItemInteractionHandler();
            itemInteractionHandler.Init();
        }

        public void Update()
        {
            //if (!UI.UIInputManager.isHovering)
            //{
                itemInteractionHandler.CheckForHit();
            //}

            #region Time Management
            if (Input.GetKeyDown(KeyCode.Tilde) || Input.GetKeyDown(KeyCode.Alpha0))
            {
                (new EventCallbacks.SetTimeScaleEvent(0f)).FireEvent();
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                (new EventCallbacks.SetTimeScaleEvent(1f)).FireEvent();
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                (new EventCallbacks.PauseTimeScaleEvent()).FireEvent();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                (new EventCallbacks.SetTimeScaleEvent(2f)).FireEvent();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                (new EventCallbacks.SetTimeScaleEvent(3f)).FireEvent();
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                (new EventCallbacks.SetTimeScaleEvent(4f)).FireEvent();
            }
            #endregion Time Management
        }
    }
}