using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GM : MonoBehaviour {

    public GameObject paddle;
    public GameObject ball;
    public Text player1ScoreText;
    public Text player2ScoreText;

    private int p1Score;
    private int p2Score;
    private GameObject[] paddles = new GameObject[2];
    private Vector3[] paddlePositions = new Vector3[2];
    // Use this for initialization
    void Start () {
        p1Score = 0;
        p2Score = 0;
        paddlePositions[0] = new Vector3(-7f, 0f, 0f);
        paddlePositions[1] = new Vector3(7f, 0f, 0f);

        paddles[0] = Instantiate(paddle, paddlePositions[0], Quaternion.identity);
        paddles[1] = Instantiate(paddle, paddlePositions[1], Quaternion.identity);

        paddles[0].GetComponent<paddleController>().ident = 1;
        paddles[1].GetComponent<paddleController>().ident = 2;

        displayScores();
    }

    private void displayScores()
    {
        player1ScoreText.text = "Score: " + p1Score;
        player2ScoreText.text = "Score: " + p2Score;
    }

    public void resetPosition(String side)
    {
        if (side == "left")
            p2Score++;
        else
            p1Score++;
        displayScores();
        paddles[0].transform.position = paddlePositions[0];
        paddles[1].transform.position = paddlePositions[1];

        ball.transform.position = new Vector3(0, 0, 0);
        ball.GetComponent<Rigidbody2D>().isKinematic = true;
        ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        ball.GetComponent<Rigidbody2D>().isKinematic = false;

        ball.GetComponent<Rigidbody2D>().AddForce(new Vector3(UnityEngine.Random.Range(-200f, 200f), UnityEngine.Random.Range(-300f, 300f), 0f));
    }

    // Update is called once per frame
    void Update () {
		
	}
}
