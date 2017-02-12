using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour {
    public GameObject floor;
    private SpriteRenderer floorRenderer;
    public GameObject wall;
    private SpriteRenderer wallRenderer;

    public MachineColorChanger topRightMachine;
    public MachineColorChanger topLeftMachine;
    public MachineColorChanger bottomLeftMachine;
    public MachineColorChanger bottomRightMachine;

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
            switch (current % walls.Length) {
                case 0:
                    topRightMachine.ChangeColor(GameColor.Blue);
                    topLeftMachine.ChangeColor(GameColor.Red);
                    bottomLeftMachine.ChangeColor(GameColor.Yellow);
                    bottomRightMachine.ChangeColor(GameColor.Green);
                    break;
                case 1:
                    topRightMachine.ChangeColor(GameColor.Red);
                    topLeftMachine.ChangeColor(GameColor.Yellow);
                    bottomLeftMachine.ChangeColor(GameColor.Green);
                    bottomRightMachine.ChangeColor(GameColor.Blue);
                    break;
                case 2:
                    topRightMachine.ChangeColor(GameColor.Yellow);
                    topLeftMachine.ChangeColor(GameColor.Green);
                    bottomLeftMachine.ChangeColor(GameColor.Blue);
                    bottomRightMachine.ChangeColor(GameColor.Red);
                    break;
                case 3:
                    topRightMachine.ChangeColor(GameColor.Green);
                    topLeftMachine.ChangeColor(GameColor.Blue);
                    bottomLeftMachine.ChangeColor(GameColor.Red);
                    bottomRightMachine.ChangeColor(GameColor.Yellow);
                    break;
            }
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
