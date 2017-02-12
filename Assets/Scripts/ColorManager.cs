using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour {
    public GameObject floor;
    private SpriteRenderer floorRenderer;
    public GameObject wall;
    private SpriteRenderer wallRenderer;

    public Sprite[] walls;
    public Sprite[] floors;
    public ColorManager setCurrent(int x)
    {
        bool shouldUpdate = current != x;
        current = x;
        if (shouldUpdate)
        {
            floorRenderer.sprite = floors[current % floors.Length];
            wallRenderer.sprite = walls[current % walls.Length];
        }
        return this;
    }
    public int getCurrent()
    {
        return current;
    }
    private int current = 3;
    // Use this for initialization
    void Start ()
    {
        floorRenderer = floor.GetComponent<SpriteRenderer>();
        wallRenderer = wall.GetComponent<SpriteRenderer>();
        floorRenderer.sprite = floors[current % floors.Length];
        wallRenderer.sprite = walls[current % walls.Length];

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
