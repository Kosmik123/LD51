﻿using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float interval = 1;
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
            currentTime = reverse ? interval : 0;
            onTick?.Invoke();
            OnTick?.Invoke();
        }
    }

    public bool IsIntervalAchieved => reverse ? (currentTime < 0) : (currentTime > interval);
}
