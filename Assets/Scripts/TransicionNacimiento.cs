using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TransicionNacimiento : MonoBehaviour
{
    public CanvasGroup pantallaNegra;
    public VideoPlayer videoPlayer;
    public AudioSource musicaAmbiental;
    public GameObject textoFinal;
    private CanvasGroup textoCanvasGroup;

    public float esperaInicial = 4f; // ⬆ Se mantiene la espera más larga
    public float duracionTransicionInicial = 2f;
    public float duracionTexto = 3f;

    public float esperaAntesDeCambiarEscena = 5f;

    void Start()
    {
        pantallaNegra.alpha = 1;
        musicaAmbiental.volume = 0.05f;

        textoCanvasGroup = textoFinal.GetComponent<CanvasGroup>();

        if (textoCanvasGroup != null)
        {
            textoCanvasGroup.alpha = 0;
        }
        else
        {
            Debug.LogWarning("⚠️ El objeto 'textoFinal' no tiene CanvasGroup. Agrégaselo si quieres controlar su visibilidad con alpha.");
        }

        StartCoroutine(ProcesoTransicion());
    }

    IEnumerator ProcesoTransicion()
    {
        yield return new WaitForSeconds(esperaInicial);

        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }

        videoPlayer.Play();
        videoPlayer.Pause(); // ⏸ Mostramos el primer frame sin empezar el video aún

        StartCoroutine(DesvanecerPantallaNegra(duracionTransicionInicial)); // 🔹 Aquí se inicia la transición suave

        yield return new WaitForSeconds(duracionTransicionInicial); // ⏳ Esperar la transición

        videoPlayer.Play(); // ▶ Inicia el video justo al terminar la transición

        yield return new WaitForSeconds(3f);

        if (textoCanvasGroup != null)
        {
            textoCanvasGroup.alpha = 1;
        }

        Debug.Log("Texto final activado. Estado: " + textoFinal.activeSelf);

        // Espera adicional para que el jugador vea el texto final
        yield return new WaitForSeconds(esperaAntesDeCambiarEscena);

        // Cambia de escena (ajusta el nombre según el tuyo)
        UnityEngine.SceneManagement.SceneManager.LoadScene("BebeGatea");
    }

    IEnumerator DesvanecerPantallaNegra(float duracion)
    {
        float tiempo = 0;
        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            pantallaNegra.alpha = Mathf.Lerp(1, 0, tiempo / duracion);
            yield return null;
        }
        pantallaNegra.alpha = 0;
    }
}
