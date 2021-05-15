using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();   
    }

    
    void Update()
    {
        if (transform.position.magnitude > 200)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        EnemyController enemyController = other.collider.GetComponent<EnemyController>();
        
        if(enemyController != null)
        {
            enemyController.Fix();
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
