using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider2D _collider2D;
    [SerializeField] private float tiempoActivo;
    [SerializeField] private float tiempoApagado;

    private bool running = true;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _collider2D = GetComponent<Collider2D>();

        StartCoroutine(Trampa());
    }

    void Update()
    {
        if (_animator.GetBool("active") == true)
        {
            _collider2D.enabled = true;
        }
        else
        {
            _collider2D.enabled = false;
        }
    }

    IEnumerator Trampa() 
    {
        while (running == true)
        {
            yield return new WaitForSeconds(tiempoApagado);
            _animator.SetBool("active", true);
            yield return new WaitForSeconds(tiempoActivo);
            _animator.SetBool("active", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DetectionPlayer"))
        {

        }
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().HealtManager.TakeDamage(10);
        }
    }
}
