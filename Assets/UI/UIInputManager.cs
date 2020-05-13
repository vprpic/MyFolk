using EventCallbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFolk.UI
{
	public class UIInputManager : MonoBehaviour
	{
		public static bool isHovering;
		public static bool isHoveringOverRadialMenuButton;

		public void OnEnterExitUI(FlexibleUIEnterExitEvent eventInfo)
		{
			isHovering = eventInfo.isHovering;
			if (isHovering)
				isHoveringOverRadialMenuButton = eventInfo.flexibleUI is RadialButtonUI;
			else
				isHoveringOverRadialMenuButton = false;
		}

		private void Start()
		{
			FlexibleUIEnterExitEvent.RegisterListener(OnEnterExitUI);
		}

	}
}