﻿public enum StatModType
{
	Flat = 100,
	PercentAdd = 200,
	PercentMult = 300//mod.value == 0.1 -> final value is 110%
}


//https://www.youtube.com/watch?v=SH25f3cXBVc
public class StatModifier
{
	public readonly float value;  //if percent type:  mod.value == 0.1 -> final value is 110%
	public readonly StatModType type;
	public readonly int order;
	public readonly object source;

	public StatModifier(float value, StatModType type, int order, object source)
	{
		this.source = source;
		this.value = value;
		this.type = type;
		this.order = order;
	}

	public StatModifier(float value, StatModType type) : this(value, type, (int)type, null) { }
	public StatModifier(float value, StatModType type, int order) : this(value, type, order, null) { }
	public StatModifier(float value, StatModType type, object source) : this(value, type, (int)type, source) { }
}
