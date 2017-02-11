using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineItemInteraction : MonoBehaviour {

    private GameColor color;
    private ItemInteraction currentItem;
    private CharacterItemInteraction currentPlayer;
    private int pushCount;
    private const int COUNT_LIMIT = 20;

    void Start () {		
	}
	
	void LateUpdate () {
        if (currentItem != null) {
            if (Input.GetButtonDown("Interact_" + currentPlayer.playerNo)) {
                pushCount++;
                if (pushCount >= COUNT_LIMIT) {
                    complete();
                }
            }
        }
	}

    internal bool StartProcessingItem(CharacterItemInteraction playerCII, ItemInteraction item) {
        if (playerCII.color == color && !item.processed) {
            if (item != null) {
                currentItem = item;
                currentPlayer = playerCII;
                pushCount = 0;
                return true;
            }
        }
        return false;
    }

    private void complete() {
        //add color
        currentItem.MarkAsProcessedBy(this);
        currentPlayer.ProcessingComplete(currentItem);
        currentPlayer = null;
        currentItem = null;
    }

}
