using EventCallbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFolk
{
	[CreateAssetMenu(menuName = "Interactions/Actions/Look At")]
	public class LookAtAction : ScriptableAction
	{
		public float damping;
		///// <param name="obj">The gameobject the player clicked on</param>
		//public override void PerformAction(GameObject obj, Vector3 worldClickPoint)
		//{
		//	InteractableItem ii = obj.GetComponent<InteractableItem>();
		//	Debug.Log("LookAtScriptableAction - Looking at " + ii.itemName);
		//	Character c = Globals.ins.currentlySelectedCharacter;

		//	Quaternion targetRotation = Quaternion.LookRotation(ii.transform.position - c.transform.position);
		//	c.transform.localRotation = targetRotation;
		//}
		public override bool CheckIfPossible(InteractableItemClickedEventInfo eventInfo, ActionCanceled actionCanceled)
		{
			return true;
		}

		public override void StartAction(InteractableItemClickedEventInfo eventInfo, StartActionOver startActionOver, ActionCanceled actionCanceled)
		{
			Debug.Log("Starting to look at item");
			startActionOver.Invoke();
		}
		public override void PerformAction(InteractableItemClickedEventInfo eventInfo, PerformActionOver performActionOver, ActionCanceled actionCanceled)
		{
			Vector3 target = eventInfo.iitem.gameObject.transform.position;
			Character character = Globals.ins.currentlySelectedCharacter;

			Vector3 targetPostition = new Vector3(target.x,
										character.gameObject.transform.position.y,
										target.z);
			character.gameObject.transform.LookAt(targetPostition);
			float angle = Vector3.Angle(target, character.gameObject.transform.forward);
			Debug.Log("angle: " + angle);
			if (Vector3.Angle(target, character.gameObject.transform.forward) < 1)
			{
				performActionOver.Invoke();
			}
		}

		public override void EndAction(InteractableItemClickedEventInfo eventInfo, EndActionOver endActionOver, ActionCanceled actionCanceled)
		{
			endActionOver.Invoke();
		}
	}
}