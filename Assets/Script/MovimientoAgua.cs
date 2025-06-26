using UnityEngine;

public class MovimientoAgua : MonoBehaviour
{
    public float velocidad = 1f;      // Velocidad del oleaje
    public float amplitud = 0.3f;     // Qué tanto sube y baja
    private Vector3 posicionOriginal;

    void Start()
    {
        posicionOriginal = transform.position;
    }

    void Update()
    {
        float y = Mathf.Sin(Time.time * velocidad) * amplitud;
        float x = Mathf.Cos(Time.time * velocidad * 0.5f) * 0.2f;
        float z = Mathf.Sin(Time.time * velocidad * 0.3f) * 0.2f;
        transform.position = new Vector3(posicionOriginal.x + x, posicionOriginal.y + y, posicionOriginal.z + z);

        if (Random.value < 0.07f)
        {
            GameObject burbuja = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            burbuja.transform.position = new Vector3(
                Random.Range(-50f, 50f),
                transform.position.y + 0.2f,
                Random.Range(-50f, 50f)
            );
            burbuja.transform.localScale = Vector3.one * 0.2f;

            // Usar shader Unlit/Color para color simple sin textura
            Material mat = new Material(Shader.Find("Unlit/Color"));
            mat.color = new Color(0.5f, 0.8f, 1f, 1f); // celeste sin transparencia
            burbuja.GetComponent<Renderer>().material = mat;

            Destroy(burbuja, 2f);
        }
    }

}
