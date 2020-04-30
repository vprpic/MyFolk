using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MyFolk.FlexibleUI { 
	public class NeedUI : FlexibleUI
	{
		public Need.NeedType type;
		public Need need;
		[HideInInspector]
		public TextMeshProUGUI nameUI;
		[HideInInspector]
		public Slider slider;
		public Image sliderFill;

		public override void Awake()
		{
			if(nameUI == null)
			{
				nameUI = GetComponentInChildren<TextMeshProUGUI>();
			}
			if(slider == null)
			{
				slider = GetComponentInChildren<Slider>();
			}
			if(sliderFill == null)
			{
				sliderFill = slider.transform.Find("Fill").gameObject.GetComponent<Image>();
			}
			base.Awake();
		}

		public void SetNewNeed(Need need)
		{
			this.need = need;
			this.slider.minValue = need.minValue;
			this.slider.maxValue = need.maxValue;
			this.slider.value = need.currentValue;
			SetSliderColor();
		}

		public void UpdateNeedUI()
		{
			this.slider.value = need.currentValue;
			SetSliderColor();
		}

		private void SetSliderColor()
		{
			float third = need.maxValue / 3f;
			if (need.currentValue >= 2 * third)
			{
				this.sliderFill.color = skinData.thirdThirdBarColor;
			}
			else if (need.currentValue >= third)
			{
				this.sliderFill.color = skinData.secondThirdBarColor;
			}
			else
			{
				this.sliderFill.color = skinData.firstThirdBarColor;
			}
		}

		protected override void OnSkinUI()
		{
			if (this.need != null)
				UpdateNeedUI();


			base.OnSkinUI();
		}
	}
}