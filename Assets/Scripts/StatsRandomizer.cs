using FPP;
using NaughtyAttributes;
using UnityEngine;

public class StatsRandomizer : MonoBehaviour
{
    public event System.Action<Stat[]> OnStatsChanged;


    [Required]
    public Battler playerBattler;
    [Required]
    public PersonController playerController;


    public float MaxPossibleStatValue => strategy.MaxStatValue;

    [SerializeField]
    private RandomizeStrategy strategy;

    [SerializeField]
    private Stat[] stats;

    private void Start()
    {
        RandomizeStats();
    }

    public void RandomizeStats()
    {
        stats = strategy.ChangeStats(stats);

        playerBattler.Attack = stats[0].Value;
        playerBattler.Defence = stats[1].Value;
        playerController.JumpForce = stats[2].Value;
        playerController.MoveSpeed = stats[3].Value;

        OnStatsChanged?.Invoke(stats);
    }
}
