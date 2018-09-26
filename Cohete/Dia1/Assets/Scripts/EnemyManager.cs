using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public GameObject[] enemies;
    public float spawnTime = 6f;
    public Transform[] spawnPoints;
    public int maxEnemies;

    private static int currentEnemies;
    private static Boolean[] enemyPositions;
    // Use this for initialization
    void Start()
    {
        enemyPositions = new Boolean[spawnPoints.Length];
        for (int i = 0; i < enemyPositions.Length; i++) enemyPositions[i] = false;

        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn()
    {
        if (currentEnemies >= maxEnemies || positionsFull()) return;

        int enemyIndex = UnityEngine.Random.Range(0, enemies.Length);

        int spawnPointIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
        while (enemyPositions[spawnPointIndex])
        {
            spawnPointIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
        }
        enemyPositions[spawnPointIndex] = true;

        GameObject go = Instantiate(enemies[enemyIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation) as GameObject;
        ovniController ovni = go.GetComponent<ovniController>();
        ovni.setPositionIndex(spawnPointIndex);
        currentEnemies++;
    }

    public static void enemyDestroyed(Collider2D enemyDestroyed)
    {
        enemyPositions[enemyDestroyed.GetComponent<ovniController>().getPositionIndex()] = false;
        Destroy(enemyDestroyed.gameObject);
        currentEnemies--;
    }

    private bool positionsFull()
    {
        foreach (Boolean pos in enemyPositions)
        {
            if (!pos) return false;
        }
        return true;
    }
}
