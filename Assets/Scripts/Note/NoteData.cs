using System;

[Serializable]
public class NoteData
{
    public int measure;
    public float beat;
    public int lane;
    public string type;
    public float hitTime;
    public float speed;
    public float spawnLeadTime;
}