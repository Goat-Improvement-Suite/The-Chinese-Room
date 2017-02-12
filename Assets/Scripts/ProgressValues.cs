using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressValues : MonoBehaviour
{

    public float current, max;
    public bool learp = true;

    internal float getProgress()
    {
        return (max != 0) ? Math.Abs(current / max) : 1f;
    }

}
