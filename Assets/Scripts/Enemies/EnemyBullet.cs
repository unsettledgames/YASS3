using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float Damage;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * Speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealthManager phm = other.GetComponentInParent<PlayerHealthManager>();
        if (phm != null)
        {
            // TODO: instantiate VFX
            phm.TakeDamage(Damage);
            Destroy(this.gameObject);
        }
    }
}
