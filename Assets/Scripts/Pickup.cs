using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    PlayerStats playerStats;

    public WaveManager waveManager;

    public Animator animator;

    public UIManager uiManager;

    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HealthKit"))
        {
            playerStats.Heal(100);
        }

        if (other.gameObject.CompareTag("Bunker") && waveManager.Bunker == true)
        {
            animator.SetBool("openBunker", true);
            uiManager.GoToMainMenu();
        }
    }
}
