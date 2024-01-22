using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private float inputH;

   
    bool isGrounded;


    bool doubleJump;


    [Header("wall Jump System")]
    public Transform wallCheck;
    bool isWallTocuh;
    bool isDliding;
    public float wallSlidingSpeed;


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

    [Header("Health system")]
    [SerializeField] private SO_PlayerHealthManager healtManager;

    public SO_PlayerHealthManager HealtManager => healtManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        healtManager.OnGameOver += OnDeath;
        }
    void OnDeath()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        Move();
        Jump();
        LaunchDamage();
        WallCheck();

    }

    private void WallCheck()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(1.7f, 0.24f), 0, GroundLayer);
        isGrounded = Physics2D.OverlapBox(wallCheck.position, new Vector2(0.3f, 1.7f), 0, GroundLayer);

        if(isWallTocuh && !isGrounded && inputH != 0)
        {
            isDliding = true;
        }
        else
        {
            isDliding = false;
        }
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
        //if (Input.GetKeyDown(KeyCode.Space) && IsInGroun())
        //{
        //    rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        //    anim.SetTrigger("Jump");
        //}
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetTrigger("Jump");

            if (IsInGroun())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                doubleJump = true;
            }

            else if (doubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                doubleJump = false;
            }
            
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

    private void FixedUpdate()
    {
        if (isDliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
    }

}
