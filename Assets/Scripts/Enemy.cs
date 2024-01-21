using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float speed = 5f;
    [SerializeField] private int scoreByDeath = 10;
    [SerializeField] private Player player;

    private int currentPatrolPoint = 0;

    void Start()
    {
        if (patrolPoints.Length > 0)
        {
            StartCoroutine(Patrol());
        }

        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void OnDestroy()
    {
        SO_ScoreManager.Instance.AddScore(scoreByDeath);
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            while (transform.position != patrolPoints[currentPatrolPoint].position)
            {
                transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPatrolPoint].position, speed * Time.deltaTime);
                yield return null;
            }
            NewPatrolPoint();
        }
    }

    private void NewPatrolPoint()
    {
        currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoints.Length;
        RotatePatrolCharacter();
    }

    private void RotatePatrolCharacter()
    {
        if(transform.position.x > patrolPoints[currentPatrolPoint].position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = Vector3.one;
        }
    }

    private void RotateStationaryCharacter()
    {
        if (player.transform.position.x < transform.position.x) 
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = Vector3.one;
        }
    }

    private void Update()
    {
        if (patrolPoints.Length == 0) 
        {
            RotateStationaryCharacter();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("DetectionPlayer"))
        {
            
        }
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().HealtManager.TakeDamage(1);
        }
    }
}
