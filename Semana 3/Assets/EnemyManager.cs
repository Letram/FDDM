using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public GameObject[] enemies;
    public float spawnTime = 6f;
    public int maxEnemies = 3;

    private static int currentEnemies;
    // Use this for initialization
    void Start()
    {

        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn()
    {
        if (currentEnemies >= maxEnemies) return;

        int enemyIndex = UnityEngine.Random.Range(0, enemies.Length);
        Instantiate(enemies[enemyIndex], new Vector3(Random.Range(5,7),Random.Range(-3,3),0), Quaternion.identity, transform);
        currentEnemies++;
    }

    internal void enemyDestroyed(GameObject enemyDestroyed)
    {
        currentEnemies--;
        Destroy(enemyDestroyed);
    }
}
