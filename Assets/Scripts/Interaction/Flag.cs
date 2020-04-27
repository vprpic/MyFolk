using System.Collections;
using System.Collections.Generic;

//https://simswiki.info/wiki.php?title=TTAB
public class Flag
{
	public enum FlagType
	{
		Visitor,
		Immediately,	//Use this if you want the action associated with the menu item to be run immediately rather than put in your Sim's queue.
		Consecutive,	//The Sim can choose to do this several times in a row.
		Children,
		DemoChildren,
		Adults,
		DebugMenu,		//Items can be marked as being on the debug menu. This means they only show up if two other things are done. 
						//Firstly in the cheats window use "boolProp testingCheatsEnabled true" and then press <Shift> when clicking
						//on an object to bring up its Pie Menu. Then you will see the debug items instead of the normal items.
		AutoFirst,
		Toddlers,
		Elders,
		Joinable,		//This means while one Sim is doing this interaction, another Sim can join them.
		Teens,
		AllowNested,	//Either another interaction can be nested within this, or it can be nested
		Nest,			//Either this interaction should be nested, or it can be nested
		Babies,
		TwoWay,
		AdultBigDogs,
		AdultCats,
		Puppies,
		Kittens,
		ElderBigDogs,
		ElderCats,
		AdultSmallDogs,
		ElderSmallDogs
	}

	public static readonly List<Flag> allFlags = new List<Flag>()
	{
		new Flag(FlagType.Visitor),
		new Flag(FlagType.Children),
		new Flag(FlagType.Adults),
		new Flag(FlagType.DebugMenu),
		new Flag(FlagType.Elders),
		new Flag(FlagType.Teens),
		new Flag(FlagType.Babies)
	};
	public readonly FlagType value;

	private Flag(FlagType value)
	{
		this.value = value;
	}

	public static Flag GetFlag(FlagType type)
	{
		foreach (Flag flag in allFlags)
		{
			if (flag.value.Equals(type))
				return flag;
		}
		return null;
	}
}

/*
	Flag f = Flag.GetFlag(FlagType.Children);
	if(f == null)
	{
		//log error
	}
 */
