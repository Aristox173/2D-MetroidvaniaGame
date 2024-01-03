using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Vector3 initialPosition;
    private bool activated = false;
    public GameObject Enemy;

    void Start()
    {
        // Store the initial position of the enemy
        initialPosition = transform.position;
    }

    public void ResetPosition()
    {
        // Reset the enemy's position to its initial position
        transform.position = initialPosition;
    }

    public void ActivateEnemy()
    {
        Enemy.SetActive(true);
    }
}