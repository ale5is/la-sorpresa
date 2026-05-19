using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class edMovimiento : MonoBehaviour
{
    public moverNarrador narrador;
    public gameManager game;
    [Header("Primera posición")]
    public Vector2 destino1 = new Vector2(0, 0);

    [Header("Posición de teletransporte")]
    public Vector2 teleport = new Vector2(0, 0);

    [Header("Segunda posición")]
    public Vector2 destino2 = new Vector2(0, 0);
    public int estado,tiempo;
    public bool activar;


    // Update is called once per frame
    void Update()
    {
        if (narrador.estado == 2 && activar)
        {
            activar = false;
            narrador.estado = estado;
            narrador.tiempo = 0;
            narrador.destino1 = destino1;
            narrador.teleport = teleport;
            narrador.destino2 = destino2;
            narrador.activar = true;
            if (game != null)
            {
                game.Activar(tiempo);
            }
        }
    }
}
