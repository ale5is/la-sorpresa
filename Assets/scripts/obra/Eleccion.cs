using UnityEngine;
using UnityEngine.SceneManagement;

public class Eleccion : MonoBehaviour
{
    [Header("Escenas")]
    public string escenaOpcion1;
    public string escenaOpcion2;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene(escenaOpcion1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene(escenaOpcion2);
        }
    }
}