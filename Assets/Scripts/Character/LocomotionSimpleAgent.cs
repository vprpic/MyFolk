using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class LocomotionSimpleAgent : MonoBehaviour
{
    LookAt lookAt;
    Vector3 worldDeltaPosition = Vector3.zero;
    Vector3 position = Vector3.zero;
    NavMeshAgent agent;
    Animator animator;

    int moveHash = Animator.StringToHash("move");
    int turnHash = Animator.StringToHash("Turn");
    int forwardHash = Animator.StringToHash("Forward");


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        // don't update the agent's position, the animation will do that
        agent.updatePosition = false;

        lookAt = GetComponent<LookAt>();
    }

    void Update()
    {

        worldDeltaPosition = agent.nextPosition - transform.position;

        // Pull agent towards character
        if (worldDeltaPosition.magnitude > agent.radius)
            agent.nextPosition = transform.position + 0.9f * worldDeltaPosition;

        bool shouldMove = agent.hasPath;
        animator.SetBool(moveHash, shouldMove);

        //Vector3 worldDeltaPosition = agent.nextPosition - transform.position;

        //// Map 'worldDeltaPosition' to local space
        //float dx = Vector3.Dot(transform.right, worldDeltaPosition);
        //float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
        //Vector2 deltaPosition = new Vector2(dx, dy);

        //// Low-pass filter the deltaMove
        //float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
        //smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);

        //// Update velocity if time advances
        //if (Time.deltaTime > 1e-5f)
        //    velocity = smoothDeltaPosition / Time.deltaTime;

        //velocity = agent.desiredVelocity;
        ////bool shouldMove = velocity.magnitude > 0.5f && agent.remainingDistance > agent.radius;
        //bool shouldMove = agent.remainingDistance < agent.stoppingDistance;

        //    // Update animation parameters
        //anim.SetBool(moveHash, shouldMove);
        //anim.SetFloat(turnHash, velocity.x);  //"velx", velocity.x);
        //anim.SetFloat(forwardHash, velocity.y);  //"vely", velocity.y);

        if (lookAt)
        {
            Vector3 temp = agent.steeringTarget + transform.forward;
            //temp.y = 0f;
            lookAt.lookAtTargetPosition = temp;
        }
        //// Pull character towards agent
        //if (worldDeltaPosition.magnitude > agent.radius)
        //    transform.position = agent.nextPosition - 0.9f * worldDeltaPosition;

        //// Pull agent towards character
        //if (worldDeltaPosition.magnitude > agent.radius)
        //    agent.nextPosition = transform.position + 0.9f * worldDeltaPosition;
    }

    //void OnAnimatorMove()
    //{
    //    // Update position to agent position
    //    transform.position = agent.nextPosition;
    //}




    void OnAnimatorMove()
    {
        // Update position based on animation movement using navigation surface height
        position = animator.rootPosition;
        position.y = agent.nextPosition.y;
        transform.position = position;


        //// Update position based on animation movement using navigation surface height
        //Vector3 position = anim.rootPosition;
        //position.y = agent.nextPosition.y;
        //transform.position = position;

        //transform.position = agent.nextPosition;
    }
}