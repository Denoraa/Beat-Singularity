using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{

    private NoteData noteData;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    
    internal void Initialize(NoteData noteData)
    {
        this.noteData = noteData;

        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        if (spriteRenderer != null)
            switch(noteData.lane)
            {
                case 0:
                    spriteRenderer.color = Color.blue;
                    break;
                case 1:
                    spriteRenderer.color = Color.red;
                    break;
            }
        rb.velocity = new Vector2(-noteData.speed, 0);

    }

}
