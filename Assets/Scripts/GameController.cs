using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [SerializeField] private int score;
    public const float totalTime = 240f;
    private float timeRemaining;
    public GameObject scoreText;
    public GameObject timeText;
    private AudioController audioController;
    [SerializeField]
    private GameObject ingameCanvas;
    [SerializeField]
    private GameObject endgameCanvas;
    private ColorManager manager;
    public GameObject Cmanager;

    // Use this for initialization
    void Start () {
        score = 0;
        timeRemaining = totalTime;
        audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
        manager = Cmanager.GetComponent<ColorManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (score >= 2) {
            timeRemaining -= Time.deltaTime;
        }
        if (timeRemaining <= 0) {
            audioController.StopPanic();
            endGame();
        } else if (timeRemaining <= 20) {
            audioController.StartPanic();
        }
        if (timeRemaining>totalTime*4f/5)
        {
            manager.setCurrent(3);
        }
        else if (timeRemaining > totalTime * 3f / 5)
        {
            manager.setCurrent(2);
        }
        else if (timeRemaining > totalTime * 2f / 5)
        {
            manager.setCurrent(1);
        }
        else if (timeRemaining > totalTime * 1f / 5)
        {
            manager.setCurrent(0);
        }
        scoreText.GetComponent<Text>().text = score.ToString();
        String fullTimeStr = TimeSpan.FromSeconds(timeRemaining).ToString();
        timeText.GetComponent<Text>().text = fullTimeStr.Remove(fullTimeStr.Length - 4).Substring(3);
    }

    public int getScore() {
        return score;
    }
    public void addPoint() {
        timeRemaining += 5;
        timeRemaining = Mathf.Clamp(timeRemaining, 0, totalTime);
        score += 1;
    }


    void endGame() {
        ingameCanvas.SetActive(false);
        endgameCanvas.SetActive(true);
        GameObject.Find("FinalScoreText").GetComponent<Text>().text = score.ToString();
    }

    public float getTimeRemaining() {
        return timeRemaining;
    }

}
