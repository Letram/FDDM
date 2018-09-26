using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballController : MonoBehaviour {

    private Rigidbody2D rb2D;

	// Use this for initialization
	void Start () {
        rb2D = GetComponent<Rigidbody2D>();

        rb2D.AddForce(new Vector3(-200f, Random.Range(-300, 300f), 0f));
	}
	
	// Update is called once per frame
	void Update () {

	}
}
