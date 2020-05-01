using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventCallbacks;

namespace MyFolk
{

	public class ActionStateData
	{
		public EventCallbacks.InteractableItemClickedEvent eventInfo;
		public ActionStateData() { }

		public ActionStateData(EventCallbacks.InteractableItemClickedEvent eventInfo)
		{
			this.eventInfo = eventInfo;
		}

		//public virtual void ResetValues()
		//{
		//	eventInfo = null;
		//}
	}

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

	public class EatFoodStateData : ActionStateData
	{
		public float currentFoodAmountAdded;
		public float timer;

		public EatFoodStateData(InteractableItemClickedEvent eventInfo) : base(eventInfo)
		{
		}

		//public override void ResetValues()
		//{
		//	base.ResetValues();
		//	this.timer = 0f;
		//	this.currentFoodAmountAdded = 0f;
		//}
	}
}