using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField]
    private Stat[] stats;
    public Stat[] Stats => stats;

    public Stat GetStat(int index) => stats[index];

    [ContextMenu("Set Colors")]
    private void SetColors()
    {
        int count = stats.Length;
        int jump1 = 1 + count / 2;
        int jump2 = (count % 2 == 0) ? jump1 - 1 : jump1;
        float hueBase = 1f / count;
        int jump = 0;
        for (int i = 0; i < count; i++)
        {
            float hue = (jump) * hueBase % 1f;
            var color = Color.HSVToRGB(hue, 1, 1);
            stats[i].color = color;
            jump += (i % 2 == 0 ? jump1 : jump2);
        }
    }
}

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