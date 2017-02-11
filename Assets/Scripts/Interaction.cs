using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interaction : MonoBehaviour {
    public abstract bool CanInteractWith(CharacterItemInteraction player, ItemInteraction item);

    // Placeholder highlighting system
    Color originalColor;

    protected virtual void Awake() {
        originalColor = GetComponent<SpriteRenderer>().color;
    }

    public virtual void Highlight(GameObject player) {
        GetComponent<SpriteRenderer>().color = Color.black;
    }

    public virtual void Unhighlight(GameObject player) {
        GetComponent<SpriteRenderer>().color = originalColor;
    }
}
