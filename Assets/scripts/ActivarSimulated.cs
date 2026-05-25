using UnityEngine;

public class ActivarSimulated : MonoBehaviour
{
    [Header("Rigidbody")]
    public Rigidbody2D rb;

    [Header("Activar por tecla")]
    public bool usarTecla = false;
    public KeyCode tecla = KeyCode.E;

    [Header("Activar por collider")]
    public bool usarCollider = true;
    public string tagActivador = "Player";

    

    void Update()
    {
        // Activar con tecla
        if (usarTecla && Input.GetKeyDown(tecla))
        {
            ASimulated();
        }
    }

    // Activar al tocar collider 2D
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!usarCollider) return;

        if (other.CompareTag(tagActivador))
        {
            ASimulated();
        }
    }

    public void ASimulated()
    {
        rb.simulated = true;
    }

    public void DeSimulated()
    {
        rb.simulated = false;
    }
}