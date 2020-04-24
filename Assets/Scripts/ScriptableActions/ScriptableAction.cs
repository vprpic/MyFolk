using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ScriptableAction : ScriptableObject
{
	public string actionName;
	public Sprite sprite;
	//TODO:
	//public float attenuation;
	//Attenuation reduces the attractiveness of an interaction over a distance. The higher the attenuation the closer the Sim will have
	//to be before he feels like using the object.

	#region Flags
	[SerializeField]
	private List<Flag> flags;

	public bool ContainsFlag(Flag flag)
	{
		foreach (Flag f in flags)
		{
			if (f.value.Equals(flag.value))
			{
				return true;
			}
		}
		return false;
	}

	//TODO: test
	public bool ContainsAllFlags(List<Flag> flagList)
	{
		bool contained = true;
		foreach (Flag flag1 in flagList)
		{
			bool currentContained = false;
			foreach (Flag flag2 in flags)
			{
				if (flag2.value.Equals(flag1.value))
				{
					currentContained = true;
					continue;
				}
			}
			if (!currentContained)
			{
				contained = false;
			}
		}
		return contained;
	}

	#endregion Flags

	#region Advertisement

	//TODO: when AI implementation

	#endregion Advertisement

	public abstract void PerformAction(GameObject obj, Vector3 worldClickPoint);
	public abstract bool CheckIfPossible(GameObject obj);
}
