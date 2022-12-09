using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** AUTO AIM:
 *  - Click to shoot in front of space ship
 *  - Hold click to target an enemy and shoot at it: this makes the crosshair appear
 *  - Crosshair always visible, just small. When an enemy is targetted, it becomes red and bigger
 * 
 */ 

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float MaxSpeed;
    [SerializeField] private float MaxDashSpeed;
    [SerializeField] private float AccelerationSpeed;
    [SerializeField] private float DashSpeed;
    [SerializeField] private float RotationSpeed;
    [SerializeField] private float HitRecoveryEasing;

    [Header("Shooting")]
    [SerializeField] private Vector2 TargetPredictionFactor;
    [SerializeField] private Vector2 AutoAimDistanceBounds;
    [SerializeField] private float MaxAutoAimAngle;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private GameObject[] LaserSpawns;

    private GameObject m_Target = null;

    private Rigidbody m_Rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            ShootLasers();

        if (m_Rigidbody.angularVelocity.magnitude > 0.1f)
            m_Rigidbody.angularVelocity = Vector3.Lerp(Vector3.zero, m_Rigidbody.angularVelocity, HitRecoveryEasing);
    }

    private void ShootLasers()
    {
        foreach (GameObject spawn in LaserSpawns)
        {
            Vector3 bulletDirection = transform.forward;

            if (m_Target != null)
            {
                Rigidbody targetBody = m_Target.GetComponent<Rigidbody>();
                Vector3 targetPosition = m_Target.transform.position;
                float targetDistance = Vector3.Distance(targetPosition, transform.position);

                float angle = Vector3.Angle(targetPosition - transform.position,
                    (transform.forward * (targetPosition - transform.position).magnitude));

                // Ignore when angle is too big
                if (Mathf.Abs(angle) > MaxAutoAimAngle || targetDistance >= AutoAimDistanceBounds.y || targetDistance <= AutoAimDistanceBounds.x)
                    bulletDirection = transform.forward;
                else
                {
                    float targetPrediction = Mathf.Lerp(TargetPredictionFactor.x, TargetPredictionFactor.y, 
                        1.0f - (targetDistance - AutoAimDistanceBounds.x) / (AutoAimDistanceBounds.y - AutoAimDistanceBounds.x));
                    if (targetBody != null)
                        targetPosition += targetBody.velocity * targetPrediction;
                    bulletDirection = (targetPosition - transform.position).normalized;
                }
            }    

            GameObject instantiated = Instantiate(Bullet, transform.position, Quaternion.Euler(Vector3.zero));
            instantiated.transform.LookAt(transform.position + bulletDirection);
            instantiated.transform.position = spawn.transform.position;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 input, rotation;
        float dashMult = 1.0f;

        if (Input.GetButton("Dash"))
            dashMult = DashSpeed;

        input.x = Input.GetAxis("Horizontal"); input.y = Input.GetAxis("Vertical"); input.z = Input.GetAxis("Torque");
        rotation.y = input.x; rotation.x = -input.y; rotation.z = -input.z;

        m_Rigidbody.velocity = transform.forward * Time.deltaTime * AccelerationSpeed * dashMult;
        m_Rigidbody.rotation *= Quaternion.Euler(rotation * Time.deltaTime * RotationSpeed);

        if (dashMult > 1.0f)
            m_Rigidbody.velocity = Vector3.ClampMagnitude(m_Rigidbody.velocity, MaxDashSpeed);
        else
            m_Rigidbody.velocity = Vector3.ClampMagnitude(m_Rigidbody.velocity, MaxSpeed);
    }

    public GameObject Target  { get =>m_Target; set=>m_Target = value; }
}
