using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineItemInteraction : Interaction {

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

    public override bool CanInteractWith(CharacterItemInteraction character, ItemInteraction item) {
        return (character != null && character.color == color && item != null && !item.processed);
    }

    public bool StartProcessingItem(CharacterItemInteraction character, ItemInteraction item) {
        if (CanInteractWith(character, item)) {
            currentItem = item;
            currentPlayer = character;
            pushCount = 0;
            return true;
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
