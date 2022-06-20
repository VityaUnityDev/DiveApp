using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public bool timerActive;
    public float currentTime;

    
    private void Update()
    {
        if (timerActive)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                CheckAutoGame();
                Start();
                currentTime = startMinutes * 60;
            }
        }


        TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = timeSpan.Seconds.ToString();
    }
    
    public void StartTimer() => timerActive = true;
    private void StopTimer() => timerActive = false;

    private void Start()
    {
        currentTime = startMinutes * 60;
       
    }

}