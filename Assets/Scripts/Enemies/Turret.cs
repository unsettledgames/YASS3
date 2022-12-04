using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject RotateY;
    public GameObject RotateX;

    [SerializeField] private Vector2 PredictionBounds;
    [SerializeField] private float ShootRate;
    [SerializeField] private float TriggerDistance;

    private PlayerController m_Player;
    // Start is called before the first frame update
    void Start()
    {
        m_Player = FrequentlyAccessed.Instance.Player;
    }

    // Update is called once per frame
    void Update()
    {
        float playerDistance = Vector3.Distance(transform.position, m_Player.transform.position);
        if (playerDistance < TriggerDistance)
        {
            Vector3 toPlayer = m_Player.transform.position - transform.position;
            Vector3 rot;
            float yAngle, xAngle;

            RotateY.transform.LookAt(m_Player.transform.position);
            RotateY.transform.localEulerAngles = new Vector3(0.0f, RotateY.transform.localEulerAngles.y, 0.0f);

            RotateX.transform.LookAt(m_Player.transform.position);
            RotateX.transform.localEulerAngles = new Vector3(RotateX.transform.localEulerAngles.x, 0.0f, 0.0f);
        }
    }
}
