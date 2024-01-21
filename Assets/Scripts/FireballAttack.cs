using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballAttack : MonoBehaviour
{
    [SerializeField] private float attackRatio = 10f;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private int damage = 1;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackRatio);
            anim.SetTrigger("Attack");
        }
    }

    private void Attack()
    {
        Instantiate(fireballPrefab, spawnPoint.position, transform.rotation, transform);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
