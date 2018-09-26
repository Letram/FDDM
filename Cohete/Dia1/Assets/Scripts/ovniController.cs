using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ovniController : MonoBehaviour {

    private Rigidbody2D ovniRigidBody;

    public int value;
    private int positionIndex;
    // Use this for initialization
    void Start () {
        ovniRigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        bool advance = Input.GetKey("up");
        bool right = Input.GetKey("right");
        bool left = Input.GetKey("left");
        bool down = Input.GetKey("down");
        bool move = false;
        if (advance && !down)
        {
            if (!right && !left)
            {
                ovniRigidBody.MoveRotation(0);
                move = true;
            }
        }
        if (left && !right)
        {
            if (left && !advance && !down)
            {
                ovniRigidBody.MoveRotation(90);
            }
            else if (left && down && !advance)
            {
                ovniRigidBody.MoveRotation(145);
            }
            else
            {
                ovniRigidBody.MoveRotation(45);
            }
            move = true;

        }
        if (right && !left)
        {
            if (right && !advance && !down)
            {
                ovniRigidBody.MoveRotation(-90);
            }
            else if (right && down && !advance)
            {
                ovniRigidBody.MoveRotation(-145);
            }
            else
            {
                ovniRigidBody.MoveRotation(-45);
            }
            move = true;

        }
        if (down && !advance)
        {
            if (!right && !left)
            {
                ovniRigidBody.MoveRotation(180);
            }
            move = true;
        }

        if (move)
        {
            ovniRigidBody.AddForce(transform.up * 20);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    public void setPositionIndex(int index)
    {
        positionIndex = index;
    }
    public int getPositionIndex()
    {
        return positionIndex;
    }
}
