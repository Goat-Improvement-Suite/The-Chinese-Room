using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPromptController : MonoBehaviour {

    private bool flashing;
    public Sprite primary, secondary;
    private float timerCount;

	// Use this for initialization
	void Start () {
        flashing = false;
        timerCount = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (flashing) {
            if (timerCount > 0.2) {
                if (this.GetComponent<SpriteRenderer>().sprite == primary) {
                    this.GetComponent<SpriteRenderer>().sprite = secondary;
                } else {
                    this.GetComponent<SpriteRenderer>().sprite = primary;
                }
                timerCount = 0f;
            }
            timerCount += Time.deltaTime;
        } else {
            this.GetComponent<SpriteRenderer>().sprite = primary;
        }
	}

    public void StartFlashing() {
        flashing = true;
    }

    public void StopFlashing() {
        flashing = false;
    }
}
