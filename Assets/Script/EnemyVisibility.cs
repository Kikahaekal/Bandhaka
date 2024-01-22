using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteVisibility : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    // Assuming InAlive is a boolean variable controlling the visibility
    public bool InAlive = true;

    private void Start()
    {
        // Get the SpriteRenderer component attached to the same GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Check the condition and set the sprite visibility accordingly
        if (InAlive)
        {
            // If InAlive is true, make the sprite visible
            spriteRenderer.enabled = true;
        }
        else
        {
            // If InAlive is false, make the sprite invisible
            spriteRenderer.enabled = false;
        }
    }
}
