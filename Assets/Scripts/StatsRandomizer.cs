using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public string name;
    public Color color = Color.white;

    private int value;
    public int Value
    {
        get => value;
        set => this.value = value;
    }
}

public class StatsRandomizer : MonoBehaviour
{
    public event System.Action<Stat[]> OnStatsChanged;

    [SerializeField]
    private Stat[] stats;
    [SerializeField]
    private int minStatValue;
    [SerializeField]
    private int valuesSum;

    private readonly List<float> randomValues = new List<float>();

    public int MaxPossibleStatValue => valuesSum - stats.Length * minStatValue;
    public int StatsCount => stats.Length;

    public Stat GetStat(int index) => stats[index];

    private void Start()
    {
        RandomizeStats();
    }

    [ContextMenu("Randomize")]
    public void RandomizeStats()
    {
        int count = stats.Length;
        randomValues.Clear();
        float sum = 0;
        for (int i = 0; i < count; i++)
        {
            float value = Random.value;
            randomValues.Add(value);
            sum += value;
        }

        int statAddition = valuesSum - count * minStatValue;
        for (int i = 0; i < count; i++)
        {
            Stat stat = stats[i];
            stat.Value = minStatValue + Mathf.RoundToInt(statAddition * randomValues[i] / sum);
        }
        OnStatsChanged?.Invoke(stats);
    }
}
