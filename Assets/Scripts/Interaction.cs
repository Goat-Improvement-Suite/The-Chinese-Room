using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interaction : MonoBehaviour {
    public abstract bool CanInteractWith(CharacterItemInteraction player, ItemInteraction item);
    public abstract void Highlight(GameObject player);
    public abstract void Unhighlight(GameObject player);
}
