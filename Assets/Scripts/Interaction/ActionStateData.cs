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
}