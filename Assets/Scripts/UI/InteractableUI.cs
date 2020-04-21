using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractableUI : MonoBehaviour//, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler
{
	//public bool isHit;
	//public bool isHovering;
	//public BoolEvent onEnterExitUI;
	//public IInteractableItem currentTarget;

	//public virtual void OnPointerEnter(PointerEventData eventData)
	//{
	//	isHovering = true;
	//	isHit = false;
	//	onEnterExitUI.Raise(isHovering);
	//}

	//public virtual void OnPointerClick(PointerEventData eventData)
	//{
	//	if (eventData.button == PointerEventData.InputButton.Left)
	//	{
	//		Debug.Log("UIInteractionRaycasting-onPointerClick " + eventData.selectedObject.name);
	//		//currentTarget = eventData.selectedObject.GetComponent<InteractableItem>();
	//		isHit = true;
	//	}
	//}

	//public virtual void OnPointerExit(PointerEventData eventData)
	//{
	//	isHovering = false;
	//	onEnterExitUI.Raise(isHovering);
	//}
}
