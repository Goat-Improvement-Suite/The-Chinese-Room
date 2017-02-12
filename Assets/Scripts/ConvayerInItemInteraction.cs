using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvayerInItemInteraction : Interaction
{
    public GameColor color;

    private ItemInteraction holding;
    [SerializeField]
    public MachineItemInteraction machine;

    [SerializeField]
    public GameObject vanishPoint;

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
            if ((holding.transform.position - vanishPoint.transform.position).magnitude < 0.5f )
            {
                if (machine.StartProcessingItem(holding))
                {
                    holding.MarkAsHeldBy(machine.gameObject);
                    holding.transform.parent = machine.gameObject.transform;
                    holding.transform.position = machine.gameObject.transform.position;
                    holding = null;
                }
            }
            else
                holding.transform.position = Vector3.Lerp(holding.transform.position, vanishPoint.transform.position, 0.1f);
        }
    }

    public override bool CanInteractWith(CharacterItemInteraction player, ItemInteraction item)
    {
        return (holding == null && player != null && player.color == color && item != null && !item.hasColor(color));
    }

    public bool ReceiveItem(CharacterItemInteraction playerItemInteraction, ItemInteraction itemInteraction)
    {
        if (!holding)
        {
            holding = itemInteraction;
            holding.MarkAsHeldBy(gameObject);
            holding.transform.parent = transform;
            holding.transform.position = transform.position;
            return true;
        }
        return false;
    }
}
