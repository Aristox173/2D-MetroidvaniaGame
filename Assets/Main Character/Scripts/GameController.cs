using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Vector2 checkpointPos;
    Rigidbody2D playerRb;
    TrailRenderer trailRenderer;
    [SerializeField] private ParticleSystem testParticleSystem = default;

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
    }

    public void UpdateCheckpoint(Vector2 pos)
    {
        checkpointPos = pos;
    }

    void Die()
    {
        StartCoroutine(Respawn(1.5f));
    }

    IEnumerator Respawn(float duration)
    {
        testParticleSystem.Play();
        playerRb.velocity = new Vector2(0, 0);
        playerRb.simulated = false;

        trailRenderer.enabled = false;

        transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(duration);
        transform.position = checkpointPos;
        transform.localScale = new Vector3(1, 1, 1);

        trailRenderer.enabled = true;

        playerRb.simulated = true;
        
    }
}