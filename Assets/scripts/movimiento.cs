using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class movimiento : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 5f;
    public float jumpForce = 7f;

    [Header("Referencias")]
    public SpriteRenderer sprite;
    public Animator anim;
    public Rigidbody2D rb;

    [Header("Vida")]
    public int vidaMax = 5;
    public int vidaActual;

    [Header("Vidas")]
    public int vidas = 3;
    public TMP_Text textoVidas;

    [Header("UI")]
    public Slider barraVida;
    public GameObject canvasGameOver;

    [Header("Invulnerabilidad")]
    public float tiempoInvulnerable = 1.5f;
    private float timerInvulnerabilidad;
    private bool invulnerable;

    [Header("Estado")]
    public bool escondido; // 👈 SE VE EN INSPECTOR

    private bool isGrounded;
    private bool muerto;
    private Vector3 puntoRespawn;

    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();

        vidaActual = vidaMax;
        puntoRespawn = transform.position;

        UpdateUI();
        //canvasGameOver?.SetActive(false);
    }

    void Update()
    {
        if (muerto)
        {
            if (Input.GetKeyDown(KeyCode.R))
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }

        Move();
        Jump();
        Flip();
        HandleInvulnerability();
    }

    // ---------------- MOVIMIENTO ----------------
    void Move()
    {
        float move = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        if (anim) anim.SetBool("running", move != 0);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void Flip()
    {
        float move = Input.GetAxisRaw("Horizontal");

        if (move != 0)
            sprite.flipX = move < 0;
    }

    // ---------------- VIDA ----------------
    public void RecibirDaño(int dmg)
    {
        if (invulnerable || muerto) return;

        ApplyDamage(dmg);
        invulnerable = true;
        timerInvulnerabilidad = tiempoInvulnerable;
    }

    void ApplyDamage(int dmg)
    {
        vidaActual -= dmg;
        UpdateUI();

        if (vidaActual <= 0)
            LoseLife();
    }

    void LoseLife()
    {
        vidas--;
        UpdateUI();

        if (vidas <= 0)
            Die();
        else
            Respawn();
    }

    void Respawn()
    {
        vidaActual = vidaMax;
        transform.position = puntoRespawn;
        rb.velocity = Vector2.zero;
        UpdateUI();
    }

    void Die()
    {
        muerto = true;
        canvasGameOver?.SetActive(true);
        rb.velocity = Vector2.zero;
    }

    // ---------------- INVULNERABILIDAD ----------------
    void HandleInvulnerability()
    {
        if (!invulnerable) return;

        timerInvulnerabilidad -= Time.deltaTime;

        if (timerInvulnerabilidad <= 0)
            invulnerable = false;
    }

    // ---------------- UI ----------------
    void UpdateUI()
    {
        if (barraVida) barraVida.value = vidaActual;
        if (textoVidas) textoVidas.text = "Vidas: " + vidas;
    }

    // ---------------- COLISIONES ----------------
    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("suelo"))
            isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("suelo"))
            isGrounded = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spawn"))
            puntoRespawn = other.transform.position;

        if (other.CompareTag("escondite"))
            escondido = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("escondite"))
            escondido = false;
    }
}