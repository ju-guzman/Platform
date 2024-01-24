using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    bool doubleJump;

    [Header("Move system")]
    private float horizontal;
    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;
    private bool isFacinRight = true;

    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.8f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 1f;
    private Vector2 wallJumpingPower = new Vector2(60f, 15f);

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask GroundLayer;

    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;

    [Header("Attack system")]
    [SerializeField] private float damage = 20f;
    [SerializeField] private Transform attackOrigin;
    [SerializeField] private float attackRadius = 1f;
    [SerializeField] private LayerMask AttackLayer;
    [SerializeField] private AudioSource attackClip;

    [Header("Health system")]
    [SerializeField] private SO_PlayerHealthManager healtManager;

    public SO_PlayerHealthManager HealtManager => healtManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        attackClip = GetComponent<AudioSource>();
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
        WallSlide();
        WallJump();
    }

    private void FixedUpdate()
    {
        if (!isWallJumping) 
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    }

    private bool IsWalled() 
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide() 
    {
        if (IsWalled() && !IsInGroun() && horizontal != 0)
        {
            isWallSliding = true;
            anim.SetBool("WallSliding", true);

            isFacinRight = !isFacinRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;

            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else 
        {
            isWallSliding = false;
            anim.SetBool("WallSliding", false);
        }
    }

    private void WallJump() 
    {
        if (isWallSliding) 
        {
            isWallJumping = false;
            wallJumpingDirection = transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            anim.SetBool("WallSliding", false);
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && wallJumpingCounter > 0f) 
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            anim.SetBool("WallSliding", false);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacinRight = !isFacinRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping() 
    {
        isWallJumping = false;
    }

    private bool IsInGroun()
    {
        Debug.DrawRay(groundCheck.position, Vector2.down, Color.red, 0.3f);
        return Physics2D.Raycast(groundCheck.position, Vector2.down, 0.3f, GroundLayer);
    }

    private void Move()
    {
        horizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        SetVisualState();
        Flip();
    }

    private void SetVisualState()
    {
        if (horizontal != 0)
        {
            anim.SetBool("Running", true);
        }
        else
        {
            anim.SetBool("Running", false);
        }
    }

    private void Flip() 
    {
        if (isFacinRight && horizontal < 0f || !isFacinRight && horizontal > 0f)
        {
            isFacinRight = !isFacinRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void Jump()
    {
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
            attackClip.PlayOneShot(attackClip.clip, 0.5f);
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
