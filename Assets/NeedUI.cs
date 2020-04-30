using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MyFolk.UI {
	public class NeedUI : MonoBehaviour
	{
		public Need need;
		public Need.NeedType type;
		public TextMeshProUGUI nameUI;
		public Slider slider;

		public void SetNewNeed(Need need)
		{
			this.need = need;
			this.slider.minValue = need.minValue;
			this.slider.maxValue = need.maxValue;
			this.slider.value = need.currentValue;
		}

		public void UpdateNeedUI()
		{
			this.slider.value = need.currentValue;
		}
	}
}