using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineInteraction : MonoBehaviour {

    private GameColor color;
    private GameObject currentItem;
    private CharacterItemInteraction currentPlayer;
    private int pushCount;
    private const int COUNT_LIMIT = 20;

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (currentItem != null) {
            if (Input.GetButtonDown("Interact_" + currentPlayer.playerNo)) {
                pushCount++;
                if (pushCount >= COUNT_LIMIT) {
                    complete();
                }
            }
        }
	}

    bool Interact (CharacterItemInteraction playerCII, GameObject obj) {
        if (playerCII.color == color) {
            if (obj != null) {
                currentItem = obj;
                currentPlayer = playerCII;
                pushCount = 0;
                return true;
            }
        }
        return false;
    }

    private void complete() {
        //add color
        currentPlayer.ProcessingComplete(currentItem);
        currentPlayer = null;
        currentItem = null;
    }

}
