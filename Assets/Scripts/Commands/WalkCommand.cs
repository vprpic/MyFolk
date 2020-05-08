using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkCommand : ICommand
{
	public NavMeshAgent agent;
	public Vector3 point;

	public WalkCommand(NavMeshAgent agent, Vector3 point)
	{
		this.agent = agent;
		this.point = point;
	}

	public void Execute()
	{
		Debug.Log("???????????????????WalkCommand: " + point.ToString());
		Debug.LogError("NOT USED??");
		//agent.SetDestination(point);
	}
}
