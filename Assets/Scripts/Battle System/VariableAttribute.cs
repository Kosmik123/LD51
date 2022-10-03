using NaughtyAttributes;
using UnityEngine;

public class VariableAttribute : MonoBehaviour
{
    public event System.Action<int> OnValueChanged;

    [SerializeField]
    private int maxValue = 100;
    public int MaxValue => maxValue;

    [SerializeField, ReadOnly]
    private int value;
    public int Value => value;

    private void Start()
    {
        FullRestore();
    }

    public void FullRestore()
    {
        Change(maxValue);
    }

    public void Change(int change)
    {
        value = Mathf.Clamp(value + change, 0, maxValue);
        OnValueChanged?.Invoke(value);
    }
}
