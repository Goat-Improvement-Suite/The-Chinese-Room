using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    private static bool alreadyExists;

	// Use this for initialization
	void Start () {
        if (alreadyExists != true) {
            alreadyExists = true;
            DontDestroyOnLoad(this.gameObject);
        } else {
            GameObject.Destroy(this.gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
