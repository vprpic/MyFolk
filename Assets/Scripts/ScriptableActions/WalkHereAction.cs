using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SriptableAction/Walk Here")]
public class WalkHereAction : ScriptableAction
{
	/// <param name="obj">The gameobject the player clicked on</param>
	public override void PerformAction(GameObject obj, Vector3 worldClickPoint)
	{
		Globals.ins.currentlySelectedCharacter.navMeshAgent.SetDestination(worldClickPoint);
	}

	public override bool CheckIfPossible(GameObject obj)
	{
		throw new System.NotImplementedException();
	}
}
