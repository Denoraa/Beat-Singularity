using System.Collections.Generic;

public static class GameEvents
{
    //BUTTON EVENTS
    public struct LeftHitEvent { }

    public struct RightHitEvent { }

    public struct LeftHoldStartEvent { }
    public struct LeftHoldEndEvent { }

    public struct RightHoldStartEvent { }
    public struct RightHoldEndEvent { }
    //BUTTON EVENTS

    //LEVEL EVENTS
    public struct LevelStartEvent
    {
        public LevelConfigSO levelConfig;

        public LevelStartEvent(LevelConfigSO levelConfig)
        {
            this.levelConfig = levelConfig;
        }
    }
    public struct LevelEndEvent { }

    public class NoteSpawnEvent
    {
        public NoteData noteData;

        public NoteSpawnEvent(NoteData noteData)
        {
            this.noteData = noteData;
        }
    }
    //LEVEL EVENTS
}