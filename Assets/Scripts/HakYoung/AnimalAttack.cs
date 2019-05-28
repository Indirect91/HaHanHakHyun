using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAttack : MonoBehaviour
{
    private Animator animator;
    private Transform playerTr;
    private Transform animalTr;

    private readonly int hashAttack = Animator.StringToHash("Attack");

    private float nextAttack = 0.0f;

    private readonly float attackRate = 0.1f;
    private readonly float damping = 10.0f;

    public bool isAttack = false;

    void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animalTr = GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(isAttack)
        {
            Attack();
            nextAttack = Time.time + attackRate + Random.Range(0.0f, 0.5f);
        }
    }

    void Attack()
    {
        animator.SetTrigger(hashAttack);
    }
}
