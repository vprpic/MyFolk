using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
[RequireComponent(typeof(NavMeshAgent))]
public class Character : MonoBehaviour
{
	public CharacterData data;
	public NavMeshAgent navMeshAgent;

	private void Awake()
	{
		navMeshAgent = GetComponent<NavMeshAgent>();
	}

	private void Start()
	{
		Globals.ins.allCharacters.Add(this);
	}

	public void SayYourName()
	{
		Debug.Log(data.characterFirstName + " " + data.characterLastName);
	}
}
