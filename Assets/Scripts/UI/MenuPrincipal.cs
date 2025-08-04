using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuPrincipal : MonoBehaviour
{
    public GameObject pantallaTransicion;

    public void Jugar()
    {
        StartCoroutine(MostrarTransicionYJugar());
    }

    IEnumerator MostrarTransicionYJugar()
    {
        pantallaTransicion.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Salir()
    {
        Application.Quit();
    }
}
