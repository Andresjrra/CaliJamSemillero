using UnityEngine;

public class ItemRestauracion : MonoBehaviour
{
    public FieldOfView fov; // Se asigna en el Inspector
    public float visionRestaurada = 8f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Colisión detectada con: " + collision.gameObject.name);

        if (collision.CompareTag("Player") && fov != null)
        {
            Debug.Log("Se restauró la visión");
            fov.RestaurarVision(visionRestaurada);
            Destroy(gameObject);
        }
        else if (fov == null)
        {
            Debug.LogError("El objeto 'FOV' no está asignado en el Inspector.");
        }
    }
}