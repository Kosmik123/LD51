using UnityEngine;
using NaughtyAttributes;

public class HealthBarController : MonoBehaviour
{
    [SerializeField, Required]
    private StatusBarController statusBar;
    [SerializeField, Required]
    private VariableAttribute health;

    private void OnEnable()
    {
        health.OnValueChanged += Refresh;
        statusBar.MaxValue = health.MaxValue;
    }

    private void Refresh(int hp)
    {
        statusBar.Value = hp;
    }

    private void OnDisable()
    {
        health.OnValueChanged -= Refresh;
    }


}
