using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class shipController : MonoBehaviour {

    private Rigidbody2D shipRigidBody;
    private AudioSource sound;
    private int score;
    public int lives;
    private Boolean paused = false;

    public GameObject fire;
    public Text pointCounter;
    public Text livesCounter;
    public float speed;
	// Use this for initialization
	void Start () {
        shipRigidBody = GetComponent<Rigidbody2D>();
        sound = GetComponent<AudioSource>();
        fire.SetActive(false);
        score = 0;
        displayScore();
    }

    private void displayScore()
    {
        pointCounter.text = "Points: " + score;
        livesCounter.text = "Lives: " + lives;
    }

    internal int getLives()
    {
        return lives;
    }

    internal void disableShip()
    {
        paused = true;
    }

    // Update is called once per frame
    void FixedUpdate () {
        bool advance = Input.GetKey("w") ;
        bool right = Input.GetKey("d");
        bool left = Input.GetKey("a");
        bool down = Input.GetKey("s");
        bool move = false;
        if (!paused)
        {
            if (advance && !down)
            {
                if (!right && !left)
                {
                    shipRigidBody.MoveRotation(0);
                    move = true;
                }
            }
            if (left && !right)
            {
                if (left && !advance && !down)
                {
                    shipRigidBody.MoveRotation(90);
                }
                else if (left && down && !advance)
                {
                    shipRigidBody.MoveRotation(145);
                }
                else
                {
                    shipRigidBody.MoveRotation(45);
                }
                move = true;

            }
            if (right && !left)
            {
                if (right && !advance && !down)
                {
                    shipRigidBody.MoveRotation(-90);
                }
                else if (right && down && !advance)
                {
                    shipRigidBody.MoveRotation(-145);
                }
                else
                {
                    shipRigidBody.MoveRotation(-45);
                }
                move = true;

            }
            if (down && !advance)
            {
                if (!right && !left)
                {
                    shipRigidBody.MoveRotation(180);
                }
                move = true;
            }

            if (move)
            {
                activeEffects();
                shipRigidBody.AddForce(transform.up * speed);
            }

            if (!move)
            {
                if (sound.isPlaying)
                {
                    sound.Stop();
                }
                fire.SetActive(false);
            }
        }
        
	}

    private void activeEffects()
    {
        fire.SetActive(true);
        shipRigidBody.AddForce(transform.up * speed);
        if (!sound.isPlaying)
        {
            sound.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!paused)
        {
            if (collider.CompareTag("Pickup"))
            {
                score += collider.GetComponent<PickupController>().value;
                PickupManager.pickupCollected(collider);
            }
            else
            {
                score -= collider.GetComponent<ovniController>().value;
                lives--;
                EnemyManager.enemyDestroyed(collider);
            }
        }
        displayScore();
    }
}
