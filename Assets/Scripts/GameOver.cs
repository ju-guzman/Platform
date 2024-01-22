using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(MoveToCredits());
    }

    IEnumerator MoveToCredits() 
    {
        LevelManager levelManager = LevelManager.Instance;

        yield return new WaitForSeconds(5);
        levelManager.Creditos();
    }
}
