using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private float inputH;

    [Header("Move system")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask GroundLayer;

    [Header("Attack system")]
    [SerializeField] private float damage = 20f;
    [SerializeField] private Transform attackOrigin;
    [SerializeField] private float attackRadius = 1f;
    [SerializeField] private LayerMask AttackLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Jump();
        LaunchDamage();
    }

    private bool IsInGroun()
    {
        Debug.DrawRay(groundCheck.position, Vector2.down, Color.red, 0.3f);
        return Physics2D.Raycast(groundCheck.position, Vector2.down, 0.3f, GroundLayer);
    }

    private void Move()
    {
        inputH = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(inputH * speed, rb.velocity.y);
        SetVisualState();
    }

    private void SetVisualState()
    {
        if (inputH != 0)
        {
            Random.Range(0, 1);
            anim.SetBool("Running", true);
            transform.eulerAngles = inputH > 0 ? Vector3.zero : new(0, 180, 0);
        }
        else
        {
            anim.SetBool("Running", false);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsInGroun())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetTrigger("Jump");
        }
    }

    private void LaunchDamage()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Attack");
        }
    }

    private void Attack()
    {
        Collider2D[] enemiesTouched = Physics2D.OverlapCircleAll(attackOrigin.position, attackRadius, AttackLayer);

        foreach (Collider2D enemy in enemiesTouched)
        {
            enemy.GetComponent<LifeComponent>().TakenDamage(damage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackOrigin.position, attackRadius);
    }
}
