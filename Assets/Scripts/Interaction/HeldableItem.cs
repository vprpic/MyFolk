using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFolk
{
	[System.Serializable]
	[RequireComponent(typeof(InteractableItem))]
	public class HeldableItem : MonoBehaviour
	{
		public Interaction pickUpInteraction;
		public Interaction putDownInteraction;

		private void Start()
		{
			InteractableItem ii = gameObject.GetComponent<InteractableItem>();
			if (ii.data != null)
			{
				this.pickUpInteraction = Globals.ins.data.pickUpInteraction;
				this.putDownInteraction = Globals.ins.data.putDownInteraction;
				if (this.pickUpInteraction != null)
				{
					ii.AddInteraction(this.pickUpInteraction);
					Debug.Log("added interaction");
				}
				else
					Debug.Log("Pick up interaction not set");
			}
			else
			{
				Debug.Log("ii.data == null");
			}
		}

		public HeldableItem PickUpItem()
		{
			this.gameObject.SetActive(false);
			return this;
		}

		public HeldableItem PutItemDown(Vector3 newPosition)
		{
			this.transform.position = newPosition;
			this.gameObject.SetActive(true);
			return this;
		}
	}
}
