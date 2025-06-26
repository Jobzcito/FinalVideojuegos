/*using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 2f;
    public LayerMask enemyLayer;
    public Animator animator;

    public float attackCooldown = 1f;
    private float lastAttackTime;

    private int damageToApply = 0;

    void Update()
    {
        // Ataque 1: Punch (E) = 20 de daño
        if (Input.GetKeyDown(KeyCode.E) && Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            damageToApply = 20;
            animator.SetTrigger("Attack");
            Invoke(nameof(DoAttack), 0.3f);
        }

        // Ataque 2: Kick1 (R) = 30 de daño
        if (Input.GetKeyDown(KeyCode.R) && Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            damageToApply = 30;
            animator.SetTrigger("Kick1");
            Invoke(nameof(DoAttack), 0.3f);
        }

        // Ataque 3: Kick2 (T) = 35 de daño
        if (Input.GetKeyDown(KeyCode.T) && Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            damageToApply = 35;
            animator.SetTrigger("Kick2");
            Invoke(nameof(DoAttack), 0.3f);
        }

        // Ataque 4: Attack (Q) = 25 de daño
        if (Input.GetKeyDown(KeyCode.Q) && Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            damageToApply = 25;
            animator.SetTrigger("Punch");
            Invoke(nameof(DoAttack), 0.3f);
        }

    }

    void DoAttack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position + transform.forward, attackRange, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponentInChildren<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageToApply);
            }
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward * 1f, attackRange);
    }
}*/

using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 2f;
    public LayerMask enemyLayer;
    public Animator animator;

    public float attackCooldown = 1f;
    private float lastAttackTime;

    private int damageToApply = 0;

    public AudioClip punchHitSound; // sonido de puño
    public AudioClip kickHitSound;  // sonido de patada

    private AudioSource audioSource;

    private AudioClip soundToPlay; //  sonido temporal según el ataque


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        // Ataque 1: Punch (E)
        if (Input.GetKeyDown(KeyCode.E) && Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            damageToApply = 20;
            soundToPlay = punchHitSound;
            animator.SetTrigger("Attack");
            Invoke(nameof(DoAttack), 0.3f);
        }

        // Ataque 2: Kick1 (R)
        if (Input.GetKeyDown(KeyCode.R) && Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            damageToApply = 30;
            soundToPlay = kickHitSound;
            animator.SetTrigger("Kick1");
            Invoke(nameof(DoAttack), 0.3f);
        }

        // Ataque 3: Kick2 (T) = 35 de daño
        if (Input.GetKeyDown(KeyCode.T) && Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            damageToApply = 35;
            soundToPlay = kickHitSound;
            animator.SetTrigger("Kick2");
            Invoke(nameof(DoAttack), 0.3f);
        }

        // Ataque 4: Attack (Q) = 25 de daño
        if (Input.GetKeyDown(KeyCode.Q) && Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            damageToApply = 25;
            soundToPlay = punchHitSound;
            animator.SetTrigger("Punch");
            Invoke(nameof(DoAttack), 0.3f);
        }

    }

    void DoAttack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position + transform.forward, attackRange, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponentInChildren<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageToApply);

                // Reproducir sonido del tipo de ataque
                if (soundToPlay != null && audioSource != null)
                {
                    audioSource.PlayOneShot(soundToPlay);
                }
            }
        }
    }



    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward * 1f, attackRange);
    }
}












