using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Behaviour:
 *      - When the player is to near or the enemy is hurt, escape until a certain distance
 *          - Add a random offset and change it every once in a while to make the fleeing more unpredictable
 *      - When at a certain distance from the player, wander in that area
 *      - After a while, return to the original area
 */
public class Wanderer : MonoBehaviour
{
    enum State {Wander, Flee, Return};

    [Header("Behaviour")]
    [SerializeField] private float WanderAreaRadius;
    [SerializeField] private float MinDistanceFromPlayer;
    [SerializeField] private float SafeDistance;
    [SerializeField] private float ReturnWaitTime;
    [SerializeField] private float PlayerPredictionAmount;
    [SerializeField] private float TargetChangeRate;

    [Header("Movement")]
    [SerializeField] private float MaxSpeed;
    [SerializeField] private float SteeringSpeed;
    [SerializeField] private float CollisionDetectionAmount;

    private Rigidbody m_Rigidbody;
    private PlayerController m_Player;

    private Vector3 m_StartPos;
    private Vector3 m_TargetPos;

    private Coroutine m_PositionRoutine;
    private State m_CurrState;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Player = FrequentlyAccessed.Instance.Player;

        m_StartPos = transform.position;
        m_Rigidbody.velocity = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 
            Random.Range(-1.0f, 1.0f)).normalized * MaxSpeed;

        m_PositionRoutine = StartCoroutine(ChangeTarget());
        m_CurrState = State.Wander;
    }

    // Update is called once per frame
    void Update()
    {
        ManageState();
        Debug.DrawLine(transform.position, m_TargetPos, Color.green);
    }

    private void FixedUpdate()
    {
        switch (m_CurrState)
        {
            case State.Wander:
                m_Rigidbody.velocity = Wander();
                break;
            case State.Flee:
                m_Rigidbody.velocity = Flee();
                break;
            case State.Return:
                m_Rigidbody.velocity = Return();
                break;
            default:
                m_Rigidbody.velocity = transform.forward * MaxSpeed;
                break;
        }

        transform.LookAt(transform.position + m_Rigidbody.velocity);
    }

    private void ManageState()
    {
        float playerDistance = Vector3.Distance(transform.position, m_Player.transform.position);

        switch (m_CurrState)
        {
            case State.Wander:
                // Change destination if you arrived
                float distance = Vector3.Distance(transform.position, m_TargetPos);
                if (distance < 0.1f)
                {
                    StopCoroutine(m_PositionRoutine);
                    m_PositionRoutine = StartCoroutine(ChangeTarget());
                }

                // Flee if the player is too near
                if (playerDistance < MinDistanceFromPlayer)
                {
                    StopCoroutine(m_PositionRoutine);
                    m_CurrState = State.Flee;
                }
                break;
            case State.Flee:
                // Enemy is safe, return to base
                if (playerDistance > SafeDistance)
                    m_CurrState = State.Return;
                break;
            case State.Return:
                // Returned to base, continue wondering
                if (Vector3.Distance(transform.position, m_TargetPos) < 1.0f)
                {
                    m_PositionRoutine = StartCoroutine(ChangeTarget());
                    m_CurrState = State.Wander;
                }
                break;
            default:
                break;
        }
    }

    private Vector3 Wander()
    {
        Vector3 desiredVelocity = (m_TargetPos - transform.position).normalized;
        Vector3 currVelocity = m_Rigidbody.velocity.normalized;
        Vector3 steeringForce = desiredVelocity - currVelocity;

        return (m_Rigidbody.velocity + steeringForce * SteeringSpeed).normalized * MaxSpeed;
    }

    private Vector3 Flee()
    {
        return (transform.position - m_Player.transform.position).normalized * MaxSpeed;
    }

    private Vector3 Return()
    {
        Vector3 desiredVelocity = (m_StartPos - transform.position).normalized;
        Vector3 currVelocity = m_Rigidbody.velocity.normalized;
        Vector3 steeringForce = desiredVelocity - currVelocity;

        return (m_Rigidbody.velocity + steeringForce * SteeringSpeed).normalized * MaxSpeed;
    }

    private IEnumerator ChangeTarget()
    {
        while (true)
        {
            SetRandomTarget();
            yield return new WaitForSeconds(Random.Range(TargetChangeRate, TargetChangeRate * 1.5f));
        }
    }

    private void SetRandomTarget()
    {
        m_TargetPos = m_StartPos + new Vector3(Random.Range(-WanderAreaRadius, WanderAreaRadius),
                Random.Range(-WanderAreaRadius, WanderAreaRadius), Random.Range(-WanderAreaRadius, WanderAreaRadius));
    }
}
