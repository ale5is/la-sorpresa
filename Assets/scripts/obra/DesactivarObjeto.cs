using UnityEngine;

public class DesaparecerObjeto : MonoBehaviour
{
    public GameObject objetoADesaparecer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (objetoADesaparecer != null)
                objetoADesaparecer.SetActive(false);
        }
    }
}