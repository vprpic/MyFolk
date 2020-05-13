using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventCallbacks;

namespace MyFolk.UI
{
	[RequireComponent(typeof(CanvasGroup))]
	public class UIBuildMode : MonoBehaviour
	{
		[HideInInspector]
		public CanvasGroup canvasGroup;

		private void Awake()
		{
			this.canvasGroup = GetComponent<CanvasGroup>();
		}

		#region buttons
		public void OnNoneToolClick()
		{
			(new SetBuildToolEvent(Building.BuildTool.None)).FireEvent();
		}
		public void OnStraightWallClick()
		{
			(new SetBuildToolEvent(Building.BuildTool.StraightWall)).FireEvent();
		}
		public void OnCurvedWallClick()
		{
			(new SetBuildToolEvent(Building.BuildTool.CurvedWall)).FireEvent();
		}
		#endregion buttons
	}
}
