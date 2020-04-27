using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        }
    }
}