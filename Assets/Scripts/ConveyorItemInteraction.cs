using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorItemInteraction : MonoBehaviour {

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
        if (time > period) {
            time = 0;
            //generate new item
            Transform t1 = Instantiate(item, new Vector3(x, y, 0), Quaternion.identity);
            t1.GetComponent<Rigidbody2D>().AddForce(movement);            
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
