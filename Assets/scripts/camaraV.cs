using UnityEngine;

public class camaraV : MonoBehaviour
{
    public Transform player;
    public float offsetY = 2f;

    void LateUpdate()
    {
        Vector3 posicion = transform.position;

        // Solo sigue al jugador en vertical (eje Y)
        posicion.y = player.position.y + offsetY;

        transform.position = posicion;
    }
}