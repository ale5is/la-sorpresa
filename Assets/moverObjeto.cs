using UnityEngine;

public class MoverAPosicion : MonoBehaviour
{
    [Header("Mover relativo a la posición inicial")]
    public Vector3 moverHacia = new Vector3(5, 0, 0);

    public float velocidad = 5f;

    [Header("Tiempo antes de moverse")]
    public float tiempoInicio = 3f;

    private Vector3 destino;

    private float timer;
    public bool movimientoActivo = false;
    private bool enterUsado = false;

    void Start()
    {
        // Posición destino
        destino = transform.position + moverHacia;
    }

    void Update()
    {
        // ENTER solo una vez
        if (!enterUsado && Input.GetKeyDown(KeyCode.Return))
        {
            enterUsado = true;

            // Reinicia timer
            timer = 0;

            // Activa inicio del timer
            movimientoActivo = true;
        }

        // Timer
        if (movimientoActivo)
        {
            timer += Time.deltaTime;

            // Cuando termina el tiempo empieza a moverse
            if (timer >= tiempoInicio)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    destino,
                    velocidad * Time.deltaTime
                );
            }
            if (transform.position == destino)
            {
                movimientoActivo = false;

                Debug.Log("Llegó");
            }

        }
    }
}