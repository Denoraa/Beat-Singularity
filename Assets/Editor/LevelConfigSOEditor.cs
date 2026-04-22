using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelConfigSO))]
public class LevelConfigSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelConfigSO levelConfig = (LevelConfigSO)target;

        GUILayout.Space(10);

        if (GUILayout.Button("Import CSV To Note List"))
        {
            if (levelConfig.csvFile == null)
            {
                Debug.LogError("csvFile 为空，先给 LevelConfigSO 指定 CSV 文件");
                return;
            }

            if (levelConfig.bpm <= 0)
            {
                Debug.LogError("BPM 必须大于 0");
                return;
            }

            levelConfig.noteDataList = ChartLoader.LoadCSV(levelConfig.csvFile, levelConfig.bpm);

            EditorUtility.SetDirty(levelConfig);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"已成功导入到 {levelConfig.name}，共 {levelConfig.noteDataList.Count} 个音符");
        }
    }
}