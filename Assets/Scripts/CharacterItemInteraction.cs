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
    public GameColor color;

    protected GameObject interactingWith;
    protected ItemInteraction holding;

    private Interaction hotInteraction;
    private Color savedInteractionSpriteColor; // Placeholder

    private CharacterMovement movement;

    [SerializeField] private Sprite aPrompt;

    private GameObject buttonPrompt;
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
        if (!interactingWith) {
            Collider2D collider = FindHotInteraction();

            // Clear previous hot interaction
            Interaction nextHotInteraction = (collider ? collider.GetComponent<Interaction>() : null);
            if (hotInteraction && hotInteraction != nextHotInteraction) {
                //Stop highlighting
                hotInteraction.Unhighlight(gameObject);
                GameObject.Destroy(buttonPrompt);
                buttonPrompt = null;
            }

            // Set and handle hot interaction
            hotInteraction = nextHotInteraction;
            if (hotInteraction) {
                //Start highlighting
                hotInteraction.Highlight(gameObject);
                Vector3 midpoint = (this.gameObject.transform.position + this.gameObject.transform.position) / 2;
                Debug.Log(midpoint.ToString());
                if (buttonPrompt != null) {
                    buttonPrompt.transform.position = midpoint;
                } else {
                    buttonPrompt = new GameObject("player_" + playerNo + " Button Prompt");
                    buttonPrompt.transform.position = midpoint;
                    SpriteRenderer buttonPromptRenderer = buttonPrompt.AddComponent<SpriteRenderer>();
                    buttonPromptRenderer.sprite = aPrompt;
                }

            }

            if (collider) {
                // Process action
                if (collider.GetComponent<CharacterItemInteraction>()) {
                    // Could give player an item
                    if (Input.GetButtonDown("Interact_" + playerNo)) {
                        if (collider.GetComponent<CharacterItemInteraction>().ReceiveItem(this, holding)) {
                            holding = null;
                        }
                        else {
                            // This should not happen
                            Debug.Log("Warning: Could not give item");
                        }
                    }
                }
                else if (collider.GetComponent<MachineItemInteraction>()) {
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
                        }
                        else {
                            // This should not happen
                            Debug.Log("Warning: Wasn't holding an item what I should be");
                        }
                    }
                } else if (collider.GetComponent<BinItemInteraction>()) {
                    // Could pick up an item
                    if (Input.GetButtonDown("Interact_" + playerNo)) {
                        if (holding) {
                            collider.GetComponent<BinItemInteraction>().DestroyItem(this, holding);
                            holding = null;
                        }
                        else {
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
