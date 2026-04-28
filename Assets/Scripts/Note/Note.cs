using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public NoteData NoteData { get; private set; }

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    
    internal void Initialize(NoteData noteData,DifficultyConfigSO difficulty)
    {
        this.NoteData = noteData;

        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        if (spriteRenderer != null)
            switch(noteData.lane)
            {
                case LaneType.Top:
                    spriteRenderer.color = Color.blue;
                    break;
                case LaneType.Down:
                    spriteRenderer.color = Color.red;
                    break;
            }
        rb.velocity = new Vector2(-noteData.speed, 0);

        float cycleTime = difficulty.badThreshold + 1f; // 确保音符在 bad 阈值后被销毁

        StartCoroutine(LifeCycle(difficulty.badThreshold));
    }

    private IEnumerator LifeCycle(float cycleTime)
    {
        ActiveNoteManager.Instance.RegisterNote(this);
        yield return new WaitForSeconds(cycleTime);
        ActiveNoteManager.Instance.UnregisterNote(this);
        Destroy(gameObject);
    }

}
