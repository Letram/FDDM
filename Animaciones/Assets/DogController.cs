using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour {

    private Animator dogAnimator;
    private Rigidbody2D dogBody;
    private Boolean locked;
    private Boolean dead;
    //como el sprite es más o menos simétrico, pues cogemos el spriteRenderer. Si no fuera el caso, usaríamos el Transform.ScaleX
    private SpriteRenderer dogRender;
	// Use this for initialization
	void Start () {
        dogAnimator = GetComponent<Animator>();
        dogRender = GetComponent<SpriteRenderer>();
        dogBody = GetComponent<Rigidbody2D>();
        locked = false;
        dead = false;
	}
	
	// Update is called once per frame
	void Update () {

        //Horizontal movement
        float mov = Input.GetAxisRaw("Horizontal"); //entre -1 y 1. Implica la dirección y la "potencia" con la que nos movemos.
        if (!dead)
        {
            if (mov > 0)
            {
                dogRender.flipX = false;
                //Si empezamos a caminar y estamos ya haciendo la animación del idle, pues cancelamos con el reset.
                dogAnimator.SetTrigger("Walk");
                dogAnimator.ResetTrigger("Idle");
                dogAnimator.ResetTrigger("Jump");

                dogBody.velocity = new Vector2(2, 0);
            }
            else if (mov < 0)
            {

                dogRender.flipX = true;
                dogAnimator.SetTrigger("Walk");
                dogAnimator.ResetTrigger("Idle");

                dogBody.velocity = new Vector2(-2, 0);

            }
            else
            {
                dogAnimator.SetTrigger("Idle");
                dogAnimator.ResetTrigger("Walk");
                dogBody.velocity = new Vector2(0, 0);

            }

            //Jump
            if (Input.GetKeyDown(KeyCode.Space) && !locked)
            {
                dogAnimator.SetTrigger("Jump");
                jump();
            }
        }
	}

    private void jump()
    {
        locked = true;
        dogBody.isKinematic = false;
        dogBody.gravityScale = 35;
        dogBody.AddForce(new Vector3(0f, 9000f, 0f));
    }
    private void landing()
    {
        dogBody.isKinematic = true;
        locked = false;
    }

    private void OnTriggerEnter2D(Collider2D enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {
            dogAnimator.SetTrigger("Die");
            dead = true;
        }
    }

    private void resetPosition()
    {
        GetComponent<Transform>().position = new Vector3(0,-0.6f,0);
        dead = false;
    }
}
