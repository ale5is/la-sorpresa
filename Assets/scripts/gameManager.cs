using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public dialogo control;

    [Header("Tiempo para pausar")]
    public float tiempo = 0;

    public float timer;

    public bool activado = false;
    private bool enterUsado = false;

    void Update()
    {
        // ENTER solo funciona una vez
        if (!enterUsado && Input.GetKeyDown(KeyCode.Return))
        {
            control.controlDialogo();
            enterUsado = true;
            tiempo = 20;
            activado = false;

        }

        // Timer automático
        if (!activado)
        {
            timer += Time.deltaTime;

            if (timer >= tiempo)
            {
                activado = true;
                timer = 0;

                control.controlDialogo();
            }
        }
    }

    public void Activar(int tiempo)
    {
        control.controlDialogo();
        enterUsado = true;
        tiempo = tiempo;
        activado = false;
    }
}