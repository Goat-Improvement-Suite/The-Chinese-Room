using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleItemInteraction : Interaction {
    public GameController gameController;

    public CharacterItemInteraction redPlayer, bluePlayer, greenPlayer, yellowPlayer;
    public Glow glowEffect;

	void Start () {
	}

    public override void Highlight(GameObject player) {
        Debug.DrawLine(player.transform.position, transform.position);
    }

    public override void Unhighlight(GameObject player) {
    }

    void Update () {
        glowEffect.glowing = (redPlayer.HasCompletePaper() || bluePlayer.HasCompletePaper() || greenPlayer.HasCompletePaper() || yellowPlayer.HasCompletePaper());
	}

    public override bool CanInteractWith(CharacterItemInteraction player, ItemInteraction item) {
        return (item != null && item.hasAllColors());
    }

    public bool ScoreItem(CharacterItemInteraction player, ItemInteraction item) {
        if (item != null && item.hasAllColors()) {
            GetComponent<AudioSource>().Play();
            DestroyObject(item.gameObject);
            gameController.addPoint();
            return true;
        }
        return false;
    }
}
