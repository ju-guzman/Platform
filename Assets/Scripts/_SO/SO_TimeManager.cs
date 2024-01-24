using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TimeManager", menuName = "Managers/TimeManager")]
public class SO_TimeManager : ScriptableObject
{
    [SerializeField] private int time = 0;

    public void OnEnable()
    {
        time = 0;
    }

    public int Time => time;
    public Action<int> OnUpdateTime { get; set; }

    public void AddSecond()
    {
        time++;
        OnUpdateTime?.Invoke(time);
    }
}
