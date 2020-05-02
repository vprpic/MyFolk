using EventCallbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFolk
{
	[CreateAssetMenu(menuName = "Actions/Look At", fileName = "LookAt_Action")]
	public class LookAtAction : ScriptableAction
	{
		public float lookSpeed;
		public float maxInteractionTime;
		
		public override bool CheckIfPossible(InteractableItemClickedEvent eventInfo)
		{
			return true;
		}

		public override void StartAction(InteractableItemClickedEvent eventInfo, ReturnCurrentInteractionState returnCurrentInteractionState, 
			StartActionOver startActionOver, ActionCanceled actionCanceled)
		{
			Vector3 target = eventInfo.iitem.gameObject.transform.position;
			//Vector3 target = new Vector3(eventInfo.character.gameObject.transform.forward.x, 
				//eventInfo.iitem.gameObject.transform.position.y, eventInfo.character.gameObject.transform.forward.z);
			LookAtStateData asd = new LookAtStateData(eventInfo, target);
			asd.firstCharacterRotation = eventInfo.character.transform.rotation;
			//Debug.Log("Starting to look at item");
			returnCurrentInteractionState(asd);
			startActionOver();
		}


		public override void PerformAction(ActionStateData actionStateData, ReturnCurrentInteractionState returnCurrentInteractionState, 
			PerformActionOver performActionOver, ActionCanceled actionCanceled)
		{
			LookAtStateData asd = (LookAtStateData)actionStateData;
			if (asd == null)
			{
				Debug.LogError("LookAtStateData not found in actionStateData");
				actionCanceled();
				return;
			}

			Vector3 tempTarget = asd.target - asd.eventInfo.character.gameObject.transform.position;
			tempTarget.y = 0;

			Quaternion lookRotation = Quaternion.LookRotation((tempTarget).normalized);
			asd.timer += Time.deltaTime * lookSpeed;
			float tempPerc = asd.timer / maxInteractionTime;
			asd.eventInfo.character.gameObject.transform.rotation = Quaternion.Slerp(asd.firstCharacterRotation, lookRotation, tempPerc);

			if (tempPerc >= 1)
			{

				performActionOver();
			}
			returnCurrentInteractionState(asd);
		}

		public override void EndAction(ActionStateData actionStateData, EndActionOver endActionOver, ActionCanceled actionCanceled)
		{
			endActionOver();
		}

		public override void CancelAction(ActionStateData actionStateData,  ActionCanceled actionCanceled)
		{
			actionCanceled();
		}
	}
}