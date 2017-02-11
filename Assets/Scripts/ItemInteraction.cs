using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : Interaction {

    internal bool processed = false;
    internal GameObject heldBy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override bool CanInteractWith(CharacterItemInteraction character, ItemInteraction item) {
        return (heldBy == null && character != null);
    }

    internal void MarkAsHeldBy(GameObject obj) {
        heldBy = obj;
    }

    internal void MarkAsProcessedBy(MachineItemInteraction machineItemInteraction) {
        processed = true;
        gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
    }
}
