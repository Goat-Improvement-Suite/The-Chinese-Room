using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleItemInteraction : Interaction {

	void Start () {
	}

    protected override void Awake() { }

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
}
