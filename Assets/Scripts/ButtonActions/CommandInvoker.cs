using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker : MonoBehaviour
{
	static Queue<ICommand> commandBuffer;
	private void Awake()
	{
		commandBuffer = new Queue<ICommand>();
	}
	public static void AddCommand(ICommand command)
	{
		commandBuffer.Enqueue(command);
	}
	private void Update()
	{
		if (commandBuffer.Count > 0)
		{
			ICommand c = commandBuffer.Dequeue();
			c.Execute();
		}
	}
}
