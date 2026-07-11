using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class EnemigoIA : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 2f;
    public float distanciaMovimiento = 4f;

    [Header("Visión (CONO)")]
    public float radioVision = 6f;
    [Range(0, 360)]
    public float anguloVision = 60f;

    [Header("Láser")]
    public float anchoLaser = 0.05f;

    private Vector3 puntoInicial;
    private bool derecha = true;

    private LineRenderer linea;
    private movimiento jugador;

    void Start()
    {
        puntoInicial = transform.position;

        linea = GetComponent<LineRenderer>();
        linea.positionCount = 2;
        linea.material = new Material(Shader.Find("Sprites/Default"));
        linea.useWorldSpace = true;
        linea.sortingOrder = 100;

        jugador = FindFirstObjectByType<movimiento>();
    }

    void Update()
    {
        linea.startWidth = anchoLaser;
        linea.endWidth = anchoLaser;

        Mover();
        VerJugadorCono();
    }

    void Mover()
    {
        if (derecha)
        {
            transform.Translate(Vector3.right * velocidad * Time.deltaTime, Space.World);

            if (transform.position.x >= puntoInicial.x + distanciaMovimiento)
            {
                derecha = false;
                Girar();
            }
        }
        else
        {
            transform.Translate(Vector3.left * velocidad * Time.deltaTime, Space.World);

            if (transform.position.x <= puntoInicial.x - distanciaMovimiento)
            {
                derecha = true;
                Girar();
            }
        }
    }

    void Girar()
    {
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    void VerJugadorCono()
    {
        if (jugador == null) return;

        Vector2 origen = transform.position;
        Vector2 direccion = derecha ? Vector2.right : Vector2.left;

        Vector2 dirJugador = (jugador.transform.position - transform.position).normalized;

        float distancia = Vector2.Distance(transform.position, jugador.transform.position);
        float angulo = Vector2.Angle(direccion, dirJugador);

        linea.SetPosition(0, origen);

        // ❌ fuera de rango o fuera del cono
        if (distancia > radioVision || angulo > anguloVision / 2f)
        {
            linea.SetPosition(1, origen + direccion * radioVision);
            linea.startColor = Color.red;
            linea.endColor = Color.red;
            return;
        }

        // 🟡 jugador escondido (SIN OBSTÁCULOS, SOLO ESTO)
        if (jugador.escondido)
        {
            linea.SetPosition(1, origen + direccion * radioVision);
            linea.startColor = Color.yellow;
            linea.endColor = Color.yellow;
            return;
        }

        // 🟢 detectado
        linea.SetPosition(1, jugador.transform.position);
        linea.startColor = Color.green;
        linea.endColor = Color.green;

        Debug.Log("¡Jugador detectado!");
    }
}