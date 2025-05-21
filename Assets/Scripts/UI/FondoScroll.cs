using UnityEngine;

public class FondoScrollUI : MonoBehaviour
{
    public RectTransform fondo1;
    public RectTransform fondo2;
    public float velocidad = 50f;

    private float anchoFondo;

    void Start()
    {
        // Calcula el ancho basado en el RectTransform
        anchoFondo = fondo1.rect.width;
    }

    void Update()
    {
        float movimiento = velocidad * Time.deltaTime;

        // Mueve ambos fondos hacia la izquierda
        fondo1.anchoredPosition -= new Vector2(movimiento, 0);
        fondo2.anchoredPosition -= new Vector2(movimiento, 0);

        // Si uno de los fondos se sale completamente a la izquierda, lo mandamos al final del otro
        if (fondo1.anchoredPosition.x <= -anchoFondo)
        {
            fondo1.anchoredPosition = new Vector2(fondo2.anchoredPosition.x + anchoFondo, fondo1.anchoredPosition.y);
        }

        if (fondo2.anchoredPosition.x <= -anchoFondo)
        {
            fondo2.anchoredPosition = new Vector2(fondo1.anchoredPosition.x + anchoFondo, fondo2.anchoredPosition.y);
        }
    }
}
