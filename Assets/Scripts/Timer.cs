using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float interval = 1;
    public float Interval { get => interval; set => interval = value; }



    private float currentTime;
    public float CurrentTime => currentTime;

    [SerializeField]
    private bool reverse;

    public UnityEvent onTick;
    public event System.Action OnTick;

    private void Update()
    {
        currentTime += reverse ? -Time.deltaTime : Time.deltaTime;
        if (IsIntervalAchieved)
        {
            currentTime = reverse ? Interval : 0;
            onTick?.Invoke();
            OnTick?.Invoke();
        }
    }

    public bool IsIntervalAchieved => reverse ? (currentTime < 0) : (currentTime > Interval);

}
