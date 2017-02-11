using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private int score;
    private float timeRemaining;


	// Use this for initialization
	void Start () {
        score = 0;
        timeRemaining = 90f;
	}
	
	// Update is called once per frame
	void Update () {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0) {
            endGame();
        }
	}

    public void addPoint() {
        score += 1;
    }


    void endGame() {

    }

}
