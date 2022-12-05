using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    [SerializeField] private float MaxHealth;

    private float m_CurrHealth;

    private void Start()
    {
        m_CurrHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        m_CurrHealth -= damage;
    }
}
