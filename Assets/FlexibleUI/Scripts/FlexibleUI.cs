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


        /// <summary>
        /// Must be attached in the editor for every UI element, used in the UIInputHandler component
        /// </summary>
        //public BoolEvent onEnterExitUI;

        protected virtual void OnSkinUI()
        {

        }

        public virtual void Awake()
        {
            OnSkinUI();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            //Debug.Log("OnPointerEnter");
            if(eventData != null)
                EventCallbacks.EventSystem.Current.FireEvent(new FlexibleUIEnterExitEventInfo(this, true));
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            //Debug.Log("OnPointerExit");
            if (eventData != null)
                EventCallbacks.EventSystem.Current.FireEvent(new FlexibleUIEnterExitEventInfo(this, false));
        }

        //TODO: remove
#if UNITY_EDITOR
        public virtual void Update()
        {
            if (Application.isEditor)
            {
                OnSkinUI();
            }
        }

#endif
    }
}
