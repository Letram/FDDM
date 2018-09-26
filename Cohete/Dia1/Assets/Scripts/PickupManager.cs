
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour {
    public GameObject[] pickups;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    private int maxPickups;
    private static int currentPickups;
    private static Boolean[] pickupPositions;
	// Use this for initialization
	void Start () {
        pickupPositions = new Boolean[spawnPoints.Length];
        for (int i = 0; i < pickupPositions.Length; i++) pickupPositions[i] = false;
        maxPickups = spawnPoints.Length;
        InvokeRepeating("Spawn", spawnTime, spawnTime);
	}

    void Spawn()
    {
        if (currentPickups >= maxPickups || positionsFull()) return;

        int pickupIndex = UnityEngine.Random.Range(0, pickups.Length);
        int spawnPointIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
        while (pickupPositions[spawnPointIndex] == true)
        {
            spawnPointIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
        }
        pickupPositions[spawnPointIndex] = true;

        GameObject go = Instantiate(pickups[pickupIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation) as GameObject;
        PickupController pickup = go.GetComponent<PickupController>();
        pickup.setPositionIndex(spawnPointIndex);
        currentPickups++;
    }

    private bool positionsFull()
    {
        foreach(Boolean pos in pickupPositions)
        {
            if (!pos) return false;
        }
        return true;
    }

    public static void pickupCollected(Collider2D pickup)
    {
        pickupPositions[pickup.gameObject.GetComponent<PickupController>().getPositionIndex()] = false;
        Destroy(pickup.gameObject);
        currentPickups--;
    }
}
