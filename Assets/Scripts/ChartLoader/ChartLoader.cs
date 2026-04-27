using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public static class ChartLoader
{
    private const float hitOffset = 0.3f; //由于unity audio系统的延迟，音符需要提前0.3秒生成以确保准时到达
    public static List<NoteData> LoadCSV(TextAsset csvFile, int bpm)
    {
        List<NoteData> noteList = new List<NoteData>();

        Transform spawnPoint = GameObject.Find("NoteSpawner").transform;
        Transform hitPoint = GameObject.Find("HitReciver").transform;

        if (csvFile == null)
        {
            Debug.LogError("没有指定 CSV 文件");
            return noteList;
        }

        string[] lines = csvFile.text.Split('\n');

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();

            if (string.IsNullOrWhiteSpace(line))
                continue;

            string[] cells = line.Split(',');

            if (cells.Length < 4)
            {
                Debug.LogWarning($"第 {i + 1} 行数据不完整: {line}");
                continue;
            }

            NoteData note = new NoteData();
            note.measure = int.Parse(cells[0].Trim());
            note.beat = float.Parse(cells[1].Trim(), CultureInfo.InvariantCulture);
            note.lane = int.Parse(cells[2].Trim());
            note.type = ConvertType(cells[3].Trim());
            note.spawnLeadTime = float.Parse(cells[4].Trim());
            note.hitTime = ConvertToTime(note.measure, note.beat, bpm);
            note.speed = SpeedCalculator.CalculateNoteSpeed(note,spawnPoint,hitPoint);
            note.spawnTime = note.hitTime - note.spawnLeadTime;
            noteList.Add(note);
        }

        Debug.Log($"读取完成，共 {noteList.Count} 个音符");
        return noteList;
    }

    private static float ConvertToTime(int measure, float beat, int bpm)
    {
        float beatDuration = 60f / bpm;
        float measureDuration = beatDuration * 4f;

        return ((measure - 1) * measureDuration )+ ((beat - 1f) * beatDuration)+ hitOffset;
    }

    private static NoteType ConvertType(string typeStr)
    {
        switch (typeStr)
        {
            case "Tap":
                return NoteType.Tap;
            case "Hold":
                return NoteType.Hold;
            case "Speed":
                return NoteType.Speed;
            case "Blank":
                return NoteType.Blank;
            default:
                Debug.LogWarning($"未知的音符类型: {typeStr}");
                return NoteType.Tap; // 默认类型
        }
    }
}