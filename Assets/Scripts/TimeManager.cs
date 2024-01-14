using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private SO_TimeManager timeManager;
    [SerializeField] private SO_PlayerHealthManager playerHealthManager;
    // Start is called before the first frame update
    void Start()
    {
        if(timeManager)
        {
            StartCoroutine(AddSecond());
            if(playerHealthManager)
            {
                playerHealthManager.OnGameOver += StopTimer;
            }
        }
    }

    public void StopTimer()
    {
        StopAllCoroutines();
    }

    private IEnumerator AddSecond()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            timeManager.AddSecond();
        }
    }
}
