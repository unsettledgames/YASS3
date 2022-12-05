using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public GameObject DeathVFX;
    [SerializeField] private Vector3 ExplosionOffset;
    [SerializeField] private float MaxHealth;

    private float m_CurrHealth;
    // Start is called before the first frame update
    void Start()
    {
        m_CurrHealth = MaxHealth;   
    }

    public void TakeDamage(float damage)
    {
        m_CurrHealth -= damage;

        if (m_CurrHealth <= 0)
        {
            // TODO: offset should be relative
            Instantiate(DeathVFX, transform.position + ExplosionOffset, Quaternion.Euler(Vector3.zero));
            Destroy(this.gameObject);
        }
    }
}
