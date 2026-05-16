# Beat Singularity


## Milestone 2 Devlog

### Feature Status

- Milestone 1 basic gameplay still works: notes spawn from chart data, travel to the hit point, and can be judged by timing.
- The game uses Unity's new Input System through `Assets/InputMap.inputactions` and `Assets/Scripts/Controllers/InputController.cs`.
- The level flow still works as a state flow: `LoadChart -> Ready -> Playing -> End` in `Assets/Scripts/Level/LevelFlowManager.cs`.
- The Unity system I want graded is Visual Scripting. The combo graph is at `Assets/Graphs/ComboManagerGraph.asset` and is used by the `ComboManagerVS` object in `Assets/Scenes/VSScene.unity`.
- The complicating gameplay factor is the Black Hole / Fever mechanic. Hitting a Black Hole note starts Fever mode through `Assets/Scripts/Fever/FeverManager.cs`, using `Assets/Scripts/ScriptableObject/FeverConfigSO.cs` for duration, score multiplier, judgement window multiplier, and camera background color.
- MS1 feedback improvements: controls are written here for the Itch page, the WebGL default size is smaller, hit buttons show key labels, same-frame multi-key input on one lane is filtered, note speed uses the real lane spawn point to hit point distance, and README headers are formatted correctly.

### Devlog Q1 - Before Coding

Feature summary: I am building the Black Hole / Fever mechanic. Black Hole notes are special notes in the chart. When the player hits one, the game enters Fever mode for a short time, changes the camera background, gives a score multiplier, and makes timing slightly more forgiving.

1. Add a special Black Hole note type.
   - Add `BlackHole` to the note type enum.
   - Make the CSV/chart parser recognize Black Hole note rows.
   - Assign a separate Black Hole prefab in the note spawner.
   - Test by running the level and confirming a Black Hole note appears in the correct lane.

2. Build the Fever mode response.
   - Create a `FeverConfigSO` so the duration, score multiplier, judgement multiplier, and background color can be edited in Unity.
   - Create a `FeverManager` that can start and stop Fever mode.
   - Trigger Fever mode when a Black Hole note receives a successful hit result.
   - Test by hitting a Black Hole note and confirming the background changes temporarily.

3. Connect Fever mode to gameplay systems.
   - Make `ScoreManager` read the current Fever score multiplier.
   - Make judgement windows read the Fever judgement multiplier.
   - Stop Fever mode when the level ends.
   - Test by comparing score gain and hit timing during normal play versus Fever mode.

### Devlog Q2 - After Coding

The task break-down helped because it gave me a simple order: first make the special note visible, then make it trigger Fever, then connect Fever to scoring and judgement. If I did it again, 
I would make each test even more specific, such as writing the exact chart row I expect to spawn a Black Hole note and the exact score value I expect during Fever mode.

### Devlog Q3 - Visual Scripting and Code Bridge

I bridge Visual Scripting and code with `Assets/Scripts/VisualScripting/EventBusVisualScriptingBridge.cs`. That C# script listens for code-side `GameEvents.LevelStartEvent` and `GameEvents.NoteJudgeEvent`, 
then sends Visual Scripting custom events named `LevelStartEvent` and `NoteJudgeEvent` into the graph. The purpose is to keep core gameplay logic in C# while letting the combo UI logic live in the Visual Scripting graph.

[Combo Manager Visual Scripting graph](https://github.com/user-attachments/assets/f6d4efa2-8d38-4608-ade8-cd60e6faf6de)

### Devlog Q4 - Unity System to Grade

Please grade the Visual Scripting system. It is the Combo Manager graph at `Assets/Graphs/ComboManagerGraph.asset`, connected through `Assets/Scripts/VisualScripting/EventBusVisualScriptingBridge.cs` and used in `Assets/Scenes/VSScene.unity`.
