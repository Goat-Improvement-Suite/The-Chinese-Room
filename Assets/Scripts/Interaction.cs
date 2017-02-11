using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interaction : MonoBehaviour {
    public abstract bool CanInteractWith(CharacterItemInteraction characterItemInteraction, ItemInteraction itemInteraction);

    // Placeholder highlighting system
    Color originalColor;

    protected virtual void Awake() {
        originalColor = GetComponent<SpriteRenderer>().color;
    }

    public virtual void Highlight() {
        GetComponent<SpriteRenderer>().color = Color.black;
    }

    public virtual void Unhighlight() {
        GetComponent<SpriteRenderer>().color = originalColor;
    }
}
