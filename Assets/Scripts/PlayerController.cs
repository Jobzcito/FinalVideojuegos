using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed = 7;
    public float rotationSpeed = 250;

    public Animator animator;

    private float x, y;

    public Rigidbody rb;
    public float jumpHeight = 1.5f; // Ajustado

    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    private bool isGrounded;
    private bool canMove = true; // Nuevo: bloquea movimiento durante animaciones

    public AudioClip jumpSound;
    private AudioSource audioSource;

    public PlayerHealth playerHealth; // arrástralo desde el Inspector

    public AudioClip praySound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        // Movimiento solo si se permite
        if (canMove)
        {
            x = Input.GetAxis("Horizontal");
            y = Input.GetAxis("Vertical");

            transform.Rotate(0, x * Time.deltaTime * rotationSpeed, 0);
            transform.Translate(0, 0, y * Time.deltaTime * runSpeed);
        }
        else
        {
            x = 0;
            y = 0;
        }

        // Parámetros de animación
        animator.SetFloat("VelX", x);
        animator.SetFloat("VelY", y);
        float speed = new Vector2(x, y).magnitude;
        animator.SetFloat("Speed", speed);

        /*// Animaciones especiales (bloquean movimiento un tiempo)
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetBool("other", false);
            animator.Play("Pray");
            StartCoroutine(TemporarilyDisableMovement(2f));
        }*/

        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetBool("other", false);
            animator.Play("Pray");

            if (praySound != null && audioSource != null)
            {
                //audioSource.PlayOneShot(praySound);
                audioSource.clip = praySound;
                audioSource.Play();

            }

            StartCoroutine(TemporarilyDisableMovement(2f));
            StartCoroutine(HealWhilePraying(2f, 5, 0.5f)); // cura 5 cada 0.5 segundos por 2s
        }


        if (Input.GetKeyDown(KeyCode.V))
        {
            animator.SetBool("other", false);
            animator.Play("Victory");
            StartCoroutine(TemporarilyDisableMovement(3f));
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetBool("other", false);
            animator.Play("Dance");
            StartCoroutine(TemporarilyDisableMovement(3f));
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            animator.SetBool("other", false);
            animator.Play("Dance2");
            StartCoroutine(TemporarilyDisableMovement(3f));
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetBool("other", false);
            animator.Play("Dance3");
            StartCoroutine(TemporarilyDisableMovement(3f));

        }

        // Bloquear otras animaciones si está quieto
        if (x != 0 || y != 0)
        {
            animator.SetBool("other", true);
        }

        /*// Saltar
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.Play("Jump");
            float jumpForce = Mathf.Sqrt(2 * jumpHeight * Physics.gravity.magnitude);
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
        }

        if (rb.linearVelocity.y < -15f)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, -15f, rb.linearVelocity.z);
        }*/

        // Saltar
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.Play("Jump");

            // Sonido de salto
            if (jumpSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }

            float jumpForce = Mathf.Sqrt(2 * jumpHeight * Physics.gravity.magnitude);
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);

        }

        // Limitador de caída
        if (rb.linearVelocity.y < -15f)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, -15f, rb.linearVelocity.z);
        }


        // Ataques (también bloquean movimiento temporal)
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("Attack");
            StartCoroutine(TemporarilyDisableMovement(0.8f));
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetTrigger("Punch");
            StartCoroutine(TemporarilyDisableMovement(0.8f));

        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetTrigger("Kick1");
            StartCoroutine(TemporarilyDisableMovement(0.8f));

        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            animator.SetTrigger("Kick2");
            StartCoroutine(TemporarilyDisableMovement(0.8f));


        }
    }

    // Función para bloquear movimiento por un tiempo
    IEnumerator TemporarilyDisableMovement(float duration)
    {
        canMove = false;
        yield return new WaitForSeconds(duration);
        canMove = true;
    }

    IEnumerator HealWhilePraying(float duration, int healAmount, float interval)
    {
        float timer = 0f;

        while (timer < duration)
        {
            if (playerHealth != null && playerHealth.currentHealth < playerHealth.maxHealth)
            {
                playerHealth.Heal(healAmount);
            }

            yield return new WaitForSeconds(interval);
            timer += interval;
        }
    }
}



