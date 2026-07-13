using TMPro;
using UnityEngine;

public class ActoManager : MonoBehaviour
{
    public TextMeshProUGUI texto;

    [Header("Configuración")]
    public float tiempoEspera = 2f;
    public float velocidadFade = 1f;

    private float timer = 0f;
    private bool haciendoFade = false;

    void Update()
    {
        if (texto == null)
            return;

        if (!haciendoFade)
        {
            timer += Time.deltaTime;

            if (timer >= tiempoEspera)
            {
                haciendoFade = true;
            }
        }
        else
        {
            Color color = texto.color;
            color.a -= velocidadFade * Time.deltaTime;
            color.a = Mathf.Clamp01(color.a);

            texto.color = color;
        }
    }
}