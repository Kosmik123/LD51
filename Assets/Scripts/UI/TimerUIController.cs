using UnityEngine;
using TMPro;

public class TimerUIController : MonoBehaviour
{
    [SerializeField]
    private Timer valueTimer;

    [SerializeField]
    private Timer eventTimer;

    [SerializeField]
    private TextMeshProUGUI label;

    private void OnEnable()
    {
        eventTimer.OnTick += Refresh;
    }

    private void Refresh()
    {
        label.text = valueTimer.CurrentTime.ToString("F0");
    }

    private void OnDisable()
    {
        eventTimer.OnTick -= Refresh;
    }
}
