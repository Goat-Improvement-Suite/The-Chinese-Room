using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    private bool fading;

	// Use this for initialization
	void Start () {
        fading = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (fading) {
            if (this.GetComponent<AudioSource>().volume - Time.deltaTime > 0) {
                this.GetComponent<AudioSource>().volume -= Time.deltaTime;
            }
        }
	}

    public void StartPanic() {
        this.GetComponent<AudioSource>().pitch = 1.5f;
    }

    public void StopPanic() {
        this.GetComponent<AudioSource>().pitch = 1f;
    }

    public void StartFade() {
        fading = true;
    }

}
