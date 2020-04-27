using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MyFolk
{
	[System.Serializable]
	[RequireComponent(typeof(NavMeshAgent))]
	public class Character : MonoBehaviour
	{
		public CharacterData data;
		public NavMeshAgent navMeshAgent;
		public InteractionQueue interactionQueue;

		private void Awake()
		{
			navMeshAgent = GetComponent<NavMeshAgent>();
			interactionQueue = new InteractionQueue();
		}

		private void Start()
		{
			Globals.ins.allCharacters.Add(this);
		}

		private void Update()
		{
			interactionQueue.Update();
		}

		public void SayYourName()
		{
			Debug.Log(data.characterFirstName + " " + data.characterLastName);
		}
	}
}