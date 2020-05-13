using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventCallbacks;

namespace MyFolk.UI
{

	[RequireComponent(typeof(CanvasGroup))]
	public class UIPlayMode : MonoBehaviour
	{
		[HideInInspector]
		public CanvasGroup canvasGroup;

		private void Awake()
		{
			this.canvasGroup = GetComponent<CanvasGroup>();
		}

		#region timescale
		public void OnPauseClick()
		{
			(new PauseTimeScaleEvent()).FireEvent();
		}
		public void OnTimeScaleOneClick()
		{
			(new SetTimeScaleEvent(1f)).FireEvent();
		}
		public void OnTimeScaleTwoClick()
		{
			(new SetTimeScaleEvent(2f)).FireEvent();
		}
		public void OnTimeScaleThreeClick()
		{
			(new SetTimeScaleEvent(3f)).FireEvent();
		}
		public void OnTimeScaleFourClick()
		{
			(new SetTimeScaleEvent(4f)).FireEvent();
		}
		#endregion timescale
	}
}