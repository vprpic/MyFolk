using EventCallbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFolk.UI
{
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
		//public BoolEvent onEnterExitUI;

		private void Awake()
		{
			mainCamera = Camera.main;
			EventSystem.Current.RegisterListener<InteractableItemClickedEventInfo>(OnInteractableClick);
			EventSystem.Current.RegisterListener<RadialButtonClickEventInfo>(OnRadialButtonClicked);
		}

		public void SpawnMenu(InteractableItemClickedEventInfo eventInfo)
		{
			if (eventInfo.iitem.Interactions.Length != 0)
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
		public void OnRadialButtonClicked(RadialButtonClickEventInfo radialButtonClickEventInfo)
		{
			//TODO: no destroying
			DestroyMenu(radialButtonClickEventInfo);
		}

		void OnInteractableClick(InteractableItemClickedEventInfo interactableItemClickedInfo)
		{
			//Debug.Log("Alerted about interactable clicked: " + interactableItemClickedInfo.iitem.itemName);
			if (spawnedMenu == null)
			{
				SpawnMenu(interactableItemClickedInfo);
			}
			else
			{
				DestroyMenu();
			}
		}

		private void DestroyMenu(RadialButtonClickEventInfo radialButtonClickEventInfo = null)
		{
			GameObject.Destroy(spawnedMenu.gameObject);
			EventSystem.Current.FireEvent(new FlexibleUIEnterExitEventInfo(
				radialButtonClickEventInfo != null ? radialButtonClickEventInfo.radialButtonUI : null,
				false));
		}
	}
}