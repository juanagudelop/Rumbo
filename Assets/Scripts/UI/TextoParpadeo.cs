using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TextoParpadeoRojo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI texto;
    private Color colorOriginal;
    private Coroutine parpadeoCoroutine;

    void Awake()
    {
        texto = GetComponent<TextMeshProUGUI>();
        colorOriginal = texto.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (parpadeoCoroutine == null)
        {
            parpadeoCoroutine = StartCoroutine(ParpadearRojo());
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (parpadeoCoroutine != null)
        {
            StopCoroutine(parpadeoCoroutine);
            parpadeoCoroutine = null;
            texto.color = colorOriginal;
        }
    }

    System.Collections.IEnumerator ParpadearRojo()
    {
        while (true)
        {
            texto.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            texto.color = colorOriginal;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
