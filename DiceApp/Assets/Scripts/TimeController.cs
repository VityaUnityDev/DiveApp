using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField] private UIView _uiView;
    [SerializeField] float startMinutes = 0.1f;
    public bool timerActive;
    public float currentTime;

    private void Start() => UpdateTime();
  
    private void Update()
    {
        if (timerActive)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                _uiView.CheckAutoGame();
                Start();
                currentTime = startMinutes * 60;
            }
        }


        TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
        _uiView.UpdateTimeText(timeSpan);
    }

    public void StartTimer() => timerActive = true;
    public void StopTimer() => timerActive = false;

    public void UpdateTime()
    {
        currentTime = startMinutes * 60;
    }

  
    
}