using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private SO_ScoreManager scoreManager;

    private void OnEnable()
    {
        if(scoreManager)
        {
            scoreManager.OnUpdateScore += UpdateScoreUI;
            UpdateScoreUI(scoreManager.Score);
        }
    }

    private void OnDisable()
    {
        scoreManager.OnUpdateScore -= UpdateScoreUI;
    }

    private void UpdateScoreUI(int score)
    {
        scoreText.text = score.ToString();
    }
}
