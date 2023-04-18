using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulletscript : MonoBehaviour
{
    [SerializeField] float bulletdamage = 2.5f;
    [SerializeField] Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(3, 6);
    }

    void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy_Melee"))
        {
            aicode enemy = collision.GetComponent<aicode>();
            enemy.TakeDamage((int)bulletdamage);

        }
        Destroy(gameObject);

    }

    public void SetVelocity(Vector2 direction, float bulletSpeed)
    {
        // Calculate the angle between the bullet's position and the mouse position
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

        // Set the velocity of the bullet using the given direction and bulletSpeed
        GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }



    public void SetDamage(float Bulletdamage)
    {
        this.bulletdamage = Bulletdamage;
    }
}





