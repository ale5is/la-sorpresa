using UnityEngine;

public class moverNarrador : MonoBehaviour
{
    private RectTransform rect;

    [Header("Primera posición")]
    public Vector2 destino1 = new Vector2(0, 0);

    [Header("Posición de teletransporte")]
    public Vector2 teleport = new Vector2(0, 0);

    [Header("Segunda posición")]
    public Vector2 destino2 = new Vector2(0, 0);

    [Header("Velocidad")]
    public float velocidad = 500f;

    [Header("Tiempo antes de iniciar")]
    public float tiempoInicio = 3f;

    public bool activar;

    public int estado = 0;

    private float timer;
    private bool enterUsado = false;

    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        // ENTER solo una vez
        if (!enterUsado && Input.GetKeyDown(KeyCode.Return))
        {
            enterUsado = true;

            // activar sistema
            activar = true;

            // reiniciar timer
            timer = 0;
        }

        // Timer antes de empezar
        if (activar)
        {
            timer += Time.deltaTime;

            if (timer >= tiempoInicio)
            {
                // Ir al primer destino
                if (estado == 0)
                {
                    rect.anchoredPosition = Vector2.MoveTowards(
                        rect.anchoredPosition,
                        destino1,
                        velocidad * Time.deltaTime
                    );

                    if (rect.anchoredPosition == destino1)
                    {
                        Debug.Log("TELEPORT");

                        // Teletransporte
                        rect.anchoredPosition = teleport;

                        estado = 1;
                    }
                }

                // Ir al segundo destino
                else if (estado == 1)
                {
                    rect.anchoredPosition = Vector2.MoveTowards(
                        rect.anchoredPosition,
                        destino2,
                        velocidad * Time.deltaTime
                    );

                    if (rect.anchoredPosition == destino2)
                    {
                        Debug.Log("LLEGÓ AL FINAL");

                        activar = false;

                        estado = 2;
                    }
                }
            }
        }
    }
}