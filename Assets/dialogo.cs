using TMPro;
using UnityEngine;

public class dialogo : MonoBehaviour
{
    public TextMeshProUGUI texto;

    [Header("Velocidad escritura")]
    public float velocidadLetras = 0.05f;

    [Header("Tiempo entre mensajes")]
    public float tiempoEntreMensajes = 2f;

    [Header("Control")]
    public bool continuarDialogo = true;

    private string[] mensajes =
    {
        "Bienvenido al juego",
        "Sere tu guia",
        "Para empezar presiona ENTER",
        "Ah",
        "...",
        "...", 
        "No te preocupes¡",
        "Por suerte exite un reparador de bugs¡",
        "Pero primero...",
        "",
        "limpiemos un poco",
        "",
        "ahora si",
        "puedes tocar los ajustes",
    };

    private int mensajeActual = 0;
    private int letraActual = 0;

    private float timerLetras = 0f;
    private float timerMensaje = 0f;

    private bool escribiendo = true;
    private bool esperandoSiguiente = false;
    private bool puedeEmpezar = false;

    void Start()
    {
        texto.text = "";
    }

    void Update()
    {
        if (escribiendo)
        {
            timerLetras += Time.deltaTime;

            if (timerLetras >= velocidadLetras)
            {
                timerLetras = 0f;

                if (letraActual < mensajes[mensajeActual].Length)
                {
                    texto.text += mensajes[mensajeActual][letraActual];
                    letraActual++;
                }
                else
                {
                    escribiendo = false;
                    esperandoSiguiente = true;
                    timerMensaje = 0f;
                }
            }
        }

        //ESPERAR ENTRE MENSAJES
        else if (esperandoSiguiente)
        {
            if (!continuarDialogo)
                return;

            timerMensaje += Time.deltaTime;

            if (timerMensaje >= tiempoEntreMensajes)
            {
                mensajeActual++;

                if (mensajeActual < mensajes.Length)
                {
                    texto.text = "";
                    letraActual = 0;

                    escribiendo = true;
                    esperandoSiguiente = false;
                }
                else
                {
                    esperandoSiguiente = false;
                    puedeEmpezar = true;
                }
            }
        }
    }

    public void controlDialogo()
    {
        continuarDialogo = !continuarDialogo;
    }
}