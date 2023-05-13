using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletTransform;
    public float bulletSpeed = 10f;
    [SerializeField] private GameObject player;

    public void Start()
    {
        bulletTransform = transform.GetChild(0);
        player = GameObject.FindWithTag("Player");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //fire the bullet 
            ShootBullet();
        }
    }

    public void ShootBullet()
    {
        // Instantiate the bullet
        GameObject newBullet = Instantiate(bulletPrefab, bulletTransform.position, Quaternion.identity);

        // Calculate the direction to the player
        Vector2 directionToPlayer = player.transform.position - newBullet.transform.position;
        directionToPlayer.Normalize();

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

        if (directionToPlayer.x < 0f)
        {
            newBullet.GetComponent<SpriteRenderer>().flipY = true;
        }
        else
        {
            newBullet.GetComponent<SpriteRenderer>().flipY = false;
        }

        // Set the rotation of the bullet towards the player
        newBullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Rigidbody2D bulletRigidbody = newBullet.GetComponent<Rigidbody2D>();

        // Add velocity to the bullet in the direction it's facing
        bulletRigidbody.velocity = newBullet.transform.right * bulletSpeed;
    }

}
