using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableItemInteraction : Interaction {
    public GameColor color;

    private ItemInteraction holding;

    void Start () {
	}

    public override void Highlight(GameObject player) {
    }

    public override void Unhighlight(GameObject player) {
    }

    void Update() {
    }

    public override bool CanInteractWith(CharacterItemInteraction player, ItemInteraction item) {
        return (holding == null && player != null && player.color == color && item != null) || 
               (holding != null && player != null && player.color == color && item == null);
    }

    public bool GiveItem(CharacterItemInteraction playerItemInteraction, ItemInteraction itemInteraction) {
        if (holding && playerItemInteraction.ReceiveItem(null, holding)) {
            holding = null;
            return true;
        }
        return false;
    }

    public bool ReceiveItem(CharacterItemInteraction playerItemInteraction, ItemInteraction itemInteraction) {
        if (!holding) {
            holding = itemInteraction;
            holding.MarkAsHeldBy(gameObject);
            holding.transform.parent = transform;
            holding.transform.position = transform.position;
            return true;
        }
        return false;
    }
}
