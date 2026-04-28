using System.Collections.Generic;

public static class GameEvents
{
    //BUTTON EVENTS
    public struct DownHitEvent { }

    public struct TopHitEvent { }

    public struct DownHoldStartEvent { }
    public struct DownHoldEndEvent { }

    public struct TopHoldStartEvent { }
    public struct TopHoldEndEvent { }
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
        public DifficultyConfigSO difficultyConfig;
        public NoteSpawnEvent(NoteData noteData, DifficultyConfigSO difficultyConfig)
        {
            this.noteData = noteData;
            this.difficultyConfig = difficultyConfig;
        }
    }
    //LEVEL EVENTS
}