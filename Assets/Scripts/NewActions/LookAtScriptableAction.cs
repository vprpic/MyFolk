using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtScriptableAction : ScriptableAction
{
	public Vector3 worldPoint;

	public override void PerformAction(GameObject obj)
	{
		Debug.Log("LookAtScriptableAction - Looking at " + obj.name);
	}

	public override void CheckIfPossible(GameObject obj)
	{
		throw new System.NotImplementedException();
	}
}
