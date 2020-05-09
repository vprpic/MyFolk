using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MyFolk.UI;

namespace MyFolk
{
    [RequireComponent(typeof(UIInputHandler))]
    public class InputHandler : MonoBehaviour
    {
        [HideInInspector]
        public UIInputHandler uIInteractionHandler;
        [HideInInspector]
        public ItemInteractionHandler itemInteractionHandler;

        private void Awake()
        {
            uIInteractionHandler = GetComponent<UIInputHandler>();

            itemInteractionHandler = new ItemInteractionHandler();
            itemInteractionHandler.Init();
        }

        private void Update()
        {
            if (!uIInteractionHandler.isHovering)
            {
                itemInteractionHandler.CheckForHit();
            }

            #region Time Management
            if (Input.GetKeyDown(KeyCode.Tilde) || Input.GetKeyDown(KeyCode.Alpha0))
            {
                Globals.ins.timeManager.SetTimeScale(0f);
            }
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                Globals.ins.timeManager.SetTimeScale(1f);
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                Globals.ins.timeManager.PauseGame();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Globals.ins.timeManager.SetTimeScale(2f);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Globals.ins.timeManager.SetTimeScale(3f);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Globals.ins.timeManager.SetTimeScale(4f);
            }
			#endregion Time Management
        }
    }
}