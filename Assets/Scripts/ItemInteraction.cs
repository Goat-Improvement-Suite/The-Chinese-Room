using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : Interaction {

    [SerializeField] internal bool blue = false;
    [SerializeField] internal bool green = false;
    [SerializeField] internal bool red = false;
    [SerializeField] internal bool yellow = false;
    internal GameObject heldBy;

    public SpriteRenderer outlineSpriteRenderer;
    public SpriteRenderer blueSymbolSpriteRenderer;
    public SpriteRenderer greenSymbolSpriteRenderer;
    public SpriteRenderer redSymbolSpriteRenderer;
    public SpriteRenderer yellowSymbolSpriteRenderer;

    void Start() {
    }

    void Update () {	
	}

    protected override void Awake() { }

    public override void Highlight(GameObject player) {
        outlineSpriteRenderer.enabled = true;
    }

    public override void Unhighlight(GameObject player) {
        outlineSpriteRenderer.enabled = false;
    }

    public override bool CanInteractWith(CharacterItemInteraction character, ItemInteraction item) {
        return (heldBy == null && character != null);
    }

    internal void MarkAsHeldBy(GameObject obj) {
        heldBy = obj;
    }

    public bool hasAllColors() {
        return (blue && red && yellow && green);
    }

    public bool hasColor(GameColor color) {
        switch (color) { 
            case GameColor.Blue:
                return blue;
            case GameColor.Red:
                return red;
            case GameColor.Yellow:
                return yellow;
            case GameColor.Green:
                return green;
        }
        return false;
    }

    public void addColor(GameColor color) {
        switch (color) {
            case GameColor.Blue:
                blue = true;
                blueSymbolSpriteRenderer.enabled = true;
                break;
            case GameColor.Red:
                red = true;
                redSymbolSpriteRenderer.enabled = true;
                break;
            case GameColor.Yellow:
                yellow = true;
                yellowSymbolSpriteRenderer.enabled = true;
                break;
            case GameColor.Green:
                green = true;
                greenSymbolSpriteRenderer.enabled = true;
                break;
        }
    }
}
