using TMPro;
using UnityEngine;

public class TimeUI : MonoBehaviour
{
    [SerializeField] private SO_TimeManager timeManager;
    [SerializeField] private TextMeshProUGUI lifeText;

    private void OnEnable()
    {
        timeManager.OnUpdateTime += UpdateTimeUI;
        UpdateTimeUI(timeManager.Time);
    }

    private void OnDisable()
    {
        timeManager.OnUpdateTime -= UpdateTimeUI;
    }

    private void UpdateTimeUI(int life)
    {
        lifeText.text = life.ToString();
    }
}
