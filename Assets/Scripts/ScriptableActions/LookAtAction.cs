using EventCallbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFolk
{
	[CreateAssetMenu(menuName = "Interactions/Actions/Look At", fileName = "LookAt_Action")]
	public class LookAtAction : ScriptableAction
	{
		public float lookSpeed;
		private float currentRotationPercentage;
		private Quaternion prevQuaternion;
		
		public override bool CheckIfPossible(InteractableItemClickedEventInfo eventInfo, ActionCanceled actionCanceled)
		{
			return true;
		}

		public override void StartAction(InteractableItemClickedEventInfo eventInfo, StartActionOver startActionOver, ActionCanceled actionCanceled)
		{
			currentRotationPercentage = 0f;
			prevQuaternion = Quaternion.identity;
			Debug.Log("Starting to look at item");
			startActionOver.Invoke();
		}
		public override void PerformAction(InteractableItemClickedEventInfo eventInfo, PerformActionOver performActionOver, ActionCanceled actionCanceled)
		{
			Vector3 target = eventInfo.iitem.gameObject.transform.position;
			Character character = Globals.ins.currentlySelectedCharacter;
			Vector3 temp = target - character.gameObject.transform.position;
			temp.y = 0;

			Quaternion lookRotation = Quaternion.LookRotation((temp).normalized);
			Debug.Log("look rotation: " + lookRotation);
			//over time
			character.gameObject.transform.rotation = Quaternion.Slerp(character.gameObject.transform.rotation, lookRotation, currentRotationPercentage);
			currentRotationPercentage += Time.deltaTime * lookSpeed;
			Debug.Log("currentRotationPercentage " + currentRotationPercentage);
			if(prevQuaternion == lookRotation && currentRotationPercentage > 0.5f)
			{
				prevQuaternion = Quaternion.identity;
				currentRotationPercentage = 0f;
				performActionOver.Invoke();
			}
			prevQuaternion = lookRotation;
		}

		public override void EndAction(InteractableItemClickedEventInfo eventInfo, EndActionOver endActionOver, ActionCanceled actionCanceled)
		{
			prevQuaternion = Quaternion.identity;
			currentRotationPercentage = 0f;
			endActionOver.Invoke();
		}
	}
}