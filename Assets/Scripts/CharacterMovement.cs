using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    public float maxPower;             //Floating point variable to store the player's movement speed.
    public float maxSpeed;

    public int playerNo;

    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

    private bool ignoreInput = false;

    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start () {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.DrawLine(transform.position, transform.position + (Vector3)rb2d.velocity);
        spriteRenderer.sortingOrder = -Mathf.RoundToInt(transform.position.y * 10);
    }

    private void FixedUpdate() {
        if (!ignoreInput) {

            //Store the current horizontal input in the float moveHorizontal.
            float moveHorizontal = Input.GetAxisRaw("Horizontal" + "_" + playerNo);

            //Store the current vertical input in the float moveVertical.
            float moveVertical = Input.GetAxisRaw("Vertical" + "_" + playerNo);

            //Use the two store floats to create a new Vector2 variable movement.
            Vector2 movement = Vector2.ClampMagnitude(new Vector2(moveHorizontal, moveVertical), 1);

            //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
            if (movement != Vector2.zero)
            {
                Vector2 force = maxPower * movement;
                float speed = rb2d.velocity.magnitude;
                Vector2.ClampMagnitude(force, maxSpeed - speed);
                rb2d.AddForce(force);
            }
        }
        //Debug.DrawLine(transform.position, transform.position + (Vector3)movement, Color.red);
    }

    internal void StartIgnoringInput() {
        ignoreInput = true;
    }

    internal void StopIgnoringInput() {
        ignoreInput = false;
    }
}
