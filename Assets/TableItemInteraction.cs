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
        return (player != null && player.color == color && item != null);
    }

    public bool ReceiveItem(CharacterItemInteraction playerItemInteraction, ItemInteraction itemInteraction) {
        Debug.Log("HERE");
        if (!holding) {
            holding = itemInteraction;
            holding.transform.parent = transform;
            return true;
        }
        return false;
    }
}
