using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    Vector2 checkpointPos;
    Rigidbody2D playerRb;
    TrailRenderer trailRenderer;
    [SerializeField] private ParticleSystem testParticleSystem = default;
    [SerializeField] private AudioClip deathClip = default; // Audio clip for death sound
    [SerializeField] private AudioClip coinClip = default; // Audio clip for coin sound
    [SerializeField] private AudioClip winClip = default; // Audio clip for win sound
    

    private int collectedCoins = 0;
    private bool shouldOpenWall = false;
    public GameObject wallToOpen;
    public Transform coinsParent; // Reference to the parent GameObject "Coins"

    public EnemyController enemyController;
    public Transform playerCheckpoint; // Reference to checkpoint 7 or wherever the enemy activates
    private bool playerReachedCheckpoint7 = false;

    public Trap trap;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Start()
    {
        checkpointPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Die();
        }

        if (collision.CompareTag("Coin"))
        {
            CollectCoin(collision.gameObject);
        }

        if (collision.CompareTag("Checkpoint"))
        {
            enemyController.ActivateEnemy();
        }

        if (collision.CompareTag("Meta"))
        {
            Win();
        }

    }

    private void Win()
    {
        if (winClip != null)
        {
            AudioSource.PlayClipAtPoint(winClip, transform.position);
        }
    }

    public void UpdateCheckpoint(Vector2 pos)
    {
        checkpointPos = pos;
    }

    void Die()
    {
        StartCoroutine(Respawn(1.5f));
        PlayDeathSound(); // Play the death sound
        PlayerDied();
    }

    void PlayDeathSound()
    {
        if (deathClip != null)
        {
            AudioSource.PlayClipAtPoint(deathClip, transform.position);
        }
    }

    void PlayCoinSound()
    {
        if (coinClip != null)
        {
            AudioSource.PlayClipAtPoint(coinClip, transform.position);
        }
    }

    IEnumerator Respawn(float duration)
    {
        testParticleSystem.Play();
        playerRb.velocity = Vector2.zero;
        playerRb.simulated = false;

        trailRenderer.enabled = false;

        transform.localScale = Vector3.zero;
        yield return new WaitForSeconds(duration);
        transform.position = checkpointPos;
        transform.localScale = Vector3.one;

        trailRenderer.enabled = true;

        playerRb.simulated = true;
    }

    void CollectCoin(GameObject coin)
    {
        PlayCoinSound();
        coin.SetActive(false); // Deactivate the collected coin
        collectedCoins++;

        if (collectedCoins >= 5)
        {
            shouldOpenWall = true;
            OpenWall();
        }
    }

    void OpenWall()
    {
        if (wallToOpen != null)
        {
            wallToOpen.SetActive(false); // Activate the wall when all coins are collected
        }
    }

    public void PlayerDied()
    {
        // Reset collected coins count
        collectedCoins = 0;

        // Reactivate all coin collectibles under the "Coins" parent GameObject
        if (coinsParent != null)
        {
            foreach (Transform coin in coinsParent)
            {
                coin.gameObject.SetActive(true);
            }
        }

        trap.ReactivateTrap();


        // Close the wall if it was open
        if (shouldOpenWall && wallToOpen != null)
        {
            wallToOpen.SetActive(true);
            shouldOpenWall = false;
        }

        if (enemyController != null)
        {
            enemyController.ResetPosition();
        }
    }

}