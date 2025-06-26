using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerWin : MonoBehaviour
{
    public GameObject winText;
    public GameObject retryText;

    private bool hasWon = false;

    public AudioClip winSound;
    private AudioSource audioSource;


    void Start()
    {
        if (winText != null) winText.SetActive(false);
        if (retryText != null) retryText.SetActive(false);

        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!hasWon && other.CompareTag("Goal"))
        {
            hasWon = true;
            Time.timeScale = 0f;

            if (winText != null) winText.SetActive(true);
            if (retryText != null) retryText.SetActive(true);

            if (winSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(winSound);
            }
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
