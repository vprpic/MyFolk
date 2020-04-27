using EventCallbacks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractionHandler
{
	public Ray ray;
	public Camera mainCamera;
	public float range = 500f;
	public InteractableItem currentTargetIItem;
	public InteractableItemClickedEventInfo currentEventInfo;
	private bool isHit;
	RaycastHit whatIHit;
	public void Init()
	{
		mainCamera = Camera.main;
	}
	public void CheckForHit()
	{
		RaycastForTarget();
		HandleInput();
	}

	private void HandleInput()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (currentTargetIItem != null)
			{
				//Debug.Log("InteractionRaycasting-Update-whatIHit: " + whatIHit.collider.name+"\n" +
					//whatIHit.point);
				currentEventInfo.iitem = currentTargetIItem;
				EventSystem.Current.FireEvent(
					currentEventInfo
				);
				currentTargetIItem.OnInteract(whatIHit.point); //the interactable doesn't listen to the event, all of them would be activated
			}
		}
	}

	private void RaycastForTarget()
	{
		if(currentEventInfo == null)
		{
			currentEventInfo = new InteractableItemClickedEventInfo();
		}
		InteractableItem interactable = null;
		currentEventInfo.screenClickPoint = Input.mousePosition;
		Ray ray = mainCamera.ScreenPointToRay(currentEventInfo.screenClickPoint);
		isHit = Physics.Raycast(ray, out whatIHit, range);
		if (isHit)
		{
			currentEventInfo.worldClickPoint = whatIHit.point;
			interactable = whatIHit.collider.GetComponent<InteractableItem>();

			if(interactable != null)
			{
				if(whatIHit.distance <= interactable.MaxRange)
				{
					if (interactable == currentTargetIItem)
					{
						return;
					}
					else if (currentTargetIItem != null)
					{
						currentTargetIItem.OnEndHover();
						currentTargetIItem = interactable;
						currentTargetIItem.OnStartHover();
						return;
					}
					else
					{
						currentTargetIItem = interactable;
						currentTargetIItem.OnStartHover();
						return;
					}
				}
				else
				{
					if(currentTargetIItem != null)
					{
						currentTargetIItem.OnEndHover();
						currentTargetIItem = null;
						return;
					}
				}
			}
			else
			{
				if (currentTargetIItem != null)
				{
					currentTargetIItem.OnEndHover();
					currentTargetIItem = null;
					return;
				}
			}
		}
		else
		{
			if (currentTargetIItem != null)
			{
				currentTargetIItem.OnEndHover();
				currentTargetIItem = null;
				return;
			}
		}
	}

}
