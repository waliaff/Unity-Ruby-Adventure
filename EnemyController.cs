using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    float speed = 3;
    public bool vertical = true;
    public float changeTime = 2.5f;

    Rigidbody2D enemyRigidbody;
    Animator enemyAnimator;
    RubyController playerController;


    float timer;
    int direction = 1;
    bool broken = true;

    public ParticleSystem smokeEffect;
    public AudioClip killSound;
    public AudioClip healthDamageSound;



    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        timer = changeTime;
        enemyAnimator = GetComponent<Animator>();
        playerController = GameObject.Find("Ruby").GetComponent<RubyController>();
    }

    void Update()
    {
        if (!broken)
        {
            return;
        }
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }   
    }
    void FixedUpdate()
    {
        if (!broken)
        {
            return;
        }
        Vector2 enemyPosition = enemyRigidbody.position;
        if (vertical)
        {
            enemyPosition.y = enemyPosition.y + Time.deltaTime * speed * direction;
            enemyAnimator.SetFloat("Move X", 0);
            enemyAnimator.SetFloat("Move Y", direction);
        }
        else
        {
            enemyPosition.x = enemyPosition.x + Time.deltaTime * speed * direction;
            enemyAnimator.SetFloat("Move X", direction);
            enemyAnimator.SetFloat("Move Y", 0);
        }
        enemyRigidbody.MovePosition(enemyPosition);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();
        if (player != null)
        {
            player.ChangeHealth(-1);
            playerController.PlaySound(healthDamageSound);
        }
    }

    public void Fix()
    {
        broken = false;
        enemyRigidbody.simulated = false;
        enemyAnimator.SetTrigger("Fixed");
        smokeEffect.Stop();
        playerController.PlaySound(killSound);
    }
}
