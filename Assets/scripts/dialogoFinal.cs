using TMPro;
using UnityEngine;

public class dialogoFinal : MonoBehaviour
{
    public TextMeshProUGUI texto;

    [TextArea]
    public string mensaje;

    public float tiempoEntrePalabras = 0.4f;
    public float duracionFade = 0.3f;

    private string[] palabras;

    private int palabraActual = 0;
    private float timer = 0;

    private bool haciendoFade = false;
    private float fadeTimer = 0;

    private string textoFijo = "";
    private string palabraFade = "";

    void Start()
    {
        palabras = mensaje.Split(' ');
        texto.text = "";
    }

    void Update()
    {
        if (!haciendoFade)
        {
            timer += Time.deltaTime;

            if (timer >= tiempoEntrePalabras && palabraActual < palabras.Length)
            {
                timer = 0;

                palabraFade = palabras[palabraActual];
                fadeTimer = 0;
                haciendoFade = true;
            }
        }
        else
        {
            fadeTimer += Time.deltaTime;

            float t = Mathf.Clamp01(fadeTimer / duracionFade);
            int alpha = Mathf.RoundToInt(t * 255);

            string hexAlpha = alpha.ToString("X2");

            texto.text =
                textoFijo +
                $"<color=#FFFFFF{hexAlpha}>{palabraFade}</color>";

            if (fadeTimer >= duracionFade)
            {
                textoFijo += palabraFade + " ";
                texto.text = textoFijo;

                palabraActual++;
                haciendoFade = false;
            }
        }
    }
}