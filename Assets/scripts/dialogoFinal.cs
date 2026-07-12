using System.Collections;
using TMPro;
using UnityEngine;

public class dialogoFinal : MonoBehaviour
{
    public TextMeshProUGUI texto;

    [TextArea]
    public string mensaje;

    public float tiempoEntreLetras = 0.03f;
    public float duracionFade = 0.2f;

    public GameObject objetoAlFinal;

    void Start()
    {
        if (objetoAlFinal != null)
            objetoAlFinal.SetActive(false);

        StartCoroutine(Escribir());
    }

    IEnumerator Escribir()
    {
        texto.text = mensaje;
        texto.ForceMeshUpdate();

        TMP_TextInfo textInfo = texto.textInfo;

        // Ocultar todas las letras
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            if (!textInfo.characterInfo[i].isVisible)
                continue;

            int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;
            int vertexIndex = textInfo.characterInfo[i].vertexIndex;

            Color32[] colors = textInfo.meshInfo[materialIndex].colors32;

            colors[vertexIndex + 0].a = 0;
            colors[vertexIndex + 1].a = 0;
            colors[vertexIndex + 2].a = 0;
            colors[vertexIndex + 3].a = 0;
        }

        texto.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

        // Mostrar cada letra con fade
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            if (!textInfo.characterInfo[i].isVisible)
                continue;

            yield return StartCoroutine(FadeCaracter(i));
            yield return new WaitForSeconds(tiempoEntreLetras);
        }

        // Activar el objeto al terminar el diálogo
        if (objetoAlFinal != null)
            objetoAlFinal.SetActive(true);
    }

    IEnumerator FadeCaracter(int index)
    {
        TMP_TextInfo textInfo = texto.textInfo;

        int materialIndex = textInfo.characterInfo[index].materialReferenceIndex;
        int vertexIndex = textInfo.characterInfo[index].vertexIndex;

        Color32[] colors = textInfo.meshInfo[materialIndex].colors32;

        float t = 0;

        while (t < duracionFade)
        {
            t += Time.deltaTime;
            byte alpha = (byte)Mathf.Lerp(0, 255, t / duracionFade);

            colors[vertexIndex + 0].a = alpha;
            colors[vertexIndex + 1].a = alpha;
            colors[vertexIndex + 2].a = alpha;
            colors[vertexIndex + 3].a = alpha;

            texto.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

            yield return null;
        }

        colors[vertexIndex + 0].a = 255;
        colors[vertexIndex + 1].a = 255;
        colors[vertexIndex + 2].a = 255;
        colors[vertexIndex + 3].a = 255;

        texto.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
    }
}