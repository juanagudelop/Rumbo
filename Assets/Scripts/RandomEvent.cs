using UnityEngine;
using UnityEngine.UI; // Necesario para trabajar con UI Image

public class RandomEvent : MonoBehaviour
{
    [Header("Configuración")]
    [Tooltip("Tiempo entre posibles eventos de caca (segundos)")]
    public float intervaloEntreCacas = 15f;
    [Tooltip("Tiempo para limpiar antes de que ocurra daño (segundos)")]
    public float tiempoLimiteLimpieza = 20f;
    [Tooltip("Cantidad de daño por ciclo (0-1)")]
    [Range(0, 1)] public float danioPorCiclo = 0.1f;

    private Animator animator;
    private float tiempoProximaCaca;
    private float contadorIncomodidad;
    private bool necesitaLimpieza;
    private Image barraVidaImage;

    void Start()
    {
        GameObject barraVidaObj = GameObject.FindWithTag("barraVida");
        if (barraVidaObj != null)
        {
            barraVidaImage = barraVidaObj.GetComponent<Image>();
            if (barraVidaImage == null)
            {
                Debug.LogError("El objeto con tag 'barraVida' no tiene componente Image");
            }
        }
        else
        {
            Debug.LogError("No se encontró objeto con tag 'barraVida'");
        }

        animator = GetComponent<Animator>();
        tiempoProximaCaca = Time.time + intervaloEntreCacas;
        contadorIncomodidad = 0f;
        necesitaLimpieza = false;
    }

    void Update()
    {
        // Sistema de caca automático
        if (Time.time >= tiempoProximaCaca && !necesitaLimpieza)
        {
            Cagar();
        }

        // Conteo de incomodidad si no lo limpian
        if (necesitaLimpieza)
        {
            contadorIncomodidad += Time.deltaTime;

            if (contadorIncomodidad >= tiempoLimiteLimpieza)
            {
                DañoPorNoLimpiar();
                contadorIncomodidad = 0f;
            }
        }
    }

    void Cagar()
    {
        animator.SetBool("isPoop", true);
        necesitaLimpieza = true;
        tiempoProximaCaca = Time.time + intervaloEntreCacas;
        Debug.Log("¡El bebé se ha cagado! Necesita limpieza");
    }

    public void DañoPorNoLimpiar()
    {
        if (barraVidaImage != null)
        {
            barraVidaImage.fillAmount -= danioPorCiclo;
            Debug.Log($"¡Daño recibido! Vida actual: {barraVidaImage.fillAmount * 100}%");
            
            if (barraVidaImage.fillAmount <= 0)
            {
                Debug.Log("¡El bebé ha muerto por falta de cuidados!");
                // Aquí puedes añadir lógica de game over
            }
        }
    }

    public void LimpiarBebe()
    {
        animator.SetBool("isPoop", false);
        necesitaLimpieza = false;
        contadorIncomodidad = 0f;
        Debug.Log("El bebé ha sido limpiado");
    }

    public void dejaDeCagar()
    {
        animator.SetBool("isPoop", false);
    }
}