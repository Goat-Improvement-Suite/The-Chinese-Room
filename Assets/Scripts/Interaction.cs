using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interaction : MonoBehaviour {
    public abstract bool CanInteractWith(CharacterItemInteraction characterItemInteraction, ItemInteraction itemInteraction);

    // Placeholder highlighting system
    Color originalColor;

    void Awake() {
        originalColor = GetComponent<SpriteRenderer>().color;
    }

    public void Highlight() {    
        GetComponent<SpriteRenderer>().color = Color.black;
    }

    public void Unhighlight() {
        GetComponent<SpriteRenderer>().color = originalColor;
    }
}
