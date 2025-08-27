using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct Range
{
    public float min;
    public float max;

    public Range(float min, float max)
    {
        this.min = min;
        this.max = max;
    }
}

public class GetRange : MonoBehaviour
{
    [SerializeField] private List<Range> ranges = new List<Range>();

    void Awake()
    {
        // Example ranges for levels
        ranges.Add(new Range(50, 100));  // Level 0
        ranges.Add(new Range(80, 150));  // Level 1
        ranges.Add(new Range(120, 200)); // Level 2
    }

    public Range GetRangeForLevel(int level)
    {
        if (level >= 0 && level < ranges.Count)
        {
            return ranges[level];
        }
        else
        {
            Debug.LogError("Level out of range!");
            return new Range(0, 0);
        }
    }
}
