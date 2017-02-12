﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineItemInteraction : Interaction
{

    public GameColor color;
    public ConvayerOutItemInteraction convayor;
    private ItemInteraction currentItem;
    private CharacterItemInteraction currentPlayer;
    private int pushCount;
    private const int COUNT_LIMIT = 20;
    private bool flush = false;

    public SpriteRenderer[] highlightSpriteRenderers;

    void Start()
    {
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

    public override bool CanInteractWith(CharacterItemInteraction character, ItemInteraction item)
    {
        return (character != null && character.color == color && currentItem != null && !currentItem.hasColor(color) && item == null);
    }

    public override void Highlight(GameObject player)
    {
        foreach (var sr in highlightSpriteRenderers)
        {
            sr.enabled = true;
        }
    }

    public override void Unhighlight(GameObject player)
    {
        foreach (var sr in highlightSpriteRenderers)
        {
            sr.enabled = false;
        }
    }

    public bool ReadyToProcess()
    {
        return currentItem == null && convayor != null && convayor.isReady();
    }

    public bool StartProcessingItem(ItemInteraction item){
        if (currentItem == null && item != null){
            GetComponent<AudioSource>().Play();
            currentItem = item;
            pushCount = 0;
            return true;
        }
        return false;

    }

    public bool StartProcessingItem(CharacterItemInteraction character)
    {
        if (currentItem != null && CanInteractWith(character, character.getHolding()))
        {
            currentPlayer = character;
            return true;
        }
        return false;
    }

    private void complete()
    {
        //add color
        GetComponent<AudioSource>().Stop();
        currentItem.addColor(color);
        currentPlayer.ProcessingComplete();
        if (!convayor.ReceiveItem(currentItem))
        {
            Debug.LogWarning("Cant Flush Machine");
        }
        currentPlayer = null;
        currentItem = null;
    }

}
