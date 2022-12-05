using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField]
    private float Speed;
    [SerializeField]
    private float Damage;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * Speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemy"))
        {
            // Get enemy health, reduce it
            EnemyHealthManager enemyHealth = other.GetComponentInParent<EnemyHealthManager>();
            if (enemyHealth != null)
                enemyHealth.TakeDamage(Damage);

            // TODO: Instantiate vfx

            // Destroy
            Destroy(this.gameObject);
        }
    }
}
