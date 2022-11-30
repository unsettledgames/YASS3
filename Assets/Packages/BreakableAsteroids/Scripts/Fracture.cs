using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fracture : MonoBehaviour
{
    [SerializeField]
    private float ExplosionForce;
    [Tooltip("\"Fractured\" is the object that this will break into")]
    public GameObject fractured;

    public void OnDestroy()
    {
        if (!this.gameObject.scene.isLoaded) return;
        GameObject instantiatedFracture = Instantiate(fractured, transform.position, transform.rotation); //Spawn in the broken version
        instantiatedFracture.transform.localScale = transform.localScale;

        for (int i = 0; i < instantiatedFracture.transform.childCount; i++)
        {
            Rigidbody rb = instantiatedFracture.transform.GetChild(i).GetComponent<Rigidbody>();

            rb.useGravity = false;
            rb.isKinematic = false;
            rb.AddForce(new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) * ExplosionForce,
                ForceMode.Impulse);
        }

        Destroy(gameObject); //Destroy the object to stop it getting in the way
    }
}
