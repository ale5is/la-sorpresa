using UnityEngine;

public class Camara : MonoBehaviour
{
    [Header("Jugador")]
    public Transform jugador;

    [Header("Posición de la cámara")]
    public Vector3 desplazamiento = new Vector3(0f, 2f, -10f);

    [Header("Zona muerta")]
    public float zonaMuertaHorizontal = 2f;
    public float zonaMuertaVertical = 1f;

    [Header("Movimiento")]
    public float tiempoSuavizado = 0.2f;
    private Vector3 velocidadCamara;

    [Header("Límites de la cámara")]
    public float limiteMinimoX = -20f;
    public float limiteMaximoX = 20f;
    public float limiteMinimoY = -5f;
    public float limiteMaximoY = 15f;

    void LateUpdate()
    {
        Vector3 posicionObjetivo = transform.position;

        // Movimiento horizontal
        float diferenciaHorizontal = (jugador.position.x + desplazamiento.x) - transform.position.x;
        if (Mathf.Abs(diferenciaHorizontal) > zonaMuertaHorizontal)
        {
            posicionObjetivo.x = jugador.position.x + desplazamiento.x -
                                 Mathf.Sign(diferenciaHorizontal) * zonaMuertaHorizontal;
        }

        // Movimiento vertical
        float diferenciaVertical = (jugador.position.y + desplazamiento.y) - transform.position.y;
        if (Mathf.Abs(diferenciaVertical) > zonaMuertaVertical)
        {
            posicionObjetivo.y = jugador.position.y + desplazamiento.y -
                                 Mathf.Sign(diferenciaVertical) * zonaMuertaVertical;
        }

        // Mantener la profundidad de la cámara
        posicionObjetivo.z = desplazamiento.z;

        // Aplicar límites
        posicionObjetivo.x = Mathf.Clamp(posicionObjetivo.x, limiteMinimoX, limiteMaximoX);
        posicionObjetivo.y = Mathf.Clamp(posicionObjetivo.y, limiteMinimoY, limiteMaximoY);

        // Mover la cámara suavemente
        transform.position = Vector3.SmoothDamp(
            transform.position,
            posicionObjetivo,
            ref velocidadCamara,
            tiempoSuavizado
        );
    }
}