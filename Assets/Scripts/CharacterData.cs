using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// A place for all data, e.g. current character selected
/// </summary>
[CreateAssetMenu(fileName = "New Character Data", menuName = "Character Data")]
[System.Serializable]
public class CharacterData : ScriptableObject
{
	public NavMeshAgent agent;

	/*
	position,
	3d model,
	...
	 */
}
