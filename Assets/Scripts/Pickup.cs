using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    public void OnTriggerEnter(Collider other) 
    {
        Debug.Log("collided");
        gameManager.Player.GetComponent<PlayerStats>().Heal(100);
    }
}
