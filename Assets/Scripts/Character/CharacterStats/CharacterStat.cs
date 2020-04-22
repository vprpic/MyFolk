using System;
using System.Collections.Generic;

//https://www.youtube.com/watch?v=SH25f3cXBVc
public class CharacterStat
{
	public float baseValue;

	public float Value
	{
		get
		{
			if (isDirty)
			{
				_value = CalculateFinalValue();
				isDirty = false;
			}
			return _value;
		}
	}

	private bool isDirty = true;
	private float _value;

	private readonly List<StatModifier> statModifiers;

	public CharacterStat(float baseValue)
	{
		this.baseValue = baseValue;
		statModifiers = new List<StatModifier>();
	}

	public void AddModifier(StatModifier mod)
	{
		isDirty = true;
		statModifiers.Add(mod);
		statModifiers.Sort(CompareModifierOrder);
	}

	private int CompareModifierOrder(StatModifier a, StatModifier b)
	{
		if (a.order < b.order)
			return -1;
		else if (a.order > b.order)
			return 1;
		else return 0;
	}

	public bool RemoveModifier(StatModifier mod)
	{
		isDirty = true;
		return statModifiers.Remove(mod);
	}

	private float CalculateFinalValue()
	{
		float finalValue = this.baseValue;
		float sumPercentAdd = 0;
		for (int i = 0; i < statModifiers.Count; i++)
		{
			StatModifier mod = statModifiers[i];

			switch (mod.type)
			{
				case StatModType.Flat:
					finalValue += mod.value;
					break;
				case StatModType.PercentAdd:
					sumPercentAdd += mod.value;
					if(i+1 >= statModifiers.Count || statModifiers[i+1].type != StatModType.PercentAdd)
					{
						finalValue *= 1 + sumPercentAdd;
					}
					break;
				case StatModType.PercentMult:
					finalValue *= 1 + mod.value; //mod.value == 0.1 -> final value is 110%
					break;
				default:
					break;
			}
		}

		return (float)Math.Round(finalValue, 4);
	}
}
