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
    public WaveManager waveManager;

    private float attackCooldown = 2f;

    private float minimumAttackDistance = 3f;

    private float lastAttackTime = 0;

    public Transform Player
    {
        get { return player; }
        set { player = value; }
    }

    public bool isDead;

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
        isDead = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            navMeshAgent.destination = player.position;
            float distance = Vector3.Distance(navMeshAgent.transform.position, player.position);
            Debug.Log(distance);

            if (distance <= minimumAttackDistance)
            {
                StopEnemy();
                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    lastAttackTime = Time.time;
                    animator.SetBool("isAttacking", true);
                    float attackDamage = Random.Range(0, 10);
                    player.GetComponent<PlayerStats>().TakeDamage(attackDamage);
                }
            }
            else
            {
                animator.SetBool("isAttacking", false);
                GoToTarget();
            }
        }
    }

    void GoToTarget()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(player.position);
    }

    void StopEnemy()
    {
        navMeshAgent.isStopped = true;
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
