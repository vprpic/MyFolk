using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace MyFolk
{
	//https://www.youtube.com/watch?v=SH25f3cXBVc
	[System.Serializable]
	public class CharacterSkill
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

		protected readonly List<SkillModifier> _statModifiers;
		public readonly ReadOnlyCollection<SkillModifier> statModifiers;

		public CharacterSkill()
		{
			_statModifiers = new List<SkillModifier>();
			statModifiers = _statModifiers.AsReadOnly();
		}

		public CharacterSkill(float baseValue) : this()
		{
			this.baseValue = baseValue;
		}
		public CharacterSkill(string name) : this()
		{
			this.charName = name;
		}

		public virtual void AddModifier(SkillModifier mod)
		{
			isDirty = true;
			_statModifiers.Add(mod);
			_statModifiers.Sort(CompareModifierOrder);
		}

		protected virtual int CompareModifierOrder(SkillModifier a, SkillModifier b)
		{
			if (a.order < b.order)
				return -1;
			else if (a.order > b.order)
				return 1;
			else return 0;
		}

		public virtual bool RemoveModifier(SkillModifier mod)
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
				SkillModifier mod = _statModifiers[i];

				switch (mod.type)
				{
					case SkillModType.Flat:
						finalValue += mod.value;
						break;
					case SkillModType.PercentAdd:
						sumPercentAdd += mod.value;
						if (i + 1 >= _statModifiers.Count || _statModifiers[i + 1].type != SkillModType.PercentAdd)
						{
							finalValue *= 1 + sumPercentAdd;
						}
						break;
					case SkillModType.PercentMult:
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