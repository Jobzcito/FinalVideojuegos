/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackRange = 3f;
    public int damage = 30;
    public PlayerHealth playerHealth;

    public float attackCooldown = 1f; // Tiempo entre ataques
    private float lastAttackTime;

    void Update()
    {
        float distance = Vector3.Distance(transform.position, playerHealth.transform.position);

        if (distance <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            playerHealth.TakeDamage(damage);
            lastAttackTime = Time.time;
        }
    }
}*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    public float attackRange = 3f;
    public int damage = 20;
    public PlayerHealth playerHealth;

    public float attackCooldown = 1.5f;
    private float lastAttackTime;

    private NavMeshAgent agent;
    private Animator animator;

    public AudioClip punchSound;  // sonido que hace al golpear al jugador
    private AudioSource audioSource;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); //  Agregado aquí
    }

    void Update()
    {
        /*float distance = Vector3.Distance(transform.position, playerHealth.transform.position);

        if (distance <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            agent.isStopped = true;

            // Lanza la animación Punch (mediante el Trigger "Attack")
            animator.SetTrigger("Attack");

            // Espera un momento y luego hace daño
            StartCoroutine(ApplyDamageAfterDelay(0.5f));
        }*/

        float distance = Vector3.Distance(transform.position, playerHealth.transform.position);

        if (distance <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            agent.isStopped = true;

            animator.SetTrigger("Attack");

            StartCoroutine(ApplyDamageAfterDelay(0.5f));
        }

        // Actualizar velocidad en el Animator
        float speed = agent.velocity.magnitude;
        animator.SetFloat("Speed", speed);
    }

    IEnumerator ApplyDamageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        playerHealth.TakeDamage(damage);

        if (punchSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(punchSound);
        }


        // Reanuda el movimiento
        agent.isStopped = false;
    }
}


