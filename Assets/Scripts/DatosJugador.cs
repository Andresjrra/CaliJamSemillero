using TMPro;
using UnityEngine;
using UnityEditor.PackageManager.Requests;

public class DatosJugador : MonoBehaviour
{
    
    public TextMeshProUGUI tiempoRestante;
    public float tiempo = 20f;

  void Start()
{
      Time.timeScale = 1; // Asegúrate de que el tiempo del juego esté en marcha al inicio
        
    }

    void Update()
    {
        if (tiempo > 0)
        {
            tiempo -= Time.deltaTime;
            tiempoRestante.text = "" + Mathf.Ceil(tiempo).ToString();

        }
        else
        {
            tiempo = 0;
            tiempoRestante.text = "0";
        }
    }
}
