using UnityEngine;

public class TreeSway : MonoBehaviour
{
    public float swayAmount = 1f;
    public float swaySpeed = 1f;

    private Vector3 initialRotation;

    void Start()
    {
        initialRotation = transform.eulerAngles;
    }

    void Update()
    {
        float sway = Mathf.Sin(Time.time * swaySpeed) * swayAmount;
        transform.eulerAngles = initialRotation + new Vector3(0, 0, sway);
    }
}
