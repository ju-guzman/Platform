using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider2D _collider2D;

    public bool activo = true;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _collider2D = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (activo == true)
        {
            _animator.SetBool("cerrar", true);
            _collider2D.enabled = true;
        }
        else 
        {
            _animator.SetBool("cerrar", false);
            _collider2D.enabled = false;

        }
    }
}
