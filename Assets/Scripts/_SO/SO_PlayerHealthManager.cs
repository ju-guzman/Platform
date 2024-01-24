using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "HealthManager", menuName = "Managers/HealthManager")]
public class SO_PlayerHealthManager : ScriptableObject
{
    [SerializeField] private int maxHealt = 3;

    private int currentHealt;

    public int CurrentHealt => currentHealt;
    public Action<int> OnHealtChange { get; set; }
    public Action OnGameOver { get; set; }

    private void OnEnable()
    {
        currentHealt = maxHealt;
    }

    public void TakeDamage(int damage)
    {
        currentHealt -= damage;
        OnHealtChange?.Invoke(currentHealt);
        if (currentHealt <= 0)
        {
            OnGameOver?.Invoke();
            currentHealt = maxHealt;
        }
    }
}
