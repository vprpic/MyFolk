public enum SkillModType
{
	Flat = 100,
	PercentAdd = 200,
	PercentMult = 300//mod.value == 0.1 -> final value is 110%
}


//https://www.youtube.com/watch?v=SH25f3cXBVc
public class SkillModifier
{
	public readonly float value;  //if percent type:  mod.value == 0.1 -> final value is 110%
	public readonly SkillModType type;
	public readonly int order;
	public readonly object source;

	public SkillModifier(float value, SkillModType type, int order, object source)
	{
		this.source = source;
		this.value = value;
		this.type = type;
		this.order = order;
	}

	public SkillModifier(float value, SkillModType type) : this(value, type, (int)type, null) { }
	public SkillModifier(float value, SkillModType type, int order) : this(value, type, order, null) { }
	public SkillModifier(float value, SkillModType type, object source) : this(value, type, (int)type, source) { }
}
