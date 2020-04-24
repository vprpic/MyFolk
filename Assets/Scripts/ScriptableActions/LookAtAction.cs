using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SriptableAction/Look At")]
public class LookAtAction : ScriptableAction
{
	/// <param name="obj">The gameobject the player clicked on</param>
	public override void PerformAction(GameObject obj, Vector3 worldClickPoint)
	{
		InteractableItem ii = obj.GetComponent<InteractableItem>();
		Debug.Log("LookAtScriptableAction - Looking at " + ii.itemName);
		Character c = Globals.ins.currentlySelectedCharacter;

		Quaternion targetRotation = Quaternion.LookRotation(ii.transform.position - c.transform.position);
		c.transform.localRotation = targetRotation;
	}

	public override bool CheckIfPossible(GameObject obj)
	{
		throw new System.NotImplementedException();
	}
}
