using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightWallController : MonoBehaviour {

    public GameObject gameManager;
    // Use this for initialization
    private void OnTriggerEnter2D(Collider2D collider)
    {
        gameManager.GetComponent<GM>().resetPosition("right");
    }
}
