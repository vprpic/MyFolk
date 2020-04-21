using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(UIInputHandler))]
public class InputHandler : MonoBehaviour
{
    //TODO: move items
    public GameObject tempDestination;
    public GameObject selectCharacter;
    public GameData currentCharacter;
    //^^^
    [HideInInspector]
    public UIInputHandler uIInteractionHandler;
    [HideInInspector]
    public ItemInteractionHandler itemInteractionHandler;

    private void Awake()
    {
        currentCharacter.currentAgent = selectCharacter.GetComponent<NavMeshAgent>();

        uIInteractionHandler = GetComponent<UIInputHandler>();

        itemInteractionHandler = new ItemInteractionHandler();
        itemInteractionHandler.Init();
    }

    private void Update()
    {
        if (!uIInteractionHandler.isHovering)
        {
            itemInteractionHandler.CheckForHit();
        }
    }
}
