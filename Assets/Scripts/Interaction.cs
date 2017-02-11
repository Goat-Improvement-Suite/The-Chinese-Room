using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interaction : MonoBehaviour {
    public abstract bool CanInteractWith(CharacterItemInteraction characterItemInteraction, ItemInteraction itemInteraction);
}
