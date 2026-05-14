using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public NoteData NoteData { get; private set; }

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private bool isConsumed = false;
    internal void Initialize(NoteData noteData,DifficultyConfigSO difficulty)
    {
        this.NoteData = noteData;

        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        if (spriteRenderer != null)
            switch(noteData.type)
            {
                case NoteType.BlackHole:
                    spriteRenderer.color = new Color(0.1f, 0f, 0.25f, 1f);
                    break;
                default:
                    switch(noteData.lane)
                    {
                        case LaneType.Top:
                            spriteRenderer.color = Color.blue;
                            break;
                        case LaneType.Down:
                            spriteRenderer.color = Color.red;
                            break;
                    }
                    break;
            }
        rb.velocity = new Vector2(-noteData.speed, 0);


        StartCoroutine(LifeCycle(difficulty.badThreshold));
    }
    internal bool Consume()
    {
        if (isConsumed)
            return false;

        isConsumed = true;
        ActiveNoteManager.Instance.UnregisterNote(this);
        Destroy(gameObject);
        return true;
    }

    private IEnumerator LifeCycle(float baseBadThreshold)
    {
        ActiveNoteManager.Instance.RegisterNote(this);

        while (!isConsumed)
        {
            if (MusicManager.Instance != null && MusicManager.Instance.MusicSource != null)
            {
                float badThreshold = baseBadThreshold * FeverManager.Instance.JudgementWindowMultiplier;
                float missTime = NoteData.hitTime + badThreshold;
                if (MusicManager.Instance.MusicSource.time > missTime)
                {
                    Debug.Log("Miss");
                    EventBus.Publish(new GameEvents.NoteJudgeEvent(HitResult.Miss));
                    Consume();
                    yield break;
                }
            }

            yield return null;
        }
    }

}
