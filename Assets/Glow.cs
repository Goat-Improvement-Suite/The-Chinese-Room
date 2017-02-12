using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glow : MonoBehaviour {
    public SpriteRenderer spriteRenderer;
    public float minScale;
    public float maxScale;
    public Color startColor;
    public Color endColor;

    public bool glowing;
    public float pulseSpeed;
    private float glowStartTime;
    private bool wasGlowing = false;
	
	void Start () {		
	}
	
	void Update () {
	    if (glowing) {
            if (!wasGlowing) {
                spriteRenderer.enabled = true;
                glowStartTime = Time.time;
            }

            float t = Mathf.Sin(pulseSpeed * (Time.time - glowStartTime));

            // Color
            spriteRenderer.color = Color.Lerp(startColor, endColor, t);

            // Scale
            float range = Mathf.Clamp(maxScale - minScale, 0, maxScale) / 2f;
            float newScale = (minScale + maxScale) / 2f + range * t;
            transform.localScale = new Vector3(newScale, newScale, 1);
        } else {
            if (wasGlowing) {
                spriteRenderer.enabled = false;
            }
        }
        wasGlowing = glowing;
	}
}
