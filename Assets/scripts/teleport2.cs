using UnityEngine;

public class teleport2 : MonoBehaviour
{
    [Header("Jugador")]
    public Transform player;

    [Header("Puntos de Teletransporte")]
    public Transform puntoIzquierdo;
    public Transform puntoDerecho;

    void Update()
    {
        // Sale por derecha → aparece en izquierda
        if (player.position.x > puntoDerecho.position.x)
        {
            Vector3 pos = player.position;
            pos.x = puntoIzquierdo.position.x;
            player.position = pos;
        }

        // Sale por izquierda → aparece en derecha
        if (player.position.x < puntoIzquierdo.position.x)
        {
            Vector3 pos = player.position;
            pos.x = puntoDerecho.position.x;
            player.position = pos;
        }
    }
}