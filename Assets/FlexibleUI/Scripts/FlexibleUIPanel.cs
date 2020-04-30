using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyFolk.FlexibleUI
{
	public class FlexibleUIPanel : FlexibleUI
	{

		[HideInInspector]
		public Image panelBackground;

		public override void Awake()
		{
			if (panelBackground == null)
			{
				panelBackground = GetComponent<Image>();
			}
			base.Awake();
		}

		protected override void OnSkinUI()
		{
			panelBackground.sprite = skinData.panelBackground;
			panelBackground.type = Image.Type.Sliced;
			panelBackground.color = skinData.defaultPanelColor;
		}

	}
}