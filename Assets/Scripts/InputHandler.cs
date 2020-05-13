using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MyFolk.UI;
using MyFolk.Time;

namespace MyFolk
{
    [RequireComponent(typeof(UIInputHandler))]
    public class InputHandler : MonoBehaviour
    {
        [HideInInspector]
        public UIInputHandler uIInteractionHandler;
        [HideInInspector]
        public ItemInteractionHandler itemInteractionHandler;
        public GameMode currentGameMode;

        private void Awake()
        {
            uIInteractionHandler = GetComponent<UIInputHandler>();

            itemInteractionHandler = new ItemInteractionHandler();
            itemInteractionHandler.Init();
        }

        private void Update()
        {

            switch (this.currentGameMode)
            {
                case GameMode.Play:
                    PlayMode();
                    break;
                case GameMode.Build:
                    break;
                case GameMode.Menu:
                    break;
            }
        }

        private void PlayMode()
        {
            if (!uIInteractionHandler.isHovering)
            {
                itemInteractionHandler.CheckForHit();
            }

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