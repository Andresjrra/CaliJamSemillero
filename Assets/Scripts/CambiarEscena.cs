using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{
    public void CambiarEscene(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }
}
