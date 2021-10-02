using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] props;
    public float spawnInterval;
    float currentRemaining;
    bool enableSpawning = true;

    void Start()
    {
        currentRemaining = spawnInterval;
    }

    void Update()
    {
        if (enableSpawning)
        {
            if(currentRemaining < 0f)
            {
                Instantiate(GetRandomProp(), new Vector2(Random.Range(-6f, 6f), 5f), Quaternion.identity);
                // Debug.Log("Spawned object!");
                currentRemaining = spawnInterval;
            } else {
                currentRemaining -= Time.deltaTime;
            }
        }
    }

    public void StopSpawning()
    {
        enableSpawning = false;
    }

    public GameObject GetRandomProp()
    {
        return props[Random.Range(0, props.Length)];
    }
}
