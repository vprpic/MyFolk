using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Controller : MonoBehaviour
{
    public GameObject tempDestination;
    public GameObject selectCharacter;
    public CharacterData currentCharacter;
    //public static CharacterData currentCharacter;


    private void Awake()
    {
        //currentCharacter = attachedInUnityCharacter;
        currentCharacter.agent = selectCharacter.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            currentCharacter.agent.SetDestination(new Vector3(12f, 0f, 12f));
            Debug.Log("fired");
        }
    }

    //private void Update()
    //{
    //if (Input.GetMouseButtonDown(0))
    //{
    //    Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hitInfo;
    //    if(Physics.Raycast(ray, out hitInfo, 100))
    //    {
    //        agent.SetDestination(hitInfo.point);
    //    }
    //}
    //}
}
