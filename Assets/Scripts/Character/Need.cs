using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventCallbacks;

namespace MyFolk {
	[System.Serializable]
	public class Need
	{
		public enum NeedType
		{
			Hunger,
			Fun,
			Social,
			Hygiene,
			Energy,
			Bladder,
			Fulfillment,
			Health,
			Comfort
		}

		//public Character owner;
		public float minValue;
		public float maxValue;
		public NeedType type;

		public float currentValue;

		public Need(NeedType type, float min, float max)
		{
			this.type = type;
			this.minValue = min;
			this.maxValue = max;

			this.currentValue = max;
		}

		public float AddToCurrentValue(float amount, bool ownerIsCurrentlySelected = false)
		{
			float newValue = Mathf.Clamp(this.currentValue + amount, this.minValue, this.maxValue);
			float amountChanged = Mathf.Abs(newValue - this.currentValue);
			amountChanged *= amount < 0 ? -1 : 1;
			this.currentValue = newValue;
			if (ownerIsCurrentlySelected && amountChanged != 0f)
			{
				//EventSystem.Current.FireEvent(new CurrentCharacterNeedChangedEvent(this, amountChanged));
				(new CurrentCharacterNeedChangedEvent(this, amountChanged)).FireEvent();
			}
			return this.currentValue;
		}
	}
}