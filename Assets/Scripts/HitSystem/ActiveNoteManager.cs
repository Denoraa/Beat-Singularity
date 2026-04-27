using System.Collections.Generic;
using UnityEngine;

public class ActiveNoteManager :Singleton<ActiveNoteManager>
{

    private List<Note> activeNotes = new List<Note>();

    public void RegisterNote(Note note)
    {
        if (!activeNotes.Contains(note))
            activeNotes.Add(note);
    }

    public void UnregisterNote(Note note)
    {
        activeNotes.Remove(note);
    }

    public List<Note> GetActiveNotes()
    {
        return activeNotes;
    }
}