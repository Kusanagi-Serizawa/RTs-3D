using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCam : MonoBehaviour
{
    public float speed = 15.0f; // Velocidad de movimiento de la cámara
    public float zoomSpeed = 10.0f; // Velocidad de zoom de la cámara
    public float minHeight = 10.0f; // Altura mínima de la cámara
    public float maxHeight = 20.0f; // Altura máxima de la cámara
    public float width = 50.0f; // Límite de ancho para el movimiento de la cámara

    void Update()
    {
        // Movimiento horizontal y vertical de la cámara
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        // Calculamos el desplazamiento de la cámara
        Vector3 movement = new Vector3(horizontalMovement, 0.0f, verticalMovement) * speed * Time.deltaTime;

        // Aplicamos el desplazamiento a la posición actual de la cámara
        transform.Translate(movement, Space.World);

        // Limitamos la posición en el eje x de la cámara dentro del ancho permitido
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, -width, width);
        transform.position = position;

        // Control de zoom con las teclas Q y E
        if (Input.GetKey(KeyCode.Q))
        {
            ZoomCamera(-zoomSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            ZoomCamera(zoomSpeed * Time.deltaTime);
        }
    }

    void ZoomCamera(float increment)
    {
        Vector3 position = transform.position;
        position.y = Mathf.Clamp(position.y + increment, minHeight, maxHeight);
        transform.position = position;
    }
}
