using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// A place for all data, e.g. current character selected
/// </summary>
[CreateAssetMenu(fileName = "New GlobalsDataObject", menuName = "Globals/Data Object")]
[System.Serializable]
public class GlobalsDataObject : ScriptableObject
{
	public CharacterData currentlySelectedCharacterData;
	public Vector3 worldClickPoint;
	public int totalCharactersInstantiated;
}
