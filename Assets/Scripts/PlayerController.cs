using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private float MaxSpeed;
    [SerializeField]
    private float AccelerationSpeed;
    [SerializeField]
    private float RotationSpeed;
    [SerializeField]
    private float HitRecoveryEasing;

    [Header("Shooting")]
    public GameObject Bullet;
    public GameObject[] LaserSpawns;

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
            Instantiate(Bullet, spawn.transform.position, 
                Quaternion.Euler(transform.eulerAngles) * Quaternion.Euler(new Vector3(0.0f, 0.0f, 45.0f)));
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
}
