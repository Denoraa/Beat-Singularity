using System.Collections.Generic;

public class ActiveNoteManager : Singleton<ActiveNoteManager>
{
    private List<Note> activeTopNotes = new List<Note>();
    private List<Note> activeDownNotes = new List<Note>();
    public void RegisterNote(Note note)
    {
        if (note == null) return;

        if (note.NoteData.lane == LaneType.Top)
        {
            if (!activeTopNotes.Contains(note))
                activeTopNotes.Add(note);
        }
        else
        {
            if (!activeDownNotes.Contains(note))
                activeDownNotes.Add(note);
        }
    }

    public void UnregisterNote(Note note)
    {
        if (note == null) return;

        if (note.NoteData.lane == LaneType.Top)
        {
            if (activeTopNotes.Contains(note))
                activeTopNotes.Remove(note);
        }
        else
        {
            if (activeDownNotes.Contains(note))
                activeDownNotes.Remove(note);
        }
    }

    public IReadOnlyList<Note> GetActiveNotes(LaneType lane)
    {
        if (lane == LaneType.Top)
            return activeTopNotes;
        else
            return activeDownNotes;
    }
}
