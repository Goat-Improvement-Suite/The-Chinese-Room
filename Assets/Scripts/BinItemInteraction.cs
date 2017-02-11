using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinItemInteraction : Interaction {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void giveItem(ItemInteraction item) {
        GameObject.Destroy(item.gameObject);
    }

    public override bool CanInteractWith(CharacterItemInteraction characterItemInteraction, ItemInteraction itemInteraction) {
        return true;
    }

}
