using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour {

    internal bool processed = false;
    internal GameObject heldBy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    internal bool CanBePickedUpBy(GameObject obj) {
        return (!heldBy && heldBy != obj);
    }

    internal void MarkAsHeldBy(GameObject obj) {
        heldBy = obj;
    }

    internal void MarkAsProcessedBy(MachineItemInteraction machineItemInteraction) {
        processed = true;
        gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
    }
}
