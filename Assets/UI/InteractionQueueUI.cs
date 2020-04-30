using EventCallbacks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFolk.UI
{
	public class InteractionQueueUI : MonoBehaviour
	{
		public InteractionQueueElementUI interactionQueueElementUIPrefab;
		public List<InteractionQueueElementUI> UIElements;
		public InteractionQueueElementUI currentElement => UIElements[0];

		public void Awake()
		{
			EventCallbacks.EventSystem.Current.RegisterListener<CharacterSelectedEventInfo>(OnCharacterSelected);
			EventCallbacks.EventSystem.Current.RegisterListener<InteractionEnqueueEventInfo>(OnEnqueuedInteraction);
			EventCallbacks.EventSystem.Current.RegisterListener<InteractionDequeueEventInfo>(OnDequeuedInteraction);
		}

		public void OnCharacterSelected(CharacterSelectedEventInfo characterSelectedEventInfo)
		{
			EnqueueElements(characterSelectedEventInfo.newCharacter.interactionQueue.interactionQueue);
		}

		private void EnqueueElements(List<(Interaction, InteractableItemClickedEventInfo)> currentQueue)
		{
			for (int i = this.transform.childCount - 1; i >= 0; i--)
			{

				GameObject.Destroy(this.transform.GetChild(i).gameObject);
			}

			foreach (var item in currentQueue)
			{
				EnqueueElement(item);
			}
		}

		public void OnEnqueuedInteraction(InteractionEnqueueEventInfo interactionQueueEnqueuedEventInfo)
		{
			EnqueueElement(interactionQueueEnqueuedEventInfo);
		}
		public void OnDequeuedInteraction(InteractionDequeueEventInfo interactionDequeueEventInfo)
		{
			DequeueElement(interactionDequeueEventInfo);
		}

		public void EnqueueElement((Interaction, InteractableItemClickedEventInfo) interaction)
		{
			InteractionQueueElementUI newElement = Instantiate(interactionQueueElementUIPrefab) as InteractionQueueElementUI;
			newElement.transform.SetParent(transform, false);

			UIElements.Add(newElement);

			newElement.Init(this, interaction);
			newElement.Animate();
		}

		public void EnqueueElement(InteractionEnqueueEventInfo interaction)
		{
			EnqueueElement((interaction.interaction,interaction.interactableItemClickedEventInfo));
		}

		internal int GetQueueIndex(InteractionQueueElementUI interactionQueueElementUI)
		{
			//int i = UIElements.FindIndex(a => a == interactionQueueElementUI);
			//Debug.Log("index: " + i);
			//return i;
			return UIElements.IndexOf(interactionQueueElementUI);
		}

		public void DequeueElement((Interaction, InteractableItemClickedEventInfo) interaction)
		{
			foreach (var item in UIElements)
			{
				if(item.interaction.Equals(interaction.Item1) && item.interactableItemClickedEventInfo.Equals(interaction.Item2))
				{
					UIElements.Remove(UIElements.Find(a => a.interaction.Equals(interaction.Item1) && a.interactableItemClickedEventInfo.Equals(interaction.Item2)));
					if(item != null)
						GameObject.Destroy(item.gameObject);
					return;
				}
			}
		}

		internal int DequeueElement(InteractionQueueElementUI interactionQueueElementUI)
		{
			if (UIElements == null || UIElements.Count < 1)
				return -1;
			int i = GetQueueIndex(interactionQueueElementUI);
			UIElements.RemoveAt(i);
			GameObject.Destroy(interactionQueueElementUI.gameObject);
			return i;
		}

		public void DequeueElement(InteractionDequeueEventInfo interaction)
		{
			DequeueElement((interaction.interaction, interaction.interactableItemClickedEventInfo));
		}
	}
}