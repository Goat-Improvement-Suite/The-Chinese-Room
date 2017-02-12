using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvayerOutItemInteraction : Interaction
{
    public GameColor color;

    private ItemInteraction holding;

    [SerializeField]
    public GameObject endPoint;

    [SerializeField]
    public GameObject startPoint;

    void Start()
    {
    }

    public override void Highlight(GameObject player)
    {
    }
    
    public override void Unhighlight(GameObject player)
    {
    }

    void Update()
    {
        if (holding)
        {
            if ((holding.transform.position - endPoint.transform.position).magnitude > 0.5f)
                holding.transform.position = Vector3.Lerp(holding.transform.position, endPoint.transform.position, 0.1f);
           
           
        }
    }

    public override bool CanInteractWith(CharacterItemInteraction player, ItemInteraction item)
    {
        return (holding != null && player != null && player.color == color && item == null);
    }

    public bool GiveItem(CharacterItemInteraction playerItemInteraction, ItemInteraction itemInteraction)
    {
        if (holding && playerItemInteraction.ReceiveItem(null, holding))
        {
            holding = null;
            return true;
        }
        return false;
    }
    public bool isReady()
    {
        return holding == null;
    }
    public bool ReceiveItem(ItemInteraction itemInteraction)
    {

        Debug.LogWarning("atempting to grab");
        if (!holding)
        {
            Debug.LogWarning("grabbed");

            holding = itemInteraction;
            holding.MarkAsHeldBy(gameObject);
            holding.transform.parent = transform;
            holding.transform.position = startPoint.transform.position;
            return true;
        }
        return false;
    }
}
