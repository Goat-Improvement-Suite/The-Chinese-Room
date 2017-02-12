using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinItemInteraction : Interaction {

    public Transform hinge;
    public float openAngle;
    private float angle;
    private bool openLid;

	void Start () {		
	}

    void Update() {
        if (openLid) {
            angle = (openAngle - angle > 0.1f ? Mathf.Lerp(angle, openAngle, 0.1f) : openAngle);
        } else {
            angle = (angle > 0.1f ? Mathf.Lerp(angle, 0, 0.1f) : 0);
        }
        hinge.localRotation = Quaternion.Euler(0, angle, 0);
    }

    public override void Highlight(GameObject player) {
        openLid = true;
    }

    public override void Unhighlight(GameObject player) {
        openLid = false;
    }

    public void DestroyItem(CharacterItemInteraction player, ItemInteraction item) {
        GetComponent<AudioSource>().Play();
        GameObject.Destroy(item.gameObject);
    }

    public override bool CanInteractWith(CharacterItemInteraction characterItemInteraction, ItemInteraction itemInteraction) {
        return true;
    }
}
