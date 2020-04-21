using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FlexibleUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public FlexibleUIData skinData;


    /// <summary>
    /// Must be attached in the editor for every UI element, used in the UIInputHandler component
    /// </summary>
    public BoolEvent onEnterExitUI;

    protected virtual void OnSkinUI()
    {

    }

    public virtual void Awake()
    {
        OnSkinUI();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(onEnterExitUI != null)
        {
            //onEnterExitUI must be attached in the editor on every FlexibleUI element
            onEnterExitUI.Raise(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (onEnterExitUI != null)
        {
            onEnterExitUI.Raise(false);
        }
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
