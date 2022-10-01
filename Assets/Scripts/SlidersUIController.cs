using UnityEngine;

public class SlidersUIController : MonoBehaviour
{
    [SerializeField]
    private StatusBarController statusBarPrefab;

    [SerializeField]
    private StatsRandomizer statsRandomizer;

    private StatusBarController[] sliders;

    private void OnEnable()
    {
        statsRandomizer.OnStatsChanged += RefreshSliders;
        int statsCount = statsRandomizer.StatsCount;
        sliders = new StatusBarController[statsCount];
        for(int i = 0; i < statsCount; i++)
        {
            sliders[i] = Instantiate(statusBarPrefab, transform);
            sliders[i].Color = statsRandomizer.GetStat(i).color;
        }
    }

    private void RefreshSliders(Stat[] stats)
    {
        int count = Mathf.Min(sliders.Length, stats.Length);
        int maxPossibleValue = statsRandomizer.MaxPossibleStatValue;
        for (int i = 0; i < count; i++)
        {
            sliders[i].Value = (float)stats[i].Value / maxPossibleValue;
        }
    }

    private void OnDisable()
    {
        statsRandomizer.OnStatsChanged -= RefreshSliders;   
    }
}
