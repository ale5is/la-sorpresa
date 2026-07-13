using TMPro;
using UnityEngine;

[System.Serializable]
public class GrupoDialogo
{
    [TextArea(2, 5)]
    public string[] frases;
}

public class dialogo : MonoBehaviour
{
    public TextMeshProUGUI texto;

    [Header("Velocidad escritura")]
    public float velocidadLetras = 0.05f;

    [Header("Tiempo entre frases")]
    public float tiempoEntreFrases = 2f;

    [Header("Control")]
    public bool continuarDialogo = true;

    [Header("Grupos de diálogo")]
    public GrupoDialogo[] grupos;

    private int grupoActual = 0;
    private int fraseActual = 0;
    private int letraActual = 0;

    private float timerLetras = 0f;
    private float timerFrase = 0f;

    private bool escribiendo = true;
    private bool esperandoSiguiente = false;

    void Start()
    {
        texto.text = "";

        if (grupos.Length == 0 || grupos[0].frases.Length == 0)
        {
            enabled = false;
            return;
        }
    }

    void Update()
    {
        if (escribiendo)
        {
            timerLetras += Time.deltaTime;

            if (timerLetras >= velocidadLetras)
            {
                timerLetras = 0f;

                string frase = grupos[grupoActual].frases[fraseActual];

                if (letraActual < frase.Length)
                {
                    texto.text += frase[letraActual];
                    letraActual++;
                }
                else
                {
                    escribiendo = false;
                    esperandoSiguiente = true;
                    timerFrase = 0f;
                }
            }
        }
        else if (esperandoSiguiente)
        {
            if (!continuarDialogo)
                return;

            timerFrase += Time.deltaTime;

            if (timerFrase >= tiempoEntreFrases)
            {
                fraseActual++;

                // ¿Quedan frases en este grupo?
                if (fraseActual < grupos[grupoActual].frases.Length)
                {
                    texto.text = "";
                    letraActual = 0;

                    escribiendo = true;
                    esperandoSiguiente = false;
                }
                else
                {
                    // Terminó el grupo
                    continuarDialogo = false;
                    esperandoSiguiente = false;
                }
            }
        }
    }

    // Llamar desde otro script para pasar al siguiente grupo
    public void controlDialogo()
    {
        if (grupoActual >= grupos.Length)
            return;

        // Si terminó el grupo actual, pasar al siguiente
        if (fraseActual >= grupos[grupoActual].frases.Length)
        {
            grupoActual++;

            if (grupoActual >= grupos.Length)
                return;

            fraseActual = 0;
        }

        texto.text = "";
        letraActual = 0;

        escribiendo = true;
        esperandoSiguiente = false;
        continuarDialogo = true;
    }
}