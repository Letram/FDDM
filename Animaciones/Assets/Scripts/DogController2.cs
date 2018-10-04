using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController2 : MonoBehaviour {

    private Rigidbody2D dogBody;
    private Animator dogAnimator;
    private Boolean grounded;
    private Boolean jumping;
    private Boolean running;
    private Boolean dying;
    private Boolean accelerated;

    public float accelerationFactor = 50f;
    public float jumpPower = 6.5f;
    public int maxSpeed = 3;

	// Use this for initialization
	void Start () {
        dogBody = GetComponent<Rigidbody2D>();
        dogAnimator = GetComponent<Animator>();

	}

    // Update is called once per frame
    void Update () {
        dogAnimator.SetFloat("Speed", Mathf.Abs(dogBody.velocity.x));
        dogAnimator.SetBool("TouchingGround", grounded);

        //jump
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
            jumping = true;

        //running
        if (Input.GetKey(KeyCode.LeftShift))
        {
            running = true;
            dogAnimator.SetTrigger("Run");
            dogAnimator.ResetTrigger("Walk");
            if (Input.GetKeyDown(KeyCode.S))
            {
                dogAnimator.SetTrigger("Slide");
                dogAnimator.ResetTrigger("Run");
            }
            if (!accelerated)
            {
                maxSpeed *= 2;
                accelerated = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            running = false;
            dogAnimator.SetTrigger("Walk");
            dogAnimator.ResetTrigger("Run");
            maxSpeed /= 2;
            accelerated = false;
        }

        //dying
        dogAnimator.SetBool("Dead", dying);

    }

    void FixedUpdate()
    {
        float hMovement = Input.GetAxis("Horizontal");

        //sprite facing left or right
        if (hMovement > 0)
            GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        else if(hMovement < 0)
            GetComponent<Transform>().localScale = new Vector3(-1, 1, 1);

        dogBody.AddForce(Vector2.right * accelerationFactor * hMovement);

        float clampedSpeed = Mathf.Clamp(dogBody.velocity.x, -maxSpeed, maxSpeed);
        dogBody.velocity = new Vector2(clampedSpeed, dogBody.velocity.y);

        if (jumping)
        {
            dogBody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jumping = false;
        }
    }

    void OnTriggerEnter2D(Collider2D cat)
    {
        if (cat.gameObject.CompareTag("Enemy"))
        {
            print(cat.gameObject.tag);
            dying = true;
        }
    }

    public void switchGrounded()
    {
        if (grounded) grounded = false;
        else grounded = true;
    }

    private void resetPosition()
    {
        GetComponent<Transform>().position = new Vector3(0, -0.6f, 0);
        dying = false;
    }
}
