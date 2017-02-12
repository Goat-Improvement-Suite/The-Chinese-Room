using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleItemInteraction : Interaction {
    public GameController gameController;

	void Start () {
	}

    public override void Highlight(GameObject player) {
        Debug.DrawLine(player.transform.position, transform.position);
    }

    public override void Unhighlight(GameObject player) {
    }

    void Update () {
	}

    public override bool CanInteractWith(CharacterItemInteraction player, ItemInteraction item) {
        return (item != null && item.hasAllColors());
    }

    public bool ScoreItem(CharacterItemInteraction player, ItemInteraction item) {
        if (item != null && item.hasAllColors()) {
            DestroyObject(item.gameObject);
            gameController.addPoint();
            return true;
        }
        return false;
    }
}
