using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscenaTecla : MonoBehaviour
{
    [Header("Escena a cargar")]
    public string nombreEscena;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(nombreEscena);
        }
    }
}