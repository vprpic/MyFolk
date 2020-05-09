using System.Collections;
using UnityEngine;
using UnityEngine.AI;

//https://docs.unity3d.com/Manual/nav-CouplingAnimationAndNavigation.html
//https://gist.github.com/gekidoslair/a37267c1402ab6ee5ad1929c8be86ea1
//https://www.reddit.com/r/Unity3D/comments/ccttbj/how_to_rotate_the_navmesh_agent_before_start/
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class LocomotionSimpleAgent : MonoBehaviour
{
    LookAt lookAt;
    Vector3 worldDeltaPosition = Vector3.zero;
    //Vector3 position = Vector3.zero;
    NavMeshAgent agent;
    Animator animator;
    Vector3 prevAgentPoint;

    int moveHash = Animator.StringToHash("move");
    int turnHash = Animator.StringToHash("Turn");
    int forwardHash = Animator.StringToHash("Forward");

    bool isMoving;

    public float maxAnimatorRotateSpeed;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        // don't update the agent's position, the animation will do that
        //agent.updatePosition = false;
        agent.updateRotation = false;

        isMoving = false;
        lookAt = GetComponent<LookAt>();
    }

    //void Update()
    //{

    //    worldDeltaPosition = agent.nextPosition - transform.position;

    //    // Pull agent towards character
    //    if (worldDeltaPosition.magnitude > agent.radius)
    //        agent.nextPosition = transform.position + 0.9f * worldDeltaPosition;

    //    bool shouldMove = agent.hasPath;
    //    animator.SetBool(moveHash, shouldMove);

    //    if (lookAt)
    //    {
    //        Vector3 temp = agent.steeringTarget + transform.forward;
    //        //temp.y = 0f;
    //        lookAt.lookAtTargetPosition = temp;
    //    }
    //}

    //void OnAnimatorMove()
    //{
    //    // Update position based on animation movement using navigation surface height
    //    position = animator.rootPosition;
    //    position.y = agent.nextPosition.y;
    //    transform.position = position;
    //}


    public bool _isRotating;
    public bool isStopping;
    public float _stationaryTurnSpeed;
    public float _movingTurnSpeed;
    public float _forwardDumpTime;
    public float _turnDumpTime;

    private void Update()
    {
        if (!_isRotating)
        {
            bool move = agent.remainingDistance > agent.stoppingDistance;
            //if (!move && animator.GetBool(moveHash))
            //    animator.SetBool(moveHash, false);

            UpdateAnimator(move ? agent.desiredVelocity : Vector3.zero);

            if (agent.hasPath && Vector3.Distance(agent.destination, transform.position) < agent.stoppingDistance/2)
            {
                StopMoving();
            }
        }

        if (isStopping)
        {
            SlowDownBeforeStopping();
        }
        //if (agent.remainingDistance < agent.stoppingDistance)
        //    agent.ResetPath();

        //worldDeltaPosition = agent.nextPosition - transform.position;

        //// Pull agent towards character
        //if (worldDeltaPosition.magnitude > agent.radius)
        //    agent.nextPosition = transform.position + 0.9f * worldDeltaPosition;
    }


    private IEnumerator RotateToNextPosition()
    {
        agent.isStopped = true;
        _isRotating = true;
        yield return null;
        float rotAngle = rotateAngle;
        animator.SetFloat(turnHash, maxAnimatorRotateSpeed*(Mathf.Sign(rotateAngle)));

        //TODO: is there a less harsh way to do this? now it instantly stops
        animator.SetFloat(forwardHash, 0f);
        while (Mathf.Abs(rotateAngle) > 0.1f)
        {
            yield return null;
        }
        animator.SetFloat(turnHash, 0f);
        _isRotating = false;
        agent.isStopped = false;
    }

    private float rotateAngle
    {
        get
        {
            Vector3 direction = agent.steeringTarget - transform.position;
            float rotateAngle = Vector3.SignedAngle(transform.forward, direction, transform.up) / 180f;
            return rotateAngle;
        }
    }

    private void UpdateAnimator(Vector3 velocity)
    {
        float turnAmount = 0f;
        float forwardAmount = 0f;

        if (velocity != Vector3.zero)
        {
            if (velocity.magnitude > 1f)
            {
                velocity.Normalize();
            }

            velocity = transform.InverseTransformDirection(velocity);

            turnAmount = Mathf.Atan2(velocity.x, velocity.z);
            forwardAmount = velocity.z;
            float turnSpeed = Mathf.Lerp(_stationaryTurnSpeed, _movingTurnSpeed, forwardAmount);

            transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, 0f);
        }

        animator.SetFloat(forwardHash, forwardAmount, _forwardDumpTime, Time.deltaTime);
        animator.SetFloat(turnHash, turnAmount, _turnDumpTime, Time.deltaTime);
    }

    public void StopMoving()
    {
        if (!isMoving)
            return;
        isStopping = true;
        agent.ResetPath();
        //isMoving = false;
    }

    private void SlowDownBeforeStopping()
    {
        animator.SetFloat(forwardHash, 0f, _forwardDumpTime * 100, Time.deltaTime);
        //animator.SetFloat(turnHash, 0f, _turnDumpTime * 100, Time.deltaTime);
        if (animator.GetFloat(forwardHash) < 0.1f) //&& animator.GetFloat(turnHash) < 0.1f)
        {
            StopMovingSudden();
        }
    }

    private void StopMovingSudden()
    {
        if (!isMoving)
            return;
        agent.ResetPath();
        //TODO: is there a less harsh way to do this? now it instantly stops
        animator.SetFloat(forwardHash, 0f);
        animator.SetFloat(turnHash, 0f);
        isStopping = false;
        isMoving = false;
    }

    public void MoveTo(Vector3 target)
    {
        if (isMoving)
            StopMoving();
        isMoving = true;
        agent.SetDestination(target);
        //animator.SetBool(moveHash, true);
        StartCoroutine(RotateToNextPosition());
    }
}