﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineItemInteraction : Interaction {

    public GameColor color;
    private ItemInteraction currentItem;
    private CharacterItemInteraction currentPlayer;
    private int pushCount;
    private const int COUNT_LIMIT = 20;

    public SpriteRenderer[] highlightSpriteRenderers;

    void Start () {
	}
	
    protected override void Awake() { }

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
        return (character != null && character.color == color && item != null && !item.hasColor(color));
    }

    public override void Highlight() {
        foreach (var sr in highlightSpriteRenderers) {
            sr.enabled = true;
        }
    }

    public override void Unhighlight() {
        foreach (var sr in highlightSpriteRenderers) {
            sr.enabled = false;
        }
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
        currentItem.addColor(color);
        currentPlayer.ProcessingComplete(currentItem);
        currentPlayer = null;
        currentItem = null;
    }

}
