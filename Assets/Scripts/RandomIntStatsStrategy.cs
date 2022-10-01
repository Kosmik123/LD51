using UnityEngine;

public abstract class RandomizeStrategy : ScriptableObject
{
    public abstract int MaxStatValue { get; }
    public abstract void ChangeStats(ref Stat[] stats);
}

public class RandomIntStatsStrategy : RandomizeStrategy
{
    [SerializeField]
    private int maxStatValue;
    public override int MaxStatValue => maxStatValue;
    [SerializeField]
    private int minStatValue;

    [SerializeField]
    private int statsSum;

    public override void ChangeStats(ref Stat[] stats)
    {
        int count = stats.Length;
        int remainingPoints = statsSum;
        for (int i = 0; i < count; i++)
        {
            int maxValue = Mathf.Min(remainingPoints, maxStatValue);
            int value = Random.Range(minStatValue, maxValue + 1);
            stats[i].Value = value;
            remainingPoints -= value;
        }
    }
}
