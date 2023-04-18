using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aicode : MonoBehaviour
{
    private Transform target; // the target to follow
    public float moveSpeed = 1.5f; // movement speed
    public float attackRange = 0.5f; // range for attacking
    public float attackCooldown = 1f; // cooldown between attacks
    public int health = 50; // health of the AI
    public float dodgeChance = 0.5f; // chance to dodge bullets
    [SerializeField] private float knockbackForce = 1f; //knockbackforce


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
                // Attack the target with 5 damage

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
    // 
    // játékos vector2 (x , y -)
    public void Attack(int damage)
    {

        target.GetComponent<heroscript>().TakeDamage(1);
        canAttack = false;
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
        else
        {
            Attack(1);
            if (other.gameObject.CompareTag("Player"))
            {
                Rigidbody2D playerRigidbody = other.gameObject.GetComponent<Rigidbody2D>();

                if (playerRigidbody != null)
                {
                    // Calculate the knockback direction
                    Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;

                    // Apply knockback force to the player Rigidbody2D component
                    playerRigidbody.AddForce(knockbackDirection * knockbackForce);
                }
            }

        }
    }
}

