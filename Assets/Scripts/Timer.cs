using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float interval = 1;
    private float currentTime;
    public float CurrentTime => currentTime;

    public UnityEvent onTick;
    public event System.Action OnTick;

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > interval)
        {
            currentTime = 0;
            onTick?.Invoke();
            OnTick?.Invoke();
        }
    }
}
