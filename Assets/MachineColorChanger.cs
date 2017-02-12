using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineColorChanger : MonoBehaviour {
    public SpriteRenderer[] machineBacks;
    public SpriteRenderer[] machineFrames;
    public ConvayerInItemInteraction inputConveyer;
    public ConvayerOutItemInteraction outputConveyer;

    public Sprite redMachineBack;
    public Sprite blueMachineBack;
    public Sprite greenMachineBack;
    public Sprite yellowMachineBack;

    public Sprite redMachineFrame;
    public Sprite blueMachineFrame;
    public Sprite greenMachineFrame;
    public Sprite yellowMachineFrame;

    public void ChangeColor(GameColor newColor) {
        GetComponent<MachineItemInteraction>().color = newColor;
        foreach (var machineBack in machineBacks) {
            machineBack.sprite = machineBackForColor(newColor);
        }
        foreach (var machineFrame in machineFrames) {
            machineFrame.sprite = machineFrameForColor(newColor);
        }
        inputConveyer.color = newColor;
        outputConveyer.color = newColor;
    }

    public Sprite machineBackForColor(GameColor color) {
        switch (color) {
            case GameColor.Red: return redMachineBack;
            case GameColor.Blue: return blueMachineBack;
            case GameColor.Green: return greenMachineBack;
            case GameColor.Yellow: return yellowMachineBack;
        }
        return blueMachineBack;
    }

    public Sprite machineFrameForColor(GameColor color) {
        switch (color) {
            case GameColor.Red: return redMachineFrame;
            case GameColor.Blue: return blueMachineFrame;
            case GameColor.Green: return greenMachineFrame;
            case GameColor.Yellow: return yellowMachineFrame;
        }
        return blueMachineBack;
    }
}
