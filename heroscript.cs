using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class heroscript : MonoBehaviour
{

    public int health = 10;
    public Transform respawnPoint;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;
    public Image[] hearts;

    [SerializeField] Rigidbody2D rb;

    [SerializeField] float movementSpeed = 6f;


    [SerializeField] float dashDistance = 5f;
    [SerializeField] float dashSpeed = 40f;

    private float vertical;
    private float horizontal;
    private bool isDashing;
    private float lastDashTime;
    private float dashCooldown = 2f;
    private Vector2 dashDirection;

    public float pushForce = 10f;

    void Start()
    {
        UpdateHearts();
    }

    void FixedUpdate()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));

        Vector2 dir = mousePosition - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (health > 0 && Time.timeScale == 1f)
        {
            vertical = Input.GetAxis("Vertical");
            horizontal = Input.GetAxis("Horizontal");
            Vector2 movement = new Vector2(horizontal, vertical);

            if (!isDashing && Time.time > lastDashTime + dashCooldown && Input.GetKeyDown(KeyCode.Mouse1))
            {
                isDashing = true;
                lastDashTime = Time.time;
                dashDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
                StartCoroutine(Dash());
            }

            if (!isDashing)
            {
                rb.MovePosition(rb.position + movement * Time.deltaTime * movementSpeed);

                // Flip the character if moving left or right
                if (movement.x < 0)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                }
                else if (movement.x > 0)
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                }
                if (angle > 90 && angle < 180)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                    gameObject.GetComponentInChildren<Gunscript>().isFlipped = true;
                }
                else if (angle < -90 && angle > -180)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                    gameObject.GetComponentInChildren<Gunscript>().isFlipped = true;
                }
                else
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                    gameObject.GetComponentInChildren<Gunscript>().isFlipped = false;
                }
            }

            if (GetComponent<SpriteRenderer>().flipX == true)
            {
                gameObject.GetComponentInChildren<Gunscript>().isFlipped = true;
            }
            else
            {
                gameObject.GetComponentInChildren<Gunscript>().isFlipped = false;
            }


        }
    }

    public void TakeDamage(int damage = 1)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }

        UpdateHearts();
    }

    void UpdateHearts()
    {
        switch (health % 2)
        {
            case 0:
                if (health != 10)
                {
                    hearts[health / 2].sprite = emptyHeart;
                }
                return;
            case 1:
                hearts[Mathf.RoundToInt(health / 2)].sprite = halfHeart;
                return;
            default:
                hearts[health / 2].sprite = fullHeart;
                return;
        }

    }


    IEnumerator Dash()
    {
        float distanceTraveled = 0f;
        while (distanceTraveled < dashDistance)
        {
            rb.velocity = dashDirection * dashSpeed;
            distanceTraveled += Vector2.Distance(rb.position, rb.position + rb.velocity * Time.deltaTime);
            yield return null;
        }
        rb.velocity = Vector2.zero;
        isDashing = false;

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }
       

    }

    void Die()
    {
        transform.position = respawnPoint.position;
        health += 10;
    }
}
