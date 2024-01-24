using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float speed = 5f;

    private int currentPatrolPoint = 0;

    void Start()
    {
        if (patrolPoints.Length > 0)
        {
            StartCoroutine(Move());
        }
    }

    IEnumerator Move()
    {
        while (true)
        {
            while (transform.position != patrolPoints[currentPatrolPoint].position)
            {
                transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPatrolPoint].position, speed * Time.deltaTime);
                yield return null;
            }
            currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoints.Length;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DetectionPlayer"))
        {

        }
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().HealtManager.TakeDamage(1);
        }
    }
}
