using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Slider healthSlider;
    public GameObject gameOverText; // Asignar desde el Inspector
    public GameObject retryText;

    public AudioClip gameOverSound;
    private AudioSource audioSource;


    /*void Start()
    {
        currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        // Asegurarse que el texto esté oculto al empezar
        if (gameOverText != null)
        {
            gameOverText.SetActive(false);
            retryText.SetActive(false);
        }
    }*/

    void Start()
    {
        currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        if (gameOverText != null) gameOverText.SetActive(false);
        if (retryText != null) retryText.SetActive(false);

        audioSource = GetComponent<AudioSource>();
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
            GameOver();
        }
    }

    /*void GameOver()
    {
        Debug.Log("Game Over");

        if (gameOverText != null)
        {
            gameOverText.SetActive(true);
            retryText.SetActive(true);
        }

        // Pausar el juego (opcional)
        Time.timeScale = 0f;
    }*/

    void GameOver()
    {
        Debug.Log("Game Over");

        if (gameOverText != null) gameOverText.SetActive(true);
        if (retryText != null) retryText.SetActive(true);

        // Reproducir sonido de Game Over
        if (gameOverSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(gameOverSound);
        }

        Time.timeScale = 0f; // Pausar juego (opcional)
    }


    void Update()
    {
        /*// Solo para probar: quítale 10 de vida cuando presiones la tecla H
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(10);
        }*/

        if (Input.GetKeyDown(KeyCode.R) && currentHealth <= 0)
        {
            Time.timeScale = 1f; // reanudar el juego
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }


    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        if (healthSlider != null)
            healthSlider.value = currentHealth;
    }
}


