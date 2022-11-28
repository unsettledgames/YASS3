using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float MaxSpeed;
    [SerializeField]
    private float AccelerationSpeed;
    [SerializeField]
    private float RotationSpeed;

    private Rigidbody m_Rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 input, rotation, tilt;

        input.x = Input.GetAxis("Horizontal"); input.y = Input.GetAxis("Vertical"); input.z = Input.GetAxis("Torque");
        rotation.y = input.x; rotation.x = -input.y; rotation.z = -input.z;

        m_Rigidbody.velocity = transform.forward * Time.deltaTime * AccelerationSpeed;
        m_Rigidbody.velocity = Vector3.ClampMagnitude(m_Rigidbody.velocity, MaxSpeed);

        m_Rigidbody.rotation *= Quaternion.Euler(rotation * Time.deltaTime * RotationSpeed);
    }
}
