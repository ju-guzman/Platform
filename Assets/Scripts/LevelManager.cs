using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else 
        { 
            Instance = this;
        }
    }

    public void MainMenu() 
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GameOver() 
    {
        SceneManager.LoadScene("GameOver");
    }

    public void Creditos() 
    {
        SceneManager.LoadScene("Creditos");
    }

    public void Level1() 
    {
        SceneManager.LoadScene("");
    }

    public void EndGame() 
    {
        Application.Quit();
    }
}
