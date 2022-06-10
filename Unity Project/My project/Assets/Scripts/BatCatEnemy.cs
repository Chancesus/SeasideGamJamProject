using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatCatEnemy : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] Transform startingPoint;

    GameObject player;
    Animator animator;

    public bool chase = false;
    bool isDead;
    private Transform attackPoint;
    private float attackRange;
    PlayerController playerController;
    private LayerMask playerLayer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        
    }

    void Update()
    {
        if (isDead == false)
        {
            if (player == null)
                return;

            if (chase == true)
            {
                Chase();
            }
            else
            {
                ReturnToStartingPosition();
            }

            Flip();
        }
        else
            return;
    }

    private void ReturnToStartingPosition()
    {
        transform.position = Vector2.MoveTowards
            (transform.position,
            startingPoint.position,
            speed * Time.deltaTime);
    }
    public void Chase()
    {
        transform.position = Vector2.MoveTowards
            (transform.position,
            player.transform.position,
            speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, player.transform.position) <= 0.3f)
        {
            Attack();
        }
    }

    private void Attack()
    {
        animator.SetTrigger("TryAttack");
        StartCoroutine(KillPlayer());   
        
    }

    IEnumerator KillPlayer()
    {
        yield return new WaitForSeconds(0.5f);
        var kill = GetComponent<Collider2D>();
        kill.enabled = true;
    }

    private void Flip()
    {
        if (transform.position.x > player.transform.position.x)
        {
            transform.rotation = Quaternion.identity;
        }
        else
        {
           transform.rotation = Quaternion.Euler(0,180,0);
        }
    }

    public void Die()
    {
        animator.SetTrigger("Die");
        GetComponent<Rigidbody2D>().isKinematic = false;
        isDead = true;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
