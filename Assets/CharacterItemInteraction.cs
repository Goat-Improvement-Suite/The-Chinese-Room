using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameColor {
    Blue,
    Red,
    Green,
    Yellow
}

public class CharacterItemInteraction : MonoBehaviour {
    public float interactionRadius;

    private LayerMask itemLayerMask, machineLayerMask, deskLayerMask, binLayerMask, holeLayerMask, playerLayerMask;
    private int playerNo;
    private GameColor color;

    private GameObject interactingWith;
    private GameObject holding;

    void Start() {
        itemLayerMask = LayerMask.GetMask("Items");
        machineLayerMask = LayerMask.GetMask("Machines");
        deskLayerMask = LayerMask.GetMask("Desks");
        binLayerMask = LayerMask.GetMask("Bins");
        holeLayerMask = LayerMask.GetMask("Hole");
        playerLayerMask = LayerMask.GetMask("Players");
        playerNo = GetComponent<CharacterMovement>().playerNo;
    }

	void Update () {
        if (!interactingWith) {
            if (holding) {
                // Check for interaction with: Player, Machine, Desk, Hole or Bin
                Collider2D collider;
                if (collider = Physics2D.OverlapCircle(transform.position, interactionRadius, playerLayerMask)) {

                } else if (collider = Physics2D.OverlapCircle(transform.position, interactionRadius, machineLayerMask)) {
                    //collider.gameObject.GetComponent<MachineInteraction>().StartProcessingItem(this, holding);
                    holding = null;
                    interactingWith = collider.gameObject;
                } else if (collider = Physics2D.OverlapCircle(transform.position, interactionRadius, deskLayerMask)) {
                } else if (collider = Physics2D.OverlapCircle(transform.position, interactionRadius, holeLayerMask)) {
                } else if (collider = Physics2D.OverlapCircle(transform.position, interactionRadius, binLayerMask)) {
                }
            } else {
                // Check for interaction with an Item
                if (Input.GetButtonDown("Interact_" + playerNo)) {

                    Collider2D collider = Physics2D.OverlapCircle(transform.position, interactionRadius, itemLayerMask);
                    if (collider)
                    {
                        //collider.gameObject.GetComponent<ItemInteraction>().StarPickUp(playerNo);
                    }
                }
                if (Input.GetButtonUp("Interact_" + playerNo))
                {
                    // Cancel interaction
                    //interactingWith.GetComponent<ItemInteraction>().Cancel(playerNo);
                    interactingWith = null;
                }
            }
        }
    }

    void ProcessingComplete(GameObject item) {
        interactingWith = null;
        holding = item;
    }
}
