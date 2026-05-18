using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public dialogo control;

    [Header("Tiempo antes de iniciar")]
    public float tiempoInicio = 3f;

    public float timer;

    private bool activado = false;
    private bool enterUsado = false;

    void Update()
    {
        // ENTER solo funciona una vez
        if (!enterUsado && Input.GetKeyDown(KeyCode.Return))
        {
            control.controlDialogo();
            enterUsado = true;
            tiempoInicio = 23;
            activado = false;
            
        }

        // Timer automático
        if (!activado)
        {
            timer += Time.deltaTime;

            if (timer >= tiempoInicio)
            {
                activado = true;
                timer = 0;

                control.controlDialogo();
            }
        }
    }
}