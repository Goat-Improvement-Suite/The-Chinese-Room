using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    private bool fadingOut;
    private CanvasScaler scaler;
    private AudioController audioController;

    // Use this for initialization
    void Start () {
        fadingOut = false;
        scaler = GameObject.Find("Canvas").GetComponent<CanvasScaler>();
        audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (fadingOut) {
            scaler.scaleFactor -= Time.deltaTime;
            if (scaler.scaleFactor < 0.03f) {
                scaler.scaleFactor = 0.03f;
                SceneManager.LoadScene("chinese_room");
            }
        }
	}

    void StartGame() {
        if (!fadingOut) {
            audioController.StartFade();
            fadingOut = true;
            //System.Threading.Thread.Sleep(500);
        }
    }

    void QuitGame() {
        Application.Quit();
    }

    void Help() {
        SceneManager.LoadScene("help");
    }
}
