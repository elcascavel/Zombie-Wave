using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform player = null;
    [SerializeField] private Animator animator = null;
    [SerializeField] private EnemyHealth health;

    [SerializeField] private CharacterController controller;

    public Transform Player
    {
        get { return player; }
        set { player = value; }
    }

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        health = GetComponent<EnemyHealth>();
        controller = GetComponent<CharacterController>();

        navMeshAgent.updateRotation = true;
    }

    private void Start()
    {
        health.onDeath += Die;
    }

    private void Die(Vector3 position)
    {
        animator.SetTrigger("die");
        controller.enabled = false;
        navMeshAgent.isStopped = true;

        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            navMeshAgent.destination = player.position;
        }
    }

    void LateUpdate()
    {
        HandleAnimation();
    }

    void HandleAnimation()
    {
        float speed = navMeshAgent.velocity.magnitude;
        animator.SetFloat("speed", speed);
    }
}
