using System;

[Serializable]
public class NoteData
{
    public int measure;
    public float beat;
    public int lane;
    public NoteType type;
    public float hitTime;
    public float speed;
    public float spawnLeadTime;
    public float spawnTime;
}