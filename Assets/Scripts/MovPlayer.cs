using UnityEngine;

public class MovPlayer : MonoBehaviour
{
    public FieldOfView fov;
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject fovObject = GameObject.Find("FOV"); // Buscar el objeto "FOV"
        if (fovObject != null)
        {
            fov = fovObject.GetComponent<FieldOfView>();
        }

        if (fov == null)
        {
            Debug.LogError("No se encontró el objeto 'FOV' con FieldOfView.");
        }
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        fov.SetOrigin(transform.position);

        // Verificar la dirección y hacer flip si es necesario
        if (movement.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (movement.x < 0 && facingRight)
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement.normalized * speed;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (facingRight ? 1 : -1); // Asegurar que la escala se invierta correctamente
        transform.localScale = scale;
    }
}
