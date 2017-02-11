using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorItemInteraction : MonoBehaviour {

    private int time, period = 0;

    private float x, y;
    public float muzzleVelocity;
    private Vector2 movement;

    public Transform item;

    // Use this for initialization
    void Start () {
        Transform t = gameObject.GetComponent<Transform>();
        x = t.position.x;
        y = (t.position.y - t.lossyScale.y/2) + item.lossyScale.y/2;

        movement = new Vector2(0, muzzleVelocity);

        period = 60;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate() {
        time++;
        if (time > period) {
            time = 0;
            //generate new item
            Transform t = Instantiate(item, new Vector3(x, y, 0), Quaternion.identity);
            t.GetComponent<Rigidbody2D>().AddForce(movement);            
        }
    }
}
