using System.Diagnostics.Contracts;
using UnityEngine;

public class BebeCacaSystem : MonoBehaviour
{
    [Header("Configuración")]
    [Tooltip("Tiempo entre posibles eventos de caca (segundos)")]
    public float intervaloEntreCacas = 15f;
    [Tooltip("Tiempo para limpiar antes de que ocurra daño (segundos)")]
    public float tiempoLimiteLimpieza = 20f;

    private Animator animator;
    private float tiempoProximaCaca;
    private float contadorIncomodidad;
    private bool necesitaLimpieza;

    void Start()
    {
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
            Debug.Log(contadorIncomodidad);
            if (contadorIncomodidad >= tiempoLimiteLimpieza)
            {
                DañoPorNoLimpiar();
                contadorIncomodidad = 0f; // Reinicia el conteo para siguiente ciclo de daño
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

    void DañoPorNoLimpiar()
    {
        Debug.Log("¡AVISO CRÍTICO! El bebé lleva demasiado tiempo sucio y sufre daño");
        // Aquí puedes añadir efectos visuales, sonidos, o lógica de daño real
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
        // yaCago = false; // Si quieres que pueda cagar de nuevo después, descomenta esta línea
    }
}