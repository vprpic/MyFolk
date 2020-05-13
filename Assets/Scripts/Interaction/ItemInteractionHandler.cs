using EventCallbacks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFolk
{
	public class ItemInteractionHandler
	{
		public Ray ray;
		public Camera mainCamera;
		public float range = 500f;
		public InteractableItem currentTargetIItem;
		public InteractableItemClickedEvent currentEventInfo;
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
					currentEventInfo.iitem = currentTargetIItem;
					(new InteractableItemClickedEvent(
							Globals.ins.currentlySelectedCharacter,
							currentEventInfo.iitem,
							currentEventInfo.hit,
							currentEventInfo.screenClickPoint
							)
					).FireEvent();
					currentTargetIItem.OnInteract(currentEventInfo.hit.point); //the interactable doesn't listen to the event, all of them would be activated
				}
			}
		}

		private void RaycastForTarget()
		{
			if (currentEventInfo == null)
			{
				currentEventInfo = new InteractableItemClickedEvent();
			}
			InteractableItem interactable = null;
			Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
			isHit = Physics.Raycast(ray, out whatIHit, range);
			if (isHit)
			{
				currentEventInfo.screenClickPoint = Input.mousePosition;
				currentEventInfo.hit = whatIHit;
				interactable = whatIHit.collider.GetComponent<InteractableItem>();

				if (interactable != null)
				{
					if (whatIHit.distance <= interactable.RaycastRange)
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
}