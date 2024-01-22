using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float velocity = 10f;

    private Rigidbody2D rb;

    private Player player;
    private Enemy enemy;

    Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        enemy = GetComponentInParent<Enemy>();
        player = GameObject.Find("Player").GetComponent<Player>();

        moveDirection = (player.transform.position - transform.position).normalized * velocity;
        rb.velocity = moveDirection;
        Destroy(gameObject, 5);
           
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Collision with fireball");
            collision.GetComponent<Player>().HealtManager.TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
