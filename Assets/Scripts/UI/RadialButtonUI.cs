using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using MyFolk.FlexibleUI;
using EventCallbacks;

namespace MyFolk
{
	[RequireComponent(typeof(Image))]
	[RequireComponent(typeof(Button))]
	public class RadialButtonUI : FlexibleUIButton
	{
		[HideInInspector]
		public RadialMenu menuParent;
		public Image background;
		public Image icon;
		public TextMeshProUGUI text;
		public string title;
		public float animateSpeed = 8f;
		public InteractableItemClickedEventInfo interactableItemEventInfo;

		//used in the radialMenuSpawner to disable the menu
		public VoidEvent onRadialButtonClick;
		[HideInInspector]
		public Interaction buttonInteraction;//buttonAction;

		public void Init(RadialMenu parent, InteractableItemClickedEventInfo eventInfo, Interaction interaction)
		{
			this.interactableItemEventInfo = eventInfo;
			this.buttonInteraction = interaction;
			//this.buttonAction = action;

			this.icon.sprite = interaction.sprite; //buttonAction.sprite;
			this.title = interaction.interactionName;
			this.text.SetText(interaction.interactionName);
			this.menuParent = parent;
		}

		public void Animate()
		{
			StartCoroutine(AnimateButtonIn());
		}

		IEnumerator AnimateButtonIn()
		{
			transform.localScale = Vector3.zero;
			float timer = 0f;
			while (timer < 1 / animateSpeed)
			{
				timer += Time.deltaTime;
				transform.localScale = Vector3.one * timer * animateSpeed;
				yield return null;
			}
			transform.localScale = Vector3.one;
		}

		public void OnRadialButtonClick()
		{
			//TODO(interaction)
			Globals.ins.currentlySelectedCharacter.interactionQueue.EnqueueInteraction(this.buttonInteraction, this.interactableItemEventInfo);
			//buttonAction.PerformAction(interactableItemEventInfo.iitem.gameObject, interactableItemEventInfo.worldClickPoint);
			onRadialButtonClick.Raise();
		}

	}
}