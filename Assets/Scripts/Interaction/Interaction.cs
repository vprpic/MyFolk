using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Interaction : ScriptableAction
{
	public List<Interaction> interactions;
	public int currentInteraction;

	public void Init()
	{
		interactions = new List<Interaction>();
		currentInteraction = 0;
	}


}
