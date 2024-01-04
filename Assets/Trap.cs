using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public SpecialGun specialGun;
    private Collider2D trapCollider;
    private bool trapInitialActivationState = true; // Store the initial activation state of the trap

    private void Start()
    {
        // Get the Collider2D component of the trap
        trapCollider = GetComponent<Collider2D>();
        // Store the initial activation state of the trap
        trapInitialActivationState = trapCollider.enabled;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            specialGun.FireBullet();
            DeactivateTrap();
        }
    }

    private void DeactivateTrap()
    {
        // Disable the Collider2D component to deactivate the trap
        trapCollider.enabled = false;
        // You can also deactivate the GameObject if needed: gameObject.SetActive(false);
    }

    public void ReactivateTrap()
    {
        // Reactivate the trap by restoring its initial activation state
        trapCollider.enabled = trapInitialActivationState;
    }
}