using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LookAtCommand : ICommand
{
	string itemName;

	public LookAtCommand(string itemName)
	{
		this.itemName = itemName;
	}

	public void Execute()
	{
		Debug.Log("I'm looking at '" + itemName + "'");
	}
}
