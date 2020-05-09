using EventCallbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFolk
{
	public class LookAtStateData : ActionStateData
	{
		public Quaternion firstCharacterRotation;
		public Vector3 target;
		public float timer;

		public LookAtStateData(InteractableItemClickedEvent eventInfo, Vector3 target) : base(eventInfo)
		{
			this.target = target;
		}

		//public override void ResetValues()
		//{
		//	base.ResetValues();
		//	this.target = Vector3.zero;
		//	this.timer = 0f;
		//}
	}


	[CreateAssetMenu(menuName = "Actions/Look At", fileName = "LookAt_Action")]
	public class LookAtAction : ScriptableAction
	{
		public float lookSpeed;
		public float maxInteractionTime;
		
		public override bool EarlyCheckIfPossible(InteractableItemClickedEvent eventInfo)
		{
			return true;
		}

		public override bool LateCheckIfPossible(ActionStateData actionStateData)
		{
			return true;
		}

		public override void StartAction(InteractableItemClickedEvent eventInfo, ReturnCurrentInteractionState returnCurrentInteractionState, StartActionOver startActionOver, ActionCanceled actionCanceled)
		{
			Vector3 target = eventInfo.iitem.gameObject.transform.position;
			LookAtStateData asd = new LookAtStateData(eventInfo, target);
			if (!LateCheckIfPossible(asd))
			{
				CancelAction(asd, actionCanceled);
				return;
			}
			//Vector3 target = new Vector3(eventInfo.character.gameObject.transform.forward.x, 
				//eventInfo.iitem.gameObject.transform.position.y, eventInfo.character.gameObject.transform.forward.z);
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
				CancelAction(actionStateData, actionCanceled);
				return;
			}

			Vector3 tempTarget = asd.target - asd.eventInfo.character.gameObject.transform.position;
			tempTarget.y = 0;

			Quaternion lookRotation = Quaternion.LookRotation((tempTarget).normalized);
			asd.timer += UnityEngine.Time.deltaTime * Globals.ins.timeManager.currentTimeScale * lookSpeed;
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