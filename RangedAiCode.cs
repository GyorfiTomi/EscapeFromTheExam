using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAiCode : MonoBehaviour
{
    private Transform target; // the target to follow
    public float moveSpeed = 1.5f; // movement speed
    public float attackRange = 5f; // range for attacking
    public float attackCooldown = 1f; // cooldown between attacks
    public int health = 50; // health of the AI
    public float dodgeChance = 0.5f; // chance to dodge bullets
    

    public GameObject bulletPrefab;
    public Transform firePoint;

    private Rigidbody2D rb;
    private bool canAttack = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (target == null) return;

        // Move towards the target
        Vector2 direction = target.position - transform.position;

        // Check if in range to attack
        if (direction.magnitude <= attackRange)
        {
            // Check if it's time to attack
            if (canAttack)
            {
                // Attack the target with a bullet

                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
                Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
                Vector2 shootDirection = (target.position - firePoint.position).normalized;
                rbBullet.velocity = shootDirection * 15f;

                canAttack = false;
                StartCoroutine(AttackCooldown());
            }

            else
            {
                // Stop moving towards the target if it's not time to attack yet
                rb.velocity = Vector2.zero;
            }
        }
        else
        {
            // Move towards the target if not in range to attack
            rb.velocity = direction.normalized * moveSpeed;
        }
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
     void OnTriggerEnter2D(Collider2D other)
    {
        // Check if hit by bullet
        if (other.CompareTag("bullet"))
        {
            // Check if dodge bullet
            if (Random.value < dodgeChance)
            {
                Destroy(other.gameObject);
            }
            else
            {
                TakeDamage(2);
                Destroy(other.gameObject);
            }
        }
    }
}