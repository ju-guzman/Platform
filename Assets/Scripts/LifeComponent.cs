using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeComponent : MonoBehaviour
{
    [SerializeField] private float life = 5f;

    public void TakenDamage(float damage)
    {
        life -= damage;

        if(life <= 0)
        {
            Destroy(gameObject);
        }
    }
}
