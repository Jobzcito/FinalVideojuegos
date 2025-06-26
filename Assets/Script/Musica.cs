using UnityEngine;

public class Musica : MonoBehaviour
{
    public AudioClip[] canciones;

    private AudioSource audioSource;
    private int indiceActual = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (canciones.Length > 0)
        {
            audioSource.clip = canciones[indiceActual];
            audioSource.Play();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            CambiarCancion();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            audioSource.mute = !audioSource.mute;
        }
    }

    void CambiarCancion()
    {
        if (canciones.Length == 0)
            return;

        indiceActual = (indiceActual + 1) % canciones.Length;
        audioSource.clip = canciones[indiceActual];
        audioSource.Play();
    }
}
