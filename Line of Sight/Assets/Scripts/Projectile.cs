using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody projectileBody;
    public float projectileDamage = 1f;
    public float projectileSpeed = 1f;

    public void Initialize(Vector3 direction)
    {
        projectileBody.AddForce(direction * 1000f * projectileSpeed);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            collision.gameObject.GetComponent<PlayerHealth>()?.TakeDamage(projectileDamage);
        }
        Destroy(gameObject);
    }
}