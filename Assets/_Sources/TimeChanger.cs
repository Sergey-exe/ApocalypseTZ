using System;
using UnityEngine;

public class TimeChanger : MonoBehaviour
{
    public void StartTime()
    {
        Time.timeScale = 1;
    }
    
    public void StopTime()
    {
        Time.timeScale = 0;
    }
}