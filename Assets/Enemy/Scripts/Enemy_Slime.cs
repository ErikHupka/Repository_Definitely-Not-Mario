using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy_Slime : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] int enemyHealth = 3;
    [SerializeField] int enemyDamage = 1;
    [SerializeField] int enemyMoveSpeed = 50;
    [SerializeField] int enemyScore = 1;
    bool isDead = false;

    [Header("Sprites")]
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite hitSprite;
    [SerializeField] Sprite dieSprite;
    [SerializeField] Slider healthSlider;

    //Components
    Rigidbody2D rigidBody;
    PolygonCollider2D enemyCollider;
    BoxCollider2D turnCollider;
    Animator animator;
    SpriteRenderer spriteRenderer;

    //GameObjects
    PlayerStats playerStats;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<PolygonCollider2D>();
        playerStats = FindObjectOfType<PlayerStats>();
        turnCollider = GetComponent<BoxCollider2D>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        healthSlider.maxValue = enemyHealth;
        healthSlider.value = enemyHealth;
    }

    private void FixedUpdate()
    {
        MoveEnemy();
    }

    void MoveEnemy()
    {
        if (isDead) { return;  }

        float movement = enemyMoveSpeed * Time.deltaTime;
        Vector2 moveVector = new(-movement, 0);

        rigidBody.velocity = moveVector;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        RotateEnemy(collision);
        GiveHitToPlayer(collision, playerStats.GetPlayerDamage());
    }

    private void RotateEnemy(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("RotateCollider"))
        {
            transform.Rotate(new Vector2(0, 180));
            healthSlider.GetComponent<RectTransform>().localScale *= new Vector2(-1, 1);
            enemyMoveSpeed = -enemyMoveSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerStats.TakeHitFromEnemy(enemyDamage);
        }
    }

    void GiveHitToPlayer(Collider2D collision, int playerDamage)
    {
        if (collision.gameObject.CompareTag("Laser"))
        {
            if (enemyHealth > 1)
            {
                enemyHealth -= playerDamage;
            }
            else
            {
                enemyHealth -= playerDamage;
                DieSequence();
            }
            healthSlider.value = enemyHealth;
            Destroy(collision.gameObject);
        }
    }

    void DieSequence()
    {
        animator.SetBool("isDead", true);
        turnCollider.enabled = false;
        enemyCollider.isTrigger = true;
        spriteRenderer.flipY = true;
        isDead = true;
        rigidBody.AddForce(new Vector2(0, 5));
        playerStats.SetEnemiesCount(enemyScore);
        Destroy(gameObject, 2);
    }
}
