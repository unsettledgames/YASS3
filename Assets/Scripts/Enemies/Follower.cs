using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float Damage;
    [SerializeField] private float PlayerPredictionAmount;

    private PlayerController m_Player;
    private Rigidbody m_PlayerBody;
    private Rigidbody m_Body;
    // Start is called before the first frame update
    void Start()
    {
        m_Player = FrequentlyAccessed.Instance.Player;
        m_PlayerBody = m_Player.GetComponent<Rigidbody>();

        m_Body = GetComponent<Rigidbody>();
        m_Body.velocity = transform.forward * Speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 currVelocity = m_Body.velocity;
        Vector3 targetVelocity = m_PlayerBody.transform.position - transform.position;

        m_Body.velocity += (targetVelocity - currVelocity) * PlayerPredictionAmount;
        m_Body.velocity = m_Body.velocity.normalized * Speed;

        transform.LookAt(transform.position + m_Body.velocity.normalized);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            PlayerHealthManager phm = other.GetComponentInParent<PlayerHealthManager>();
            if (phm != null)
                phm.TakeDamage(Damage);
            GetComponent<EnemyHealthManager>().TakeDamage(999999);
        }
    }
}
