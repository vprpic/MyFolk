using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenuSpawner : MonoBehaviour
{
	[HideInInspector]
	public Camera mainCamera;
	public RadialMenu menuPrefab;

	[HideInInspector]
	public RadialMenu spawnedMenu;

	/// <summary>
	/// When the radial menu is destroyed it doesn't register as stopped hovering so we raise the event after destroying
	/// </summary>
	public BoolEvent onEnterExitUI;

	private void Awake()
	{
		mainCamera = Camera.main;
	}

	public void SpawnMenu(IInteractableItem item)
	{
		if (item.Actions.Length != 0)
		{
			RadialMenu newMenu = Instantiate(menuPrefab) as RadialMenu;
			newMenu.transform.SetParent(transform, false);
			//TODO: test with item.pointclick and change in interactable to give 
			Vector3 mousePosition = Input.mousePosition;
			//newMenu.worldPoint = mainCamera.ScreenToWorldPoint(mousePosition);
			newMenu.transform.position = mousePosition;
			spawnedMenu = newMenu;
			spawnedMenu.worldPoint = item.ClickPoint;
			newMenu.SpawnButtons(item);
		}
	}

	/// <summary>
	/// A radial button from the menu has been clicked, remove the menu from screen
	/// </summary>
	public void OnRadialButtonClicked()
	{
		//TODO: no destroying
		DestroyMenu();
	}

	public void OnInteractableClick(InteractableItem item)
	{
		if (spawnedMenu == null)
		{
			SpawnMenu(item);
		}
		else
		{
			DestroyMenu();
		}
	}

	private void DestroyMenu()
	{
		GameObject.Destroy(spawnedMenu.gameObject);
		onEnterExitUI.Raise(false);
	}
}
