using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    Animator animator;
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] LayerMask enemyLayer;

   
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            KickAttack();
        }
    }

    void KickAttack()
    {
        animator.SetTrigger("KickAttack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<BatCatEnemy>().Die();
        }
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


