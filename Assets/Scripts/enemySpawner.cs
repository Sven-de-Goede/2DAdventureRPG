using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    private Vector2 spawnRange = new Vector2(2, 2);
    public GameObject slime;
    private float spawnCooldown = 2f;
    public GameObject[] enemyList;

    // Update is called once per frame
    void Update()
    {
        spawnCooldown -= 1f * Time.fixedDeltaTime;

        //picks random enemy from enemy list array, spawns it within defined spawn range.
        if (spawnCooldown <= 0)
        {
            Instantiate(enemyList[Random.Range(0, (enemyList.Length - 1))],new Vector2(Random.Range(-spawnRange.x, spawnRange.x), Random.Range(-spawnRange.y, spawnRange.y)), Quaternion.identity);
            spawnCooldown = 5f;
        }
    }
}
