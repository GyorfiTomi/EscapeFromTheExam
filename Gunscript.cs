using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunscript : MonoBehaviour
{
    [SerializeField] private Transform rotationPivot; // Reference to the Rotation Pivot GameObject
    [SerializeField] private Transform gunPivot; // Reference to the Gun Pivot GameObject
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletFirePos; // Reference to the bullet fire position

    public bool isFlipped = false;
    public bool isBarrellLift = false;
    [SerializeField] private GameObject weapon;

    void Update()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the angle between the character's position and the mouse position
        float angle = Mathf.Atan2(mousePosition.y - transform.position.y, mousePosition.x - transform.position.x) * Mathf.Rad2Deg;

        // Limit the angle between -90 and 90 degrees
        angle = Mathf.Clamp(angle, -90f, 90f);

        // Set the rotation of the Rotation Pivot GameObject
        rotationPivot.rotation = Quaternion.Euler(0f, 0f, angle);

        // Calculate the angle between the gun's position and the mouse position
        float gunAngle = Mathf.Atan2(mousePosition.y - gunPivot.position.y, mousePosition.x - gunPivot.position.x) * Mathf.Rad2Deg;

        // Set the rotation of the Gun Pivot GameObject
        gunPivot.rotation = Quaternion.Euler(0f, 0f, gunAngle);






        if (isFlipped )
        {
            weapon.gameObject.transform.position = transform.parent.position + new Vector3(-1, 0, 0);
            weapon.GetComponent<SpriteRenderer>().flipY = true;
            Vector3 original = bulletFirePos.transform.localPosition;
            if (isBarrellLift == false)
            {
                bulletFirePos.transform.localPosition = original + new Vector3(0, -1.75f, 0);
                isBarrellLift = true;
            }
        }
        else 
        {
            weapon.gameObject.transform.position = transform.parent.position + new Vector3(1, 0, 0);
            weapon.GetComponent<SpriteRenderer>().flipY = false;
            Vector3 original = bulletFirePos.transform.localPosition;
            if (isBarrellLift == true)
            {
                bulletFirePos.transform.localPosition = original + new Vector3(0, 1.75f, 0);
                isBarrellLift = false;
            }
        }



        if (Input.GetMouseButtonDown(0))
        {
            // Create a new bullet object
            GameObject bulletObj = Instantiate(bulletPrefab, bulletFirePos.position, Quaternion.identity);
            Bulletscript bullet = bulletObj.GetComponent<Bulletscript>();

            // Set the bullet's damage
            bullet.SetDamage(5.0f);

            // Set the bullet's position and velocity
            Vector2 bulletDirection = ((Vector2)mousePosition - (Vector2)bulletFirePos.position).normalized;
            bullet.SetVelocity(bulletDirection, bulletSpeed);

            // Destroy the bullet after some time
            StartCoroutine(DestroyAfterTime(bulletObj));
        }


    }

    IEnumerator DestroyAfterTime(GameObject bulletObj, float delay = 5.0f)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bulletObj);
    }
}
