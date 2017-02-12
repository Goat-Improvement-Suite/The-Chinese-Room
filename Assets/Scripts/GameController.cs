using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    private int score;
    private float timeRemaining;
    public GameObject scoreText;
    public GameObject timeText;
    private AudioController audioController;

	// Use this for initialization
	void Start () {
        score = 0;
        timeRemaining = 120f;
        audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
	}
	
	// Update is called once per frame
	void Update () {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0) {
            audioController.StopPanic();
            endGame();
        } else if (timeRemaining <= 20) {
            audioController.StartPanic();
        }

        scoreText.GetComponent<Text>().text = score.ToString();
        String fullTimeStr = TimeSpan.FromSeconds(timeRemaining).ToString();
        timeText.GetComponent<Text>().text = fullTimeStr.Remove(fullTimeStr.Length - 4).Substring(3);
    }

    public void addPoint() {
        score += 1;
    }


    void endGame() {

    }

    public float getTimeRemaining() {
        return timeRemaining;
    }

}
