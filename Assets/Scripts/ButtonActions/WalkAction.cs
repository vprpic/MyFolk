using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "New Action", menuName = "Action/Walk")]
public class WalkAction : ButtonAction
{

	public void Init(string title, Sprite sprite, NavMeshAgent agent, Vector3 walkToPoint)
	{
		this.title = title;
		this.sprite = sprite;
		this.agent = agent;
		this.clickPoint = walkToPoint;
	}

	public void EnqueueCommand()
	{
		CommandInvoker.AddCommand(new WalkCommand(agent,clickPoint));
	}

	public override void PrepareExecution(NavMeshAgent agent, Vector3 clickPoint)
	{
		this.clickPoint = clickPoint;
		this.agent = agent;
		EnqueueCommand();
	}
}
