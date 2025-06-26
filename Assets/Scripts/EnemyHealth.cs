/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public Slider healthSlider; // Conectar en el Inspector

    void Start()
    {
        currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Destruir enemigo al morir
        Destroy(gameObject);
    }
}*/

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public Slider healthSlider;

    public float visibleTime = 2f; // cuánto tiempo permanece visible
    private Coroutine hideCoroutine;

    public AudioClip deathSound;
    private AudioSource audioSource;


    void Start()
    {
        currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
            healthSlider.gameObject.SetActive(false); // ocultar al inicio
        }
        audioSource = GetComponent<AudioSource>(); //  para poder reproducir sonidos
    }

    /*public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
            healthSlider.gameObject.SetActive(true);

            // Reiniciar la corrutina si ya estaba corriendo
            if (hideCoroutine != null)
                StopCoroutine(hideCoroutine);

            hideCoroutine = StartCoroutine(HideAfterDelay());
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }*/

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;

        // Activar modo huida si la vida es baja
        if (currentHealth <= 40)
        {
            EnemyAI ai = GetComponent<EnemyAI>();
            if (ai != null)
            {
                ai.isFleeing = true;
            }
        }

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
            healthSlider.gameObject.SetActive(true);

            if (hideCoroutine != null)
                StopCoroutine(hideCoroutine);

            hideCoroutine = StartCoroutine(HideAfterDelay());
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(visibleTime);

        if (healthSlider != null)
        {
            healthSlider.gameObject.SetActive(false);
        }
    }

    void Die()
    {
        /*if (healthSlider != null)
        {
            healthSlider.gameObject.SetActive(false); // Ocultar la barra antes de morir
        }

        Destroy(gameObject);*/

        /*if (healthSlider != null)
            healthSlider.gameObject.SetActive(false);

        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
            Destroy(gameObject, deathSound.length);
        }
        else
        {

            Destroy(gameObject); // destruir de inmediato si no hay sonido
        }*/

        if (healthSlider != null)
            healthSlider.gameObject.SetActive(false);

        GameManager.instance.AddKill(); // SIEMPRE llama a esto primero

        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
            Destroy(gameObject, deathSound.length); // se destruye luego del sonido
        }
        else
        {
            Destroy(gameObject); // se destruye de inmediato
        }

    }
}
