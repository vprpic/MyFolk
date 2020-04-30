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
			float r, g, b, curr, perc;
			float sixth = need.maxValue / 6f;
			float max = 2 * sixth;
			if (need.currentValue >= 5 * sixth)
			{
				this.sliderFill.color = skinData.thirdThirdBarColor;
			}
			else if (need.currentValue >= 3 * sixth)
			{
				curr = need.currentValue - 3 * sixth;
				perc = curr / max;
				r = Mathf.Lerp(skinData.secondThirdBarColor.r, skinData.thirdThirdBarColor.r, perc);
				g = Mathf.Lerp(skinData.secondThirdBarColor.g, skinData.thirdThirdBarColor.g, perc);
				b = Mathf.Lerp(skinData.secondThirdBarColor.b, skinData.thirdThirdBarColor.b, perc);
				this.sliderFill.color = new Color(r, g, b);
			}
			else if(need.currentValue >= sixth)
			{
				curr = need.currentValue - sixth;
				perc = curr / max;
				r = Mathf.Lerp(skinData.firstThirdBarColor.r, skinData.secondThirdBarColor.r, perc);
				g = Mathf.Lerp(skinData.firstThirdBarColor.g, skinData.secondThirdBarColor.g, perc);
				b = Mathf.Lerp(skinData.firstThirdBarColor.b, skinData.secondThirdBarColor.b, perc);
				this.sliderFill.color = new Color(r, g, b);
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