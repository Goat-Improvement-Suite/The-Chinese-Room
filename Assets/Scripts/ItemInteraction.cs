using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : Interaction {

    [SerializeField] internal bool blue = false;
    [SerializeField] internal bool green = false;
    [SerializeField] internal bool red = false;
    [SerializeField] internal bool yellow = false;
    internal GameObject heldBy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
