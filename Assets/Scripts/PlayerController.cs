using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float MaxSpeed;
    [SerializeField] private float AccelerationSpeed;
    [SerializeField] private float RotationSpeed;
    [SerializeField] private float HitRecoveryEasing;

    [Header("Shooting")]
    [SerializeField] private float TargetPredictionFactor;
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

                if (Vector3.Angle(targetPosition.normalized, transform.forward) > MaxAutoAimAngle)
                    bulletDirection = transform.forward;
                else
                {
                    if (targetBody != null)
                        targetPosition += targetBody.velocity.normalized * TargetPredictionFactor;
                    bulletDirection = targetPosition - transform.position;
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

        input.x = Input.GetAxis("Horizontal"); input.y = Input.GetAxis("Vertical"); input.z = Input.GetAxis("Torque");
        rotation.y = input.x; rotation.x = -input.y; rotation.z = -input.z;

        m_Rigidbody.velocity = transform.forward * Time.deltaTime * AccelerationSpeed;
        m_Rigidbody.velocity = Vector3.ClampMagnitude(m_Rigidbody.velocity, MaxSpeed);

        m_Rigidbody.rotation *= Quaternion.Euler(rotation * Time.deltaTime * RotationSpeed);
    }

    public void SetTarget(GameObject target) { m_Target = target; }
    public void ResetTarget() { m_Target = null; }
}
