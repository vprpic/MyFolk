using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemInteractionHandler
{
	public Ray ray;
	public Camera mainCamera;
	public float range = 500f;
	public IInteractableItem currentTarget;
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
			if (currentTarget != null)
			{
				Globals.ins.SetLastWorldClickPoint(whatIHit.point);
				Debug.Log("InteractionRaycasting-Update-whatIHit: " + whatIHit.collider.name+"\n" +
					whatIHit.point);
				currentTarget.OnInteract(whatIHit.point);
			}
		}
	}

	private void RaycastForTarget()
	{
		float distance;
		IInteractableItem interactable = null;
		Vector3 mousePosition = Input.mousePosition;
		Ray ray = mainCamera.ScreenPointToRay(mousePosition);
		isHit = Physics.Raycast(ray, out whatIHit, range);
		if (isHit)
		{
			distance = whatIHit.distance;
			interactable = whatIHit.collider.GetComponent<IInteractableItem>();

			if(interactable != null)
			{
				if(whatIHit.distance <= interactable.MaxRange)
				{
					if (interactable == currentTarget)
					{
						return;
					}
					else if (currentTarget != null)
					{
						currentTarget.OnEndHover();
						currentTarget = interactable;
						currentTarget.OnStartHover();
						return;
					}
					else
					{
						currentTarget = interactable;
						currentTarget.OnStartHover();
						return;
					}
				}
				else
				{
					if(currentTarget != null)
					{
						currentTarget.OnEndHover();
						currentTarget = null;
						return;
					}
				}
			}
			else
			{
				if (currentTarget != null)
				{
					currentTarget.OnEndHover();
					currentTarget = null;
					return;
				}
			}
		}
		else
		{
			if (currentTarget != null)
			{
				currentTarget.OnEndHover();
				currentTarget = null;
				return;
			}
		}
	}

}
