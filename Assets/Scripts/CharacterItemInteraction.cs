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

    internal int playerNo;
    internal GameColor color;

    protected GameObject interactingWith;
    protected ItemInteraction holding;

    private CharacterMovement movement;

    void Start() {
        itemLayerMask = LayerMask.GetMask("Items");
        machineLayerMask = LayerMask.GetMask("Machines");
        deskLayerMask = LayerMask.GetMask("Desks");
        binLayerMask = LayerMask.GetMask("Bins");
        holeLayerMask = LayerMask.GetMask("Hole");
        playerLayerMask = LayerMask.GetMask("Players");
        playerNo = GetComponent<CharacterMovement>().playerNo;
        movement = GetComponent<CharacterMovement>();
    }

	void Update () {
        if (!interactingWith) {
            if (holding) {
                // Check for interaction with: Player, Machine, Desk, Hole or Bin
                if (Input.GetButtonDown("Interact_" + playerNo)) {
                    Collider2D collider = null;
                    if ((collider = Physics2D.OverlapCircle(transform.position, interactionRadius, playerLayerMask)) &&
                        collider.gameObject.GetComponent<CharacterItemInteraction>().GiveItem(this, holding))
                    {
                        holding = null;
                    //} else if (collider = Physics2D.OverlapCircle(transform.position, interactionRadius, machineLayerMask) &&
                    //           collider.gameObject.GetComponent<MachineItemInteraction>().StartProcessingItem(this, holding))
                    //{
                    //    holding = null;
                    //    interactingWith = collider.gameObject;
                    //    movement.StartIgnoringInput();
                    //}
                    //else if (collider = Physics2D.OverlapCircle(transform.position, interactionRadius, deskLayerMask) &&
                    //         collider.gameObject.GetComponent<DeskInteraction>().PlaceItem(this, holding))
                    //{
                    //    holding = null;
                    //}
                    //else if (collider = Physics2D.OverlapCircle(transform.position, interactionRadius, holeLayerMask) &&
                    //         collider.gameObject.GetComponent<HoleInteraction>().ScoreItem(this, holding))
                    //{
                    //    holding = null;
                    //}
                    //else if (collider = Physics2D.OverlapCircle(transform.position, interactionRadius, binLayerMask) &&
                    //         collider.gameObject.GetComponent<BinInteraction>().DestroyItem(this, holding))
                    //{
                    //    holding = null;
                    }
                }
            } else {
                if (Input.GetButtonDown("Interact_" + playerNo)) {
                    Collider2D collider = null;
                    if ((collider = Physics2D.OverlapCircle(transform.position, interactionRadius, itemLayerMask)) &&
                        (collider.gameObject.GetComponent<ItemInteraction>().heldBy == null))
                    {
                        holding = collider.gameObject.GetComponent<ItemInteraction>();
                        holding.heldBy = this.gameObject;
                        collider.transform.parent = transform;
                    }
                }

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

    internal bool GiveItem(CharacterItemInteraction playerItemInteraction, ItemInteraction itemInteraction) {
        if (!holding) {
            holding = itemInteraction;
            return true;
        }
        return false;
    }

    void ProcessingComplete(ItemInteraction item) {
        interactingWith = null;
        movement.StopIgnoringInput();
        holding = item;
    }
}
