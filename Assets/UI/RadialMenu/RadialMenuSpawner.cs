using EventCallbacks;
using System;
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
			//EventSystem.Current.RegisterListener<InteractableItemClickedEvent>(OnInteractableClick);
			InteractableItemClickedEvent.RegisterListener(OnInteractableClick);
			RadialButtonClickEvent.RegisterListener(OnRadialButtonClicked);
			//EventSystem.Current.RegisterListener<RadialButtonClickEvent>(OnRadialButtonClicked);
		}

		public void SpawnMenu(InteractableItemClickedEvent eventInfo, List<Interaction> possibleInteractions)
		{
			if (eventInfo.iitem.Interactions.Count != 0)
			{
				RadialMenu newMenu = Instantiate(menuPrefab) as RadialMenu;
				newMenu.transform.SetParent(transform, false);
				newMenu.transform.position = eventInfo.screenClickPoint;
				spawnedMenu = newMenu;
				//spawnedMenu.worldPoint = eventInfo.worldClickPoint;
				newMenu.SpawnButtons(eventInfo, possibleInteractions);
			}
		}

		/// <summary>
		/// A radial button from the menu has been clicked, remove the menu from screen
		/// </summary>
		public void OnRadialButtonClicked(RadialButtonClickEvent radialButtonClickEventInfo)
		{
			//TODO: no destroying
			DestroyMenu(radialButtonClickEventInfo);
		}

		void OnInteractableClick(InteractableItemClickedEvent interactableItemClickedInfo)
		{
			//Debug.Log("Alerted about interactable clicked: " + interactableItemClickedInfo.iitem.itemName);
			if (spawnedMenu == null)
			{
				List<Interaction> possibleInteractions = GetCurrentlyPossibleActions(interactableItemClickedInfo);
				if(possibleInteractions.Count > 0)
					SpawnMenu(interactableItemClickedInfo, possibleInteractions);
			}
			else
			{
				DestroyMenu();
			}
		}

		private List<Interaction> GetCurrentlyPossibleActions(InteractableItemClickedEvent eventInfo)
		{
			List<Interaction> allTempInteractions = eventInfo.iitem.Interactions;
			allTempInteractions.AddRange(eventInfo.tempCharacterInteractions);

			List<Interaction> possibleInteractions = new List<Interaction>();
			foreach (Interaction interaction in allTempInteractions)
			{
				if(interaction == null)
				{
					Debug.LogError("Interaction not set correctly for interactable item: " + eventInfo.iitem.itemName);
					continue;
				}
				if (interaction.CheckIfInteractionPossible(eventInfo))
				{
					possibleInteractions.Add(interaction);
				}
			}
			return possibleInteractions;
		}

		private void DestroyMenu(RadialButtonClickEvent radialButtonClickEventInfo = null)
		{
			GameObject.Destroy(spawnedMenu.gameObject);
			(new FlexibleUIEnterExitEvent(
				radialButtonClickEventInfo != null ? radialButtonClickEventInfo.radialButtonUI : null,
				false)).FireEvent();
		}
	}
}