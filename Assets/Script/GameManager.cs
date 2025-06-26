using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int kills = 0;
    public int totalEnemies = 11;

    public TextMeshProUGUI killsText;
    public GameObject winText;
    public GameObject retryText;       // NUEVO: texto de "Presiona R"
    public AudioClip winSound;

    private AudioSource audioSource;
    private bool hasWon = false;       // NUEVO: para controlar el reinicio

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (winText != null)
            winText.SetActive(false);

        if (retryText != null)
            retryText.SetActive(false);  // ocultar el mensaje al inicio

        UpdateKillsText();
    }

    public void AddKill()
    {
        kills++;
        UpdateKillsText();

        if (kills >= totalEnemies)
        {
            hasWon = true;

            if (winText != null)
                winText.SetActive(true);

            if (retryText != null)
                retryText.SetActive(true);   // Mostrar texto de reinicio

            if (winSound != null && audioSource != null)
                audioSource.PlayOneShot(winSound);

            Time.timeScale = 0f;
        }
    }

    void UpdateKillsText()
    {
        if (killsText != null)
        {
            killsText.text = "Enemigos derrotados: " + kills + " / " + totalEnemies;
        }
    }

    void Update()
    {
        if (hasWon && Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
