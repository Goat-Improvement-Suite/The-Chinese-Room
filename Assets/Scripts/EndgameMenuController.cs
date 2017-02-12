using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndgameMenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ReplayLevel() {
        SceneManager.LoadScene("chinese_room");
    }

    public void BackToMenu() {
        SceneManager.LoadScene("menu");
    }
}
