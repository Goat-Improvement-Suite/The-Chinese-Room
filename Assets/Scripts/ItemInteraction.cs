using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : Interaction {

    [SerializeField] internal bool blue = false;
    [SerializeField] internal bool green = false;
    [SerializeField] internal bool red = false;
    [SerializeField] internal bool yellow = false;
    internal GameObject heldBy;

    private SpriteRenderer outlineSpriteRenderer;

    void Start() {
        var outlineObject = transform.Find("Outline");
        if (outlineObject) {
            outlineSpriteRenderer = outlineObject.GetComponent<SpriteRenderer>();
        }
   	}
	
	void Update () {	
	}

    protected override void Awake() { }

    public override void Highlight() {
        outlineSpriteRenderer.enabled = true;
    }

    public override void Unhighlight() {
        outlineSpriteRenderer.enabled = false;
    }

    public override bool CanInteractWith(CharacterItemInteraction character, ItemInteraction item) {
        return (heldBy == null && character != null);
    }

    internal void MarkAsHeldBy(GameObject obj) {
        heldBy = obj;
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
                break;
            case GameColor.Red:
                red = true;
                break;
            case GameColor.Yellow:
                yellow = true;
                break;
            case GameColor.Green:
                green = true;
                break;
        }
    }
}
