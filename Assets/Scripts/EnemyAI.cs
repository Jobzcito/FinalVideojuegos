/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 15f;
    public float attackRange = 2f;
    public float walkRadius = 15f;
    public float waitTime = 2f;

    private NavMeshAgent agent;
    private Animator animator;
    private float timer;

    //private bool isChasingPlayer = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        GoToRandomPosition();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            //isChasingPlayer = true;
            agent.SetDestination(player.position);
        }
        else
        {
            //isChasingPlayer = false;

            // Patrullaje aleatorio
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                timer += Time.deltaTime;

                if (timer >= waitTime)
                {
                    GoToRandomPosition();
                    timer = 0f;
                }
            }
        }

        // Calcular valores locales para animaciones
        Vector3 localVelocity = transform.InverseTransformDirection(agent.velocity);
        float velX = localVelocity.x;
        float velY = localVelocity.z;

        animator.SetFloat("VelX", velX);
        animator.SetFloat("VelY", velY);
    }

    void GoToRandomPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
        randomDirection += transform.position;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, walkRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 15f;
    public float attackRange = 2f;
    public float walkRadius = 15f;
    public float waitTime = 2f;

    public float normalSpeed = 3.5f;
    //public float fleeSpeed = 6.5f;
    [HideInInspector] public bool isFleeing = false;

    private NavMeshAgent agent;
    private Animator animator;
    private float timer;

    public AudioClip fleeSound;
    private AudioSource audioSource;
    private bool fleeSoundPlayed = false;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        agent.speed = normalSpeed;
        GoToRandomPosition();
    }

    /*void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        /*if (isFleeing)
        {
            // Huye alejándose del jugador
            Vector3 fleeDirection = (transform.position - player.position).normalized;
            Vector3 fleeTarget = transform.position + fleeDirection * 10f;
            agent.speed = fleeSpeed;
            agent.SetDestination(fleeTarget);
        }*/

    /*if (isFleeing)
    {
        // Huye con la velocidad normal (sin reducirla)
        agent.speed = normalSpeed;

        // Solo recalcula si está cerca de su destino
        if (!agent.pathPending && agent.remainingDistance < 1f)
        {
            Vector3 fleeDirection = (transform.position - player.position).normalized;
            Vector3 fleeTarget = transform.position + fleeDirection * 15f;

            if (NavMesh.SamplePosition(fleeTarget, out NavMeshHit hit, 15f, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }
        }
    }

    if (isFleeing && !fleeSoundPlayed)
    {
        if (fleeSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(fleeSound);
            fleeSoundPlayed = true;
        }
    }


    else if (distanceToPlayer <= detectionRange)
    {
        // Persigue al jugador
        agent.speed = normalSpeed;
        agent.SetDestination(player.position);
    }
    else
    {
        // Patrullaje aleatorio
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            timer += Time.deltaTime;

            if (timer >= waitTime)
            {
                GoToRandomPosition();
                timer = 0f;
            }
        }
    }

    // Calcular valores locales para animaciones
    Vector3 localVelocity = transform.InverseTransformDirection(agent.velocity);
    float velX = localVelocity.x;
    float velY = localVelocity.z;

    animator.SetFloat("VelX", velX);
    animator.SetFloat("VelY", velY);
}*/

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (isFleeing)
        {
            // 1. Reproducir sonido solo una vez
            if (!fleeSoundPlayed && fleeSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(fleeSound);
                fleeSoundPlayed = true;
            }

            // 2. Huir (recalcula destino si ya llegó al anterior)
            agent.speed = normalSpeed;

            if (!agent.pathPending && agent.remainingDistance < 1f)
            {
                Vector3 fleeDirection = (transform.position - player.position).normalized;
                Vector3 fleeTarget = transform.position + fleeDirection * 15f;

                if (NavMesh.SamplePosition(fleeTarget, out NavMeshHit hit, 15f, NavMesh.AllAreas))
                {
                    agent.SetDestination(hit.position);
                }
            }
        }
        else if (distanceToPlayer <= detectionRange)
        {
            // Perseguir al jugador
            agent.speed = normalSpeed;
            agent.SetDestination(player.position);
        }
        else
        {
            // Patrullaje aleatorio
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                timer += Time.deltaTime;

                if (timer >= waitTime)
                {
                    GoToRandomPosition();
                    timer = 0f;
                }
            }
        }

        // Actualizar animación
        Vector3 localVelocity = transform.InverseTransformDirection(agent.velocity);
        animator.SetFloat("VelX", localVelocity.x);
        animator.SetFloat("VelY", localVelocity.z);
    }


    void GoToRandomPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
        randomDirection += transform.position;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, walkRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}

