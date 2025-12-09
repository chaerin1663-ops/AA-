using UnityEngine;

public class updown : MonoBehaviour
{

    public float initialSpeed = 5.0f;
    public float accelerationRate = 0.1f;
    private float currentSpeed;

    public float hoverAmplitude = 0.5f;
    public float hoverFrequency = 2.0f;

    private Vector3 startPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        currentSpeed = initialSpeed;
    
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed += accelerationRate * Time.deltaTime;
        transform.Translate(Vector3.left * currentSpeed * Time.deltaTime);

        HoverMovement();
    }

    private void HoverMovement()
    {
        float yOffset = Mathf.Sin(Time.time * hoverFrequency) * hoverAmplitude;
        transform.position = new Vector3(
            transform.position.x,
            startPosition.y+yOffset,
            startPosition.z
            );
    }    
}
