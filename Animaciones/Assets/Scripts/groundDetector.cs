using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundDetector : MonoBehaviour {

    private DogController2 dog;
	// Use this for initialization
	void Start () {
        dog = GetComponentInParent<DogController2>();
	}

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("ground"))
            dog.switchGrounded();
    }

    private void OnCollisionExit2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("ground"))
            dog.switchGrounded();
    }
}
