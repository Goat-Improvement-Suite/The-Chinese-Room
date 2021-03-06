﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorItemInteraction : MonoBehaviour {

    public GameController gameController;
    private int time = 0;
    public int period;
    public int startDelay;
  //  public int rotationPeriod;

    private float x, y;
    public float muzzleSpeed;
    private float muzzleBearing; //radians
    private Vector2 movement;

    public Transform item;

    // Use this for initialization
    void Start () {
        Transform t = gameObject.GetComponent<Transform>();
        muzzleSpeed *= (12.5f * t.lossyScale.y)/100;

        UpdateBearings();       

        time = period - startDelay;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate() {
        /*Transform t = gameObject.GetComponent<Transform>();
        t.RotateAround(new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 1f), 1f/rotationPeriod);
        
         UpdateBearings();*/

        time++;
        bool spawnWithAllColors = false;
        bool doSpawn = false;
        int numberOfPapers = GameObject.FindGameObjectsWithTag("Paper").Length;
        if (numberOfPapers == 0 && gameController.getScore() == 0) {
            doSpawn = true;
            spawnWithAllColors = true;
        }
        if (numberOfPapers < gameController.getScore()) {
            doSpawn = true;
        }
        if (doSpawn && time <= period) {
            doSpawn = false;
        }

        if (doSpawn) {
            time = 0;
            //generate new item
            Transform t1 = Instantiate(item, new Vector3(x, y, 0), Quaternion.identity);
            t1.GetComponent<Rigidbody2D>().AddForce(movement);
            if (spawnWithAllColors) {
                var item = t1.GetComponent<ItemInteraction>();
                item.red = true;
                item.green = true;
                item.blue = true;
                item.yellow = true;
            }
        }
    }

    void UpdateBearings() {
        Transform t = gameObject.GetComponent<Transform>();

        muzzleBearing = t.rotation.ToEuler().z;

        x = t.position.x - Mathf.Sin(muzzleBearing) * (t.lossyScale.y / 2);
        y = t.position.y + Mathf.Cos(muzzleBearing) * (t.lossyScale.y / 2);

        movement = new Vector2(Mathf.Sin(muzzleBearing) * muzzleSpeed, -Mathf.Cos(muzzleBearing) * muzzleSpeed);
    }
}
