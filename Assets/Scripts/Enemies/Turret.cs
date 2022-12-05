using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Externals")]
    public GameObject Bullet;
    public GameObject RotateY;
    public GameObject RotateX;
    public GameObject ShootSpawn;

    [Header("Shooting")]
    [SerializeField] private Vector2 PredictionBounds;
    [SerializeField] private float ShootRate;
    [SerializeField] private float FlurryDuration;
    [SerializeField] private float PauseDuration;
    [SerializeField] private float TriggerDistance;

    private PlayerController m_Player;
    private float m_NextShootStartTime;
    private float m_NextShootStopTime;
    // Start is called before the first frame update
    void Start()
    {
        m_Player = FrequentlyAccessed.Instance.Player;
        m_NextShootStartTime = Time.time + ShootRate;
        m_NextShootStopTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float playerDistance = Vector3.Distance(transform.position, m_Player.transform.position);
        if (playerDistance < TriggerDistance)
        {
            AimAtPlayer();
            Shoot();
        }
    }

    void Shoot()
    {
        // TODO: turret autoaim
        if (m_NextShootStopTime < m_NextShootStartTime)
            m_NextShootStopTime = m_NextShootStartTime + FlurryDuration;

        if (Time.time <= m_NextShootStopTime && Time.time >= m_NextShootStartTime)
        {
            GameObject projectile = Instantiate(Bullet, ShootSpawn.transform.position, Quaternion.Euler(Vector3.zero));
            projectile.transform.LookAt(m_Player.transform.position);

            m_NextShootStartTime = Time.time + ShootRate;
        }
    }

    void AimAtPlayer()
    {
        Vector3 rot;
        float yAngle, xAngle;

        RotateY.transform.LookAt(m_Player.transform.position);
        RotateY.transform.localEulerAngles = new Vector3(0.0f, RotateY.transform.localEulerAngles.y, 0.0f);

        RotateX.transform.LookAt(m_Player.transform.position);
        RotateX.transform.localEulerAngles = new Vector3(RotateX.transform.localEulerAngles.x, 0.0f, 0.0f);
    }

    IEnumerator ResetShootingTime()
    {
        yield return new WaitForSeconds(PauseDuration);

        m_NextShootStartTime = Time.time + m_NextShootStartTime;
    }
}
