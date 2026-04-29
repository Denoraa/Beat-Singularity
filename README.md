###Develog


###Milestone 1

1. One Visual Scripting graph I use in my game is the Combo Manager graph. It listens for custom events such as LevelStartEvent and NoteJudgeEvent. When the level starts, the graph resets the combo and max combo values to 0 and updates the UI text. When a NoteJudgeEvent happens, the graph checks the hit result using a Switch node. If the result is Perfect or Good, it adds 1 to the combo count, else if the result is Miss or Bad, it resets combo back to 0. Then it compares the current combo with max combo and updates max combo if needed. Finally, it updates the TextMeshPro UI to display the current combo.

2. I updated my break-down by adding a state machine system for the rhythm gameplay flow. The level now moves through LoadChart, Ready, Playing, and End states. LoadChart imports the CSV chart data, Ready waits for the player to begin, Playing activates music, note spawning, hit detection, and combo tracking, and End happens when the song finishes. Each note also has its own smaller states: Spawned, Active, Hit, or Missed.

This state machine is connected to the other systems in my game. The MusicManager controls timing during the Playing state, so note spawning stays synced to the song. The input and hit systems change note states from active to hit or missed. The Combo Manager graph reacts to those hit results and updates UI feedback. The EventBus connects all of these systems together, allowing each part of the game to respond to state changes without being tightly coupled.
