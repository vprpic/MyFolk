using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// A place for all data, e.g. current character selected
/// </summary>
[CreateAssetMenu(fileName = "New Game Data", menuName = "Game Data")]
[System.Serializable]
public class GameData : ScriptableObject
{
	public NavMeshAgent currentAgent;
	public Vector3 clickPoint;
}
