using UnityEngine;

public class SlidersUIController : MonoBehaviour
{
    [SerializeField]
    private StatusBarController statusBarPrefab;

    [SerializeField]
    private StatsRandomizer statsRandomizer;

    [SerializeField]
    private StatusBarController[] sliders;

    private void OnEnable()
    {
        statsRandomizer.OnStatsChanged += RefreshSliders;
    }

    private void RefreshSliders(Stat[] stats)
    {
        int count = Mathf.Min(sliders.Length, stats.Length);
        for (int i = 0; i < count; i++)
        {
            sliders[i].Value = (float)stats[i].Value;
            sliders[i].MaxValue = statsRandomizer.MaxPossibleStatValue;
        }
    }

    private void OnDisable()
    {
        statsRandomizer.OnStatsChanged -= RefreshSliders;   
    }
}
