using UnityEngine;

public class Elevador : MonoBehaviour
{
    [Header("Movimiento")]
    public float altura = 5f;
    public float velocidad = 2f;

    private Vector3 puntoA;
    private Vector3 puntoB;
    private Vector3 objetivo;

    void Start()
    {
        puntoA = transform.position;
        puntoB = transform.position + Vector3.up * altura;

        objetivo = puntoB;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            objetivo,
            velocidad * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, objetivo) < 0.01f)
        {
            objetivo = (objetivo == puntoA) ? puntoB : puntoA;
        }
    }

    // ✅ 2D COLLISION ENTER
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    // ✅ 2D COLLISION EXIT
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}