using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovEnemy : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;
    public float rangoDeSonido = 8f;
    public AudioSource audioSource;
    public AudioSource gritoSource;
    public FieldOfView fov;

    private Rigidbody2D rb;
    private Vector2 movement;
    private GameOverManager gameOverManager;
    private bool gameOverActivado = false; // Evita múltiples activaciones
    private bool puedeGritar = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameOverManager = FindFirstObjectByType<GameOverManager>();

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        audioSource.loop = true;
        audioSource.Play();
        audioSource.volume = 0f;
    }

    void Update()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            movement = direction;

            ControlarSonido();
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !gameOverActivado)
        {
            gameOverActivado = true;
            gameOverManager.ActivarGameOverPanel();
            Debug.Log("Game Over Activado");
        }
    }

    void ControlarSonido()
{
    if (PauseMenu.isPaused) // Verifica si el juego está pausado
    {
        audioSource.Pause(); // Pausa el audio del enemigo
        return;
    }
    else
    {
        audioSource.UnPause(); // Reanuda el audio si estaba pausado
    }

    float distancia = Vector2.Distance(transform.position, player.position);
  
    if (distancia < rangoDeSonido)
    {
        if (EstaEnCampoDeVision())
        {
            audioSource.volume = 1f;

            if (puedeGritar)
            {
                StartCoroutine(ReproducirGrito());
            }
        }
        else
        {
            audioSource.volume = 0.3f;
        }

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
    else
    {
        audioSource.volume = 0f;
    }
}


    IEnumerator ReproducirGrito()
    {
        puedeGritar = false;
        gritoSource.Play();
        yield return new WaitForSeconds(16f);
        puedeGritar = true;
    }


    bool EstaEnCampoDeVision()
    {
        if (fov == null) return false;

        float distanciaEnVision = fov.distanciaVision;
        float distancia = Vector2.Distance(transform.position, player.position);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, (player.position - transform.position).normalized, distanciaEnVision, fov.LayerMask);

        bool enVision = distancia <= distanciaEnVision && (hit.collider == null || hit.collider.CompareTag("Player"));

        Debug.Log($" Murciélago en visión: {enVision} (Distancia en visión: {distanciaEnVision}, Distancia real: {distancia}, Hit: {(hit.collider != null ? hit.collider.name : "Ninguno")})");

        return enVision;
    }
}