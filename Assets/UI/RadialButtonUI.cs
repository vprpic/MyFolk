using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using MyFolk.FlexibleUI;
using EventCallbacks;

namespace MyFolk.UI
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

		[HideInInspector]
		public Interaction buttonInteraction;

		public void Init(RadialMenu parent, InteractableItemClickedEventInfo eventInfo, Interaction interaction)
		{
			this.interactableItemEventInfo = eventInfo;
			this.buttonInteraction = interaction;

			this.icon.sprite = interaction.radialButtonSprite;
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

		/// <summary>
		/// Referenced in editor
		/// </summary>
		public void OnRadialButtonClick()
		{
			Globals.ins.currentlySelectedCharacter.interactionQueue.EnqueueInteraction(this.buttonInteraction, this.interactableItemEventInfo);
			EventCallbacks.EventSystem.Current.FireEvent(new RadialButtonClickEventInfo(this));
		}

	}
}