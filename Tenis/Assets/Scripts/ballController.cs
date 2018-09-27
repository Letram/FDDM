using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballController : MonoBehaviour {

    private Rigidbody2D rb2D;
    private int value = 1;
	// Use this for initialization
	void Start () {
        rb2D = GetComponent<Rigidbody2D>();

        rb2D.AddForce(new Vector3(-200f, Random.Range(-300, 300f), 0f));
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void resetPosition()
    {
        transform.position = new Vector3(0, 0, 0);
        rb2D.isKinematic = true;
        rb2D.velocity = new Vector2(0, 0);
        rb2D.isKinematic = false;

        rb2D.AddForce(new Vector3(UnityEngine.Random.Range(-450, -200), UnityEngine.Random.Range(-75, 50), 0f));
    }
}
