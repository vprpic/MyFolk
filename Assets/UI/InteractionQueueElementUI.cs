using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using MyFolk.FlexibleUI;
using EventCallbacks;
using System;

namespace MyFolk.UI
{
	[RequireComponent(typeof(Image))]
	[RequireComponent(typeof(Button))]
	public class InteractionQueueElementUI : FlexibleUIButton
	{
		[HideInInspector]
		public InteractionQueueUI menuParent;
		public Image background;
		public Image icon;
		public string title => interaction.interactionName;
		public float animateSpeed = 8f;
		public InteractableItemClickedEvent interactableItemClickedEventInfo;

		[HideInInspector]
		public Interaction interaction;

		internal void Init(InteractionQueueUI parent, InteractionEnqueueEvent eventInfo)
		{
			this.menuParent = parent;
			this.interactableItemClickedEventInfo = eventInfo.interactableItemClickedEventInfo;
			this.interaction = eventInfo.interaction;
			this.icon.sprite = interactableItemClickedEventInfo.iitem.QueueSprite;
		}

		internal void Init(InteractionQueueUI parent, (Interaction, InteractableItemClickedEvent) interactionInfo)
		{
			this.menuParent = parent;
			this.interactableItemClickedEventInfo = interactionInfo.Item2;
			this.interaction = interactionInfo.Item1;
			this.icon.sprite = interactableItemClickedEventInfo.iitem.QueueSprite;
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
		public void OnElementClick()
		{
			int index = menuParent.DequeueUIElement(this);
			(new FlexibleUIEnterExitEvent(this, false)).FireEvent();
			(new InteractionQueueElementUIClickEvent(this, index)).FireEvent();
		}

	}
}