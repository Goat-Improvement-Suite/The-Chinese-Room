using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameColor
{
    Blue,
    Red,
    Green,
    Yellow
}

public class CharacterItemInteraction : Interaction
{
    public float interactionRadius;

    private LayerMask itemLayerMask, machineLayerMask, deskLayerMask, binLayerMask, holeLayerMask, playerLayerMask, inputConvayorLayerMask;

    internal int playerNo;
    public GameColor color;
    public Transform holdPoint;

    protected GameObject interactingWith;
    protected ItemInteraction holding;

    private Interaction hotInteraction;
    private Color savedInteractionSpriteColor; // Placeholder
    private const float playerInteractCooldown = 0.3f;
    private float lastPlayerInteractTime = -playerInteractCooldown;

    private CharacterMovement movement;


    [SerializeField] private Sprite aPrompt;
    [SerializeField] private Sprite aPrompt2;

    private GameObject buttonPrompt;
    void Start()
    {
        itemLayerMask = LayerMask.GetMask("Items");
        machineLayerMask = LayerMask.GetMask("Machines");
        deskLayerMask = LayerMask.GetMask("Desks");
        binLayerMask = LayerMask.GetMask("Bins");
        holeLayerMask = LayerMask.GetMask("Hole");
        playerLayerMask = LayerMask.GetMask("Players");
        inputConvayorLayerMask = LayerMask.GetMask("MachineConveyor");
        playerNo = GetComponent<CharacterMovement>().playerNo;
        movement = GetComponent<CharacterMovement>();
    }

    public override void Highlight(GameObject player) { }
    public override void Unhighlight(GameObject player) { }

    private Collider2D FindHotInteraction()
    {
        LayerMask[] masks = new LayerMask[] { itemLayerMask, playerLayerMask, machineLayerMask, deskLayerMask, holeLayerMask, binLayerMask, inputConvayorLayerMask };
        foreach (LayerMask mask in masks)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactionRadius, mask);
            foreach (Collider2D collider in colliders)
            {
                if (collider.GetComponent<Interaction>().CanInteractWith(this, holding))
                {
                    return collider;
                }
            }
        }
        return null;
    }

    void Update()
    {
        if (!interactingWith)
        {
            Collider2D collider = FindHotInteraction();

            // Clear previous hot interaction
            Interaction nextHotInteraction = (collider ? collider.GetComponent<Interaction>() : null);
            if (hotInteraction && hotInteraction != nextHotInteraction)
            {
                //Stop highlighting
                hotInteraction.Unhighlight(gameObject);
                GameObject.Destroy(buttonPrompt);
                buttonPrompt = null;
            }

            // Set and handle hot interaction
            hotInteraction = nextHotInteraction;
            if (hotInteraction)
            {
                //Start highlighting
                hotInteraction.Highlight(gameObject);
                Vector3 midpoint = Vector3.Lerp(this.transform.position, hotInteraction.gameObject.transform.position, 0.5f);
                if (buttonPrompt != null)
                {
                    buttonPrompt.transform.position = midpoint;

                    if (hotInteraction.gameObject.GetComponent<MachineItemInteraction>() != null) {
                        buttonPrompt.GetComponent<ButtonPromptController>().StartFlashing();
                    } else {
                        buttonPrompt.GetComponent<ButtonPromptController>().StopFlashing();
                    }
                } else {
                    buttonPrompt = new GameObject("player_" + playerNo + " Button Prompt");
                    buttonPrompt.transform.position = midpoint;
                    SpriteRenderer buttonPromptRenderer = buttonPrompt.AddComponent<SpriteRenderer>();
                    ButtonPromptController buttonPromptController = buttonPrompt.AddComponent<ButtonPromptController>();
                    buttonPromptController.primary = aPrompt;
                    buttonPromptController.secondary = aPrompt2;
                    buttonPromptRenderer.sortingLayerName = "Prompts";
                    buttonPrompt.transform.localScale = Vector3.Lerp(buttonPrompt.transform.localScale, Vector3.zero, 0.5f);
                }

            }

            if (collider)
            {
                // Process action
                if (collider.GetComponent<CharacterItemInteraction>())
                {
                    // Could give player an item
                    if (Input.GetButtonDown("Interact_" + playerNo))
                    {
                        if (holding) {
                            // Try to give item to other player
                            if (collider.GetComponent<CharacterItemInteraction>().ReceiveItemFromPlayer(this, holding)) {
                                lastPlayerInteractTime = Time.time;
                                holding = null;
                            } else {
                                // This should not happen
                                Debug.Log("Warning: Could not give item");
                            }
                        } else {
                            // Try to take item from other player
                            if (collider.GetComponent<CharacterItemInteraction>().GiveItemToPlayer(this)) {
                                lastPlayerInteractTime = Time.time;
                            } else {
                                // This should not happen
                                Debug.Log("Warning: Could not take item");
                            }
                        }
                    }
                }
                else if (collider.GetComponent<TableItemInteraction>())
                {
                    // Could put item on table slot
                    if (Input.GetButtonDown("Interact_" + playerNo))
                    {
                        if (holding)
                        {
                            if (collider.GetComponent<TableItemInteraction>().ReceiveItem(this, holding))
                            {
                                holding = null;
                            }
                            else
                            {
                                // This should not happen
                                Debug.Log("Warning: Could not put item on table");
                            }
                        }
                        else
                        {
                            if (!collider.GetComponent<TableItemInteraction>().GiveItem(this, holding))
                            {
                                // This should not happen
                                Debug.Log("Warning: Could not pick item up from table");
                            }
                        }
                    }
                }
                else if (collider.GetComponent<ConvayerInItemInteraction>())
                {
                    // Could put item on table slot
                    if (Input.GetButtonDown("Interact_" + playerNo))
                    {
                        if (holding)
                        {
                            if (collider.GetComponent<ConvayerInItemInteraction>().ReceiveItem(this, holding))
                            { holding = null; }
                            else
                            {
                                // This should not happen
                                Debug.Log("Warning: Could not get item on Convayor");
                            }
                        }
                        else
                        {
                        }
                    }
                }
                else if (collider.GetComponent<ConvayerOutItemInteraction>())
                {
                    // Could put item on table slot
                    if (Input.GetButtonDown("Interact_" + playerNo))
                    {
                        if (!holding)
                        {
                            if (!collider.GetComponent<ConvayerOutItemInteraction>().GiveItem(this, holding))
                            {

                                // This should not happen
                                Debug.Log("Warning: Could not put item on Convayor");
                            }
                            else
                            {
                            }
                        }
                        else
                        {
                        }
                    }
                }
                else if (collider.GetComponent<MachineItemInteraction>())
                {
                    // Could process an item in a machine
                    if (Input.GetButtonDown("Interact_" + playerNo))
                    {
                        if (!holding)
                        {
                            if (collider.GetComponent<MachineItemInteraction>().StartProcessingItem(this))
                            {
                                interactingWith = collider.gameObject;
                                movement.StartIgnoringInput();
                            }
                            else
                            {
                                // This should not happen
                                Debug.Log("Warning: Could not use machine");
                            }
                        }
                        else
                        {
                            // This should not happen
                            Debug.Log("Warning: Was holding an item when I shouldn't be");
                        }
                    }
                }
                else if (collider.GetComponent<BinItemInteraction>())
                {
                    // Could pick up an item
                    if (Input.GetButtonDown("Interact_" + playerNo))
                    {
                        if (holding)
                        {
                            collider.GetComponent<BinItemInteraction>().DestroyItem(this, holding);
                            holding = null;
                        }
                        else
                        {
                            // This should not happen
                            Debug.Log("Warning: Wasn't holding an item when I should be");
                        }
                    }
                }
                else if (collider.GetComponent<HoleItemInteraction>())
                {
                    // Could score a complete item
                    if (Input.GetButtonDown("Interact_" + playerNo))
                    {
                        if (holding)
                        {
                            if (collider.GetComponent<HoleItemInteraction>().ScoreItem(this, holding))
                            {
                                holding = null;
                            }
                        }
                        else
                        {
                            // This should not happen
                            Debug.Log("Warning: Wasn't holding an item when I should be");
                        }
                    }
                }
                else if (collider.GetComponent<ItemInteraction>())
                {
                    // Could pick up an item
                    if (Input.GetButtonDown("Interact_" + playerNo))
                    {
                        if (!holding)
                        {
                            if (!ReceiveItem(null, collider.GetComponent<ItemInteraction>()))
                            {
                                // This should not happen
                                Debug.Log("Warning: Couldn't pick up item (already holding one?)");
                            }
                        }
                        else
                        {
                            // This should not happen
                            Debug.Log("Warning: Was holding an item when I shouldn't be");
                        }
                    }
                }
            }
        }
    }


    public override bool CanInteractWith(CharacterItemInteraction character, ItemInteraction item) {
        if (Time.time - lastPlayerInteractTime < playerInteractCooldown) { return false; }
        return (holding == null && item != null && interactingWith == null) ||
               (holding != null && item == null && interactingWith == null);
    }

    public bool GiveItemToPlayer(CharacterItemInteraction otherPlayer) {
        if (holding) {
            if (otherPlayer.ReceiveItem(this, holding)) {
                lastPlayerInteractTime = Time.time;
                holding = null;
                return true;
            }
        }
        return false;
    }

    public bool ReceiveItemFromPlayer(CharacterItemInteraction player, ItemInteraction item) {
        bool result = ReceiveItem(player, item);
        if (result) {
            lastPlayerInteractTime = Time.time;
        }
        return result;
    }

    public bool ReceiveItem(CharacterItemInteraction playerItemInteraction, ItemInteraction itemInteraction)
    {
        if (!holding)
        {
            holding = itemInteraction;
            KillItemPhysics(holding);
            holding.Sound();
            holding.MarkAsHeldBy(gameObject);
            holding.transform.parent = transform;
            holding.transform.position = holdPoint.position;
            return true;
        }
        return false;
    }

    public void ProcessingComplete()
    {
        interactingWith = null;
        movement.StopIgnoringInput();
    }

    internal void KillItemPhysics(ItemInteraction item)
    {
        item.GetComponent<Rigidbody2D>().isKinematic = true;
        item.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        item.GetComponent<Rigidbody2D>().angularVelocity = 0f;
    }

    public void DestroyHolding()
    {
        if (holding) {
            GameObject.Destroy(holding.gameObject);
            holding = null;
        }
    }

    public ItemInteraction getHolding()
    {
        return holding;
    }

    public bool HasCompletePaper() {
        return (holding != null && holding.hasAllColors());
    }
}
