using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour {

    private Animator dogAnimator;
    private Rigidbody2D dogBody;
    private Boolean locked;
    private Boolean dead;
    private int direction;
    //como el sprite es más o menos simétrico, pues cogemos el spriteRenderer. Si no fuera el caso, usaríamos el Transform.ScaleX
    private SpriteRenderer dogRender;
	// Use this for initialization
	void Start () {
        dogAnimator = GetComponent<Animator>();
        dogRender = GetComponent<SpriteRenderer>();
        dogBody = GetComponent<Rigidbody2D>();
        locked = false;
        dead = false;
        direction = 1;
	}
	
	// Update is called once per frame
	void Update () {

        //Horizontal movement
        float mov = Input.GetAxisRaw("Horizontal"); //entre -1 y 1. Implica la dirección y la "potencia" con la que nos movemos.
        if (!dead)
        {
            if(mov != 0)
            {
                if (mov > 0)
                {
                    dogRender.flipX = false;
                    direction = 1;
                }
                else
                {
                    dogRender.flipX = true;
                    direction = -1;
                }

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    if (Input.GetKeyDown(KeyCode.S))
                    {
                        dogAnimator.SetTrigger("Slide");
                        slide();
                    }
                    else
                    {
                        dogAnimator.SetTrigger("Run");
                        dogAnimator.ResetTrigger("Idle");
                        dogBody.velocity = new Vector2(6 * direction, 0);
                    }
                }
                else
                {

                    dogAnimator.SetTrigger("Walk");
                    resetTriggers(new String[] { "Idle" });
                    dogBody.velocity = new Vector2(2 * direction, 0);
                }

            }else{
                dogAnimator.SetTrigger("Idle");
                resetTriggers(new String[] {"Walk"});
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

    private void slide()
    {
        GetComponent<BoxCollider2D>().size = new Vector2(GetComponent<BoxCollider2D>().size.y, GetComponent<BoxCollider2D>().size.x);
        GetComponent<BoxCollider2D>().offset = new Vector2(GetComponent<BoxCollider2D>().offset.x, -0.87f);
        dogBody.velocity = new Vector2(12 * direction, 0);
    }

    private void slideEnd()
    {
        GetComponent<BoxCollider2D>().size = new Vector2(GetComponent<BoxCollider2D>().size.y, GetComponent<BoxCollider2D>().size.x);
        GetComponent<BoxCollider2D>().offset = new Vector2(GetComponent<BoxCollider2D>().offset.x, 0);

    }

    private void jump()
    {
        locked = true;
        dogBody.gravityScale = 35;
        dogBody.AddForce(Vector2.up * 8000);
    }
    private void landing()
    {
        dogBody.gravityScale = 1;
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

    private void resetTriggers(String[] triggers)
    {
        foreach (String trigger in triggers)
            dogAnimator.ResetTrigger(trigger);
    }
}
