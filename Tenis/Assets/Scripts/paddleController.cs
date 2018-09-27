//using System.Collections;
//using System.Collections.Generic;
using System;
using UnityEngine;


public class paddleController : MonoBehaviour {

    public int ident;

    private float paddleSpeed = 0.1f;
    private string axisControl;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        axisControl = "Vertical" + ident;
        print(axisControl);
        float yPos = transform.position.y + (Input.GetAxisRaw(axisControl) * paddleSpeed);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(yPos, -6f, 6f), 0f);
    }
}
