using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using EventCallbacks;

namespace MyFolk.FlexibleUI
{
    public class FlexibleUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public FlexibleUIData skinData;

        protected virtual void OnSkinUI() { }
        public virtual void Update() { }
        public virtual void Awake()
        {
            OnSkinUI();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData != null)
            {
                //Debug.Log("OnPointerEnter: " + eventData.pointerCurrentRaycast.gameObject.name);
                (new FlexibleUIEnterExitEvent(this, true)).FireEvent();
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (eventData != null)
            {
                    //Debug.Log("OnPointerExit");
                (new FlexibleUIEnterExitEvent(this, false)).FireEvent();
            }
        }

    }
}