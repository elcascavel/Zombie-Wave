using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieNavMesh : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform player = null;
    [SerializeField] private Animator animator = null;

    public Transform Player
    {
        get { return player; }
        set { player = value; }
    }

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        navMeshAgent.updateRotation = true;
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
