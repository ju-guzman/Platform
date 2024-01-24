using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider2D _collider2D;
    [SerializeField] private AudioSource laser;

    public bool activo = true;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _collider2D = GetComponent<Collider2D>();
        laser = GetComponent<AudioSource>();
        InvokeRepeating(nameof(Espera), 0f, 5f);
    }

    void Update()
    {
        if (activo == true)
        {
            laser.enabled = true;
            _animator.SetBool("cerrar", true);
            _collider2D.enabled = true;
        }
        else 
        {
            laser.enabled = false;
            _animator.SetBool("cerrar", false);
            _collider2D.enabled = false;

        }
    }

    IEnumerator Espera() 
    {
        yield return new WaitForSeconds(5);
        activo = !activo;
    }
}
