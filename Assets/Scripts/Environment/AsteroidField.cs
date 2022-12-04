using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidField : MonoBehaviour
{
    public GameObject[] Asteroids;

    [SerializeField] private float SpawnRadius = 800;
    [SerializeField] private float Amount = 1000;
    [SerializeField] private float DelayBetweenSpawns = 0;
    [SerializeField] private Vector2 ScaleBounds;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnField());
    }

    private IEnumerator SpawnField()
    {
        Vector3 asteroidPos;
        float asteroidScale;

        for (int i=0; i<Amount; i++)
        {
            asteroidPos.x = Random.Range(-SpawnRadius, SpawnRadius);
            asteroidPos.y = Random.Range(-SpawnRadius, SpawnRadius);
            asteroidPos.z = Random.Range(-SpawnRadius, SpawnRadius);

            asteroidScale = Random.Range(ScaleBounds.x, ScaleBounds.y);

            GameObject instance = Instantiate(Asteroids[Random.Range(0, Asteroids.Length)], asteroidPos, Quaternion.Euler(asteroidPos));
            instance.transform.localScale *= asteroidScale;

            if (DelayBetweenSpawns > 0)
                yield return new WaitForSeconds(DelayBetweenSpawns);
        }

        yield return null;
    }
}
