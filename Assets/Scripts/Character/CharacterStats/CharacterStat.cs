using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace MyFolk
{
	//https://www.youtube.com/watch?v=SH25f3cXBVc
	[System.Serializable]
	public class CharacterStat
	{
		public float baseValue;
		public string charName;

		public virtual float Value
		{
			get
			{
				if (isDirty || baseValue != lastBaseValue)
				{
					lastBaseValue = baseValue;
					_value = CalculateFinalValue();
					isDirty = false;
				}
				return _value;
			}
		}

		protected bool isDirty = true;
		protected float _value;
		protected float lastBaseValue = float.MinValue;

		protected readonly List<StatModifier> _statModifiers;
		public readonly ReadOnlyCollection<StatModifier> statModifiers;

		public CharacterStat()
		{
			_statModifiers = new List<StatModifier>();
			statModifiers = _statModifiers.AsReadOnly();
		}

		public CharacterStat(float baseValue) : this()
		{
			this.baseValue = baseValue;
		}
		public CharacterStat(string name) : this()
		{
			this.charName = name;
		}

		public virtual void AddModifier(StatModifier mod)
		{
			isDirty = true;
			_statModifiers.Add(mod);
			_statModifiers.Sort(CompareModifierOrder);
		}

		protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
		{
			if (a.order < b.order)
				return -1;
			else if (a.order > b.order)
				return 1;
			else return 0;
		}

		public virtual bool RemoveModifier(StatModifier mod)
		{
			if (_statModifiers.Remove(mod))
			{
				isDirty = true;
				return true;
			}
			return false;
		}

		public virtual bool RemoveAllModifiersFromSource(object source)
		{
			bool didRemove = false;

			for (int i = _statModifiers.Count - 1; i >= 0; i--)
			{
				if (_statModifiers[i].source == source)
				{
					didRemove = true;
					isDirty = true;
					_statModifiers.RemoveAt(i);
				}
			}

			return didRemove;
		}

		protected virtual float CalculateFinalValue()
		{
			float finalValue = this.baseValue;
			float sumPercentAdd = 0;
			for (int i = 0; i < _statModifiers.Count; i++)
			{
				StatModifier mod = _statModifiers[i];

				switch (mod.type)
				{
					case StatModType.Flat:
						finalValue += mod.value;
						break;
					case StatModType.PercentAdd:
						sumPercentAdd += mod.value;
						if (i + 1 >= _statModifiers.Count || _statModifiers[i + 1].type != StatModType.PercentAdd)
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
}