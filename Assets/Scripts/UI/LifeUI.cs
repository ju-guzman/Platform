using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LifeUI : MonoBehaviour
{
    [SerializeField] private SO_PlayerHealthManager healthManager;
    [SerializeField] private TextMeshProUGUI lifeText;

    private void OnEnable()
    {
        healthManager.OnHealtChange += UpdateLifeUI;
        UpdateLifeUI(healthManager.CurrentHealt);
    }

    private void OnDisable()
    {
        healthManager.OnHealtChange -= UpdateLifeUI;
    }

    private void UpdateLifeUI(int life)
    {
        lifeText.text = life.ToString();
    }
}
