using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameColor {
    Blue,
    Red,
    Green,
    Yellow
}

public class CharacterItemInteraction : Interaction {
    public float interactionRadius;

    private LayerMask itemLayerMask, machineLayerMask, deskLayerMask, binLayerMask, holeLayerMask, playerLayerMask;

    internal int playerNo;
    internal GameColor color;

    protected GameObject interactingWith;
    protected ItemInteraction holding;

    private Interaction hotInteraction;
    private Color savedInteractionSpriteColor; // Placeholder

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


    private Collider2D FindHotInteraction() {
        Collider2D result = null;
        if (holding) {
            LayerMask[] masks = new LayerMask[] { playerLayerMask, machineLayerMask, deskLayerMask, holeLayerMask, binLayerMask };
            foreach (LayerMask mask in masks) {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactionRadius, mask);
                foreach (Collider2D collider in colliders) {
                    if (collider && collider.GetComponent<Interaction>().CanInteractWith(this, holding)) {
                        result = collider;
                        break;
                    } else {
                        result = null;
                    }
                }
            }
        } else {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, interactionRadius, itemLayerMask);
            if (collider && collider.GetComponent<Interaction>().CanInteractWith(this, holding)) {
                result = collider;
            } else {
                result = null;
            }
        }
        return result;
    }

    void Update() {
        if (playerNo == 0) {
            for (int a = 0; a < 360; a += 5) {
                Debug.DrawLine(transform.position, transform.position + Quaternion.Euler(0, 0, a) * new Vector3(0, interactionRadius, 0));
            }
        }
        if (!interactingWith) {
            Collider2D collider = FindHotInteraction();

            // Clear previous hot interaction
            Interaction nextHotInteraction = (collider ? collider.GetComponent<Interaction>() : null);
            if (hotInteraction && hotInteraction != nextHotInteraction) {
                hotInteraction.Unhighlight();
            }

            // Set and handle hot interaction
            hotInteraction = nextHotInteraction;
            if (hotInteraction && hotInteraction.GetComponent<SpriteRenderer>()) {
                hotInteraction.Highlight();
            }

            if (collider) {
                // Process action
                if (collider.GetComponent<CharacterItemInteraction>()) {
                    // Could give player an item
                    if (Input.GetButtonDown("Interact_" + playerNo)) {
                        if (collider.GetComponent<CharacterItemInteraction>().ReceiveItem(this, holding)) {
                            holding = null;
                        } else {
                            // This should not happen
                            Debug.Log("Warning: Could not give item");
                        }
                    }
                } else if (collider.GetComponent<MachineItemInteraction>()) {
                    // Could process an item in a machine
                    if (Input.GetButtonDown("Interact_" + playerNo)) {
                        if (holding) {
                            if (collider.GetComponent<MachineItemInteraction>().StartProcessingItem(this, holding)) {
                                holding = null;
                                interactingWith = collider.gameObject;
                                movement.StartIgnoringInput();
                            }
                            else {
                                // This should not happen
                                Debug.Log("Warning: Could not use machine");
                            }
                        } else {
                            // This should not happen
                            Debug.Log("Warning: Wasn't holding an item what I should be");
                        }
                    }
                } else if (collider.GetComponent<ItemInteraction>()) {
                    // Could pick up an item
                    if (Input.GetButtonDown("Interact_" + playerNo)) {
                        if (!holding) {
                            holding = collider.GetComponent<ItemInteraction>();
                            KillItemPhysics(holding);
                            holding.MarkAsHeldBy(gameObject);
                            collider.transform.parent = transform;
                        } else {
                            // This should not happen
                            Debug.Log("Warning: Wasn't holding an item what I should be");
                        }
                    }
                }
            }
        }
    }

    public override bool CanInteractWith(CharacterItemInteraction character, ItemInteraction item) {
        return (holding == null && item != null);
    }

    internal bool ReceiveItem(CharacterItemInteraction playerItemInteraction, ItemInteraction itemInteraction) {
        if (!holding) {
            holding = itemInteraction;
            KillItemPhysics(holding);
            holding.transform.parent = transform;
            return true;
        }
        return false;
    }

    internal void ProcessingComplete(ItemInteraction item) {
        interactingWith = null;
        movement.StopIgnoringInput();
        holding = item;
    }

    internal void KillItemPhysics(ItemInteraction item) {
        item.GetComponent<Rigidbody2D>().isKinematic = true;
        item.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        item.GetComponent<Rigidbody2D>().angularVelocity = 0f;
    }
}
