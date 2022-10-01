using UnityEngine;

public class StatsRandomizer : MonoBehaviour
{
    public event System.Action<Stat[]> OnStatsChanged;

    [SerializeField]
    private CharacterStats characterStats;
    public CharacterStats CharacterStats => characterStats;

    public float MaxPossibleStatValue => strategy.MaxStatValue;

    [SerializeField]
    private RandomizeStrategy strategy;

    private void Start()
    {
        RandomizeStats();
    }

    public void RandomizeStats()
    {
        var stats = characterStats.Stats;
        strategy.ChangeStats(ref stats);
        OnStatsChanged?.Invoke(characterStats.Stats);
    }
}
