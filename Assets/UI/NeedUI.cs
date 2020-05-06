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
				r = skinData.thirdThirdBarColor.r;
				g = skinData.thirdThirdBarColor.g;
				b = skinData.thirdThirdBarColor.b;
			}
			else if (need.currentValue >= 3 * sixth)
			{
				curr = need.currentValue - 3 * sixth;
				perc = curr / max;
				r = Mathf.Lerp(skinData.secondThirdBarColor.r, skinData.thirdThirdBarColor.r, perc);
				g = Mathf.Lerp(skinData.secondThirdBarColor.g, skinData.thirdThirdBarColor.g, perc);
				b = Mathf.Lerp(skinData.secondThirdBarColor.b, skinData.thirdThirdBarColor.b, perc);
			}
			else if(need.currentValue >= sixth)
			{
				curr = need.currentValue - sixth;
				perc = curr / max;
				r = Mathf.Lerp(skinData.firstThirdBarColor.r, skinData.secondThirdBarColor.r, perc);
				g = Mathf.Lerp(skinData.firstThirdBarColor.g, skinData.secondThirdBarColor.g, perc);
				b = Mathf.Lerp(skinData.firstThirdBarColor.b, skinData.secondThirdBarColor.b, perc);
			}
			else
			{
				r = skinData.firstThirdBarColor.r;
				g = skinData.firstThirdBarColor.g;
				b = skinData.firstThirdBarColor.b;
			}
			float tempPerc = (need.currentValue % 0.5f)*2f;
			r -= 0.1f;
			g -= 0.1f;
			b -= 0.1f;
			float tempColor = Mathf.Clamp01(r + 0.2f);
			r = Mathf.Lerp(r, tempColor, tempPerc);
			tempColor = Mathf.Clamp01(g + 0.2f);
			g = Mathf.Lerp(g, tempColor, tempPerc);
			tempColor = Mathf.Clamp01(b + 0.2f);
			b = Mathf.Lerp(b, tempColor, tempPerc);

			this.sliderFill.color = new Color(r, g, b);
		}

		protected override void OnSkinUI()
		{
			if (this.need != null)
				UpdateNeedUI();


			base.OnSkinUI();
		}
	}
}