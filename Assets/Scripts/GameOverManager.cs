using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;


    void Awake()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false); 
        }
    }

    public void ActivarGameOverPanel()
    {
        if (gameOverPanel != null)
        {
            Time.timeScale = 0; // Detiene el tiempo del juego
            gameOverPanel.SetActive(true); 
            Debug.Log("Panel de Game Over activado"); // Verifica en la consola si se activa correctamente
        }
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene("Menu"); 
    }

    public void ReiniciarNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }

    public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego..."); 
        Application.Quit();
    }
}