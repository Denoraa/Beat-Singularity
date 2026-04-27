using System.Collections.Generic;

public class ActiveNoteManager : Singleton<ActiveNoteManager>
{
    private List<Note> activeNotes = new List<Note>();

    public void RegisterNote(Note note)
    {
        if (note == null) return;

        if (!activeNotes.Contains(note))
            activeNotes.Add(note);
    }

    public void UnregisterNote(Note note)
    {
        if (note == null) return;

        activeNotes.Remove(note);
    }

    public IReadOnlyList<Note> GetActiveNotes()
    {
        return activeNotes;
    }
}
