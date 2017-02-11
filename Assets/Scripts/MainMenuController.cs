﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void StartGame() {
        SceneManager.LoadScene("chinese_room");
    }

    void QuitGame() {
        Application.Quit();
    }

    void Help() {
        SceneManager.LoadScene("help");
    }
}
