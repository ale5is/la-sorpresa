using UnityEngine;

public class desactivarObjeto : MonoBehaviour
{
    [Header("Objeto a desactivar")]
    public GameObject objeto;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica si tiene el tag Player
        if (collision.gameObject.CompareTag("Player"))
        {
            objeto.SetActive(false);
        }
    }
}