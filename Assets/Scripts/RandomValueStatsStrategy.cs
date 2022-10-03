using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RandomValueStatsStrategy : RandomizeStrategy
{
    [SerializeField]
    private int maxStatValue;
    public override int MaxStatValue => maxStatValue;
    [SerializeField]
    private int minStatValue;
    [SerializeField]
    private int valuesSum;

    private readonly List<float> randomValues = new List<float>();


    [ContextMenu("Randomize")]
    public override Stat[] ChangeStats(Stat[] stats)
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
        return stats;
    }
}
