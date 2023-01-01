using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public GameObject DeathVFX;
    public GameObject VFXSpawn;
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
            Vector3 spawnPos = transform.position;
            if (VFXSpawn != null)
                spawnPos = VFXSpawn.transform.position;
            // TODO: offset should be relative
            Instantiate(DeathVFX, spawnPos, Quaternion.Euler(Vector3.zero));
            Destroy(this.gameObject);
        }
    }

    public float GetCurrentHealth()
    {
        return m_CurrHealth;
    }
}
