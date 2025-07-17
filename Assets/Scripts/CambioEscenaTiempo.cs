using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAutoChanger : MonoBehaviour
{
    [Header("Tiempo antes de cambiar de escena (segundos)")]
    [Tooltip("Tiempo en segundos antes de cambiar de escena automáticamente")]
    public float tiempoDeEspera = 15f;

    private void Start()
    {
        Invoke("CambiarEscena", tiempoDeEspera);
    }

    private void CambiarEscena()
    {
        int escenaActual = SceneManager.GetActiveScene().buildIndex;
        int siguienteEscena = escenaActual + 1;

        if (siguienteEscena < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(siguienteEscena);
        }
        else
        {
            Debug.LogWarning("No hay más escenas en el Build Settings para cargar.");
        }
    }
}
