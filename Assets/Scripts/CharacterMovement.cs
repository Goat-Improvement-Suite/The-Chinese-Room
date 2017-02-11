﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    public float speed;             //Floating point variable to store the player's movement speed.
    public float maxSpeed;

    public int playerNo;

    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

    // Use this for initialization
    void Start () {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate() {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxisRaw("Horizontal" + "_" + playerNo);

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxisRaw("Vertical" + "_" + playerNo);

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        if (rb2d.velocity.magnitude < maxSpeed && movement != Vector2.zero) {
           rb2d.AddForce(movement * speed);
        }

        //Debug.DrawLine(transform.position, transform.position + (Vector3)movement, Color.red);
        //Debug.DrawLine(transform.position, transform.position + (Vector3)rb2d.velocity);
    }
}
