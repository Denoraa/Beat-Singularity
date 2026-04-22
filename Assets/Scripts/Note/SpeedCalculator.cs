using UnityEngine;

public static class SpeedCalculator
{
    public static float CalculateNoteSpeed(NoteData data, Transform spawnPoint, Transform hitPoint)
    {
        if (data == null)
        {
            Debug.LogWarning("note 为空，无法计算速度");
            return 0f;
        }

        if (spawnPoint == null || hitPoint == null)
        {
            Debug.LogWarning("spawnPoint 或 hitPoint 为空，无法计算速度");
            return 0f;
        }

        if (data.spawnLeadTime <= 0f)
        {
            Debug.LogWarning("spawnLeadTime 必须大于 0");
            return 0f;
        }

        float distance = Vector3.Distance(spawnPoint.position, hitPoint.position);
        float speed = distance / data.spawnLeadTime;

        return speed;
    }
}