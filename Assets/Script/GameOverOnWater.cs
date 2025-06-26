using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverOnWater : MonoBehaviour
{
    public GameObject gameOverText;
    public GameObject retryText;

    private bool isGameOver = false;

    public AudioClip gameOverSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    /*void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            gameOverText.SetActive(true);
            retryText.SetActive(true); // Mostrar el mensaje de reintentar
            Time.timeScale = 0f;
            isGameOver = true;
        }
    }*/

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            gameOverText.SetActive(true);
            retryText.SetActive(true);
            Time.timeScale = 0f;
            isGameOver = true;

            // Reproducir sonido de Game Over
            if (gameOverSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(gameOverSound);
            }
        }
    }


    void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
