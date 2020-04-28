using EventCallbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFolk.UI
{
	public class UIInputHandler : MonoBehaviour
	{
		public bool isHovering;

		public void OnEnterExitUI(FlexibleUIEnterExitEventInfo eventInfo)
		{
			this.isHovering = eventInfo.isHovering;
			Debug.Log("OnEnterExitUI: " + isHovering.ToString());
		}

		private void Start()
		{
			EventSystem.Current.RegisterListener<FlexibleUIEnterExitEventInfo>(OnEnterExitUI);
		}

	}
}