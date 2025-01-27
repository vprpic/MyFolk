﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFolk.UI
{
	[RequireComponent(typeof(CanvasGroup))]
	public class UIMenuMode : MonoBehaviour
	{
		[HideInInspector]
		public CanvasGroup canvasGroup;

		private void Awake()
		{
			this.canvasGroup = GetComponent<CanvasGroup>();
		}
	}
}