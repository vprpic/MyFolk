using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public abstract class ButtonAction : ScriptableObject
{
    public Sprite sprite;
    public string title;
    public ICommand command;
    [HideInInspector]
    public Vector3 clickPoint;
    [HideInInspector]
    public NavMeshAgent agent;

    public abstract void PrepareExecution(NavMeshAgent agent, Vector3 clickPoint);

    public void Execute()
    {
        CommandInvoker.AddCommand(command);
    }
}