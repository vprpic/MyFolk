using EventCallbacks;
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
		EventSystem.Current.RegisterListener<InteractableItemClickedEventInfo>(OnInteractableClick);
	}

	public void SpawnMenu(InteractableItemClickedEventInfo eventInfo)
	{
		if (eventInfo.iitem.Actions.Length != 0)
		{
			RadialMenu newMenu = Instantiate(menuPrefab) as RadialMenu;
			newMenu.transform.SetParent(transform, false);
			newMenu.transform.position = eventInfo.screenClickPoint;
			spawnedMenu = newMenu;
			//spawnedMenu.worldPoint = eventInfo.worldClickPoint;
			newMenu.SpawnButtons(eventInfo);
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

	void OnInteractableClick(InteractableItemClickedEventInfo interactableItemClickedInfo)
	{
		Debug.Log("Alerted about interactable clicked: " + interactableItemClickedInfo.iitem.itemName);
		if (spawnedMenu == null)
		{
			SpawnMenu(interactableItemClickedInfo);
		}
		else
		{
			DestroyMenu();
		}
	}

	//public void OnInteractableClick(IInteractableItem item)
	//{
	//	if (spawnedMenu == null)
	//	{
	//		SpawnMenu(item);
	//	}
	//	else
	//	{
	//		DestroyMenu();
	//	}
	//}

	private void DestroyMenu()
	{
		GameObject.Destroy(spawnedMenu.gameObject);
		onEnterExitUI.Raise(false);
	}
}
