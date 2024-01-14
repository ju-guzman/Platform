using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreManager", menuName = "Managers/ScoreManager")]
public class SO_ScoreManager : ScriptableObject
{
    [SerializeField] private int score = 0;

    public static SO_ScoreManager Instance { get; private set; }
    public int Score => score;
    public Action<int> OnUpdateScore { get; set; }

    public void OnEnable()
    {
        score = 0;
        Instance = this;
    }

    public void AddScore(int score)
    {
        this.score += score;
        OnUpdateScore?.Invoke(this.score);
    }
}
