using UnityEngine;
using UnityEngine.SceneManagement;

public class cambiarEscena : MonoBehaviour
{
    [Header("Nombre de la escena")]
    public string nombreEscena;

    // Colisión 2D
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica si es el Player
        if (collision.gameObject.CompareTag("Player"))
        {
            CargarEscena();
        }
    }

    // Función pública para botones
    public void CargarEscena()
    {
        SceneManager.LoadScene(nombreEscena);
    }
}