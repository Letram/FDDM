using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    private float hMovement;
    private float rotation;
    private int direction = 1;
    void Start()
    {
        hMovement = UnityEngine.Random.Range(3, 4);
        rotation = UnityEngine.Random.Range(2, 10);
    }
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x - (hMovement*Time.deltaTime), transform.position.y, transform.position.z);
        transform.Rotate(new Vector3(0, 0, direction*(transform.rotation.z+rotation)));
        if (transform.position.x < -7)
            destroy();
	}

    internal void destroy()
    {
        GetComponentInParent<EnemyManager>().enemyDestroyed(this.gameObject);
    }
}
